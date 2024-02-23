using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Net.WebSockets;
using System.Security.Claims;
using webapi_test211223.Models;
using System.Data;
using webapi_test211223.Services;

namespace webapi_test211223.Controllers
{

    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {

    //    private readonly MyDBContext _context;
      //  private readonly ILogger<HomeController> _logger;
        private readonly IHomeService _homeservice;

        public HomeController( IHomeService homeService)
        {
          //  _logger = logger;
     //       _context = context;
            _homeservice = homeService;
        }

        [Authorize(Roles = "Member")]
        [HttpGet(Name = "GetKhachHang")]
        public async Task<ActionResult<IEnumerable<KhachHangModel>>> Get()
        {
            /*Dành cho đọc giá trị token
            
            string authorizationHeader = HttpContext.Request.Headers["Authorization"];

            // Kiểm tra xem Authorization header có tồn tại và có định dạng đúng không
            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
            {
                
                // Lấy giá trị của bearer token
                string bearerToken = authorizationHeader.Substring("Bearer ".Length).Trim();
                //    Console.WriteLine(bearerToken);
                // Bạn có thể sử dụng giá trị của bearer token ở đây
                // Ví dụ: kiểm tra hoặc xác thực token
                // Đồng thời, lưu ý rằng cần thực hiện các biện pháp bảo mật thích hợp khi xử lý token.
                // Xử lý đọc token
                var handler = new JwtSecurityTokenHandler();
                try
                {
                    var jsonToken = handler.ReadToken(bearerToken) as JwtSecurityToken;
                    if (jsonToken != null)
                    {
                        var userId = jsonToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value.ToString();
                        //Xác thực
                        // Lấy danh sách các claims từ token, trong đó có roles.
                        var claims = jsonToken.Claims;
                        var roles = claims
                            .Where(c => c.Type == ClaimTypes.Role)
                            .Select(c => c.Value)
                            .ToArray();

                        Console.WriteLine(userId + "/" + roles[0].ToString());
                    }
                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            */
            var list =  await _homeservice.getKH();
            if (list == null)
                return NotFound();
            else
                return Ok(list);
        }
        [Authorize(Roles = "Member")]
        [HttpPost]
        public async Task<IActionResult> InsertKhachHang([FromBody]KhachHangModel _khachhang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _homeservice.AddKH(_khachhang);
            if (result.type.Equals("Success"))
            {
                //  return CreatedAtAction(nameof(GetKhachHangByID), new { id = _khachhang.iMaKH }, _khachhang);
                return Ok(result);
            }
            else
                return BadRequest(result.message);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<KhachHangModel>> GetKhachHangByID(int id)
        {
            var khachhangbyid = await _homeservice.getKHbyID(id);
            
            if (khachhangbyid == null)
            {
                return NotFound();
            }

            return khachhangbyid;
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _homeservice.DeleteKH(id);
            switch(result.type)
            {
                case "Success":
                    return Ok("Success");
               //     break;
                case "NotFound":
                    return NotFound();
               //     break;
                case "Failure":
                    return BadRequest(result.message);
                //    break;
                default:
                    return BadRequest();
                //    break;
            }
            
        }


        [HttpPut("{iMaKH}")]
        public async Task<IActionResult> UpdateKhachHang(int iMaKH, [FromBody] KhachHangModel _khachhang)
        {
            var result = await _homeservice.UpdateKH(iMaKH, _khachhang);
            switch (result.type)
            {
                case "Success":
                    return Ok("Success");
                //     break;
                case "NotFound":
                    return NotFound();
                //     break;
                case "Failure":
                    return BadRequest(result.message);
                //    break;
                default:
                    return BadRequest();
                    //    break;
            }
            

        }


        [HttpPatch("{iMaKH}")]
        public async Task<IActionResult> PatchKhachHang(int iMaKH, [FromBody] JsonPatchDocument<KhachHangModel> patchDocument)
        {
            var result = await _homeservice.PatchKH(iMaKH, patchDocument, ModelState);
            switch (result.type)
            {
                case "Success":
                    return Ok("Success");
                //     break;
                case "NotFound":
                    return NotFound();
                //     break;
                case "Failure":
                    return BadRequest(result.message);
                //    break;
                default:
                    return BadRequest();
                    //    break;
            }
        

            //var existingUser = _userService.GetUserById(id);

            // Áp dụng các thay đổi lên existingUser trực tiếp
        //    patchDocument.ApplyTo(khachhangbyid, ModelState);
 
            /*
            var validator = new UserModelValidator();
            var validationResult = validator.Validate(existingUser);

            if (!validationResult.IsValid)
            {
                // Chuyển đổi lỗi từ Fluent Validation sang ModelState nếu cần
                validationResult.Errors.ForEach(error => ModelState.AddModelError(error.PropertyName, error.ErrorMessage));
                return BadRequest(ModelState);
            }
            */
                // Update user with patched data
                // Đánh dấu user đã thay đổi và chỉ cập nhật các trường được thay đổi
                /*
                var existingUser = await _context.tblKhachHang.FindAsync(iMaKH);
                _context.tblKhachHang.Attach(existingUser);
                _context.Entry(existingUser).CurrentValues.SetValues(khachhangbyid);*/

        }



    /*    private bool EntityExists(int id)
        {
            return _context.tblKhachHang.Any(e => e.iMaKH == id);
        }
        */
        /*    [HttpPost(Name = "ReturnMessage")]
            public string GetString(TestModel mymodel)
            {
                //_logger.LogInformation("Sample");
                Console.Write(mymodel.test_key);
                return "why u try to get this?";
            } */
    }
}
