using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MyCourseWork.AuthorizationService.Entities;

namespace MyCourseWork
{
    public class EmployeeContext : DbContext, AuthorizationService.Interface.IDbContext
    {
        public EmployeeContext() : base("DBConnection")
        { }

        public DbSet<EmployeeEntity> Employee { get; set; }
        public DbSet<AuthificationDateEntity> AuthDate { get; set; }
        public DbSet<PhonesEntity> Phones { get; set; }
        public DbSet<AcceptLevelEntity> AcceptLevel { get; set; }
        public DbSet<ApplicationsEntity> Applications { get; set; }
    }
}
