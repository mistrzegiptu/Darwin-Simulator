using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using DarwinSimulator.model;
using System.ComponentModel;
using DarwinSimulator.model.records;
using System.Runtime.CompilerServices;

namespace DarwinSimulator
{
    internal class Simulation : INotifyPropertyChanged
    {
        public WorldMap WorldMap { get; }
        private WorldStats _worldStats;

        private Thread? simulationThread;

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
        private bool isRunning = true;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Simulation(Parameters parameters)
        { 
            WorldMap = WorldMapFactory.CreateWorld(parameters);
        }

        public void Run()
        {
            simulationThread = new Thread(() =>
            {
                while (isRunning)
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
            });

            simulationThread.IsBackground = true;
            simulationThread.Start();
        }

        public void SwitchRunningState()
        {
            isRunning = !isRunning;

            if (isRunning && (simulationThread == null || !simulationThread.IsAlive))
            {
                Run(); // Restart simulation if it's stopped
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
