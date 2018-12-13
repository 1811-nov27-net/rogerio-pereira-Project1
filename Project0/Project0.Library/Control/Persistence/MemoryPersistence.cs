using System;
using System.Collections.Generic;
using System.Text;
using Project0.Library.Model;

namespace Project0.Library.Control.Persistence
{
    public class MemoryPersistence : IPersistence
    {
        public List<AModelBase> list = new List<AModelBase>();

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public AModelBase Find(int id)
        {
            throw new NotImplementedException();
        }

        public AModelBase Find(string name)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            list.Add((AModelBase)this);
            return true;
        }

        public bool Save(int id)
        {
            throw new NotImplementedException();
        }
    }
}
