using System.Threading.Tasks;

namespace Cloudbash.Infrastructure.Firehose
{
    public interface IFirehoseClient
    {
        Task WriteAsync(byte[] data);
    }
}
