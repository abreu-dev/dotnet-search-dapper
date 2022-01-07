﻿using Dapper;
using DotNetSearch.Domain.Entities;
using DotNetSearch.Domain.Interfaces;
using DotNetSearch.Infra.Data.Context;
using DotNetSearch.Infra.Data.DbConnection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetSearch.Infra.Data.Repositories
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public CategoriaRepository(DotNetSearchDbContext dbContext,
                                   IDbConnectionFactory dbConnectionFactory) : base(dbContext) 
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public override async Task<IEnumerable<Categoria>> DapperGetAll()
        {
            var query = "SELECT * FROM \"Categoria\"";

            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                return await connection.QueryAsync<Categoria>(query);
            }
        }

        public override async Task<Categoria> DapperGetById(Guid id)
        {
            var query = $"SELECT * FROM \"Categoria\" WHERE \"Id\" = '{id}'";

            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Categoria>(query);
            }
        }
    }
}
