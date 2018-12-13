using Project0.Library.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Project0.Library.Control.Export
{
    public abstract class AExport : IExport
    {
        public abstract void Export(List<AModelBase> list);

        protected void ExportToFile(string fileName, List<AModelBase> list)
        {
            var serializer = new XmlSerializer(typeof(List<AModelBase>));

            FileStream fileStream = null;
            try
            {
                fileStream = new FileStream(fileName, FileMode.Create);
                serializer.Serialize(fileStream, list);
            }
            catch (IOException e)
            {
                Console.WriteLine($"Some error in file I/O: {e.Message}");
            }
            finally
            {
                fileStream?.Dispose();
            }
        }
    }
}
