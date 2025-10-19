using AutoMapper;
using Admin.Domains;
using Admin.DomainTranferObjects.DTO;
using Admin.DomainTranferObjects.TichHop;
using Newtonsoft.Json;
using System.Collections.Generic;
namespace Admin.Application
{
    internal static class CustomDtoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {

            configuration.CreateMap<NguoiDung_ThongTin, NguoiDung_ThongTinDto>();
            configuration.CreateMap<NguoiDung_ThongTinDto, NguoiDung_ThongTin>();

        }
    }
}
