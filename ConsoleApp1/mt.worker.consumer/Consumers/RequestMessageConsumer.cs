using MassTransit;
using Shared.RequestResponseMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mt.worker.consumer.Consumers
{
    public class RequestMessageConsumer : IConsumer<RequestMessage>
    {
        public Task Consume(ConsumeContext<RequestMessage> context)
        {
            throw new NotImplementedException();
        }
    }
}
