CREATE DATABASE [NHibernate101]
GO

USE [NHibernate101]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 11/02/2009 21:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Posts]    Script Date: 11/02/2009 21:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Posts](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [varchar](200) NOT NULL,
	[Body] [text] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[IsPublic] [bit] NOT NULL,
 CONSTRAINT [PK_Blogs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PostCategory]    Script Date: 11/02/2009 21:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostCategory](
	[idPost] [uniqueidentifier] NOT NULL,
	[idCategory] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_PostCategory_Categories]    Script Date: 11/02/2009 21:45:51 ******/
ALTER TABLE [dbo].[PostCategory]  WITH CHECK ADD  CONSTRAINT [FK_PostCategory_Categories] FOREIGN KEY([idCategory])
REFERENCES [dbo].[Categories] ([Id])
GO
ALTER TABLE [dbo].[PostCategory] CHECK CONSTRAINT [FK_PostCategory_Categories]
GO
/****** Object:  ForeignKey [FK_PostCategory_Posts]    Script Date: 11/02/2009 21:45:51 ******/
ALTER TABLE [dbo].[PostCategory]  WITH CHECK ADD  CONSTRAINT [FK_PostCategory_Posts] FOREIGN KEY([idPost])
REFERENCES [dbo].[Posts] ([Id])
GO
ALTER TABLE [dbo].[PostCategory] CHECK CONSTRAINT [FK_PostCategory_Posts]
GO
