
IF OBJECT_ID('[dbo].[SJ_UserSelect]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[SJ_UserSelect] 
END 
GO
CREATE PROC [dbo].[SJ_UserSelect] 
    @Id uniqueidentifier
AS 
SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
	
	SELECT [Id], [Email], [Password], [AccountType], [UserType], [RegisteredDateUtc], [IsActivated], [IsLocked], [LockedDateUtc], [ConfirmationCode] 
	FROM   [dbo].[User] 
	WHERE  ([Id] = @Id OR @Id IS NULL) 

SET NOCOUNT OFF 
GO
-----------------------------------------------

IF OBJECT_ID('[dbo].[SJ_UserInsert]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[SJ_UserInsert] 
END 
GO
CREATE PROC [dbo].[SJ_UserInsert] 
    @Id uniqueidentifier,
    @Email nvarchar(225),
    @Password nvarchar(MAX) = NULL,
    @AccountType int,
    @UserType int,
    @RegisteredDateUtc datetime,
    @IsActivated bit,
    @IsLocked bit,
    @LockedDateUtc datetime = NULL,
    @ConfirmationCode nvarchar(MAX) = NULL
AS 
SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[User] ([Id], [Email], [Password], [AccountType], [UserType], [RegisteredDateUtc], [IsActivated], [IsLocked], [LockedDateUtc], [ConfirmationCode])
	SELECT @Id, @Email, @Password, @AccountType, @UserType, @RegisteredDateUtc, @IsActivated, @IsLocked, @LockedDateUtc, @ConfirmationCode
	
	-- Begin Return Select <- do not remove
	SELECT [Id], [Email], [Password], [AccountType], [UserType], [RegisteredDateUtc], [IsActivated], [IsLocked], [LockedDateUtc], [ConfirmationCode]
	FROM   [dbo].[User]
	WHERE  [Id] = @Id
	-- End Return Select <- do not remove
          
	COMMIT 
    
SET NOCOUNT OFF
GO 
----------------------------------------------------

IF OBJECT_ID('[dbo].[SJ_UserUpdate]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[SJ_UserUpdate] 
END 
GO
CREATE PROC [dbo].[SJ_UserUpdate] 
    @Id uniqueidentifier,
    @Email nvarchar(225),
    @Password nvarchar(MAX) = NULL,
    @AccountType int,
    @UserType int,
    @RegisteredDateUtc datetime,
    @IsActivated bit,
    @IsLocked bit,
    @LockedDateUtc datetime = NULL,
    @ConfirmationCode nvarchar(MAX) = NULL
AS 
SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
	
	BEGIN TRAN

	UPDATE [dbo].[User]
	SET    [Id] = @Id, [Email] = @Email, [Password] = @Password, [AccountType] = @AccountType, [UserType] = @UserType, [RegisteredDateUtc] = @RegisteredDateUtc, [IsActivated] = @IsActivated, [IsLocked] = @IsLocked, [LockedDateUtc] = @LockedDateUtc, [ConfirmationCode] = @ConfirmationCode
	WHERE  [Id] = @Id
	
	-- Begin Return Select <- do not remove
	SELECT [Id], [Email], [Password], [AccountType], [UserType], [RegisteredDateUtc], [IsActivated], [IsLocked], [LockedDateUtc], [ConfirmationCode]
	FROM   [dbo].[User]
	WHERE  [Id] = @Id	
	-- End Return Select <- do not remove

	COMMIT

SET NOCOUNT OFF 
GO
-----------------------------------------

IF OBJECT_ID('[dbo].[SJ_UserDelete]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[SJ_UserDelete] 
END 
GO
CREATE PROC [dbo].[SJ_UserDelete] 
    @Id uniqueidentifier
AS 
SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[User]
	WHERE  [Id] = @Id

	COMMIT

SET NOCOUNT OFF 

GO
