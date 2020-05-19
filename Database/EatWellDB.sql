CREATE DATABASE EatWellBD
GO

USE [EatWellDB]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 5/19/2020 6:05:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryID] [int] NOT NULL,
	[CategoryName] [nvarchar](50) NOT NULL,
	[CategorySlug] [nvarchar](max) NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Options]    Script Date: 5/19/2020 6:05:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Options](
	[OptionID] [int] NOT NULL,
	[OptionName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Options] PRIMARY KEY CLUSTERED 
(
	[OptionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 5/19/2020 6:05:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[OrderID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[Price] [money] NOT NULL,
	[Quantity] [money] NOT NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC,
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 5/19/2020 6:05:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[OrderPhone] [nvarchar](24) NULL,
	[OrderDate] [datetime] NULL,
	[OrderStatus] [nvarchar](50) NULL,
	[ShippingAddress] [nvarchar](50) NULL,
	[OrderDescription] [nvarchar](max) NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductCategories]    Script Date: 5/19/2020 6:05:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductCategories](
	[ProductID] [int] NOT NULL,
	[CategoryID] [int] NOT NULL,
 CONSTRAINT [PK_ProductCategories] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC,
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductOptions]    Script Date: 5/19/2020 6:05:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductOptions](
	[OptionID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[Price] [money] NOT NULL,
 CONSTRAINT [PK_ProductOptions] PRIMARY KEY CLUSTERED 
(
	[OptionID] ASC,
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 5/19/2020 6:05:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductID] [int] NOT NULL,
	[ProductName] [nvarchar](50) NOT NULL,
	[Photo] [image] NULL,
	[Description] [nvarchar](max) NULL,
	[ProductSlug] [nvarchar](max) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 5/19/2020 6:05:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] NOT NULL,
	[UserName] [nvarchar](20) NOT NULL,
	[Password] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Fullname] [nvarchar](50) NULL,
	[Address] [nvarchar](50) NULL,
	[Phone] [nvarchar](24) NULL,
	[IsAdmin] [bit] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsAdmin]  DEFAULT ((0)) FOR [IsAdmin]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Orders] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Orders] ([OrderID])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Orders]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Products] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Products]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Users]
GO
ALTER TABLE [dbo].[ProductCategories]  WITH CHECK ADD  CONSTRAINT [FK_ProductCategories_Categories] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Categories] ([CategoryID])
GO
ALTER TABLE [dbo].[ProductCategories] CHECK CONSTRAINT [FK_ProductCategories_Categories]
GO
ALTER TABLE [dbo].[ProductCategories]  WITH CHECK ADD  CONSTRAINT [FK_ProductCategories_Products] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[ProductCategories] CHECK CONSTRAINT [FK_ProductCategories_Products]
GO
ALTER TABLE [dbo].[ProductOptions]  WITH CHECK ADD  CONSTRAINT [FK_ProductOptions_Options] FOREIGN KEY([OptionID])
REFERENCES [dbo].[Options] ([OptionID])
GO
ALTER TABLE [dbo].[ProductOptions] CHECK CONSTRAINT [FK_ProductOptions_Options]
GO
ALTER TABLE [dbo].[ProductOptions]  WITH CHECK ADD  CONSTRAINT [FK_ProductOptions_Products] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[ProductOptions] CHECK CONSTRAINT [FK_ProductOptions_Products]
GO
