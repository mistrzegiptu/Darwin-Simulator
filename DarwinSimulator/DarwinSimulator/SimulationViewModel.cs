using DarwinSimulator.model;
using DarwinSimulator.model.records;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DarwinSimulator
{
    public class SimulationViewModel : INotifyPropertyChanged
    {
        public ICommand SwitchSimulationStateCommand { get; }
        private readonly Simulation _simulation;
        public ObservableCollection<GridCellViewModel> GridCells { get; } = new();

        private WorldStats _worldStats;
        public WorldStats WorldStats
        {
            get => _worldStats;
            set
            {
                _worldStats = value;
                OnPropertyChanged();
            }
        }

        public int Height { get; }
        public int Width { get; }

        public SimulationViewModel(int width, int height, Simulation simulation)
        {
            Width = width;
            Height = height;

            CreateGrid(width, height);
            
            SwitchSimulationStateCommand = new Command(SwitchSimulationState);
            
            _simulation = simulation;
            _simulation.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(Simulation.WorldStats))
                    WorldStats = _simulation.WorldStats;

                UpdateGrid();
            };
        }

        private void CreateGrid(int width, int height)
        {
            GridCells.Clear();

            for(int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    GridCells.Add(new GridCellViewModel(new Vector2d(x, y)));
                }
            }
        }

        private void UpdateGrid()
        {
            foreach(var cell in GridCells)
            {
                var element = _simulation.WorldMap.ObjectAt(cell.Position);
                cell.Update(element);
            }
        }

        private void SwitchSimulationState()
        {
            _simulation.SwitchRunningState();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName]string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
