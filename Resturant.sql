CREATE TABLE Restaurant (
    RestaurantID INT PRIMARY KEY IDENTITY,
    OwnerID INT,
    Name NVARCHAR(100),
    Description NVARCHAR(255),
    AddressID INT,
    LogoUrl NVARCHAR(255),
    CoverImageUrl NVARCHAR(255),
    OpeningTime TIME,
    ClosingTime TIME,
    Rating FLOAT,
    IsActive BIT,
    CreatedAt DATETIME DEFAULT GETDATE(),
    DeliveryFee DECIMAL(10,2),
    Review NVARCHAR(255),
    FOREIGN KEY (OwnerID) REFERENCES [User](UserID),
    FOREIGN KEY (AddressID) REFERENCES Addresses(AddressID)
);
GO
CREATE PROCEDURE Restaurant_Create
    @OwnerID INT,
    @Name NVARCHAR(100),
    @AddressID INT
AS
BEGIN
    INSERT INTO Restaurant (OwnerID, Name, AddressID)
    VALUES (@OwnerID, @Name, @AddressID);
    SELECT SCOPE_IDENTITY() AS RestaurantID;
END;
GO
CREATE PROCEDURE Restaurant_Read
    @RestaurantID INT
AS
BEGIN
    SELECT * FROM Restaurant
    WHERE RestaurantID = @RestaurantID;
END;
GO
CREATE PROCEDURE Restaurant_Update
    @RestaurantID INT,
    @Name NVARCHAR(100),
    @IsActive BIT
AS
BEGIN
    UPDATE Restaurant
    SET Name = @Name,
        IsActive = @IsActive
    WHERE RestaurantID = @RestaurantID;
END;
GO
CREATE PROCEDURE sp_Restaurant_Delete
    @RestaurantID INT
AS
BEGIN
    DELETE FROM Restaurant
    WHERE RestaurantID = @RestaurantID;
END;