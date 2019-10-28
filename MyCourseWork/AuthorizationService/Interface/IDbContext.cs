using System;
using System.Data.Entity;
using MyCourseWork.AuthorizationService.Entities;

namespace MyCourseWork.AuthorizationService.Interface
{
    public interface IDbContext : IDisposable
    {
        DbSet<EmployeeEntity> Employee { get; set; }
        DbSet<AuthificationDateEntity> AuthDate { get; set; }
        DbSet<PhonesEntity> Phones { get; set; }
        DbSet<AcceptLevelEntity> AcceptLevel { get; set; }
        DbSet<ApplicationsEntity> Applications { get; set; }
        int SaveChanges();
    }

}
