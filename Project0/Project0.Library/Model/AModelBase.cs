using Project0.Library.Control.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project0.Library.Model
{
    public abstract class AModelBase : MemoryPersistence
    {
        /// <summary>
        /// ModelName used for persistence
        /// </summary>
        protected readonly string modelName;

        /// <summary>
        /// Constructor
        /// </summary>
        protected AModelBase()
        {
            modelName = GetModelName();
        }

        /// <summary>
        /// AModelBase function
        /// </summary>
        /// <returns>Model Name</returns>
        protected abstract string GetModelName();
    }
}
