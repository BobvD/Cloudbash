using System;

namespace Cloudbash.Domain.EventStore
{
    public interface IEventRecord
    {
		/// <summary>
		/// Id of the Aggregate connected to this event.
		/// </summary>
		Guid AggregateId { get; }

		/// <summary>
		/// Name (type) of the event.
		/// </summary>
		string EventType { get; }

		/// <summary>
		/// The actual event data stored as an array of bytes.
		/// </summary>
		string Data { get; }

		/// <summary>
		/// Position of the event.
		/// </summary>
		long EventVersion { get; }

		/// <summary>
		/// Time of the event being commited.
		/// </summary>
		DateTime Created { get; }
	}
}
