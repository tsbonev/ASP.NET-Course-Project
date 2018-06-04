CREATE TABLE [dbo].[Like] (
    [UserID]    INT NOT NULL,
    [ChapterID] INT NOT NULL,
    CONSTRAINT [PK_Like] PRIMARY KEY CLUSTERED ([UserID] ASC, [ChapterID] ASC),
    CONSTRAINT [FK_Like_Chapter] FOREIGN KEY ([ChapterID]) REFERENCES [dbo].[Chapter] ([ID]),
    CONSTRAINT [FK_Like_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);

