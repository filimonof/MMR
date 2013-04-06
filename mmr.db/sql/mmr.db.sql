/*
Deployment script for mmr.db
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "mmr.db"
:setvar DefaultDataPath "c:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\DATA\"
:setvar DefaultLogPath "c:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\DATA\"

GO
USE [master]

GO
:on error exit
GO
IF (DB_ID(N'$(DatabaseName)') IS NOT NULL
    AND DATABASEPROPERTYEX(N'$(DatabaseName)','Status') <> N'ONLINE')
BEGIN
    RAISERROR(N'The state of the target database, %s, is not set to ONLINE. To deploy to this database, its state must be set to ONLINE.', 16, 127,N'$(DatabaseName)') WITH NOWAIT
    RETURN
END

GO

IF NOT EXISTS (SELECT 1 FROM [master].[dbo].[sysdatabases] WHERE [name] = N'$(DatabaseName)')
BEGIN
    RAISERROR(N'You cannot deploy this update script to target FILIMONOVVV-MRD\SQLEXPRESS. The database for which this script was built, mmr.db, does not exist on this server.', 16, 127) WITH NOWAIT
    RETURN
END

GO

IF (@@servername != 'FILIMONOVVV-MRD\SQLEXPRESS')
BEGIN
    RAISERROR(N'The server name in the build script %s does not match the name of the target server %s. Verify whether your database project settings are correct and whether your build script is up to date.', 16, 127,N'FILIMONOVVV-MRD\SQLEXPRESS',@@servername) WITH NOWAIT
    RETURN
END

GO

IF CAST(DATABASEPROPERTY(N'$(DatabaseName)','IsReadOnly') as bit) = 1
BEGIN
    RAISERROR(N'You cannot deploy this update script because the database for which it was built, %s , is set to READ_ONLY.', 16, 127, N'$(DatabaseName)') WITH NOWAIT
    RETURN
END

GO
USE [$(DatabaseName)]

GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'disable';


GO
/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script	
 Use SQLCMD syntax to include a file into the pre-deployment script			
 Example:      :r .\filename.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

GO
/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script		
 Use SQLCMD syntax to include a file into the post-deployment script			
 Example:      :r .\filename.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

/*  Status */
--DELETE FROM [dbo].[tblPPBR_Status]
/*
INSERT INTO [dbo].[tblPPBR_Status] VALUES ( 0,'');
INSERT INTO [dbo].[tblPPBR_Status] VALUES ( 10,'получен ППБР');
INSERT INTO [dbo].[tblPPBR_Status] VALUES ( 11,'ошибка при получении ППБР');
INSERT INTO [dbo].[tblPPBR_Status] VALUES ( 20,'отправлен в СК-2003');
INSERT INTO [dbo].[tblPPBR_Status] VALUES ( 21,'ошибка при отправке в СК-2003');
INSERT INTO [dbo].[tblPPBR_Status] VALUES ( 30,'ППБР отправлен адресатам');
INSERT INTO [dbo].[tblPPBR_Status] VALUES ( 31,'ошибка при отправке ППБР адресатам');
*/

/*  Time */
--DELETE FROM [dbo].[tblTime]
/*
INSERT INTO [dbo].[tblTime] VALUES ( 1,'00:30');
INSERT INTO [dbo].[tblTime] VALUES ( 2,'01:00');
INSERT INTO [dbo].[tblTime] VALUES ( 3,'01:30');
INSERT INTO [dbo].[tblTime] VALUES ( 4,'02:00');
INSERT INTO [dbo].[tblTime] VALUES ( 5,'02:30');
INSERT INTO [dbo].[tblTime] VALUES ( 6,'03:00');
INSERT INTO [dbo].[tblTime] VALUES ( 7,'02:30*');  -- ?
INSERT INTO [dbo].[tblTime] VALUES ( 8,'03:00*');  -- ?
INSERT INTO [dbo].[tblTime] VALUES ( 9,'03:30');
INSERT INTO [dbo].[tblTime] VALUES (10,'04:00');
INSERT INTO [dbo].[tblTime] VALUES (11,'04:30');
INSERT INTO [dbo].[tblTime] VALUES (12,'05:00');
INSERT INTO [dbo].[tblTime] VALUES (13,'05:30');
INSERT INTO [dbo].[tblTime] VALUES (14,'06:00');
INSERT INTO [dbo].[tblTime] VALUES (15,'06:30');
INSERT INTO [dbo].[tblTime] VALUES (16,'07:00');
INSERT INTO [dbo].[tblTime] VALUES (17,'07:30');
INSERT INTO [dbo].[tblTime] VALUES (18,'08:00');
INSERT INTO [dbo].[tblTime] VALUES (19,'08:30');
INSERT INTO [dbo].[tblTime] VALUES (20,'09:00');
INSERT INTO [dbo].[tblTime] VALUES (21,'09:30');
INSERT INTO [dbo].[tblTime] VALUES (22,'10:00');
INSERT INTO [dbo].[tblTime] VALUES (23,'10:30');
INSERT INTO [dbo].[tblTime] VALUES (24,'11:00');
INSERT INTO [dbo].[tblTime] VALUES (25,'11:30');
INSERT INTO [dbo].[tblTime] VALUES (26,'12:00');
INSERT INTO [dbo].[tblTime] VALUES (27,'12:30');
INSERT INTO [dbo].[tblTime] VALUES (28,'13:00');
INSERT INTO [dbo].[tblTime] VALUES (29,'13:30');
INSERT INTO [dbo].[tblTime] VALUES (30,'14:00');
INSERT INTO [dbo].[tblTime] VALUES (31,'14:30');
INSERT INTO [dbo].[tblTime] VALUES (32,'15:00');
INSERT INTO [dbo].[tblTime] VALUES (33,'15:30');
INSERT INTO [dbo].[tblTime] VALUES (34,'16:00');
INSERT INTO [dbo].[tblTime] VALUES (35,'16:30');
INSERT INTO [dbo].[tblTime] VALUES (36,'17:00');
INSERT INTO [dbo].[tblTime] VALUES (37,'17:30');
INSERT INTO [dbo].[tblTime] VALUES (38,'18:00');
INSERT INTO [dbo].[tblTime] VALUES (39,'18:30');
INSERT INTO [dbo].[tblTime] VALUES (40,'19:00');
INSERT INTO [dbo].[tblTime] VALUES (41,'19:30');
INSERT INTO [dbo].[tblTime] VALUES (42,'20:00');
INSERT INTO [dbo].[tblTime] VALUES (43,'20:30');
INSERT INTO [dbo].[tblTime] VALUES (44,'21:00');
INSERT INTO [dbo].[tblTime] VALUES (45,'21:30');
INSERT INTO [dbo].[tblTime] VALUES (46,'22:00');
INSERT INTO [dbo].[tblTime] VALUES (47,'22:30');
INSERT INTO [dbo].[tblTime] VALUES (48,'23:00');
INSERT INTO [dbo].[tblTime] VALUES (49,'23:30');
INSERT INTO [dbo].[tblTime] VALUES (50,'24:00');
*/

GO
