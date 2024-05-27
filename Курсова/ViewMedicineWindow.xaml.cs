using System;
using System.IO;
using System.Windows;

namespace Курсова
{
    /// <summary>
    /// Interaction logic for ViewMedicineWindow.xaml
    /// </summary>
    public partial class ViewMedicineWindow : Window
    {
        private string filePath;
        private string name;
        private string producer;

        public ViewMedicineWindow(string filePath, string name, string producer)
        {
            InitializeComponent();
            this.filePath = filePath;
            this.name = name;
            this.producer = producer;
            LoadMedicineInfo();
        }
        private void LoadMedicineInfo()
        {
            try
            {
                InfoTextBox.Text = File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при завантаженні інформації: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            PasswordWindow passwordWindow = new PasswordWindow();
            if (passwordWindow.ShowDialog() == true)
            {
                string enteredPassword = passwordWindow.Password;

                if (enteredPassword == "1111")
                {
                    AddMedicineWindow editWindow = new AddMedicineWindow(filePath);
                    editWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Невірний пароль!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            PasswordWindow passwordWindow = new PasswordWindow();
            if (passwordWindow.ShowDialog() == true)
            {
                string enteredPassword = passwordWindow.Password;

                if (enteredPassword == "1111")
                {
                    MessageBoxResult result = MessageBox.Show("Ви впевнені, що хочете видалити цей файл?", "Підтвердження видалення", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        try
                        {
                            Medicine.Delete(filePath);
                            MessageBox.Show("Файл успішно видалений.", "Підтвердження", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Сталася помилка при видаленні файлу: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Невірний пароль!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
