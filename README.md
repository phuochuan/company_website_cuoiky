# company_website_cuoiky
## SỬ DỤNG DOTNET CORE MVC 

### Phần cơ bản: 
Tạo một website cơ bản thực hiện công việc quản lý nội dung của công ty, bao gồm các đối tượng liên quan như: bài đăng thông báo, lịch trình làm việc của công ty, thể loại, nhân viên phòng ban, công ty. Website gồm các màn hình có các chức năng như thêm, sửa, xóa, danh sách, tìm kiếm (theo thuộc tính của đối tượng trong màn hình tương ứng). 

**Yêu cầu**:
**Khởi tạo được dự án cơ bản**
Tạo các đối tượng theo đề bài để thực hiện công việc quản lý trong web
Tạo được chức năng thêm, sửa, xóa cho các màn hình
Kết nối được với cơ sở dữ liệu để thao tác dữ liệu (thêm, sửa, xóa, truy xuất)
Tạo được chức năng tìm kiếm, lọc theo điều kiện cho các màn hình sử dụng LINQ
Kiểm tra ràng buộc dữ liệu cho chương trình. Dữ liệu của các trường input phải tuân thủ theo ràng buộc sau: 
– Tiêu đề bài đăng, tiêu đề lịch trình có chiều dài tối đa là 150 ký tự và tổi thiểu là 20 ký tự.
– Chương trình phải bắt được lỗi sai định dạng ngày tháng năm đối với trường ngày, phải theo định dạng dd/MM/YYYY.
– Số điện thoại: phải là chuỗi số có chiều dài 10 ký tự và phải bắt đầu bằng một trong các chuỗi số: 090, 098, 091, 031, 035 hoặc 038. 
Chú ý: các trường tạo cho các đối tượng càng chi tiết, được sử dụng cho các việc lọc, tìm kiếm, hiển thị đầy đủ thì mới đủ điểm, chứ không thể 3 thuộc tính cũng giống 7 thuộc tính.

### Phần nâng cao:
Có thêm mục upload quản lý ảnh của bài đăng, ảnh minh hoạ, thumbnail của lịch trình, ảnh trong bài...
Có thêm đối tượng người dùng để quản lý chương trình
Có màn hình login cho người dùng để vào màn hình admin mới thao tác được các màn hình trên (tham khảo hướng dẫn https://medium.com/@bruno-bernardes-tech/how-to-implement-jwt-authentication-in-asp-net-core-269f258f19be)
Áp dụng các kĩ năng để tạo giao diện web thân thiện với người dùng
Yêu cầu nộp báo cáo:
Chụp hình lịch sử sử dụng Git, lịch sử upload code của dự án này lên GIT (Upload GIT thường xuyên, ko upload quá ít lần --> hỏi code ko trả lời đc --> 0 đ)
Quay video màn hình chạy của dự án tất cả các chức năng
Tạo file Readme để ghi lại các bước cài đặt hướng dẫn
Export file sql để nộp kèm