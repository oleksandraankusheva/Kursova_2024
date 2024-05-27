using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Курсова
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Producer_Text.SelectionChanged += Producer_Text_SelectionChanged;
        }
        
        private void Create_Button_Click(object sender, RoutedEventArgs e)
        {
            string medicineName = Name_Text.Text;
            string producerName = Producer_Text.Text;
            if (string.IsNullOrEmpty(medicineName))
            {
                MessageBox.Show("Будь ласка, введіть назву медикаменту!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(producerName))
            {
                MessageBox.Show("Будь ласка, оберіть веробника!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                PasswordWindow passwordWindow = new PasswordWindow();
                if (passwordWindow.ShowDialog() == true)
                {
                    string enteredPassword = passwordWindow.Password;

                    if (enteredPassword == "1111")
                    {

                        AddMedicineWindow addMedicineWindow = new AddMedicineWindow(medicineName, producerName);
                        addMedicineWindow.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Невірний пароль!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
        private void Search_Button_Click(object sender, RoutedEventArgs e)
        {
            string medicineName = Name_Text.Text;
            ComboBoxItem selectedProducerItem = Producer_Text.SelectedItem as ComboBoxItem;
            string producerName = selectedProducerItem?.Content.ToString();
            string projectFolder = @"D:\source\repos\Курсова\Курсова\bin\Debug";
            string[] typeFolders = { "Сироп", "Таблетки", "Крем" };

            if (NameCheck.IsChecked == true)
            {
                string foundFilePath = FindMedicineFileByName(medicineName, projectFolder, typeFolders);
                if (foundFilePath != null)
                {
                    ViewMedicineWindow viewWindow = new ViewMedicineWindow(foundFilePath, medicineName, producerName);
                    viewWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Файл не знайдено.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(producerName))
                {
                    MessageBox.Show("Оберіть виробника.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string foundFilePath = FindMedicineFile(medicineName, producerName, projectFolder, typeFolders);
                if (foundFilePath != null)
                {
                    ViewMedicineWindow viewWindow = new ViewMedicineWindow(foundFilePath, medicineName, producerName);
                    viewWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Файл не знайдено.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private string FindMedicineFile(string medicineName, string producerName, string projectFolder, string[] typeFolders)
        {
            foreach (string typeFolder in typeFolders)
            {
                string producerFolderPath = Path.Combine(projectFolder, producerName, typeFolder);
                if (Directory.Exists(producerFolderPath))
                {
                    string[] files = Directory.GetFiles(producerFolderPath, $"{medicineName}.txt", SearchOption.TopDirectoryOnly);
                    if (files.Length > 0)
                    {
                        return files[0];
                    }
                }
            }

            return null;
        }

        private string FindMedicineFileByName(string medicineName, string projectFolder, string[] typeFolders)
        {
            foreach (string typeFolder in typeFolders)
            {
                string[] producerFolders = Directory.GetDirectories(projectFolder);
                foreach (string producerFolder in producerFolders)
                {
                    string typeFolderPath = Path.Combine(producerFolder, typeFolder);
                    if (Directory.Exists(typeFolderPath))
                    {
                        string[] files = Directory.GetFiles(typeFolderPath, $"{medicineName}.txt", SearchOption.TopDirectoryOnly);
                        if (files.Length > 0)
                        {
                            return files[0];
                        }
                    }
                }
            }

            return null;
        }
        private void Producer_Text_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Producer_Text.SelectedItem != null)
            {
                string selectedItemContent = ((ComboBoxItem)Producer_Text.SelectedItem).Content.ToString();

                Producer_Text.Text = selectedItemContent;
            }
        }
    }
}

