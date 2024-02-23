using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using webapi_test211223.Models;
using webapi_test211223.ViewModels;

namespace webapi_test211223.Services
{
    public class NguoiDungService : INguoiDungService
    {
        private readonly MyDBContext _context;
        private readonly IMapper _mapper;
        private readonly JwtOptions _jwtOptions;
        public NguoiDungService(MyDBContext context, IMapper mapper, IOptions<JwtOptions> jwtOptions)
        {
            _context = context;
            _mapper = mapper;
            _jwtOptions = jwtOptions.Value;
        }
        public async Task<string> Authenticate(LoginRequest request)
        {
            if(!NguoiDungExists(request.sUserName))
            {
                throw new Exception("Couldn't find username " + request.sUserName);
            }
            else
            {
                var NguoiDung = await _context.tblNguoiDung.FirstOrDefaultAsync(m => m .sUserName.ToLower().Equals(request.sUserName.ToLower()));
                if(request.sPassword.ToLower().Equals(NguoiDung.sPassword.ToLower()))
                {
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.Email,NguoiDung.sEmail!),
                        new Claim(ClaimTypes.GivenName,NguoiDung.sHovaten!),
                        new Claim(ClaimTypes.Role, "Member"),
                        new Claim(ClaimTypes.NameIdentifier,NguoiDung.PK_iMataikhoan.ToString()),
                        
                //        new Claim(ClaimTypes.Name, NguoiDung.sUserName),
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SigningKey!));
                    var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _jwtOptions.Issuer,
                        _jwtOptions.Audience,
                        claims,
                        expires: DateTime.Now.AddHours(3),
                        signingCredentials: signingCredentials);
                    return new JwtSecurityTokenHandler().WriteToken(token);
                }
                else
                {
                    throw new Exception("Couldn't login username " + request.sUserName);
                }
            }
        }

        public async Task<bool> Register(NguoiDungRequest request)
        {
            var user = _mapper.Map<NguoiDungModel>(request);
            var findmyuser = await _context.tblNguoiDung.FirstOrDefaultAsync(m => m.sUserName.ToLower().Equals(request.sUserName.ToLower()));
            if (findmyuser != null)
            {
                throw new Exception("already have user with username " + request.sUserName);
            }
            try
            {
                var result = _context.tblNguoiDung.Add(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }

        private bool NguoiDungExists(string? username)
        {
            return (_context.tblNguoiDung?.Any(e => e.sUserName.ToLower().Equals(username.ToLower()))).GetValueOrDefault();
        }
    }
}
