CREATE TABLE [dbo].[TagRel] (
    [StoryID] INT NOT NULL,
    [TagID]   INT NOT NULL,
    CONSTRAINT [PK_TagRel] PRIMARY KEY CLUSTERED ([StoryID] ASC, [TagID] ASC),
    CONSTRAINT [FK_TagRel_Stories] FOREIGN KEY ([StoryID]) REFERENCES [dbo].[Story] ([ID]),
    CONSTRAINT [FK_TagRel_Tags] FOREIGN KEY ([TagID]) REFERENCES [dbo].[Tag] ([ID])
);



