﻿using Microsoft.EntityFrameworkCore;
using ProjectManagement.DataAccess.Context;
using ProjectManagement.Domain.Models;
using ProjectManagement.UI.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace ProjectManagement.UI.Services
{
    public class DeveloperDataService : IDeveloperDataService
    {
        #region Fields
        private readonly Func<ProjectManagementDbContext> _dbContextCreator;
        #endregion

        #region Ctor
        public DeveloperDataService(Func<ProjectManagementDbContext> dbContextCreator)
        {
            _dbContextCreator = dbContextCreator;
        } 
        #endregion

        #region Methods
        public async Task<Developer> GetByIdAsync(Guid developerId)
        {
            await using ProjectManagementDbContext dbContext = _dbContextCreator();
            return await dbContext.Developers.AsNoTracking().SingleAsync(d => d.Id == developerId);
        }
        #endregion
    }
}