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
        public int WorldWidth { get; set; }
        public int WorldHeight { get; set; }
        public MapType SelectedMapType { get; set; }
        public PlanterType SelectedPlanterType { get; set; }
        public int StartingPlantCount { get; set; }
        public int EnergyForEating { get; set; }
        public int DailyPlantGrow { get; set; }
        public int StartingAnimalCount { get; set; }
        public int NewFirePeriod { get; set; }
        public int FireDuration { get; set; }

        public int MinMutationCount { get; set; }
        public int MaxMutationCount { get; set; }
        public GenomeType SelectedGenomeType { get; set; }
        public int GenomeLength { get; set; }

        public int StartingEnergyLevel { get; set; }
        public int MinEnergyForReproducing { get; set; }
        public int EnergyUsedForReproducing { get; set; }
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
