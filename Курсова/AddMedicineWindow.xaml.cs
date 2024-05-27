using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using static Курсова.Medicine;

namespace Курсова
{
    /// <summary>
    /// Interaction logic for AddMedicineWindow.xaml
    /// </summary>
    public partial class AddMedicineWindow : Window
    {
        private bool isDataSaved = false;
        private string _filePath;
        public string FilePath { get; private set; }

        public AddMedicineWindow(string medicineName, string producerName)
        {
            InitializeComponent();
            Name_Text.Text = medicineName;
            Producer_Text.Text = producerName;
            Closing += AddMedicineWindow_Closing;
        }
        public AddMedicineWindow(string filePath)
        {
            InitializeComponent();
            _filePath = filePath;
            LoadMedicineData();
        }
        private void LoadMedicineData()
        {
            if (File.Exists(_filePath))
            {
                string[] lines = File.ReadAllLines(_filePath);
                Name_Text.Text = lines[0].Split(':')[1].Trim();
                Number_Text.Text = lines[1].Split(':')[1].Trim();
                DateAll_Text.Text = lines[2].Split(':')[1].Trim();
                Producer_Text.Text = lines[3].Split(':')[1].Trim();
                string type = lines[4].Split(':')[1].Trim();
                if (type == "Сироп")
                {
                    Type_1.IsChecked = true;
                }
                else if (type == "Таблетки")
                {
                    Type_2.IsChecked = true;
                }
                else if (type == "Крем")
                {
                    Type_3.IsChecked = true;
                }
                Quantity_Text.Text = lines[5].Split(':')[1].Trim();
                Destination_Text.Text = lines[6].Split(':')[1].Trim();
                Save_Text.Text = lines[7].Split(':')[1].Trim();

                string recipe = lines[8].Split(':')[1].Trim();
                RYes.IsChecked = recipe == "Так";
                RNo.IsChecked = recipe == "Ні";

                Person_Text.Text = lines[9].Split(':')[1].Trim();
                Date_Text.Text = lines[10].Split(':')[1].Trim();
                Information_Text.Text = lines[11].Split(':')[1].Trim();
            }
        }
        private void SaveDoc_Click(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = Producer_Text;
            ComboBoxItem selectedItem = comboBox.SelectedItem as ComboBoxItem;

            if (selectedItem != null)
            {
                string selectedContent = selectedItem.Content.ToString();
                Medicine medicine = null;

                if (Type_1.IsChecked == true)
                {
                    medicine = new SyrupMedicine(selectedContent);
                }
                else if (Type_2.IsChecked == true)
                {
                    medicine = new TabletMedicine(selectedContent);
                }
                else if (Type_3.IsChecked == true)
                {
                    medicine = new CreamMedicine(selectedContent);
                }
                else
                {
                    MessageBox.Show("Оберіть тип медикаменту.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                medicine.Name = Name_Text.Text;
                medicine.Number = Number_Text.Text;
                medicine.DateAll = DateAll_Text.Text;
                medicine.Producer = selectedContent;
                medicine.Quantity = Quantity_Text.Text;
                medicine.Save = Save_Text.Text;
                medicine.Recipe = RYes.IsChecked == true ? "Так" : "Ні";
                medicine.Destination = Destination_Text.Text;
                medicine.Person = Person_Text.Text;
                medicine.Date = Date_Text.Text;
                medicine.Information = Information_Text.Text;

                medicine.SaveToFile();
            }

            MessageBox.Show("Дані збережено успішно.", "Інформація", MessageBoxButton.OK, MessageBoxImage.Information);
            isDataSaved = true;
        }

        private void AddMedicineWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(isDataSaved == false)
            {
                MessageBoxResult result = MessageBox.Show("Бажаєте зберегти зміни перед закриттям?", "Зберегти файл", MessageBoxButton.YesNoCancel);

                switch (result)
                {
                    case MessageBoxResult.Yes:
                        SaveDoc_Click(sender, null);
                        break;
                    case MessageBoxResult.No:
                        break;
                    case MessageBoxResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }
    }
}
