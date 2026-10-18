using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab03_OOPAdvanced
{
    public abstract class SinhVienDaoTao : SinhVien, IExportable
    {
        public int SoTinChi { get; set; }

        protected SinhVienDaoTao(string ma, string ten, DateTime ngaySinh, double diem, int soTinChi)
            : base(ma, ten, ngaySinh, diem)
        {
            SoTinChi = soTinChi;
        }

        public abstract string LoaiSV { get; }
        protected abstract decimal DonGiaTinChi { get; }

        public decimal TinhHocPhi()
        {
            return SoTinChi * DonGiaTinChi;
        }

        public override string MoTa()
        {
            return "[" + LoaiSV + "] " + base.MoTa() + " - " + SoTinChi + " TC";
        }

        public override bool IsValid(out string message)
        {
            if (!base.IsValid(out message)) return false;
            if (SoTinChi < 0 || SoTinChi > 30)
            {
                message = "Số tín chỉ phải trong [0, 30]";
                return false;
            }
            message = "";
            return true;
        }

        public string ToCsv()
        {
            return string.Join(",",
                Escape(MaSV),
                Escape(HoTen),
                NgaySinh.ToString("dd/MM/yyyy"),
                DiemTB.ToString("0.00", CultureInfo.InvariantCulture),
                Escape(LoaiSV),
                SoTinChi.ToString(),
                TinhHocPhi().ToString("0"));
        }

        private static string Escape(string s)
        {
            return "\"" + (s ?? "").Replace("\"", "\"\"") + "\"";
        }
    }

    public class SVChinhQuy : SinhVienDaoTao
    {
        public SVChinhQuy(string ma, string ten, DateTime ngaySinh, double diem, int soTinChi)
            : base(ma, ten, ngaySinh, diem, soTinChi) { }

        public override string LoaiSV { get { return "Chính quy"; } }
        protected override decimal DonGiaTinChi { get { return 450000m; } }
    }

    public class SVLienThong : SinhVienDaoTao
    {
        public SVLienThong(string ma, string ten, DateTime ngaySinh, double diem, int soTinChi)
            : base(ma, ten, ngaySinh, diem, soTinChi) { }

        public override string LoaiSV { get { return "Liên thông"; } }
        protected override decimal DonGiaTinChi { get { return 520000m; } }
    }

    public class SVTaiChuc : SinhVienDaoTao
    {
        public SVTaiChuc(string ma, string ten, DateTime ngaySinh, double diem, int soTinChi)
            : base(ma, ten, ngaySinh, diem, soTinChi) { }

        public override string LoaiSV { get { return "Tại chức"; } }
        protected override decimal DonGiaTinChi { get { return 380000m; } }
    }

    // Bài 10: nhiều loại sinh viên, interface xuất dữ liệu, tìm kiếm, thống kê theo loại
    public class QuanLySinhVien
    {
        private readonly List<SinhVienDaoTao> _ds = new List<SinhVienDaoTao>();

        public QuanLySinhVien()
        {
            _ds.Add(new SVChinhQuy("24000091", "Nguyễn Tiến Hùng", new DateTime(2006, 10, 26), 9.2, 18));
            _ds.Add(new SVChinhQuy("24000465", "Lê Đức Bình", new DateTime(2006, 4, 25), 8.1, 16));
            _ds.Add(new SVLienThong("24000474", "Nguyễn Thiện Chiến", new DateTime(2000, 9, 27), 7.8, 15));
            _ds.Add(new SVLienThong("24000430", "Hoàng Ngọc Linh", new DateTime(2001, 8, 31), 6.2, 12));
            _ds.Add(new SVTaiChuc("24000059", "Lương Hoàng Anh", new DateTime(1999, 12, 28), 5.9, 10));
            _ds.Add(new SVTaiChuc("24000112", "Phạm Văn Dũng", new DateTime(1998, 3, 3), 4.5, 8));
        }

        public void Chay()
        {
            while (true)
            {
                Console.WriteLine("\n--- QUẢN LÝ SINH VIÊN (NÂNG CẤP) ---");
                Console.WriteLine("1. Hiển thị danh sách");
                Console.WriteLine("2. Thêm sinh viên (chọn loại)");
                Console.WriteLine("3. Tìm kiếm");
                Console.WriteLine("4. Thống kê theo loại");
                Console.WriteLine("5. Xuất dữ liệu (CSV)");
                Console.WriteLine("6. Kiểm tra tính hợp lệ của dữ liệu");
                Console.WriteLine("0. Quay lại");
                Console.Write("Chọn: ");
                switch (Console.ReadLine())
                {
                    case "1": HienThi(_ds); break;
                    case "2": Them(); break;
                    case "3": TimKiem(); break;
                    case "4": ThongKe(); break;
                    case "5": Xuat(); break;
                    case "6": KiemTra(); break;
                    case "0": return;
                    default: Console.WriteLine("Lựa chọn không hợp lệ."); break;
                }
            }
        }

        private static void HienThi(List<SinhVienDaoTao> ds)
        {
            if (ds.Count == 0) { Console.WriteLine("(Danh sách trống)"); return; }
            foreach (SinhVienDaoTao sv in ds)
                Console.WriteLine("{0} - {1} - Học phí: {2:N0}", sv.MoTa(), sv.XepLoai(), sv.TinhHocPhi());
        }

        private void Them()
        {
            Console.WriteLine("Loại: 1. Chính quy   2. Liên thông   3. Tại chức");
            Console.Write("Chọn loại: ");
            string loai = Console.ReadLine();
            if (loai != "1" && loai != "2" && loai != "3")
            {
                Console.WriteLine("Loại không hợp lệ.");
                return;
            }

            string ma = ChuoiKhongRong("Mã SV: ");
            if (_ds.Any(x => x.MaSV.Equals(ma, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Mã đã tồn tại!");
                return;
            }
            string ten = ChuoiKhongRong("Họ tên: ");
            DateTime ns = Ngay("Ngày sinh (dd/MM/yyyy): ");
            double diem = SoThuc("Điểm TB (0-10): ", 0, 10);
            int tc = SoNguyen("Số tín chỉ (0-30): ", 0, 30);

            SinhVienDaoTao sv;
            if (loai == "1") sv = new SVChinhQuy(ma, ten, ns, diem, tc);
            else if (loai == "2") sv = new SVLienThong(ma, ten, ns, diem, tc);
            else sv = new SVTaiChuc(ma, ten, ns, diem, tc);

            string thongBao;
            if (!sv.IsValid(out thongBao))
            {
                Console.WriteLine("Dữ liệu không hợp lệ: " + thongBao);
                return;
            }
            _ds.Add(sv);
            Console.WriteLine("Đã thêm.");
        }

        private void TimKiem()
        {
            Console.WriteLine("1. Theo mã   2. Theo tên (chứa từ khóa)   3. Theo loại");
            Console.Write("Chọn: ");
            string chon = Console.ReadLine();
            List<SinhVienDaoTao> kq;
            if (chon == "1")
            {
                string kw = ChuoiKhongRong("Mã SV: ");
                kq = _ds.Where(x => x.MaSV.Equals(kw, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            else if (chon == "2")
            {
                string kw = ChuoiKhongRong("Từ khóa: ");
                kq = _ds.Where(x => x.HoTen.IndexOf(kw, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }
            else if (chon == "3")
            {
                Console.Write("Loại (1. Chính quy, 2. Liên thông, 3. Tại chức): ");
                string l = Console.ReadLine();
                if (l == "1") kq = _ds.Where(x => x is SVChinhQuy).ToList();
                else if (l == "2") kq = _ds.Where(x => x is SVLienThong).ToList();
                else if (l == "3") kq = _ds.Where(x => x is SVTaiChuc).ToList();
                else { Console.WriteLine("Loại không hợp lệ."); return; }
            }
            else
            {
                Console.WriteLine("Lựa chọn không hợp lệ.");
                return;
            }
            Console.WriteLine("Tìm thấy {0} kết quả:", kq.Count);
            HienThi(kq);
        }

        private void ThongKe()
        {
            if (_ds.Count == 0) { Console.WriteLine("(Danh sách trống)"); return; }
            Console.WriteLine("{0,-12}{1,6}{2,10}{3,18}", "Loại", "SL", "ĐTB", "Tổng học phí");
            foreach (var nhom in _ds.GroupBy(x => x.LoaiSV))
            {
                Console.WriteLine("{0,-12}{1,6}{2,10:N2}{3,18:N0}",
                    nhom.Key, nhom.Count(), nhom.Average(x => x.DiemTB), nhom.Sum(x => x.TinhHocPhi()));
            }
            Console.WriteLine("{0,-12}{1,6}{2,10:N2}{3,18:N0}",
                "Tổng", _ds.Count, _ds.Average(x => x.DiemTB), _ds.Sum(x => x.TinhHocPhi()));
        }

        private void Xuat()
        {
            if (_ds.Count == 0) { Console.WriteLine("(Danh sách trống)"); return; }
            const string tieuDe = "MaSV,HoTen,NgaySinh,DiemTB,Loai,SoTinChi,HocPhi";
            List<IExportable> dsXuat = _ds.Cast<IExportable>().ToList();

            Console.WriteLine(tieuDe);
            foreach (IExportable e in dsXuat) Console.WriteLine(e.ToCsv());

            try
            {
                List<string> dong = new List<string> { tieuDe };
                dong.AddRange(dsXuat.Select(e => e.ToCsv()));
                File.WriteAllLines("SinhVien.csv", dong, new UTF8Encoding(true));
                Console.WriteLine("Đã lưu tệp: " + Path.GetFullPath("SinhVien.csv"));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Không ghi được tệp: " + ex.Message);
            }
        }

        private void KiemTra()
        {
            int loi = 0;
            foreach (SinhVienDaoTao sv in _ds)
            {
                IValidatable v = sv;
                string thongBao;
                if (!v.IsValid(out thongBao))
                {
                    Console.WriteLine("{0}: {1}", sv.ToDisplayText(), thongBao);
                    loi++;
                }
            }
            Console.WriteLine(loi == 0 ? "Tất cả {0} sinh viên đều hợp lệ." : "Có {0} sinh viên không hợp lệ.",
                loi == 0 ? _ds.Count : loi);
        }

        // ---------- Nhập liệu an toàn (TryParse), bắt nhập lại khi sai ----------
        private static string ChuoiKhongRong(string nhan)
        {
            while (true)
            {
                Console.Write(nhan);
                string s = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(s)) return s.Trim();
                Console.WriteLine("  -> Không được để trống, nhập lại.");
            }
        }

        private static double SoThuc(string nhan, double min, double max)
        {
            while (true)
            {
                Console.Write(nhan);
                string s = Console.ReadLine();
                double v;
                if (double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out v)
                    || double.TryParse(s, NumberStyles.Float, new CultureInfo("vi-VN"), out v))
                {
                    if (v >= min && v <= max) return v;
                }
                Console.WriteLine("  -> Giá trị không hợp lệ (cần trong [{0}, {1}]).", min, max);
            }
        }

        private static int SoNguyen(string nhan, int min, int max)
        {
            while (true)
            {
                Console.Write(nhan);
                int v;
                if (int.TryParse(Console.ReadLine(), out v) && v >= min && v <= max) return v;
                Console.WriteLine("  -> Giá trị không hợp lệ (cần trong [{0}, {1}]).", min, max);
            }
        }

        private static DateTime Ngay(string nhan)
        {
            while (true)
            {
                Console.Write(nhan);
                string s = Console.ReadLine();
                DateTime d;
                if (DateTime.TryParseExact(s, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out d) && d <= DateTime.Today)
                    return d;
                Console.WriteLine("  -> Ngày không hợp lệ (định dạng dd/MM/yyyy, không ở tương lai).");
            }
        }
    }
}
