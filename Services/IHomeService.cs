using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using webapi_test211223.Models;

namespace webapi_test211223.Services
{
    public interface IHomeService
    {
        Task<IEnumerable<KhachHangModel>> getKH();
        Task<Result> AddKH(KhachHangModel _khachhang);
        Task<KhachHangModel> getKHbyID(int id);
        Task<Result> DeleteKH(int id);
        Task<Result> UpdateKH(int MaKH, KhachHangModel model);
        Task<Result> PatchKH(int iMaKH, JsonPatchDocument<KhachHangModel> patchDocument, ModelStateDictionary modelstate);
    }
}
