namespace Cloudbash.Application.Common.Interfaces
{
    public interface IFileService
    {
        string GetUploadUrl(string filename, string type);
    }
}
