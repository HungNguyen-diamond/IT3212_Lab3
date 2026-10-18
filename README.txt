==============================================================
LAB 03 - KẾ THỪA, ĐA HÌNH, LỚP TRỪU TƯỢNG VÀ INTERFACE
Học phần: Lập trình .NET (IT3212)
Trường Đại học Công nghệ Đông Á - Khoa Công nghệ Thông tin
==============================================================

Họ và tên   : Nguyễn Tiến Hùng
Mã sinh viên: 24000091
Lớp         : Lập trình .NET-1-1-26(N03)

1. MÔI TRƯỜNG
-------------
- Visual Studio 2026.
- Dự án Console (.NET 8), tên project: Lab03_OOPAdvanced.

2. CÁCH CHẠY
------------
1. Mở Lab03_OOPAdvanced.csproj (hoặc file .sln) bằng Visual Studio 2026.
2. Nhấn Ctrl+F5 để chạy.
3. Nhập số từ 1 đến 10 để chọn bài; nhập 0 để thoát.
Ngày sinh khi nhập (Bài 10) theo định dạng dd/MM/yyyy.

3. CẤU TRÚC MÃ NGUỒN
--------------------
  Program.cs         Menu chọn bài; Bài 1-9 là các hàm Bai1()..Bai9()
  Nguoi.cs           Bài 1, 2   : Nguoi
  SinhVien.cs        Bài 1, 2, 4: SinhVien : Nguoi, IValidatable, IPrintable
  Interfaces.cs      IValidatable, IPrintable, IMove, IPayment, IExportable
  HocPhi.cs          Bài 3, 5   : NguoiHoc (abstract), SinhVienChinhQuy,
                                  SinhVienLienThong
  NhanVien.cs        Bài 6      : NhanVien (abstract), NhanVienVanPhong,
                                  NhanVienKinhDoanh, GiangVien
  PhuongTien.cs      Bài 7      : PhuongTien (abstract), XeMay, OTo, XeDap
  TaiLieu.cs         Bài 8      : TaiLieu (abstract), Sach, TapChi, LuanVan
  ThanhToan.cs       Bài 9      : ThanhToanTienMat, ChuyenKhoan, ViDienTu,
                                  GiaoDich, XuLyThanhToan
  QuanLySinhVien.cs  Bài 10     : SinhVienDaoTao (abstract), SVChinhQuy,
                                  SVLienThong, SVTaiChuc, QuanLySinhVien

Các lớp được gom theo bài: mỗi tệp chứa các lớp cùng thuộc một bài.

4. NỘI DUNG TỪNG BÀI
--------------------
Bài 1 : Nguoi (HoTen, NgaySinh) là lớp cơ sở. SinhVien kế thừa Nguoi và
        gọi constructor lớp cha bằng ": base(ten, ngaySinh)".
Bài 2 : Nguoi.MoTa() là virtual; SinhVien ghi đè bằng override và gọi
        base.MoTa(). Biến kiểu Nguoi giữ đối tượng SinhVien vẫn chạy
        bản MoTa() của SinhVien (đa hình).
Bài 3 : NguoiHoc là lớp trừu tượng có phương thức abstract TinhHocPhi().
        - SinhVienChinhQuy : số tín chỉ x 450.000
        - SinhVienLienThong: số tín chỉ x 520.000
Bài 4 : IValidatable (IsValid) và IPrintable (ToDisplayText); SinhVien
        thực thi cả hai. Có 3 sinh viên mẫu: hợp lệ, thiếu mã, điểm sai.
Bài 5 : List<NguoiHoc> chứa nhiều kiểu sinh viên; gọi TinhHocPhi() qua
        tham chiếu lớp cha và cộng tổng học phí.
Bài 6 : NhanVien trừu tượng với 3 lớp con, mỗi lớp một công thức lương:
        - Văn phòng : lương cơ bản / 26 x số ngày công
        - Kinh doanh: lương cơ bản + doanh số x tỷ lệ hoa hồng
        - Giảng viên: lương cơ bản + số giờ dạy x đơn giá giờ
Bài 7 : IMove (tốc độ, tăng tốc, dừng, chi phí vận hành). PhuongTien là
        lớp trừu tượng dùng chung; XeMay, OTo, XeDap khác nhau ở tốc độ
        tối đa và chi phí mỗi km.
Bài 8 : TaiLieu trừu tượng; Sach, TapChi, LuanVan ghi đè MoTa(). Danh
        sách List<TaiLieu> chặn trùng mã, tìm theo từ khóa tiêu đề bằng
        LINQ và lọc theo kiểu tài liệu bằng OfType<T>().
Bài 9 : IPayment (KiemTra, ThucHien). XuLyThanhToan nhận mọi phương thức
        thanh toán: kiểm tra, thực hiện, rồi ghi nhận GiaoDich (thành
        công hoặc thất bại kèm lý do).
Bài 10: Sinh viên có 3 loại (Chính quy, Liên thông, Tại chức) kế thừa
        SinhVienDaoTao. Menu gồm: hiển thị, thêm theo loại, tìm kiếm
        (mã, tên, loại), thống kê theo loại, xuất CSV qua IExportable
        (in ra màn hình và lưu tệp SinhVien.csv), kiểm tra hợp lệ qua
        IValidatable.

5. DỮ LIỆU THỬ VÀ KẾT QUẢ DỰ KIẾN
---------------------------------
Bài 3 : An (Chính quy) 18 TC    = 8.100.000
        Bình (Liên thông) 15 TC = 7.800.000
Bài 4 : 24000091 - Nguyễn Tiến Hùng    -> Hợp lệ
        (mã rỗng) - Lê Đức Bình        -> Không hợp lệ: Thiếu mã
        24000474 - Nguyễn Thiện Chiến  -> Không hợp lệ: Điểm sai (điểm 11)
Bài 5 : An 18 TC (CQ) 8.100.000; Bình 15 TC (LT) 7.800.000;
        Chi 20 TC (CQ) 9.000.000; Dũng 12 TC (LT) 6.240.000
        Tổng học phí = 31.140.000
Bài 6 : NV01 Nguyễn An (văn phòng, 24 ngày công)        = 7.384.615
        NV02 Lê Chi (kinh doanh, doanh số 120 triệu, 5%) = 13.000.000
        NV03 Phạm Dũng (giảng viên, 40 giờ x 200.000)    = 17.000.000
        Tổng quỹ lương = 37.384.615; lương cao nhất: Phạm Dũng
Bài 7 : Quãng đường 30 km
        Xe máy (tối đa 80 km/h) : 22,5 phút, chi phí 36.000 đ
        Ô tô   (tối đa 120 km/h): 15,0 phút, chi phí 105.000 đ
        Xe đạp (tối đa 25 km/h) : 72,0 phút, chi phí 0 đ
Bài 8 : 5 tài liệu (2 sách, 2 tạp chí, 1 luận văn); thêm sách trùng mã
        S01 báo lỗi. Từ khóa "lập trình" cho 2 kết quả (S01, L01).
Bài 9 : Tiền mặt 500.000                      -> thành công
        Tiền mặt 50.000.000                   -> thất bại (vượt hạn mức)
        Chuyển khoản 1.500.000 (số dư 2 triệu) -> thành công, còn 500.000
        Chuyển khoản 1.000.000                -> thất bại (số dư không đủ)
        Ví MoMo 300.000 (số dư 1 triệu)       -> thành công, còn 700.000
        Ví MoMo 6.000.000                     -> thất bại (vượt hạn mức)
        Tổng 6 giao dịch, 3 thành công, tổng tiền thành công 2.300.000
Bài 10: 6 sinh viên mẫu (2 Chính quy, 2 Liên thông, 2 Tại chức).
        Thống kê theo loại:
          Chính quy : SL 2, ĐTB 8,65, tổng học phí 15.300.000
          Liên thông: SL 2, ĐTB 7,00, tổng học phí 14.040.000
          Tại chức  : SL 2, ĐTB 5,20, tổng học phí  6.840.000
          Tổng      : SL 6, tổng học phí 36.180.000

6. PHẦN ĐÃ HOÀN THÀNH
---------------------
[x] Bài 1  - Kế thừa Nguoi - SinhVien
[x] Bài 2  - Ghi đè phương thức hiển thị
[x] Bài 3  - Lớp trừu tượng và học phí
[x] Bài 4  - Interface kiểm tra và in dữ liệu
[x] Bài 5  - Danh sách đa hình
[x] Bài 6  - Hệ thống nhân viên
[x] Bài 7  - Quản lý phương tiện
[x] Bài 8  - Quản lý tài liệu thư viện
[x] Bài 9  - Hệ thống thanh toán
[x] Bài 10 - Nâng cấp quản lý sinh viên

7. GHI CHÚ
----------
- Các interface IValidatable, IPrintable sẽ được tái sử dụng ở tầng BUS
  trong các lab sau.
- Ảnh chụp kết quả từng bài nằm kèm trong thư mục nộp bài.
