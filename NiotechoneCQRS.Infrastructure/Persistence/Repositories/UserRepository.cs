using Dapper;
using NiotechoneCQRS.Domain.Entities;
using NiotechoneCQRS.Domain.Interfaces;
using NiotechoneCQRS.Infrastructure.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiotechoneCQRS.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public UserRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IList<User>> GetAllUsers(CancellationToken cancellationToken = default)
        {
            using var connection = _connectionFactory.CreateConnection();

            const string sql = @"
                SELECT *
                FROM [User]";

            var users = await connection.QueryAsync<User>(sql);

            return users.AsList();
        }
    }
}