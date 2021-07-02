using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NEVAR_AQC.Data.EF.Migrations
{
    public partial class MigrationV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CTGCustomerType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    DeletedBy = table.Column<long>(nullable: true),
                    DeletedTime = table.Column<DateTime>(nullable: true),
                    Note = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    Status = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTGCustomerType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CTGDepartment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    DeletedBy = table.Column<long>(nullable: true),
                    DeletedTime = table.Column<DateTime>(nullable: true),
                    Note = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTGDepartment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CTGField",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    DeletedBy = table.Column<long>(nullable: true),
                    DeletedTime = table.Column<DateTime>(nullable: true),
                    Note = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    Symbol = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTGField", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CTGReturnInvoiceResultType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    DeletedBy = table.Column<long>(nullable: true),
                    DeletedTime = table.Column<DateTime>(nullable: true),
                    Note = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Status = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTGReturnInvoiceResultType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CTGRole",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    DeletedBy = table.Column<long>(nullable: true),
                    DeletedTime = table.Column<DateTime>(nullable: true),
                    Note = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Status = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTGRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CTGSystemFunction",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    DeletedBy = table.Column<long>(nullable: true),
                    DeletedTime = table.Column<DateTime>(nullable: true),
                    Note = table.Column<string>(maxLength: 100, nullable: true),
                    Key = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    Parent = table.Column<int>(nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTGSystemFunction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CTGRequirementType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    DeletedBy = table.Column<long>(nullable: true),
                    DeletedTime = table.Column<DateTime>(nullable: true),
                    Note = table.Column<string>(maxLength: 100, nullable: true),
                    Vietnamese = table.Column<string>(maxLength: 200, nullable: true),
                    English = table.Column<string>(maxLength: 200, nullable: true),
                    Alias = table.Column<string>(maxLength: 10, nullable: true),
                    Symbol = table.Column<string>(maxLength: 10, nullable: true),
                    DepartmentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTGRequirementType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CTGRequirementType_CTGDepartment_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "CTGDepartment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CTGTestObject",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    DeletedBy = table.Column<long>(nullable: true),
                    DeletedTime = table.Column<DateTime>(nullable: true),
                    Note = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    FieldId = table.Column<long>(nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTGTestObject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CTGTestObject_CTGField_FieldId",
                        column: x => x.FieldId,
                        principalTable: "CTGField",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SYSUser",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    DeletedBy = table.Column<long>(nullable: true),
                    DeletedTime = table.Column<DateTime>(nullable: true),
                    Note = table.Column<string>(maxLength: 100, nullable: true),
                    Username = table.Column<string>(maxLength: 50, nullable: true),
                    PasswordEncrypted = table.Column<string>(maxLength: 100, nullable: true),
                    PasswordSalt = table.Column<string>(maxLength: 10, nullable: true),
                    DisplayName = table.Column<string>(maxLength: 100, nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: true),
                    DepartmentId = table.Column<int>(nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 20, nullable: true),
                    RoleId = table.Column<int>(nullable: false),
                    SignalRId = table.Column<string>(maxLength: 100, nullable: true),
                    ActiveStatus = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYSUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SYSUser_CTGDepartment_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "CTGDepartment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SYSUser_CTGRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "CTGRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CTGTestProperty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    DeletedBy = table.Column<long>(nullable: true),
                    DeletedTime = table.Column<DateTime>(nullable: true),
                    Note = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    ObjectId = table.Column<long>(nullable: false),
                    Unit = table.Column<string>(maxLength: 100, nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTGTestProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CTGTestProperty_CTGTestObject_ObjectId",
                        column: x => x.ObjectId,
                        principalTable: "CTGTestObject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CTGRequirementStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    DeletedBy = table.Column<long>(nullable: true),
                    DeletedTime = table.Column<DateTime>(nullable: true),
                    Note = table.Column<string>(maxLength: 100, nullable: true),
                    ProcessStatus = table.Column<string>(maxLength: 100, nullable: true),
                    HtmlColour = table.Column<string>(maxLength: 6, nullable: true),
                    Status = table.Column<bool>(nullable: true),
                    SYSUserEntityId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTGRequirementStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CTGRequirementStatus_SYSUser_SYSUserEntityId",
                        column: x => x.SYSUserEntityId,
                        principalTable: "SYSUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SYSCustomer",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    DeletedBy = table.Column<long>(nullable: true),
                    DeletedTime = table.Column<DateTime>(nullable: true),
                    Note = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: true),
                    CustomerTypeId = table.Column<int>(nullable: true),
                    ProvinceId = table.Column<int>(nullable: true),
                    DistrictId = table.Column<int>(nullable: true),
                    WardsId = table.Column<int>(nullable: true),
                    Address = table.Column<string>(maxLength: 1000, nullable: true),
                    Email = table.Column<string>(maxLength: 150, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 20, nullable: true),
                    Fax = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYSCustomer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SYSCustomer_SYSUser_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "SYSUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SYSCustomer_CTGCustomerType_CustomerTypeId",
                        column: x => x.CustomerTypeId,
                        principalTable: "CTGCustomerType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SYSCustomer_SYSUser_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "SYSUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SYSCustomer_SYSUser_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "SYSUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SYSRoleFunction",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    DeletedBy = table.Column<long>(nullable: true),
                    DeletedTime = table.Column<DateTime>(nullable: true),
                    Note = table.Column<string>(maxLength: 100, nullable: true),
                    RoleId = table.Column<int>(nullable: false),
                    FunctionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYSRoleFunction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SYSRoleFunction_SYSUser_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "SYSUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SYSRoleFunction_SYSUser_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "SYSUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SYSRoleFunction_CTGSystemFunction_FunctionId",
                        column: x => x.FunctionId,
                        principalTable: "CTGSystemFunction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SYSRoleFunction_SYSUser_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "SYSUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SYSRoleFunction_CTGRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "CTGRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CTGTestMethod",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    DeletedBy = table.Column<long>(nullable: true),
                    DeletedTime = table.Column<DateTime>(nullable: true),
                    Note = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    TestPropertyId = table.Column<long>(nullable: false),
                    SymbolAttached = table.Column<string>(maxLength: 10, nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTGTestMethod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CTGTestMethod_CTGTestProperty_TestPropertyId",
                        column: x => x.TestPropertyId,
                        principalTable: "CTGTestProperty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SYSRequirementInvoice",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    DeletedBy = table.Column<long>(nullable: true),
                    DeletedTime = table.Column<DateTime>(nullable: true),
                    Note = table.Column<string>(maxLength: 100, nullable: true),
                    RequirementTypeId = table.Column<int>(nullable: false),
                    Serial = table.Column<int>(nullable: true),
                    SerialYear = table.Column<int>(nullable: true),
                    InvoiceNo = table.Column<string>(maxLength: 50, nullable: true),
                    Edition = table.Column<int>(nullable: false),
                    FieldId = table.Column<long>(nullable: true),
                    CustomerId = table.Column<long>(nullable: false),
                    Representative = table.Column<string>(maxLength: 100, nullable: true),
                    RepresentativePhone = table.Column<string>(maxLength: 20, nullable: true),
                    OtherInformation = table.Column<string>(maxLength: 200, nullable: true),
                    OtherRequirement = table.Column<string>(maxLength: 200, nullable: true),
                    IsSaveSpecimen = table.Column<bool>(nullable: false),
                    SaveSpecimenTime = table.Column<string>(nullable: true),
                    IsUseSubcontractors = table.Column<bool>(nullable: false),
                    ResultDay = table.Column<DateTime>(nullable: true),
                    ReturnInvoiceResultTypeId = table.Column<int>(nullable: false),
                    ResultInvoiceAmount = table.Column<int>(nullable: false),
                    ProcessStatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYSRequirementInvoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SYSRequirementInvoice_SYSUser_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "SYSUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SYSRequirementInvoice_SYSCustomer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "SYSCustomer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SYSRequirementInvoice_SYSUser_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "SYSUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SYSRequirementInvoice_CTGField_FieldId",
                        column: x => x.FieldId,
                        principalTable: "CTGField",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SYSRequirementInvoice_SYSUser_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "SYSUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SYSRequirementInvoice_CTGRequirementStatus_ProcessStatusId",
                        column: x => x.ProcessStatusId,
                        principalTable: "CTGRequirementStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SYSRequirementInvoice_CTGRequirementType_RequirementTypeId",
                        column: x => x.RequirementTypeId,
                        principalTable: "CTGRequirementType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SYSRequirementInvoice_CTGReturnInvoiceResultType_ReturnInvoiceResultTypeId",
                        column: x => x.ReturnInvoiceResultTypeId,
                        principalTable: "CTGReturnInvoiceResultType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IDCalibrationRequirement",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    DeletedBy = table.Column<long>(nullable: true),
                    DeletedTime = table.Column<DateTime>(nullable: true),
                    Note = table.Column<string>(maxLength: 100, nullable: true),
                    RequirementInvoiceId = table.Column<long>(nullable: false),
                    NameOfMeasuringDevice = table.Column<string>(maxLength: 200, nullable: true),
                    SerialNumber = table.Column<string>(maxLength: 20, nullable: true),
                    TechnicalCharacteristics = table.Column<string>(maxLength: 200, nullable: true),
                    Amount = table.Column<int>(nullable: true),
                    AmountUnit = table.Column<string>(maxLength: 50, nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDCalibrationRequirement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IDCalibrationRequirement_SYSUser_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "SYSUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IDCalibrationRequirement_SYSUser_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "SYSUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IDCalibrationRequirement_SYSUser_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "SYSUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IDCalibrationRequirement_SYSRequirementInvoice_RequirementInvoiceId",
                        column: x => x.RequirementInvoiceId,
                        principalTable: "SYSRequirementInvoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IDTestRequirement",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    DeletedBy = table.Column<long>(nullable: true),
                    DeletedTime = table.Column<DateTime>(nullable: true),
                    Note = table.Column<string>(maxLength: 100, nullable: true),
                    RequirementInvoiceId = table.Column<long>(nullable: false),
                    ObjectId = table.Column<long>(nullable: true),
                    SpecimenName = table.Column<string>(maxLength: 200, nullable: true),
                    SpecimenSymbol = table.Column<string>(maxLength: 200, nullable: true),
                    SpecimenOrder = table.Column<int>(nullable: false),
                    SpecimenCode = table.Column<string>(maxLength: 100, nullable: true),
                    ImageLink = table.Column<string>(maxLength: 1000, nullable: true),
                    SpecimenAmount = table.Column<int>(nullable: true),
                    SpecimenStatus = table.Column<string>(nullable: true),
                    SpecimenQuantum = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDTestRequirement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IDTestRequirement_SYSUser_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "SYSUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IDTestRequirement_SYSUser_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "SYSUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IDTestRequirement_SYSUser_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "SYSUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IDTestRequirement_CTGTestObject_ObjectId",
                        column: x => x.ObjectId,
                        principalTable: "CTGTestObject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IDTestRequirement_SYSRequirementInvoice_RequirementInvoiceId",
                        column: x => x.RequirementInvoiceId,
                        principalTable: "SYSRequirementInvoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IDTRTestProperty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    DeletedBy = table.Column<long>(nullable: true),
                    DeletedTime = table.Column<DateTime>(nullable: true),
                    Note = table.Column<string>(maxLength: 100, nullable: true),
                    SpecimenId = table.Column<long>(nullable: false),
                    TestPropertyId = table.Column<long>(nullable: true),
                    TestMethodId = table.Column<long>(nullable: true),
                    PlanFromTime = table.Column<DateTime>(nullable: true),
                    PlanToTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDTRTestProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IDTRTestProperty_SYSUser_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "SYSUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IDTRTestProperty_SYSUser_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "SYSUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IDTRTestProperty_SYSUser_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "SYSUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IDTRTestProperty_IDTestRequirement_SpecimenId",
                        column: x => x.SpecimenId,
                        principalTable: "IDTestRequirement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IDTRTestProperty_CTGTestMethod_TestMethodId",
                        column: x => x.TestMethodId,
                        principalTable: "CTGTestMethod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IDTRTestProperty_CTGTestProperty_TestPropertyId",
                        column: x => x.TestPropertyId,
                        principalTable: "CTGTestProperty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IDTRImplementer",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    DeletedBy = table.Column<long>(nullable: true),
                    DeletedTime = table.Column<DateTime>(nullable: true),
                    Note = table.Column<string>(maxLength: 100, nullable: true),
                    SpecimenPropertyId = table.Column<long>(nullable: false),
                    UserId = table.Column<long>(nullable: true),
                    IsAccept = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDTRImplementer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IDTRImplementer_SYSUser_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "SYSUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IDTRImplementer_SYSUser_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "SYSUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IDTRImplementer_SYSUser_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "SYSUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IDTRImplementer_IDTRTestProperty_SpecimenPropertyId",
                        column: x => x.SpecimenPropertyId,
                        principalTable: "IDTRTestProperty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IDTRImplementer_SYSUser_UserId",
                        column: x => x.UserId,
                        principalTable: "SYSUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IDTRTestProcessAASUCVISAESMethod",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    DeletedBy = table.Column<long>(nullable: true),
                    DeletedTime = table.Column<DateTime>(nullable: true),
                    Note = table.Column<string>(maxLength: 100, nullable: true),
                    SpecimenPropertyId = table.Column<long>(nullable: false),
                    QuantumL1 = table.Column<string>(nullable: true),
                    QuantumL2 = table.Column<string>(nullable: true),
                    SymbolC = table.Column<string>(nullable: true),
                    ValueC1 = table.Column<string>(nullable: true),
                    ValueC2 = table.Column<string>(nullable: true),
                    ValueC3 = table.Column<string>(nullable: true),
                    ValueC4 = table.Column<string>(nullable: true),
                    ValueC5 = table.Column<string>(nullable: true),
                    ValueC6 = table.Column<string>(nullable: true),
                    ValueC7 = table.Column<string>(nullable: true),
                    AbsorbanceC1 = table.Column<string>(nullable: true),
                    AbsorbanceC2 = table.Column<string>(nullable: true),
                    AbsorbanceC3 = table.Column<string>(nullable: true),
                    AbsorbanceC4 = table.Column<string>(nullable: true),
                    AbsorbanceC5 = table.Column<string>(nullable: true),
                    AbsorbanceC6 = table.Column<string>(nullable: true),
                    AbsorbanceC7 = table.Column<string>(nullable: true),
                    StandardLineEquation = table.Column<string>(nullable: true),
                    CoefficientR2 = table.Column<string>(nullable: true),
                    ExtraStandardConcentration = table.Column<string>(nullable: true),
                    T1 = table.Column<string>(nullable: true),
                    T2 = table.Column<string>(nullable: true),
                    MeasurementResultsL1 = table.Column<string>(nullable: true),
                    MeasurementResultsL2 = table.Column<string>(nullable: true),
                    MeasurementResultsT1 = table.Column<string>(nullable: true),
                    MeasurementResultsT2 = table.Column<string>(nullable: true),
                    DilutionCoefficientL1 = table.Column<string>(nullable: true),
                    DilutionCoefficientL2 = table.Column<string>(nullable: true),
                    DilutionCoefficientT1 = table.Column<string>(nullable: true),
                    DilutionCoefficientT2 = table.Column<string>(nullable: true),
                    CalculationFormula = table.Column<string>(nullable: true),
                    ResultL1 = table.Column<string>(nullable: true),
                    ResultL2 = table.Column<string>(nullable: true),
                    ResultT1 = table.Column<string>(nullable: true),
                    ResultT2 = table.Column<string>(nullable: true),
                    AverageResultsL = table.Column<string>(nullable: true),
                    AverageResultsT = table.Column<string>(nullable: true),
                    PercentOfRevoke = table.Column<string>(nullable: true),
                    ReportResults = table.Column<string>(nullable: true),
                    TimeReportResults = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDTRTestProcessAASUCVISAESMethod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IDTRTestProcessAASUCVISAESMethod_IDTRTestProperty_SpecimenPropertyId",
                        column: x => x.SpecimenPropertyId,
                        principalTable: "IDTRTestProperty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IDTRTestProcessOtherMethod",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    DeletedBy = table.Column<long>(nullable: true),
                    DeletedTime = table.Column<DateTime>(nullable: true),
                    Note = table.Column<string>(maxLength: 100, nullable: true),
                    SpecimenPropertyId = table.Column<long>(nullable: false),
                    MonitoringData = table.Column<string>(nullable: true),
                    ReportResults = table.Column<string>(nullable: true),
                    TimeReportResults = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDTRTestProcessOtherMethod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IDTRTestProcessOtherMethod_IDTRTestProperty_SpecimenPropertyId",
                        column: x => x.SpecimenPropertyId,
                        principalTable: "IDTRTestProperty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IDTRTestProcessVolumeMethod",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    DeletedBy = table.Column<long>(nullable: true),
                    DeletedTime = table.Column<DateTime>(nullable: true),
                    Note = table.Column<string>(maxLength: 100, nullable: true),
                    SpecimenPropertyId = table.Column<long>(nullable: false),
                    QuantumL1 = table.Column<string>(nullable: true),
                    QuantumL2 = table.Column<string>(nullable: true),
                    SolutionName1 = table.Column<string>(nullable: true),
                    ConcentrationOfSolution1 = table.Column<string>(nullable: true),
                    SolutionName2 = table.Column<string>(nullable: true),
                    ConcentrationOfSolution2 = table.Column<string>(nullable: true),
                    OtherMonitoringData = table.Column<string>(nullable: true),
                    DilutionCoefficient = table.Column<string>(nullable: true),
                    T1 = table.Column<string>(nullable: true),
                    T2 = table.Column<string>(nullable: true),
                    CalculationFormula = table.Column<string>(nullable: true),
                    ResultL1 = table.Column<string>(nullable: true),
                    ResultL2 = table.Column<string>(nullable: true),
                    ResultT1 = table.Column<string>(nullable: true),
                    ResultT2 = table.Column<string>(nullable: true),
                    AverageResultsL = table.Column<string>(nullable: true),
                    AverageResultsT = table.Column<string>(nullable: true),
                    PercentOfRevoke = table.Column<string>(nullable: true),
                    ReportResults = table.Column<string>(nullable: true),
                    TimeReportResults = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDTRTestProcessVolumeMethod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IDTRTestProcessVolumeMethod_IDTRTestProperty_SpecimenPropertyId",
                        column: x => x.SpecimenPropertyId,
                        principalTable: "IDTRTestProperty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IDTRTestProcessWeightMethod",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    DeletedBy = table.Column<long>(nullable: true),
                    DeletedTime = table.Column<DateTime>(nullable: true),
                    Note = table.Column<string>(maxLength: 100, nullable: true),
                    SpecimenPropertyId = table.Column<long>(nullable: false),
                    QuantumL1 = table.Column<string>(nullable: true),
                    QuantumL2 = table.Column<string>(nullable: true),
                    WeightOfScaleSymbolL1 = table.Column<string>(nullable: true),
                    WeightOfCupL1 = table.Column<string>(nullable: true),
                    WeightOfCupAndSpecimenL1 = table.Column<string>(nullable: true),
                    WeightOfScaleSymbolL2 = table.Column<string>(nullable: true),
                    WeightOfCupL2 = table.Column<string>(nullable: true),
                    WeightOfCupAndSpecimenL2 = table.Column<string>(nullable: true),
                    SymbolT1 = table.Column<string>(nullable: true),
                    WeightOfCupT1 = table.Column<string>(nullable: true),
                    WeightOfCupAndSpecimenT1 = table.Column<string>(nullable: true),
                    SymbolT2 = table.Column<string>(nullable: true),
                    WeightOfCupT2 = table.Column<string>(nullable: true),
                    WeightOfCupAndSpecimenT2 = table.Column<string>(nullable: true),
                    DilutionCoefficientL1 = table.Column<string>(nullable: true),
                    DilutionCoefficientSymbolL1 = table.Column<string>(nullable: true),
                    DilutionCoefficientL2 = table.Column<string>(nullable: true),
                    DilutionCoefficientSymbolL2 = table.Column<string>(nullable: true),
                    DilutionCoefficientT1 = table.Column<string>(nullable: true),
                    DilutionCoefficientSymbolT1 = table.Column<string>(nullable: true),
                    DilutionCoefficientT2 = table.Column<string>(nullable: true),
                    DilutionCoefficientSymbolT2 = table.Column<string>(nullable: true),
                    CalculationFormula = table.Column<string>(nullable: true),
                    ResultSymbolL1 = table.Column<string>(nullable: true),
                    ResultL1 = table.Column<string>(nullable: true),
                    ResultSymbolL2 = table.Column<string>(nullable: true),
                    ResultL2 = table.Column<string>(nullable: true),
                    ResultSymbolT1 = table.Column<string>(nullable: true),
                    ResultT1 = table.Column<string>(nullable: true),
                    ResultSymbolT2 = table.Column<string>(nullable: true),
                    ResultT2 = table.Column<string>(nullable: true),
                    AverageResultsL = table.Column<string>(nullable: true),
                    AverageResultsT = table.Column<string>(nullable: true),
                    PercentOfRevoke = table.Column<string>(nullable: true),
                    ReportResults = table.Column<string>(nullable: true),
                    TimeReportResults = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDTRTestProcessWeightMethod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IDTRTestProcessWeightMethod_IDTRTestProperty_SpecimenPropertyId",
                        column: x => x.SpecimenPropertyId,
                        principalTable: "IDTRTestProperty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CTGRequirementStatus_SYSUserEntityId",
                table: "CTGRequirementStatus",
                column: "SYSUserEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CTGRequirementType_DepartmentId",
                table: "CTGRequirementType",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CTGTestMethod_TestPropertyId",
                table: "CTGTestMethod",
                column: "TestPropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_CTGTestObject_FieldId",
                table: "CTGTestObject",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_CTGTestProperty_ObjectId",
                table: "CTGTestProperty",
                column: "ObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_IDCalibrationRequirement_CreatedBy",
                table: "IDCalibrationRequirement",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_IDCalibrationRequirement_DeletedBy",
                table: "IDCalibrationRequirement",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_IDCalibrationRequirement_ModifiedBy",
                table: "IDCalibrationRequirement",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_IDCalibrationRequirement_RequirementInvoiceId",
                table: "IDCalibrationRequirement",
                column: "RequirementInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_IDTestRequirement_CreatedBy",
                table: "IDTestRequirement",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_IDTestRequirement_DeletedBy",
                table: "IDTestRequirement",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_IDTestRequirement_ModifiedBy",
                table: "IDTestRequirement",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_IDTestRequirement_ObjectId",
                table: "IDTestRequirement",
                column: "ObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_IDTestRequirement_RequirementInvoiceId",
                table: "IDTestRequirement",
                column: "RequirementInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_IDTRImplementer_CreatedBy",
                table: "IDTRImplementer",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_IDTRImplementer_DeletedBy",
                table: "IDTRImplementer",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_IDTRImplementer_ModifiedBy",
                table: "IDTRImplementer",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_IDTRImplementer_SpecimenPropertyId",
                table: "IDTRImplementer",
                column: "SpecimenPropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_IDTRImplementer_UserId",
                table: "IDTRImplementer",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IDTRTestProcessAASUCVISAESMethod_SpecimenPropertyId",
                table: "IDTRTestProcessAASUCVISAESMethod",
                column: "SpecimenPropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_IDTRTestProcessOtherMethod_SpecimenPropertyId",
                table: "IDTRTestProcessOtherMethod",
                column: "SpecimenPropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_IDTRTestProcessVolumeMethod_SpecimenPropertyId",
                table: "IDTRTestProcessVolumeMethod",
                column: "SpecimenPropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_IDTRTestProcessWeightMethod_SpecimenPropertyId",
                table: "IDTRTestProcessWeightMethod",
                column: "SpecimenPropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_IDTRTestProperty_CreatedBy",
                table: "IDTRTestProperty",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_IDTRTestProperty_DeletedBy",
                table: "IDTRTestProperty",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_IDTRTestProperty_ModifiedBy",
                table: "IDTRTestProperty",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_IDTRTestProperty_SpecimenId",
                table: "IDTRTestProperty",
                column: "SpecimenId");

            migrationBuilder.CreateIndex(
                name: "IX_IDTRTestProperty_TestMethodId",
                table: "IDTRTestProperty",
                column: "TestMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_IDTRTestProperty_TestPropertyId",
                table: "IDTRTestProperty",
                column: "TestPropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_SYSCustomer_CreatedBy",
                table: "SYSCustomer",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SYSCustomer_CustomerTypeId",
                table: "SYSCustomer",
                column: "CustomerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SYSCustomer_DeletedBy",
                table: "SYSCustomer",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SYSCustomer_ModifiedBy",
                table: "SYSCustomer",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SYSRequirementInvoice_CreatedBy",
                table: "SYSRequirementInvoice",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SYSRequirementInvoice_CustomerId",
                table: "SYSRequirementInvoice",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SYSRequirementInvoice_DeletedBy",
                table: "SYSRequirementInvoice",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SYSRequirementInvoice_FieldId",
                table: "SYSRequirementInvoice",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_SYSRequirementInvoice_ModifiedBy",
                table: "SYSRequirementInvoice",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SYSRequirementInvoice_ProcessStatusId",
                table: "SYSRequirementInvoice",
                column: "ProcessStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_SYSRequirementInvoice_RequirementTypeId",
                table: "SYSRequirementInvoice",
                column: "RequirementTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SYSRequirementInvoice_ReturnInvoiceResultTypeId",
                table: "SYSRequirementInvoice",
                column: "ReturnInvoiceResultTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SYSRoleFunction_CreatedBy",
                table: "SYSRoleFunction",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SYSRoleFunction_DeletedBy",
                table: "SYSRoleFunction",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SYSRoleFunction_FunctionId",
                table: "SYSRoleFunction",
                column: "FunctionId");

            migrationBuilder.CreateIndex(
                name: "IX_SYSRoleFunction_ModifiedBy",
                table: "SYSRoleFunction",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SYSRoleFunction_RoleId",
                table: "SYSRoleFunction",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SYSUser_DepartmentId",
                table: "SYSUser",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_SYSUser_RoleId",
                table: "SYSUser",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IDCalibrationRequirement");

            migrationBuilder.DropTable(
                name: "IDTRImplementer");

            migrationBuilder.DropTable(
                name: "IDTRTestProcessAASUCVISAESMethod");

            migrationBuilder.DropTable(
                name: "IDTRTestProcessOtherMethod");

            migrationBuilder.DropTable(
                name: "IDTRTestProcessVolumeMethod");

            migrationBuilder.DropTable(
                name: "IDTRTestProcessWeightMethod");

            migrationBuilder.DropTable(
                name: "SYSRoleFunction");

            migrationBuilder.DropTable(
                name: "IDTRTestProperty");

            migrationBuilder.DropTable(
                name: "CTGSystemFunction");

            migrationBuilder.DropTable(
                name: "IDTestRequirement");

            migrationBuilder.DropTable(
                name: "CTGTestMethod");

            migrationBuilder.DropTable(
                name: "SYSRequirementInvoice");

            migrationBuilder.DropTable(
                name: "CTGTestProperty");

            migrationBuilder.DropTable(
                name: "SYSCustomer");

            migrationBuilder.DropTable(
                name: "CTGRequirementStatus");

            migrationBuilder.DropTable(
                name: "CTGRequirementType");

            migrationBuilder.DropTable(
                name: "CTGReturnInvoiceResultType");

            migrationBuilder.DropTable(
                name: "CTGTestObject");

            migrationBuilder.DropTable(
                name: "CTGCustomerType");

            migrationBuilder.DropTable(
                name: "SYSUser");

            migrationBuilder.DropTable(
                name: "CTGField");

            migrationBuilder.DropTable(
                name: "CTGDepartment");

            migrationBuilder.DropTable(
                name: "CTGRole");
        }
    }
}
