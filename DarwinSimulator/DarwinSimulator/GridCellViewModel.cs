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

namespace DarwinSimulator
{
    internal class GridCellViewModel : INotifyPropertyChanged
    {
        public Vector2d Position { get; }

        private Brush _color;
        public Brush Color
        {
            get => _color;
            set
            {
                _color = value;
                OnPropertyChanged();    
            }
        }

        public GridCellViewModel(Vector2d position)
        {
            Position = position;
            _color = Brushes.LightGray;
        }

        public void Update(IWorldElement? element)
        {
            if (element is Animal)
                Color = Brushes.Brown;
            else if (element is Plant)
                Color = Brushes.Green;
            else
                Color = Brushes.LightGray;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
