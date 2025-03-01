using DarwinSimulator.model.records;
using DarwinSimulator.model;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DarwinSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public MenuWindow()
        {
            InitializeComponent();
        }
        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WorldParameters worldParameters = new WorldParameters(
                    int.Parse(WorldWidthTextBox.Text),
                    int.Parse(WorldHeightTextBox.Text),
                    (MapType)MapTypeComboBox.SelectedItem,
                    (PlanterType)PlanterTypeComboBox.SelectedItem,
                    int.Parse(StartingPlantCountTextBox.Text),
                    int.Parse(EnergyForEatingTextBox.Text),
                    int.Parse(DailyPlantGrowTextBox.Text),
                    int.Parse(StartingAnimalCountTextBox.Text),
                    int.Parse(NewFirePeriodTextBox.Text),
                    int.Parse(FireDurationTextBox.Text));

                GenomeParameters genomeParameters = new GenomeParameters(
                    int.Parse(MinMutationCountTextBox.Text),
                    int.Parse(MaxMutationCountTextBox.Text),
                    (GenomeType)GenomeTypeComboBox.SelectedItem,
                    int.Parse(GenomeLengthTextBox.Text));

                AnimalParameters animalParameters = new AnimalParameters(
                    int.Parse(StartingEnergyLevelTextBox.Text),
                    int.Parse(MinEnergyForReproducingTextBox.Text),
                    int.Parse(EnergyUsedForReproducingTextBox.Text),
                    (AnimalType)AnimalTypeComboBox.SelectedItem);

                Parameters parameters = new Parameters(animalParameters, genomeParameters, worldParameters);

                Simulation simulation = new Simulation(parameters);
                SimulationWindow simulationWindow = new SimulationWindow();
                simulationWindow.Show();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}