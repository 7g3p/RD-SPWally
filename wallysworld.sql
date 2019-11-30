DROP DATABASE IF EXISTS wallysworld;

CREATE DATABASE IF NOT EXISTS wallysworld;

USE wallysworld;

--
-- Definition of table `categories`
--

DROP TABLE IF EXISTS `products`;
CREATE TABLE `products`
(
	`ProductID` int NOT NULL,
	`ProductName` varchar(50) NOT NULL, 
    `wPrice` double(13, 2) NOT NULL,
    `Stock` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*!40000 ALTER TABLE `products` DISABLE KEYS */;
INSERT INTO `products` (`ProductID`,`ProductName`, `wPrice`, `Stock`) VALUES 
 (1, 'Disco Queen Wallpaper (roll)', 12.95, 56),
 (2, 'Countryside Wallpaper (roll)', 11.95, 24),
 (3, 'Drywall Tape (roll)', 3.95, 120),
 (4, 'Drywall Tape (pkg 10)', 36.95, 30),
 (5, 'Drywall Repair Compound (tube)', 6.95, 90),
 (6, 'Victorian Lace Wallpaper (roll)', 14.95, 44);
/*!40000 ALTER TABLE `products` ENABLE KEYS */;


DROP TABLE IF EXISTS `customers`;
CREATE TABLE `customers` (
  `CustomerID` int NOT NULL,
  `FirstName` tinytext NOT NULL,
  `LastName` tinytext NOT NULL,
  `Phone` int(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*!40000 ALTER TABLE `customers` DISABLE KEYS */;
INSERT INTO `customers` (`CustomerID`, `FirstName`, `LastName`, `Phone`) VALUES 
 (1, 'Carlo', 'Sgro', 519-555-0000),
 (2, 'Norbert', 'Mika', 416-555-1111),
 (3, 'Russell', 'Foubert', 519-555-2222),
 (4, 'Sean', 'Clarke', 519-555-3333);
/*!40000 ALTER TABLE `customers` ENABLE KEYS */;

DROP TABLE IF EXISTS `branch`;
CREATE TABLE `branch`
(
	`BranchID` int NOT NULL,
    `BranchName` tinytext DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*!40000 ALTER TABLE `branch` DISABLE KEYS */;
INSERT INTO `branch` (`BranchID`, `BranchName`) VALUES 
 (1, 'Sports World'),
 (2, 'Waterloo'),
 (3, 'Cambridge Mall');
/*!40000 ALTER TABLE `branch` ENABLE KEYS */;


DROP TABLE IF EXISTS `orders`;
CREATE TABLE `orders`
(
	`OrderID` int NOT NULL,
    `CustomerID` int NOT NULL,
    `ProductID` int NOT NULL,
    `BranchID` int NOT NULL,
    `OrderDate` date NOT NULL,
    `sPrice` double(13, 2) DEFAULT NULL,
    `Status` tinytext NOT NULL,
    `ItemQuantity` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;


ALTER TABLE orders MODIFY OrderID INT AUTO_INCREMENT PRIMARY KEY; 
ALTER TABLE branch MODIFY BranchID INT AUTO_INCREMENT PRIMARY KEY;
ALTER TABLE customers MODIFY CustomerID INT AUTO_INCREMENT PRIMARY KEY;
ALTER TABLE products MODIFY ProductID INT AUTO_INCREMENT PRIMARY KEY;


/* FOREIGN KEYS */

ALTER TABLE orders ADD CONSTRAINT FOREIGN KEY (CustomerID) REFERENCES customers (CustomerID);
ALTER TABLE orders ADD CONSTRAINT FOREIGN KEY (ProductID) REFERENCES products (ProductID);
ALTER TABLE orders ADD CONSTRAINT FOREIGN KEY (BranchID) REFERENCES branch (BranchID);
ALTER TABLE orders ADD CONSTRAINT CHECK (`Status` = 'PAID' OR `Status` = 'RFND');


/*
DROP TRIGGER IF EXISTS `wallysworld`.`orders_AFTER_INSERT`;
DELIMITER $$
USE `wallysworld`$$
CREATE DEFINER=`root`@`localhost` TRIGGER `orders_AFTER_INSERT` AFTER INSERT ON `orders` FOR EACH ROW BEGIN
	UPDATE orders o INNER JOIN products p ON p.ProductID = o.ProductID
    SET o.sPrice = (p.wPrice * 1.4);
END$$
DELIMITER ;
*/

