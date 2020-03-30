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

namespace Phase1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        item[,] items = new item[4, 6];
        int comboBoxIndex = 0;
        int index;
        struct item
        {
            public string name { get; set; }
            public bool isChecked { get; set; }
            public int price { get; set; }
            public int number { get; set; }
        }
        public MainWindow()
        {
            InitializeComponent();
            
            //select checkboxes
            for (int i = 0; i < 6; i++)
                items[0, i].name = "";
            //combobox item1
            items[1,0].name = "HamBurger";
            items[1,1].name = "CheeseBurger";
            items[1,2].name = "ChickenBurger";
            items[1,3].name = "HotDog";
            items[1,4].name = "ChickenFillet";
            items[1,5].name = "Vegan Sandwich";

            items[1, 0].price = 20000;
            items[1, 1].price = 23000;
            items[1, 2].price = 20000;
            items[1, 3].price = 18000;
            items[1, 4].price = 20000;
            items[1, 5].price = 15000;
            //combobox item2
            items[2, 0].name = "Pepperoni Pizza";
            items[2, 1].name = "Cheese Pizza";
            items[2, 2].name = "Sausage Pizza";
            items[2, 3].name = "Chicken Pizza";
            items[2, 4].name = "Special Pizza";
            items[2, 5].name = "Vegan Pizza";

            items[2, 0].price = 25000;
            items[2, 1].price = 20000;
            items[2, 2].price = 21000;
            items[2, 3].price = 21000;
            items[2, 4].price = 27000;
            items[2, 5].price = 22000;
            //combobox item3
            items[3, 0].name = "Fries";
            items[3, 1].name = "Nuggets";
            items[3, 2].name = "Salad";
            items[3, 3].name = "Soda";
            items[3, 4].name = "Juice";
            items[3, 5].name = "Water";

            items[3, 0].price = 12000;
            items[3, 1].price = 14000;
            items[3, 2].price = 10000;
            items[3, 3].price = 3000;
            items[3, 4].price = 3000;
            items[3, 5].price = 1500;
        }
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Tab2 == null)
                return;
            if (Tab2.IsSelected)
            {
                DescriptionText.Visibility = Visibility.Visible;
                Order.Text = "";
                Total.Text = "";
                int Sum = 0;

                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 6; j++)
                        if (items[i, j].isChecked)
                        {
                            Order.Text += String.Format("{0,-16}x{2,-13}{1,8} T", items[i, j].name, items[i, j].price * items[i, j].number, items[i, j].number);
                            Sum += items[i, j].price * items[i, j].number;
                            if (!(i == 3 && j == 5))
                                Order.Text += "\n";
                        }
                Total.Text+= "----------------------------------------\n";
                Total.Text+= String.Format("{0,-30}{1,8} T", "Total", Sum);
            }
            else if (Tab1.IsSelected)
                DescriptionText.Visibility = Visibility.Hidden;
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            Tab2.IsSelected = true;
        }
        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            comboBoxIndex = ((ComboBoxItem)sender).TabIndex;

            CB0.Content = items[comboBoxIndex, 0].name;
            CB1.Content = items[comboBoxIndex, 1].name;
            CB2.Content = items[comboBoxIndex, 2].name;
            CB3.Content = items[comboBoxIndex, 3].name;
            CB4.Content = items[comboBoxIndex, 4].name;
            CB5.Content = items[comboBoxIndex, 5].name;
                               
            CB0.IsChecked=items[comboBoxIndex, 0].isChecked;
            CB1.IsChecked=items[comboBoxIndex, 1].isChecked;
            CB2.IsChecked=items[comboBoxIndex, 2].isChecked;
            CB3.IsChecked=items[comboBoxIndex, 3].isChecked;
            CB4.IsChecked=items[comboBoxIndex, 4].isChecked;
            CB5.IsChecked=items[comboBoxIndex, 5].isChecked;

            if(comboBoxIndex == 0)
            {
                CB0.IsEnabled = false;
                CB1.IsEnabled = false;
                CB2.IsEnabled = false;
                CB3.IsEnabled = false;
                CB4.IsEnabled = false;
                CB5.IsEnabled = false;
            }
            else
            {
                CB0.IsEnabled = true;
                CB1.IsEnabled = true;
                CB2.IsEnabled = true;
                CB3.IsEnabled = true;
                CB4.IsEnabled = true;
                CB5.IsEnabled = true;
            }
        }
        private void CheckBox_Changed(object sender, RoutedEventArgs e)
        {
            index = int.Parse(((CheckBox)sender).Name.ToString().Substring(2));

            items[comboBoxIndex, index].isChecked = (bool)((CheckBox)sender).IsChecked;
            
            if(items[comboBoxIndex, index].isChecked)
            {
                quantity.IsEnabled = true;
                sliderNumber.IsEnabled = true;
                if (items[comboBoxIndex, index].number == 0)
                    quantity.Value = items[comboBoxIndex, index].number = 1;
                else
                    quantity.Value = items[comboBoxIndex, index].number;

                var bc = new BrushConverter();
                var color = (Brush)bc.ConvertFrom("#313131");
                CB0.BorderBrush = color;
                CB1.BorderBrush = color;
                CB2.BorderBrush = color;
                CB3.BorderBrush = color;
                CB4.BorderBrush = color;
                CB5.BorderBrush = color;
                ((CheckBox)sender).BorderBrush = Brushes.Red;
            }
            else
            {
                quantity.IsEnabled = false;
                sliderNumber.IsEnabled = false;

                var bc = new BrushConverter();
                var color = (Brush)bc.ConvertFrom("#313131");
                ((CheckBox)sender).BorderBrush = color;
            }
        }

        private void Finish_Click(object sender, RoutedEventArgs e)
        {
                MessageBoxResult result = MessageBox.Show("دیگه نه در این حد\n فوقش میتونم برنامه رو ببندم", "", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                    this.Close();
        }

        private void quantity_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            items[comboBoxIndex, index].number = (int)quantity.Value;
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (quantity != null)
                quantity.IsEnabled = false;
            if (sliderNumber != null)
                sliderNumber.IsEnabled = false;
           
            var bc = new BrushConverter();
            var color = (Brush)bc.ConvertFrom("#313131");
            CB0.BorderBrush = color;
            CB1.BorderBrush = color;
            CB2.BorderBrush = color;
            CB3.BorderBrush = color;
            CB4.BorderBrush = color;
            CB5.BorderBrush = color;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("((: این چهارتا دکمه دکوریه", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
    }
}
