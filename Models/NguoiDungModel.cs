using System.ComponentModel.DataAnnotations;

namespace webapi_test211223.Models
{
    public class NguoiDungModel
    {
        [Key]
        public int PK_iMataikhoan { get; set; }
        public string sUserName { get; set; }
        public string sPassword { get; set; }
        public string sHovaten { get; set; }
        public string? sSDT { get; set; }
        public DateTime? dNgaysinh { get; set; }
        public string? sEmail { get; set; }
    }
}
