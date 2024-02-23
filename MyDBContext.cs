using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using webapi_test211223.Models;

namespace webapi_test211223
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        {
        }
        public DbSet<KhachHangModel> tblKhachHang { get; set; }
        public DbSet<NguoiDungModel> tblNguoiDung { get; set; }
    }
}
