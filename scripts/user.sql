﻿/*
Deployment script for C:\SOURCES\GEOMSG\SOURCE\WEB\APP_DATA\GEOMSGDB.MDF

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


CREATE TABLE [dbo].[User] (
    [ID]         INT            IDENTITY (1, 1) NOT NULL,
    [Email]      NVARCHAR (50)  NOT NULL,
    [Password]   NVARCHAR (MAX) NOT NULL,
    [DataCreate] DATETIME       NOT NULL,
    [DataUpdate] DATETIME       NOT NULL,
    [DataBirth]  DATETIME       NOT NULL,
    [Name]       NVARCHAR (100) NOT NULL,
    [Blocked]    BIT            NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);
   