using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab03_OOPAdvanced
{
    public class SinhVien : Nguoi, IValidatable, IPrintable
    {
        public string MaSV { get; set; }
        public double DiemTB { get; set; }

        public SinhVien(string ma, string ten, DateTime ngaySinh, double diem)
            : base(ten, ngaySinh)
        {
            MaSV = ma;
            DiemTB = diem;
        }

        public override string MoTa()
        {
            return base.MoTa() + " - " + MaSV + " - " + DiemTB.ToString("N2");
        }

        public string XepLoai()
        {
            if (DiemTB >= 9) return "Xuất sắc";
            if (DiemTB >= 8) return "Giỏi";
            if (DiemTB >= 6.5) return "Khá";
            if (DiemTB >= 5) return "Trung bình";
            return "Yếu";
        }

        public virtual bool IsValid(out string message)
        {
            if (string.IsNullOrWhiteSpace(MaSV)) { message = "Thiếu mã"; return false; }
            if (string.IsNullOrWhiteSpace(HoTen)) { message = "Thiếu họ tên"; return false; }
            if (NgaySinh.Date > DateTime.Today) { message = "Ngày sinh ở tương lai"; return false; }
            if (DiemTB < 0 || DiemTB > 10) { message = "Điểm sai"; return false; }
            message = "";
            return true;
        }

        public string ToDisplayText()
        {
            return MaSV + " - " + HoTen;
        }
    }
}
