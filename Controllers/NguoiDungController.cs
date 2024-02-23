using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi_test211223.Models;
using webapi_test211223.Services;
using webapi_test211223.ViewModels;

namespace webapi_test211223.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NguoiDungController : ControllerBase
    {
        private readonly INguoiDungService _nguoiDungService;
        public NguoiDungController(INguoiDungService nguoidungservice)
        {
            _nguoiDungService = nguoidungservice;
        }
        //API không nhận dữ liệu từ FromForm nếu gửi theo cách thông thường
        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resultToken = await _nguoiDungService.Authenticate(request);
            return string.IsNullOrEmpty(resultToken) ? BadRequest("Username or password is incorrect.") : Ok(new { token = resultToken });
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] NguoiDungRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _nguoiDungService.Register(request);
            return !result ? BadRequest("Register is unsuccessful.") : Ok();
        }

    }
}
