
USE [swipejob_beta]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 29/10/2016 10:38:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Applicant]    Script Date: 29/10/2016 10:38:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Applicant](
	[Id] [uniqueidentifier] NOT NULL,
	[JobSeekerId] [uniqueidentifier] NOT NULL,
	[JobId] [uniqueidentifier] NOT NULL,
	[ApplicantStatus] [int] NOT NULL,
	[CreatedDateUtc] [datetime] NOT NULL,
	[UpdatedDateUtc] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Applicant] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CompanyHisotry]    Script Date: 29/10/2016 10:38:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyHisotry](
	[Id] [uniqueidentifier] NOT NULL,
	[JobSeekerId] [uniqueidentifier] NOT NULL,
	[CompanyName] [nvarchar](max) NULL,
	[Position] [nvarchar](max) NULL,
	[From] [datetime] NULL,
	[To] [datetime] NULL,
	[CreatedDateUtc] [datetime] NOT NULL,
	[UpdatedDateUtc] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.CompanyHisotry] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Education]    Script Date: 29/10/2016 10:38:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Education](
	[Id] [uniqueidentifier] NOT NULL,
	[EducationLevel] [int] NOT NULL,
	[FieldOfStudyId] [uniqueidentifier] NOT NULL,
	[Certification] [nvarchar](max) NULL,
	[MinimumGrade] [nvarchar](max) NULL,
	[ExperienceLevel] [int] NOT NULL,
	[JobId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_dbo.Education] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Employer]    Script Date: 29/10/2016 10:38:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Employer](
	[UserId] [uniqueidentifier] NOT NULL,
	[CompanyName] [nvarchar](max) NOT NULL,
	[CompanyRegistrationNumber] [nvarchar](max) NULL,
	[Logo] [varbinary](max) NULL,
	[Address] [nvarchar](max) NULL,
	[WebLink] [nvarchar](max) NULL,
	[ContactName] [nvarchar](max) NOT NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[NatureOfBusiness] [nvarchar](max) NULL,
	[OverView] [ntext] NULL,
	[CreatedDateUtc] [datetime] NOT NULL,
	[UpdatedDateUtc] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Employer] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Feedback]    Script Date: 29/10/2016 10:38:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedback](
	[Id] [uniqueidentifier] NOT NULL,
	[FullName] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[Email] [nvarchar](225) NOT NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[Message] [nvarchar](max) NULL,
	[CreatedDateUtc] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Feedback] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Inbox]    Script Date: 29/10/2016 10:38:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inbox](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[RecipientId] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Message] [nvarchar](max) NULL,
	[CreatedDateUtc] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Inbox] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Industry]    Script Date: 29/10/2016 10:38:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Industry](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Industry] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Job]    Script Date: 29/10/2016 10:38:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Job](
	[Id] [uniqueidentifier] NOT NULL,
	[EmployerId] [uniqueidentifier] NOT NULL,
	[LanguageId] [uniqueidentifier] NOT NULL,
	[LocationId] [uniqueidentifier] NOT NULL,
	[JobType] [int] NOT NULL,
	[JobName] [nvarchar](max) NOT NULL,
	[MinSalary] [decimal](18, 2) NOT NULL,
	[MaxSalary] [decimal](18, 2) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[JobStartDate] [datetime] NOT NULL,
	[JobDescription] [nvarchar](max) NULL,
	[IsStartWorkImmediately] [bit] NOT NULL,
	[StartWorkingAt] [nvarchar](max) NULL,
	[EndWorkingAt] [nvarchar](max) NULL,
	[AvailableDate] [datetime] NOT NULL,
	[HoursPerDay] [int] NOT NULL,
	[DayPerWeek] [int] NOT NULL,
	[DayPerMonth] [int] NOT NULL,
	[GenderRequired] [int] NOT NULL,
	[ExtraSalary] [nvarchar](max) NULL,
	[IsSalaryIncludeMealAndBreakTime] [bit] NOT NULL,
	[ViewCount] [bigint] NOT NULL,
	[CreatedDateUtc] [datetime] NOT NULL,
	[UpdatedDateUtc] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Job] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[JobSeeker]    Script Date: 29/10/2016 10:38:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[JobSeeker](
	[UserId] [uniqueidentifier] NOT NULL,
	[Avartar] [varbinary](max) NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[Gender] [int] NOT NULL,
	[NationalServiceStatus] [int] NOT NULL,
	[DateOfBirth] [datetime] NULL,
	[NRICNumber] [nvarchar](max) NULL,
	[NRICType] [int] NOT NULL,
	[Race] [nvarchar](max) NULL,
	[Religions] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[MobileNumber] [nvarchar](max) NULL,
	[PostalCode] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[MoreDocument] [varbinary](max) NULL,
	[Extension] [nvarchar](max) NULL,
	[ExperienceYear] [int] NOT NULL,
	[HighestEducation] [nvarchar](max) NULL,
	[LanguageId] [uniqueidentifier] NOT NULL,
	[ExpectedPosition] [nvarchar](max) NULL,
	[ExpectedLocation] [nvarchar](max) NULL,
	[ExpectedJobCategory] [nvarchar](max) NULL,
	[ExpectedSalary] [decimal](18, 2) NOT NULL,
	[CanNegotiation] [bit] NOT NULL,
	[InterestIn] [ntext] NULL,
	[AdditionalInfo] [ntext] NULL,
	[CreatedDateUtc] [datetime] NOT NULL,
	[UpdatedDateUtc] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.JobSeeker] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[JobSeekerTempProfile]    Script Date: 29/10/2016 10:38:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobSeekerTempProfile](
	[Id] [uniqueidentifier] NOT NULL,
	[IndustryId] [uniqueidentifier] NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[PhoneNumber] [nvarchar](max) NOT NULL,
	[DayOfBirthUtc] [datetime] NULL,
	[ExperienceLevel] [int] NULL,
	[RegisteredDateUtc] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.JobSeekerTempProfile] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Language]    Script Date: 29/10/2016 10:38:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Language](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[CultureName] [nvarchar](max) NULL,
	[CreatedDateUtc] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Language] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Location]    Script Date: 29/10/2016 10:38:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Location](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Location] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 29/10/2016 10:38:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [uniqueidentifier] NOT NULL,
	[Email] [nvarchar](225) NOT NULL,
	[Password] [nvarchar](max) NULL,
	[AccountType] [int] NOT NULL,
	[UserType] [int] NOT NULL,
	[RegisteredDateUtc] [datetime] NOT NULL,
	[IsActivated] [bit] NOT NULL,
	[IsLocked] [bit] NOT NULL,
	[LockedDateUtc] [datetime] NULL,
	[ConfirmationCode] [nvarchar](max) NULL,
	[ParentId] [uniqueidentifier] NULL,
	[AccountLevelParent] [uniqueidentifier] NULL,
 CONSTRAINT [PK_dbo.User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Index [IX_JobId]    Script Date: 29/10/2016 10:38:51 ******/
CREATE NONCLUSTERED INDEX [IX_JobId] ON [dbo].[Applicant]
(
	[JobId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_JobSeekerId]    Script Date: 29/10/2016 10:38:51 ******/
CREATE NONCLUSTERED INDEX [IX_JobSeekerId] ON [dbo].[Applicant]
(
	[JobSeekerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_JobSeekerId]    Script Date: 29/10/2016 10:38:51 ******/
CREATE NONCLUSTERED INDEX [IX_JobSeekerId] ON [dbo].[CompanyHisotry]
(
	[JobSeekerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FieldOfStudyId]    Script Date: 29/10/2016 10:38:51 ******/
CREATE NONCLUSTERED INDEX [IX_FieldOfStudyId] ON [dbo].[Education]
(
	[FieldOfStudyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_JobId]    Script Date: 29/10/2016 10:38:51 ******/
CREATE NONCLUSTERED INDEX [IX_JobId] ON [dbo].[Education]
(
	[JobId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserId]    Script Date: 29/10/2016 10:38:51 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[Employer]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_RecipientId]    Script Date: 29/10/2016 10:38:51 ******/
CREATE NONCLUSTERED INDEX [IX_RecipientId] ON [dbo].[Inbox]
(
	[RecipientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserId]    Script Date: 29/10/2016 10:38:51 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[Inbox]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployerId]    Script Date: 29/10/2016 10:38:51 ******/
CREATE NONCLUSTERED INDEX [IX_EmployerId] ON [dbo].[Job]
(
	[EmployerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_LanguageId]    Script Date: 29/10/2016 10:38:51 ******/
CREATE NONCLUSTERED INDEX [IX_LanguageId] ON [dbo].[Job]
(
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_LocationId]    Script Date: 29/10/2016 10:38:51 ******/
CREATE NONCLUSTERED INDEX [IX_LocationId] ON [dbo].[Job]
(
	[LocationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_LanguageId]    Script Date: 29/10/2016 10:38:51 ******/
CREATE NONCLUSTERED INDEX [IX_LanguageId] ON [dbo].[JobSeeker]
(
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserId]    Script Date: 29/10/2016 10:38:51 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[JobSeeker]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_IndustryId]    Script Date: 29/10/2016 10:38:51 ******/
CREATE NONCLUSTERED INDEX [IX_IndustryId] ON [dbo].[JobSeekerTempProfile]
(
	[IndustryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Applicant] ADD  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [dbo].[CompanyHisotry] ADD  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [dbo].[Education] ADD  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [dbo].[Feedback] ADD  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [dbo].[Inbox] ADD  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [dbo].[Industry] ADD  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [dbo].[Job] ADD  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [dbo].[JobSeekerTempProfile] ADD  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [dbo].[Language] ADD  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [dbo].[Location] ADD  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [dbo].[Applicant]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Applicant_dbo.Job_JobId] FOREIGN KEY([JobId])
REFERENCES [dbo].[Job] ([Id])
GO
ALTER TABLE [dbo].[Applicant] CHECK CONSTRAINT [FK_dbo.Applicant_dbo.Job_JobId]
GO
ALTER TABLE [dbo].[Applicant]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Applicant_dbo.JobSeeker_JobSeekerId] FOREIGN KEY([JobSeekerId])
REFERENCES [dbo].[JobSeeker] ([UserId])
GO
ALTER TABLE [dbo].[Applicant] CHECK CONSTRAINT [FK_dbo.Applicant_dbo.JobSeeker_JobSeekerId]
GO
ALTER TABLE [dbo].[CompanyHisotry]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CompanyHisotry_dbo.JobSeeker_JobSeekerId] FOREIGN KEY([JobSeekerId])
REFERENCES [dbo].[JobSeeker] ([UserId])
GO
ALTER TABLE [dbo].[CompanyHisotry] CHECK CONSTRAINT [FK_dbo.CompanyHisotry_dbo.JobSeeker_JobSeekerId]
GO
ALTER TABLE [dbo].[Education]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Education_dbo.Industry_FieldOfStudyId] FOREIGN KEY([FieldOfStudyId])
REFERENCES [dbo].[Industry] ([Id])
GO
ALTER TABLE [dbo].[Education] CHECK CONSTRAINT [FK_dbo.Education_dbo.Industry_FieldOfStudyId]
GO
ALTER TABLE [dbo].[Education]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Education_dbo.Job_JobId] FOREIGN KEY([JobId])
REFERENCES [dbo].[Job] ([Id])
GO
ALTER TABLE [dbo].[Education] CHECK CONSTRAINT [FK_dbo.Education_dbo.Job_JobId]
GO
ALTER TABLE [dbo].[Employer]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Employer_dbo.User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Employer] CHECK CONSTRAINT [FK_dbo.Employer_dbo.User_UserId]
GO
ALTER TABLE [dbo].[Inbox]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Inbox_dbo.JobSeeker_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[JobSeeker] ([UserId])
GO
ALTER TABLE [dbo].[Inbox] CHECK CONSTRAINT [FK_dbo.Inbox_dbo.JobSeeker_UserId]
GO
ALTER TABLE [dbo].[Inbox]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Inbox_dbo.User_RecipientId] FOREIGN KEY([RecipientId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Inbox] CHECK CONSTRAINT [FK_dbo.Inbox_dbo.User_RecipientId]
GO
ALTER TABLE [dbo].[Inbox]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Inbox_dbo.User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Inbox] CHECK CONSTRAINT [FK_dbo.Inbox_dbo.User_UserId]
GO
ALTER TABLE [dbo].[Job]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Job_dbo.Employer_EmployerId] FOREIGN KEY([EmployerId])
REFERENCES [dbo].[Employer] ([UserId])
GO
ALTER TABLE [dbo].[Job] CHECK CONSTRAINT [FK_dbo.Job_dbo.Employer_EmployerId]
GO
ALTER TABLE [dbo].[Job]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Job_dbo.Language_LanguageId] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Language] ([Id])
GO
ALTER TABLE [dbo].[Job] CHECK CONSTRAINT [FK_dbo.Job_dbo.Language_LanguageId]
GO
ALTER TABLE [dbo].[Job]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Job_dbo.Location_LocationId] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Location] ([Id])
GO
ALTER TABLE [dbo].[Job] CHECK CONSTRAINT [FK_dbo.Job_dbo.Location_LocationId]
GO
ALTER TABLE [dbo].[JobSeeker]  WITH CHECK ADD  CONSTRAINT [FK_dbo.JobSeeker_dbo.Language_LanguageId] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Language] ([Id])
GO
ALTER TABLE [dbo].[JobSeeker] CHECK CONSTRAINT [FK_dbo.JobSeeker_dbo.Language_LanguageId]
GO
ALTER TABLE [dbo].[JobSeeker]  WITH CHECK ADD  CONSTRAINT [FK_dbo.JobSeeker_dbo.User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[JobSeeker] CHECK CONSTRAINT [FK_dbo.JobSeeker_dbo.User_UserId]
GO
ALTER TABLE [dbo].[JobSeekerTempProfile]  WITH CHECK ADD  CONSTRAINT [FK_dbo.JobSeekerTempProfile_dbo.Industry_IndustryId] FOREIGN KEY([IndustryId])
REFERENCES [dbo].[Industry] ([Id])
GO
ALTER TABLE [dbo].[JobSeekerTempProfile] CHECK CONSTRAINT [FK_dbo.JobSeekerTempProfile_dbo.Industry_IndustryId]
GO
USE [master]
GO
ALTER DATABASE [swipejob_beta] SET  READ_WRITE 
GO

--------------2016_10_29-----------------


ALTER TABLE [User]
Add ParentId uniqueidentifier NULL; -- ParentId <=> Id

  ALTER TABLE [User]
Add AccountLevelParent tinyint NULL; -- Level of Account: 1,2,3

CREATE TABLE JOBTITLE(
	Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	JOB_TITLE_NAME NVARCHAR(255) NULL,
	JOB_TITLE_PARENT INT NULL,
	JOB_TITLE_LEVEL TINYINT NULL,
	USERID VARCHAR(255),
	CREATE_DATE [datetime2](7) NOT NULL DEFAULT(GETDATE())
)
