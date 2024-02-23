using AutoMapper;
using webapi_test211223.Models;

namespace webapi_test211223.ViewModels
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<NguoiDungRequest, NguoiDungModel>();
        }
    }
}
