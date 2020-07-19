using Plugin.Media.Abstractions;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Fdo.Contato.Vistoria.Models
{
    public class VehicleImage : INotifyPropertyChanged
    {
        public VehicleImage()
        {
            Tag = Guid.NewGuid().ToString();
        }

        public VehicleImage(Guid tag)
        {
            Tag = tag.ToString();
        }

        public MediaFile Image { get; set; }

        public string Name { get; set; }

        public string Tag { get; set; }

        public double UploadProgress 
        {
            get => _uploadProgress;
            set
            {
                if (_uploadProgress != value)
                {
                    _uploadProgress = value;
                    OnPropertyChanged(nameof(UploadProgress));
                }
            }
        }
        private double _uploadProgress { get; set; }

        private string _statusImage { get; set; }
        public string StatusImage
        {
            get => _statusImage;
            set
            {
                if (_statusImage != value)
                {
                    _statusImage = value;
                    OnPropertyChanged(nameof(StatusImage));
                }
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
