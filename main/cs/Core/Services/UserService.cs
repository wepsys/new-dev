using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wepsys.DianaHr.Core.Entities;
using Wepsys.DianaHr.Core.Repository;
using Wepsys.DianaHr.Core.Services.Contracts;

namespace Wepsys.DianaHr.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            ArgumentNullException.ThrowIfNull(userRepository, nameof(userRepository));
            _userRepository = userRepository;

        }
        
        public async Task<ISet<User>>RetrieveAsync()
        {
            return await _userRepository.RetrieveAsync().ConfigureAwait(false);
        }

        public async Task<ISet<User>>RetrieveByIdAsync(Guid id)
        {
            ArgumentNullException.ThrowIfNull(id, nameof(id));
            return await _userRepository.RetrieveByIdAsync(id).ConfigureAwait(false);
        }
        
        public async Task PersistAsync(User user)
        {
            ArgumentNullException.ThrowIfNull(user, nameof(user));

            await _userRepository.PersistAsync(user).ConfigureAwait(false);
        }

        public async Task UpdateAsync(User user)
        {
            ArgumentNullException.ThrowIfNull(user, nameof(user));
            await _userRepository.UpdateAsync(user).ConfigureAwait(false);
        }
        
        public async Task DeleteAsync(Guid id)
        {
            ArgumentNullException.ThrowIfNull(id, nameof(id));
            await _userRepository.DeleteAsync(id).ConfigureAwait(false);
        }
    }
}