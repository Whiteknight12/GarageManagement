using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GarageManagement.ViewModels
{
    public partial class ChatBotPageViewmodel : BaseViewModel
    {
        private string _knowledgeBase = string.Empty;
        // Add these injections to the class
        private readonly APIClientService<PhieuTiepNhan> _phieuTiepNhanService;
        private readonly APIClientService<KhachHang> _khachHangService;
        private readonly APIClientService<HieuXe> _hieuXeService;
        private readonly APIClientService<TienCong> _tienCongService;
        private readonly APIClientService<VatTuPhuTung> _vatTuPhuTungService;
        private readonly APIClientService<ChiTietPhieuSuaChua> _chiTietPhieuSuaChuaService;
        private readonly APIClientService<PhieuSuaChua> _phieuSuaChuaService;
        private readonly APIClientService<PhieuThuTien> _phieuThuTienService;
        private readonly APIClientService<PhieuNhapVatTu> _phieuNhapVatTuService;
        private readonly APIClientService<ChiTietPhieuNhapVatTu> _chiTietPhieuNhapVatTuService;
        private readonly APIClientService<BaoCaoDoanhThuThang> _baoCaoDoanhThuThangService;
        private readonly APIClientService<ChiTietBaoCaoDoanhThuThang> _chiTietBaoCaoDoanhThuThangService;
        private readonly APIClientService<BaoCaoTon> _baoCaoTonService;
        private readonly APIClientService<ChiTietBaoCaoTon> _chiTietBaoCaoTonService;
        private readonly APIClientService<NoiDungSuaChua> _noiDungSuaChuaService;
        private readonly APIClientService<LichSu> _lichSuService;
        private readonly APIClientService<VTPTChiTietPhieuSuaChua> _vtptChiTietPhieuSuaChuaService;
        private readonly APIClientService<ThongBao> _thongBaoService;
        private readonly APIClientService<NguoiDungThongBao> _nguoiDungThongBaoService;
        private readonly APIClientService<Xe> _xeService;
        private readonly APIClientService<ThamSo> _thamSoService;
        private readonly APIClientService<NhomNguoiDung> _nhomNguoiDungService;

        [ObservableProperty]
        private string currentMessage;

        [ObservableProperty]
        private ObservableCollection<MessageItem> messages = new();

        [ObservableProperty]
        private bool isInitialDataLoaded = false;

        [ObservableProperty]
        private string statusMessage = "Đang kết nối với cơ sở dữ liệu...";

        [ObservableProperty]
        private bool isProcessingQuery = false;

        public ChatBotPageViewmodel(
            APIClientService<PhieuTiepNhan> phieuTiepNhanService,
            APIClientService<KhachHang> khachHangService,
            APIClientService<HieuXe> hieuXeService,
            APIClientService<TienCong> tienCongService,
            APIClientService<VatTuPhuTung> vatTuPhuTungService,
            APIClientService<ChiTietPhieuSuaChua> chiTietPhieuSuaChuaService,
            APIClientService<PhieuSuaChua> phieuSuaChuaService,
            APIClientService<PhieuThuTien> phieuThuTienService,
            APIClientService<PhieuNhapVatTu> phieuNhapVatTuService,
            APIClientService<ChiTietPhieuNhapVatTu> chiTietPhieuNhapVatTuService,
            APIClientService<BaoCaoDoanhThuThang> baoCaoDoanhThuThangService,
            APIClientService<ChiTietBaoCaoDoanhThuThang> chiTietBaoCaoDoanhThuThangService,
            APIClientService<BaoCaoTon> baoCaoTonService,
            APIClientService<ChiTietBaoCaoTon> chiTietBaoCaoTonService,
            APIClientService<NoiDungSuaChua> noiDungSuaChuaService,
            APIClientService<LichSu> lichSuService,
            APIClientService<VTPTChiTietPhieuSuaChua> vtptChiTietPhieuSuaChuaService,
            APIClientService<ThongBao> thongBaoService,
            APIClientService<NguoiDungThongBao> nguoiDungThongBaoService,
            APIClientService<Xe> xeService,
            APIClientService<ThamSo> thamSoService,
            APIClientService<NhomNguoiDung> nhomNguoiDungService)
        {
            Messages = new ObservableCollection<MessageItem>();
            _phieuTiepNhanService = phieuTiepNhanService;
            _khachHangService = khachHangService;
            _hieuXeService = hieuXeService;
            _tienCongService = tienCongService;
            _vatTuPhuTungService = vatTuPhuTungService;
            _chiTietPhieuSuaChuaService = chiTietPhieuSuaChuaService;
            _phieuSuaChuaService = phieuSuaChuaService;
            _phieuThuTienService = phieuThuTienService;
            _phieuNhapVatTuService = phieuNhapVatTuService;
            _chiTietPhieuNhapVatTuService = chiTietPhieuNhapVatTuService;
            _baoCaoDoanhThuThangService = baoCaoDoanhThuThangService;
            _chiTietBaoCaoDoanhThuThangService = chiTietBaoCaoDoanhThuThangService;
            _baoCaoTonService = baoCaoTonService;
            _chiTietBaoCaoTonService = chiTietBaoCaoTonService;
            _noiDungSuaChuaService = noiDungSuaChuaService;
            _lichSuService = lichSuService;
            _vtptChiTietPhieuSuaChuaService = vtptChiTietPhieuSuaChuaService;
            _thongBaoService = thongBaoService;
            _nguoiDungThongBaoService = nguoiDungThongBaoService;
            _xeService = xeService;
            _thamSoService = thamSoService;

            // Initialize by adding welcome message
            Messages.Add(new MessageItem
            {
                Text = "Xin chào! Tôi là trợ lý ảo Garage AI. Tôi có thể giúp bạn tìm kiếm thông tin về garage, xe cộ, khách hàng và nhiều thứ khác. Hãy đặt câu hỏi!",
                IsUser = false
            });

            // Load data asynchronously
            
            _nhomNguoiDungService = nhomNguoiDungService;
            _ = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            try
            {
                IsBusy = true;
                StatusMessage = "Đang tải dữ liệu từ cơ sở dữ liệu...";

                _knowledgeBase = await GenerateKnowledgeBaseAsync();

                IsInitialDataLoaded = true;
                IsBusy = false;
                StatusMessage = "Sẵn sàng trả lời câu hỏi!";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Lỗi kết nối: {ex.Message}";
            }
        }

        // Add this method to retrieve knowledge from all database entities
        private async Task<string> GenerateKnowledgeBaseAsync()
        {
            StringBuilder knowledgeBase = new StringBuilder();

            try
            {
                // Get basic counts of all entities
                knowledgeBase.AppendLine("# Thông tin cơ sở dữ liệu");

                // 1. Phiếu tiếp nhận xe
                var phieuTiepNhans = await _phieuTiepNhanService.GetAll() ?? new List<PhieuTiepNhan>();
                knowledgeBase.AppendLine($"- Có {phieuTiepNhans.Count} phiếu tiếp nhận xe");
                var pendingPhieus = phieuTiepNhans.Count(p => !p.DaHoanThanhBaoTri);
                knowledgeBase.AppendLine($"  + Đang chờ bảo trì: {pendingPhieus} xe");
                knowledgeBase.AppendLine($"  + Đã hoàn thành bảo trì: {phieuTiepNhans.Count - pendingPhieus} xe");

                // 2. Khách hàng
                var khachHangs = await _khachHangService.GetAll() ?? new List<KhachHang>();
                knowledgeBase.AppendLine($"- Có {khachHangs.Count} khách hàng");
                var khWithDebt = khachHangs.Count(k => k.TienNo > 0);
                knowledgeBase.AppendLine($"  + Khách hàng có nợ: {khWithDebt} khách hàng");
                var totalDebt = khachHangs.Sum(k => k.TienNo);
                knowledgeBase.AppendLine($"  + Tổng số tiền nợ: {totalDebt:N0} VND");

                // Top khách hàng nợ nhiều nhất
                var topDebtors = khachHangs.Where(k => k.TienNo > 0)
                    .OrderByDescending(k => k.TienNo)
                    .Take(3)
                    .ToList();

                if (topDebtors.Any())
                {
                    knowledgeBase.AppendLine("  + Top 3 khách hàng nợ nhiều nhất:");
                    foreach (var kh in topDebtors)
                    {
                        knowledgeBase.AppendLine($"    * {kh.HoVaTen}: {kh.TienNo:N0} VND, SĐT: {kh.SoDienThoai}");
                    }
                }

                // 3. Hiệu xe
                var hieuXes = await _hieuXeService.GetAll() ?? new List<HieuXe>();
                knowledgeBase.AppendLine($"- Có {hieuXes.Count} hiệu xe");
                knowledgeBase.AppendLine("  + Các hiệu xe: " + string.Join(", ", hieuXes.Select(h => h.TenHieuXe).Where(n => !string.IsNullOrEmpty(n))));

                // 4. Tiền công
                var tienCongs = await _tienCongService.GetAll() ?? new List<TienCong>();
                knowledgeBase.AppendLine($"- Có {tienCongs.Count} loại tiền công");
                if (tienCongs.Any())
                {
                    var avgCost = tienCongs.Average(tc => tc.DonGiaLoaiTienCong);
                    knowledgeBase.AppendLine($"  + Đơn giá trung bình: {avgCost:N0} VND");

                    // Top 3 tiền công cao nhất
                    var topTienCongs = tienCongs.OrderByDescending(tc => tc.DonGiaLoaiTienCong).Take(3);
                    knowledgeBase.AppendLine("  + Top 3 tiền công cao nhất:");
                    foreach (var tc in topTienCongs)
                    {
                        knowledgeBase.AppendLine($"    * {tc.TenLoaiTienCong}: {tc.DonGiaLoaiTienCong:N0} VND");
                    }
                }

                // 5. Vật tư phụ tùng
                var vatTus = await _vatTuPhuTungService.GetAll() ?? new List<VatTuPhuTung>();
                knowledgeBase.AppendLine($"- Có {vatTus.Count} loại vật tư phụ tùng");
                var outOfStock = vatTus.Count(v => v.SoLuong <= 5);
                knowledgeBase.AppendLine($"  + Sắp hết hàng (SL ≤ 5): {outOfStock} loại");

                // Danh sách vật tư sắp hết hàng
                var lowStock = vatTus.Where(v => v.SoLuong <= 5).OrderBy(v => v.SoLuong).Take(5);
                if (lowStock.Any())
                {
                    knowledgeBase.AppendLine("  + Vật tư sắp hết hàng:");
                    foreach (var vt in lowStock)
                    {
                        knowledgeBase.AppendLine($"    * {vt.TenLoaiVatTuPhuTung}: Còn {vt.SoLuong} cái/chiếc");
                    }
                }

                // 6. Phiếu sửa chữa
                var phieuSuaChuas = await _phieuSuaChuaService.GetAll() ?? new List<PhieuSuaChua>();
                knowledgeBase.AppendLine($"- Có {phieuSuaChuas.Count} phiếu sửa chữa");
                if (phieuSuaChuas.Any())
                {
                    var totalRevenue = phieuSuaChuas.Sum(p => p.TongTien);
                    knowledgeBase.AppendLine($"  + Tổng doanh thu từ sửa chữa: {totalRevenue:N0} VND");

                    // Lấy 5 phiếu gần nhất
                    var recentPhieus = phieuSuaChuas.OrderByDescending(p => p.NgaySuaChua).Take(5);
                    knowledgeBase.AppendLine("  + 5 phiếu sửa chữa gần đây nhất:");
                    foreach (var phieu in recentPhieus)
                    {
                        knowledgeBase.AppendLine($"    * Phiếu {phieu.Id} - Ngày {phieu.NgaySuaChua.ToShortDateString()} - {phieu.TongTien:N0} VND");
                    }
                }

                // 7. Phiếu thu tiền
                var phieuThuTiens = await _phieuThuTienService.GetAll() ?? new List<PhieuThuTien>();
                knowledgeBase.AppendLine($"- Có {phieuThuTiens.Count} phiếu thu tiền");
                if (phieuThuTiens.Any())
                {
                    var totalCollected = phieuThuTiens.Sum(p => p.SoTienThu);
                    knowledgeBase.AppendLine($"  + Tổng số tiền đã thu: {totalCollected:N0} VND");

                    // Các phiếu thu tiền gần đây
                    var recentPhieuThu = phieuThuTiens.OrderByDescending(p => p.NgayThuTien).Take(3);
                    knowledgeBase.AppendLine("  + Các phiếu thu tiền gần đây:");
                    foreach (var phieu in recentPhieuThu)
                    {
                        knowledgeBase.AppendLine($"    * Ngày {phieu.NgayThuTien.ToShortDateString()}: {phieu.SoTienThu:N0} VND");
                    }
                }

                // 8. Nhập vật tư
                var phieuNhaps = await _phieuNhapVatTuService.GetAll() ?? new List<PhieuNhapVatTu>();
                knowledgeBase.AppendLine($"- Có {phieuNhaps.Count} phiếu nhập vật tư");
                if (phieuNhaps.Any())
                {
                    var totalCost = phieuNhaps.Sum(p => p.TongTien);
                    knowledgeBase.AppendLine($"  + Tổng chi phí nhập vật tư: {totalCost:N0} VND");
                }

                // 9. Xe
                var xes = await _xeService.GetAll() ?? new List<Xe>();
                knowledgeBase.AppendLine($"- Có {xes.Count} xe trong hệ thống");
                var xesDangSua = xes.Count(x => x.KhaDung.Value);
                knowledgeBase.AppendLine($"  + Xe đang trong quá trình sửa chữa: {xesDangSua} xe");

                // 10. Báo cáo
                var baoCaos = await _baoCaoDoanhThuThangService.GetAll() ?? new List<BaoCaoDoanhThuThang>();
                knowledgeBase.AppendLine($"- Có {baoCaos.Count} báo cáo doanh thu tháng");

                if (baoCaos.Any())
                {
                    // Tìm báo cáo mới nhất
                    var latestReport = baoCaos.OrderByDescending(b => b.Nam).ThenByDescending(b => b.Thang).FirstOrDefault();
                    if (latestReport != null)
                    {
                        knowledgeBase.AppendLine($"  + Báo cáo mới nhất: Tháng {latestReport.Thang}/{latestReport.Nam} - Doanh thu {latestReport.TongDoanhThu:N0} VND");
                    }
                }

                // 11. Tham số hệ thống
                var thamSos = await _thamSoService.GetAll() ?? new List<ThamSo>();
                knowledgeBase.AppendLine($"- Có {thamSos.Count} tham số hệ thống đã cấu hình");

                // 12. Thông báo
                var thongBaos = await _thongBaoService.GetAll() ?? new List<ThongBao>();
                knowledgeBase.AppendLine($"- Có {thongBaos.Count} thông báo");

                // Thông tin chi tiết về các thông báo gần đây
                var recentThongBaos = thongBaos.OrderByDescending(t => t.taoVaoLuc).Take(3);
                if (recentThongBaos.Any())
                {
                    knowledgeBase.AppendLine("  + Các thông báo gần đây:");
                    foreach (var tb in recentThongBaos)
                    {
                        knowledgeBase.AppendLine($"    * {tb.taoVaoLuc.ToShortDateString()}: {tb.Content}");
                    }
                }

                // Thông tin chi tiết về mỗi loại vật tư
                knowledgeBase.AppendLine("\n# Thông tin chi tiết về vật tư phụ tùng");
                foreach (var vatTu in vatTus.Take(10)) // Chỉ lấy 10 vật tư để không quá dài
                {
                    knowledgeBase.AppendLine($"- {vatTu.TenLoaiVatTuPhuTung ?? "Không tên"}: SL: {vatTu.SoLuong}, Giá bán: {vatTu.DonGiaBanLoaiVatTuPhuTung:N0} VND");
                }

                // Phân tích tình trạng tài chính
                var totalRevenueAllTime = phieuSuaChuas.Sum(p => p.TongTien);
                var totalCollectedAllTime = phieuThuTiens.Sum(p => p.SoTienThu);
                var totalExpenseAllTime = phieuNhaps.Sum(p => p.TongTien);
                var currentDebt = khachHangs.Sum(k => k.TienNo);

                knowledgeBase.AppendLine("\n# Tổng quan tài chính");
                knowledgeBase.AppendLine($"- Tổng doanh thu: {totalRevenueAllTime:N0} VND");
                knowledgeBase.AppendLine($"- Tổng chi phí: {totalExpenseAllTime:N0} VND");
                knowledgeBase.AppendLine($"- Tổng tiền đã thu: {totalCollectedAllTime:N0} VND");
                knowledgeBase.AppendLine($"- Tổng tiền nợ hiện tại: {currentDebt:N0} VND");

                return knowledgeBase.ToString();
            }
            catch (Exception ex)
            {
                return "Đã xảy ra lỗi khi truy xuất dữ liệu: " + ex.Message;
            }
        }

        [RelayCommand]
        public async Task SendMessageAsync()
        {
            if (string.IsNullOrWhiteSpace(CurrentMessage)) return;

            var userMessage = new MessageItem { Text = CurrentMessage, IsUser = true };
            Messages.Add(userMessage);

            try
            {
                IsProcessingQuery = true;
                var reply = await CallAIService(CurrentMessage);
                Messages.Add(new MessageItem { Text = reply, IsUser = false });
            }
            catch (Exception ex)
            {
                Messages.Add(new MessageItem { Text = $"Đã xảy ra lỗi: {ex.Message}", IsUser = false });
            }
            finally
            {
                IsProcessingQuery = false;
                CurrentMessage = string.Empty;
            }
        }

        [RelayCommand]
        public async Task QueryDatabaseAsync(string queryType)
        {
            IsBusy = true;
            string query = string.Empty;

            switch (queryType)
            {
                case "vattu":
                    query = "Liệt kê các loại vật tư phụ tùng sắp hết hàng";
                    break;
                case "khachhang":
                    query = "Khách hàng nào đang nợ nhiều nhất?";
                    break;
                case "doanhso":
                    query = "Báo cáo tóm tắt doanh số hiện tại";
                    break;
                case "xesuachua":
                    query = "Có bao nhiêu xe đang trong quá trình sửa chữa?";
                    break;
                default:
                    query = "Tóm tắt tình hình chung của garage";
                    break;
            }

            Messages.Add(new MessageItem { Text = query, IsUser = true });

            try
            {
                IsProcessingQuery = true;
                var reply = await CallAIService(query);
                Messages.Add(new MessageItem { Text = reply, IsUser = false });
            }
            catch (Exception ex)
            {
                Messages.Add(new MessageItem { Text = $"Đã xảy ra lỗi: {ex.Message}", IsUser = false });
            }
            finally
            {
                IsProcessingQuery = false;
                IsBusy = false;
            }
        }

        private async Task<string> CallAIService(string prompt)
        {
            if (!IsInitialDataLoaded)
            {
                _knowledgeBase = await GenerateKnowledgeBaseAsync();
            }

            using var client = new HttpClient();
            var apiKey = "AIzaSyBf0xyHSQW2A4Y2Tf6d-0R0GD_8XRz0WcE"; // Note: In a production app, this should be stored securely
            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key={apiKey}";

            var systemPrompt = "Bạn là chatbot hỗ trợ cho phần mềm quản lý garage ô tô. " +
                               "Hãy dựa vào dữ liệu được cung cấp để trả lời câu hỏi từ người dùng. " +
                               "Nếu không có thông tin cần thiết, hãy cho biết bạn không có đủ thông tin. " +
                               "Luôn trả lời bằng tiếng Việt một cách lịch sự và chuyên nghiệp.";

            var json = new
            {
                contents = new[]
                {
                    new
                    {
                        role = "user",
                        parts = new[]
                        {
                            new { text = systemPrompt + "\n\nDữ liệu:\n" + _knowledgeBase + "\n\nCâu hỏi: " + prompt }
                        }
                    }
                },
                generationConfig = new
                {
                    temperature = 0.7,
                    topK = 40,
                    topP = 0.9,
                    maxOutputTokens = 800,
                    stopSequences = new string[] { }
                }
            };

            var content = new StringContent(
                JsonSerializer.Serialize(json),
                Encoding.UTF8,
                "application/json"
            );

            try
            {
                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return $"Đã có lỗi xảy ra: {(int)response.StatusCode} - {errorContent}";
                }

                var responseString = await response.Content.ReadAsStringAsync();

                using var doc = JsonDocument.Parse(responseString);

                var text = doc.RootElement
                    .GetProperty("candidates")[0]
                    .GetProperty("content")
                    .GetProperty("parts")[0]
                    .GetProperty("text")
                    .GetString();

                return text ?? "Không có phản hồi.";
            }
            catch (Exception ex)
            {
                return $"Lỗi khi gọi API: {ex.Message}";
            }
        }
    }

    public class MessageItem
    {
        public string Text { get; set; }
        public bool IsUser { get; set; }
    }
}