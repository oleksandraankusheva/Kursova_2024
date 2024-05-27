using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсова
{
    public abstract class Type : Producer
    {
        public string TypeName { get; set; }

        public override string FolderName => $"{ProducerName}\\{TypeName}";

        public Type(string producerName, string typeName) : base(producerName)
        {
            TypeName = typeName;
        }

        protected override void SaveToFileInProducerFolder(string producerDirectory)
        {
            string typeDirectory = Path.Combine(producerDirectory, TypeName);
            if (!Directory.Exists(typeDirectory))
            {
                Directory.CreateDirectory(typeDirectory);
            }
            SaveToFileInTypeFolder(typeDirectory);
        }

        protected abstract void SaveToFileInTypeFolder(string typeDirectory);
    }
}
