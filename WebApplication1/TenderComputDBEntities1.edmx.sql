
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/29/2019 13:21:33
-- Generated from EDMX file: E:\TC final v2.2.1\TC final v2.2\WebApplication1\TenderComputDBEntities1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [TenderComputDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_tblComponies_tblTenders]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblComponies] DROP CONSTRAINT [FK_tblComponies_tblTenders];
GO
IF OBJECT_ID(N'[dbo].[FK_tblComponies_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblComponies] DROP CONSTRAINT [FK_tblComponies_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_tblTenders_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblTenders] DROP CONSTRAINT [FK_tblTenders_Users];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[tblComponies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblComponies];
GO
IF OBJECT_ID(N'[dbo].[tblTenders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblTenders];
GO
IF OBJECT_ID(N'[dbo].[tblUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblUsers];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'tblUsers'
CREATE TABLE [dbo].[tblUsers] (
    [ID] int  NOT NULL,
    [fname] nvarchar(50)  NULL,
    [lname] nvarchar(50)  NULL,
    [username] nvarchar(50)  NULL,
    [Password] nvarchar(50)  NULL
);
GO

-- Creating table 'tblTenders'
CREATE TABLE [dbo].[tblTenders] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [TenderNo] int  NULL,
    [TenderType] bit  NULL,
    [Currency] int  NULL,
    [UserID] int  NULL,
    [t] float  NULL,
    [Estimate] float  NULL,
    [Fguaranty] float  NULL,
    [i] float  NULL,
    [TenderDate] datetime  NULL,
    [CurrencyPrice] float  NULL,
    [Quorum] float  NULL,
    [Average] float  NULL,
    [Variance] float  NULL,
    [UpperLimit] float  NULL,
    [BottomLimit] float  NULL,
    [TenderName] nvarchar(100)  NULL
);
GO

-- Creating table 'tblComponies'
CREATE TABLE [dbo].[tblComponies] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ComponyName] nvarchar(50)  NULL,
    [Price] float  NULL,
    [IsAccept] bit  NULL,
    [X] float  NULL,
    [Comment] nvarchar(80)  NULL,
    [t] float  NULL,
    [L] float  NULL,
    [IeDiff] float  NULL,
    [UserID] int  NULL,
    [TenderID] int  NULL,
    [IsWin] bit  NULL,
    [CurrencyPrice] float  NULL,
    [RialiPrice] float  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'tblUsers'
ALTER TABLE [dbo].[tblUsers]
ADD CONSTRAINT [PK_tblUsers]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tblTenders'
ALTER TABLE [dbo].[tblTenders]
ADD CONSTRAINT [PK_tblTenders]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tblComponies'
ALTER TABLE [dbo].[tblComponies]
ADD CONSTRAINT [PK_tblComponies]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserID] in table 'tblTenders'
ALTER TABLE [dbo].[tblTenders]
ADD CONSTRAINT [FK_tblTenders_Users]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[tblUsers]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblTenders_Users'
CREATE INDEX [IX_FK_tblTenders_Users]
ON [dbo].[tblTenders]
    ([UserID]);
GO

-- Creating foreign key on [TenderID] in table 'tblComponies'
ALTER TABLE [dbo].[tblComponies]
ADD CONSTRAINT [FK_tblComponies_tblTenders]
    FOREIGN KEY ([TenderID])
    REFERENCES [dbo].[tblTenders]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblComponies_tblTenders'
CREATE INDEX [IX_FK_tblComponies_tblTenders]
ON [dbo].[tblComponies]
    ([TenderID]);
GO

-- Creating foreign key on [UserID] in table 'tblComponies'
ALTER TABLE [dbo].[tblComponies]
ADD CONSTRAINT [FK_tblComponies_Users]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[tblUsers]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblComponies_Users'
CREATE INDEX [IX_FK_tblComponies_Users]
ON [dbo].[tblComponies]
    ([UserID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------