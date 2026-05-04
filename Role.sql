CREATE TABLE Role (
    UserID INT,
    role NVARCHAR(50),
    PRIMARY KEY (UserID),
    FOREIGN KEY (UserID) REFERENCES [User](UserID)
);
GO
CREATE PROCEDURE Role_Create
    @UserID INT,
    @role NVARCHAR(50)
AS
BEGIN
    INSERT INTO Role (UserID, role)
    VALUES (@UserID, @role);
END;
GO
CREATE PROCEDURE Role_Read
    @UserID INT
AS
BEGIN
    SELECT UserID, role
    FROM Role
    WHERE UserID = @UserID;
END;
GO
CREATE PROCEDURE Role_Update
    @UserID INT,
    @role NVARCHAR(50)
AS
BEGIN
    UPDATE Role
    SET role = @role
    WHERE UserID = @UserID
END;
GO
CREATE PROCEDURE Role_Delete
    @UserID INT
AS
BEGIN
    DELETE FROM Role
    WHERE UserID = @UserID
END;
GO