using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using DarwinSimulator.model;

namespace DarwinSimulator
{
    internal class Simulation
    {
        public WorldMap WorldMap { get; }
        
        private int day = 0;
        private bool isRunning = false;

        public Simulation(Parameters parameters)
        { 
            WorldMap = WorldMapFactory.CreateWorld(parameters);
        }

        public void Run()
        {
            while(!isRunning)
            {
                WorldMap.PassDay(day);
                day++;
            }
            try 
            {
                Thread.Sleep(1000);
            } 
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void SwitchRunningState()
        {
            isRunning = !isRunning;
        }
    }
}
