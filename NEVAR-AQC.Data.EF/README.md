USE [DBNAME]
GO
/****** Object:  Trigger [dbo].[GENERATE_SERIAL_REQUIREMENTINVOICE]    Script Date: 15/06/2019 14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[GENERATE_SERIAL_REQUIREMENTINVOICE]
ON [dbo].[SYSRequirementInvoice]
FOR INSERT
AS
BEGIN
	IF((SELECT Inserted.Edition FROM Inserted) = 0)
	BEGIN 
		DECLARE @Id BIGINT, @RequirementTypeId INT, @CurrentSerial INT, @RequirementTypeAlias NVARCHAR(10), @CountRecordWithSerial INT, @UpdateSuccess BIT;
		SET @Id = (SELECT Inserted.Id FROM Inserted);
		SET @RequirementTypeId = (SELECT Inserted.RequirementTypeId FROM Inserted);
		SET @RequirementTypeAlias = (SELECT Alias FROM dbo.CTGRequirementType WHERE Id = @RequirementTypeId);
	
		SET @UpdateSuccess = 0;
		WHILE @UpdateSuccess = 0 BEGIN
			BEGIN TRANSACTION;
				SET @CurrentSerial = ISNULL((SELECT TOP 1 Serial FROM dbo.SYSRequirementInvoice WHERE RequirementTypeId = @RequirementTypeId AND SerialYear = YEAR(CURRENT_TIMESTAMP) AND IsDeleted = 0 AND Edition = 0 ORDER BY Serial DESC), 0);
				UPDATE dbo.SYSRequirementInvoice SET Serial = @CurrentSerial + 1, SerialYear = YEAR(CURRENT_TIMESTAMP), InvoiceNo = CONCAT(@CurrentSerial + 1, '/', YEAR(CURRENT_TIMESTAMP), '/', @RequirementTypeAlias) WHERE Id = @Id;
				SET @CountRecordWithSerial = (SELECT COUNT(Serial) FROM dbo.SYSRequirementInvoice WHERE Serial = @CurrentSerial + 1 AND IsDeleted = 0 AND Edition = 0);
				IF (@CountRecordWithSerial > 1)
					ROLLBACK TRANSACTION;
				ELSE 
					SET @UpdateSuccess = 1;
					COMMIT TRANSACTION;
			END;
	END;
END;
/* Tránh Serial bị trùng
1. Update InvoiceNo trong transaction, nếu COUNT(Serial == (@SerialTemp + 1)) thì ROLLBACK
2. Hàng đợi TRIGGER (nếu có)
*/

/***** Trigger for create Specimen Code *****/
USE [DBNAME]
GO
/****** Object:  Trigger [dbo].[UPDATE_SPECIMEN_CODE]    Script Date: 01/07/2019 13:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER TRIGGER [dbo].[UPDATE_SPECIMEN_CODE]
ON [dbo].[IDTestRequirement]
FOR INSERT
AS
BEGIN
	DECLARE @Id BIGINT, @SpecimenCode NVARCHAR(100), @RequirementInvoiceId BIGINT, @SpecimenOrder INT, @SerialInvoice BIGINT, @FieldId BIGINT, @FieldSymbol NVARCHAR(10), @InvoiceNo BIGINT
	DECLARE Specimen_Cursor CURSOR FOR SELECT INSERTED.Id, INSERTED.RequirementInvoiceId, INSERTED.SpecimenOrder, INSERTED.SpecimenCode  FROM INSERTED; 
	OPEN Specimen_Cursor; 

	FETCH NEXT FROM Specimen_Cursor INTO @Id, @RequirementInvoiceId, @SpecimenOrder, @SpecimenCode; 
	WHILE @@FETCH_STATUS = 0 
	BEGIN
		IF (@SpecimenCode IS NULL)
			BEGIN
			SET @SerialInvoice = (SELECT Serial FROM dbo.SYSRequirementInvoice WHERE Id = @RequirementInvoiceId);
			SET @FieldId = (SELECT FieldId FROM dbo.SYSRequirementInvoice WHERE Id = @RequirementInvoiceId);
			SET @FieldSymbol = (SELECT Symbol FROM dbo.CTGField WHERE Id = @FieldId);

			UPDATE dbo.IDTestRequirement SET SpecimenCode = CONCAT('TN', @FieldSymbol, '/', CASE WHEN @SerialInvoice < 10 THEN '0' ELSE '' END, @SerialInvoice , '/', CASE WHEN @SpecimenOrder < 10 THEN '0' ELSE '' END, @SpecimenOrder) WHERE Id = @Id;
			FETCH NEXT FROM Specimen_Cursor INTO @Id, @RequirementInvoiceId, @SpecimenOrder, @SpecimenCode; 
			END
		ELSE
			BEGIN
			FETCH NEXT FROM Specimen_Cursor INTO @Id, @RequirementInvoiceId, @SpecimenOrder, @SpecimenCode; 
			END;
	END; 
	CLOSE Specimen_Cursor;
	DEALLOCATE Specimen_Cursor;	
END;


USE [NEVAR-AQC-V2]
GO
/****** Object:  Trigger [dbo].[UPDATE_SPECIMEN_CODE]    Script Date: 04/10/2019 15:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER TRIGGER [dbo].[UPDATE_SPECIMEN_CODE]
ON [dbo].[IDTestRequirement]
FOR INSERT
AS
BEGIN
	DECLARE @Id BIGINT, @RequirementInvoiceId BIGINT, @SpecimenOrder INT, @SerialInvoice BIGINT, @FieldId BIGINT, @FieldSymbol NVARCHAR(10), @InvoiceNo BIGINT
	DECLARE Specimen_Cursor CURSOR FOR SELECT INSERTED.Id, INSERTED.RequirementInvoiceId, INSERTED.SpecimenOrder  FROM INSERTED; 
	OPEN Specimen_Cursor; 

	FETCH NEXT FROM Specimen_Cursor INTO @Id, @RequirementInvoiceId, @SpecimenOrder; 
	WHILE @@FETCH_STATUS = 0 
	BEGIN
		SET @SerialInvoice = (SELECT Serial FROM dbo.SYSRequirementInvoice WHERE Id = @RequirementInvoiceId);
		SET @FieldId = (SELECT FieldId FROM dbo.SYSRequirementInvoice WHERE Id = @RequirementInvoiceId);
		SET @FieldSymbol = (SELECT Symbol FROM dbo.CTGField WHERE Id = @FieldId);

		UPDATE dbo.IDTestRequirement SET SpecimenCode = CONCAT('TN', @FieldSymbol, '/', CASE WHEN @SerialInvoice < 10 THEN '0' ELSE '' END, @SerialInvoice , '/', CASE WHEN @SpecimenOrder < 10 THEN '0' ELSE '' END, @SpecimenOrder) WHERE Id = @Id;
		FETCH NEXT FROM Specimen_Cursor INTO @Id, @RequirementInvoiceId, @SpecimenOrder; 
	END; 
	CLOSE Specimen_Cursor;
	DEALLOCATE Specimen_Cursor;	
END;

