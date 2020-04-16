using Cloudbash.Domain.EventStore;
using System;

namespace Cloudbash.Domain.SeedWork
{
    public class EventRecord : IEventRecord
    {
		/// <summary>
		/// Id of the Aggregate connected to this event.
		/// </summary>
		public Guid AggregateId { get; private set; }

		/// <summary>
		/// Name (type) of the event.
		/// </summary>
		public string EventType { get; private set; }

		/// <summary>
		/// The actual event data stored as an array of bytes.
		/// </summary>
		public string Data { get; private set; }

		/// <summary>
		/// Position of the event.
		/// </summary>
		public long AggregateVersion { get; private set; }

		/// <summary>
		/// Time of the event being commited.
		/// </summary>
		public DateTime Created { get; private set; }

		public EventRecord(Guid aggregateId, string eventType, string data, long aggregateVersion)
		{
			AggregateId = aggregateId;
			EventType = eventType;
			Data = data;
			AggregateVersion = aggregateVersion;
			Created = DateTime.Now;
			Console.WriteLine("AGGREGATE VERSION: " + aggregateVersion);
		}
	}
}
