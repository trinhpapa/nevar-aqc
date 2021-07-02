### Hệ thống kiểm định chất lượng
#Scrafold-DbContext:
Scaffold-DbContext "Server=.\SQLEXPRESS; Database=NEVAR-AQC; User ID=sa;Password=trinhpapa;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities -context NEVARDbContext -StartupProject NEVAR-AQC

#Update 14/03/2019
- Đổi tên bảng sang tiếng Việt.
- Cập nhật thêm bảng, danh mục.
- Cấu trúc lại project

