CREATE TABLE MenuItem (
    ItemID INT PRIMARY KEY IDENTITY,

    ImageUrl NVARCHAR(255),
    Name NVARCHAR(100),
    Description NVARCHAR(255),
    Price DECIMAL(10,2),

    PreparationTime INT,

    IsAvailable BIT,
    CreatedAt DATETIME DEFAULT GETDATE(),

    MenuCatID INT,

    FOREIGN KEY (MenuCatID) REFERENCES MenuCategory(MenuCatID)
);
GO

CREATE PROCEDURE MenuItem_Create
    @ImageUrl NVARCHAR(255),
    @Name NVARCHAR(100),
    @Description NVARCHAR(255),
    @Price DECIMAL(10,2),
    @PreparationTime INT,
    @MenuCatID INT
AS
BEGIN
    INSERT INTO MenuItem (ImageUrl, Name, Description, Price, PreparationTime, MenuCatID)
    VALUES (@ImageUrl, @Name, @Description, @Price, @PreparationTime, @MenuCatID);

    SELECT SCOPE_IDENTITY() AS ItemID;
END;
GO

CREATE PROCEDURE MenuItem_Read
    @ItemID INT
AS
BEGIN
    SELECT * FROM MenuItem WHERE ItemID = @ItemID;
END;
GO

CREATE PROCEDURE MenuItem_Read_ByRestaurant
    @RestaurantID INT
AS
BEGIN
    SELECT MI.*
    FROM MenuItem MI
    JOIN MenuCategory MC ON MI.MenuCatID = MC.MenuCatID
    WHERE MC.RestaurantID = @RestaurantID;
END;
GO

CREATE PROCEDURE MenuItem_Update
    @ItemID INT,
    @ImageUrl NVARCHAR(255),
    @Name NVARCHAR(100),
    @Description NVARCHAR(255),
    @Price DECIMAL(10,2),
    @PreparationTime INT,
    @IsAvailable BIT
AS
BEGIN
    UPDATE MenuItem
    SET ImageUrl = @ImageUrl,
        Name = @Name,
        Description = @Description,
        Price = @Price,
        PreparationTime = @PreparationTime,
        IsAvailable = @IsAvailable
    WHERE ItemID = @ItemID;
END;
GO

CREATE PROCEDURE MenuItem_Delete
    @ItemID INT
AS
BEGIN
    DELETE FROM MenuItem WHERE ItemID = @ItemID;
END;
GO