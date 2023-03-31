using MedicalTest2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using MedicalTest2.ViewModels;

namespace MedicalTest2.Data
{
    public class ApplicationDbContext : IdentityDbContext<MyUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
           
        }
        public DbSet<Employee> Employees { set; get; }
        //public DbSet<Category> Categories { set; get; }
        //public DbSet<EmployeeCategory> EmployeeCategories { set; get; }
        public DbSet<Nationality> Nationalities { set; get; }
        public DbSet<Destination> Destinations { set; get; }
        public DbSet<Department> Departments { set; get; }
        public DbSet<ActualWork> ActualWorks { set; get; }
        public DbSet<JobCategory> JobCategories { set; get; }
        public DbSet<Gender> Genders { set; get; }
        public DbSet<WorkType> WorkTypes { set; get; }
        public DbSet<Attachment> Attachments { set; get; }
        public DbSet<EmployeeAttachment> EmployeeAttachments { set; get; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<MyUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
        }
    }
}
