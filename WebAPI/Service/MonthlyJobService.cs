using Microsoft.EntityFrameworkCore;
using Quartz;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Service
{
    public class MonthlyJobService : IJob
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public MonthlyJobService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var triggerName = context.Trigger.Key.Name;
            var now = DateTime.UtcNow.ToLocalTime();
            var currentMonth = now.Month;
            var currentYear = now.Year;

            if (triggerName == "FirstDayTrigger")
            {
                // === ĐẦU THÁNG ===
                var vatTu = await _applicationDbContext.vatTuPhuTungs.ToListAsync();

                var baoCaoTon = new BaoCaoTon
                {
                    Id = Guid.NewGuid(),
                    Thang = currentMonth,
                    Nam = currentYear
                };
                _applicationDbContext.baoCaoTons.Add(baoCaoTon);

                var chiTietList = vatTu.Select(vtpt => new ChiTietBaoCaoTon
                {
                    Id = Guid.NewGuid(),
                    VatTuPhuTungId = vtpt.VatTuPhuTungId,
                    TonDau = vtpt.SoLuong,
                    BaoCaoTonId = baoCaoTon.Id,
                    TonCuoi = 0,
                    PhatSinh = 0
                }).ToList();

                _applicationDbContext.chiTietBaoCaoTons.AddRange(chiTietList);
                await _applicationDbContext.SaveChangesAsync();
            }

            else if (triggerName == "LastDayTrigger")
            {
                // === CUỐI THÁNG ===
                var baoCao = await _applicationDbContext.baoCaoTons
                    .FirstOrDefaultAsync(u => u.Thang == currentMonth && u.Nam == currentYear);

                if (baoCao is not null)
                {
                    var chiTietList = await _applicationDbContext.chiTietBaoCaoTons
                        .Where(u => u.BaoCaoTonId == baoCao.Id).ToListAsync();

                    var vtptDict = await _applicationDbContext.vatTuPhuTungs
                        .ToDictionaryAsync(x => x.VatTuPhuTungId, x => x.SoLuong);

                    var phieuNhapList = await _applicationDbContext.phieuNhapVatTus
                        .Where(u => u.NgayNhap.Month == currentMonth && u.NgayNhap.Year == currentYear)
                        .Select(pn => pn.Id)
                        .ToListAsync();

                    var chiTietNhapList = await _applicationDbContext.chiTietPhieuNhapVatTus
                        .Where(ct => phieuNhapList.Contains(ct.PhieuNhapId))
                        .ToListAsync();

                    foreach (var chiTiet in chiTietList)
                    {
                        chiTiet.TonCuoi = vtptDict.TryGetValue(chiTiet.VatTuPhuTungId, out var ton) ? ton : 0;
                        chiTiet.PhatSinh = chiTietNhapList
                            .Where(x => x.VatTuId == chiTiet.VatTuPhuTungId)
                            .Sum(x => x.SoLuong);
                    }

                    _applicationDbContext.chiTietBaoCaoTons.UpdateRange(chiTietList);
                    await _applicationDbContext.SaveChangesAsync();
                }
            }
        }
    }
}
