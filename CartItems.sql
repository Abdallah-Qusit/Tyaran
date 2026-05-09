CREATE TABLE CartItems (
    CartItemID INT PRIMARY KEY IDENTITY,

    SpecialInst NVARCHAR(255),
    Quantity INT,

    CartID INT,
    ItemID INT,

    FOREIGN KEY (CartID) REFERENCES Cart(CartID),
    FOREIGN KEY (ItemID) REFERENCES MenuItem(ItemID)
);
GO

CREATE PROCEDURE CartItem_Create
    @SpecialInst NVARCHAR(255),
    @Quantity INT,
    @CartID INT,
    @ItemID INT
AS
BEGIN
    INSERT INTO CartItems (SpecialInst, Quantity, CartID, ItemID)
    VALUES (@SpecialInst, @Quantity, @CartID, @ItemID);

    SELECT SCOPE_IDENTITY() AS CartItemID;
END;
GO

CREATE PROCEDURE CartItem_Read
    @CartItemID INT
AS
BEGIN
    SELECT * FROM CartItems WHERE CartItemID = @CartItemID;
END;
GO

CREATE PROCEDURE CartItem_Update
    @CartItemID INT,
    @SpecialInst NVARCHAR(255),
    @Quantity INT
AS
BEGIN
    UPDATE CartItems
    SET SpecialInst = @SpecialInst,
        Quantity = @Quantity
    WHERE CartItemID = @CartItemID;
END;
GO

CREATE PROCEDURE CartItem_Delete
    @CartItemID INT
AS
BEGIN
    DELETE FROM CartItems WHERE CartItemID = @CartItemID;
END;
GO