using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсова
{
    public class TabletMedicine : Type
    {
        public TabletMedicine(string producerName) : base(producerName, "Таблетки") { }

        protected override void SaveToFileInTypeFolder(string typeDirectory)
        {
            string fileName = Name + ".txt";
            string filePath = Path.Combine(typeDirectory, fileName);

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine($"Назва: {Name}");
                writer.WriteLine($"Серія №: {Number}");
                writer.WriteLine($"Термін придатності: {DateAll}");
                writer.WriteLine($"Виробник: {Producer}");
                writer.WriteLine($"Тип: {TypeName}");
                writer.WriteLine($"Кількість: {Quantity}");
                writer.WriteLine($"Призначення: {Destination}");
                writer.WriteLine($"Зберігання: {Save}");
                writer.WriteLine($"Рецепт: {Recipe}");
                writer.WriteLine($"Прийняв: {Person}");
                writer.WriteLine($"Дата прийняття: {DateTime.Now}");
                writer.WriteLine($"Додаткова інформація: {Information}");
            }
        }
    }
}
