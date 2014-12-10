using System;
using System.Data.Entity;
using Zoo.BLL.Entities;


namespace Zoo.DAL
{
   class ZooDbContext : DbContext
    {
        private ZooDbContext()
            : base("DefaultConnection") 
        {
           
        }
        private static ZooDbContext instance = new ZooDbContext();
        public static ZooDbContext Instance
        {
            get
            {
                return instance;
            }
        }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Feeding> Feeding { get; set; }
        public DbSet<Lifecycle> Lifecycles { get; set; }
       
        public DbSet<ATD> ATDs { get; set; }
    }
}

