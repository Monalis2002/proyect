using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.Linq;
using EcoDrive.Core;
using EcoDrive.Data;

namespace EcoDrive.GUI
{
    public partial class MainWindow : Window
    {
        private readonly LogisticsManager manager;
        private ObservableCollection<string> vehiclesList;
        private ObservableCollection<string> packagesList;
        private ObservableCollection<string> logsList;

        public MainWindow()
        {
            InitializeComponent();
            manager = new LogisticsManager();
            vehiclesList = new ObservableCollection<string>();
            packagesList = new ObservableCollection<string>();
            logsList = new ObservableCollection<string>();

            VehiclesListBox.ItemsSource = vehiclesList;
            PackagesListBox.ItemsSource = packagesList;
            LogsListBox.ItemsSource = logsList;

            InitializeSampleData();
            UpdateUI();
        }

        private void InitializeSampleData()
        {
            manager.AddVehicle(new Truck("TRUCK-001", 5000, 2));
            manager.AddVehicle(new Truck("TRUCK-002", 8000, 3));
            manager.AddVehicle(new Van("VAN-001", 1500, true));
            manager.AddVehicle(new Van("VAN-002", 1500, false));

            AddLog("✓ Loaded 4 vehicles into system");

            manager.AddPackage(new StandardPackage("PKG-001", 50, "Madrid", "Barcelona"));
            manager.AddPackage(new StandardPackage("PKG-002", 75, "Barcelona", "Valencia"));
            manager.AddPackage(new PriorityPackage("PKG-003", 30, "Valencia", "Madrid", true, true));
            manager.AddPackage(new StandardPackage("PKG-004", 120, "Madrid", "Seville"));
            manager.AddPackage(new PriorityPackage("PKG-005", 25, "Barcelona", "Seville", false, true));

            AddLog("✓ Loaded 5 packages into system");

            manager.AddRoute("Madrid", "Barcelona", 620);
            manager.AddRoute("Barcelona", "Valencia", 360);
            manager.AddRoute("Valencia", "Madrid", 360);
            manager.AddRoute("Madrid", "Seville", 540);
            manager.AddRoute("Barcelona", "Seville", 850);

            AddLog("✓ Loaded delivery routes");
        }

        private void UpdateUI()
        {
            vehiclesList.Clear();
            foreach (var vehicle in manager.GetVehicles())
            {
                vehiclesList.Add(vehicle.ToString());
            }

            packagesList.Clear();
            foreach (var package in manager.GetPackages())
            {
                packagesList.Add(package.ToString());
            }

            TotalVehiclesLabel.Content = $"Total Vehicles: {manager.GetVehiclesCount()}";
            TotalPackagesLabel.Content = $"Total Packages: {manager.GetPackagesCount()}";
            TotalRoutesLabel.Content = $"Total Routes: {manager.GetRoutesCount()}";
        }

        private void AddLog(string message)
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            logsList.Insert(0, $"[{timestamp}] {message}");
            if (logsList.Count > 100)
                logsList.RemoveAt(logsList.Count - 1);
        }

        private void AssignPackagesButton_Click(object sender, RoutedEventArgs e)
        {
            manager.OptimizePackageDistribution();
            AddLog("✓ Packages optimally distributed across vehicles");
            UpdateUI();
        }

        private void SortPackagesButton_Click(object sender, RoutedEventArgs e)
        {
            manager.SortPackagesByWeight();
            AddLog("✓ Packages sorted by weight using QuickSort");
            UpdateUI();
        }

        private void CalculateCostsButton_Click(object sender, RoutedEventArgs e)
        {
            double totalCost = manager.CalculateTotalShippingCosts();
            TotalCostLabel.Content = $"Total Shipping Cost: ${totalCost:F2}";
            AddLog($"✓ Calculated total shipping cost: ${totalCost:F2}");
        }

        private void FindRouteButton_Click(object sender, RoutedEventArgs e)
        {
            double shortestDistance = manager.FindShortestRoute("Madrid", "Barcelona");
            if (shortestDistance > 0)
            {
                ResultTextBlock.Text = $"Shortest route from Madrid to Barcelona: {shortestDistance} km";
                AddLog($"✓ Found shortest route: {shortestDistance} km");
            }
            else
            {
                ResultTextBlock.Text = "Route not found";
                AddLog("✗ Route calculation failed");
            }
        }

        private void ClearLogsButton_Click(object sender, RoutedEventArgs e)
        {
            logsList.Clear();
            AddLog("Logs cleared");
        }

        private void GenerateReportButton_Click(object sender, RoutedEventArgs e)
        {
            var report = manager.GenerateSystemReport();
            ResultTextBlock.Text = report;
            AddLog("✓ Generated comprehensive system report");
        }
    }
}
