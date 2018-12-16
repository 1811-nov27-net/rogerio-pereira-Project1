using System;
using System.Collections;
using System.Collections.Generic;

namespace Project1.DataAccess.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Orders Create(Orders model);
        void Delete(int id);
        IList GetAll();
        IList GetByAddress(int addressId);
        IList GetByCustomer(int customerId);
        Orders GetById(int id);
        IList GetByName(string name);
        DateTime getLastOrderDate(int addressId);
        Orders Save(Orders model, int? id = null);
        void SaveChanges();
        Orders Update(Orders model, int? id = null);
    }
}