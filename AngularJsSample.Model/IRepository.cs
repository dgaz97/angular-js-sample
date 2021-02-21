using System.Collections.Generic;

namespace AngularJsSample.Model
{
    /// <summary>
    /// Generic interface for repository
    /// </summary>
    /// <typeparam name="T">Model that the interface implementation will be using</typeparam>
    /// <typeparam name="TKey">Type of key that the implementation will be using (usually integer)</typeparam>
    public interface IRepository<T, TKey>
    {
        /// <summary>
        /// Gets list of objects of type T
        /// </summary>
        /// <returns>A list of objects of type T</returns>
        List<T> FindAll();
        /// <summary>
        /// Gets specific object of type T, by key of type TKey
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Object of type T</returns>
        T FindBy(TKey key);
        /// <summary>
        /// Adds a new object of type T
        /// </summary>
        /// <param name="item">New object of type T that we're creating</param>
        /// <returns>TKey of newly created object of type T</returns>
        TKey Add(T item);
        /// <summary>
        /// Deletes the object of type T
        /// </summary>
        /// <param name="item">Object of type T to delete</param>
        /// <returns>true or false</returns>
        bool Delete(T item);
        /// <summary>
        /// Updates existing object of type T
        /// </summary>
        /// <param name="item">Object of type T that contains the new data</param>
        /// <returns>Object of type T</returns>
        T Save(T item);
    }
}
