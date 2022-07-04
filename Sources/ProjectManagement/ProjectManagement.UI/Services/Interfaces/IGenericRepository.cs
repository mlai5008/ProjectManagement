using System;
using System.Threading.Tasks;

namespace ProjectManagement.UI.Services.Interfaces
{
    public interface IGenericRepository<T>
    {
        #region Methods
        Task<T> GetByIdAsync(Guid id);
        Task SaveAsync();
        bool HasChanges();
        void Add(T model);
        void Remove(T model); 
        #endregion
    }
}