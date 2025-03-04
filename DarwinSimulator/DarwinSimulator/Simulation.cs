using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using DarwinSimulator.model;
using System.ComponentModel;
using DarwinSimulator.model.records;

namespace DarwinSimulator
{
    internal class Simulation : INotifyPropertyChanged
    {
        public WorldMap WorldMap { get; }
        private WorldStats _worldStats;
        public WorldStats WorldStats
        { 
            get => _worldStats;
            set
            {
                _worldStats = value;
                OnPropertyChanged(nameof(WorldStats));
            }
        }
        
        private int day = 0;
        private bool isRunning = false;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Simulation(Parameters parameters)
        { 
            WorldMap = WorldMapFactory.CreateWorld(parameters);
        }

        public void Run()
        {
            while (!isRunning)
            {
                WorldMap.PassDay(day);
                day++;
                WorldStats = WorldMap.WorldStats;
                try
                {
                    Thread.Sleep(1000);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public void SwitchRunningState()
        {
            isRunning = !isRunning;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
