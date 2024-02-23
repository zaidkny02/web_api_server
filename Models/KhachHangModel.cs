using System.ComponentModel.DataAnnotations;

namespace webapi_test211223.Models
{
    public class KhachHangModel
    {
        [Key]
        [Range(1, int.MaxValue, ErrorMessage = "iMaKH is required.")]
        
        public int iMaKH { get; set; }
        public string? sTenKH { get; set; }
        public string? sDiachi { get; set; }
        public string? sDienthoai { get; set; }
        public bool bGioitinh { get; set; }
        public int iTuoi { get; set; }

        public KhachHangModel() { }
        public KhachHangModel(int iMaKH, string? sTenKH, string? sDiachi, string? sDienthoai, bool bGioitinh, int iTuoi)
        {
            this.iMaKH = iMaKH;
            this.sTenKH = sTenKH;
            this.sDiachi = sDiachi;
            this.sDienthoai = sDienthoai;
            this.bGioitinh = bGioitinh;
            this.iTuoi = iTuoi;
        }

        public KhachHangModel(string? sTenKH, string? sDiachi, string? sDienthoai, bool bGioitinh, int iTuoi)
        {
            this.sTenKH = sTenKH;
            this.sDiachi = sDiachi;
            this.sDienthoai = sDienthoai;
            this.bGioitinh = bGioitinh;
            this.iTuoi = iTuoi;
        }
    }
}
