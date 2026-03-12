using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.DbContexts
{
    public class MainDbContext : DbContext
    {

        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
        }
        public DbSet<Users> users { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<Employee> employees { get; set; }
        public DbSet<LeaveRequest> leaveRequests { get; set; }

        public DbSet<Attendance> attendances { get; set; }
        public DbSet<Department> departments {  get; set; }

        public DbSet<Designation> designations { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Employee>()
        //        .HasOne(e => e.Manager)
        //        .WithMany(e => e.Subordinates)
        //        .HasForeignKey(e => e.ManagerId)
        //        .OnDelete(DeleteBehavior.Restrict);
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
    .HasOne(e => e.Manager)
    .WithMany(e => e.Subordinates)
    .HasForeignKey(e => e.ManagerId)
    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
