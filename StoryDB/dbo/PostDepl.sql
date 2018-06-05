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
DELETE FROM [Category] WHERE ID BETWEEN 1 AND 5
DELETE FROM [User] WHERE ID BETWEEN 1 AND 2
DELETE FROM [Story] WHERE ID BETWEEN 1 AND 5
DELETE FROM [Chapter] WHERE ID BETWEEN 1 AND 5
DELETE FROM [Like] WHERE ID BETWEEN 1 AND 2

-- Init Category
SET IDENTITY_INSERT [dbo].[Category] ON
INSERT INTO [Category](ID, Name, lft, rgt) VALUES (1, N'Stories', 1, 10)
INSERT INTO [Category](ID, Name, lft, rgt) VALUES (2, N'Conciliators', 2, 5)
INSERT INTO [Category](ID, Name, lft, rgt) VALUES (3, N'Tiebreaker', 3, 4)
INSERT INTO [Category](ID, Name, lft, rgt) VALUES (4, N'Horror', 6, 7)
INSERT INTO [Category](ID, Name, lft, rgt) VALUES (5, N'Untruthful Stories', 8, 9)
SET IDENTITY_INSERT dbo.Category OFF
GO

-- Init User
SET IDENTITY_INSERT [dbo].[User] ON
INSERT INTO [User] (ID, Username, FirstName, LastName, PasswordHash, PasswordSalt)
VALUES (1, N'admin', N'Tsvetozar', N'Bonev', N'1D0E26786C3242DD5C3122CE3B46370B77EA5EADABAB1915F13CA2BE77168166',
N'66074b8f-2250-4ced-b06f-0b6e09c50795')
INSERT INTO [User] (ID, Username, FirstName, LastName, PasswordHash, PasswordSalt)
VALUES (2, N'user', N'Petko', N'Petkov', N'603117F7495B794F2BF8D4444C106018AF43705E4736670519F94CB901F9B382',
N'65ce8c42-f6b3-4485-88ff-3f658e8a71fc')
SET IDENTITY_INSERT [dbo].[User] OFF
GO

-- Init Story
SET IDENTITY_INSERT [dbo].[Story] ON
INSERT INTO [Story] (ID, Name, ImgLink, Slug, Category)
VALUES (1, N'The Long Night of Jinx Charisma', N'th1.png', N'tlnojc', 3)
INSERT INTO [Story] (ID, Name, ImgLink, Slug, Category)
VALUES (2, N'The Mimic and the Conciliators', N'th2.png', N'tlnojc', 3) 
INSERT INTO [Story] (ID, Name, ImgLink, Slug, Category)
VALUES (3, N'The Railroad to Damnation', N'th3.png', N'tlnojc', 3)
INSERT INTO [Story] (ID, Name, ImgLink, Slug, Category)
VALUES (4, N'Cursed Sails', N'th4.png', N'cursed-sails', 4)
INSERT INTO [Story] (ID, Name, ImgLink, Slug, Category)
VALUES (5, N'Interloper', N'665_th7.png', N'interloper', 4) 
SET IDENTITY_INSERT [dbo].[Story] OFF
GO


-- Init Chapter
SET IDENTITY_INSERT [dbo].[Chapter] ON
INSERT INTO [Chapter] (ID, StoryID, ChapterNum, ChapterName, Text, DateCreated)
VALUES (1, 1, 1, N'Dusk', N'dusk', "Lorem ipsum dolor sit amet", getdate())
INSERT INTO [Chapter] (ID, StoryID, ChapterNum, ChapterName, Text, DateCreated)
VALUES (2, 1, 2, N'Hino', N'hino', "Lorem ipsum dolor sit amet", getdate())
INSERT INTO [Chapter] (ID, StoryID, ChapterNum, ChapterName, Text, DateCreated)
VALUES (3, 1, 3, N'Vee', N'vee', "Lorem ipsum dolor sit amet", getdate())
INSERT INTO [Chapter] (ID, StoryID, ChapterNum, ChapterName, Text, DateCreated)
VALUES (4, 2, 1, N'Test1', N'test1', "Test1", getdate())
INSERT INTO [Chapter] (ID, StoryID, ChapterNum, ChapterName, Text, DateCreated)
VALUES (5, 2, 2, N'Test2', N'test2', "Test2", getdate())
SET IDENTITY_INSERT [dbo].[Chapter] OFF
GO

-- Init Like
SET IDENTITY_INSERT [dbo].[Like] ON
INSERT INTO [Like] (UserID, ChapterID)
VALUES (2, 1)
INSERT INTO [Like] (UserID, ChapterID)
VALUES (2, 2)
SET IDENTITY_INSERT [dbo].[Like] OFF
GO