using DataEntity;
using System.Diagnostics.Eventing.Reader;
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

namespace Sklad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            var context = new WarehouseContext();
            // context.ClearAndReseed(); //Smaže všechny záznamy a resetuje identity, pokud chcete začít s prázdnou databází, Nepouštět když tam jsou data!!!
            context.Seed(); // Seed the database with initial data if necessary
            //context.Seed2(); // Seed the database with initial data if necessary

            // AddSampleMaterial(); 
            // AddSampleMaterialWithUoM();
            // ShowMaterials();


        }

        private void ShowMaterials()
        {
            var context = new WarehouseContext();
            var material = context.Materials.Where(m => m.SafetyStock>1200).FirstOrDefault();

            if (material != null)
            {
                MessageBox.Show($"Material: {material.Name}, Safety Stock: {material.SafetyStock}, Pallet Capacity: {material.PalletCapacity}, Received On: {material.ReceivedOn}");
            }
            else
            {
                MessageBox.Show("No materials found in the database.");
            }

            var materials = context.Materials.ToList();

            string message = string.Join("\n", materials.Select(m => $"Material: {m.Name}, Safety Stock: {m.SafetyStock}, Pallet Capacity: {m.PalletCapacity}, Received On: {m.ReceivedOn}"));

            MessageBox.Show(message);

        }

        void AddSampleMaterial()
        {
            var context = new WarehouseContext();
            var newMaterial = new Material
            {
                Name = "Cihla",
                SafetyStock = 1000,
                PalletCapacity = 50,
                ReceivedOn = DateTime.Now
            };
            context.Materials.Add(newMaterial);
            context.SaveChanges();
        }

        void AddSampleMaterialWithUoM()
        {
            var context = new WarehouseContext();

            var uom = context.UnitsOfMeasure.FirstOrDefault(u => u.Description == "ks");

            if (uom != null)
            {
                var newMaterial = new Material
                {
                    Name = "Cihla",
                    SafetyStock = 1000,
                    PalletCapacity = 50,
                    UnitOfMeasure = uom
                };

                context.Materials.Add(newMaterial);
                context.SaveChanges();
            }
        }


    }
}