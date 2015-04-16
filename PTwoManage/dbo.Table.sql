CREATE TABLE [dbo].[Table]
(
	[ID] INT NOT NULL PRIMARY KEY, 
    [Name] NCHAR(50) NULL, 
    [Password] NCHAR(100) NULL, 
    [Email] NCHAR(50) NULL, 
    [Phone] INT NULL, 
    [Ranking] INT NULL, 
    [OpenTag] BIT NULL, 
    [CloseTag] BIT NULL
)
