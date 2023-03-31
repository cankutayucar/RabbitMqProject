using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using NwtCoreAppExcel.UI.Hubs;
using NwtCoreAppExcel.UI.Models;

namespace NwtCoreAppExcel.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHubContext<MyHub> _hubContext;
        public FilesController(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment, IHubContext<MyHub> hubContext)
        {
            _appDbContext = appDbContext;
            _webHostEnvironment = webHostEnvironment;
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file, int fileId)
        {
            if (file is not { Length: > 0 }) return BadRequest();
            var userFile = await _appDbContext.UserFiles.FirstAsync(x => x.Id == fileId);
            var filePath = userFile.FileName + Path.GetExtension(file.FileName);
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "files", filePath);
            await using FileStream stream = new(path, FileMode.Create);
            await file.CopyToAsync(stream);
            userFile.CreatedDate = DateTime.Now;
            userFile.FilePath = filePath;
            userFile.FileStatus = FileStatus.Completed;
            await _appDbContext.SaveChangesAsync();
            // signalr 
            await _hubContext.Clients.User(userFile.UserId).SendAsync("completedFile");
            return Ok();
        }
    }
}
