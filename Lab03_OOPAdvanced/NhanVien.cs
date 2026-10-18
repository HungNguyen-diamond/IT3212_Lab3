using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab03_OOPAdvanced
{
    public abstract class NhanVien
    {
        public string MaNV { get; set; }
        public string HoTen { get; set; }
        public decimal LuongCoBan { get; set; }

        protected NhanVien(string ma, string ten, decimal luongCoBan)
        {
            MaNV = ma;
            HoTen = ten;
            LuongCoBan = luongCoBan;
        }

        public abstract string ChucVu { get; }
        public abstract decimal TinhLuong();

        public virtual string MoTa()
        {
            return ChucVu + " - " + MaNV + " - " + HoTen;
        }
    }

    public class NhanVienVanPhong : NhanVien
    {
        public int SoNgayCong { get; set; }

        public NhanVienVanPhong(string ma, string ten, decimal luongCoBan, int soNgayCong)
            : base(ma, ten, luongCoBan)
        {
            SoNgayCong = soNgayCong;
        }

        public override string ChucVu { get { return "Nhân viên văn phòng"; } }

        public override decimal TinhLuong()
        {
            return Math.Round(LuongCoBan / 26m * SoNgayCong, 0);
        }
    }

    public class NhanVienKinhDoanh : NhanVien
    {
        public decimal DoanhSo { get; set; }
        public decimal TyLeHoaHong { get; set; }

        public NhanVienKinhDoanh(string ma, string ten, decimal luongCoBan, decimal doanhSo, decimal tyLeHoaHong)
            : base(ma, ten, luongCoBan)
        {
            DoanhSo = doanhSo;
            TyLeHoaHong = tyLeHoaHong;
        }

        public override string ChucVu { get { return "Nhân viên kinh doanh"; } }

        public override decimal TinhLuong()
        {
            return LuongCoBan + DoanhSo * TyLeHoaHong;
        }
    }

    public class GiangVien : NhanVien
    {
        public int SoGioDay { get; set; }
        public decimal DonGiaGio { get; set; }

        public GiangVien(string ma, string ten, decimal luongCoBan, int soGioDay, decimal donGiaGio)
            : base(ma, ten, luongCoBan)
        {
            SoGioDay = soGioDay;
            DonGiaGio = donGiaGio;
        }

        public override string ChucVu { get { return "Giảng viên"; } }

        public override decimal TinhLuong()
        {
            return LuongCoBan + SoGioDay * DonGiaGio;
        }
    }
}
