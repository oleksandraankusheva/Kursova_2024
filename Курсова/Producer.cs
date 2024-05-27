using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсова
{
    public abstract class Producer : Medicine
    {
        public override string FolderName => Producer;

        public string ProducerName { get; set; }

        public Producer(string producerName)
        {
            ProducerName = producerName;
        }

        public override void SaveToFile()
        {
            string producerDirectory = Path.Combine(Environment.CurrentDirectory, ProducerName);
            if (!Directory.Exists(producerDirectory))
            {
                Directory.CreateDirectory(producerDirectory);
            }
            SaveToFileInProducerFolder(producerDirectory);
        }

        protected abstract void SaveToFileInProducerFolder(string producerDirectory);
    }
}
