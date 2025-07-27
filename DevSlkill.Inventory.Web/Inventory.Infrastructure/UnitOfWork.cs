using DevSkill.Inventory.Web.Domain.Utilities;
using Demo.Infrastructure.Utilities;
using Inventory.Domain;
using Inventory.Domain.Repositories;
using Inventory.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Infrastructure
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
        protected ISqlUtility SqlUtility { get; private set; }

        public UnitOfWork(DbContext context)
        {
            _dbContext = context;
            SqlUtility = new SqlUtility(_dbContext.Database.GetDbConnection());

        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
