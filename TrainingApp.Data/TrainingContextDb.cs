using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

using System;
using TrainingApp.Data.Models.Employee;


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

    }



}