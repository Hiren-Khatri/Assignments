USE [master]
GO
/****** Object:  Database [DbProductManagement]    Script Date: 1/12/2021 4:08:18 PM ******/
CREATE DATABASE [DbProductManagement]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DbProductManagement', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\DbProductManagement.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DbProductManagement_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\DbProductManagement_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [DbProductManagement] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DbProductManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DbProductManagement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DbProductManagement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DbProductManagement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DbProductManagement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DbProductManagement] SET ARITHABORT OFF 
GO
ALTER DATABASE [DbProductManagement] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DbProductManagement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DbProductManagement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DbProductManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DbProductManagement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DbProductManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DbProductManagement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DbProductManagement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DbProductManagement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DbProductManagement] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DbProductManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DbProductManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DbProductManagement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DbProductManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DbProductManagement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DbProductManagement] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DbProductManagement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DbProductManagement] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DbProductManagement] SET  MULTI_USER 
GO
ALTER DATABASE [DbProductManagement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DbProductManagement] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DbProductManagement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DbProductManagement] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DbProductManagement] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DbProductManagement] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [DbProductManagement] SET QUERY_STORE = OFF
GO
USE [DbProductManagement]
GO
/****** Object:  Table [dbo].[ProductCategories]    Script Date: 1/12/2021 4:08:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductCategories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ProductCategories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UX_ProductCategories_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 1/12/2021 4:08:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Price] [float] NOT NULL,
	[Quantity] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[ShortDescription] [nvarchar](100) NOT NULL,
	[LongDescription] [nvarchar](max) NULL,
	[SmallImage] [nvarchar](max) NOT NULL,
	[LargeImage] [nvarchar](max) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 1/12/2021 4:08:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](30) NULL,
	[LastName] [nvarchar](30) NULL,
	[Password] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_ProductCategories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[ProductCategories] ([Id])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_ProductCategories_CategoryId]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Users_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Users_CreatedBy]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Users_ModifiedBy] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Users_ModifiedBy]
GO
USE [master]
GO
ALTER DATABASE [DbProductManagement] SET  READ_WRITE 
GO
