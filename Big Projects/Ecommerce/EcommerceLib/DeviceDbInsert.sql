use DeviceEcommerce

INSERT INTO Brands (Name) VALUES
('Dell'),
('HP'),
('Lenovo'),
('Asus'),
('Acer'),
('Apple'),
('Samsung'),
('Microsoft'),
('Sony'),
('Toshiba');

-- Вставка данных в таблицу Categories
INSERT INTO Categories (Name,Icon) VALUES
('Laptops','https://png.pngtree.com/png-vector/20230218/ourmid/pngtree-laptop-icon-png-image_6606927.png'),
('Desktops','https://cdn-icons-png.flaticon.com/512/5/5088.png'),
('Monitors','https://cdn-icons-png.flaticon.com/512/3474/3474360.png'),
('Printers','https://cdn-icons-png.flaticon.com/512/3022/3022251.png'),
('Tablets','https://cdn-icons-png.flaticon.com/512/0/319.png'),
('Pereferic Accessories','https://cdn4.iconfinder.com/data/icons/electronics-15/100/electronics-06-512.png'),
('Networking','https://cdn-icons-png.flaticon.com/512/1239/1239719.png'),
('Storage','https://cdn-icons-png.flaticon.com/512/3151/3151254.png'),
('Gaming','https://cdn-icons-png.flaticon.com/512/5087/5087379.png');

-- Вставка данных в таблицу SubCategories
INSERT INTO SubCategories (Name, CategoryId, Icon) VALUES
('Ultrabooks', 1,'https://static.thenounproject.com/png/1007449-200.png'),
('Tower Desktops', 2,'https://cdn-icons-png.flaticon.com/512/2493/2493293.png'),
('Gaming Monitors', 3, 'https://cdn1.iconfinder.com/data/icons/game-57/2000/1-21-512.png'),
('All-in-One Printers', 4,'https://static.thenounproject.com/png/324840-200.png'),
('Android Tablets', 5,'https://cdn-icons-png.flaticon.com/512/0/319.png'),
('Keyboards', 6,'https://cdn-icons-png.flaticon.com/512/7546/7546214.png'),
('Routers', 7,'https://cdn-icons-png.flaticon.com/512/2502/2502330.png'),
('Internal SSDs', 8,'https://cdn-icons-png.flaticon.com/512/2286/2286814.png'),
('Gaming Consoles', 9,'https://cdn-icons-png.flaticon.com/512/3205/3205299.png');

-- Вставка данных в таблицу Products
INSERT INTO Products (Name, Price, BrandId, SubCategoryId, AddedDate) VALUES
('Dell XPS 13', 1299.99,  1, 1,GETDATE()),
('Dell Latitude 3520', 1265.99,  1, 1,GETDATE()),
('HP Pavilion Desktop', 799.992,2, 2,GETDATE()),
('Asus ROG Strix Monitor', 399.99,  4, 3,GETDATE()),
('Epson WorkForce Printer', 249.99,  3, 4,GETDATE()),
('iPad Pro', 999.99, 6, 5,GETDATE()),
('Logitech Wireless Mouse', 29.99,  8, 6,GETDATE()),
('Linksys Wi-Fi Router', 79.99,7, 7,GETDATE()),
('Samsung SSD 1TB', 149.99,  5, 8,GETDATE()),
('PlayStation 5', 499.99,  10, 9,GETDATE());


-- Вставка данных в таблицу BrandAndSubCategories
INSERT INTO BrandAndSubCategories (BrandId, SubCategoryId) VALUES
(1, 1),
(2, 2),
(4, 3),
(3, 4),
(6, 5),
(8, 6),
(7, 7),
(5, 8),
(10, 9);

-- Вставка данных в таблицу Characteristics
INSERT INTO Characteristics (Name, Description, SubCategoryId) VALUES
('Processor Type', 'Intel, AMD or another', 1),
('Refresh Rate', '', 2),
('Print Speed', 'Speed of print in minute', 4),
('Operating System', 'Android, IOS', 5),
('Connection Type', 'USB, Bluetooth', 6),
('Wireless Standard', '', 7),
('Storage Capacity', 'Storage Capacity', 8),
('GPU Model', '', 9);

-- Вставка данных в таблицу ProductChars
INSERT INTO ProductChars (CharacteristicId, ProductId, Value) VALUES
(1, 1, 'Intel Core i7'),
(1, 2, 'AMD Ryzen 5'),
(3, 3, '144Hz'),
(4, 4, '30 ppm'),
(5, 6, 'IOS'),
(6, 8, 'Wireless'),
(7, 8, '802.11ac'),
(8, 10, '1TB');