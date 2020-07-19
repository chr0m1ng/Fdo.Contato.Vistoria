using Plugin.FileUploader.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace Fdo.Contato.Vistoria.Models.Requests
{
    public class UploadImagesRequest : BaseUploadRequest
    {
        public IEnumerable<FilePathItem> FilePathItems { get; set; }

        public UploadImagesRequest(IEnumerable<string> imagePaths)
        {
            FilePathItems = imagePaths.Select(ip => new FilePathItem(REQUEST_FILE_FIELD_NAME, ip));
        }
    }
}
