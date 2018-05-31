CREATE TABLE [dbo].[Comments] (
    [ID]           INT             IDENTITY (1, 1) NOT NULL,
    [UserID]       INT             NOT NULL,
    [RestaurantID] INT             NOT NULL,
    [CommentText]      NVARCHAR (2000) NOT NULL,
    [DateCreated]  DATETIME        NOT NULL,
    CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Comments_Restaurants] FOREIGN KEY ([RestaurantID]) REFERENCES [dbo].[Restaurants] ([ID]),
    CONSTRAINT [FK_Comments_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([ID])
);

