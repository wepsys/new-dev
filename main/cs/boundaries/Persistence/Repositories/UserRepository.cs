using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wepsys.DianaHr.Core.Entities;
using Wepsys.DianaHr.Core.Repository;

namespace Wepsys.DianaHr.Persistence.Repositories
{
    /// <summary>
    /// Creating Classs for the repository and store all the methods
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly WepsysDianaHrContext _wepsysDianaHrContext;


        /// <summary>
        /// Validation from the User Repository to make the referecen from the ---
        /// </summary>
        /// <param name="wepsysDianaHrContext"></param>
        public UserRepository(WepsysDianaHrContext wepsysDianaHrContext)
        {
            _wepsysDianaHrContext = wepsysDianaHrContext;
        }
        
        /// <summary>
        /// This is the Repository to Get user information completely 
        /// </summary>
        /// <returns></returns>
        public async Task<ISet<User>> RetrieveAsync()
        {
            List<User> users = await _wepsysDianaHrContext.User.ToListAsync().ConfigureAwait(false);
            return users.ToHashSet();
        }
        /// <summary>
        /// This is the Repository to get the user information by the ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ISet<User>> RetrieveByIdAsync(Guid id)
        {
            var user = await _wepsysDianaHrContext.User
                .FirstOrDefaultAsync(x => x.Id == id)
                .ConfigureAwait(false);

            return new HashSet<User> { user };
        }
        
        /// <summary>
        /// This is the repository to Create a new user information 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<User> CreateAsync(User user)
        {
            _wepsysDianaHrContext.User.Add(user);
            await _wepsysDianaHrContext.SaveChangesAsync().ConfigureAwait(false);
            return user;
        }
        
        /// <summary>
        /// Repository to make validation and post the information to the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task PersistAsync(User user)
        {
            await _wepsysDianaHrContext.User.AddAsync(user).ConfigureAwait(false);
            await _wepsysDianaHrContext.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Validation to Put the user information 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task UpdateAsync(User user)
        {
            _wepsysDianaHrContext.User.Update(user);

            await _wepsysDianaHrContext.SaveChangesAsync().ConfigureAwait(false);
        }
        
        /// <summary>
        /// Validation to Delete the user information by the ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            var existinguser = await _wepsysDianaHrContext.User.FindAsync(id).ConfigureAwait(false);
            _wepsysDianaHrContext.User.Remove(existinguser!);
            await _wepsysDianaHrContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}