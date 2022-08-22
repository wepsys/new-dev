using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wepsys.DianaHr.Core.Entities;

namespace Wepsys.DianaHr.Core.Services.Contracts
{


    public interface IUserService
    { 
        Task<ISet<User>>  RetrieveAsync();
        Task <ISet<User>> RetrieveByIdAsync(Guid id);
      Task PersistAsync(User user);
      Task UpdateAsync(User user);
      Task DeleteAsync(Guid id);
      
      
    }
}