USE [master]
GO
/****** Object:  Database [EPAM.SocialNetworkDB]    Script Date: 13.10.2019 21:07:30 ******/
CREATE DATABASE [EPAM.SocialNetworkDB]
GO
ALTER DATABASE [EPAM.SocialNetworkDB] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EPAM.SocialNetworkDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EPAM.SocialNetworkDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EPAM.SocialNetworkDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EPAM.SocialNetworkDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EPAM.SocialNetworkDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EPAM.SocialNetworkDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [EPAM.SocialNetworkDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [EPAM.SocialNetworkDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EPAM.SocialNetworkDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EPAM.SocialNetworkDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EPAM.SocialNetworkDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [EPAM.SocialNetworkDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EPAM.SocialNetworkDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EPAM.SocialNetworkDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EPAM.SocialNetworkDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EPAM.SocialNetworkDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [EPAM.SocialNetworkDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EPAM.SocialNetworkDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EPAM.SocialNetworkDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [EPAM.SocialNetworkDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [EPAM.SocialNetworkDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EPAM.SocialNetworkDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [EPAM.SocialNetworkDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [EPAM.SocialNetworkDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [EPAM.SocialNetworkDB] SET  MULTI_USER 
GO
ALTER DATABASE [EPAM.SocialNetworkDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EPAM.SocialNetworkDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [EPAM.SocialNetworkDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [EPAM.SocialNetworkDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [EPAM.SocialNetworkDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [EPAM.SocialNetworkDB] SET QUERY_STORE = OFF
GO
USE [EPAM.SocialNetworkDB]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [EPAM.SocialNetworkDB]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 13.10.2019 21:07:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Login] [nvarchar](max) NOT NULL,
	[PasswordHash] [nvarchar](max) NOT NULL,
	[ProfileId] [int] NOT NULL,
	[RoleId] [int] NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Friends]    Script Date: 13.10.2019 21:07:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Friends](
	[AccountId] [int] NOT NULL,
	[FriendId] [int] NOT NULL,
	[IsAccept] [bit] NOT NULL,
 CONSTRAINT [PK_Friends] PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC,
	[FriendId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 13.10.2019 21:07:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[AccountFromId] [int] NOT NULL,
	[AccountToId] [int] NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
	[DateOfCreation] [datetime] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Profiles]    Script Date: 13.10.2019 21:07:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Profiles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[MiddleName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[Male] [nvarchar](max) NULL,
	[DateOfBirth] [date] NULL,
	[ProfilePhoto] [varbinary](max) NULL,
	[City] [nvarchar](max) NULL,
 CONSTRAINT [PK_AccountProfiles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 13.10.2019 21:07:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_Profiles] FOREIGN KEY([ProfileId])
REFERENCES [dbo].[Profiles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_Profiles]
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_Roles]
GO
ALTER TABLE [dbo].[Friends]  WITH CHECK ADD  CONSTRAINT [FK_Friends_Accounts] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Accounts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Friends] CHECK CONSTRAINT [FK_Friends_Accounts]
GO
/****** Object:  StoredProcedure [dbo].[AddAccount]    Script Date: 13.10.2019 21:07:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddAccount]
	@Login NVARCHAR(MAX),
	@PasswordHash NVARCHAR(MAX),
	@ProfileId INT,
	@RoleId INT,
	@Id INT OUT
AS
BEGIN
	INSERT INTO Accounts([Login], [PasswordHash], [ProfileId], [RoleId])
	VALUES (@Login, @PasswordHash, @ProfileId, @RoleId)

	SET @Id = @@IDENTITY;
END
GO
/****** Object:  StoredProcedure [dbo].[AddFriend]    Script Date: 13.10.2019 21:07:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddFriend]
	@AccountId INT,
	@FriendId INT,
	@IsAccept BIT,
	@IsAdd BIT OUT
AS
BEGIN
	IF @AccountId <> @FriendId
		BEGIN
			INSERT INTO Friends ([AccountId], [FriendId], [IsAccept])
			VALUES (@AccountId, @FriendId, @IsAccept)

			SET @IsAdd = 1;
		END
	ELSE
		BEGIN
			SET @IsAdd = 0;
		END
END
GO
/****** Object:  StoredProcedure [dbo].[AddMessage]    Script Date: 13.10.2019 21:07:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddMessage]
	@AccountFromId INT,
	@AccountToId INT,
	@Text NVARCHAR(MAX),
	@DateOfCreation DATETIME,
	@IsAdd BIT OUT
AS
BEGIN
	IF @AccountFromId <> @AccountToId
		BEGIN
			INSERT INTO [Messages] ([AccountFromId], [AccountToId], [Text], [DateOfCreation])
			VALUES (@AccountFromId, @AccountToId, @Text, @DateOfCreation);

			SET @IsAdd = 1;
		END
	ELSE
		BEGIN
			SET @IsAdd = 0;
		END
END
GO
/****** Object:  StoredProcedure [dbo].[AddProfile]    Script Date: 13.10.2019 21:07:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddProfile]
	@FirstName NVARCHAR(MAX),
	@MiddleName NVARCHAR(MAX),
	@LastName NVARCHAR(MAX),
	@Male NVARCHAR(MAX),
	@DateOfBirth DATETIME,
	@City NVARCHAR(MAX),
	@ProfilePhoto VARBINARY(MAX),
	@Id INT OUT
AS
BEGIN
	INSERT INTO Profiles([FirstName], [MiddleName], [LastName], [Male], [DateOfBirth], [ProfilePhoto], [City])
	VALUES (@FirstName, @MiddleName, @LastName, @Male, @DateOfBirth, @ProfilePhoto, @City)

	SET @Id = @@IDENTITY;
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteAccount]    Script Date: 13.10.2019 21:07:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteAccount]
	@Id INT
AS
BEGIN
	DELETE FROM Accounts WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteFriend]    Script Date: 13.10.2019 21:07:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteFriend]
	@AccountId INT,
	@FriendId INT,
	@IsDelete BIT OUT
AS
BEGIN
	IF @AccountId <> @FriendId
		BEGIN
			DELETE FROM [Friends] WHERE [AccountId] = @AccountId AND [FriendId] = @FriendId;
			SET @IsDelete = 1;
		END
	ELSE
		BEGIN
			SET @IsDelete = 0;
		END
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteFriendsByAccId]    Script Date: 13.10.2019 21:07:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteFriendsByAccId]
	@AccountId INT
AS
BEGIN
	DELETE FROM [Friends] WHERE [AccountId] = @AccountId OR [FriendId] = @AccountId;
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteMessage]    Script Date: 13.10.2019 21:07:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteMessage]
	@AccountFromId INT,
	@AccountToId INT,
	@IsDelete BIT OUT
AS
BEGIN
	IF @AccountFromId <> @AccountToId
		BEGIN
			DELETE FROM [Messages] WHERE [AccountFromId] = @AccountFromId AND [AccountToId] = @AccountToId;
			SET @IsDelete = 1;
		END
	ELSE
		BEGIN
			SET @IsDelete = 0;
		END
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteMessagesByAccId]    Script Date: 13.10.2019 21:07:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteMessagesByAccId]
	@AccountId INT
AS
BEGIN
	DELETE FROM [Messages] WHERE [AccountFromId] = @AccountId OR [AccountToId] = @AccountId;
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteProfile]    Script Date: 13.10.2019 21:07:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteProfile]
	@Id INT
AS
BEGIN
	DELETE FROM [Profiles] WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteRole]    Script Date: 13.10.2019 21:07:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteRole]
	@Id INT
AS
BEGIN
	DELETE FROM [Roles] WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[GetAccountById]    Script Date: 13.10.2019 21:07:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAccountById]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM Accounts
	WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[GetAccounts]    Script Date: 13.10.2019 21:07:31 ******/
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
/****** Object:  StoredProcedure [dbo].[GetFriendById]    Script Date: 13.10.2019 21:07:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetFriendById]
	@AccountId INT,
	@FriendId INT
AS
BEGIN
	IF @AccountId <> @FriendId
		BEGIN
			SET NOCOUNT ON;
			SELECT * FROM [Friends]
			WHERE [AccountId] = @AccountId AND [FriendId] = @FriendId;
		END
END
GO
/****** Object:  StoredProcedure [dbo].[GetFriends]    Script Date: 13.10.2019 21:07:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetFriends]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM [Friends]
END
GO
/****** Object:  StoredProcedure [dbo].[GetMessages]    Script Date: 13.10.2019 21:07:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetMessages]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM [Messages];
END
GO
/****** Object:  StoredProcedure [dbo].[GetProfileById]    Script Date: 13.10.2019 21:07:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetProfileById]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM Profiles
	WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[GetProfiles]    Script Date: 13.10.2019 21:07:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetProfiles]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM [Profiles]
END
GO
/****** Object:  StoredProcedure [dbo].[GetRoleById]    Script Date: 13.10.2019 21:07:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetRoleById]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM Roles
	WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[GetRoles]    Script Date: 13.10.2019 21:07:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetRoles]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM Roles
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateAccountById]    Script Date: 13.10.2019 21:07:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateAccountById]
	@Id INT,
	@Login NVARCHAR(MAX),
	@PasswordHash NVARCHAR(MAX),
	@ProfileId INT,
	@RoleId INT
AS
BEGIN 
	
	UPDATE Accounts
	SET
		[Login] = @Login,
		[PasswordHash] = @PasswordHash,
		[ProfileId] = @ProfileId,
		[RoleId] = @RoleId
	WHERE [Id] = @Id;

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateFriendById]    Script Date: 13.10.2019 21:07:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateFriendById]
	@AccountId INT,
	@FriendId INT,
	@IsAccept BIT,
	@IsUpdate BIT OUT
AS
BEGIN 
	IF @AccountId <> @FriendId
		BEGIN
			UPDATE [Friends]
			SET
				[AccountId] = @AccountId,
				[FriendId] = @FriendId,
				[IsAccept] = @IsAccept
			WHERE AccountId = @AccountId AND FriendId = @FriendId;

			SET @IsUpdate = 1;
		END
	ELSE
		BEGIN
			SET @IsUpdate = 0;
		END
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateMessageById]    Script Date: 13.10.2019 21:07:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateMessageById]
	@AccountFromId INT,
	@AccountToId INT,
	@Text NVARCHAR(MAX),
	@DateOfCreation DATETIME,
	@IsUpdate BIT OUT
AS
BEGIN 
	IF @AccountFromId <> @AccountToId
		BEGIN
			UPDATE [Messages]
			SET
				[AccountFromId] = @AccountFromId,
				[AccountToId] = @AccountToId,
				[Text] = @Text,
				[DateOfCreation] = @DateOfCreation
			WHERE [AccountFromId] = @AccountFromId AND [AccountToId] = @AccountToId;

			SET @IsUpdate = 1;
		END
	ELSE
		BEGIN
			SET @IsUpdate = 0;
		END
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateProfileById]    Script Date: 13.10.2019 21:07:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateProfileById]
	@Id INT,
	@FirstName NVARCHAR(MAX),
	@MiddleName NVARCHAR(MAX),
	@LastName NVARCHAR(MAX),
	@Male NVARCHAR(MAX),
	@DateOfBirth DATETIME,
	@ProfilePhoto VARBINARY(MAX),
	@City NVARCHAR(MAX)
AS
BEGIN 
	
	UPDATE Profiles
	SET
		[FirstName] = @FirstName,
		[MiddleName] = @MiddleName,
		[LastName] = @LastName,
		[Male] = @Male,
		[DateOfBirth] = @DateOfBirth,
		[ProfilePhoto] = @ProfilePhoto,
		[City] = @City
	WHERE [Id] = @Id;

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateRoleById]    Script Date: 13.10.2019 21:07:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateRoleById]
	@Id INT,
	@Name NVARCHAR(MAX)
AS
BEGIN 
	
	UPDATE Roles
	SET
		[Name] = @Name
	WHERE Id = @id;

END
GO
USE [master]
GO
ALTER DATABASE [EPAM.SocialNetworkDB] SET  READ_WRITE 
GO
