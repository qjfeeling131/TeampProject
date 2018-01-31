using System;
using BaseAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BaseAPI.EntityFrameworkCore
{
    public class BaseContext:DbContext
    {
        public DbSet<Student> ss_student { get; set; }

        public BaseContext(DbContextOptions<BaseContext> options) : base(options)
        {
         
        }
    }
}
