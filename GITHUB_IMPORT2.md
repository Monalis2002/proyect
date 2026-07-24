# EcoDrive-Manager

Proyecto completo de gestión logística en C# con WPF, programación orientada a objetos, estructuras dinámicas, grafos y algoritmos recursivos.

## Estructura del proyecto

- ConsoleApp1.csproj
- Program.cs
- Core/
- Data/
- GUI/

## 1) Archivo de proyecto

### ConsoleApp1.csproj
```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFramework>net9.0-windows</TargetFramework>
    <UseWPF>true</UseWPF>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <RootNamespace>EcoDrive</RootNamespace>
  </PropertyGroup>

</Project>
```

## 2) Punto de entrada

### Program.cs
```csharp
using EcoDrive.GUI;
using System.Windows;

// Launch the EcoDrive-Manager WPF application
var app = new App();
app.Run();
```

## 3) Aplicación WPF

### GUI/App.xaml
```xml
<Application x:Class="EcoDrive.GUI.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             StartupUri="MainWindow.xaml">
    <Application.Resources>
    </Application.Resources>
</Application>
```

### GUI/App.xaml.cs
```csharp
using System.Windows;

namespace EcoDrive.GUI
{
    public partial class App : Application
    {
    }
}
```

### GUI/MainWindow.xaml
```xml
<Window x:Class="EcoDrive.GUI.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="EcoDrive-Manager: Logistics Management System"
        Height="800"
        Width="1200"
        Background="#F0F0F0"
        WindowStartupLocation="CenterScreen">
    <Grid>
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="1*"/>
            <ColumnDefinition Width="1*"/>
            <ColumnDefinition Width="1*"/>
        </Grid.ColumnDefinitions>
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="1*"/>
            <RowDefinition Height="Auto"/>
        </Grid.RowDefinitions>

        <!-- Header -->
        <Border Grid.Row="0" Grid.ColumnSpan="3" Background="#2C3E50" Padding="20">
            <StackPanel>
                <TextBlock Text="EcoDrive-Manager: Intelligent Logistics Simulation System"
                           FontSize="24" FontWeight="Bold" Foreground="White" Margin="0,0,0,10"/>
                <TextBlock Text="Advanced routing, package management, and vehicle optimization"
                           FontSize="12" Foreground="#ECF0F1"/>
            </StackPanel>
        </Border>

        <!-- Statistics Panel -->
        <Border Grid.Row="1" Grid.Column="0" Margin="10" Background="White" BorderBrush="#BDC3C7" BorderThickness="1">
            <StackPanel Margin="15">
                <TextBlock Text="System Statistics" FontSize="14" FontWeight="Bold" Margin="0,0,0,10" Foreground="#2C3E50"/>
                <Label x:Name="TotalVehiclesLabel" Content="Total Vehicles: 0" Margin="0,5" Padding="5,0"/>
                <Label x:Name="TotalPackagesLabel" Content="Total Packages: 0" Margin="0,5" Padding="5,0"/>
                <Label x:Name="TotalRoutesLabel" Content="Total Routes: 0" Margin="0,5" Padding="5,0"/>
                <Label x:Name="TotalCostLabel" Content="Total Shipping Cost: $0.00" Margin="0,5" Padding="5,0" FontWeight="Bold" Foreground="#27AE60"/>
                <Separator Margin="0,10"/>
                <TextBlock Text="Fleet Vehicles" FontSize="12" FontWeight="Bold" Margin="0,10,0,5" Foreground="#2C3E50"/>
                <ListBox x:Name="VehiclesListBox" Height="180" Margin="0,5" BorderBrush="#BDC3C7" BorderThickness="1"/>
            </StackPanel>
        </Border>

        <!-- Packages Panel -->
        <Border Grid.Row="1" Grid.Column="1" Margin="10" Background="White" BorderBrush="#BDC3C7" BorderThickness="1">
            <StackPanel Margin="15">
                <TextBlock Text="Inventory Management" FontSize="14" FontWeight="Bold" Margin="0,0,0,10" Foreground="#2C3E50"/>
                <TextBlock Text="Active Packages:" FontSize="11" FontWeight="Bold" Margin="0,0,0,5"/>
                <ListBox x:Name="PackagesListBox" Height="280" Margin="0,5" BorderBrush="#BDC3C7" BorderThickness="1"/>
            </StackPanel>
        </Border>

        <!-- Logs and Results Panel -->
        <Border Grid.Row="1" Grid.Column="2" Margin="10" Background="White" BorderBrush="#BDC3C7" BorderThickness="1">
            <StackPanel Margin="15">
                <TextBlock Text="System Logs and Results" FontSize="14" FontWeight="Bold" Margin="0,0,0,10" Foreground="#2C3E50"/>
                <ListBox x:Name="LogsListBox" Height="220" Margin="0,5" BorderBrush="#BDC3C7" BorderThickness="1" Background="#FAFAFA"/>
                <Border Background="#F8F8F8" BorderBrush="#BDC3C7" BorderThickness="1" Margin="0,10,0,0" Padding="8" MinHeight="40">
                    <TextBlock x:Name="ResultTextBlock" Text="Results will appear here..." TextWrapping="Wrap" FontSize="11"/>
                </Border>
            </StackPanel>
        </Border>

        <!-- Control Buttons Panel -->
        <Grid Grid.Row="2" Grid.ColumnSpan="3" Background="White" Margin="0">
            <StackPanel Orientation="Horizontal" Margin="10">
                <Button Content="Optimize Distribution" Click="AssignPackagesButton_Click" 
                        Background="#3498DB" Foreground="White" Padding="10,8" Width="140" Cursor="Hand" Margin="2"/>
                <Button Content="Sort Packages" Click="SortPackagesButton_Click" 
                        Background="#9B59B6" Foreground="White" Padding="10,8" Width="110" Cursor="Hand" Margin="2"/>
                <Button Content="Calculate Costs" Click="CalculateCostsButton_Click" 
                        Background="#27AE60" Foreground="White" Padding="10,8" Width="120" Cursor="Hand" Margin="2"/>
                <Button Content="Find Best Route" Click="FindRouteButton_Click" 
                        Background="#E74C3C" Foreground="White" Padding="10,8" Width="120" Cursor="Hand" Margin="2"/>
                <Button Content="Generate Report" Click="GenerateReportButton_Click" 
                        Background="#F39C12" Foreground="White" Padding="10,8" Width="125" Cursor="Hand" Margin="2"/>
                <Button Content="Clear Logs" Click="ClearLogsButton_Click" 
                        Background="#95A5A6" Foreground="White" Padding="10,8" Width="100" Cursor="Hand" Margin="2"/>
            </StackPanel>
        </Grid>
    </Grid>
</Window>
```

### GUI/MainWindow.xaml.cs
```csharp
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.ObjectModel;
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
```

### GUI/LogisticsManager.cs
```csharp
using System.Text;
using EcoDrive.Core;
using EcoDrive.Data;

namespace EcoDrive.GUI
{
    public class LogisticsManager
    {
        private DynamicLinkedList<Vehicle> vehicles;
        private DynamicLinkedList<Package> packages;
        private LogisticsGraph routeGraph;

        public LogisticsManager()
        {
            vehicles = new DynamicLinkedList<Vehicle>();
            packages = new DynamicLinkedList<Package>();
            routeGraph = new LogisticsGraph();
        }

        public void AddVehicle(Vehicle vehicle)
        {
            vehicles.AddLast(vehicle);
        }

        public void AddPackage(Package package)
        {
            packages.AddLast(package);
        }

        public void AddRoute(string fromCity, string toCity, double distanceKm)
        {
            routeGraph.AddRoute(fromCity, toCity, distanceKm);
        }

        public List<Vehicle> GetVehicles()
        {
            return vehicles.ToArray().ToList();
        }

        public List<Package> GetPackages()
        {
            return packages.ToArray().ToList();
        }

        public int GetVehiclesCount() => vehicles.Count;

        public int GetPackagesCount() => packages.Count;

        public int GetRoutesCount() => routeGraph.GetRouteCount();

        public void OptimizePackageDistribution()
        {
            var vehicleArray = vehicles.ToArray();
            var packageArray = packages.ToArray();

            if (vehicleArray.Length == 0 || packageArray.Length == 0)
                return;

            foreach (var vehicle in vehicleArray)
            {
                foreach (var package in packageArray)
                {
                    if (package.Status == PackageStatus.Pending)
                    {
                        if (vehicle.AddPackage(package))
                        {
                            package.Status = PackageStatus.InTransit;
                        }
                    }
                }
            }
        }

        public void SortPackagesByWeight()
        {
            var packageArray = packages.ToArray();
            if (packageArray.Length > 1)
            {
                LogisticsAlgorithms.QuickSortByWeight(packageArray, 0, packageArray.Length - 1);
            }
        }

        public double CalculateTotalShippingCosts()
        {
            double totalCost = 0;
            var vehicleArray = vehicles.ToArray();

            foreach (var vehicle in vehicleArray)
            {
                if (vehicle.Packages.Count > 0)
                {
                    double averageDistance = 100;
                    double vehicleCost = LogisticsAlgorithms.CalculateTotalShippingCost(
                        vehicle.Packages.ToArray(), vehicle, averageDistance);
                    totalCost += vehicleCost;
                }
            }

            return totalCost;
        }

        public double FindShortestRoute(string origin, string destination)
        {
            return routeGraph.GetShortestDistance(origin, destination);
        }

        public string GenerateSystemReport()
        {
            var sb = new StringBuilder();
            sb.AppendLine("═══════════════════════════════════════════════════════════");
            sb.AppendLine("         EcoDrive-Manager System Report");
            sb.AppendLine("═══════════════════════════════════════════════════════════");
            sb.AppendLine();
            sb.AppendLine("📊 FLEET OVERVIEW:");
            sb.AppendLine($"  Total Vehicles: {vehicles.Count}");
            var vehicleArray = vehicles.ToArray();
            double totalCapacity = vehicleArray.Sum(v => v.MaxCapacityKg);
            double totalLoad = vehicleArray.Sum(v => v.Packages.Sum(p => p.WeightKg));
            sb.AppendLine($"  Total Capacity: {totalCapacity:F2} kg");
            sb.AppendLine($"  Current Load: {totalLoad:F2} kg");
            sb.AppendLine($"  Utilization: {(totalLoad / totalCapacity * 100):F1}%");
            sb.AppendLine();
            sb.AppendLine("📦 INVENTORY STATUS:");
            sb.AppendLine($"  Total Packages: {packages.Count}");
            var packageArray = packages.ToArray();
            var pendingCount = packageArray.Count(p => p.Status == PackageStatus.Pending);
            var intransitCount = packageArray.Count(p => p.Status == PackageStatus.InTransit);
            var deliveredCount = packageArray.Count(p => p.Status == PackageStatus.Delivered);
            sb.AppendLine($"  Pending: {pendingCount} | In Transit: {intransitCount} | Delivered: {deliveredCount}");
            sb.AppendLine();
            sb.AppendLine("🗺️ ROUTE NETWORK:");
            var cities = routeGraph.GetAllCities();
            sb.AppendLine($"  Total Cities: {cities.Count}");
            sb.AppendLine($"  Total Routes: {routeGraph.GetRouteCount()}");
            sb.AppendLine($"  Cities: {string.Join(", ", cities)}");
            sb.AppendLine();
            sb.AppendLine("💰 COST ANALYSIS:");
            double totalCosts = CalculateTotalShippingCosts();
            sb.AppendLine($"  Total Shipping Cost: ${totalCosts:F2}");
            sb.AppendLine($"  Average Cost per Package: ${(totalCosts / (packages.Count > 0 ? packages.Count : 1)):F2}");
            sb.AppendLine();
            sb.AppendLine("═══════════════════════════════════════════════════════════");

            return sb.ToString();
        }
    }
}
```

## 4) Lógica central

### Core/Vehicle.cs
```csharp
namespace EcoDrive.Core
{
    public abstract class Vehicle
    {
        public string VehicleId { get; protected set; }
        public string Name { get; protected set; }
        public double MaxCapacityKg { get; protected set; }
        public double CurrentLoadKg { get; private set; }
        public double FuelCostPerKm { get; protected set; }
        public VehicleStatus Status { get; set; }
        public List<Package> Packages { get; private set; }

        protected Vehicle(string vehicleId, string name, double maxCapacity, double fuelCost)
        {
            VehicleId = vehicleId;
            Name = name;
            MaxCapacityKg = maxCapacity;
            FuelCostPerKm = fuelCost;
            CurrentLoadKg = 0;
            Status = VehicleStatus.Idle;
            Packages = new List<Package>();
        }

        public bool AddPackage(Package package)
        {
            if (CurrentLoadKg + package.WeightKg <= MaxCapacityKg)
            {
                Packages.Add(package);
                CurrentLoadKg += package.WeightKg;
                return true;
            }
            return false;
        }

        public bool RemovePackage(Package package)
        {
            if (Packages.Remove(package))
            {
                CurrentLoadKg -= package.WeightKg;
                return true;
            }
            return false;
        }

        public abstract double CalculateShippingCost(double distance);
        public abstract double CalculateShippingCost(double distance, DeliveryPriority priority);
        public abstract double CalculateShippingCost(double distance, DeliveryPriority priority, WeatherCondition weather);

        public override string ToString()
        {
            return $"{Name} (ID: {VehicleId}) - Load: {CurrentLoadKg:F2}/{MaxCapacityKg:F2} kg - Status: {Status}";
        }
    }

    public enum VehicleStatus
    {
        Idle,
        InTransit,
        Loading,
        Unloading,
        Maintenance
    }
}
```

### Core/Truck.cs
```csharp
namespace EcoDrive.Core
{
    public class Truck : Vehicle
    {
        public int AxleCount { get; private set; }
        public double MaintenanceCostPerKm { get; private set; }

        public Truck(string vehicleId, double maxCapacity = 5000, int axleCount = 2)
            : base(vehicleId, $"Truck-{axleCount}Axle", maxCapacity, fuelCost: 0.25)
        {
            AxleCount = axleCount;
            MaintenanceCostPerKm = 0.05;
        }

        public override double CalculateShippingCost(double distance)
        {
            double baseCost = distance * FuelCostPerKm;
            double maintenanceCost = distance * MaintenanceCostPerKm;
            double loadCost = (CurrentLoadKg / MaxCapacityKg) * 10;
            return baseCost + maintenanceCost + loadCost;
        }

        public override double CalculateShippingCost(double distance, DeliveryPriority priority)
        {
            double baseCost = CalculateShippingCost(distance);
            double priorityMultiplier = priority switch
            {
                DeliveryPriority.Standard => 1.0,
                DeliveryPriority.Express => 1.5,
                DeliveryPriority.NextDay => 1.25,
                _ => 1.0
            };
            return baseCost * priorityMultiplier;
        }

        public override double CalculateShippingCost(double distance, DeliveryPriority priority, WeatherCondition weather)
        {
            double baseCost = CalculateShippingCost(distance, priority);
            double weatherMultiplier = weather switch
            {
                WeatherCondition.Clear => 1.0,
                WeatherCondition.Rainy => 1.1,
                WeatherCondition.Snowy => 1.35,
                WeatherCondition.Stormy => 1.6,
                _ => 1.0
            };
            return baseCost * weatherMultiplier;
        }
    }
}
```

### Core/Van.cs
```csharp
namespace EcoDrive.Core
{
    public class Van : Vehicle
    {
        public double BatteryCapacityKwh { get; private set; }
        public bool IsElectric { get; private set; }

        public Van(string vehicleId, double maxCapacity = 1500, bool isElectric = true)
            : base(vehicleId, isElectric ? "Electric-Van" : "Fuel-Van", maxCapacity, 
                fuelCost: isElectric ? 0.08 : 0.15)
        {
            IsElectric = isElectric;
            BatteryCapacityKwh = isElectric ? 100 : 0;
        }

        public override double CalculateShippingCost(double distance)
        {
            double baseCost = distance * FuelCostPerKm;
            double loadCost = (CurrentLoadKg / MaxCapacityKg) * 5;
            
            if (IsElectric)
            {
                double chargingCost = distance / 5.0;
                return baseCost + loadCost + chargingCost;
            }
            return baseCost + loadCost;
        }

        public override double CalculateShippingCost(double distance, DeliveryPriority priority)
        {
            double baseCost = CalculateShippingCost(distance);
            double priorityMultiplier = priority switch
            {
                DeliveryPriority.Standard => 1.0,
                DeliveryPriority.Express => 1.4,
                DeliveryPriority.NextDay => 1.1,
                _ => 1.0
            };
            return baseCost * priorityMultiplier;
        }

        public override double CalculateShippingCost(double distance, DeliveryPriority priority, WeatherCondition weather)
        {
            double baseCost = CalculateShippingCost(distance, priority);
            double weatherMultiplier = weather switch
            {
                WeatherCondition.Clear => 1.0,
                WeatherCondition.Rainy => 1.05,
                WeatherCondition.Snowy => 1.2,
                WeatherCondition.Stormy => 1.4,
                _ => 1.0
            };

            if (IsElectric)
                weatherMultiplier = Math.Min(weatherMultiplier, 1.15);

            return baseCost * weatherMultiplier;
        }
    }
}
```

### Core/Package.cs
```csharp
namespace EcoDrive.Core
{
    public abstract class Package
    {
        public string PackageId { get; protected set; }
        public string Description { get; protected set; }
        public double WeightKg { get; protected set; }
        public string OriginCity { get; protected set; }
        public string DestinationCity { get; protected set; }
        public PackageStatus Status { get; set; }
        public DateTime CreatedDate { get; protected set; }

        protected Package(string packageId, double weightKg, string origin, string destination, string description)
        {
            PackageId = packageId;
            WeightKg = weightKg;
            OriginCity = origin;
            DestinationCity = destination;
            Description = description;
            Status = PackageStatus.Pending;
            CreatedDate = DateTime.Now;
        }

        public abstract double CalculateBaseCost();

        public override string ToString()
        {
            return $"Package {PackageId}: {WeightKg}kg - {OriginCity} → {DestinationCity} - Status: {Status}";
        }
    }

    public enum PackageStatus
    {
        Pending,
        InTransit,
        Delivered,
        Failed,
        Returned
    }

    public enum DeliveryPriority
    {
        Standard,
        NextDay,
        Express
    }

    public enum WeatherCondition
    {
        Clear,
        Rainy,
        Snowy,
        Stormy
    }
}
```

### Core/StandardPackage.cs
```csharp
namespace EcoDrive.Core
{
    public class StandardPackage : Package
    {
        public const double BaseCostPerKg = 1.5;

        public StandardPackage(string packageId, double weightKg, string origin, string destination)
            : base(packageId, weightKg, origin, destination, "Standard Delivery")
        {
        }

        public override double CalculateBaseCost()
        {
            return WeightKg * BaseCostPerKg;
        }
    }
}
```

### Core/PriorityPackage.cs
```csharp
namespace EcoDrive.Core
{
    public class PriorityPackage : Package
    {
        public const double BaseCostPerKg = 3.0;
        public bool RequiresRefrigeration { get; private set; }
        public bool RequiresSignature { get; private set; }

        public PriorityPackage(string packageId, double weightKg, string origin, string destination, 
            bool requiresRefrigeration = false, bool requiresSignature = true)
            : base(packageId, weightKg, origin, destination, "Priority/Expedited Delivery")
        {
            RequiresRefrigeration = requiresRefrigeration;
            RequiresSignature = requiresSignature;
        }

        public override double CalculateBaseCost()
        {
            double baseCost = WeightKg * BaseCostPerKg;
            if (RequiresRefrigeration) baseCost *= 1.3;
            if (RequiresSignature) baseCost *= 1.15;
            return baseCost;
        }
    }
}
```

### Core/LogisticsAlgorithms.cs
```csharp
namespace EcoDrive.Core
{
    public static class LogisticsAlgorithms
    {
        public static void QuickSortByWeight(Package[] packages, int left, int right)
        {
            if (left < right)
            {
                int partition = PartitionByWeight(packages, left, right);
                QuickSortByWeight(packages, left, partition - 1);
                QuickSortByWeight(packages, partition + 1, right);
            }
        }

        private static int PartitionByWeight(Package[] packages, int left, int right)
        {
            double pivot = packages[right].WeightKg;
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (packages[j].WeightKg < pivot)
                {
                    i++;
                    (packages[i], packages[j]) = (packages[j], packages[i]);
                }
            }

            (packages[i + 1], packages[right]) = (packages[right], packages[i + 1]);
            return i + 1;
        }

        public static void MergeSortByPriority(Package[] packages, int left, int right)
        {
            if (left < right)
            {
                int mid = (left + right) / 2;
                MergeSortByPriority(packages, left, mid);
                MergeSortByPriority(packages, mid + 1, right);
                MergeByPriority(packages, left, mid, right);
            }
        }

        private static void MergeByPriority(Package[] packages, int left, int mid, int right)
        {
            int leftSize = mid - left + 1;
            int rightSize = right - mid;
            Package[] leftArray = new Package[leftSize];
            Package[] rightArray = new Package[rightSize];

            Array.Copy(packages, left, leftArray, 0, leftSize);
            Array.Copy(packages, mid + 1, rightArray, 0, rightSize);

            int i = 0, j = 0, k = left;
            while (i < leftSize && j < rightSize)
            {
                if (ComparePriority(leftArray[i], rightArray[j]) <= 0)
                {
                    packages[k++] = leftArray[i++];
                }
                else
                {
                    packages[k++] = rightArray[j++];
                }
            }

            while (i < leftSize)
                packages[k++] = leftArray[i++];
            while (j < rightSize)
                packages[k++] = rightArray[j++];
        }

        private static int ComparePriority(Package a, Package b)
        {
            return a.DestinationCity.Length.CompareTo(b.DestinationCity.Length);
        }

        public static int BinarySearchPackageId(Package[] packages, string packageId, int left, int right)
        {
            if (left > right)
                return -1;

            int mid = (left + right) / 2;
            int comparison = packages[mid].PackageId.CompareTo(packageId);

            if (comparison == 0)
                return mid;
            else if (comparison > 0)
                return BinarySearchPackageId(packages, packageId, left, mid - 1);
            else
                return BinarySearchPackageId(packages, packageId, mid + 1, right);
        }

        public static double CalculateOptimalDistributionCost(List<Package> packages, 
            Vehicle[] vehicles, int packageIndex, double currentCost)
        {
            if (packageIndex >= packages.Count)
                return currentCost;

            double minCost = double.MaxValue;
            Package currentPackage = packages[packageIndex];

            foreach (var vehicle in vehicles)
            {
                if (vehicle.AddPackage(currentPackage))
                {
                    double cost = currentCost + currentPackage.CalculateBaseCost();
                    double recursiveCost = CalculateOptimalDistributionCost(packages, vehicles, 
                        packageIndex + 1, cost);
                    minCost = Math.Min(minCost, recursiveCost);
                    vehicle.RemovePackage(currentPackage);
                }
            }

            return minCost == double.MaxValue ? currentCost : minCost;
        }

        public static double FindShortestDeliveryRoute(string current, string destination,
            Dictionary<string, List<(string, double)>> graph, HashSet<string> visited,
            double currentDistance = 0)
        {
            if (current == destination)
                return currentDistance;

            if (!graph.ContainsKey(current))
                return double.MaxValue;

            visited.Add(current);
            double minDistance = double.MaxValue;

            foreach (var (nextCity, distance) in graph[current])
            {
                if (!visited.Contains(nextCity))
                {
                    double result = FindShortestDeliveryRoute(nextCity, destination, graph,
                        visited, currentDistance + distance);
                    minDistance = Math.Min(minDistance, result);
                }
            }

            visited.Remove(current);
            return minDistance;
        }

        public static double CalculateTotalShippingCost(Package[] packages, Vehicle vehicle,
            double distance, int index = 0)
        {
            if (index >= packages.Length)
                return 0;

            double currentCost = packages[index].CalculateBaseCost() 
                + vehicle.CalculateShippingCost(distance / packages.Length);
            return currentCost + CalculateTotalShippingCost(packages, vehicle, distance, index + 1);
        }
    }
}
```

## 5) Estructuras de datos

### Data/LinkedListNode.cs
```csharp
namespace EcoDrive.Data
{
    public class LinkedListNode<T> where T : class
    {
        public T Data { get; set; }
        public LinkedListNode<T>? Next { get; set; }

        public LinkedListNode(T data)
        {
            Data = data;
            Next = null;
        }
    }
}
```

### Data/DynamicLinkedList.cs
```csharp
namespace EcoDrive.Data
{
    public class DynamicLinkedList<T> where T : class
    {
        private LinkedListNode<T>? head;
        private int count;

        public int Count => count;
        public bool IsEmpty => head == null;

        public DynamicLinkedList()
        {
            head = null;
            count = 0;
        }

        public void AddFirst(T data)
        {
            var newNode = new LinkedListNode<T>(data);
            newNode.Next = head;
            head = newNode;
            count++;
        }

        public void AddLast(T data)
        {
            var newNode = new LinkedListNode<T>(data);
            
            if (head == null)
            {
                head = newNode;
            }
            else
            {
                var current = head;
                while (current.Next != null)
                    current = current.Next;
                current.Next = newNode;
            }
            count++;
        }

        public T? RemoveFirst()
        {
            if (head == null)
                return null;

            T data = head.Data;
            head = head.Next;
            count--;
            return data;
        }

        public bool Remove(T data)
        {
            if (head == null)
                return false;

            if (head.Data == data)
            {
                head = head.Next;
                count--;
                return true;
            }

            var current = head;
            while (current.Next != null)
            {
                if (current.Next.Data == data)
                {
                    current.Next = current.Next.Next;
                    count--;
                    return true;
                }
                current = current.Next;
            }

            return false;
        }

        public T[] ToArray()
        {
            var array = new T[count];
            var current = head;
            int index = 0;

            while (current != null)
            {
                array[index++] = current.Data;
                current = current.Next;
            }

            return array;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        public void Clear()
        {
            head = null;
            count = 0;
            GC.Collect();
        }

        public override string ToString()
        {
            return $"LinkedList<{typeof(T).Name}> - Count: {count}";
        }
    }
}
```

### Data/LogisticsGraph.cs
```csharp
namespace EcoDrive.Data
{
    public class LogisticsGraph
    {
        private Dictionary<string, List<(string destination, double distance)>> adjacencyList;

        public LogisticsGraph()
        {
            adjacencyList = new Dictionary<string, List<(string, double)>>();
        }

        public void AddCity(string cityName)
        {
            if (!adjacencyList.ContainsKey(cityName))
                adjacencyList[cityName] = new List<(string, double)>();
        }

        public void AddRoute(string fromCity, string toCity, double distanceKm)
        {
            AddCity(fromCity);
            AddCity(toCity);

            if (!adjacencyList[fromCity].Any(r => r.destination == toCity))
                adjacencyList[fromCity].Add((toCity, distanceKm));
        }

        public List<(string, double)>? GetAdjacentCities(string cityName)
        {
            return adjacencyList.ContainsKey(cityName) ? adjacencyList[cityName] : null;
        }

        public double GetShortestDistance(string startCity, string endCity)
        {
            if (!adjacencyList.ContainsKey(startCity) || !adjacencyList.ContainsKey(endCity))
                return -1;

            var distances = new Dictionary<string, double>();
            var queue = new Queue<string>();

            foreach (var city in adjacencyList.Keys)
                distances[city] = double.MaxValue;

            distances[startCity] = 0;
            queue.Enqueue(startCity);

            while (queue.Count > 0)
            {
                string current = queue.Dequeue();

                foreach (var (nextCity, dist) in adjacencyList[current])
                {
                    double newDistance = distances[current] + dist;
                    if (newDistance < distances[nextCity])
                    {
                        distances[nextCity] = newDistance;
                        queue.Enqueue(nextCity);
                    }
                }
            }

            return distances[endCity] == double.MaxValue ? -1 : distances[endCity];
        }

        public List<string> GetAllCities()
        {
            return adjacencyList.Keys.ToList();
        }

        public int GetRouteCount()
        {
            return adjacencyList.Values.Sum(routes => routes.Count);
        }

        public override string ToString()
        {
            return $"LogisticsGraph - Cities: {adjacencyList.Count}, Routes: {GetRouteCount()}";
        }
    }
}
```

## 6) Instrucciones de ejecución

1. Crea una carpeta con el nombre del proyecto.
2. Crea las carpetas Core, Data y GUI.
3. Copia cada bloque de código en su archivo correspondiente.
4. Abre la solución en Visual Studio o VS Code.
5. Ejecuta con .NET 9 y WPF.

## 7) Nota

Este archivo sirve como exportación única para pegarlo en GitHub o usarlo como base para un README completo del proyecto.
