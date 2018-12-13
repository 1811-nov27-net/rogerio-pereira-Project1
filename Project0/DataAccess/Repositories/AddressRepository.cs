using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project0.DataAccess.Repositories
{
    public class AddressRepository : ARepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="db">Project0Context</param>
        public AddressRepository(Project0Context db) : base(db) { }

        public override void Delete(int id)
        {
            Addresses tracked = Db.Addresses.Find(id);
            if (tracked == null)
            {
                throw new ArgumentException("No Address with this id", nameof(id));
            }
            Db.Remove(tracked);
        }

        public override IList GetAll()
        {
            return (List<Addresses>) Db.Addresses.ToList();
        }

        public override AModel GetById(int id)
        {
            return Db.Addresses.Find(id);
        }

        //Search by the Address 1
        public override IList GetByName(string name)
        {
            return (List<Addresses>)Db.Addresses.Where(model => model.Address1.Contains(name)).ToList();
        }
        
        //Search by the Customer Id
        public IList GetByCustomerId(int customerId)
        {
            return (List<Addresses>)Db.Addresses.Where(model => model.CustomerId == customerId).ToList();
        }

        protected override AModel Create(AModel model)
        {
            Db.Add((Addresses)model);

            return (Addresses)model;
        }

        protected override AModel Update(AModel model, int? id = null)
        {
            if (id == null)
            {
                throw new ArgumentException("Nedded id", nameof(id));
            }

            Addresses tracked = Db.Addresses.Find(id);
            if (tracked == null)
            {
                throw new ArgumentException("No Address with this id", nameof(id));
            }

            Db.Entry(tracked).CurrentValues.SetValues(model);

            return (Addresses)model;
        }

        public void SetDefaultAddress(int addressId, int customerId)
        {
            List<Addresses> addresses = (List<Addresses>)GetByCustomerId(customerId);

            foreach(Addresses address in addresses)
            {
                if(address.Id == addressId)
                    address.DefaultAddress = true;
                else
                    address.DefaultAddress = false;

                Save(address, address.Id);
            }
        }
    }
}
