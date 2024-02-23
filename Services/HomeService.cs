using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using webapi_test211223.Models;

namespace webapi_test211223.Services
{
    public class HomeService : IHomeService
    {
        private readonly MyDBContext _context;
        public HomeService(MyDBContext context)
        {
            _context = context;
        }

        public async Task<Result> AddKH(KhachHangModel _khachhang)
        {
            var result = new Result();
            try
            {
                _context.tblKhachHang.Add(_khachhang);
                await _context.SaveChangesAsync();
                result.type = "Success";
                result.message = "Success";
            }
            catch(Exception ex)
            {
                result.type = "Failure";
                result.message = ex.ToString();
            }
            return result;
        }

        public async Task<Result> DeleteKH(int id)
        {
            var result = new Result();
            var khachhangbyid = await _context.tblKhachHang.FindAsync(id);

            if (khachhangbyid == null)
            {
                result.type = "NotFound";
                result.message = "NotFound";
            }
            else
            {
                try
                {
                    _context.tblKhachHang.Remove(khachhangbyid);
                    await _context.SaveChangesAsync();
                    result.type = "Success";
                    result.message = "Success";
                }
                catch (Exception ex)
                {
                    result.type = "Failure";
                    result.message = ex.ToString();
                }
            }
            return result;
        }

        public async Task<IEnumerable<KhachHangModel>> getKH()
        {
            return await _context.tblKhachHang.ToListAsync();
        }

        public async Task<KhachHangModel> getKHbyID(int id)
        {
            return await _context.tblKhachHang.FirstOrDefaultAsync(m => m.iMaKH == id); 
        }

        public async Task<Result> PatchKH(int iMaKH, JsonPatchDocument<KhachHangModel> patchDocument, ModelStateDictionary modelstate)
        {
            var result = new Result();
            if (patchDocument == null)
            {
                result.type = "Failure";
                result.message = "BadRequest";
            }
            var khachhangbyid = await _context.tblKhachHang.FindAsync(iMaKH);
            if (khachhangbyid == null)
            {
                result.type = "NotFound";
                result.message = "NotFound";
            }
            patchDocument.ApplyTo(khachhangbyid);
            if (!modelstate.IsValid)
            {
                result.type = "Failure";
                result.message = "BadRequest";
            }
            try
            {
                await _context.SaveChangesAsync();
                result.type = "Success";
                result.message = "Success";
            }
            catch(Exception ex)
            {
                result.type = "Failure";
                result.message = ex.ToString();
            }
            return result;
        }
        public async Task<Result> UpdateKH(int MaKH, KhachHangModel model)
        {
            var result = new Result();
            if (MaKH != model.iMaKH)
            {
                result.type = "Failure";
                result.message = "BadRequest";
            }

            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                result.type = "Success";
                result.message = "Success";
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!EntityExists(MaKH))
                {
                    result.type = "NotFound";
                    result.message = "NotFound";
                }
                else
                {
                    result.type = "Failure";
                    result.message = ex.ToString();
                }
            }
            return result;
        }


        private bool EntityExists(int id)
        {
            return _context.tblKhachHang.Any(e => e.iMaKH == id);
        }
    }
}
