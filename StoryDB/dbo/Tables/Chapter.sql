CREATE TABLE [dbo].[Chapter] (
    [ID]          INT            NOT NULL,
    [StoryID]     INT            NOT NULL,
    [ChapterNum]  INT            NOT NULL,
    [ChapterName] NVARCHAR (MAX) NOT NULL,
    [Slug]        NVARCHAR (MAX) NOT NULL,
    [TextLink]    NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Chapters] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Chapters_Stories] FOREIGN KEY ([StoryID]) REFERENCES [dbo].[Story] ([ID])
);



