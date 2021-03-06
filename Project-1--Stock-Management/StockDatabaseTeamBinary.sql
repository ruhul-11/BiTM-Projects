USE [master]
GO
/****** Object:  Database [StockDB]    Script Date: 13/11/2019 12:00:35 ******/
CREATE DATABASE [StockDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'StockDB', FILENAME = N'C:\Users\M i L o N\StockDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'StockDB_log', FILENAME = N'C:\Users\M i L o N\StockDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [StockDB] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [StockDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [StockDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [StockDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [StockDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [StockDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [StockDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [StockDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [StockDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [StockDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [StockDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [StockDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [StockDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [StockDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [StockDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [StockDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [StockDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [StockDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [StockDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [StockDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [StockDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [StockDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [StockDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [StockDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [StockDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [StockDB] SET  MULTI_USER 
GO
ALTER DATABASE [StockDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [StockDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [StockDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [StockDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [StockDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [StockDB] SET QUERY_STORE = OFF
GO
USE [StockDB]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [StockDB]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 13/11/2019 12:00:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryID] [int] NOT NULL,
	[CategoryName] [varchar](50) NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Companies]    Script Date: 13/11/2019 12:00:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Companies](
	[CompanyID] [int] NOT NULL,
	[CompanyName] [varchar](50) NULL,
 CONSTRAINT [PK_Companies] PRIMARY KEY CLUSTERED 
(
	[CompanyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Items]    Script Date: 13/11/2019 12:00:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Items](
	[ItemID] [int] IDENTITY(1,1) NOT NULL,
	[ItemName] [varchar](50) NULL,
	[CategoryID] [int] NULL,
	[CompanyID] [int] NULL,
	[AvailableQuantity] [int] NULL,
	[ReorderLevel] [int] NULL,
	[Date] [date] NULL,
	[Time] [time](7) NULL,
 CONSTRAINT [PK_Items] PRIMARY KEY CLUSTERED 
(
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ItemStatus]    Script Date: 13/11/2019 12:00:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemStatus](
	[StatusID] [int] IDENTITY(1,1) NOT NULL,
	[ItemID] [int] NULL,
	[ItemName] [varchar](50) NULL,
	[SaleQuantity] [int] NULL,
	[DamageQuantity] [int] NULL,
	[LostQuantity] [int] NULL,
 CONSTRAINT [PK_ItemStatus] PRIMARY KEY CLUSTERED 
(
	[StatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SaleItems]    Script Date: 13/11/2019 12:00:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleItems](
	[SaleID] [int] IDENTITY(1,1) NOT NULL,
	[ItemID] [int] NULL,
	[ItemName] [varchar](50) NULL,
	[SaleQty] [int] NULL,
	[Date] [date] NULL,
 CONSTRAINT [PK_SaleItems] PRIMARY KEY CLUSTERED 
(
	[SaleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StockOutItems]    Script Date: 13/11/2019 12:00:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StockOutItems](
	[ItemID] [int] NULL,
	[ItemName] [varchar](50) NULL,
	[CompanyName] [varchar](50) NULL,
	[Quantity] [int] NULL
) ON [PRIMARY]
GO
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (1, N'Stationary')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (2, N'Cosmetics')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (3, N'Electronics')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (4, N'Kitchen Items')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (5, N'Books')
INSERT [dbo].[Companies] ([CompanyID], [CompanyName]) VALUES (1, N'Unilever')
INSERT [dbo].[Companies] ([CompanyID], [CompanyName]) VALUES (2, N'RFL')
INSERT [dbo].[Companies] ([CompanyID], [CompanyName]) VALUES (3, N'Walton')
INSERT [dbo].[Companies] ([CompanyID], [CompanyName]) VALUES (4, N'Nova')
INSERT [dbo].[Companies] ([CompanyID], [CompanyName]) VALUES (5, N'Motorola')
SET IDENTITY_INSERT [dbo].[Items] ON 

INSERT [dbo].[Items] ([ItemID], [ItemName], [CategoryID], [CompanyID], [AvailableQuantity], [ReorderLevel], [Date], [Time]) VALUES (5010, N'Khata', 1, 1, 34, 10, CAST(N'2019-11-13' AS Date), CAST(N'08:38:46.7324163' AS Time))
INSERT [dbo].[Items] ([ItemID], [ItemName], [CategoryID], [CompanyID], [AvailableQuantity], [ReorderLevel], [Date], [Time]) VALUES (5011, N'Pen', 1, 1, 0, 10, CAST(N'2019-11-13' AS Date), CAST(N'08:50:31.8550334' AS Time))
INSERT [dbo].[Items] ([ItemID], [ItemName], [CategoryID], [CompanyID], [AvailableQuantity], [ReorderLevel], [Date], [Time]) VALUES (5012, N'Pencil', 1, 2, 0, 20, CAST(N'2019-11-13' AS Date), CAST(N'08:50:45.3571580' AS Time))
SET IDENTITY_INSERT [dbo].[Items] OFF
SET IDENTITY_INSERT [dbo].[ItemStatus] ON 

INSERT [dbo].[ItemStatus] ([StatusID], [ItemID], [ItemName], [SaleQuantity], [DamageQuantity], [LostQuantity]) VALUES (2, 5010, N'Khata', 6, 0, 0)
INSERT [dbo].[ItemStatus] ([StatusID], [ItemID], [ItemName], [SaleQuantity], [DamageQuantity], [LostQuantity]) VALUES (3, 5011, N'Pen', 0, 0, 0)
INSERT [dbo].[ItemStatus] ([StatusID], [ItemID], [ItemName], [SaleQuantity], [DamageQuantity], [LostQuantity]) VALUES (4, 5012, N'Pencil', 5, 0, 0)
SET IDENTITY_INSERT [dbo].[ItemStatus] OFF
SET IDENTITY_INSERT [dbo].[SaleItems] ON 

INSERT [dbo].[SaleItems] ([SaleID], [ItemID], [ItemName], [SaleQty], [Date]) VALUES (3, 5012, N'Pencil', 5, CAST(N'2019-11-13' AS Date))
INSERT [dbo].[SaleItems] ([SaleID], [ItemID], [ItemName], [SaleQty], [Date]) VALUES (4, 5010, N'Khata', 0, CAST(N'2019-11-13' AS Date))
INSERT [dbo].[SaleItems] ([SaleID], [ItemID], [ItemName], [SaleQty], [Date]) VALUES (5, 5010, N'Khata', 2, CAST(N'2019-11-13' AS Date))
INSERT [dbo].[SaleItems] ([SaleID], [ItemID], [ItemName], [SaleQty], [Date]) VALUES (6, 5010, N'Khata', 2, CAST(N'2019-11-13' AS Date))
INSERT [dbo].[SaleItems] ([SaleID], [ItemID], [ItemName], [SaleQty], [Date]) VALUES (7, 5010, N'Khata', 0, CAST(N'2019-11-13' AS Date))
INSERT [dbo].[SaleItems] ([SaleID], [ItemID], [ItemName], [SaleQty], [Date]) VALUES (8, 5010, N'Khata', 0, CAST(N'2019-11-13' AS Date))
INSERT [dbo].[SaleItems] ([SaleID], [ItemID], [ItemName], [SaleQty], [Date]) VALUES (9, 5010, N'Khata', 2, CAST(N'2019-11-13' AS Date))
SET IDENTITY_INSERT [dbo].[SaleItems] OFF
ALTER TABLE [dbo].[Items]  WITH CHECK ADD  CONSTRAINT [FK_Items_Categories] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Categories] ([CategoryID])
GO
ALTER TABLE [dbo].[Items] CHECK CONSTRAINT [FK_Items_Categories]
GO
ALTER TABLE [dbo].[Items]  WITH CHECK ADD  CONSTRAINT [FK_Items_Companies] FOREIGN KEY([CompanyID])
REFERENCES [dbo].[Companies] ([CompanyID])
GO
ALTER TABLE [dbo].[Items] CHECK CONSTRAINT [FK_Items_Companies]
GO
ALTER TABLE [dbo].[ItemStatus]  WITH CHECK ADD  CONSTRAINT [FK_ItemStatus_Items] FOREIGN KEY([ItemID])
REFERENCES [dbo].[Items] ([ItemID])
GO
ALTER TABLE [dbo].[ItemStatus] CHECK CONSTRAINT [FK_ItemStatus_Items]
GO
ALTER TABLE [dbo].[SaleItems]  WITH CHECK ADD  CONSTRAINT [FK_SaleItems_Items] FOREIGN KEY([ItemID])
REFERENCES [dbo].[Items] ([ItemID])
GO
ALTER TABLE [dbo].[SaleItems] CHECK CONSTRAINT [FK_SaleItems_Items]
GO
USE [master]
GO
ALTER DATABASE [StockDB] SET  READ_WRITE 
GO
