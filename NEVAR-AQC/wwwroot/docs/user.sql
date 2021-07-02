USE [NEVAR-AQC]
GO
SET IDENTITY_INSERT [dbo].[SYSUser] ON 

INSERT [dbo].[SYSUser] ([Id], [CreatedBy], [CreatedTime], [ModifiedBy], [ModifiedTime], [IsDeleted], [DeletedBy], [DeletedTime], [Note], [Username], [PasswordEncrypted], [PasswordSalt], [DisplayName], [DateOfBirth], [DepartmentId], [Email], [PhoneNumber], [RoleId], [SignalRId], [ActiveStatus]) VALUES (1, NULL, CAST(N'2019-05-28T23:02:50.8720869' AS DateTime2), NULL, NULL, 0, NULL, NULL, NULL, N'admin', N'952603e9c96065ff6f5a600abfb842c4', N'erfbuyhgft', N'Lê Hoàng Trình', CAST(N'1991-01-01T00:00:00.0000000' AS DateTime2), 1, N'hoangtrinh1610@gmail.com', N'0986161095', 1, NULL, 1)
INSERT [dbo].[SYSUser] ([Id], [CreatedBy], [CreatedTime], [ModifiedBy], [ModifiedTime], [IsDeleted], [DeletedBy], [DeletedTime], [Note], [Username], [PasswordEncrypted], [PasswordSalt], [DisplayName], [DateOfBirth], [DepartmentId], [Email], [PhoneNumber], [RoleId], [SignalRId], [ActiveStatus]) VALUES (2, NULL, CAST(N'2019-05-29T16:02:22.2568629' AS DateTime2), NULL, NULL, 0, NULL, NULL, NULL, N'test', N'02fe0a6bc8085bc7a2c366da63e575b2', N'30T14wzc2Y', N'Lê Văn C', NULL, 1, NULL, NULL, 1, NULL, 1)
INSERT [dbo].[SYSUser] ([Id], [CreatedBy], [CreatedTime], [ModifiedBy], [ModifiedTime], [IsDeleted], [DeletedBy], [DeletedTime], [Note], [Username], [PasswordEncrypted], [PasswordSalt], [DisplayName], [DateOfBirth], [DepartmentId], [Email], [PhoneNumber], [RoleId], [SignalRId], [ActiveStatus]) VALUES (3, NULL, CAST(N'2019-05-31T10:15:02.8980725' AS DateTime2), NULL, NULL, 0, NULL, NULL, NULL, N'huyennt', N'bdc5da8a8a7b4c5a2e77ac7311edf12e', N'hWXKQY98Qs', N'Nguyễn Thị Huyền', NULL, 1, NULL, NULL, 1, NULL, 1)
INSERT [dbo].[SYSUser] ([Id], [CreatedBy], [CreatedTime], [ModifiedBy], [ModifiedTime], [IsDeleted], [DeletedBy], [DeletedTime], [Note], [Username], [PasswordEncrypted], [PasswordSalt], [DisplayName], [DateOfBirth], [DepartmentId], [Email], [PhoneNumber], [RoleId], [SignalRId], [ActiveStatus]) VALUES (4, NULL, CAST(N'2019-05-31T10:17:33.0813874' AS DateTime2), NULL, NULL, 0, NULL, NULL, NULL, N'maipt', N'de1110665ebb0cbb21bc42641934f251', N'RYzWhGIMMk', N'Phạm Thị Mai', NULL, 1, NULL, NULL, 1, NULL, 1)
INSERT [dbo].[SYSUser] ([Id], [CreatedBy], [CreatedTime], [ModifiedBy], [ModifiedTime], [IsDeleted], [DeletedBy], [DeletedTime], [Note], [Username], [PasswordEncrypted], [PasswordSalt], [DisplayName], [DateOfBirth], [DepartmentId], [Email], [PhoneNumber], [RoleId], [SignalRId], [ActiveStatus]) VALUES (5, NULL, CAST(N'2019-05-31T10:34:33.4099966' AS DateTime2), NULL, NULL, 0, NULL, NULL, NULL, N'nganntt', N'7f770cbe9d37bde849656e9a88f41e46', N'GTYpVOBBbv', N'Nguyễn Thị Thanh Nga', NULL, 1, NULL, NULL, 1, NULL, 1)
SET IDENTITY_INSERT [dbo].[SYSUser] OFF
