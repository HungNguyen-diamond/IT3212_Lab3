using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab03_OOPAdvanced
{
    public abstract class TaiLieu
    {
        public string MaTL { get; set; }
        public string TieuDe { get; set; }
        public int NamXB { get; set; }

        protected TaiLieu(string ma, string tieuDe, int namXB)
        {
            MaTL = ma;
            TieuDe = tieuDe;
            NamXB = namXB;
        }

        public abstract string LoaiTaiLieu { get; }

        public virtual string MoTa()
        {
            return string.Format("[{0}] {1} - {2} ({3})", LoaiTaiLieu, MaTL, TieuDe, NamXB);
        }
    }

    public class Sach : TaiLieu
    {
        public string TacGia { get; set; }
        public string NhaXB { get; set; }
        public int SoTrang { get; set; }

        public Sach(string ma, string tieuDe, int namXB, string tacGia, string nhaXB, int soTrang)
            : base(ma, tieuDe, namXB)
        {
            TacGia = tacGia;
            NhaXB = nhaXB;
            SoTrang = soTrang;
        }

        public override string LoaiTaiLieu { get { return "Sách"; } }

        public override string MoTa()
        {
            return base.MoTa() + string.Format(" - Tác giả: {0} - NXB: {1} - {2} trang", TacGia, NhaXB, SoTrang);
        }
    }

    public class TapChi : TaiLieu
    {
        public int SoPhatHanh { get; set; }
        public int ThangPhatHanh { get; set; }

        public TapChi(string ma, string tieuDe, int namXB, int soPhatHanh, int thangPhatHanh)
            : base(ma, tieuDe, namXB)
        {
            SoPhatHanh = soPhatHanh;
            ThangPhatHanh = thangPhatHanh;
        }

        public override string LoaiTaiLieu { get { return "Tạp chí"; } }

        public override string MoTa()
        {
            return base.MoTa() + string.Format(" - Số {0}, tháng {1}", SoPhatHanh, ThangPhatHanh);
        }
    }

    public class LuanVan : TaiLieu
    {
        public string TacGia { get; set; }
        public string GiangVienHuongDan { get; set; }
        public string BacHoc { get; set; }

        public LuanVan(string ma, string tieuDe, int namXB, string tacGia, string gvhd, string bacHoc)
            : base(ma, tieuDe, namXB)
        {
            TacGia = tacGia;
            GiangVienHuongDan = gvhd;
            BacHoc = bacHoc;
        }

        public override string LoaiTaiLieu { get { return "Luận văn"; } }

        public override string MoTa()
        {
            return base.MoTa() + string.Format(" - Tác giả: {0} - GVHD: {1} - Bậc: {2}", TacGia, GiangVienHuongDan, BacHoc);
        }
    }
}
