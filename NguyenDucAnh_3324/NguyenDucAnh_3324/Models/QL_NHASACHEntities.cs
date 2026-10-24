using System.Data.Entity;

namespace NguyenDucAnh_3324.Models
{
    public class QL_NHASACHEntities : DbContext
    {
        public QL_NHASACHEntities() : base("name=QL_NHASACHEntities")
        {
        }

        public DbSet<SACH> SACHs { get; set; }
        public DbSet<THELOAI> THELOAIs { get; set; }
        public DbSet<TAIKHOAN> TAIKHOANs { get; set; }
        public DbSet<HOADON> HOADONs { get; set; }
        public DbSet<CTHOADON> CTHOADONs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CTHOADON>()
                        .HasKey(c => new { c.MaHD, c.MaSach });

            base.OnModelCreating(modelBuilder);
        }
    }
}
