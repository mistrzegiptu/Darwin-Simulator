using DarwinSimulator.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DarwinSimulator
{
    public class GridCellViewModel : INotifyPropertyChanged
    {
        public Vector2d Position { get; }

        private BitmapImage? _image;
        private static readonly Dictionary<string, BitmapImage> _imageCache = new();

        public BitmapImage? Image
        {
            get => _image;
            set
            {
                _image = value;
                OnPropertyChanged();
            }
        }

        public GridCellViewModel(Vector2d position)
        {
            Position = position;
        }

        public void Update(IWorldElement? element)
        {
            Image = element != null ? LoadImage(element.GetImageFileName()) : null;
        }

        private BitmapImage LoadImage(string path)
        {
            if(!_imageCache.TryGetValue(path, out var image))
            {
                string resourcePath = $"pack://application:,,,/DarwinSimulator;component/resources/{path}";
                image = new BitmapImage(new Uri(resourcePath, UriKind.Absolute));
                image.DecodePixelHeight = 32;
                image.DecodePixelWidth = 32;
                image.CacheOption = BitmapCacheOption.OnLoad;

                _imageCache.Add(path, image);
            }

            return image;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
