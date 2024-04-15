Họ tên: Lưu Đình Huy
MSSV: 21127058
Tên TK: NV001
Mật khẩu: 123
DB: localhost không port, không username password, dùng authentication
Video demo: https://drive.google.com/file/d/1dNn6ehR7rP-V_w76SMJOuZtnpp3Zh-uT/view?usp=sharing
QuanLyBanHang la folder chua source code
QuanLyCuaHang la folder chua folder release
Các chức năng đã thực hiên:
1. Màn hình đăng nhập
Cho nhập username và password để đi vào màn hình chính. Có chức năng lưu username và password ở local để lần sau người dùng không cần đăng nhập lại. Password cần được mã hóa.
2. Màn hình dashboard
Cung cấp tổng quan về hệ thống đang quản lí, ví dụ:

- Có tổng cộng bao nhiêu sản phẩm đang bán
- Có tổng cộng bao nhiêu đơn hàng mới trong tuần / tháng
- Liệt kê top 5 sản phẩm đang sắp hết hàng (số lượng < 5)
3. Quản lí hàng hóa 

- Import dữ liệu gốc ban đầu (loại sản phẩm, danh sách các sản phẩm) từ tập tin Excel hoặc Access. Chú ý không sử dụng Excel hoặc Access làm CSDL chính.
- Thao tác với **Loại sản phẩm**: Xem danh sách, Thêm, Xóa, Cập nhật
- Thao tác với **Sản phẩm**
    - Xem danh sách theo Loại sản phẩm
        - Có phân trang
        - Sắp xếp theo tiêu chí
    - Xem chi tiết một sản phẩm
        - Xóa, cập nhật sản phẩm
    - Thêm mới một sản phẩm
- Cho phép tìm kiếm sản phẩm theo tên
- Cho phép lọc lại sản phẩm theo khoảng giá
4. Quản lí các đơn hàng 

- [ ]  Tạo ra các đơn hàng
- [ ]  Cho phép xóa một đơn hàng, cập nhật một đơn hàng
- [ ]  Cho phép xem danh sách các đơn hàng có phân trang, xem chi tiết một đơn hàng
- [ ]  Tìm kiếm các đơn hàng từ ngày đến ngày
5. Báo cáo thống kê

- Báo cáo doanh thu và lợi nhuận theo ngày đến ngày, theo tuần, theo tháng, theo năm (vẽ biểu đồ)
- Xem các sản phẩm và số lượng bán theo ngày đến ngày, theo tuần, theo tháng, theo năm (vẽ biểu đồ)

 6. Cấu hình 

- Cho phép hiệu chỉnh số lượng sản phẩm mỗi trang
- Cho phép khi chạy chương trình lên thì mở lại màn hình cuối mà lần trước tắt
7. Đóng gói thành file cài đặt 
- Cần đóng gói thành file exe để tự cài chương trình vào hệ thống
8. Sử dụng một thiết kế giao diện tốt lấy từ pinterest
9. Báo cáo các sản phẩm bán chạy trong tuần, trong tháng, trong năm 
10. Quản lí khách hàng 
11. Tổ chức theo mô hình 3 lớp 
12. Sử dụng mô hình MVVM
13. Sử dụng Dependency injection cho mainviewmodel
Các chức năng chưa thực hiên:
- [ ]  Làm rối mã nguồn (obfuscator) chống dịch ngược (0.25 điểm)
- [ ]  Thêm chế độ dùng thử - cho phép xài full phần mềm trong 15 ngày. Hết 15 ngày bắt đăng kí (mã code hay cách kích hoạt nào đó) (0.5 điểm)
- [ ]  Bổ sung khuyến mãi giảm giá (1 điểm)
- [ ]  Sử dụng giao diện Ribbon (0.25 điểm)
- [ ]  Backup / restore database (0.5 điểm)
- [ ]  Chương trình có khả năng mở rộng động theo kiến trúc plugin (1 điểm)
- [ ]  Sử dụng Dependency injection (1 điểm)
- [ ]  Sử dụng DevExpress / Telerik  / Kendo UI  (1 điểm)
- [ ]  Có khả năng cập nhật tính năng mới qua mạng sử dụng ClickOnce(0.5 điểm)
- [ ]  Sử dụng thư viện WinUI mới (1 điểm)
- [ ]  Kết nối API Rest API (1 điểm)
- [ ]  Kết nối GraphQL API (1 điểm)
- [ ]  Tự động thay đổi sắp xếp hợp lí các thành phần theo độ rộng màn hình (responsive layout) (0.5 điểm)
Chức năng xem xét cộng điểm: Tổ chức theo mô hình 3 lớp, MVVM, thống kê các sản phẩm bán chạy
Điểm tự đánh giá: 9.5
