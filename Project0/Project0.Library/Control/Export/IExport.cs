using Project0.Library.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project0.Library.Control.Export
{
    public interface IExport
    {
        void Export(List<AModelBase> list);
    }
}
