CREATE TABLE User_Phone (
    UserID INT,
    PhoneNumber NVARCHAR(20),
    PRIMARY KEY (UserID, PhoneNumber),
    FOREIGN KEY (UserID) REFERENCES [User](UserID)
);
GO
CREATE PROCEDURE UserPhone_Create
    @UserID INT,
    @PhoneNumber NVARCHAR(20)
AS
BEGIN
    INSERT INTO User_Phone (UserID, PhoneNumber)
    VALUES (@UserID, @PhoneNumber);
END;
GO
CREATE PROCEDURE UserPhone_Read
    @UserID INT
AS
BEGIN
    SELECT UserID, PhoneNumber
    FROM User_Phone
    WHERE UserID = @UserID;
END;
GO
CREATE PROCEDURE UserPhone_Update
    @UserID INT,
    @NewPhone NVARCHAR(20)
AS
BEGIN
    UPDATE User_Phone
    SET PhoneNumber = @NewPhone
    WHERE UserID = @UserID
END;
GO
CREATE PROCEDURE UserPhone_Delete
    @UserID INT
AS
BEGIN
    DELETE FROM User_Phone
    WHERE UserID = @UserID
END;
GO