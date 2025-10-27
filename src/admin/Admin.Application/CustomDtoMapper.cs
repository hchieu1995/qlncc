using Admin.Domains;
using Admin.DomainTranferObjects.DTO;
using AutoMapper;
namespace Admin.Application
{
    internal static class CustomDtoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<NguoiDung_ThongTin, NguoiDung_ThongTinDto>();
            configuration.CreateMap<NguoiDung_ThongTinDto, NguoiDung_ThongTin>();

            configuration.CreateMap<Dm_CauHinh, DmCauHinhDto>();
            configuration.CreateMap<DmCauHinhDto, Dm_CauHinh>();
        }
    }
}
