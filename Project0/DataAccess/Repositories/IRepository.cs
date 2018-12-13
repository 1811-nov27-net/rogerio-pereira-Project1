using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Project0.DataAccess.Repositories
{
    /// <summary>
    /// Repository's Interface
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Get all data stored in data base
        /// </summary>
        /// <returns>IList (AModel)</returns>
        IList GetAll();

        /// <summary>
        /// Get model based on ID
        /// </summary>
        /// <param name="id">Model's id</param>
        /// <returns>AModel</returns>
        AModel GetById(int id);

        /// <summary>
        /// Get model based on name
        /// </summary>
        /// <param name="name">Model's name</param>
        /// <returns>AModel</returns>
        IList GetByName(string name);

        /// <summary>
        /// Save a model
        /// </summary>
        /// <param name="model">Model to be saved</param>
        /// <param name="id">Model's id</param>
        /// <returns>Model saved</returns>
        AModel Save(AModel model, int? id = null);

        /// <summary>
        /// Delete model
        /// </summary>
        /// <param name="model">Model to be deleted</param>
        /// <returns>Operation's Status</returns>
        void Delete(int id);
    }
}
