using System;

namespace Cloudbash.Domain.Events
{
    public class Header
    {
        public Guid CorrelationId { get; private set; }
        public string UserName { get; private set; }

        public Header(Guid correlationId, string userName)
        {
            this.CorrelationId = correlationId;
            this.UserName = userName;
        }
    }
}
