using DarwinSimulator.model.records;
using DarwinSimulator.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DarwinSimulator
{
    internal class MenuViewModel
    {
        public int WorldWidth { get; set; } = 10;
        public int WorldHeight { get; set; } = 10;
        public MapType SelectedMapType { get; set; }
        public PlanterType SelectedPlanterType { get; set; }
        public int StartingPlantCount { get; set; } = 10;
        public int EnergyForEating { get; set; } = 2;
        public int DailyPlantGrow { get; set; } = 10;
        public int StartingAnimalCount { get; set; } = 20;
        public int NewFirePeriod { get; set; } = 0;
        public int FireDuration { get; set; } = 0;

        public int MinMutationCount { get; set; } = 1;
        public int MaxMutationCount { get; set; } = 5;
        public GenomeType SelectedGenomeType { get; set; }
        public int GenomeLength { get; set; } = 10;

        public int StartingEnergyLevel { get; set; } = 25;
        public int MinEnergyForReproducing { get; set; } = 10;
        public int EnergyUsedForReproducing { get; set; } = 5;
        public AnimalType SelectedAnimalType { get; set; }


        public ICommand RunSimulationCommand { get; }

        public MenuViewModel()
        {
            RunSimulationCommand = new Command(RunSimulation);
        }

        private void RunSimulation()
        {
            try
            {
                WorldParameters worldParameters = new(WorldWidth, WorldHeight, SelectedMapType, SelectedPlanterType, StartingPlantCount,
                    EnergyForEating, DailyPlantGrow, StartingAnimalCount, NewFirePeriod, FireDuration);
                AnimalParameters animalParameters = new(StartingEnergyLevel, MinEnergyForReproducing, EnergyUsedForReproducing, SelectedAnimalType);
                GenomeParameters genomeParameters = new(MinMutationCount, MaxMutationCount, SelectedGenomeType, GenomeLength);

                Parameters parameters = new(animalParameters, genomeParameters, worldParameters);
                Simulation simulation = new Simulation(parameters);
                SimulationWindow simulationWindow = new SimulationWindow(parameters.WorldParameters.Width, parameters.WorldParameters.Height, simulation);
                simulationWindow.Show();

                //Task task = Task.Run(() => simulation.Run());
                Thread thread = new Thread(() => simulation.Run());
                thread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
