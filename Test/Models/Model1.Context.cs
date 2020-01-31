//------------------------------------------------------------------------------
// This class is part of data access layer (Repositort Pattern) It handles transaction between Database Storage
// All the model classes (Entity classes/Databse TABLES) Interact with this class to pass information between database and app
// 
//------------------------------------------------------------------------------

namespace Test.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class RCMSEntities2 : DbContext
    {
        private static RCMSEntities2 rCMS = null;//SIGNLETON
        public RCMSEntities2()
            : base("name=RCMSEntities2")
        {
        }
        public static RCMSEntities2 getInstance() //SIGNLETON
        {
            if (rCMS == null)
                return new RCMSEntities2();
            return rCMS;

        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Authentication> Authentications { get; set; }
        public virtual DbSet<Complaint> Complaints { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<LabAsign> LabAsigns { get; set; }
        public virtual DbSet<Labor> Labors { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<PayrollCust> PayrollCusts { get; set; }
        public virtual DbSet<payrollLab> payrollLabs { get; set; }
        public virtual DbSet<Rating_Values> Rating_Values { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Status> Status { get; set; }
    }
}
