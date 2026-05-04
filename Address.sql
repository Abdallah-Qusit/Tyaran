CREATE TABLE Addresses (
    AddressID INT PRIMARY KEY IDENTITY,
    UserID INT,
    Street NVARCHAR(100),
    Building NVARCHAR(50),
    Floor NVARCHAR(20),
    Apartment NVARCHAR(20),
    City NVARCHAR(50),
    Latitude FLOAT,
    Longitude FLOAT,
    IsDefault BIT,
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES [User](UserID)
);
GO
CREATE PROCEDURE Address_Create
    @UserID INT,
    @City NVARCHAR(50)
AS
BEGIN
    INSERT INTO Addresses (UserID, City)
    VALUES (@UserID, @City);

    SELECT SCOPE_IDENTITY() AS AddressID;
END;
GO
CREATE PROCEDURE Address_Read
    @AddressID INT
AS
BEGIN
    SELECT * FROM Addresses WHERE AddressID = @AddressID;
END;
GO
CREATE PROCEDURE Address_Update
    @AddressID INT,
    @City NVARCHAR(50)
AS
BEGIN
    UPDATE Addresses
    SET City = @City
    WHERE AddressID = @AddressID;
END;
GO
CREATE PROCEDURE Address_Delete
    @AddressID INT
AS
BEGIN
    DELETE FROM Addresses WHERE AddressID = @AddressID;
END;