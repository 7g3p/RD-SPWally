use wallysworld;

SELECT * FROM `orders` WHERE `ItemID` = 2 AND `AuthorizationStatus` = TRUE ORDER BY `ItemQuantity` DESC limit 1;

