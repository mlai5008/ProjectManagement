﻿using ProjectManagement.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagement.UI.Services.Interfaces
{
    public interface IMeetingRepository : IGenericRepository<Meeting>
    {
        #region Methods
        Task<List<Developer>> GetAllDevelopersAsync(); 
        #endregion
    }
}