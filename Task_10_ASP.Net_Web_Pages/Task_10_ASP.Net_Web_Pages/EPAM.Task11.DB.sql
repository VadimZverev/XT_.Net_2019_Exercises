USE [master]
GO
/****** Object:  Database [EPAM.Task11.DB]    Script Date: 21.09.2019 5:34:12 ******/
CREATE DATABASE [EPAM.Task11.DB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'EPAM.Task11.DB', FILENAME = N'EPAM.Task11.DB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'EPAM.Task11.DB_log', FILENAME = N'EPAM.Task11.DB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [EPAM.Task11.DB] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EPAM.Task11.DB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EPAM.Task11.DB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EPAM.Task11.DB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EPAM.Task11.DB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EPAM.Task11.DB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EPAM.Task11.DB] SET ARITHABORT OFF 
GO
ALTER DATABASE [EPAM.Task11.DB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [EPAM.Task11.DB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EPAM.Task11.DB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EPAM.Task11.DB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EPAM.Task11.DB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [EPAM.Task11.DB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EPAM.Task11.DB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EPAM.Task11.DB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EPAM.Task11.DB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EPAM.Task11.DB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [EPAM.Task11.DB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EPAM.Task11.DB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EPAM.Task11.DB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [EPAM.Task11.DB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [EPAM.Task11.DB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EPAM.Task11.DB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [EPAM.Task11.DB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [EPAM.Task11.DB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [EPAM.Task11.DB] SET  MULTI_USER 
GO
ALTER DATABASE [EPAM.Task11.DB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EPAM.Task11.DB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [EPAM.Task11.DB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [EPAM.Task11.DB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [EPAM.Task11.DB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [EPAM.Task11.DB] SET QUERY_STORE = OFF
GO
USE [EPAM.Task11.DB]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [EPAM.Task11.DB]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 21.09.2019 5:34:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Login] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Role] [int] NOT NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Awards]    Script Date: 21.09.2019 5:34:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Awards](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Image] [varbinary](max) NULL,
 CONSTRAINT [PK_Award] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AwardUser]    Script Date: 21.09.2019 5:34:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AwardUser](
	[UserId] [int] NOT NULL,
	[AwardId] [int] NOT NULL,
 CONSTRAINT [PK_AwardUser] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[AwardId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 21.09.2019 5:34:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[Age] [int] NOT NULL,
	[Image] [varbinary](max) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[AwardUser]  WITH CHECK ADD  CONSTRAINT [FK_AwardUser_Award] FOREIGN KEY([AwardId])
REFERENCES [dbo].[Awards] ([Id])
GO
ALTER TABLE [dbo].[AwardUser] CHECK CONSTRAINT [FK_AwardUser_Award]
GO
ALTER TABLE [dbo].[AwardUser]  WITH CHECK ADD  CONSTRAINT [FK_AwardUser_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[AwardUser] CHECK CONSTRAINT [FK_AwardUser_User]
GO
/****** Object:  StoredProcedure [dbo].[AddAccount]    Script Date: 21.09.2019 5:34:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddAccount]
	-- Add the parameters for the stored procedure here
	@Login NVARCHAR(MAX),
	@Password NVARCHAR(MAX),
	@Role INT
AS
BEGIN
	INSERT INTO Accounts([Login], [Password], [Role])
	VALUES (@Login, @Password, @Role)
END
GO
/****** Object:  StoredProcedure [dbo].[AddAward]    Script Date: 21.09.2019 5:34:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[AddAward]
	-- Add the parameters for the stored procedure here
	@Title NVARCHAR(MAX),
	@Image VARBINARY(MAX)
AS
BEGIN
	INSERT INTO Awards([Title], [Image])
	VALUES (@Title, @Image)
END
GO
/****** Object:  StoredProcedure [dbo].[AddAwardUser]    Script Date: 21.09.2019 5:34:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddAwardUser]
	-- Add the parameters for the stored procedure here
	@AwardId INT,
	@UserId INT
AS
BEGIN
	INSERT INTO AwardUser([AwardId], [UserId])
	VALUES (@AwardId, @UserId)
END
GO
/****** Object:  StoredProcedure [dbo].[AddUser]    Script Date: 21.09.2019 5:34:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddUser]
	-- Add the parameters for the stored procedure here
	@Name NVARCHAR(60),
	@DateOfBirth DATE,
	@Age INT,
	@Image VARBINARY(MAX)
AS
BEGIN
	INSERT INTO Users([Name], [DateOfBirth], [Age], [Image])
	VALUES (@Name, @DateOfBirth, @Age, @Image)
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteAccount]    Script Date: 21.09.2019 5:34:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteAccount]
	@id INT
AS
BEGIN
	DELETE FROM Accounts WHERE Id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteAward]    Script Date: 21.09.2019 5:34:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteAward]
	@id INT
AS
BEGIN
	DELETE FROM Awards WHERE Id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteAwardUser]    Script Date: 21.09.2019 5:34:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteAwardUser]
	@awardId INT,
	@userId INT
AS
BEGIN
	DELETE FROM AwardUser WHERE AwardId = @awardId AND UserId = @userId
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteUser]    Script Date: 21.09.2019 5:34:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteUser]
	@id INT
AS
BEGIN
	DELETE FROM Users WHERE Id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[GetAccountById]    Script Date: 21.09.2019 5:34:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAccountById]
	@id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM Accounts
	WHERE Id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[GetAccounts]    Script Date: 21.09.2019 5:34:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAccounts]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM Accounts
END
GO
/****** Object:  StoredProcedure [dbo].[GetAwardById]    Script Date: 21.09.2019 5:34:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAwardById]
	@id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM Awards WHERE ID = @id
END
GO
/****** Object:  StoredProcedure [dbo].[GetAwards]    Script Date: 21.09.2019 5:34:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAwards]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM Awards;
END
GO
/****** Object:  StoredProcedure [dbo].[GetAwardUser]    Script Date: 21.09.2019 5:34:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAwardUser]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * From AwardUser
END
GO
/****** Object:  StoredProcedure [dbo].[GetAwardUserByIds]    Script Date: 21.09.2019 5:34:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAwardUserByIds]
	@awardId INT,
	@userId INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM AwardUser 
	WHERE AwardId = @awardId AND UserId = @userId
END
GO
/****** Object:  StoredProcedure [dbo].[GetUserById]    Script Date: 21.09.2019 5:34:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetUserById]
	@id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM Users WHERE ID = @id
END
GO
/****** Object:  StoredProcedure [dbo].[GetUsers]    Script Date: 21.09.2019 5:34:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetUsers]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM Users
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateAccountById]    Script Date: 21.09.2019 5:34:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateAccountById]
	@id INT,
	@login NVARCHAR(MAX),
	@password NVARCHAR(MAX),
	@role INT
AS
BEGIN 
	
	UPDATE Accounts
	SET
		[Login] = @login,
		[Password] = @password,
		[Role] = @role
	WHERE Id = @id;

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateAwardById]    Script Date: 21.09.2019 5:34:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateAwardById]
	@id INT,
	@title NVARCHAR(MAX),
	@image VARBINARY(MAX)
AS
BEGIN 
	
	UPDATE Awards 
	SET
		[Title] = @title,
		[Image] = @image
	WHERE Id = @id;

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateUserById]    Script Date: 21.09.2019 5:34:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateUserById]
	@id INT,
	@name NVARCHAR(50),
	@dateOfBirth DATE,
	@age INT,
	@image VARBINARY(MAX)
AS
BEGIN 
	
	UPDATE Users 
	SET
		[Name] = @name,
		[DateOfBirth] = @dateOfBirth,
		[Age] = @age,
		[Image] = @image
	WHERE Id = @id;

END
GO
USE [master]
GO
ALTER DATABASE [EPAM.Task11.DB] SET  READ_WRITE 
GO
