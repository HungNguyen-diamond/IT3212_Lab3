using Lab03_OOPAdvanced;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab03_OOPAdvanced
{
    public abstract class NguoiHoc
    {
        public string HoTen { get; set; }
        public int SoTinChi { get; set; }

        public abstract string LoaiHinh { get; }
        public abstract decimal TinhHocPhi();
    }

    public class SinhVienChinhQuy : NguoiHoc
    {
        public override string LoaiHinh { get { return "Chính quy"; } }

        public override decimal TinhHocPhi()
        {
            return SoTinChi * 450000m;
        }
    }

    public class SinhVienLienThong : NguoiHoc
    {
        public override string LoaiHinh { get { return "Liên thông"; } }

        public override decimal TinhHocPhi()
        {
            return SoTinChi * 520000m;
        }
    }
}
