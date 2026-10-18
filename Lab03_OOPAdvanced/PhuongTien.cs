using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab03_OOPAdvanced
{
    public abstract class PhuongTien : IMove
    {
        public string TenPhuongTien { get; private set; }
        public double TocDoToiDa { get; private set; }
        public double TocDoHienTai { get; private set; }

        protected PhuongTien(string ten, double tocDoToiDa)
        {
            TenPhuongTien = ten;
            TocDoToiDa = tocDoToiDa;
            TocDoHienTai = 0;
        }

        public void TangToc(double delta)
        {
            TocDoHienTai = Math.Max(0, Math.Min(TocDoToiDa, TocDoHienTai + delta));
        }

        public void Dung()
        {
            TocDoHienTai = 0;
        }

        public abstract decimal ChiPhiVanHanh(double km);
    }

    public class XeMay : PhuongTien
    {
        public XeMay() : base("Xe máy", 80) { }

        public override decimal ChiPhiVanHanh(double km)
        {
            return (decimal)km * 1200m;
        }
    }

    public class OTo : PhuongTien
    {
        public OTo() : base("Ô tô", 120) { }

        public override decimal ChiPhiVanHanh(double km)
        {
            return (decimal)km * 3500m;
        }
    }

    public class XeDap : PhuongTien
    {
        public XeDap() : base("Xe đạp", 25) { }

        public override decimal ChiPhiVanHanh(double km)
        {
            return 0m;
        }
    }
}
