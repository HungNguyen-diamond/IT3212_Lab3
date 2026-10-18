using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab03_OOPAdvanced
{
    public class ThanhToanTienMat : IPayment
    {
        public decimal HanMuc { get; set; }

        public ThanhToanTienMat() { HanMuc = 20000000m; }

        public string TenPhuongThuc { get { return "Tiền mặt"; } }

        public bool KiemTra(decimal soTien, out string thongBao)
        {
            if (soTien <= 0) { thongBao = "Số tiền phải lớn hơn 0"; return false; }
            if (soTien > HanMuc) { thongBao = "Vượt hạn mức tiền mặt " + HanMuc.ToString("N0"); return false; }
            thongBao = "";
            return true;
        }

        public void ThucHien(decimal soTien)
        {
            
        }
    }

    public class ChuyenKhoan : IPayment
    {
        public string SoTaiKhoan { get; set; }
        public decimal SoDu { get; private set; }

        public ChuyenKhoan(string soTaiKhoan, decimal soDu)
        {
            SoTaiKhoan = soTaiKhoan;
            SoDu = soDu;
        }

        public string TenPhuongThuc { get { return "Chuyển khoản"; } }

        public bool KiemTra(decimal soTien, out string thongBao)
        {
            if (string.IsNullOrWhiteSpace(SoTaiKhoan)) { thongBao = "Thiếu số tài khoản"; return false; }
            if (soTien <= 0) { thongBao = "Số tiền phải lớn hơn 0"; return false; }
            if (soTien > SoDu) { thongBao = "Số dư không đủ (còn " + SoDu.ToString("N0") + ")"; return false; }
            thongBao = "";
            return true;
        }

        public void ThucHien(decimal soTien)
        {
            SoDu -= soTien;
        }
    }

    public class ViDienTu : IPayment
    {
        public string TenVi { get; set; }
        public decimal SoDu { get; private set; }
        public decimal HanMucGiaoDich { get; set; }

        public ViDienTu(string tenVi, decimal soDu)
        {
            TenVi = tenVi;
            SoDu = soDu;
            HanMucGiaoDich = 5000000m;
        }

        public string TenPhuongThuc { get { return "Ví điện tử (" + TenVi + ")"; } }

        public bool KiemTra(decimal soTien, out string thongBao)
        {
            if (soTien <= 0) { thongBao = "Số tiền phải lớn hơn 0"; return false; }
            if (soTien > HanMucGiaoDich) { thongBao = "Vượt hạn mức mỗi giao dịch " + HanMucGiaoDich.ToString("N0"); return false; }
            if (soTien > SoDu) { thongBao = "Số dư ví không đủ (còn " + SoDu.ToString("N0") + ")"; return false; }
            thongBao = "";
            return true;
        }

        public void ThucHien(decimal soTien)
        {
            SoDu -= soTien;
        }
    }

    public class GiaoDich
    {
        public string Ma { get; private set; }
        public DateTime ThoiGian { get; private set; }
        public string PhuongThuc { get; private set; }
        public decimal SoTien { get; private set; }
        public bool ThanhCong { get; private set; }
        public string GhiChu { get; private set; }

        public GiaoDich(string ma, DateTime thoiGian, string phuongThuc, decimal soTien, bool thanhCong, string ghiChu)
        {
            Ma = ma;
            ThoiGian = thoiGian;
            PhuongThuc = phuongThuc;
            SoTien = soTien;
            ThanhCong = thanhCong;
            GhiChu = ghiChu;
        }

        public override string ToString()
        {
            return string.Format("{0} | {1:HH:mm:ss} | {2,-22} | {3,12:N0} | {4}{5}",
                Ma, ThoiGian, PhuongThuc, SoTien,
                ThanhCong ? "THÀNH CÔNG" : "THẤT BẠI",
                ThanhCong ? "" : " - " + GhiChu);
        }
    }

    public class XuLyThanhToan
    {
        private readonly List<GiaoDich> _lichSu = new List<GiaoDich>();

        public IReadOnlyList<GiaoDich> LichSu { get { return _lichSu; } }

        public GiaoDich ThucHien(IPayment phuongThuc, decimal soTien)
        {
            string thongBao;
            bool hopLe = phuongThuc.KiemTra(soTien, out thongBao);
            if (hopLe)
            {
                phuongThuc.ThucHien(soTien);
                thongBao = "Thành công";
            }
            GiaoDich gd = new GiaoDich(string.Format("GD{0:000}", _lichSu.Count + 1),
                DateTime.Now, phuongThuc.TenPhuongThuc, soTien, hopLe, thongBao);
            _lichSu.Add(gd);
            return gd;
        }
    }
}
