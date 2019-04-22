CREATE TABLE [dbo].[Customers](
[Id] [int] IDENTITY(1,1) NOT NULL,
[Name] [nvarchar](200) NOT NULL,
[Active] [bit] NOT NULL,
[RegisterDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Customers] PRIMARY KEY CLUSTERED 
(
[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET IDENTITY_INSERT [dbo].[Customers] ON 
GO
INSERT [dbo].[Customers] ([Id], [Name], [Active], [RegisterDate]) VALUES (1, N'Rodrigo', 1, CAST(N'2019-02-25T15:10:02.543' AS DateTime))
GO
INSERT [dbo].[Customers] ([Id], [Name], [Active], [RegisterDate]) VALUES (2, N'Portela', 1, CAST(N'2019-02-25T15:10:02.543' AS DateTime))
GO
INSERT [dbo].[Customers] ([Id], [Name], [Active], [RegisterDate]) VALUES (3, N'Mauricio', 1, CAST(N'2019-02-22T15:10:02.543' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Customers] OFF
GO
