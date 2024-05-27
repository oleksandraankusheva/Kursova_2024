using System.IO;

namespace Курсова
{
    public abstract class Medicine
    {
        private string name;
        private string number;
        private string dateAll;
        private string producer;
        private string save;
        private string quantity;
        private string type;
        private string recipe;
        private string destination;
        private string person;
        private string date;
        private string information;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Number
        {
            get { return number; }
            set { number = value; }
        }

        public string DateAll
        {
            get { return dateAll; }
            set { dateAll = value; }
        }

        public string Producer
        {
            get { return producer; }
            set { producer = value; }
        }

        public string Save
        {
            get { return save; }
            set { save = value; }
        }

        public string Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public string Recipe
        {
            get { return recipe; }
            set { recipe = value; }
        }

        public string Destination
        {
            get { return destination; }
            set { destination = value; }
        }

        public string Person
        {
            get { return person; }
            set { person = value; }
        }

        public string Date
        {
            get { return date; }
            set { date = value; }
        }

        public string Information
        {
            get { return information; }
            set { information = value; }
        }

        public abstract string FolderName { get; }
        public abstract void SaveToFile();
        public static void Delete(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            else
            {
                throw new FileNotFoundException("File not found");
            }
        }
    }
}

