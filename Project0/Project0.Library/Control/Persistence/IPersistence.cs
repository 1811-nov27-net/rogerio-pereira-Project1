using Project0.Library.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project0.Library.Control.Persistence
{
    public interface IPersistence
    {
        /// <summary>
        /// Create Data
        /// </summary>
        /// <param name="data">Data to be stored</param>
        /// <returns>Status</returns>
        bool Save();

        /// <summary>
        /// Update Data
        /// </summary>
        /// <param name="data">>Data to be stored</param>
        /// <param name="id">Item's Id</param>
        /// <returns>Status</returns>
        bool Save(int id);

        /// <summary>
        /// </summary>
        /// <param name="id">Item's Id</param>
        /// <returns>Model searched
        /// Find Item</returns>
        AModelBase Find(int id);

        /// <summary>
        /// </summary>
        /// <param name="name">Item's Name</param>
        /// <returns>Model searched
        /// Find Item</returns>
        AModelBase Find(string name);

        /// <summary>
        /// Delete item
        /// </summary>
        /// <param name="id">Item's Idparam>
        /// <returns>Status</returns>
        bool Delete(int id);
    }
}
