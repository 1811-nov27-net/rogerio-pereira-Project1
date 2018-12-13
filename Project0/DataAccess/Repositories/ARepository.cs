using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Project0.DataAccess.Repositories
{
    /// <summary>
    /// Repository's Abstract class
    /// </summary>
    public abstract class ARepository : IRepository
    {
        protected Project0Context Db { get; }

        public ARepository(Project0Context db)
        {
            Db = db ?? throw new ArgumentNullException(nameof(db));
        }

        /// <summary>
        /// Delete model
        /// </summary>
        /// <param name="model">Model to be deleted</param>
        /// <returns>Operation's Status</returns>
        public abstract void Delete(int id);

        /// <summary>
        /// Get all data stored in data base
        /// </summary>
        /// <returns>IList (AModel)</returns>
        public abstract IList GetAll();

        /// <summary>
        /// Get model based on ID
        /// </summary>
        /// <param name="id">Model's id</param>
        /// <returns>AModel</returns>
        public abstract AModel GetById(int id);

        /// <summary>
        /// Get model based on name
        /// </summary>
        /// <param name="name">Model's name</param>
        /// <returns>AModel</returns>
        public abstract IList GetByName(string name);

        /// <summary>
        /// Save a model
        /// </summary>
        /// <param name="model">Model to be saved</param>
        /// <param name="id">Model's id</param>
        /// <returns>Model saved</returns>
        public AModel Save(AModel model, int? id = null)
        {
            if (id == null || id < 1)
                return Create(model);
            else
                return Update(model, id);
        }
        /// <summary>
        /// Store new model
        /// </summary>
        /// <param name="model">Model to be saved</param>
        /// <returns>Model created</returns>
        protected abstract AModel Create(AModel model);

        /// <summary>
        /// Update Model
        /// </summary>
        /// <param name="model">Model to be updated</param>
        /// <param name="id">Model's Id</param>
        /// <returns>Model updated</returns>
        protected abstract AModel Update(AModel model, int? id = null);

        /// <summary>
        /// Save Database Changes
        /// </summary>
        public void SaveChanges()
        {
            Db.SaveChanges();
        }
    }
}
