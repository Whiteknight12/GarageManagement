using WebAPI.Models;

namespace WebAPI.Data
{
    public static class SeedData
    {
        public static void SeedInitialData(this ApplicationDbContext context)
        {
            // ====================== Seed ThamSo ====================== 
            var thamSoList = new List<ThamSo>
            {
                new ThamSo
                {
                    Id = Guid.NewGuid(),
                    TenThamSo = "SoXeToiDaTiepNhan",
                    GiaTri = 30,
                    MoTa = "Số xe tối đa có thể tiếp nhận trong một ngày"
                },
                new ThamSo
                {
                    Id = Guid.NewGuid(),
                    TenThamSo = "TiLeDonGiaBan",
                    GiaTri = 1.1,
                    MoTa = "Tỉ lệ đơn giá bán so với đơn giá nhập"
                },
                new ThamSo
                {
                    Id = Guid.NewGuid(),
                    TenThamSo = "VuotSoTienNo",
                    GiaTri = 0,
                    MoTa = "Được phép thu tiền quá tiền khách nợ hay không. 1-có / 0-không"
                }
            };
            context.thamSos.AddRange(thamSoList);

            // ====================== Seed NhomNguoiDung ======================
            var adminGroupId = new Guid("88888888-8888-8888-8888-000000000001");
            var managerGroupId = new Guid("88888888-8888-8888-8888-000000000002");
            var staffGroupId = new Guid("88888888-8888-8888-8888-000000000003");
            var customerGroupId = new Guid("88888888-8888-8888-8888-000000000004");

            var nhomNguoiDungList = new List<NhomNguoiDung>
            {
                new NhomNguoiDung { Id = adminGroupId, TenNhom = "admin" },
                new NhomNguoiDung { Id = managerGroupId, TenNhom = "manager" },
                new NhomNguoiDung { Id = staffGroupId, TenNhom = "staff" },
                new NhomNguoiDung { Id = customerGroupId, TenNhom = "customer" }
            };
            context.nhomNguoiDungs.AddRange(nhomNguoiDungList);

            // ====================== Seed ChucNang ======================
            var functionList = new List<(string name, Guid id)>
            {
                ("quan li nhan vien", Guid.NewGuid()),
                ("quan li danh sach loai tien cong", Guid.NewGuid()),
                ("quan li danh sach hieu xe", Guid.NewGuid()),
                ("lap phieu nhap", Guid.NewGuid()),
                ("lap phieu thu tien", Guid.NewGuid()),
                ("quan li lich su", Guid.NewGuid()),
                ("quan li danh sach xe", Guid.NewGuid()),
                ("quan li phieu tiep nhan", Guid.NewGuid()),
                ("quan li phieu nhap", Guid.NewGuid()),
                ("tiep nhan xe", Guid.NewGuid()),
                ("quan li bao cao ton", Guid.NewGuid()),
                ("quan li danh sach loai vat tu", Guid.NewGuid()),
                ("quan li phieu thu tien", Guid.NewGuid()),
                ("quan li bao cao thang", Guid.NewGuid()),
                ("quan li khach hang", Guid.NewGuid()),
                ("quan li phieu sua chua", Guid.NewGuid()),
                ("khach hang xem danh sach xe", Guid.NewGuid()),
                ("lap phieu sua chua", Guid.NewGuid()),
                ("phan quyen", Guid.NewGuid()),
                ("thay doi tham so", Guid.NewGuid()),
                ("quan li danh sach tai khoan", Guid.NewGuid()),
                ("tao thong bao", Guid.NewGuid())
            };

            var chucNangList = functionList.Select(f => new ChucNang
            {
                Id = f.id,
                TenChucNang = f.name,
                TenManHinhDuocLoad = f.name
            }).ToList();

            context.chucNangs.AddRange(chucNangList);

            // ====================== Seed PhanQuyen ======================
            var phanQuyenList = new List<PhanQuyen>();

            // Admin has access to all functions
            foreach (var function in functionList)
            {
                phanQuyenList.Add(new PhanQuyen
                {
                    Id = Guid.NewGuid(),
                    NhomNguoiDungId = adminGroupId,
                    ChucNangId = function.id
                });
            }

            // Manager has access to most functions except some admin-only functions
            foreach (var function in functionList)
            {
                if (function.name != "phan quyen" && function.name != "thay doi tham so")
                {
                    phanQuyenList.Add(new PhanQuyen
                    {
                        Id = Guid.NewGuid(),
                        NhomNguoiDungId = managerGroupId,
                        ChucNangId = function.id
                    });
                }
            }

            // Staff has access to operational functions
            var staffFunctions = new[]
            {
                "tiep nhan xe",
                "quan li danh sach xe",
                "lap phieu sua chua",
                "quan li phieu sua chua",
                "lap phieu thu tien",
                "quan li khach hang",
                "quan li phieu tiep nhan"
            };

            foreach (var function in functionList.Where(f => staffFunctions.Contains(f.name)))
            {
                phanQuyenList.Add(new PhanQuyen
                {
                    Id = Guid.NewGuid(),
                    NhomNguoiDungId = staffGroupId,
                    ChucNangId = function.id
                });
            }

            // Customer can only view their vehicles
            var customerFunction = functionList.FirstOrDefault(f => f.name == "khach hang xem danh sach xe");
            phanQuyenList.Add(new PhanQuyen
            {
                Id = Guid.NewGuid(),
                NhomNguoiDungId = customerGroupId,
                ChucNangId = customerFunction.id
            });

            context.phanQuyens.AddRange(phanQuyenList);

            // ====================== Seed NhanVien ======================
            var nhanVien1Id = Guid.NewGuid();
            var nhanVien2Id = Guid.NewGuid();
            var nhanVien3Id = Guid.NewGuid();

            var nhanVienList = new List<NhanVien>
            {
                new NhanVien
                {
                    Id = nhanVien1Id,
                    HoTen = "Nguyễn Văn An",
                    CCCD = "012345678901",
                    GioiTinh = "Nam",
                    Tuoi = 35,
                    DiaChi = "123 Nguyễn Trãi, Q.1, TP.HCM",
                    SoDienThoai = "0901234567",
                    Email = "an.nguyen@example.com"
                },
                new NhanVien
                {
                    Id = nhanVien2Id,
                    HoTen = "Trần Thị Bình",
                    CCCD = "012345678902",
                    GioiTinh = "Nữ",
                    Tuoi = 28,
                    DiaChi = "456 Lê Lợi, Q.5, TP.HCM",
                    SoDienThoai = "0912345678",
                    Email = "binh.tran@example.com"
                },
                new NhanVien
                {
                    Id = nhanVien3Id,
                    HoTen = "Lê Văn Cường",
                    CCCD = "012345678903",
                    GioiTinh = "Nam",
                    Tuoi = 42,
                    DiaChi = "789 Cách Mạng Tháng 8, Q.3, TP.HCM",
                    SoDienThoai = "0923456789",
                    Email = "cuong.le@example.com"
                }
            };
            context.nhanViens.AddRange(nhanVienList);

            // ====================== Seed KhachHang ======================
            var khachHang1Id = Guid.NewGuid();
            var khachHang2Id = Guid.NewGuid();
            var khachHang3Id = Guid.NewGuid();
            var khachHang4Id = Guid.NewGuid();

            var khachHangList = new List<KhachHang>
            {
                new KhachHang
                {
                    Id = khachHang1Id,
                    HoVaTen = "Phan Thị Dung",
                    CCCD = "012345678904",
                    Tuoi = 30,
                    DiaChi = "101 Võ Văn Ngân, Q.Thủ Đức, TP.HCM",
                    SoDienThoai = "0934567890",
                    Email = "dung.phan@example.com",
                    TienNo = 0,
                    GioiTinh = "Nữ"
                },
                new KhachHang
                {
                    Id = khachHang2Id,
                    HoVaTen = "Hoàng Văn Em",
                    CCCD = "012345678905",
                    Tuoi = 45,
                    DiaChi = "202 Phạm Văn Đồng, Q.Gò Vấp, TP.HCM",
                    SoDienThoai = "0945678901",
                    Email = "em.hoang@example.com",
                    TienNo = 2000000,
                    GioiTinh = "Nam"
                },
                new KhachHang
                {
                    Id = khachHang3Id,
                    HoVaTen = "Mai Thị Giáng",
                    CCCD = "012345678906",
                    Tuoi = 27,
                    DiaChi = "303 Lê Văn Việt, Q.9, TP.HCM",
                    SoDienThoai = "0956789012",
                    Email = "giang.mai@example.com",
                    TienNo = 500000,
                    GioiTinh = "Nữ"
                },
                new KhachHang
                {
                    Id = khachHang4Id,
                    HoVaTen = "Trịnh Văn Hưng",
                    CCCD = "012345678907",
                    Tuoi = 38,
                    DiaChi = "404 Quang Trung, Q.12, TP.HCM",
                    SoDienThoai = "0967890123",
                    Email = "hung.trinh@example.com",
                    TienNo = 0,
                    GioiTinh = "Nam"
                }
            };
            context.khachHangs.AddRange(khachHangList);

            // ====================== Seed TaiKhoan ======================
            var taiKhoanAdminId = Guid.NewGuid();
            var taiKhoanManagerId = Guid.NewGuid();
            var taiKhoanStaff1Id = Guid.NewGuid();
            var taiKhoanStaff2Id = Guid.NewGuid();
            var taiKhoanCustomer1Id = Guid.NewGuid();
            var taiKhoanCustomer2Id = Guid.NewGuid();

            var taiKhoanList = new List<TaiKhoan>
            {
                new TaiKhoan
                {
                    Id = taiKhoanAdminId,
                    TenDangNhap = "admin",
                    MatKhau = "admin123",
                    NhomNguoiDungId = adminGroupId,
                    NgayCap = DateTime.UtcNow
                },
                new TaiKhoan
                {
                    Id = taiKhoanManagerId,
                    TenDangNhap = "manager",
                    MatKhau = "manager123",
                    NhomNguoiDungId = managerGroupId,
                    NgayCap = DateTime.UtcNow
                },
                new TaiKhoan
                {
                    Id = taiKhoanStaff1Id,
                    TenDangNhap = "staff1",
                    MatKhau = "staff123",
                    NhomNguoiDungId = staffGroupId,
                    NgayCap = DateTime.UtcNow
                },
                new TaiKhoan
                {
                    Id = taiKhoanStaff2Id,
                    TenDangNhap = "staff2",
                    MatKhau = "staff456",
                    NhomNguoiDungId = staffGroupId,
                    NgayCap = DateTime.UtcNow
                },
                new TaiKhoan
                {
                    Id = taiKhoanCustomer1Id,
                    TenDangNhap = "customer1",
                    MatKhau = "customer123",
                    NhomNguoiDungId = customerGroupId,
                    NgayCap = DateTime.UtcNow
                },
                new TaiKhoan
                {
                    Id = taiKhoanCustomer2Id,
                    TenDangNhap = "customer2",
                    MatKhau = "customer456",
                    NhomNguoiDungId = customerGroupId,
                    NgayCap = DateTime.UtcNow
                }
            };
            context.taiKhoans.AddRange(taiKhoanList);

            // Update TaiKhoanId for NhanVien
            nhanVienList[0].TaiKhoanId = taiKhoanAdminId;
            nhanVienList[1].TaiKhoanId = taiKhoanManagerId;
            nhanVienList[2].TaiKhoanId = taiKhoanStaff1Id;

            // Update TaiKhoanId for KhachHang
            khachHangList[0].TaiKhoanId = taiKhoanCustomer1Id;
            khachHangList[2].TaiKhoanId = taiKhoanCustomer2Id;

            // ====================== Seed HieuXe ======================
            var hieuXe1Id = Guid.NewGuid();
            var hieuXe2Id = Guid.NewGuid();
            var hieuXe3Id = Guid.NewGuid();
            var hieuXe4Id = Guid.NewGuid();
            var hieuXe5Id = Guid.NewGuid();

            var hieuXeList = new List<HieuXe>
            {
                new HieuXe
                {
                    Id = hieuXe1Id,
                    TenHieuXe = "Toyota"
                },
                new HieuXe
                {
                    Id = hieuXe2Id,
                    TenHieuXe = "Honda"
                },
                new HieuXe
                {
                    Id = hieuXe3Id,
                    TenHieuXe = "Ford"
                },
                new HieuXe
                {
                    Id = hieuXe4Id,
                    TenHieuXe = "Mazda"
                },
                new HieuXe
                {
                    Id = hieuXe5Id,
                    TenHieuXe = "Hyundai"
                }
            };
            context.hieuXes.AddRange(hieuXeList);

            // ====================== Seed Xe ======================
            var xe1Id = Guid.NewGuid();
            var xe2Id = Guid.NewGuid();
            var xe3Id = Guid.NewGuid();
            var xe4Id = Guid.NewGuid();
            var xe5Id = Guid.NewGuid();
            var xe6Id = Guid.NewGuid();

            var xeList = new List<Xe>
            {
                new Xe
                {
                    Id = xe1Id,
                    Ten = "Toyota Camry",
                    BienSo = "51A-12345",
                    HieuXeId = hieuXe1Id,
                    KhachHangId = khachHang1Id,
                    KhaDung = true,
                    TienNo = 0
                },
                new Xe
                {
                    Id = xe2Id,
                    Ten = "Honda Civic",
                    BienSo = "51A-23456",
                    HieuXeId = hieuXe2Id,
                    KhachHangId = khachHang1Id,
                    KhaDung = true,
                    TienNo = 0
                },
                new Xe
                {
                    Id = xe3Id,
                    Ten = "Ford Ranger",
                    BienSo = "51A-34567",
                    HieuXeId = hieuXe3Id,
                    KhachHangId = khachHang2Id,
                    KhaDung = true,
                    TienNo = 2000000
                },
                new Xe
                {
                    Id = xe4Id,
                    Ten = "Mazda CX-5",
                    BienSo = "51A-45678",
                    HieuXeId = hieuXe4Id,
                    KhachHangId = khachHang3Id,
                    KhaDung = true,
                    TienNo = 500000
                },
                new Xe
                {
                    Id = xe5Id,
                    Ten = "Hyundai Tucson",
                    BienSo = "51A-56789",
                    HieuXeId = hieuXe5Id,
                    KhachHangId = khachHang4Id,
                    KhaDung = true,
                    TienNo = 0
                },
                new Xe
                {
                    Id = xe6Id,
                    Ten = "Toyota Fortuner",
                    BienSo = "51A-67890",
                    HieuXeId = hieuXe1Id,
                    KhachHangId = khachHang4Id,
                    KhaDung = true,
                    TienNo = 0
                }
            };
            context.xes.AddRange(xeList);

            // ====================== Seed PhieuTiepNhan ======================
            var phieuTiepNhan1Id = Guid.NewGuid();
            var phieuTiepNhan2Id = Guid.NewGuid();
            var phieuTiepNhan3Id = Guid.NewGuid();

            var phieuTiepNhanList = new List<PhieuTiepNhan>
            {
                new PhieuTiepNhan
                {
                    Id = phieuTiepNhan1Id,
                    NgayTiepNhan = DateTime.Now.AddDays(-10),
                    XeId = xe1Id,
                    DaHoanThanhBaoTri = true
                },
                new PhieuTiepNhan
                {
                    Id = phieuTiepNhan2Id,
                    NgayTiepNhan = DateTime.Now.AddDays(-5),
                    XeId = xe3Id,
                    DaHoanThanhBaoTri = false
                },
                new PhieuTiepNhan
                {
                    Id = phieuTiepNhan3Id,
                    NgayTiepNhan = DateTime.Now.AddDays(-2),
                    XeId = xe4Id,
                    DaHoanThanhBaoTri = false
                }
            };
            context.phieuTiepNhans.AddRange(phieuTiepNhanList);

            // ====================== Seed NoiDungSuaChua ======================
            var rnd = new Random();

            var noiDungSuaChua1Id = Guid.NewGuid();
            var noiDungSuaChua2Id = Guid.NewGuid();
            var noiDungSuaChua3Id = Guid.NewGuid();
            var noiDungSuaChua4Id = Guid.NewGuid();
            var noiDungSuaChua5Id = Guid.NewGuid();

            var noiDungIds = new List<Guid>(5);
            noiDungIds.Add(noiDungSuaChua1Id);
            noiDungIds.Add(noiDungSuaChua2Id);
            noiDungIds.Add(noiDungSuaChua3Id);
            noiDungIds.Add(noiDungSuaChua4Id);
            noiDungIds.Add(noiDungSuaChua5Id);

            var noiDungSuaChuaList = new List<NoiDungSuaChua>
            {
                new NoiDungSuaChua
                {
                    Id = noiDungSuaChua1Id,
                    TenNoiDungSuaChua = "Thay dầu động cơ"
                },
                new NoiDungSuaChua
                {
                    Id = noiDungSuaChua2Id,
                    TenNoiDungSuaChua = "Thay lọc gió"
                },
                new NoiDungSuaChua
                {
                    Id = noiDungSuaChua3Id,
                    TenNoiDungSuaChua = "Thay má phanh"
                },
                new NoiDungSuaChua
                {
                    Id = noiDungSuaChua4Id,
                    TenNoiDungSuaChua = "Sửa hệ thống điện"
                },
                new NoiDungSuaChua
                {
                    Id = noiDungSuaChua5Id,
                    TenNoiDungSuaChua = "Bảo dưỡng định kỳ"
                }
            };

            int targetCount = 100;
            for (int i = noiDungSuaChuaList.Count + 1; i <= targetCount; i++)
            {
                var newId = Guid.NewGuid();
                noiDungIds.Add(newId);
                noiDungSuaChuaList.Add(new NoiDungSuaChua
                {
                    Id = newId,
                    TenNoiDungSuaChua = $"Nội dung sửa chữa {i}"
                });
            }

            context.noiDungSuaChuas.AddRange(noiDungSuaChuaList);   

            // ====================== Seed TienCong ======================
            var tienCong1Id = Guid.NewGuid();
            var tienCong2Id = Guid.NewGuid();
            var tienCong3Id = Guid.NewGuid();
            var tienCong4Id = Guid.NewGuid();
            var tienCong5Id = Guid.NewGuid();

            var tienCongList = new List<TienCong>
            {
                new TienCong
                {
                    Id = tienCong1Id,
                    TenLoaiTienCong = "Thay dầu",
                    DonGiaLoaiTienCong = 150000,
                    NoiDungSuaChuaId = noiDungSuaChua1Id
                },
                new TienCong
                {
                    Id = tienCong2Id,
                    TenLoaiTienCong = "Thay lọc gió",
                    DonGiaLoaiTienCong = 100000,
                    NoiDungSuaChuaId = noiDungSuaChua2Id
                },
                new TienCong
                {
                    Id = tienCong3Id,
                    TenLoaiTienCong = "Thay má phanh",
                    DonGiaLoaiTienCong = 350000,
                    NoiDungSuaChuaId = noiDungSuaChua3Id
                },
                new TienCong
                {
                    Id = tienCong4Id,
                    TenLoaiTienCong = "Sửa điện",
                    DonGiaLoaiTienCong = 250000,
                    NoiDungSuaChuaId = noiDungSuaChua4Id
                },
                new TienCong
                {
                    Id = tienCong5Id,
                    TenLoaiTienCong = "Bảo dưỡng 10.000 km",
                    DonGiaLoaiTienCong = 500000,
                    NoiDungSuaChuaId = noiDungSuaChua5Id
                }
            };

            

            for (int i = tienCongList.Count + 1; i <= 100; i++)
            {
                tienCongList.Add(new TienCong
                {
                    Id = Guid.NewGuid(),
                    TenLoaiTienCong = $"Tiền công {i}",                                 
                    DonGiaLoaiTienCong = rnd.Next(50_000, 600_000),                    
                    NoiDungSuaChuaId = noiDungIds[rnd.Next(noiDungIds.Count)]        
                });
            }

            context.tienCongs.AddRange(tienCongList);

            // ====================== Seed VatTuPhuTung ======================
            var vatTu1Id = Guid.NewGuid();
            var vatTu2Id = Guid.NewGuid();
            var vatTu3Id = Guid.NewGuid();
            var vatTu4Id = Guid.NewGuid();
            var vatTu5Id = Guid.NewGuid();
            var vatTu6Id = Guid.NewGuid();
            var vatTu7Id = Guid.NewGuid();
            var vatTu8Id = Guid.NewGuid();

            var vatTuPhuTungList = new List<VatTuPhuTung>
            {
                new VatTuPhuTung
                {
                    VatTuPhuTungId = vatTu1Id,
                    TenLoaiVatTuPhuTung = "Dầu động cơ 5W-30",
                    SoLuong = 50,
                    DonGiaBanLoaiVatTuPhuTung = 250000
                },
                new VatTuPhuTung
                {
                    VatTuPhuTungId = vatTu2Id,
                    TenLoaiVatTuPhuTung = "Lọc gió động cơ",
                    SoLuong = 25,
                    DonGiaBanLoaiVatTuPhuTung = 150000
                },
                new VatTuPhuTung
                {
                    VatTuPhuTungId = vatTu3Id,
                    TenLoaiVatTuPhuTung = "Lọc dầu",
                    SoLuong = 40,
                    DonGiaBanLoaiVatTuPhuTung = 120000
                },
                new VatTuPhuTung
                {
                    VatTuPhuTungId = vatTu4Id,
                    TenLoaiVatTuPhuTung = "Má phanh trước",
                    SoLuong = 30,
                    DonGiaBanLoaiVatTuPhuTung = 450000
                },
                new VatTuPhuTung
                {
                    VatTuPhuTungId = vatTu5Id,
                    TenLoaiVatTuPhuTung = "Má phanh sau",
                    SoLuong = 30,
                    DonGiaBanLoaiVatTuPhuTung = 350000
                },
                new VatTuPhuTung
                {
                    VatTuPhuTungId = vatTu6Id,
                    TenLoaiVatTuPhuTung = "Bugi",
                    SoLuong = 60,
                    DonGiaBanLoaiVatTuPhuTung = 95000
                },
                new VatTuPhuTung
                {
                    VatTuPhuTungId = vatTu7Id,
                    TenLoaiVatTuPhuTung = "Dây curoa",
                    SoLuong = 15,
                    DonGiaBanLoaiVatTuPhuTung = 320000
                },
                new VatTuPhuTung
                {
                    VatTuPhuTungId = vatTu8Id,
                    TenLoaiVatTuPhuTung = "Ắc quy",
                    SoLuong = 20,
                    DonGiaBanLoaiVatTuPhuTung = 980000
                }
            };

            var random = new Random();
            for (int i = vatTuPhuTungList.Count + 1; i <= 200; i++)
            {
                vatTuPhuTungList.Add(new VatTuPhuTung
                {
                    VatTuPhuTungId = Guid.NewGuid(),
                    TenLoaiVatTuPhuTung = $"Loại vật tư {i}",    // Tên kiểu “Loại vật tư 9”, “Loại vật tư 10”, …
                    SoLuong = random.Next(10, 100),                // Số lượng ngẫu nhiên 10–99
                    DonGiaBanLoaiVatTuPhuTung = random.Next(50_000, 1_000_000) // Đơn giá ngẫu nhiên
                });
            }

            context.vatTuPhuTungs.AddRange(vatTuPhuTungList);

            // You can also add some PhieuSuaChua and related data if needed
            // This creates a service record for a completed repair
            var phieuSuaChua1Id = Guid.NewGuid();
            var chiTietPSC1Id = Guid.NewGuid();
            var chiTietPSC2Id = Guid.NewGuid();

            var phieuSuaChuaList = new List<PhieuSuaChua>
            {
                new PhieuSuaChua
                {
                    Id = phieuSuaChua1Id,
                    XeId = xe1Id,
                    NgaySuaChua = DateTime.Now.AddDays(-8),
                    TongTien = 550000
                }
            };
            context.phieuSuaChuas.AddRange(phieuSuaChuaList);

            var chiTietPSCList = new List<ChiTietPhieuSuaChua>
            {
                new ChiTietPhieuSuaChua
                {
                    Id = chiTietPSC1Id,
                    PhieuSuaChuaId = phieuSuaChua1Id,
                    TienCongId = tienCong1Id,
                    TienCongApDung = 150000,
                    ThanhTien = 400000 // 150000 + 250000 (dầu)
                },
                new ChiTietPhieuSuaChua
                {
                    Id = chiTietPSC2Id,
                    PhieuSuaChuaId = phieuSuaChua1Id,
                    TienCongId = tienCong3Id,
                    TienCongApDung = 350000,
                    ThanhTien = 800000 // 350000 + 450000 (má phanh)
                }
            };
            context.chiTietPhieuSuaChuas.AddRange(chiTietPSCList);

            var vtptList = new List<VTPTChiTietPhieuSuaChua>
            {
                new VTPTChiTietPhieuSuaChua
                {
                    Id = Guid.NewGuid(),
                    ChiTietPhieuSuaChuaId = chiTietPSC1Id,
                    VatTuPhuTungId = vatTu1Id,
                    SoLuong = 1,
                    DonGiaVTPTApDung = 250000
                },
                new VTPTChiTietPhieuSuaChua
                {
                    Id = Guid.NewGuid(),
                    ChiTietPhieuSuaChuaId = chiTietPSC2Id,
                    VatTuPhuTungId = vatTu4Id,
                    SoLuong = 1,
                    DonGiaVTPTApDung = 450000
                }
            };
            context.vtptChiTietPhieuSuaChuas.AddRange(vtptList);


            // Save changes to database
            context.SaveChanges();
        }
    }
}