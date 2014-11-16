using System;
using System.Data.Entity;
using Zoo.DAL.Entities;


namespace Zoo.DAL
{
    public class ZooDbContext : DbContext
    {
        public ZooDbContext()
            : base("DefaultConnection")
        {

        }
       
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Gender> Genders { get; set; }
    }
}
