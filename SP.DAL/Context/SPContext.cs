using SP.Common.Constant;
using SP.Model.Entities;
using System.Data.Entity;

namespace SP.DAL.Context
{
    public class SPContext :DbContext
    {
        public SPContext() : base("SkyPatchingContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Types().Configure(f => f.ToTable(Constant.SkyPatchingTablePrefix + f.ClrType.Name));
        }

        public DbSet<User> Users { get; set; } 
    }
}
