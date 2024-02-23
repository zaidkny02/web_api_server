using webapi_test211223.ViewModels;

namespace webapi_test211223.Services
{
    public interface INguoiDungService
    {
        Task<string> Authenticate(LoginRequest request);

        Task<bool> Register(NguoiDungRequest request);
    }
}
