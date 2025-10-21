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

            configuration.CreateMap<Ql_CoCauToChuc, Ql_CoCauToChucDto>();
            configuration.CreateMap<Ql_CoCauToChucDto, Ql_CoCauToChuc>();

            configuration.CreateMap<Ql_ToChuc_ThanhVien, Ql_ToChuc_ThanhVienDto>();
            configuration.CreateMap<Ql_ToChuc_ThanhVienDto, Ql_ToChuc_ThanhVien>();
        }
    }
}
