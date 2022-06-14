using Furniture;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Type = Furniture.Type;


namespace furniture
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private void FurnitureItemList_SelectedChanget(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
        private Furniture.DataContext context { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            context = new DataContext();
            UpdateData();
        }

        public void UpdateData()
        {
            List<Type> DatabaseTypes = context.Types.Include(type => type.Furnitures).ToList();
            TypesItemList.ItemsSource = DatabaseTypes;
            List<Furniture.Furniture> DatabaseBoats = context.Furnitures.Include(furniture => furniture.Type).ToList();
            FurnitureItemList.ItemsSource = DatabaseBoats;

            TypeComboBox.ItemsSource = DatabaseTypes;

        }
        private bool IsEmpty(string str)
        {
            return String.IsNullOrWhiteSpace(str);
        }

        private void CreateFurniture(object sender, RoutedEventArgs e)
        {
            var name = NameTextBox.Text;
            var price = PriceTextBox.Text;
            var type = TypeComboBox.Text;


            if (IsEmpty(name) || IsEmpty(price) || type == null)
            {
                MessageBox.Show("Fill all fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                context.Furnitures.Add(new Furniture.Furniture()
                {
                    Furniture_Name = name,
                    Price = Convert.ToInt32(price),
                    Id = Convert.ToInt32(type)



                });
                context.SaveChanges();
                UpdateData();
            }
            NameTextBox.Clear();
            PriceTextBox.Clear();
            //TypeComboBox.SelectedItem = " ";
        }

        private void UpdateFurniture(object sender, RoutedEventArgs e)
        {
            Furniture.Furniture selectedFurniture = FurnitureItemList.SelectedItem as Furniture.Furniture;
            var name = NameTextBox.Text;
            var price = PriceTextBox.Text;
            var type = TypeComboBox.Text;

            if (IsEmpty(name) == false && IsEmpty(price) == false && type != null)
            {
                Furniture.Furniture furniture = context.Furnitures.Find(selectedFurniture.Furniture_number);
                furniture.Furniture_Name = name;
                furniture.Price = Convert.ToInt32(price);
                furniture.Id = Convert.ToInt32(type);

                context.SaveChanges();
                UpdateData();

            }
            else
            {
                MessageBox.Show("Fill all fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            NameTextBox.Clear();
            PriceTextBox.Clear();
            TypeComboBox.SelectedItem = " ";

        }

        private void DeleteFurniture(object sender, RoutedEventArgs e)
        {
            if (FurnitureItemList.SelectedItem is Furniture.Furniture selectedFurniture)
            {
                Furniture.Furniture furniture = selectedFurniture;
                context.Furnitures.Remove(furniture);
                context.SaveChanges();
                UpdateData();
            }
            else
            {
                MessageBox.Show("Select some row", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CreateType(object sender, RoutedEventArgs e)
        {
            var type = TypeTextBox.Text;


            if (IsEmpty(type))
            {
                MessageBox.Show("Fill all fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                context.Types.Add(new Type()
                {
                    Type_Name = type

                });
                context.SaveChanges();
                UpdateData();
            }
            TypeTextBox.Clear();
        }

        private void UpdateType(object sender, RoutedEventArgs e)
        {
            Type selectedType = TypesItemList.SelectedItem as Type;
            var type = TypeTextBox.Text;


            if (IsEmpty(type) == false)
            {
                Type Type = context.Types.Find(selectedType.Type_Id);
                Type.Type_Name = type;

                context.SaveChanges();
                UpdateData();

            }
            else
            {
                MessageBox.Show("Fill all fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            TypeTextBox.Clear();
        }

        private void DeleteType(object sender, RoutedEventArgs e)
        {
            if (TypesItemList.SelectedItem is Type selectedType)
            {
                Type type = selectedType;
                context.Types.Remove(type);
                context.SaveChanges();
                UpdateData();
            }
            else
            {
                MessageBox.Show("Select some row", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Search_Type(object sender, RoutedEventArgs e)
        {
            var types = "";
            var type = TypeComboBox.Text;
            foreach (Furniture.Furniture furniture in FurnitureItemList.Items)
            {

                if (Convert.ToInt32(furniture.Id) == Convert.ToInt32(type))
                {
                    types = types + furniture.Furniture_number + "\t" + furniture.Furniture_Name + "\t" + furniture.Price + "\t" + furniture.Id + "\n";
                }

            }
            if (types == "")
            {
                types = "Not Found";
            }
            MessageBox.Show($"result:\n{types}", "Result of Search", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void Sum(object sender, RoutedEventArgs e)
        {

            int sum = 0;
            foreach (Furniture.Furniture furniture in FurnitureItemList.Items)
            {

                sum += furniture.Price;
            }
            MessageBox.Show($"Sum = {sum}", "Sum of prices", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void Search_Name(object sender, RoutedEventArgs e)
        {
            var names = "";
            var name = NameTextBox.Text;
            foreach (Furniture.Furniture furniture in FurnitureItemList.Items)
            {

                if (furniture.Furniture_Name.IndexOf(name) != -1)
                {
                    names = names + furniture.Furniture_number + "\t" + furniture.Furniture_Name + "\t" + furniture.Price + "\t" + furniture.Id + "\n";
                }

            }
            if (names == "")
            {
                names = "Not Found";
            }
            MessageBox.Show($"result:\n{names}", "Result of Search", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    
    }
}

