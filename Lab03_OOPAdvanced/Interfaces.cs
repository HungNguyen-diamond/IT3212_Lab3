using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab03_OOPAdvanced
{
    public interface IValidatable
    {
        bool IsValid(out string message);
    }

    public interface IPrintable
    {
        string ToDisplayText();
    }

    public interface IMove
    {
        string TenPhuongTien { get; }
        double TocDoToiDa { get; }
        double TocDoHienTai { get; }
        void TangToc(double delta);
        void Dung();
        decimal ChiPhiVanHanh(double km);
    }

    public interface IPayment
    {
        string TenPhuongThuc { get; }
        bool KiemTra(decimal soTien, out string thongBao);
        void ThucHien(decimal soTien);
    }

    public interface IExportable
    {
        string ToCsv();
    }
}
