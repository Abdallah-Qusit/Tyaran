CREATE TABLE [User] (
    UserID INT PRIMARY KEY IDENTITY,
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    Email NVARCHAR(100),
    Password NVARCHAR(255),
    CreatedAt DATETIME DEFAULT GETDATE(),
    InActive BIT DEFAULT 0,
    CouponId INT NULL
);
GO
CREATE PROCEDURE User_Create
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @Email NVARCHAR(100),
    @Password NVARCHAR(255)
AS
BEGIN
    INSERT INTO [User] (FirstName, LastName, Email, Password)
    VALUES (@FirstName, @LastName, @Email, @Password);
    SELECT SCOPE_IDENTITY() AS UserID;
END;
GO
CREATE PROCEDURE User_Read
    @UserID INT
AS
BEGIN
    SELECT * FROM [User]
    WHERE UserID = @UserID;
END;
GO
CREATE PROCEDURE User_Update
    @UserID INT,
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @Email NVARCHAR(100)
AS
BEGIN
    UPDATE [User]
    SET FirstName = @FirstName,
        LastName = @LastName,
        Email = @Email
    WHERE UserID = @UserID;
END;
GO
CREATE PROCEDURE User_Delete
    @UserID INT
AS
BEGIN
    DELETE FROM [User]
    WHERE UserID = @UserID;
END;
GO