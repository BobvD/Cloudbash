using System;

namespace Cloudbash.Domain.SeedWork
{
    public class EventRecord
    {
		public readonly Guid AggregateId;

		/// <summary>
		/// Name (type) of the event.
		/// </summary>
		public readonly string EventType;

		/// <summary>
		/// The actual event data stored as an array of bytes.
		/// </summary>
		public readonly string Data;

		/// <summary>
		/// Position of the event.
		/// </summary>
		public readonly long EventVersion;

		/// <summary>
		/// Time of the event being commited.
		/// </summary>
		public readonly DateTime Created;
		public EventRecord(Guid aggregateId, string eventType, string data, long eventVersion)
		{
			AggregateId = aggregateId;
			EventType = eventType;
			Data = data;
			EventVersion = eventVersion;
			Created = DateTime.Now;
		}
	}
}
