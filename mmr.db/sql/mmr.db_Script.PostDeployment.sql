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
