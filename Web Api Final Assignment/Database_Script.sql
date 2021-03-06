USE [master]
GO
/****** Object:  Database [DbHotelManagement]    Script Date: 1/14/2021 6:33:16 PM ******/
CREATE DATABASE [DbHotelManagement]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DbHotelManagement', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\DbHotelManagement.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DbHotelManagement_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\DbHotelManagement_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [DbHotelManagement] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DbHotelManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DbHotelManagement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DbHotelManagement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DbHotelManagement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DbHotelManagement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DbHotelManagement] SET ARITHABORT OFF 
GO
ALTER DATABASE [DbHotelManagement] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DbHotelManagement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DbHotelManagement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DbHotelManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DbHotelManagement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DbHotelManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DbHotelManagement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DbHotelManagement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DbHotelManagement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DbHotelManagement] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DbHotelManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DbHotelManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DbHotelManagement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DbHotelManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DbHotelManagement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DbHotelManagement] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DbHotelManagement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DbHotelManagement] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DbHotelManagement] SET  MULTI_USER 
GO
ALTER DATABASE [DbHotelManagement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DbHotelManagement] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DbHotelManagement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DbHotelManagement] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DbHotelManagement] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DbHotelManagement] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [DbHotelManagement] SET QUERY_STORE = OFF
GO
USE [DbHotelManagement]
GO
/****** Object:  Table [dbo].[Bookings]    Script Date: 1/14/2021 6:33:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bookings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BookingDate] [date] NOT NULL,
	[RoomId] [int] NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_Bookings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Hotels]    Script Date: 1/14/2021 6:33:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hotels](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[HotelName] [nvarchar](100) NOT NULL,
	[City] [nvarchar](50) NOT NULL,
	[Pincode] [int] NOT NULL,
	[ContactPerson] [nvarchar](50) NOT NULL,
	[ContactNumber] [nvarchar](15) NOT NULL,
	[Website] [nvarchar](max) NULL,
	[Facebook] [nvarchar](max) NULL,
	[Twitter] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_Hotels] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Room]    Script Date: 1/14/2021 6:33:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Room](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[HotelId] [int] NOT NULL,
	[RoomName] [nvarchar](50) NOT NULL,
	[RoomCategory] [int] NOT NULL,
	[RoomPrice] [float] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Bookings]  WITH CHECK ADD  CONSTRAINT [FK_Bookings_Room_RoomId] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([Id])
GO
ALTER TABLE [dbo].[Bookings] CHECK CONSTRAINT [FK_Bookings_Room_RoomId]
GO
ALTER TABLE [dbo].[Room]  WITH CHECK ADD  CONSTRAINT [FK_Room_Hotels_HotelId] FOREIGN KEY([HotelId])
REFERENCES [dbo].[Hotels] ([Id])
GO
ALTER TABLE [dbo].[Room] CHECK CONSTRAINT [FK_Room_Hotels_HotelId]
GO
USE [master]
GO
ALTER DATABASE [DbHotelManagement] SET  READ_WRITE 
GO
