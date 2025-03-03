using DarwinSimulator.model.records;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DarwinSimulator
{
    internal class SimulationViewModel : INotifyPropertyChanged
    {
        public ICommand SwitchSimulationStateCommand { get; }
        private Simulation _simulation;

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

        public SimulationViewModel(int width, int height, Simulation simulation)
        {
            CreateGrid(width, height);
            SwitchSimulationStateCommand = new Command(SwitchSimulationState);
            _simulation = simulation;
        }

        private void CreateGrid(int width, int height)
        {

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
