using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wepsys.DianaHr.Core.Entities;

namespace Wepsys.DianaHr.Core.Repository
{
    public interface IUserRepository
    {

        /// <summary>
        /// Create the RetrieveAsync Interface (for the user)
        /// </summary>
        /// <returns></returns>
        Task<ISet<User>> RetrieveAsync();

        /// <summary>
        /// Create the RetrieveAsync Interface by ID (user)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ISet<User>> RetrieveByIdAsync(Guid id);

        /// <summary>
        /// Create the PersisAsync Interface 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task PersistAsync(User user);

        /// <summary>
        /// Create the DeleteAsynct Interface (Remove users)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Create the UpdateAsync Interface (Modify user information )
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task UpdateAsync(User user);
    }
}