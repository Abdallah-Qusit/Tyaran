CREATE TABLE MenuCategory (
    MenuCatID INT PRIMARY KEY IDENTITY,

    InActive BIT,
    Cartname NVARCHAR(100),

    RestaurantID INT,

    FOREIGN KEY (RestaurantID) REFERENCES Restaurant(RestaurantID)
);
GO

CREATE PROCEDURE MenuCategory_Create
    @InActive BIT,
    @Cartname NVARCHAR(100),
    @RestaurantID INT
AS
BEGIN
    INSERT INTO MenuCategory (InActive, Cartname, RestaurantID)
    VALUES (@InActive, @Cartname, @RestaurantID);

    SELECT SCOPE_IDENTITY() AS MenuCatID;
END;
GO

CREATE PROCEDURE MenuCategory_Read
    @MenuCatID INT
AS
BEGIN
    SELECT * FROM MenuCategory WHERE MenuCatID = @MenuCatID;
END;
GO

CREATE PROCEDURE MenuCategory_Read_ByRestaurant
    @RestaurantID INT
AS
BEGIN
    SELECT * FROM MenuCategory WHERE RestaurantID = @RestaurantID;
END;
GO

CREATE PROCEDURE MenuCategory_Update
    @MenuCatID INT,
    @InActive BIT,
    @Cartname NVARCHAR(100)
AS
BEGIN
    UPDATE MenuCategory
    SET InActive = @InActive,
        Cartname = @Cartname
    WHERE MenuCatID = @MenuCatID;
END;
GO

CREATE PROCEDURE MenuCategory_Delete
    @MenuCatID INT
AS
BEGIN
    DELETE FROM MenuCategory WHERE MenuCatID = @MenuCatID;
END;
GO