using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

using System;
using TrainingApp.Data.Models.Employee;
using TrainingApp.Data.Models.Users;


namespace TrainingApp.Data
{
    public class TrainingContextDb : DbContext
    {
        public string ConnectionString => _connectionString;
        private readonly string _connectionString;
        public TrainingContextDb(DbContextOptions<TrainingContextDb> options)
                : base(options)
        {
            if (options != null)
            {

            }

        }


        public DbSet<Employee> Employees { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleMapping> RoleMappings { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
    }



}