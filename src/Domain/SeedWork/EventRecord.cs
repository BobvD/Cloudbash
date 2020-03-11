using System;

namespace Cloudbash.Domain.SeedWork
{
    public class EventRecord
    {	
		public readonly string EventType;
		public readonly byte[] Data;
		public readonly DateTime Created;

		public EventRecord(string eventType, byte[] data, DateTime created)
		{
			EventType = eventType;
			Data = data;
			Created = created;
		}

	}
}
