#region	License
// <License>
//     <Copyright> 2019 © NEVAR Technology Solutions</Copyright>
//     <Url> https://github.com/trinhpapa </Url>
//     <Author> Le Hoang Trinh </Author>
//     <Project> NEVAR-AQC.Data.EF </Project>
//     <File>
//         <Name> DbInitializer.cs </Name>
//         <Created> 1/4/2019 - 21:35:44 </Created>
//         <Key></Key>
//     </File>
//     <Summary>
//         DbInitializer.cs
//     </Summary>
// <License>
#endregion License

using NEVAR_AQC.Core.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NEVAR_AQC.Data.EF
{
    public class DbInitializer
    {
        public NEVARDbContext _context;

        public DbInitializer(NEVARDbContext context)
        {
            _context = context;
        }

        public async Task SeedRole()
        {
            if (!_context.CTGRole.Any())
            {
                var roles = new[]
                {
                    new CTGRoleEntity {Name = "Administrator", Status = true, IsDeleted = false , CreatedTime = DateTime.Now},
                    new CTGRoleEntity {Name = "Nhà thầu phụ", Status = true, IsDeleted = false, CreatedTime = DateTime.Now },
                };
                await _context.CTGRole.AddRangeAsync(roles);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SeedDepartment()
        {
            if (!_context.CTGDepartment.Any())
            {
                var departments = new[]
                {
                    new CTGDepartmentEntity {Name = "Đơn vị quản trị", Status = true, IsDeleted = false, CreatedTime = DateTime.Now },
                    new CTGDepartmentEntity {Name = "Đơn vị thử nghiệm", Status = true, IsDeleted = false, CreatedTime = DateTime.Now},
                    new CTGDepartmentEntity {Name = "Đơn vị hiệu chuẩn", Status = true, IsDeleted = false, CreatedTime = DateTime.Now }
                };
                await _context.CTGDepartment.AddRangeAsync(departments);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SeedRequirementType()
        {
            if (!_context.CTGRequirementType.Any())
            {
                var data = new[]
                {
                    new CTGRequirementTypeEntity {Vietnamese = "Phiếu yêu cầu thử nghiệm", English = "Test Requirement", Alias = "YCTN", Symbol = "TN", DepartmentId = 2, IsDeleted = false, CreatedTime = DateTime.Now },
                    new CTGRequirementTypeEntity {Vietnamese = "Phiếu yêu cầu hiệu chuẩn", English = "Calibration Requirement", Alias = "YCHC", Symbol = "HC", DepartmentId = 3, IsDeleted = false, CreatedTime = DateTime.Now },
                };
                await _context.CTGRequirementType.AddRangeAsync(data);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SeedField()
        {
            if (!_context.CTGField.Any())
            {
                var data = new[]
                {
                    new CTGFieldEntity {Name = "Thử nghiệm hóa học, vi sinh", Symbol = "1", IsDeleted = false, CreatedTime = DateTime.Now },
                    new CTGFieldEntity {Name = "Thử nghiệm điện-điện tử", Symbol = "2", IsDeleted = false, CreatedTime = DateTime.Now },
                    new CTGFieldEntity {Name = "Thử nghiệm xăng dầu", Symbol = "3", IsDeleted = false, CreatedTime = DateTime.Now },
                };
                await _context.CTGField.AddRangeAsync(data);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SeedTestObject()
        {
            if (!_context.CTGTestObject.Any())
            {
                var data = new[]
                {
                    new CTGTestObjectEntity {Name = "Nước", IsDeleted = false, CreatedTime = DateTime.Now, FieldId = 1 },
                };
                await _context.CTGTestObject.AddRangeAsync(data);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SeedReturnInvoiceResultType()
        {
            if (!_context.CTGReturnInvoiceResultType.Any())
            {
                var data = new[]
                {
                    new CTGReturnInvoiceResultTypeEntity {Name = "Tại PTN", Status = true, CreatedTime = DateTime.Now },
                    new CTGReturnInvoiceResultTypeEntity {Name = "Bưu điện", Status = true, CreatedTime = DateTime.Now },
                    new CTGReturnInvoiceResultTypeEntity {Name = "Fax", Status = true, CreatedTime = DateTime.Now },
                };
                await _context.CTGReturnInvoiceResultType.AddRangeAsync(data);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SeedRequirementStatus()
        {
            if (!_context.CTGRequirementStatus.Any())
            {
                var data = new[]
                {
                    new CTGRequirementStatusEntity { ProcessStatus = "Đã gửi bộ phận", HtmlColour = "e74c3c", IsDeleted = false, CreatedTime = DateTime.Now },
                    new CTGRequirementStatusEntity { ProcessStatus = "Đã nhận xử lý", HtmlColour = "3498db", IsDeleted = false, CreatedTime = DateTime.Now },
                    new CTGRequirementStatusEntity { ProcessStatus = "Đã lên kế hoạch", HtmlColour = "f39c12", IsDeleted = false, CreatedTime = DateTime.Now },
                    new CTGRequirementStatusEntity { ProcessStatus = "Đang thực hiện", HtmlColour = "2ecc71", IsDeleted = false, CreatedTime = DateTime.Now },
                    new CTGRequirementStatusEntity { ProcessStatus = "Đã có kết quả", HtmlColour = "2ecc71", IsDeleted = false, CreatedTime = DateTime.Now },
                    new CTGRequirementStatusEntity { ProcessStatus = "Đã trả kết quả", HtmlColour = "34495e", IsDeleted = false, CreatedTime = DateTime.Now },
                };
                await _context.CTGRequirementStatus.AddRangeAsync(data);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SeedUser()
        {
            if (!_context.SYSUser.Any())
            {
                var users = new[]
                {
                    new SYSUserEntity {Username = "admin", PasswordEncrypted = "952603e9c96065ff6f5a600abfb842c4", PasswordSalt = "erfbuyhgft", DisplayName = "Lê Hoàng Trình", DateOfBirth = Convert.ToDateTime("01/01/1991"), DepartmentId = 1, Email = "hoangtrinh1610@gmail.com", PhoneNumber = "0986161095", RoleId = 1, ActiveStatus = true, IsDeleted = false, CreatedTime = DateTime.Now },
                    new SYSUserEntity {Username = "nhathauphu", PasswordEncrypted = "952603e9c96065ff6f5a600abfb842c4", PasswordSalt = "erfbuyhgft", DisplayName = "Nhà Thầu Phụ", DateOfBirth = Convert.ToDateTime("01/01/1991"), DepartmentId = 1, Email = "", PhoneNumber = "", RoleId = 2, ActiveStatus = true, IsDeleted = false, CreatedTime = DateTime.Now }
                };
                await _context.SYSUser.AddRangeAsync(users);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SeedFunction()
        {
            if (!_context.CTGSystemFunction.Any())
            {
                var functions = new[]
                {
                    new CTGSystemFunctionEntity {Key = 100, Name = "Bộ phận tiếp nhận", Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 101, Name = "Thêm yêu cầu", Parent = 100, Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 102, Name = "Sửa yêu cầu", Parent = 100, Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 103, Name = "Xóa yêu cầu", Parent = 100, Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 104, Name = "In phiếu", Parent = 100, Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 105, Name = "Tổng hợp phiếu", Parent = 100, Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 200, Name = "Bộ phận thử nghiệm", Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 201, Name = "Phòng quản lý", Parent = 200, Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 202, Name = "Nhận yêu cầu", Parent = 201, Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 203, Name = "Tạo kế hoạch", Parent = 201, Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 204, Name = "Xem kế hoạch", Parent = 201, Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 205, Name = "In kế hoạch", Parent = 201, Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 206, Name = "Tổng hợp kết quả", Parent = 201, Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 207, Name = "Phòng kỹ thuật", Parent = 200, Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 208, Name = "Nhận công việc", Parent = 207, Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 209, Name = "Nhập kết quả", Parent = 207, Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 210, Name = "Xem kết quả", Parent = 207, Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 211, Name = "Nhập báo cáo tiến trình", Parent = 207, Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 400, Name = "Quản trị hệ thống", Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 401, Name = "Quản lí tài khoản", Parent = 400, Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 402, Name = "Thêm tài khoản", Parent = 401, Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 403, Name = "Sửa tài khoản", Parent = 401, Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 404, Name = "Xóa tài khoản", Parent = 401, Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 405, Name = "Reset mật khẩu tài khoản", Parent = 401, Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 406, Name = "Quản lí quyền", Parent = 400, Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 407, Name = "Thêm quyền", Parent = 406, Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 408, Name = "Sửa quyền", Parent = 406, Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 409, Name = "Xóa quyền", Parent = 406, Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 410, Name = "Quản lí khách hàng", Parent = 400, Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 411, Name = "Thêm khách hàng", Parent = 410, Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 412, Name = "Sửa khách hàng", Parent = 410, Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 413, Name = "Xóa khách hàng", Parent = 410, Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 414, Name = "Quản lí lĩnh vực thử nghiệm", Parent = 400, Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 415, Name = "Thêm lĩnh vực thử nghiệm", Parent = 414, Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 416, Name = "Sửa lĩnh vực thử nghiệm", Parent = 414, Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 417, Name = "Xóa lĩnh vực thử nghiệm", Parent = 414, Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 418, Name = "Quản lí đối tượng thử nghiệm", Parent = 400, Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 419, Name = "Thêm đối tượng thử nghiệm", Parent = 418, Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 420, Name = "Sửa đối tượng thử nghiệm", Parent = 418, Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 421, Name = "Xóa đối tượng thử nghiệm", Parent = 418, Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 422, Name = "Quản lí chỉ tiêu thử nghiệm", Parent = 400, Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 423, Name = "Thêm chỉ tiêu thử nghiệm", Parent = 422, Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 424, Name = "Sửa chỉ tiêu thử nghiệm", Parent = 422, Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 425, Name = "Xóa chỉ tiêu thử nghiệm", Parent = 422, Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 426, Name = "Quản lí phương pháp thử nghiệm", Parent = 400, Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 427, Name = "Thêm chỉ phương pháp nghiệm", Parent = 426, Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 428, Name = "Sửa chỉ phương pháp nghiệm", Parent = 426, Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 429, Name = "Xóa chỉ phương pháp nghiệm", Parent = 426, Status = true, IsDeleted = false },
                    new CTGSystemFunctionEntity {Key = 500, Name = "Báo cáo thống kê", Status = true, IsDeleted = false },
                };
                await _context.CTGSystemFunction.AddRangeAsync(functions);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SeedRoleFunction()
        {
            if (!_context.SYSRoleFunction.Any())
            {
                var data = new[]
               {
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 1 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 2 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 3 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 4 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 5 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 6 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 7 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 8 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 9 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 10 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 11 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 12 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 13 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 14 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 15 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 16 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 17 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 18 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 19 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 20 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 21 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 22 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 23 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 24 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 25 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 26 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 27 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 28 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 29 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 30 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 31 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 32 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 33 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 34 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 35 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 36 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 37 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 38 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 39 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 40 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 41 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 42 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 43 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 44 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 45 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 46 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 47 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 48 },
                    new SYSRoleFunctionEntity { RoleId = 1, FunctionId = 49 },
                };
                await _context.SYSRoleFunction.AddRangeAsync(data);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SeedCustomerType()
        {
            if (!_context.CTGCustomerType.Any())
            {
                var data = new[]
                {
                    new CTGCustomerTypeEntity { Name = "Cá nhân", Status = true },
                    new CTGCustomerTypeEntity { Name = "Doanh nghiệp", Status = true },
                };
                await _context.CTGCustomerType.AddRangeAsync(data);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SeedCustomer()
        {
            if (!_context.SYSCustomer.Any())
            {
                var data = new[]
                {
                    new SYSCustomerEntity { Name = "Công ty vận tải Mai Linh", Address = "Tp. Vinh, Tỉnh Nghệ An", Email = "email@mailinh.com", PhoneNumber = "02381.111.11", Fax = "02381.111.111", CustomerTypeId = 2 }
                };
                await _context.SYSCustomer.AddRangeAsync(data);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SeedTestMethod()
        {
            if (!_context.CTGTestMethod.Any())
            {
                var data = new[]
                {
                    new CTGTestMethodEntity { Name = "TCVN 7877:2008", TestPropertyId = 1 },
                    new CTGTestMethodEntity { Name = "TCVN 6626:2000", TestPropertyId = 1 },
                    new CTGTestMethodEntity { Name = "TCVN 6193:1996", SymbolAttached = "(A)", TestPropertyId = 1 },
                    new CTGTestMethodEntity { Name = "TCVN 7325:2016", SymbolAttached = "(A)", TestPropertyId = 1 },
                    new CTGTestMethodEntity { Name = "TCVN 6492:2011", SymbolAttached = "(A)", TestPropertyId = 1 },
                    new CTGTestMethodEntity { Name = "TB..TN1/KT-05", TestPropertyId = 1 },
                    new CTGTestMethodEntity { Name = "SMEWW 2320 B", TestPropertyId = 1 },
                    new CTGTestMethodEntity { Name = "TCVN 6184:2008", TestPropertyId = 1 },
                    new CTGTestMethodEntity { Name = "TCVN6179-1:1996", TestPropertyId = 1 },
                };
                await _context.CTGTestMethod.AddRangeAsync(data);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SeedTestProperty()
        {
            if (!_context.CTGTestProperty.Any())
            {
                var data = new[]
                {
                    new CTGTestPropertyEntity { Name = "Hàm lượng Thủy ngân", ObjectId = 1 },
                    new CTGTestPropertyEntity { Name = "Hàm lượng Asen tổng số" },
                    new CTGTestPropertyEntity { Name = "Hàm lượng Chì tổng số" },
                    new CTGTestPropertyEntity { Name = "Hàm lượng Oxy hòa tan", Note = " Determination of dissolved oxygen", Unit = "mg/L", ObjectId = 1  },
                    new CTGTestPropertyEntity { Name = "pH", Note = "Determination of pH", ObjectId = 1  },
                    new CTGTestPropertyEntity { Name = "Độ mặn", Note = "Sanility", Unit = "‰", ObjectId = 1  },
                };
                await _context.CTGTestProperty.AddRangeAsync(data);
                await _context.SaveChangesAsync();
            }
        }
    }
}