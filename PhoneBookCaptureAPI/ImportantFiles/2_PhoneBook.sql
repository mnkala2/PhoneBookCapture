USE [PhoneBookSys]
GO

/****** Object:  Table [dbo].[PhoneBook]    Script Date: 2021/06/08 22:09:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PhoneBook](
	[PhoneBookID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[EntryID] [int] NOT NULL,
 CONSTRAINT [PK_PhoneBook] PRIMARY KEY CLUSTERED 
(
	[PhoneBookID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[PhoneBook]  WITH CHECK ADD  CONSTRAINT [FK_PhoneBook_PhoneBook] FOREIGN KEY([EntryID])
REFERENCES [dbo].[Entries] ([EntryID])
GO

ALTER TABLE [dbo].[PhoneBook] CHECK CONSTRAINT [FK_PhoneBook_PhoneBook]
GO


