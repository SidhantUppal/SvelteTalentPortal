USE [master]
GO
/****** Object:  Database [talentportal]    Script Date: 07-03-2024 10:52:18 ******/
CREATE DATABASE [talentportal]
 CONTAINMENT = NONE

ALTER DATABASE [talentportal] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [talentportal].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [talentportal] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [talentportal] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [talentportal] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [talentportal] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [talentportal] SET ARITHABORT OFF 
GO
ALTER DATABASE [talentportal] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [talentportal] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [talentportal] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [talentportal] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [talentportal] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [talentportal] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [talentportal] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [talentportal] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [talentportal] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [talentportal] SET  DISABLE_BROKER 
GO
ALTER DATABASE [talentportal] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [talentportal] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [talentportal] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [talentportal] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [talentportal] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [talentportal] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [talentportal] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [talentportal] SET RECOVERY FULL 
GO
ALTER DATABASE [talentportal] SET  MULTI_USER 
GO
ALTER DATABASE [talentportal] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [talentportal] SET DB_CHAINING OFF 
GO
ALTER DATABASE [talentportal] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [talentportal] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [talentportal] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [talentportal] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'talentportal', N'ON'
GO
ALTER DATABASE [talentportal] SET QUERY_STORE = ON
GO
ALTER DATABASE [talentportal] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [talentportal]
GO
/****** Object:  Table [dbo].[AttendanceLog]    Script Date: 07-03-2024 10:52:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AttendanceLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TimeIn] [datetime] NOT NULL,
	[TimeOut] [datetime] NULL,
	[TotalWorkingHours] [time](7) NULL,
	[Status] [varchar](50) NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdatedAt] [datetime] NULL,
	[UserId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DsrLog]    Script Date: 07-03-2024 10:52:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DsrLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[DsrIds] [nvarchar](4000) NOT NULL,
	[SenderIds] [nvarchar](4000) NOT NULL,
	[SenderCCIds] [nvarchar](4000) NOT NULL,
	[SubmitDate] [datetime] NOT NULL,
	[Status] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Dsrs]    Script Date: 07-03-2024 10:52:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dsrs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[FromTime] [datetime] NOT NULL,
	[ToTime] [datetime] NOT NULL,
	[Description] [varchar](5000) NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdatedAt] [datetime] NOT NULL,
	[UserId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Leaves]    Script Date: 07-03-2024 10:52:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Leaves](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LeaveTypeId] [int] NOT NULL,
	[FromDate] [datetime] NOT NULL,
	[ToDate] [datetime] NOT NULL,
	[Reason] [nvarchar](4000) NOT NULL,
	[SenderEmailIds] [nvarchar](4000) NOT NULL,
	[SenderCCEmailIds] [nvarchar](4000) NOT NULL,
	[UserId] [int] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdatedAt] [datetime] NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LeaveTypes]    Script Date: 07-03-2024 10:52:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeaveTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectMapping]    Script Date: 07-03-2024 10:52:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectMapping](
	[ProjectId] [int] NOT NULL,
	[UserId] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Projects]    Script Date: 07-03-2024 10:52:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Projects](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[Description] [varchar](500) NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reports]    Script Date: 07-03-2024 10:52:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reports](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[Description] [nvarchar](4000) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdatedAt] [datetime] NOT NULL,
	[UserId] [int] NOT NULL,
	[SenderEmailIds] [nvarchar](400) NOT NULL,
	[SenderCCEmailIds] [nvarchar](4000) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 07-03-2024 10:52:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](255) NULL,
	[LastName] [varchar](255) NULL,
	[Username] [varchar](255) NULL,
	[Salt] [varchar](255) NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
	[Email] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AttendanceLog] ON 

INSERT [dbo].[AttendanceLog] ([Id], [TimeIn], [TimeOut], [TotalWorkingHours], [Status], [CreatedAt], [UpdatedAt], [UserId]) VALUES (11, CAST(N'2024-02-15T18:21:41.460' AS DateTime), CAST(N'2024-02-15T19:00:55.650' AS DateTime), CAST(N'00:39:14.1900655' AS Time), N'Absent', CAST(N'2024-02-15T18:21:41.460' AS DateTime), NULL, 1)
INSERT [dbo].[AttendanceLog] ([Id], [TimeIn], [TimeOut], [TotalWorkingHours], [Status], [CreatedAt], [UpdatedAt], [UserId]) VALUES (12, CAST(N'2024-02-16T09:23:49.557' AS DateTime), NULL, NULL, N'Working', CAST(N'2024-02-16T09:23:49.573' AS DateTime), NULL, 1)
INSERT [dbo].[AttendanceLog] ([Id], [TimeIn], [TimeOut], [TotalWorkingHours], [Status], [CreatedAt], [UpdatedAt], [UserId]) VALUES (1009, CAST(N'2024-02-22T09:51:53.757' AS DateTime), NULL, NULL, N'Working', CAST(N'2024-02-22T09:51:53.783' AS DateTime), NULL, 1)
INSERT [dbo].[AttendanceLog] ([Id], [TimeIn], [TimeOut], [TotalWorkingHours], [Status], [CreatedAt], [UpdatedAt], [UserId]) VALUES (1010, CAST(N'2024-03-06T11:23:36.917' AS DateTime), NULL, NULL, N'Working', CAST(N'2024-03-06T11:23:36.933' AS DateTime), NULL, 1)
SET IDENTITY_INSERT [dbo].[AttendanceLog] OFF
GO
SET IDENTITY_INSERT [dbo].[DsrLog] ON 

INSERT [dbo].[DsrLog] ([Id], [UserId], [DsrIds], [SenderIds], [SenderCCIds], [SubmitDate], [Status]) VALUES (10, 1, N'32,33,34', N'3,4', N'3,4', CAST(N'2024-02-15T19:00:49.660' AS DateTime), N'')
SET IDENTITY_INSERT [dbo].[DsrLog] OFF
GO
SET IDENTITY_INSERT [dbo].[Dsrs] ON 

INSERT [dbo].[Dsrs] ([Id], [ProjectId], [FromTime], [ToTime], [Description], [CreatedAt], [UpdatedAt], [UserId]) VALUES (25, 1, CAST(N'2024-02-13T03:33:15.690' AS DateTime), CAST(N'2024-02-13T09:39:18.960' AS DateTime), N'this is ease of learning ', CAST(N'2024-02-13T13:32:22.487' AS DateTime), CAST(N'2024-02-13T15:06:54.520' AS DateTime), 1)
INSERT [dbo].[Dsrs] ([Id], [ProjectId], [FromTime], [ToTime], [Description], [CreatedAt], [UpdatedAt], [UserId]) VALUES (26, 2, CAST(N'2024-02-13T15:37:25.383' AS DateTime), CAST(N'2024-02-13T18:40:25.383' AS DateTime), N'this is project for partial dynamics', CAST(N'2024-02-13T13:35:27.887' AS DateTime), CAST(N'2024-02-13T15:06:56.047' AS DateTime), 1)
INSERT [dbo].[Dsrs] ([Id], [ProjectId], [FromTime], [ToTime], [Description], [CreatedAt], [UpdatedAt], [UserId]) VALUES (27, 1, CAST(N'2024-02-13T15:51:11.207' AS DateTime), CAST(N'2024-02-13T17:53:11.207' AS DateTime), N'this is events', CAST(N'2024-02-13T13:49:12.450' AS DateTime), CAST(N'2024-02-13T15:06:56.797' AS DateTime), 1)
INSERT [dbo].[Dsrs] ([Id], [ProjectId], [FromTime], [ToTime], [Description], [CreatedAt], [UpdatedAt], [UserId]) VALUES (28, 1, CAST(N'2024-02-13T19:36:58.450' AS DateTime), CAST(N'2024-02-13T23:40:58.450' AS DateTime), N'fjsdlkfjsdlkfsdlk ', CAST(N'2024-02-13T18:35:58.480' AS DateTime), CAST(N'2024-02-13T18:35:58.480' AS DateTime), 1)
INSERT [dbo].[Dsrs] ([Id], [ProjectId], [FromTime], [ToTime], [Description], [CreatedAt], [UpdatedAt], [UserId]) VALUES (29, 2, CAST(N'2024-02-13T19:37:06.890' AS DateTime), CAST(N'2024-02-13T20:38:06.890' AS DateTime), N'fdsfsdf', CAST(N'2024-02-13T18:36:06.893' AS DateTime), CAST(N'2024-02-13T18:36:06.893' AS DateTime), 1)
INSERT [dbo].[Dsrs] ([Id], [ProjectId], [FromTime], [ToTime], [Description], [CreatedAt], [UpdatedAt], [UserId]) VALUES (30, 1, CAST(N'2024-02-14T10:02:41.790' AS DateTime), CAST(N'2024-02-14T11:03:41.790' AS DateTime), N'this is today dsr', CAST(N'2024-02-14T10:02:41.807' AS DateTime), CAST(N'2024-02-14T10:02:41.807' AS DateTime), 1)
INSERT [dbo].[Dsrs] ([Id], [ProjectId], [FromTime], [ToTime], [Description], [CreatedAt], [UpdatedAt], [UserId]) VALUES (31, 1, CAST(N'2024-02-14T13:40:46.230' AS DateTime), CAST(N'2024-02-14T15:42:46.230' AS DateTime), N'this is current dsr', CAST(N'2024-02-14T11:38:46.240' AS DateTime), CAST(N'2024-02-14T16:56:39.840' AS DateTime), 1)
INSERT [dbo].[Dsrs] ([Id], [ProjectId], [FromTime], [ToTime], [Description], [CreatedAt], [UpdatedAt], [UserId]) VALUES (32, 1, CAST(N'2024-02-15T12:01:02.407' AS DateTime), CAST(N'2024-02-15T13:03:02.407' AS DateTime), N'this is updated data', CAST(N'2024-02-15T09:59:02.420' AS DateTime), CAST(N'2024-02-15T14:42:56.643' AS DateTime), 1)
INSERT [dbo].[Dsrs] ([Id], [ProjectId], [FromTime], [ToTime], [Description], [CreatedAt], [UpdatedAt], [UserId]) VALUES (33, 1, CAST(N'2024-02-15T16:51:54.060' AS DateTime), CAST(N'2024-02-15T14:49:54.060' AS DateTime), N'this is defined way ', CAST(N'2024-02-15T13:48:54.073' AS DateTime), CAST(N'2024-02-15T13:48:54.073' AS DateTime), 1)
INSERT [dbo].[Dsrs] ([Id], [ProjectId], [FromTime], [ToTime], [Description], [CreatedAt], [UpdatedAt], [UserId]) VALUES (34, 1, CAST(N'2024-02-15T17:46:09.440' AS DateTime), CAST(N'2024-02-15T19:48:09.440' AS DateTime), N'this is dynamics data', CAST(N'2024-02-15T14:43:09.447' AS DateTime), CAST(N'2024-02-15T14:43:09.447' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Dsrs] OFF
GO
SET IDENTITY_INSERT [dbo].[Leaves] ON 

INSERT [dbo].[Leaves] ([Id], [LeaveTypeId], [FromDate], [ToDate], [Reason], [SenderEmailIds], [SenderCCEmailIds], [UserId], [CreatedAt], [UpdatedAt], [Status]) VALUES (1, 2, CAST(N'2024-02-14T00:00:00.000' AS DateTime), CAST(N'2024-02-15T00:00:00.000' AS DateTime), N'this is simple reason', N'4', N'3', 1, CAST(N'2024-02-14T11:28:49.310' AS DateTime), CAST(N'2024-02-14T11:28:49.310' AS DateTime), N'Pending')
INSERT [dbo].[Leaves] ([Id], [LeaveTypeId], [FromDate], [ToDate], [Reason], [SenderEmailIds], [SenderCCEmailIds], [UserId], [CreatedAt], [UpdatedAt], [Status]) VALUES (2, 2, CAST(N'2024-02-17T00:00:00.000' AS DateTime), CAST(N'2024-02-17T00:00:00.000' AS DateTime), N'fdfsdfs', N'4,3', N'3,4', 1, CAST(N'2024-02-14T13:51:59.173' AS DateTime), CAST(N'2024-02-14T13:51:59.173' AS DateTime), N'Pending')
INSERT [dbo].[Leaves] ([Id], [LeaveTypeId], [FromDate], [ToDate], [Reason], [SenderEmailIds], [SenderCCEmailIds], [UserId], [CreatedAt], [UpdatedAt], [Status]) VALUES (3, 4, CAST(N'2024-02-15T00:00:00.000' AS DateTime), CAST(N'2024-03-09T00:00:00.000' AS DateTime), N'long work', N'4', N'3', 1, CAST(N'2024-02-14T15:56:47.253' AS DateTime), CAST(N'2024-02-14T15:56:47.253' AS DateTime), N'Pending')
SET IDENTITY_INSERT [dbo].[Leaves] OFF
GO
SET IDENTITY_INSERT [dbo].[LeaveTypes] ON 

INSERT [dbo].[LeaveTypes] ([Id], [Name]) VALUES (1, N'Full Day')
INSERT [dbo].[LeaveTypes] ([Id], [Name]) VALUES (2, N'Half Day')
INSERT [dbo].[LeaveTypes] ([Id], [Name]) VALUES (3, N'Short Leave')
INSERT [dbo].[LeaveTypes] ([Id], [Name]) VALUES (4, N'Work From Home')
SET IDENTITY_INSERT [dbo].[LeaveTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[Projects] ON 

INSERT [dbo].[Projects] ([Id], [Name], [Description], [CreatedAt], [UpdatedAt]) VALUES (1, N'Ease Of Learning', N'This is project structural information', CAST(N'2024-02-13T04:45:22.153' AS DateTime), CAST(N'2024-02-13T04:45:22.153' AS DateTime))
INSERT [dbo].[Projects] ([Id], [Name], [Description], [CreatedAt], [UpdatedAt]) VALUES (2, N'Partial Dynamics', N'This is project for dynamics', CAST(N'2024-02-13T04:45:46.103' AS DateTime), CAST(N'2024-02-13T04:45:46.103' AS DateTime))
INSERT [dbo].[Projects] ([Id], [Name], [Description], [CreatedAt], [UpdatedAt]) VALUES (3, N'Business Central', N'This is project for business central', CAST(N'2024-02-13T04:46:00.117' AS DateTime), CAST(N'2024-02-13T04:46:00.117' AS DateTime))
SET IDENTITY_INSERT [dbo].[Projects] OFF
GO
SET IDENTITY_INSERT [dbo].[Reports] ON 

INSERT [dbo].[Reports] ([Id], [ProjectId], [Description], [CreatedAt], [UpdatedAt], [UserId], [SenderEmailIds], [SenderCCEmailIds]) VALUES (1, 2, N'jkljjlklk', CAST(N'2024-02-14T13:12:00.753' AS DateTime), CAST(N'2024-02-14T13:12:00.753' AS DateTime), 1, N'3,4', N'3,4')
INSERT [dbo].[Reports] ([Id], [ProjectId], [Description], [CreatedAt], [UpdatedAt], [UserId], [SenderEmailIds], [SenderCCEmailIds]) VALUES (2, 2, N'this is my partial css', CAST(N'2024-02-14T18:15:05.300' AS DateTime), CAST(N'2024-02-14T18:15:05.300' AS DateTime), 1, N'3,4', N'3,4')
SET IDENTITY_INSERT [dbo].[Reports] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [FirstName], [LastName], [Username], [Salt], [CreatedAt], [UpdatedAt], [Email]) VALUES (1, N'Karley', N'Espinoza', N'karleyEspinoza@mail.com', N'7EB3534E9438DA2F0A6A8AA8CD4D51FD0165EAE8D4E03D8509E86B84A5237247:514953C7E3D48C6CF1FE0417012CE91A:50000:SHA256', CAST(N'2024-02-12T15:39:51.737' AS DateTime), CAST(N'2024-02-12T15:39:51.737' AS DateTime), N'karleyEspinoza@mail.com')
INSERT [dbo].[Users] ([Id], [FirstName], [LastName], [Username], [Salt], [CreatedAt], [UpdatedAt], [Email]) VALUES (3, N'User1', N'User1', N'user1@mail.com', N'7EB3534E9438DA2F0A6A8AA8CD4D51FD0165EAE8D4E03D8509E86B84A5237247:514953C7E3D48C6CF1FE0417012CE91A:50000:SHA256', CAST(N'2024-02-13T14:52:28.120' AS DateTime), CAST(N'2024-02-13T14:52:28.120' AS DateTime), N'user1@mail.com')
INSERT [dbo].[Users] ([Id], [FirstName], [LastName], [Username], [Salt], [CreatedAt], [UpdatedAt], [Email]) VALUES (4, N'user3', N'user3', N'user3', N'F37646FBED82D3C2617BAB6CF0E7C12465A81EB7338524BE59EEF2710D568AC8:2A38025A57E8BB03D6780A8D26A5BA22:50000:SHA256', CAST(N'2024-02-13T15:04:10.573' AS DateTime), CAST(N'2024-02-13T15:04:10.573' AS DateTime), N'user3@mail.com')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[AttendanceLog] ADD  DEFAULT (getdate()) FOR [TimeIn]
GO
ALTER TABLE [dbo].[AttendanceLog] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[AttendanceLog] ADD  DEFAULT ((0)) FOR [UserId]
GO
ALTER TABLE [dbo].[DsrLog] ADD  DEFAULT ((0)) FOR [UserId]
GO
ALTER TABLE [dbo].[DsrLog] ADD  DEFAULT (getdate()) FOR [SubmitDate]
GO
ALTER TABLE [dbo].[DsrLog] ADD  DEFAULT ('') FOR [Status]
GO
ALTER TABLE [dbo].[Dsrs] ADD  DEFAULT ((0)) FOR [ProjectId]
GO
ALTER TABLE [dbo].[Dsrs] ADD  DEFAULT (getutcdate()) FOR [FromTime]
GO
ALTER TABLE [dbo].[Dsrs] ADD  DEFAULT (getutcdate()) FOR [ToTime]
GO
ALTER TABLE [dbo].[Dsrs] ADD  DEFAULT (getutcdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Dsrs] ADD  DEFAULT (getutcdate()) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[Dsrs] ADD  DEFAULT ((0)) FOR [UserId]
GO
ALTER TABLE [dbo].[Leaves] ADD  DEFAULT ((0)) FOR [LeaveTypeId]
GO
ALTER TABLE [dbo].[Leaves] ADD  DEFAULT (getdate()) FOR [FromDate]
GO
ALTER TABLE [dbo].[Leaves] ADD  DEFAULT (getdate()) FOR [ToDate]
GO
ALTER TABLE [dbo].[Leaves] ADD  DEFAULT ('') FOR [Reason]
GO
ALTER TABLE [dbo].[Leaves] ADD  DEFAULT ('') FOR [SenderEmailIds]
GO
ALTER TABLE [dbo].[Leaves] ADD  DEFAULT ('') FOR [SenderCCEmailIds]
GO
ALTER TABLE [dbo].[Leaves] ADD  DEFAULT ((0)) FOR [UserId]
GO
ALTER TABLE [dbo].[Leaves] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Leaves] ADD  DEFAULT (getdate()) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[Leaves] ADD  DEFAULT ('') FOR [Status]
GO
ALTER TABLE [dbo].[LeaveTypes] ADD  DEFAULT ('') FOR [Name]
GO
ALTER TABLE [dbo].[ProjectMapping] ADD  DEFAULT ((0)) FOR [ProjectId]
GO
ALTER TABLE [dbo].[ProjectMapping] ADD  DEFAULT ((0)) FOR [UserId]
GO
ALTER TABLE [dbo].[Projects] ADD  DEFAULT ('') FOR [Name]
GO
ALTER TABLE [dbo].[Projects] ADD  DEFAULT (getutcdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Reports] ADD  DEFAULT ((0)) FOR [ProjectId]
GO
ALTER TABLE [dbo].[Reports] ADD  DEFAULT ('') FOR [Description]
GO
ALTER TABLE [dbo].[Reports] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Reports] ADD  DEFAULT (getdate()) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[Reports] ADD  DEFAULT ((0)) FOR [UserId]
GO
ALTER TABLE [dbo].[Reports] ADD  DEFAULT ('') FOR [SenderEmailIds]
GO
ALTER TABLE [dbo].[Reports] ADD  DEFAULT ('') FOR [SenderCCEmailIds]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getdate()) FOR [UpdatedAt]
GO
USE [master]
GO
ALTER DATABASE [talentportal] SET  READ_WRITE 
GO
