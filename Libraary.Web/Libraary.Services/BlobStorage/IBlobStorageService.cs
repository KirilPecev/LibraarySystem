namespace Libraary.Services.BlobStorage
{
    public interface IBlobStorageService
    {
        string UploadFileToBlob(string strFileName, byte[] fileData, string fileMimeType);

        void DeleteBlobData(string fileUrl);
    }
}
