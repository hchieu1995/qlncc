using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Admin.Application.AppServices;
using Admin.Authorization;
using Admin.Domains;
using Admin.DomainTranferObjects;
using Admin.DomainTranferObjects.DTO;
using Admin.Shared.DomainTranferObjects;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Admin.AppServices
{
    [AbpAuthorize]
    public class CauHinhHeThongAppService : BnnAdminServiceBase
    {
        private readonly IRepository<Dm_CauHinh, long> _dmCauHinhRepository;
        public CauHinhHeThongAppService(
            IRepository<Dm_CauHinh, long> dmCauHinhRepository
            )
        {
            _dmCauHinhRepository = dmCauHinhRepository;
        }
        //[AbpAuthorize(AppPermissions.Admin_HeThong_CauHinh)]
        //public async Task<TableShowItem> DanhSachCauHinh(TableFilterItem input)
        //{
        //    var res = new TableShowItem();
        //    var query = _dmCauHinhRepository.GetAll()
        //        .WhereIf(!string.IsNullOrEmpty(input.filterext), m => m.CauHinh_Ma.Contains(input.filterext.Trim()) ||
        //            m.CauHinh_GiaTri.Contains(input.filterext.Trim())
        //    );

        //    res.totalCount = await query.CountAsync();

        //    var dscauhinh = query
        //            .OrderBy(input.sort ?? "CauHinh_Ma asc")
        //            .Skip(input.skip).Take(input.take)
        //            .ToList();
        //    var list = ObjectMapper.Map<List<DmCauHinhDto>>(dscauhinh);
        //    var lsttochucid = dscauhinh.Where(m => m.ToChuc_Id > 0).Select(m => m.ToChuc_Id).ToList();
        //    if (lsttochucid.Count > 0)
        //    {
        //        var dstochuc = _ql_CoCauToChucRepository.GetAll().Where(m => lsttochucid.Contains(m.Id)).ToList();
        //        foreach (var item in list)
        //        {
        //            if (item.ToChuc_Id > 0)
        //                item.Tc_Ten = dstochuc.FirstOrDefault(m => m.Id == item.ToChuc_Id)?.Tc_Ten;
        //        }
        //    }

        //    res.data = list.Cast<object>().ToList();
        //    return res;
        //}

        //public async Task<GenericResultDto> Create(DmCauHinhDto input)
        //{
        //    GenericResultDto rs = new GenericResultDto();
        //    try
        //    {
        //        rs.Message = Validate(input);
        //        if (!string.IsNullOrWhiteSpace(rs.Message))
        //            return rs;
        //        Dm_CauHinh ch = new Dm_CauHinh();
        //        Normalize(input, ch);
        //        if (_dmCauHinhRepository.GetAll().Any(m => m.CauHinh_Ma == ch.CauHinh_Ma))
        //        {
        //            rs.Message = "Trùng mã cấu hình";
        //            return rs;
        //        }
        //        await _dmCauHinhRepository.InsertAsync(ch);
        //        rs.Success = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        rs.Message = CommonConst.LoiPhatSinh;
        //        Logger.Error(JsonConvert.SerializeObject(input), ex);
        //    }
        //    return rs;
        //}

        //public async Task<GenericResultDto> Update(DmCauHinhDto input)
        //{
        //    GenericResultDto rs = new GenericResultDto();
        //    try
        //    {
        //        rs.Message = Validate(input);
        //        if (!string.IsNullOrWhiteSpace(rs.Message))
        //            return rs;
        //        Dm_CauHinh ch = await _dmCauHinhRepository.GetAsync(input.Id);
        //        Normalize(input, ch);
        //        await _dmCauHinhRepository.UpdateAsync(ch);
        //        rs.Success = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        rs.Message = CommonConst.LoiPhatSinh;
        //        Logger.Error(JsonConvert.SerializeObject(input), ex);
        //    }
        //    return rs;
        //}
        //void Normalize(DmCauHinhDto input, Dm_CauHinh ch)
        //{
        //    if (ch.Id == 0)
        //        ch.CauHinh_Ma = input.CauHinh_Ma?.Trim().Replace(" ", "");
        //    ch.CauHinh_GiaTri = input.CauHinh_GiaTri?.Trim();
        //    ch.CauHinh_MoTa = input.CauHinh_MoTa?.Trim();
        //    ch.ToChuc_Id = input.ToChuc_Id;
        //}
        //string Validate(DmCauHinhDto input)
        //{
        //    if (string.IsNullOrWhiteSpace(input.CauHinh_Ma))
        //        return "Mã cấu hình không được để trống";
        //    if (string.IsNullOrWhiteSpace(input.CauHinh_GiaTri))
        //        return "Giá trị cấu hình không được để trống";
        //    return null;
        //}

        //public async Task Delete(int id)
        //{
        //    await _dmCauHinhRepository.DeleteAsync(m => m.Id == id);
        //}

        ////[AbpAuthorize(AppPermissions.Admin_HeThong_CauHinh_KetNoiEmail)]
        //public async Task<GenericResultDto> LuuCauHinhEmail(CauHinhEmail input)
        //{
        //    GenericResultDto rs = new GenericResultDto();
        //    try
        //    {
        //        var tc = GetUserInfo_ToChuc();
        //        if (tc.ToChuc == null || tc.ToChuc.Id == 0)
        //        {
        //            rs.Message = "Không tìm thấy thông tin tổ chức";
        //            return rs;
        //        }

        //        rs.Message = ValidateAndNormalizeCauHinhEmail(input);
        //        if (!string.IsNullOrWhiteSpace(rs.Message))
        //            return rs;

        //        Dm_CauHinh ch = await _dmCauHinhRepository.GetAll().Where(m => m.CauHinh_Ma == MaCauHinh.KETNOIEMAIL && m.ToChuc_Id == tc.ToChuc.Id).FirstOrDefaultAsync();
        //        if (ch == null)
        //        {
        //            ch = new Dm_CauHinh()
        //            {
        //                ToChuc_Id = tc.ToChuc.Id,
        //                CauHinh_Ma = MaCauHinh.KETNOIEMAIL,
        //                CauHinh_GiaTri = JsonConvert.SerializeObject(input)
        //            };
        //            await _dmCauHinhRepository.InsertAsync(ch);
        //        }
        //        else
        //            ch.CauHinh_GiaTri = JsonConvert.SerializeObject(input);
        //        rs.Success = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        rs.Message = CommonConst.LoiPhatSinh;
        //        Logger.Error(JsonConvert.SerializeObject(input), ex);
        //    }
        //    return rs;
        //}
        
        //string ValidateAndNormalizeCauHinhEmail(CauHinhEmail input)
        //{
        //    if (string.IsNullOrWhiteSpace(input.MAIL_SMTP))
        //        return "Host không được để trống";
        //    else
        //        input.MAIL_SMTP = input.MAIL_SMTP.Trim().Replace(" ", "");

        //    //if(!CommonFunction.IsValidEmail(input.MAIL_SMTP))
        //    //    return "Host không đúng định dạng";

        //    if (string.IsNullOrWhiteSpace(input.MAIL_FROM))
        //        return "Email gửi không được để trống";
        //    else
        //        input.MAIL_FROM = input.MAIL_FROM.Trim().Replace(" ", "");

        //    if (!CommonFunction.IsValidEmail(input.MAIL_FROM))
        //        return "Email gửi không đúng định dạng";

        //    if (string.IsNullOrWhiteSpace(input.MAIL_ACCOUNT))
        //        return "Tài khoản không được để trống";
        //    else
        //        input.MAIL_ACCOUNT = input.MAIL_ACCOUNT.Trim().Replace(" ", "");

        //    if (!CommonFunction.IsValidEmail(input.MAIL_ACCOUNT))
        //        return "Tài khoản không đúng định dạng";

        //    if (string.IsNullOrWhiteSpace(input.MAIL_PASSWORD))
        //        return "Mật khẩu không được để trống";
        //    else
        //        input.MAIL_PASSWORD = input.MAIL_PASSWORD.Trim().Replace(" ", "");

        //    if (string.IsNullOrWhiteSpace(input.MAIL_PORT))
        //        return "Cổng không được để trống";

        //    input.MAIL_NAME = input.MAIL_NAME?.Trim();

        //    return null;
        //}
        //internal async Task<CauHinhEmail> GetCauHinhEmail(Ql_CoCauToChuc tochuc)
        //{
        //    tochuc = tochuc?.Id > 0 ? tochuc : GetUserInfo_ToChuc().ToChuc;
        //    if (tochuc?.Id > 0)
        //    {
        //        Dm_CauHinh ch = await _dmCauHinhRepository.GetAll().Where(m => m.CauHinh_Ma == MaCauHinh.KETNOIEMAIL && m.ToChuc_Id == tochuc.Id).FirstOrDefaultAsync();
        //        return ch == null ? null : JsonConvert.DeserializeObject<CauHinhEmail>(ch.CauHinh_GiaTri);
        //    }
        //    return null;
        //}
    }
}
