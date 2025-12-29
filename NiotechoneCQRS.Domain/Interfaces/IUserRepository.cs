using NiotechoneCQRS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiotechoneCQRS.Domain.Interfaces;

public interface IUserRepository
{
    Task<IList<User>> GetAllUsers(CancellationToken cancellationToken = default);
}
