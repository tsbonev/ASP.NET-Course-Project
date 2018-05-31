/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

-- initially delete all data that we add by default
DELETE FROM Restaurants WHERE CityID BETWEEN 1 AND 4 OR CategoryID BETWEEN 1 AND 6
DELETE FROM Cities WHERE ID BETWEEN 1 AND 4
DELETE FROM Categories WHERE ID BETWEEN 1 AND 6


-- Init Cities
SET IDENTITY_INSERT dbo.Cities ON
INSERT INTO Cities (ID, Name) VALUES (1, N'Veliko Tarnovo')
INSERT INTO Cities (ID, Name) VALUES (2, N'Sofia')
INSERT INTO Cities (ID, Name) VALUES (3, N'Varna')
INSERT INTO Cities (ID, Name) VALUES (4, N'Plovdiv')
SET IDENTITY_INSERT dbo.Cities OFF
GO

-- Init Categories
SET IDENTITY_INSERT dbo.Categories ON
INSERT INTO Categories (ID, Name) VALUES (1, N'Бирария')
INSERT INTO Categories (ID, Name) VALUES (2, N'Бар')
INSERT INTO Categories (ID, Name) VALUES (3, N'Пицария')
INSERT INTO Categories (ID, Name) VALUES (4, N'Ресторант')
INSERT INTO Categories (ID, Name) VALUES (5, N'Сладкарница')
INSERT INTO Categories (ID, Name) VALUES (6, N'Пиано бар')
SET IDENTITY_INSERT dbo.Categories OFF
GO

-- Init Restaurants
--SET IDENTITY_INSERT dbo.Restaurants ON
INSERT INTO Restaurants (CategoryID, CityID, Name, [Description], ImageName, DateCreated, Email)
     VALUES (1, 1, N'City pub', N'City pub description', null, getdate(), null);
INSERT INTO Restaurants (CategoryID, CityID, Name, [Description], ImageName, DateCreated, Email)
     VALUES (3, 1, N'Его', N'Его description', null, getdate(), null);
INSERT INTO Restaurants (CategoryID, CityID, Name, [Description], ImageName, DateCreated, Email)
     VALUES (3, 1, N'La scalla', N'La scalla description', null, getdate(), null);
INSERT INTO Restaurants (CategoryID, CityID, Name, [Description], ImageName, DateCreated, Email)
     VALUES (4, 1, N'Щастливеца', N'Щастливецаdescription', null, getdate(), null);
INSERT INTO Restaurants (CategoryID, CityID, Name, [Description], ImageName, DateCreated, Email)
     VALUES (5, 1, N'Неделя', N'Неделя description', null, getdate(), null);
INSERT INTO Restaurants (CategoryID, CityID, Name, [Description], ImageName, DateCreated, Email)
     VALUES (5, 2, N'Неделя', N'Неделя description', null, getdate(), null);
INSERT INTO Restaurants (CategoryID, CityID, Name, [Description], ImageName, DateCreated, Email)
     VALUES (5, 3, N'Неделя', N'Неделя description', null, getdate(), null);
INSERT INTO Restaurants (CategoryID, CityID, Name, [Description], ImageName, DateCreated, Email)
     VALUES (5, 4, N'Неделя', N'Неделя description', null, getdate(), null);
INSERT INTO Restaurants (CategoryID, CityID, Name, [Description], ImageName, DateCreated, Email)
     VALUES (6, 1, N'Пияното Пиано', N'Пияното Пиано description', null, getdate(), null);
--SET IDENTITY_INSERT dbo.Restaurants OFF