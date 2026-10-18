using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab03_OOPAdvanced
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            while (true)
            {
                Console.WriteLine("\n========== LAB 03 - OOP ADVANCED ==========");
                Console.WriteLine(" 1. Bài 1 : Kế thừa Nguoi - SinhVien");
                Console.WriteLine(" 2. Bài 2 : Ghi đè phương thức hiển thị (virtual/override)");
                Console.WriteLine(" 3. Bài 3 : Lớp trừu tượng NguoiHoc - Học phí");
                Console.WriteLine(" 4. Bài 4 : Interface IValidatable, IPrintable");
                Console.WriteLine(" 5. Bài 5 : Danh sách đa hình List<NguoiHoc>");
                Console.WriteLine(" 6. Bài 6 : Hệ thống nhân viên");
                Console.WriteLine(" 7. Bài 7 : Quản lý phương tiện (IMove)");
                Console.WriteLine(" 8. Bài 8 : Quản lý tài liệu thư viện");
                Console.WriteLine(" 9. Bài 9 : Hệ thống thanh toán (IPayment)");
                Console.WriteLine("10. Bài 10: Nâng cấp quản lý sinh viên");
                Console.WriteLine(" 0. Thoát");
                Console.Write("Chọn bài: ");
                switch (Console.ReadLine())
                {
                    case "1": Bai1(); break;
                    case "2": Bai2(); break;
                    case "3": Bai3(); break;
                    case "4": Bai4(); break;
                    case "5": Bai5(); break;
                    case "6": Bai6(); break;
                    case "7": Bai7(); break;
                    case "8": Bai8(); break;
                    case "9": Bai9(); break;
                    case "10": new QuanLySinhVien().Chay(); break;
                    case "0": return;
                    default: Console.WriteLine("Lựa chọn không hợp lệ."); break;
                }
            }
        }

        static void Bai1()
        {
            Console.WriteLine("\n[Bài 1] Kế thừa Nguoi - SinhVien");
            SinhVien sv = new SinhVien("24000091", "Nguyễn Tiến Hùng", new DateTime(2006, 10, 26), 9.2);

            Console.WriteLine("HoTen   : " + sv.HoTen);
            Console.WriteLine("NgaySinh: " + sv.NgaySinh.ToString("dd/MM/yyyy"));
            Console.WriteLine("Tuổi    : " + sv.TinhTuoi());
            Console.WriteLine("MaSV    : " + sv.MaSV);
            Console.WriteLine("DiemTB  : " + sv.DiemTB.ToString("N2"));

            Nguoi n = sv;   
            Console.WriteLine("SinhVien có thể gán cho biến kiểu Nguoi: " + n.HoTen);
        }

        static void Bai2()
        {
            Console.WriteLine("\n[Bài 2] Ghi đè MoTa()");
            Nguoi nguoi = new Nguoi("Trần Văn Nam", new DateTime(1990, 3, 15));
            Console.WriteLine("Nguoi    : " + nguoi.MoTa());

            SinhVien sv = new SinhVien("24000465", "Lê Đức Bình", new DateTime(2006, 4, 25), 8.1);
            Console.WriteLine("SinhVien : " + sv.MoTa());

            Nguoi thamChieu = sv;   
            Console.WriteLine("Qua tham chiếu Nguoi: " + thamChieu.MoTa());
        }

        static void Bai3()
        {
            Console.WriteLine("\n[Bài 3] Lớp trừu tượng NguoiHoc");

            NguoiHoc a = new SinhVienChinhQuy { HoTen = "An", SoTinChi = 18 };
            NguoiHoc b = new SinhVienLienThong { HoTen = "Bình", SoTinChi = 15 };

            Console.WriteLine("{0} ({1}) - {2} TC x 450.000 = {3:N0}", a.HoTen, a.LoaiHinh, a.SoTinChi, a.TinhHocPhi());
            Console.WriteLine("{0} ({1}) - {2} TC x 520.000 = {3:N0}", b.HoTen, b.LoaiHinh, b.SoTinChi, b.TinhHocPhi());
        }

        static void Bai4()
        {
            Console.WriteLine("\n[Bài 4] IValidatable và IPrintable");
            SinhVien[] mau =
            {
                new SinhVien("24000091", "Nguyễn Tiến Hùng", new DateTime(2006, 10, 26), 9.2),
                new SinhVien("", "Lê Đức Bình", new DateTime(2006, 4, 25), 8.1),
                new SinhVien("24000474", "Nguyễn Thiện Chiến", new DateTime(2006, 9, 27), 11)
            };

            foreach (SinhVien sv in mau)
            {
                IPrintable p = sv;
                IValidatable v = sv;
                string thongBao;
                bool ok = v.IsValid(out thongBao);
                Console.WriteLine("{0,-35} -> {1}", p.ToDisplayText(), ok ? "Hợp lệ" : "Không hợp lệ: " + thongBao);
            }
        }

        static void Bai5()
        {
            Console.WriteLine("\n[Bài 5] List<NguoiHoc> - đa hình");
            List<NguoiHoc> ds = new List<NguoiHoc>();
            ds.Add(new SinhVienChinhQuy { HoTen = "An", SoTinChi = 18 });
            ds.Add(new SinhVienLienThong { HoTen = "Bình", SoTinChi = 15 });
            ds.Add(new SinhVienChinhQuy { HoTen = "Chi", SoTinChi = 20 });
            ds.Add(new SinhVienLienThong { HoTen = "Dũng", SoTinChi = 12 });

            decimal tong = 0;
            foreach (NguoiHoc item in ds)
            {
                decimal hocPhi = item.TinhHocPhi();   
                tong += hocPhi;
                Console.WriteLine("{0,-6} ({1,-10}): {2,12:N0}", item.HoTen, item.LoaiHinh, hocPhi);
            }
            Console.WriteLine("Tổng học phí: {0:N0}", tong);
        }

        static void Bai6()
        {
            Console.WriteLine("\n[Bài 6] Hệ thống nhân viên - tính lương đa hình");
            List<NhanVien> ds = new List<NhanVien>
            {
                new NhanVienVanPhong("NV01", "Nguyễn An", 8000000m, 24),
                new NhanVienKinhDoanh("NV02", "Lê Chi", 7000000m, 120000000m, 0.05m),
                new GiangVien("NV03", "Phạm Dũng", 9000000m, 40, 200000m)
            };

            Console.WriteLine("{0,-22}{1,-8}{2,-14}{3,16}", "Chức vụ", "Mã NV", "Họ tên", "Lương");
            Console.WriteLine(new string('-', 60));
            decimal tong = 0;
            NhanVien cao = ds[0];
            foreach (NhanVien nv in ds)
            {
                decimal luong = nv.TinhLuong();  
                tong += luong;
                if (luong > cao.TinhLuong()) cao = nv;
                Console.WriteLine("{0,-22}{1,-8}{2,-14}{3,16:N0}", nv.ChucVu, nv.MaNV, nv.HoTen, luong);
            }
            Console.WriteLine(new string('-', 60));
            Console.WriteLine("{0,-44}{1,16:N0}", "Tổng quỹ lương:", tong);
            Console.WriteLine("Lương cao nhất: {0} ({1:N0})", cao.HoTen, cao.TinhLuong());
        }

        static void Bai7()
        {
            Console.WriteLine("\n[Bài 7] Phương tiện - IMove (quãng đường 30 km)");
            const double quangDuong = 30;
            List<IMove> ds = new List<IMove> { new XeMay(), new OTo(), new XeDap() };

            foreach (IMove pt in ds)
            {
                Console.WriteLine("\n" + pt.TenPhuongTien + " (tốc độ tối đa " + pt.TocDoToiDa + " km/h)");
                for (int i = 1; i <= 4; i++)
                {
                    pt.TangToc(30);
                    Console.WriteLine("  Tăng tốc lần {0}: {1} km/h", i, pt.TocDoHienTai);
                }
                double phut = quangDuong / pt.TocDoHienTai * 60;
                Console.WriteLine("  Đi {0} km ở {1} km/h mất {2:N1} phút", quangDuong, pt.TocDoHienTai, phut);
                Console.WriteLine("  Chi phí vận hành: {0:N0} đ", pt.ChiPhiVanHanh(quangDuong));
                pt.Dung();
                Console.WriteLine("  Dừng xe: {0} km/h", pt.TocDoHienTai);
            }
        }

        static void ThemTaiLieu(List<TaiLieu> ds, TaiLieu tl)
        {
            if (ds.Any(x => x.MaTL.Equals(tl.MaTL, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException("Mã tài liệu đã tồn tại: " + tl.MaTL);
            ds.Add(tl);
        }

        static void InTaiLieu(IEnumerable<TaiLieu> ds)
        {
            bool coDuLieu = false;
            foreach (TaiLieu tl in ds)
            {
                Console.WriteLine("  " + tl.MoTa());   
                coDuLieu = true;
            }
            if (!coDuLieu) Console.WriteLine("  (Không có tài liệu nào)");
        }

        static void Bai8()
        {
            Console.WriteLine("\n[Bài 8] Quản lý tài liệu thư viện");
            List<TaiLieu> ds = new List<TaiLieu>();
            ThemTaiLieu(ds, new Sach("S01", "Lập trình C# căn bản", 2022, "Nguyễn Văn A", "NXB Giáo dục", 320));
            ThemTaiLieu(ds, new Sach("S02", "Cấu trúc dữ liệu và giải thuật", 2020, "Trần Thị B", "NXB Khoa học", 410));
            ThemTaiLieu(ds, new TapChi("T01", "Tạp chí Công nghệ thông tin", 2024, 5, 3));
            ThemTaiLieu(ds, new TapChi("T02", "Tạp chí Khoa học máy tính", 2023, 12, 11));
            ThemTaiLieu(ds, new LuanVan("L01", "Ứng dụng lập trình hướng đối tượng trong quản lý", 2023, "Lê Văn C", "TS. Phạm D", "Thạc sĩ"));

            try { ThemTaiLieu(ds, new Sach("S01", "Sách trùng mã", 2021, "X", "Y", 100)); }
            catch (InvalidOperationException ex) { Console.WriteLine("Thêm trùng mã -> Lỗi: " + ex.Message); }

            Console.WriteLine("\nToàn bộ tài liệu:");
            InTaiLieu(ds);

            Console.WriteLine("\nChỉ Sách:");
            InTaiLieu(ds.OfType<Sach>());

            Console.WriteLine("\nChỉ Tạp chí:");
            InTaiLieu(ds.OfType<TapChi>());

            Console.WriteLine("\nChỉ Luận văn:");
            InTaiLieu(ds.OfType<LuanVan>());

            Console.Write("\nNhập từ khóa tiêu đề cần tìm: ");
            string kw = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(kw)) { Console.WriteLine("Chưa nhập từ khóa."); return; }
            kw = kw.Trim();
            Console.WriteLine("Kết quả tìm \"{0}\":", kw);
            InTaiLieu(ds.Where(x => x.TieuDe.IndexOf(kw, StringComparison.OrdinalIgnoreCase) >= 0));
        }

        static void Bai9()
        {
            Console.WriteLine("\n[Bài 9] Hệ thống thanh toán");
            XuLyThanhToan xl = new XuLyThanhToan();
            ChuyenKhoan ck = new ChuyenKhoan("0123456789", 2000000m);
            ViDienTu vi = new ViDienTu("MoMo", 1000000m);
            ThanhToanTienMat tm = new ThanhToanTienMat();

            IPayment[] pt = { tm, tm, ck, ck, vi, vi };
            decimal[] soTien = { 500000m, 50000000m, 1500000m, 1000000m, 300000m, 6000000m };

            for (int i = 0; i < pt.Length; i++)
                Console.WriteLine(xl.ThucHien(pt[i], soTien[i]));

            decimal tongThanhCong = 0;
            int soThanhCong = 0;
            foreach (GiaoDich gd in xl.LichSu)
            {
                if (gd.ThanhCong) { tongThanhCong += gd.SoTien; soThanhCong++; }
            }
            Console.WriteLine("\nTổng {0} giao dịch, thành công {1}, tổng tiền thành công: {2:N0}",
                xl.LichSu.Count, soThanhCong, tongThanhCong);
            Console.WriteLine("Số dư tài khoản còn lại: {0:N0}", ck.SoDu);
            Console.WriteLine("Số dư ví còn lại       : {0:N0}", vi.SoDu);
        }
    }
}
