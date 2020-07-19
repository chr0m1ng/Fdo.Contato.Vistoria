using Plugin.FileUploader.Abstractions;

namespace Fdo.Contato.Vistoria.Models.Requests
{
    public class UploadImageRequest : BaseUploadRequest
    {
        public FilePathItem FilePathItem { get; set; }

        public UploadImageRequest(string imagePath)
        {
            FilePathItem = new FilePathItem(REQUEST_FILE_FIELD_NAME, imagePath);
        }
    }
}
