using Microsoft.EntityFrameworkCore;
using ProjectManagement.Domain.Models;

namespace ProjectManagement.DataAccess.Context
{
    public class ProjectManagementDbContext : DbContext
    {
        #region Field
        private const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProjectManagement;Integrated Security=True"; 
        #endregion

        #region Ctor
        public ProjectManagementDbContext()
        { }

        public ProjectManagementDbContext(DbContextOptions<ProjectManagementDbContext> options) : base(options)
        { } 
        #endregion

        #region Properties
        public virtual DbSet<Developer> Developers { get; set; }
        #endregion

        #region Methods
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }
        #endregion
    }
}