Create table UserLogin (
	[Id] [int] IDENTITY(1,1) NOT NULL Primary Key,
	[UserName] [nvarchar](256) NULL,
	[CreatedDate] [datetime] NOT NULL default(getdate())

)

Create table GroupCustomer(
	[Id] [int] IDENTITY(1,1) NOT NULL Primary Key,
	[GroupName] [nvarchar](256) NULL,
	UserLoginId [int] not null,
	[CreatedDate] [datetime] NOT NULL default(getdate())
)

Create table GroupDetail(
	[Id] [bigint] IDENTITY(1,1) NOT NULL Primary Key,
	GroupCustomerId [int] not null,
	CustomerId varchar(10)
)


ALTER TABLE [dbo].GroupCustomer  WITH CHECK ADD  CONSTRAINT [FK_GroupCustomer_UserLogin] FOREIGN KEY(UserLoginId)
REFERENCES [dbo].UserLogin (Id)
GO

ALTER TABLE [dbo].GroupDetail  WITH CHECK ADD  CONSTRAINT [FK_GroupCustomer_GroupDetail] FOREIGN KEY(GroupCustomerId)
REFERENCES [dbo].GroupCustomer (Id)
GO

ALTER TABLE [dbo].GroupDetail  WITH CHECK ADD  CONSTRAINT [FK_Customer_GroupDetail] FOREIGN KEY(CustomerId)
REFERENCES [dbo].Customer (CustomerId)
GO


