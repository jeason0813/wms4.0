
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/14/2016 09:18:06
-- Generated from EDMX file: F:\github\wms\DBModel\DB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [WMS];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_BackInputRecord]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Record] DROP CONSTRAINT [FK_BackInputRecord];
GO
IF OBJECT_ID(N'[dbo].[FK_BackOutputRecord]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Record] DROP CONSTRAINT [FK_BackOutputRecord];
GO
IF OBJECT_ID(N'[dbo].[FK_CheckBillCheckDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CheckDetail] DROP CONSTRAINT [FK_CheckBillCheckDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_CheckBillRecord]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Record] DROP CONSTRAINT [FK_CheckBillRecord];
GO
IF OBJECT_ID(N'[dbo].[FK_DIYBillRecord]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Record] DROP CONSTRAINT [FK_DIYBillRecord];
GO
IF OBJECT_ID(N'[dbo].[FK_GiveBillRecord]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Record] DROP CONSTRAINT [FK_GiveBillRecord];
GO
IF OBJECT_ID(N'[dbo].[FK_LocationChangeRecord]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Record] DROP CONSTRAINT [FK_LocationChangeRecord];
GO
IF OBJECT_ID(N'[dbo].[FK_MenuAMenuB]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MenuB] DROP CONSTRAINT [FK_MenuAMenuB];
GO
IF OBJECT_ID(N'[dbo].[FK_OtherInputRecord]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Record] DROP CONSTRAINT [FK_OtherInputRecord];
GO
IF OBJECT_ID(N'[dbo].[FK_OtherOutputRecord]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Record] DROP CONSTRAINT [FK_OtherOutputRecord];
GO
IF OBJECT_ID(N'[dbo].[FK_TransferBillRecord]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Record] DROP CONSTRAINT [FK_TransferBillRecord];
GO
IF OBJECT_ID(N'[dbo].[FK_WarehouseLocation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Location] DROP CONSTRAINT [FK_WarehouseLocation];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[BackInput]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BackInput];
GO
IF OBJECT_ID(N'[dbo].[BackOutput]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BackOutput];
GO
IF OBJECT_ID(N'[dbo].[BusinessType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BusinessType];
GO
IF OBJECT_ID(N'[dbo].[CheckBill]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CheckBill];
GO
IF OBJECT_ID(N'[dbo].[CheckDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CheckDetail];
GO
IF OBJECT_ID(N'[dbo].[CostItem]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CostItem];
GO
IF OBJECT_ID(N'[dbo].[DIYBill]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DIYBill];
GO
IF OBJECT_ID(N'[dbo].[GiveBill]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GiveBill];
GO
IF OBJECT_ID(N'[dbo].[GoodItem]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GoodItem];
GO
IF OBJECT_ID(N'[dbo].[InOutType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InOutType];
GO
IF OBJECT_ID(N'[dbo].[InWarehouse]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InWarehouse];
GO
IF OBJECT_ID(N'[dbo].[ItemLine]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ItemLine];
GO
IF OBJECT_ID(N'[dbo].[ItemUnit]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ItemUnit];
GO
IF OBJECT_ID(N'[dbo].[LineWay]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LineWay];
GO
IF OBJECT_ID(N'[dbo].[LoadGoodsType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LoadGoodsType];
GO
IF OBJECT_ID(N'[dbo].[Location]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Location];
GO
IF OBJECT_ID(N'[dbo].[LocationChange]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LocationChange];
GO
IF OBJECT_ID(N'[dbo].[MenuA]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MenuA];
GO
IF OBJECT_ID(N'[dbo].[MenuB]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MenuB];
GO
IF OBJECT_ID(N'[dbo].[OtherInput]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OtherInput];
GO
IF OBJECT_ID(N'[dbo].[OtherOutput]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OtherOutput];
GO
IF OBJECT_ID(N'[dbo].[ReceiveFile]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ReceiveFile];
GO
IF OBJECT_ID(N'[dbo].[Record]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Record];
GO
IF OBJECT_ID(N'[dbo].[SendFileSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SendFileSet];
GO
IF OBJECT_ID(N'[dbo].[TaskBill]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TaskBill];
GO
IF OBJECT_ID(N'[dbo].[TransferBill]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TransferBill];
GO
IF OBJECT_ID(N'[dbo].[Warehouse]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Warehouse];
GO
IF OBJECT_ID(N'[DBStoreContainer].[Company]', 'U') IS NOT NULL
    DROP TABLE [DBStoreContainer].[Company];
GO
IF OBJECT_ID(N'[DBStoreContainer].[Departent]', 'U') IS NOT NULL
    DROP TABLE [DBStoreContainer].[Departent];
GO
IF OBJECT_ID(N'[DBStoreContainer].[User]', 'U') IS NOT NULL
    DROP TABLE [DBStoreContainer].[User];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'TransferBill'
CREATE TABLE [dbo].[TransferBill] (
    [Id] uniqueidentifier  NOT NULL,
    [BillCode] nvarchar(50)  NULL,
    [CreateDate] datetime  NULL,
    [ChargePerson] nvarchar(50)  NULL,
    [ExaminePerson] nvarchar(50)  NULL,
    [ExamineDate] datetime  NULL,
    [MakePerson] nvarchar(50)  NULL,
    [Warehouse] nvarchar(100)  NULL,
    [WarehouseId] int  NULL,
    [InputType] nvarchar(100)  NULL,
    [BusinessType] nvarchar(100)  NULL,
    [LineWay] nvarchar(100)  NULL,
    [Remark] nvarchar(1000)  NULL,
    [LBBillCode] nvarchar(100)  NULL,
    [LBBillDate] datetime  NULL,
    [LBFrom] nvarchar(100)  NULL,
    [LBMoveType] nvarchar(100)  NULL,
    [LBRemark] nvarchar(500)  NULL,
    [BillState] int  NULL,
    [Department] nvarchar(100)  NULL,
    [DepartmentId] nvarchar(100)  NULL,
    [Company] nvarchar(100)  NULL,
    [CompanyId] nvarchar(100)  NULL
);
GO

-- Creating table 'BackInput'
CREATE TABLE [dbo].[BackInput] (
    [Id] uniqueidentifier  NOT NULL,
    [BillCode] nvarchar(50)  NULL,
    [CreateDate] datetime  NULL,
    [ChargePerson] nvarchar(50)  NULL,
    [ExaminePerson] nvarchar(50)  NULL,
    [ExamineDate] datetime  NULL,
    [MakePerson] nvarchar(50)  NULL,
    [Warehouse] nvarchar(100)  NULL,
    [WarehouseId] int  NULL,
    [OutputType] nvarchar(100)  NULL,
    [BusinessType] nvarchar(100)  NULL,
    [LineWay] nvarchar(100)  NULL,
    [Remark] nvarchar(1000)  NULL,
    [LBBillCode] nvarchar(100)  NULL,
    [LBBillDate] datetime  NULL,
    [LBBackWarehouse] nvarchar(100)  NULL,
    [LBMoveType] nvarchar(100)  NULL,
    [LBRemark] nvarchar(500)  NULL,
    [LBBackAddress] nvarchar(500)  NULL,
    [LBContacts] nvarchar(100)  NULL,
    [LBPhone] nvarchar(100)  NULL,
    [BillState] int  NULL,
    [Department] nvarchar(100)  NULL,
    [DepartmentId] nvarchar(100)  NULL,
    [Company] nvarchar(100)  NULL,
    [CompanyId] nvarchar(100)  NULL
);
GO

-- Creating table 'OtherInput'
CREATE TABLE [dbo].[OtherInput] (
    [Id] uniqueidentifier  NOT NULL,
    [BillCode] nvarchar(50)  NULL,
    [CreateDate] datetime  NULL,
    [ChargePerson] nvarchar(50)  NULL,
    [ExaminePerson] nvarchar(50)  NULL,
    [ExamineDate] datetime  NULL,
    [MakePerson] nvarchar(50)  NULL,
    [Warehouse] nvarchar(100)  NULL,
    [WarehouseId] int  NULL,
    [InputType] nvarchar(100)  NULL,
    [BusinessType] nvarchar(100)  NULL,
    [LineWay] nvarchar(100)  NULL,
    [Remark] nvarchar(1000)  NULL,
    [BillState] int  NULL,
    [Department] nvarchar(100)  NULL,
    [DepartmentId] nvarchar(100)  NULL,
    [Company] nvarchar(100)  NULL,
    [CompanyId] nvarchar(100)  NULL
);
GO

-- Creating table 'DIYBill'
CREATE TABLE [dbo].[DIYBill] (
    [Id] uniqueidentifier  NOT NULL,
    [BillCode] nvarchar(50)  NULL,
    [CreateDate] datetime  NULL,
    [ChargePerson] nvarchar(50)  NULL,
    [ExaminePerson] nvarchar(50)  NULL,
    [ExamineDate] datetime  NULL,
    [MakePerson] nvarchar(50)  NULL,
    [InputType] nvarchar(100)  NULL,
    [Remark] nvarchar(1000)  NULL,
    [BillState] int  NULL,
    [Department] nvarchar(100)  NULL,
    [DepartmentId] nvarchar(100)  NULL,
    [Company] nvarchar(100)  NULL,
    [CompanyId] nvarchar(100)  NULL
);
GO

-- Creating table 'LocationChange'
CREATE TABLE [dbo].[LocationChange] (
    [Id] uniqueidentifier  NOT NULL,
    [BillCode] nvarchar(50)  NULL,
    [CreateDate] datetime  NULL,
    [ChargePerson] nvarchar(50)  NULL,
    [ExaminePerson] nvarchar(50)  NULL,
    [ExamineDate] datetime  NULL,
    [MakePerson] nvarchar(50)  NULL,
    [Remark] nvarchar(1000)  NULL,
    [BillState] int  NULL,
    [Department] nvarchar(100)  NULL,
    [DepartmentId] nvarchar(100)  NULL,
    [Company] nvarchar(100)  NULL,
    [CompanyId] nvarchar(100)  NULL
);
GO

-- Creating table 'CheckBill'
CREATE TABLE [dbo].[CheckBill] (
    [Id] uniqueidentifier  NOT NULL,
    [BillCode] nvarchar(50)  NULL,
    [CreateDate] datetime  NULL,
    [Warehouse] nvarchar(100)  NULL,
    [WarehouseId] int  NULL,
    [ChargePerson] nvarchar(50)  NULL,
    [ExaminePerson] nvarchar(50)  NULL,
    [ExamineDate] datetime  NULL,
    [MakePerson] nvarchar(50)  NULL,
    [Remark] nvarchar(1000)  NULL,
    [BillState] int  NULL,
    [Department] nvarchar(100)  NULL,
    [DepartmentId] nvarchar(100)  NULL,
    [Company] nvarchar(100)  NULL,
    [CompanyId] nvarchar(100)  NULL
);
GO

-- Creating table 'GiveBill'
CREATE TABLE [dbo].[GiveBill] (
    [Id] uniqueidentifier  NOT NULL,
    [BillCode] nvarchar(50)  NULL,
    [CreateDate] datetime  NULL,
    [ChargePerson] nvarchar(50)  NULL,
    [ExaminePerson] nvarchar(50)  NULL,
    [ExamineDate] datetime  NULL,
    [MakePerson] nvarchar(50)  NULL,
    [Warehouse] nvarchar(100)  NULL,
    [WarehouseId] int  NULL,
    [OutputType] nvarchar(100)  NULL,
    [BusinessType] nvarchar(100)  NULL,
    [LineWay] nvarchar(100)  NULL,
    [Remark] nvarchar(1000)  NULL,
    [LBBillCode] nvarchar(100)  NULL,
    [LBBillDate] datetime  NULL,
    [LBTaskBillCode] nvarchar(100)  NULL,
    [LBLine] nvarchar(200)  NULL,
    [LBContacts] nvarchar(100)  NULL,
    [LBPhone] nvarchar(100)  NULL,
    [LBMailCode] nvarchar(100)  NULL,
    [LBCustomerCode] nvarchar(100)  NULL,
    [LBCustomerName] nvarchar(500)  NULL,
    [LBSendAddress] nvarchar(100)  NULL,
    [LBRemark] nvarchar(500)  NULL,
    [BillState] int  NULL,
    [Department] nvarchar(100)  NULL,
    [DepartmentId] nvarchar(100)  NULL,
    [Company] nvarchar(100)  NULL,
    [CompanyId] nvarchar(100)  NULL
);
GO

-- Creating table 'BackOutput'
CREATE TABLE [dbo].[BackOutput] (
    [Id] uniqueidentifier  NOT NULL,
    [BillCode] nvarchar(50)  NULL,
    [CreateDate] datetime  NULL,
    [Warehouse] nvarchar(100)  NULL,
    [WarehouseId] int  NULL,
    [InputType] nvarchar(100)  NULL,
    [ChargePerson] nvarchar(50)  NULL,
    [ExaminePerson] nvarchar(50)  NULL,
    [ExamineDate] datetime  NULL,
    [MakePerson] nvarchar(50)  NULL,
    [BusinessType] nvarchar(100)  NULL,
    [LineWay] nvarchar(100)  NULL,
    [Remark] nvarchar(1000)  NULL,
    [LBBillCode] nvarchar(100)  NULL,
    [LBGiveBillCode] nvarchar(100)  NULL,
    [LBBillDate] datetime  NULL,
    [LBCustomerCode] nvarchar(100)  NULL,
    [LBCustomerName] nvarchar(500)  NULL,
    [LBSendAddress] nvarchar(100)  NULL,
    [LBContacts] nvarchar(100)  NULL,
    [LBPhone] nvarchar(100)  NULL,
    [LBMailCode] nvarchar(100)  NULL,
    [LBReson] nvarchar(500)  NULL,
    [LBRemark] nvarchar(500)  NULL,
    [BillState] int  NULL,
    [Department] nvarchar(100)  NULL,
    [DepartmentId] nvarchar(100)  NULL,
    [Company] nvarchar(100)  NULL,
    [CompanyId] nvarchar(100)  NULL
);
GO

-- Creating table 'OtherOutput'
CREATE TABLE [dbo].[OtherOutput] (
    [Id] uniqueidentifier  NOT NULL,
    [BillCode] nvarchar(50)  NULL,
    [CreateDate] datetime  NULL,
    [Warehouse] nvarchar(100)  NULL,
    [WarehouseId] int  NULL,
    [OutputType] nvarchar(100)  NULL,
    [MakePerson] nvarchar(50)  NULL,
    [ExamineDate] datetime  NULL,
    [ExaminePerson] nvarchar(50)  NULL,
    [ChargePerson] nvarchar(50)  NULL,
    [BusinessType] nvarchar(100)  NULL,
    [LineWay] nvarchar(100)  NULL,
    [Remark] nvarchar(1000)  NULL,
    [BillState] int  NULL,
    [Department] nvarchar(100)  NULL,
    [DepartmentId] nvarchar(100)  NULL,
    [Company] nvarchar(100)  NULL,
    [CompanyId] nvarchar(100)  NULL
);
GO

-- Creating table 'Record'
CREATE TABLE [dbo].[Record] (
    [Id] uniqueidentifier  NOT NULL,
    [MainTableId] uniqueidentifier  NULL,
    [ItemCode] nvarchar(100)  NULL,
    [ItemLine] nvarchar(100)  NULL,
    [ItemName] nvarchar(100)  NULL,
    [ItemSpecifications] nvarchar(100)  NULL,
    [ItemLocation] nvarchar(100)  NULL,
    [Warehouse] nvarchar(100)  NULL,
    [WarehouseId] int  NULL,
    [ItemBatch] nvarchar(100)  NULL,
    [ItemUnit] nvarchar(100)  NULL,
    [Count] float  NULL,
    [Weight] float  NULL,
    [ReceiveCount] float  NULL,
    [Remark] nvarchar(500)  NULL,
    [State] int  NULL,
    [CreateDate] datetime  NULL,
    [ExamineDate] datetime  NULL,
    [Department] nvarchar(100)  NULL,
    [DepartmentId] nvarchar(100)  NULL
);
GO

-- Creating table 'MenuA'
CREATE TABLE [dbo].[MenuA] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(100)  NULL,
    [OrderNumber] int  NULL
);
GO

-- Creating table 'MenuB'
CREATE TABLE [dbo].[MenuB] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [MenuAId] int  NOT NULL,
    [Name] nvarchar(100)  NULL,
    [Url] nvarchar(500)  NULL,
    [OrderNumber] int  NULL
);
GO

-- Creating table 'InWarehouse'
CREATE TABLE [dbo].[InWarehouse] (
    [Id] uniqueidentifier  NOT NULL,
    [ItemCode] nvarchar(max)  NOT NULL,
    [ItemLine] nvarchar(max)  NOT NULL,
    [ItemName] nvarchar(max)  NOT NULL,
    [ItemSpecifications] nvarchar(max)  NOT NULL,
    [ItemLocation] nvarchar(max)  NOT NULL,
    [ItemBatch] nvarchar(100)  NULL,
    [ItemUnit] nvarchar(100)  NULL,
    [Count] float  NULL,
    [Department] nvarchar(100)  NULL,
    [DepartmentId] nvarchar(100)  NULL,
    [Company] nvarchar(100)  NULL,
    [CompanyId] nvarchar(100)  NULL
);
GO

-- Creating table 'Location'
CREATE TABLE [dbo].[Location] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [WarehouseId] int  NOT NULL,
    [Name] nvarchar(100)  NULL,
    [CreateDate] datetime  NULL,
    [CreateBy] nvarchar(100)  NULL,
    [Remark] nvarchar(500)  NULL,
    [Department] nvarchar(100)  NULL,
    [DepartmentId] nvarchar(100)  NULL,
    [Company] nvarchar(100)  NULL,
    [CompanyId] nvarchar(100)  NULL
);
GO

-- Creating table 'Warehouse'
CREATE TABLE [dbo].[Warehouse] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(100)  NULL,
    [Remark] nvarchar(500)  NULL,
    [IsManage] int  NULL,
    [CreateBy] nvarchar(100)  NULL,
    [CreateDate] datetime  NULL,
    [Owner] nvarchar(100)  NULL,
    [Department] nvarchar(100)  NULL,
    [DepartmentId] nvarchar(100)  NULL,
    [Company] nvarchar(100)  NULL,
    [CompanyId] nvarchar(100)  NULL
);
GO

-- Creating table 'TaskBill'
CREATE TABLE [dbo].[TaskBill] (
    [Id] uniqueidentifier  NOT NULL,
    [BillCode] nvarchar(50)  NULL,
    [CreateDate] datetime  NULL,
    [MakePerson] nvarchar(50)  NULL,
    [ExamineDate] datetime  NULL,
    [ExaminePerson] nvarchar(50)  NULL,
    [ChargePerson] nvarchar(50)  NULL,
    [Remark] nvarchar(1000)  NULL,
    [Department] nvarchar(100)  NULL,
    [DepartmentId] nvarchar(100)  NULL,
    [Company] nvarchar(100)  NULL,
    [CompanyId] nvarchar(100)  NULL
);
GO

-- Creating table 'GoodItem'
CREATE TABLE [dbo].[GoodItem] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ItemCode] nvarchar(100)  NULL,
    [ItemLine] nvarchar(100)  NULL,
    [ItemName] nvarchar(100)  NULL,
    [ItemSpecifications] nvarchar(100)  NULL,
    [ItemLocation] nvarchar(100)  NULL,
    [ItemUnit] nvarchar(100)  NULL,
    [IsBatch] int  NULL,
    [Department] nvarchar(100)  NULL,
    [DepartmentId] nvarchar(100)  NULL,
    [Company] nvarchar(100)  NULL,
    [CompanyId] nvarchar(100)  NULL
);
GO

-- Creating table 'CheckDetail'
CREATE TABLE [dbo].[CheckDetail] (
    [Id] uniqueidentifier  NOT NULL,
    [MainTableId] uniqueidentifier  NULL,
    [ItemCode] nvarchar(100)  NULL,
    [ItemLine] nvarchar(100)  NULL,
    [ItemName] nvarchar(100)  NULL,
    [ItemSpecifications] nvarchar(100)  NULL,
    [ItemLocation] nvarchar(100)  NULL,
    [ItemBatch] nvarchar(100)  NULL,
    [ItemUnit] nvarchar(100)  NULL,
    [Count] float  NULL,
    [Remark] nvarchar(500)  NULL,
    [State] int  NULL,
    [CreateDate] datetime  NULL,
    [ExamineDate] datetime  NULL
);
GO

-- Creating table 'InOutType'
CREATE TABLE [dbo].[InOutType] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(100)  NULL,
    [InoutType] int  NULL,
    [Remark] nvarchar(500)  NULL,
    [Department] nvarchar(100)  NULL,
    [DepartmentId] nvarchar(100)  NULL,
    [Company] nvarchar(100)  NULL,
    [CompanyId] nvarchar(100)  NULL
);
GO

-- Creating table 'BusinessType'
CREATE TABLE [dbo].[BusinessType] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(100)  NULL,
    [Remark] nvarchar(500)  NULL,
    [Department] nvarchar(100)  NULL,
    [DepartmentId] nvarchar(100)  NULL,
    [Company] nvarchar(100)  NULL,
    [CompanyId] nvarchar(100)  NULL
);
GO

-- Creating table 'LineWay'
CREATE TABLE [dbo].[LineWay] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(100)  NULL,
    [Remark] nvarchar(500)  NULL,
    [Department] nvarchar(100)  NULL,
    [DepartmentId] nvarchar(100)  NULL,
    [Company] nvarchar(100)  NULL,
    [CompanyId] nvarchar(100)  NULL
);
GO

-- Creating table 'LoadGoodsType'
CREATE TABLE [dbo].[LoadGoodsType] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(100)  NULL,
    [Remark] nvarchar(500)  NULL,
    [Department] nvarchar(100)  NULL,
    [DepartmentId] nvarchar(100)  NULL,
    [Company] nvarchar(100)  NULL,
    [CompanyId] nvarchar(100)  NULL
);
GO

-- Creating table 'ItemLine'
CREATE TABLE [dbo].[ItemLine] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(100)  NULL,
    [Remark] nvarchar(500)  NULL,
    [Department] nvarchar(100)  NULL,
    [DepartmentId] nvarchar(100)  NULL,
    [Company] nvarchar(100)  NULL,
    [CompanyId] nvarchar(100)  NULL
);
GO

-- Creating table 'ItemUnit'
CREATE TABLE [dbo].[ItemUnit] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(100)  NULL,
    [Remark] nvarchar(500)  NULL,
    [Department] nvarchar(100)  NULL,
    [DepartmentId] nvarchar(100)  NULL,
    [Company] nvarchar(100)  NULL,
    [CompanyId] nvarchar(100)  NULL
);
GO

-- Creating table 'CostItem'
CREATE TABLE [dbo].[CostItem] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(100)  NULL,
    [Type] int  NULL,
    [Department] nvarchar(100)  NULL,
    [DepartmentId] nvarchar(100)  NULL,
    [Company] nvarchar(100)  NULL,
    [CompanyId] nvarchar(100)  NULL
);
GO

-- Creating table 'ReceiveFile'
CREATE TABLE [dbo].[ReceiveFile] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Contacts] nvarchar(max)  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [Emailcode] nvarchar(max)  NOT NULL,
    [Phone] nvarchar(max)  NOT NULL,
    [Tel] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Fax] nvarchar(max)  NOT NULL,
    [CreateBy] nvarchar(max)  NOT NULL,
    [CreateDate] nvarchar(max)  NOT NULL,
    [Department] nvarchar(100)  NULL,
    [DepartmentId] nvarchar(100)  NULL,
    [Company] nvarchar(100)  NULL,
    [CompanyId] nvarchar(100)  NULL
);
GO

-- Creating table 'SendFileSet'
CREATE TABLE [dbo].[SendFileSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Contacts] nvarchar(max)  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [Emailcode] nvarchar(max)  NOT NULL,
    [Phone] nvarchar(max)  NOT NULL,
    [Tel] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Fax] nvarchar(max)  NOT NULL,
    [CreateBy] nvarchar(max)  NOT NULL,
    [CreateDate] nvarchar(max)  NOT NULL,
    [Department] nvarchar(100)  NULL,
    [DepartmentId] nvarchar(100)  NULL,
    [Company] nvarchar(100)  NULL,
    [CompanyId] nvarchar(100)  NULL
);
GO

-- Creating table 'Company'
CREATE TABLE [dbo].[Company] (
    [CmopanyCode] varchar(30)  NOT NULL,
    [CompanyAbbr] varchar(30)  NULL,
    [CompanyFullName] varchar(60)  NULL,
    [Corporate] varchar(50)  NULL,
    [FoundDT] datetime  NULL,
    [Address] varchar(120)  NULL,
    [Remark] varchar(100)  NULL,
    [IsCenter] char(1)  NULL,
    [CreateBy] varchar(30)  NULL,
    [CreateDT] datetime  NULL
);
GO

-- Creating table 'Departent'
CREATE TABLE [dbo].[Departent] (
    [DeptCode] varchar(50)  NOT NULL,
    [DeptType] int  NULL,
    [CompanyCode] varchar(50)  NULL,
    [DeptName] varchar(50)  NULL,
    [TEL] varchar(50)  NULL,
    [ContractType] int  NULL,
    [IsCenter] char(1)  NULL,
    [Address] varchar(120)  NULL,
    [CreateBy] varchar(30)  NULL,
    [CreateDT] datetime  NULL,
    [Area] nvarchar(100)  NULL,
    [Longitude] decimal(18,6)  NULL,
    [Latitude] decimal(18,6)  NULL,
    [ProvinceCode] int  NULL,
    [CityCode] int  NULL,
    [AreaCode] int  NULL
);
GO

-- Creating table 'User'
CREATE TABLE [dbo].[User] (
    [UserCode] varchar(50)  NOT NULL,
    [UserName] varchar(50)  NULL,
    [UserPassword] varchar(50)  NULL,
    [TEL] varchar(50)  NULL,
    [Phone] varchar(50)  NULL,
    [EMail] varchar(50)  NULL,
    [Takedt] datetime  NULL,
    [Sex] int  NULL,
    [Birthday] datetime  NULL,
    [Address] nvarchar(100)  NULL,
    [IsVail] char(1)  NULL,
    [Remark] nvarchar(50)  NULL,
    [CreateBy] varchar(30)  NULL,
    [CreateDT] datetime  NULL,
    [ModifyPwdDt] datetime  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'TransferBill'
ALTER TABLE [dbo].[TransferBill]
ADD CONSTRAINT [PK_TransferBill]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BackInput'
ALTER TABLE [dbo].[BackInput]
ADD CONSTRAINT [PK_BackInput]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OtherInput'
ALTER TABLE [dbo].[OtherInput]
ADD CONSTRAINT [PK_OtherInput]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DIYBill'
ALTER TABLE [dbo].[DIYBill]
ADD CONSTRAINT [PK_DIYBill]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LocationChange'
ALTER TABLE [dbo].[LocationChange]
ADD CONSTRAINT [PK_LocationChange]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CheckBill'
ALTER TABLE [dbo].[CheckBill]
ADD CONSTRAINT [PK_CheckBill]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'GiveBill'
ALTER TABLE [dbo].[GiveBill]
ADD CONSTRAINT [PK_GiveBill]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BackOutput'
ALTER TABLE [dbo].[BackOutput]
ADD CONSTRAINT [PK_BackOutput]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OtherOutput'
ALTER TABLE [dbo].[OtherOutput]
ADD CONSTRAINT [PK_OtherOutput]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Record'
ALTER TABLE [dbo].[Record]
ADD CONSTRAINT [PK_Record]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MenuA'
ALTER TABLE [dbo].[MenuA]
ADD CONSTRAINT [PK_MenuA]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MenuB'
ALTER TABLE [dbo].[MenuB]
ADD CONSTRAINT [PK_MenuB]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'InWarehouse'
ALTER TABLE [dbo].[InWarehouse]
ADD CONSTRAINT [PK_InWarehouse]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Location'
ALTER TABLE [dbo].[Location]
ADD CONSTRAINT [PK_Location]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Warehouse'
ALTER TABLE [dbo].[Warehouse]
ADD CONSTRAINT [PK_Warehouse]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TaskBill'
ALTER TABLE [dbo].[TaskBill]
ADD CONSTRAINT [PK_TaskBill]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'GoodItem'
ALTER TABLE [dbo].[GoodItem]
ADD CONSTRAINT [PK_GoodItem]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CheckDetail'
ALTER TABLE [dbo].[CheckDetail]
ADD CONSTRAINT [PK_CheckDetail]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'InOutType'
ALTER TABLE [dbo].[InOutType]
ADD CONSTRAINT [PK_InOutType]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BusinessType'
ALTER TABLE [dbo].[BusinessType]
ADD CONSTRAINT [PK_BusinessType]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LineWay'
ALTER TABLE [dbo].[LineWay]
ADD CONSTRAINT [PK_LineWay]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LoadGoodsType'
ALTER TABLE [dbo].[LoadGoodsType]
ADD CONSTRAINT [PK_LoadGoodsType]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ItemLine'
ALTER TABLE [dbo].[ItemLine]
ADD CONSTRAINT [PK_ItemLine]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ItemUnit'
ALTER TABLE [dbo].[ItemUnit]
ADD CONSTRAINT [PK_ItemUnit]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CostItem'
ALTER TABLE [dbo].[CostItem]
ADD CONSTRAINT [PK_CostItem]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ReceiveFile'
ALTER TABLE [dbo].[ReceiveFile]
ADD CONSTRAINT [PK_ReceiveFile]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SendFileSet'
ALTER TABLE [dbo].[SendFileSet]
ADD CONSTRAINT [PK_SendFileSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [CmopanyCode] in table 'Company'
ALTER TABLE [dbo].[Company]
ADD CONSTRAINT [PK_Company]
    PRIMARY KEY CLUSTERED ([CmopanyCode] ASC);
GO

-- Creating primary key on [DeptCode] in table 'Departent'
ALTER TABLE [dbo].[Departent]
ADD CONSTRAINT [PK_Departent]
    PRIMARY KEY CLUSTERED ([DeptCode] ASC);
GO

-- Creating primary key on [UserCode] in table 'User'
ALTER TABLE [dbo].[User]
ADD CONSTRAINT [PK_User]
    PRIMARY KEY CLUSTERED ([UserCode] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [MainTableId] in table 'Record'
ALTER TABLE [dbo].[Record]
ADD CONSTRAINT [FK_TransferBillRecord]
    FOREIGN KEY ([MainTableId])
    REFERENCES [dbo].[TransferBill]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TransferBillRecord'
CREATE INDEX [IX_FK_TransferBillRecord]
ON [dbo].[Record]
    ([MainTableId]);
GO

-- Creating foreign key on [MainTableId] in table 'Record'
ALTER TABLE [dbo].[Record]
ADD CONSTRAINT [FK_BackInputRecord]
    FOREIGN KEY ([MainTableId])
    REFERENCES [dbo].[BackInput]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BackInputRecord'
CREATE INDEX [IX_FK_BackInputRecord]
ON [dbo].[Record]
    ([MainTableId]);
GO

-- Creating foreign key on [MainTableId] in table 'Record'
ALTER TABLE [dbo].[Record]
ADD CONSTRAINT [FK_OtherInputRecord]
    FOREIGN KEY ([MainTableId])
    REFERENCES [dbo].[OtherInput]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OtherInputRecord'
CREATE INDEX [IX_FK_OtherInputRecord]
ON [dbo].[Record]
    ([MainTableId]);
GO

-- Creating foreign key on [MainTableId] in table 'Record'
ALTER TABLE [dbo].[Record]
ADD CONSTRAINT [FK_DIYBillRecord]
    FOREIGN KEY ([MainTableId])
    REFERENCES [dbo].[DIYBill]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DIYBillRecord'
CREATE INDEX [IX_FK_DIYBillRecord]
ON [dbo].[Record]
    ([MainTableId]);
GO

-- Creating foreign key on [MainTableId] in table 'Record'
ALTER TABLE [dbo].[Record]
ADD CONSTRAINT [FK_LocationChangeRecord]
    FOREIGN KEY ([MainTableId])
    REFERENCES [dbo].[LocationChange]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LocationChangeRecord'
CREATE INDEX [IX_FK_LocationChangeRecord]
ON [dbo].[Record]
    ([MainTableId]);
GO

-- Creating foreign key on [MainTableId] in table 'Record'
ALTER TABLE [dbo].[Record]
ADD CONSTRAINT [FK_CheckBillRecord]
    FOREIGN KEY ([MainTableId])
    REFERENCES [dbo].[CheckBill]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CheckBillRecord'
CREATE INDEX [IX_FK_CheckBillRecord]
ON [dbo].[Record]
    ([MainTableId]);
GO

-- Creating foreign key on [MainTableId] in table 'Record'
ALTER TABLE [dbo].[Record]
ADD CONSTRAINT [FK_GiveBillRecord]
    FOREIGN KEY ([MainTableId])
    REFERENCES [dbo].[GiveBill]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GiveBillRecord'
CREATE INDEX [IX_FK_GiveBillRecord]
ON [dbo].[Record]
    ([MainTableId]);
GO

-- Creating foreign key on [MainTableId] in table 'Record'
ALTER TABLE [dbo].[Record]
ADD CONSTRAINT [FK_BackOutputRecord]
    FOREIGN KEY ([MainTableId])
    REFERENCES [dbo].[BackOutput]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BackOutputRecord'
CREATE INDEX [IX_FK_BackOutputRecord]
ON [dbo].[Record]
    ([MainTableId]);
GO

-- Creating foreign key on [MainTableId] in table 'Record'
ALTER TABLE [dbo].[Record]
ADD CONSTRAINT [FK_OtherOutputRecord]
    FOREIGN KEY ([MainTableId])
    REFERENCES [dbo].[OtherOutput]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OtherOutputRecord'
CREATE INDEX [IX_FK_OtherOutputRecord]
ON [dbo].[Record]
    ([MainTableId]);
GO

-- Creating foreign key on [MenuAId] in table 'MenuB'
ALTER TABLE [dbo].[MenuB]
ADD CONSTRAINT [FK_MenuAMenuB]
    FOREIGN KEY ([MenuAId])
    REFERENCES [dbo].[MenuA]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MenuAMenuB'
CREATE INDEX [IX_FK_MenuAMenuB]
ON [dbo].[MenuB]
    ([MenuAId]);
GO

-- Creating foreign key on [WarehouseId] in table 'Location'
ALTER TABLE [dbo].[Location]
ADD CONSTRAINT [FK_WarehouseLocation]
    FOREIGN KEY ([WarehouseId])
    REFERENCES [dbo].[Warehouse]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WarehouseLocation'
CREATE INDEX [IX_FK_WarehouseLocation]
ON [dbo].[Location]
    ([WarehouseId]);
GO

-- Creating foreign key on [MainTableId] in table 'CheckDetail'
ALTER TABLE [dbo].[CheckDetail]
ADD CONSTRAINT [FK_CheckBillCheckDetail]
    FOREIGN KEY ([MainTableId])
    REFERENCES [dbo].[CheckBill]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CheckBillCheckDetail'
CREATE INDEX [IX_FK_CheckBillCheckDetail]
ON [dbo].[CheckDetail]
    ([MainTableId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------