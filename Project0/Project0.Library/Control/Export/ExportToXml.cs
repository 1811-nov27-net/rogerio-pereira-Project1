using System;
using System.Collections.Generic;
using Project0.Library.Model;

namespace Project0.Library.Control.Export
{
    public class ExportToXml : AExport
    {
        public ExportToXml(List<AModelBase> list)
        {
            Export(list);
        }

        public override void Export(List<AModelBase> list)
        {
            string dateTime = DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
            string fileName = Environment.CurrentDirectory + $"{ typeof(AModelBase)}_{dateTime}.xml";

            ExportToFile(fileName, list);
        }
    }
}
