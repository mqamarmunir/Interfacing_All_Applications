-- MySQL Administrator dump 1.4
--
-- ------------------------------------------------------
-- Server version	5.7.26-log


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


--
-- Create schema mi
--

CREATE DATABASE /*!32312 IF NOT EXISTS*/ mi;
USE mi;

--
-- Table structure for table `mi`.`cliqmachinemappings`
--

DROP TABLE IF EXISTS `cliqmachinemappings`;
CREATE TABLE `cliqmachinemappings` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `BranchID` int(10) unsigned NOT NULL DEFAULT '0',
  `Test_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `TestName` varchar(200) NOT NULL DEFAULT '',
  `CliqAttributeID` int(10) unsigned NOT NULL DEFAULT '0',
  `AttributeName` varchar(100) NOT NULL DEFAULT '',
  `MachineAttributeCode` varchar(30) NOT NULL DEFAULT '',
  `Active` tinyint(1) NOT NULL DEFAULT '0',
  `MachineID` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  KEY `Index_Active` (`Active`),
  KEY `Index_MachineAttributecode` (`MachineAttributeCode`),
  KEY `FK_cliqmachinemappings_MachineId` (`MachineID`),
  CONSTRAINT `FK_cliqmachinemappings_MachineId` FOREIGN KEY (`MachineID`) REFERENCES `mi_tinstruments` (`InstrumentID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=175 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `mi`.`cliqmachinemappings`
--

/*!40000 ALTER TABLE `cliqmachinemappings` DISABLE KEYS */;
/*!40000 ALTER TABLE `cliqmachinemappings` ENABLE KEYS */;


--
-- Table structure for table `mi`.`cliqtestsandattributes`
--

DROP TABLE IF EXISTS `cliqtestsandattributes`;
CREATE TABLE `cliqtestsandattributes` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `test_id` int(10) unsigned NOT NULL DEFAULT '0',
  `test_name` varchar(100) NOT NULL DEFAULT '',
  `department_id` int(10) unsigned NOT NULL DEFAULT '0',
  `att_id` int(10) unsigned NOT NULL DEFAULT '0',
  `att_name` varchar(100) NOT NULL DEFAULT '',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=330 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `mi`.`cliqtestsandattributes`
--

/*!40000 ALTER TABLE `cliqtestsandattributes` DISABLE KEYS */;
INSERT INTO `cliqtestsandattributes` (`id`,`test_id`,`test_name`,`department_id`,`att_id`,`att_name`) VALUES 
 (241,310,'Creatinine',13,89,'Creatinine'),
 (242,309,'Blood Urea Nitrogen (BUN)',13,95,'BUN'),
 (243,309,'Blood Urea Nitrogen (BUN)',13,94,'Urea'),
 (244,252,'Bilirubin Total',13,227,'Bilirubin Total'),
 (245,260,'Alkaline Phosphatase',13,228,'Alkaline Phosphatase'),
 (246,554,'Gamma GT',13,81,'Gamma GT'),
 (247,712,'2 Hrs. Blood Sugar',13,226,'2 Hrs. Blood Sugar2'),
 (248,712,'2 Hrs. Blood Sugar',13,225,'2Hr. Blood Sugar pp.'),
 (249,756,'Fasting Blood Sugar (FBS)',13,121,'Blood Glucose Fasting'),
 (250,756,'Fasting Blood Sugar (FBS)',13,105,'Sugar Fasting (Blood)'),
 (251,757,'Random Blood Sugar (RBS)',13,106,'Sugar Random (Blood)'),
 (252,447,'CK-MB',13,91,'CK-MB'),
 (253,227,'LDH',13,52,'LDH'),
 (254,385,'AST',13,102,'AST'),
 (255,742,'Magnesium (Serum)',13,242,'Magnesium (Serum)'),
 (256,746,'Potassium (Serum)',13,250,'Potassium (Serum)'),
 (257,753,'Sodium (Serum)',13,103,'Sodium (Serum)'),
 (258,439,'Chloride (Serum)',13,96,'Chloride (Serum)');
INSERT INTO `cliqtestsandattributes` (`id`,`test_id`,`test_name`,`department_id`,`att_id`,`att_name`) VALUES 
 (259,1315,'High Sensitive C-Reactive Protein (CRP)',13,453,'HS CRP'),
 (260,1128,'Serum Acetone (Ketone)',13,98,'Serum Ketone'),
 (261,340,'Albumin (Serum)',13,229,'Albumin (Serum)'),
 (262,423,'Calcium (Serum)',13,82,'Calcium (Serum)'),
 (263,429,'Cardiac Enzyme Profile',13,90,'CPK'),
 (264,429,'Cardiac Enzyme Profile',13,97,'CKMB'),
 (265,429,'Cardiac Enzyme Profile',13,52,'LDH'),
 (266,429,'Cardiac Enzyme Profile',13,93,'SGOT (AST)'),
 (267,218,'Cholesterol',13,92,'Cholesterol'),
 (268,731,'Serum Electrolytes (SE)',13,86,'Sodium'),
 (269,731,'Serum Electrolytes (SE)',13,85,'Potassium'),
 (270,731,'Serum Electrolytes (SE)',13,84,'Chloride'),
 (271,731,'Serum Electrolytes (SE)',13,82,'Calcium (Serum)'),
 (272,731,'Serum Electrolytes (SE)',13,83,'Bicarbonate'),
 (273,731,'Serum Electrolytes (SE)',13,87,'calc(ion)'),
 (274,530,'Fluid LDH',13,120,'Fluid LDH'),
 (275,531,'Fluid Protein',13,119,'Fluid Protein (Pus)'),
 (276,532,'Fluid Sugar',13,118,'Fluid Sugar (Pus)');
INSERT INTO `cliqtestsandattributes` (`id`,`test_id`,`test_name`,`department_id`,`att_id`,`att_name`) VALUES 
 (277,733,'GTT 2 Hrs-75 gm Oral Glucose',13,76,'Glucose Fasting'),
 (278,733,'GTT 2 Hrs-75 gm Oral Glucose',13,111,'Urine Gluocse 2 Hrs.'),
 (279,733,'GTT 2 Hrs-75 gm Oral Glucose',13,112,'Urine Glucose 1 Hr.'),
 (280,733,'GTT 2 Hrs-75 gm Oral Glucose',13,113,'Urine Glucose (Fasting)'),
 (281,733,'GTT 2 Hrs-75 gm Oral Glucose',13,114,'GTT-2 Hrs.'),
 (282,733,'GTT 2 Hrs-75 gm Oral Glucose',13,115,'GTT-1 Hr.'),
 (283,643,'Iron (Serum)',13,99,'Serum Iron'),
 (284,210,'Liver Function Test (LFT)',13,227,'Bilirubin Total'),
 (285,210,'Liver Function Test (LFT)',13,241,'SGPT (ALT)'),
 (286,210,'Liver Function Test (LFT)',13,240,'Alkaline Phosphatase (ALP)'),
 (287,210,'Liver Function Test (LFT)',13,81,'Gamma GT'),
 (288,210,'Liver Function Test (LFT)',13,231,'Albumin'),
 (289,752,'Renal Function Test (RFT)',13,94,'Urea'),
 (290,752,'Renal Function Test (RFT)',13,89,'Creatinine'),
 (291,752,'Renal Function Test (RFT)',13,247,'Uric Acid'),
 (292,752,'Renal Function Test (RFT)',13,95,'BUN');
INSERT INTO `cliqtestsandattributes` (`id`,`test_id`,`test_name`,`department_id`,`att_id`,`att_name`) VALUES 
 (293,764,'Triglycerides',13,117,'Triglycerides'),
 (294,244,'Uric Acid (Serum)',13,108,'Uric Acid (Serum)'),
 (295,192,'Urine For Microalbumin',13,249,'Microalbumin (Urine)'),
 (296,240,'Glucose Challenge Test (GCT)',13,76,'Glucose Fasting'),
 (297,240,'Glucose Challenge Test (GCT)',13,79,'Glucose 1Hr.'),
 (298,240,'Glucose Challenge Test (GCT)',13,78,'Glucose 2Hrs.'),
 (299,331,'A/G Ratio',13,231,'Albumin'),
 (300,331,'A/G Ratio',13,107,'Total Protein'),
 (301,331,'A/G Ratio',13,230,'A/G Ratio'),
 (302,331,'A/G Ratio',13,232,'Globulin'),
 (303,507,'Electrolytes (Urine)',13,86,'Sodium'),
 (304,507,'Electrolytes (Urine)',13,85,'Potassium'),
 (305,507,'Electrolytes (Urine)',13,84,'Chloride'),
 (306,507,'Electrolytes (Urine)',13,83,'Bicarbonate'),
 (307,658,'Low Density Lipoprotein (LDL)',13,239,'Low Density Lipoprotein (LDL)'),
 (308,682,'Osmolality (SERUM)',13,110,'Serum Osmolality'),
 (309,714,'24 Hrs. Urine Calcium',13,255,'24 Hrs. Urine Calcium');
INSERT INTO `cliqtestsandattributes` (`id`,`test_id`,`test_name`,`department_id`,`att_id`,`att_name`) VALUES 
 (310,714,'24 Hrs. Urine Calcium',13,256,'Total Volume'),
 (311,714,'24 Hrs. Urine Calcium',13,233,'Total Volume'),
 (312,720,'24 Hrs. Urine Microalbumin',13,249,'Microalbumin (Urine)'),
 (313,721,'24 Hrs. Urine Phosphorus',13,236,'24 Hrs. Urine Phosphorus'),
 (314,722,'24 Hrs. Urine Potassium',13,235,'24 Hrs. Urine Potassium'),
 (315,722,'24 Hrs. Urine Potassium',13,233,'Total Volume'),
 (316,738,'High Density Lipoprotein (HDL)',13,238,'High Density Lipoprotein (HDL)'),
 (317,747,'Potassium (Urine)',13,244,'Potassium (Urine)'),
 (318,749,'Protein (Spot Urine)',13,245,'Protein (Spot Urine)'),
 (319,754,'Sodium (Spot Urine)',13,104,'Urine Sodium'),
 (320,759,'TIBC (Serum)',13,100,'TIBC'),
 (321,766,'Uric Acid (Spot Urine)',13,109,'Uric Acid (Spot Urine)'),
 (322,768,'Urine Chloride',13,84,'Chloride'),
 (323,723,'24 Hrs. Urine Protein',13,233,'Total Volume'),
 (324,723,'24 Hrs. Urine Protein',13,234,'24 Hrs. Urine Protein'),
 (325,1519,'Lipid Profile',13,92,'Cholesterol');
INSERT INTO `cliqtestsandattributes` (`id`,`test_id`,`test_name`,`department_id`,`att_id`,`att_name`) VALUES 
 (326,1519,'Lipid Profile',13,117,'Triglycerides'),
 (327,1519,'Lipid Profile',13,238,'High Density Lipoprotein (HDL)'),
 (328,1519,'Lipid Profile',13,239,'Low Density Lipoprotein (LDL)'),
 (329,247,'Creatinine Clearance',13,258,'24 Hrs. Urine Creatinine Clearance');
/*!40000 ALTER TABLE `cliqtestsandattributes` ENABLE KEYS */;


--
-- Table structure for table `mi`.`mi_setting`
--

DROP TABLE IF EXISTS `mi_setting`;
CREATE TABLE `mi_setting` (
  `ID` varchar(50) NOT NULL,
  `Parameter1` varchar(150) NOT NULL,
  `Parameter2` varchar(45) DEFAULT NULL,
  `Parameter3` varchar(45) DEFAULT NULL,
  `Parameter4` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `mi`.`mi_setting`
--

/*!40000 ALTER TABLE `mi_setting` DISABLE KEYS */;
INSERT INTO `mi_setting` (`ID`,`Parameter1`,`Parameter2`,`Parameter3`,`Parameter4`) VALUES 
 ('CreateFile','N','','','25'),
 ('EmailInfo','SarimHaleem@yahoo.com','11','82.165.187.167','25'),
 ('LogFilePath','C:\\Documents and Settings\\Administrator\\Desktop','','','25'),
 ('Read','3','','','25'),
 ('Refresh','2','','','25'),
 ('Retention','6','','','25'),
 ('Send','5','','','25'),
 ('Upload','4','','','25');
/*!40000 ALTER TABLE `mi_setting` ENABLE KEYS */;


--
-- Table structure for table `mi`.`mi_taudit`
--

DROP TABLE IF EXISTS `mi_taudit`;
CREATE TABLE `mi_taudit` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `senton` datetime DEFAULT NULL,
  `sentto` varchar(45) DEFAULT NULL,
  `filename` varchar(45) DEFAULT NULL,
  `status` varchar(45) DEFAULT NULL,
  `maxresultid` int(10) unsigned NOT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_mi_taudit_1` (`maxresultid`),
  CONSTRAINT `FK_mi_taudit_1` FOREIGN KEY (`maxresultid`) REFERENCES `mi_tresult` (`ResultID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=993669 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `mi`.`mi_taudit`
--

/*!40000 ALTER TABLE `mi_taudit` DISABLE KEYS */;
/*!40000 ALTER TABLE `mi_taudit` ENABLE KEYS */;


--
-- Table structure for table `mi`.`mi_tbooking`
--

DROP TABLE IF EXISTS `mi_tbooking`;
CREATE TABLE `mi_tbooking` (
  `BookingID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `LabID` varchar(20) DEFAULT NULL,
  `PatientID` int(10) unsigned DEFAULT NULL,
  `Patient_name` varchar(150) DEFAULT NULL,
  `Test_Code` varchar(50) NOT NULL,
  `Machine_TestID` varchar(50) NOT NULL,
  `Test_Name` varchar(100) NOT NULL,
  `InstrumentID` int(10) unsigned NOT NULL,
  `Machine_Time` datetime DEFAULT NULL,
  `SeqID` varchar(50) DEFAULT NULL,
  `BatchNo` varchar(50) DEFAULT NULL,
  `Sample_Type` varchar(50) DEFAULT NULL,
  `EnteredBy` int(10) unsigned NOT NULL,
  `EnteredOn` datetime NOT NULL,
  `ClientID` char(4) NOT NULL,
  `Active` char(1) NOT NULL,
  `SendOn` datetime DEFAULT NULL,
  `ReceivedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`BookingID`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `mi`.`mi_tbooking`
--

/*!40000 ALTER TABLE `mi_tbooking` DISABLE KEYS */;
/*!40000 ALTER TABLE `mi_tbooking` ENABLE KEYS */;


--
-- Table structure for table `mi`.`mi_tinstruments`
--

DROP TABLE IF EXISTS `mi_tinstruments`;
CREATE TABLE `mi_tinstruments` (
  `InstrumentID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Supplierid` int(10) unsigned DEFAULT NULL,
  `Instrument_Name` varchar(150) NOT NULL,
  `Model` varchar(50) NOT NULL,
  `I_Release` varchar(50) DEFAULT NULL,
  `Description` varchar(1000) DEFAULT NULL,
  `Manual` varchar(150) DEFAULT NULL,
  `Patient_no_format` varchar(50) DEFAULT NULL,
  `BarCode_Standard` varchar(100) DEFAULT NULL,
  `Bidirectional` char(1) NOT NULL,
  `Communication_Stnadard` varchar(50) NOT NULL,
  `Communication_method` varchar(50) NOT NULL,
  `PORT` varchar(10) NOT NULL DEFAULT '',
  `BaudRate` int(10) unsigned NOT NULL,
  `Parity` varchar(10) NOT NULL,
  `Stopbit` varchar(10) NOT NULL,
  `DataBit` varchar(10) NOT NULL,
  `FlowControl` varchar(10) NOT NULL,
  `Acknowledgement_code` varchar(10) DEFAULT NULL,
  `EnteredBy` int(10) unsigned NOT NULL,
  `EnteredOn` datetime NOT NULL,
  `ClientID` varchar(45) NOT NULL,
  `Active` char(1) NOT NULL,
  `RecordTerminator` varchar(10) DEFAULT NULL,
  `ParsingAlgorithm` smallint(5) unsigned NOT NULL DEFAULT '0',
  `CliqInstrumentID` int(10) unsigned DEFAULT NULL,
  `IpAddress` varchar(15) DEFAULT NULL,
  PRIMARY KEY (`InstrumentID`),
  UNIQUE KEY `UniqueIndex_PORT` (`PORT`)
) ENGINE=InnoDB AUTO_INCREMENT=32 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `mi`.`mi_tinstruments`
--

/*!40000 ALTER TABLE `mi_tinstruments` DISABLE KEYS */;
INSERT INTO `mi_tinstruments` (`InstrumentID`,`Supplierid`,`Instrument_Name`,`Model`,`I_Release`,`Description`,`Manual`,`Patient_no_format`,`BarCode_Standard`,`Bidirectional`,`Communication_Stnadard`,`Communication_method`,`PORT`,`BaudRate`,`Parity`,`Stopbit`,`DataBit`,`FlowControl`,`Acknowledgement_code`,`EnteredBy`,`EnteredOn`,`ClientID`,`Active`,`RecordTerminator`,`ParsingAlgorithm`,`CliqInstrumentID`,`IpAddress`) VALUES 
 (27,NULL,'SysmexXp100','XP100',NULL,NULL,NULL,NULL,NULL,'Y','ASTM','Serial','COM6',9600,'None','One','8','None','6',1,'2019-06-15 17:05:01','541','Y','L|1',1,1636,''),
 (28,NULL,'Sysmex CA600','CA600',NULL,NULL,NULL,NULL,NULL,'Y','ASTM','Serial','COM5',9600,'None','One','8','None','6',1,'2019-06-15 17:05:45','541','Y','L|1',1,1638,''),
 (29,NULL,'Archtect ci4100','ci4100',NULL,NULL,NULL,NULL,NULL,'Y','ASTM','Serial','COM3',9600,'None','One','8','None','6',1,'2019-06-15 05:13:50','541','Y','L|1',4,1637,''),
 (30,NULL,'DirUIH500','H500',NULL,NULL,NULL,NULL,NULL,'Y','DirUIH500','Serial','COM4',9600,'None','One','8','None','',1,'2019-06-16 03:43:45','541','Y','',7,1639,''),
 (31,NULL,'Alegria','Alegria',NULL,NULL,NULL,NULL,NULL,'Y','ASTM','Serial','COM2',9600,'None','One','8','None','6',1,'2019-06-16 03:45:03','541','Y','L|1',1,1635,'');
/*!40000 ALTER TABLE `mi_tinstruments` ENABLE KEYS */;


--
-- Table structure for table `mi`.`mi_tpersonnel`
--

DROP TABLE IF EXISTS `mi_tpersonnel`;
CREATE TABLE `mi_tpersonnel` (
  `PersonId` int(10) unsigned NOT NULL,
  `OrgId` char(3) NOT NULL,
  `DepartmentId` int(10) unsigned NOT NULL,
  `SubdepartmentId` int(10) unsigned NOT NULL,
  `DesignationId` int(10) unsigned NOT NULL,
  `Active` char(1) NOT NULL,
  `ServiceNo` varchar(20) DEFAULT NULL,
  `Salutation` varchar(6) NOT NULL,
  `FName` varchar(30) NOT NULL,
  `MName` varchar(30) DEFAULT NULL,
  `LName` varchar(30) DEFAULT NULL,
  `Acronym` varchar(10) NOT NULL,
  `FHName` varchar(30) DEFAULT NULL,
  `Sex` char(1) NOT NULL,
  `DOB` datetime DEFAULT NULL,
  `BloodGroup` varchar(3) DEFAULT NULL,
  `MS` char(1) DEFAULT NULL,
  `NIC` varchar(15) DEFAULT NULL,
  `NICValidUpto` datetime DEFAULT NULL,
  `Passport` varchar(20) DEFAULT NULL,
  `PassportValidUpto` datetime DEFAULT NULL,
  `HPhoneNo1` varchar(15) DEFAULT NULL,
  `HPhoneNo2` varchar(15) DEFAULT NULL,
  `OPhoneNo1` varchar(15) DEFAULT NULL,
  `OPhoneNo2` varchar(15) DEFAULT NULL,
  `CPhoneNo` varchar(15) DEFAULT NULL,
  `Email` varchar(50) DEFAULT NULL,
  `CurrentAddress` varchar(250) DEFAULT NULL,
  `PermanentAddress` varchar(250) DEFAULT NULL,
  `PictureRef` varchar(255) DEFAULT NULL,
  `LoginId` varchar(15) DEFAULT NULL,
  `Pasword` varchar(15) DEFAULT NULL,
  `JoiningDate` datetime DEFAULT NULL,
  `LeavingDate` datetime DEFAULT NULL,
  `Education` varchar(255) DEFAULT NULL,
  `Nature` char(1) DEFAULT NULL,
  `ContractExpiry` datetime DEFAULT NULL,
  `ReferenceCode` varchar(15) DEFAULT NULL,
  `Salary` int(10) unsigned DEFAULT NULL,
  `FaxNo` varchar(15) DEFAULT NULL,
  `Enteredby` int(10) unsigned NOT NULL,
  `Enteredon` datetime NOT NULL,
  `clientid` char(3) NOT NULL,
  `crossdept` char(1) NOT NULL,
  PRIMARY KEY (`PersonId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `mi`.`mi_tpersonnel`
--

/*!40000 ALTER TABLE `mi_tpersonnel` DISABLE KEYS */;
INSERT INTO `mi_tpersonnel` (`PersonId`,`OrgId`,`DepartmentId`,`SubdepartmentId`,`DesignationId`,`Active`,`ServiceNo`,`Salutation`,`FName`,`MName`,`LName`,`Acronym`,`FHName`,`Sex`,`DOB`,`BloodGroup`,`MS`,`NIC`,`NICValidUpto`,`Passport`,`PassportValidUpto`,`HPhoneNo1`,`HPhoneNo2`,`OPhoneNo1`,`OPhoneNo2`,`CPhoneNo`,`Email`,`CurrentAddress`,`PermanentAddress`,`PictureRef`,`LoginId`,`Pasword`,`JoiningDate`,`LeavingDate`,`Education`,`Nature`,`ContractExpiry`,`ReferenceCode`,`Salary`,`FaxNo`,`Enteredby`,`Enteredon`,`clientid`,`crossdept`) VALUES 
 (1,'007',3,13,1,'Y','00001','Mr.','Trees','Technologies','','trs','software','M','1991-01-01 00:00:00',NULL,'S','',NULL,'',NULL,'','','','','','','','',NULL,'trees','kdc1234',NULL,NULL,'','P',NULL,'',0,'',1,'2010-02-25 16:22:56','007','Y');
/*!40000 ALTER TABLE `mi_tpersonnel` ENABLE KEYS */;


--
-- Table structure for table `mi`.`mi_tpreferencesetting`
--

DROP TABLE IF EXISTS `mi_tpreferencesetting`;
CREATE TABLE `mi_tpreferencesetting` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `serveraddress` varchar(45) DEFAULT NULL,
  `archivalfolderpath` varchar(45) DEFAULT NULL,
  `serverfolderpath` varchar(45) DEFAULT NULL,
  `serveruser` varchar(45) DEFAULT NULL,
  `serverpassword` varchar(45) DEFAULT NULL,
  `transfer_method` varchar(45) DEFAULT NULL,
  `status` char(1) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `mi`.`mi_tpreferencesetting`
--

/*!40000 ALTER TABLE `mi_tpreferencesetting` DISABLE KEYS */;
INSERT INTO `mi_tpreferencesetting` (`id`,`serveraddress`,`archivalfolderpath`,`serverfolderpath`,`serveruser`,`serverpassword`,`transfer_method`,`status`) VALUES 
 (1,'87.106.184.4','D:\\Qamar','D:\\Qamar','root','trees','direct db transfer','Y');
/*!40000 ALTER TABLE `mi_tpreferencesetting` ENABLE KEYS */;


--
-- Table structure for table `mi`.`mi_tresult`
--

DROP TABLE IF EXISTS `mi_tresult`;
CREATE TABLE `mi_tresult` (
  `ResultID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `BookingID` varchar(50) NOT NULL DEFAULT '',
  `AttributeID` varchar(50) NOT NULL DEFAULT '',
  `Result` varchar(100) NOT NULL DEFAULT '',
  `EnteredBy` int(10) unsigned NOT NULL DEFAULT '0',
  `EnteredOn` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `ClientID` char(4) DEFAULT NULL,
  `Status` char(1) NOT NULL DEFAULT '0',
  `InstrumentId` int(10) unsigned NOT NULL DEFAULT '0',
  `senton` datetime DEFAULT NULL,
  `sentto` varchar(1000) DEFAULT NULL,
  PRIMARY KEY (`ResultID`),
  KEY `Index_2` (`Status`,`EnteredOn`),
  KEY `FK_mi_tresult_1` (`InstrumentId`),
  KEY `Index_4` (`BookingID`),
  CONSTRAINT `FK_mi_tresult_1` FOREIGN KEY (`InstrumentId`) REFERENCES `mi_tinstruments` (`InstrumentID`)
) ENGINE=InnoDB AUTO_INCREMENT=1193953 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `mi`.`mi_tresult`
--

/*!40000 ALTER TABLE `mi_tresult` DISABLE KEYS */;
INSERT INTO `mi_tresult` (`ResultID`,`BookingID`,`AttributeID`,`Result`,`EnteredBy`,`EnteredOn`,`ClientID`,`Status`,`InstrumentId`,`senton`,`sentto`) VALUES 
 (1193940,'5541973','Date','2019-04-30 16 :10',1,'2019-06-16 15:49:56','541','X',30,'2019-06-16 15:50:02','https://apps.olivecliq.com/oliveapi/web/interfacing/save-data'),
 (1193941,'5541973','UBG','Normal 3.4umol/L',1,'2019-06-16 15:49:56','541','X',30,'2019-06-16 15:50:02','https://apps.olivecliq.com/oliveapi/web/interfacing/save-data'),
 (1193942,'5541973','BIL','Neg',1,'2019-06-16 15:49:56','541','X',30,'2019-06-16 15:50:02','https://apps.olivecliq.com/oliveapi/web/interfacing/save-data'),
 (1193943,'5541973','KET','2+ mmol/L',1,'2019-06-16 15:49:56','541','X',30,'2019-06-16 15:50:02','https://apps.olivecliq.com/oliveapi/web/interfacing/save-data'),
 (1193944,'5541973','BLD','3+',1,'2019-06-16 15:49:56','541','X',30,'2019-06-16 15:50:02','https://apps.olivecliq.com/oliveapi/web/interfacing/save-data'),
 (1193945,'5541973','PRO','Neg',1,'2019-06-16 15:49:56','541','X',30,'2019-06-16 15:50:02','https://apps.olivecliq.com/oliveapi/web/interfacing/save-data'),
 (1193946,'5541973','NIT','Neg',1,'2019-06-16 15:49:56','541','X',30,'2019-06-16 15:50:02','https://apps.olivecliq.com/oliveapi/web/interfacing/save-data');
INSERT INTO `mi_tresult` (`ResultID`,`BookingID`,`AttributeID`,`Result`,`EnteredBy`,`EnteredOn`,`ClientID`,`Status`,`InstrumentId`,`senton`,`sentto`) VALUES 
 (1193947,'5541973','LEU','Neg',1,'2019-06-16 15:49:56','541','X',30,'2019-06-16 15:50:02','https://apps.olivecliq.com/oliveapi/web/interfacing/save-data'),
 (1193948,'5541973','GLU','3+ mmol/L',1,'2019-06-16 15:49:56','541','X',30,'2019-06-16 15:50:02','https://apps.olivecliq.com/oliveapi/web/interfacing/save-data'),
 (1193949,'5541973','SG','1.020',1,'2019-06-16 15:49:56','541','X',30,'2019-06-16 15:50:03','https://apps.olivecliq.com/oliveapi/web/interfacing/save-data'),
 (1193950,'5541973','pH','<=5.0',1,'2019-06-16 15:49:56','541','X',30,'2019-06-16 15:50:03','https://apps.olivecliq.com/oliveapi/web/interfacing/save-data'),
 (1193951,'5541973','Color','',1,'2019-06-16 15:49:56','541','N',30,NULL,NULL),
 (1193952,'5541973','Clarity','',1,'2019-06-16 15:49:56','541','N',30,NULL,NULL);
/*!40000 ALTER TABLE `mi_tresult` ENABLE KEYS */;


--
-- Table structure for table `mi`.`mi_tsuppliers`
--

DROP TABLE IF EXISTS `mi_tsuppliers`;
CREATE TABLE `mi_tsuppliers` (
  `SupplierID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Supplier_Name` varchar(150) NOT NULL,
  `Address` varchar(500) DEFAULT NULL,
  `Phone_1` varchar(20) DEFAULT NULL,
  `Phone_2` varchar(20) DEFAULT NULL,
  `Fax_1` varchar(20) DEFAULT NULL,
  `Fax_2` varchar(20) DEFAULT NULL,
  `Email` varchar(50) DEFAULT NULL,
  `CellNo` varchar(20) DEFAULT NULL,
  `WebAddress` varchar(50) DEFAULT NULL,
  `Linkedin_ID` varchar(50) DEFAULT NULL,
  `CP_Name` varchar(50) NOT NULL,
  `CP_Designation` varchar(50) DEFAULT NULL,
  `CP_Office_Phone` varchar(20) DEFAULT NULL,
  `CP_Cell_no` varchar(20) DEFAULT NULL,
  `CP_Email` varchar(50) DEFAULT NULL,
  `City` varchar(50) DEFAULT NULL,
  `Country` varchar(45) DEFAULT NULL,
  `EnteredBy` int(10) unsigned NOT NULL,
  `EnteredOn` datetime NOT NULL,
  `Clientid` char(4) NOT NULL,
  `Active` char(1) NOT NULL,
  `Ref_SupplierID` int(10) unsigned DEFAULT NULL,
  PRIMARY KEY (`SupplierID`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `mi`.`mi_tsuppliers`
--

/*!40000 ALTER TABLE `mi_tsuppliers` DISABLE KEYS */;
INSERT INTO `mi_tsuppliers` (`SupplierID`,`Supplier_Name`,`Address`,`Phone_1`,`Phone_2`,`Fax_1`,`Fax_2`,`Email`,`CellNo`,`WebAddress`,`Linkedin_ID`,`CP_Name`,`CP_Designation`,`CP_Office_Phone`,`CP_Cell_no`,`CP_Email`,`City`,`Country`,`EnteredBy`,`EnteredOn`,`Clientid`,`Active`,`Ref_SupplierID`) VALUES 
 (5,'Abbott Diagnostic Division','House no. 663/9, old bara road, University Town, Peshawar','0915840252','','0915840252','','muhammad.jan@abbott.com','03005983709','http://www.abbott.com','Abbott','Muhammad Akif Jan','Field Manager','0915840252','03005983709','muhammad.jan@abbott.com','Peshawar','Pakistan',1,'2010-10-13 00:00:00','0001','Y',NULL),
 (6,'Roche Diagnostic Division','31 Street 63, F-7/3, Islamabad','0512270639','0512823374','0512823375','','','03005122723','','','Faizuddin Minhaji','','0512270639','03005122723','','','',1,'2010-10-13 00:00:00','0001','Y',NULL),
 (7,'Hoora Pharma','','0515598267','','0515598268','','','03008263759','http://www.hoorapharma.com','','Abdul Rasheed Chohan','CEO/President','02134305031-5','03008263759','rasheed@hoorapharma.com','','',1,'2010-10-13 00:00:00','0001','Y',NULL),
 (8,'S. Ejaz Uddin & Co.','13, Khayban-e-Johur, I-8/3, Islamabad','0514430267','0514430267','0514443245','','','','www.sejazuddin.com','','Farrukh Iqbal','Manager Technical Services','0514430267','03008543010','secodiag@comsats.net.pk','','',1,'2010-10-13 00:00:00','0001','Y',NULL);
INSERT INTO `mi_tsuppliers` (`SupplierID`,`Supplier_Name`,`Address`,`Phone_1`,`Phone_2`,`Fax_1`,`Fax_2`,`Email`,`CellNo`,`WebAddress`,`Linkedin_ID`,`CP_Name`,`CP_Designation`,`CP_Office_Phone`,`CP_Cell_no`,`CP_Email`,`City`,`Country`,`EnteredBy`,`EnteredOn`,`Clientid`,`Active`,`Ref_SupplierID`) VALUES 
 (9,'Burhani Enterprises','21/A, Krachi Market, Khyber Bazar, Peshawar','0912571193','','0912591340','','burhanient@gmail.com','','','','Tablib Hussain','','0912571193','03008595221','burhani52ent@yahoo.com','','',1,'2010-10-13 00:00:00','0001','Y',NULL),
 (10,'Vantage Technologies','251 (125/B), Street no. 11-B, Phase-1, Ghauri Town, Kaha East, Islmabad Highway, Islamabad','0512615152','0512615157','0512615153','','vntage@brain.net.pk','','http://www.vtpak.com','','Mohsin Ur Raza','Product Specialist','0512615152','','mail@vtpak.com','','',1,'2010-10-13 00:00:00','0001','Y',NULL),
 (11,'Chemical House','Qamar Mansion opp. UBL Bank, Sadar Road, Peshawar (Cantt)','915277858','427351690','915277858','','zia_haq@chemical-house.com','03339158685','','','Zia Ul Haq','Branch Manager KPK','915277858','03339158685','zia_haq@chemical-house.com','Peshawar','',1,'2010-10-13 00:00:00','0001','Y',NULL),
 (12,'Seico Scientific','104, Muhammadia Plaza. college Road, Rawalpindi','515532115','515555313','515540314','','seico@seico.com.pk','03008558297','www.seico.com.pk','','Rana Muhammad Asif','Product Manager','515532115','03008558297','seico@seico.com.pk','Rawalpindi','',1,'2010-10-13 00:00:00','0001','Y',NULL);
/*!40000 ALTER TABLE `mi_tsuppliers` ENABLE KEYS */;


--
-- Table structure for table `mi`.`mi_ttestattribute`
--

DROP TABLE IF EXISTS `mi_ttestattribute`;
CREATE TABLE `mi_ttestattribute` (
  `AttributeID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Machine_testid` int(10) unsigned NOT NULL,
  `LIMSAttributeID` char(6) NOT NULL,
  `LIMSAttributeName` varchar(150) NOT NULL,
  `MachineAttributeName` varchar(150) NOT NULL,
  `EnteredBy` int(10) unsigned NOT NULL,
  `EnteredOn` datetime NOT NULL,
  `ClientId` char(4) NOT NULL,
  `Active` char(1) NOT NULL,
  `MachineAttributeCode` varchar(20) NOT NULL,
  PRIMARY KEY (`AttributeID`)
) ENGINE=InnoDB AUTO_INCREMENT=169 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `mi`.`mi_ttestattribute`
--

/*!40000 ALTER TABLE `mi_ttestattribute` DISABLE KEYS */;
INSERT INTO `mi_ttestattribute` (`AttributeID`,`Machine_testid`,`LIMSAttributeID`,`LIMSAttributeName`,`MachineAttributeName`,`EnteredBy`,`EnteredOn`,`ClientId`,`Active`,`MachineAttributeCode`) VALUES 
 (4,9,'001691','Albumin','ALB',1,'2010-10-13 00:00:00','0001','Y','ALB'),
 (5,11,'003164','Plasma Ammonia','Plasma Ammonia',1,'2010-10-13 00:00:00','0001','Y','AMON'),
 (6,13,'001714','AST (GOT)','Aspartate transaminase (AST)',1,'2010-10-13 00:00:00','0001','Y','AST'),
 (7,12,'001855','Serum Amylase','Serum Amylase',1,'2010-10-13 00:00:00','0001','Y','AMY'),
 (8,14,'001682','Blood Urea','Blood Urea',1,'2010-10-13 00:00:00','0001','Y','BUN'),
 (9,15,'001679','Calcium Total','Calcium Total',1,'2010-10-13 00:00:00','0001','Y','CA'),
 (10,16,'001688','Total Cholesterol','Total Cholesterol',1,'2010-10-13 00:00:00','0001','Y','CHOL'),
 (11,17,'001667','CPK','CPK',1,'2010-10-13 00:00:00','0001','Y','CK'),
 (12,18,'001684','Serum Creatinine','Serum Creatinine',1,'2010-10-13 00:00:00','0001','Y','CREA'),
 (13,19,'001842','Bilirubin Direct','Bilirubin Direct',1,'2010-10-13 00:00:00','0001','Y','DBIL');
INSERT INTO `mi_ttestattribute` (`AttributeID`,`Machine_testid`,`LIMSAttributeID`,`LIMSAttributeName`,`MachineAttributeName`,`EnteredBy`,`EnteredOn`,`ClientId`,`Active`,`MachineAttributeCode`) VALUES 
 (14,20,'001770','Plasma Glucose (R)','Glucose (R)',1,'2010-10-13 00:00:00','0001','Y','GLU'),
 (15,21,'001716','LDH','LDH',1,'2010-10-13 00:00:00','0001','Y','LDH'),
 (16,22,'001680','Magnesium','Magnesium',1,'2010-10-13 00:00:00','0001','Y','MG'),
 (17,23,'001681','Phosphorous','Phosphorous (PO4```)',1,'2010-10-13 00:00:00','0001','Y','PHOS'),
 (18,24,'001693','Bilrubin Total','Bilirubin Total',1,'2010-10-13 00:00:00','0001','Y','TBIL'),
 (19,25,'001689','Triglyceride','Triglyceride',1,'2010-10-13 00:00:00','0001','Y','TGL'),
 (20,26,'001685','Uric Acid','Uric Acid',1,'2010-10-13 00:00:00','0001','Y','URCA'),
 (21,27,'001698','CEA (Carcinoemberyonic Antigen)','CEA (Carcinoemberyonic Antigen)',1,'2010-10-13 00:00:00','0001','Y','CEA'),
 (22,28,'003349','Cytomegalovirus (CMV) IgG','Cytomegalovirus (CMV) IgG',1,'2010-10-13 00:00:00','0001','Y','CVG'),
 (23,29,'003351','Cytomegalovirus (CMV) IgM','Cytomegalovirus (CMV) IgM',1,'2010-10-13 00:00:00','0001','Y','CMM A');
INSERT INTO `mi_ttestattribute` (`AttributeID`,`Machine_testid`,`LIMSAttributeID`,`LIMSAttributeName`,`MachineAttributeName`,`EnteredBy`,`EnteredOn`,`ClientId`,`Active`,`MachineAttributeCode`) VALUES 
 (24,30,'003483','Plasma Cortisol (morning)','Plasma Cortisol',1,'2010-10-13 00:00:00','0001','Y','COR'),
 (25,31,'003421','Estradiol','Estradiol',1,'2010-10-13 00:00:00','0001','Y','E2'),
 (26,32,'001722','fT4','fT4',1,'2010-10-13 00:00:00','0001','Y','F4'),
 (27,33,'001674','Serum Ferritin','Serum Ferritin',1,'2010-10-13 00:00:00','0001','Y','FER'),
 (28,34,'003423','FSH','FSH',1,'2010-10-13 00:00:00','0001','Y','FSH'),
 (29,35,'003123','ß-hCG','ß-hCG',1,'2010-10-13 00:00:00','0001','Y','HCG'),
 (30,36,'003540','H.pylori lgG antibodies','H.pylori lgG antibodies',1,'2010-10-13 00:00:00','0001','Y','HPG'),
 (31,37,'003426','Parathormone','Plasma Parathyroid Hormone (PTH) intact',1,'2010-10-13 00:00:00','0001','Y','iPT'),
 (32,38,'003422','LH','LH',1,'2010-10-13 00:00:00','0001','Y','LH'),
 (33,39,'001697','CA -125','CA-125',1,'2010-10-13 00:00:00','0001','Y','OV');
INSERT INTO `mi_ttestattribute` (`AttributeID`,`Machine_testid`,`LIMSAttributeID`,`LIMSAttributeName`,`MachineAttributeName`,`EnteredBy`,`EnteredOn`,`ClientId`,`Active`,`MachineAttributeCode`) VALUES 
 (34,40,'001699','Prostate Specific Antigen ','Prostate Specific Antigen (PSA)',1,'2010-10-13 00:00:00','0001','Y','PSA'),
 (35,41,'001721','TSH','TSH',1,'2010-10-13 00:00:00','0001','Y','TSH'),
 (36,42,'001720','T3 Total','T3 Total',1,'2010-10-13 00:00:00','0001','Y','T3'),
 (37,43,'003412','Testosterone (ARC)','Testosterone',1,'2010-10-13 00:00:00','0001','Y','TES'),
 (38,44,'003537','Thyroglobulin','Thyroglobulin',1,'2010-10-13 00:00:00','0001','Y','TG'),
 (39,45,'003357','Troponin-I','Troponin I',1,'2010-10-13 00:00:00','0001','Y','TPI'),
 (40,46,'001799','Toxoplasma IgG Antibodies','Toxoplasma IgG Antibodies',1,'2010-10-13 00:00:00','0001','Y','TXP'),
 (41,47,'001800','Toxplasma IgM Antibodies','Toxoplasma IgM Antibodies',1,'2010-10-13 00:00:00','0001','Y','TXU'),
 (42,48,'003536','Valproic Acid','Plasma Valproic Acid',1,'2010-10-13 00:00:00','0001','Y','VAL'),
 (43,50,'001721','TSH','TSH',1,'2010-11-02 00:00:00','0001','Y','10');
INSERT INTO `mi_ttestattribute` (`AttributeID`,`Machine_testid`,`LIMSAttributeID`,`LIMSAttributeName`,`MachineAttributeName`,`EnteredBy`,`EnteredOn`,`ClientId`,`Active`,`MachineAttributeCode`) VALUES 
 (44,51,'003412','Testosterone (ARC)','Testo',1,'2010-11-02 00:00:00','0001','Y','110'),
 (45,52,'003424','Progesterone','Prog',1,'2010-11-02 00:00:00','0001','Y','120'),
 (46,53,'001727','Prolactin','PRL',1,'2010-11-02 00:00:00','0001','Y','130'),
 (47,54,'003422','LH','LH',1,'2010-11-02 00:00:00','0001','Y','140'),
 (48,55,'003423','FSH','FSH',1,'2010-11-02 00:00:00','0001','Y','150'),
 (49,56,'003123','ß-hCG','HCG-BETA',1,'2010-11-02 00:00:00','0001','Y','761'),
 (50,57,'001722','fT4','FT4',1,'2010-11-02 00:00:00','0001','Y','30'),
 (51,58,'001698','CEA (Carcinoemberyonic Antigen)','CEA',1,'2010-11-02 00:00:00','0001','Y','301'),
 (52,59,'003125','Alpha -fetoprotiens (AFP)','AFP',1,'2010-11-02 00:00:00','0001','Y','311'),
 (53,60,'001699','Prostate Specific Antigen ','PSA',1,'2010-11-02 00:00:00','0001','Y','320'),
 (54,61,'001697','CA -125','CA-125',1,'2010-11-02 00:00:00','0001','Y','341');
INSERT INTO `mi_ttestattribute` (`AttributeID`,`Machine_testid`,`LIMSAttributeID`,`LIMSAttributeName`,`MachineAttributeName`,`EnteredBy`,`EnteredOn`,`ClientId`,`Active`,`MachineAttributeCode`) VALUES 
 (55,62,'002948','Hepatitis B Surface Antigen','HBSAG',1,'2010-11-02 00:00:00','0001','Y','400'),
 (56,63,'002951','Anti-HCV (Antibodies)','A-HCV',1,'2010-11-02 00:00:00','0001','Y','420'),
 (57,64,'002952','Anti-HIV I & II (Antibodies)','A-HIV',1,'2010-11-02 00:00:00','0001','Y','490'),
 (58,65,'001720','T3 Total','T3',1,'2010-11-02 00:00:00','0001','Y','50'),
 (59,66,'001750','Vit B12','B12',1,'2010-11-02 00:00:00','0001','Y','600'),
 (60,67,'001987','S. Folate','FOL',1,'2010-11-02 00:00:00','0001','Y','612'),
 (61,68,'003265','Immunoglobulin E (IgE)','IGE',1,'2010-11-02 00:00:00','0001','Y','630'),
 (62,69,'003424','Progesterone','PROG',1,'2010-11-02 00:00:00','0001','Y','191'),
 (63,70,'002950','Hepatitis Be Antigen','HBeAg',1,'2010-11-02 00:00:00','0001','Y','301'),
 (64,71,'002947','Hepatitis B Surface Antibody','Anti-HBs',1,'2010-11-02 00:00:00','0001','Y','131'),
 (65,72,'001674','Serum Ferritin','Ferritin',1,'2010-11-02 00:00:00','0001','Y','61');
INSERT INTO `mi_ttestattribute` (`AttributeID`,`Machine_testid`,`LIMSAttributeID`,`LIMSAttributeName`,`MachineAttributeName`,`EnteredBy`,`EnteredOn`,`ClientId`,`Active`,`MachineAttributeCode`) VALUES 
 (66,73,'003412','Testosterone (ARC)','Testost',1,'2010-11-02 00:00:00','0001','Y','231'),
 (67,74,'003523','Anti-TP antibodies','Syphilis',1,'2010-11-02 00:00:00','0001','Y','561'),
 (68,75,'003125','Alpha -fetoprotiens (AFP)','AFP_2',1,'2010-11-02 00:00:00','0001','Y','2'),
 (69,76,'002948','Hepatitis B Surface Antigen','HBsAg',1,'2010-11-02 00:00:00','0001','Y','149'),
 (70,77,'003422','LH','_LH',1,'2010-11-02 00:00:00','0001','Y','641'),
 (71,77,'003422','LH','_LH',1,'2010-11-02 00:00:00','0001','Y','641'),
 (72,79,'003423','FSH','FSH',1,'2010-11-02 00:00:00','0001','Y','81'),
 (73,80,'002949','Hepatitis Be Antibody','Anti-HBe',1,'2010-11-02 00:00:00','0001','Y','291'),
 (74,81,'003123','ß-hCG','BETAhCG',1,'2010-11-02 00:00:00','0001','Y','651'),
 (75,82,'003483','Plasma Cortisol (evening)','CortisolE',1,'2010-11-02 00:00:00','0001','Y','601'),
 (76,82,'003484','Plasma Cortisol (morning)','CortisolM',1,'2010-11-02 00:00:00','0001','Y','601');
INSERT INTO `mi_ttestattribute` (`AttributeID`,`Machine_testid`,`LIMSAttributeID`,`LIMSAttributeName`,`MachineAttributeName`,`EnteredBy`,`EnteredOn`,`ClientId`,`Active`,`MachineAttributeCode`) VALUES 
 (77,82,'003484','Plasma Cortisol (morning)','CortisolM',1,'2010-11-02 00:00:00','0001','Y','601'),
 (78,83,'002952','Anti-HIV I & II (Antibodies)','HIV AG/AB',1,'2010-11-02 00:00:00','0001','Y','721'),
 (79,84,'001699','Prostate Specific Antigen ','Total PSA',1,'2010-11-02 00:00:00','0001','Y','211'),
 (80,85,'003448','Hepatitis B core Total (Antibodies)','Anti-HBcII',1,'2010-11-02 00:00:00','0001','Y','580'),
 (81,86,'001727','Prolactin','Prolactin',1,'2010-11-02 00:00:00','0001','Y','201'),
 (82,87,'002951','Anti-HCV (Antibodies)','Anti-HCV',1,'2010-11-02 00:00:00','0001','Y','161'),
 (83,88,'001920','BNP','_BNP',1,'2010-11-02 00:00:00','0001','Y','739'),
 (84,89,'002953','Hepatitis A IgM antibodies','HAVAB IgM',1,'2010-11-02 00:00:00','0001','Y','321'),
 (85,90,'002952','Anti-HIV I & II (Antibodies)','HIV1_2_g0',1,'2010-11-02 00:00:00','0001','Y','829'),
 (86,91,'003349','Cytomegalovirus (CMV) IgG','CMV-G',1,'2010-11-02 00:00:00','0001','Y','743');
INSERT INTO `mi_ttestattribute` (`AttributeID`,`Machine_testid`,`LIMSAttributeID`,`LIMSAttributeName`,`MachineAttributeName`,`EnteredBy`,`EnteredOn`,`ClientId`,`Active`,`MachineAttributeCode`) VALUES 
 (87,92,'003351','Cytomegalovirus (CMV) IgM','CMV-M',1,'2010-11-02 00:00:00','0001','Y','744'),
 (88,93,'001721','TSH','TSH_3GEN',1,'2010-11-02 00:00:00','0001','Y','213'),
 (89,94,'001720','T3 Total','TT3-Mab',1,'2010-11-02 00:00:00','0001','Y','276'),
 (90,95,'002951','Anti-HCV (Antibodies)','HCV-3',1,'2010-11-02 00:00:00','0001','Y','841'),
 (91,96,'003423','FSH','FSH',1,'2010-11-02 00:00:00','0001','Y','37'),
 (92,97,'003422','LH','LH',1,'2010-11-02 00:00:00','0001','Y','25'),
 (93,98,'001727','Prolactin','PRL-3IS',1,'2010-11-02 00:00:00','0001','Y','55'),
 (94,99,'003424','Progesterone','PROG',1,'2010-11-02 00:00:00','0001','Y','92'),
 (95,100,'001892','D-dimer','D-dimer',1,'2010-11-02 00:00:00','0001','Y','868'),
 (96,101,'002947','Hepatitis B Surface Antibody','AUSAB',1,'2010-11-02 00:00:00','0001','Y','118'),
 (97,102,'001699','Prostate Specific Antigen ','PSA-Total',1,'2010-11-02 00:00:00','0001','Y','443');
INSERT INTO `mi_ttestattribute` (`AttributeID`,`Machine_testid`,`LIMSAttributeID`,`LIMSAttributeName`,`MachineAttributeName`,`EnteredBy`,`EnteredOn`,`ClientId`,`Active`,`MachineAttributeCode`) VALUES 
 (98,103,'003123','ß-hCG','B-hCG',1,'2010-11-02 00:00:00','0001','Y','16'),
 (99,104,'003412','Testosterone (ARC)','Testostrn',1,'2010-11-02 00:00:00','0001','Y','90'),
 (100,105,'002949','Hepatitis Be Antibody','Anti-HBe',1,'2010-11-02 00:00:00','0001','Y','192'),
 (101,106,'001674','Serum Ferritin','Ferritin',1,'2010-11-02 00:00:00','0001','Y','321'),
 (102,107,'003448','Hepatitis B core Total (Antibodies)','CORE',1,'2010-11-02 00:00:00','0001','Y','126'),
 (103,108,'002950','Hepatitis Be Antigen','HBe_2',1,'2010-11-02 00:00:00','0001','Y','193'),
 (104,109,'002948','Hepatitis B Surface Antigen','HBsAg',1,'2010-11-02 00:00:00','0001','Y','106'),
 (105,110,'003126','Plasma Cyclosporin A (CsA) level','Cyclo',1,'2010-11-02 00:00:00','0001','Y','693'),
 (106,111,'001722','fT4','Free-T4',1,'2010-11-02 00:00:00','0001','Y','219'),
 (107,112,'003125','Alpha -fetoprotiens (AFP)','AFP',1,'2010-11-02 00:00:00','0001','Y','728');
INSERT INTO `mi_ttestattribute` (`AttributeID`,`Machine_testid`,`LIMSAttributeID`,`LIMSAttributeName`,`MachineAttributeName`,`EnteredBy`,`EnteredOn`,`ClientId`,`Active`,`MachineAttributeCode`) VALUES 
 (108,113,'001695','Plasma Digoxin Level','Digoxin',1,'2010-11-02 00:00:00','0001','Y','601'),
 (109,114,'003421','Estradiol','Estradiol',1,'2010-11-02 00:00:00','0001','Y','71'),
 (110,115,'001920','BNP','BNP',1,'2010-11-02 00:00:00','0001','Y','377'),
 (111,116,'001859','Rubella IgG','RubellaG',1,'2010-11-02 00:00:00','0001','Y','723'),
 (112,117,'001860','Rubella IgM','RubellaM',1,'2010-11-02 00:00:00','0001','Y','754'),
 (113,118,'001799','Toxoplasma IgG Antibodies','TOXO-G',1,'2010-11-02 00:00:00','0001','Y','712'),
 (114,119,'001800','Toxplasma IgM Antibodies','TOXO-M',1,'2010-11-02 00:00:00','0001','Y','734'),
 (115,120,'003357','Troponin-I','TnI-ADV',1,'2010-11-02 00:00:00','0001','Y','313'),
 (116,121,'003483','Plasma Cortisol (evening)','CortisolE',1,'2010-11-02 00:00:00','0001','Y','848'),
 (117,121,'003483','Plasma Cortisol (evening)','CortisolE',1,'2010-11-02 00:00:00','0001','Y','848');
INSERT INTO `mi_ttestattribute` (`AttributeID`,`Machine_testid`,`LIMSAttributeID`,`LIMSAttributeName`,`MachineAttributeName`,`EnteredBy`,`EnteredOn`,`ClientId`,`Active`,`MachineAttributeCode`) VALUES 
 (118,121,'003484','Plasma Cortisol (morning)','CortisolM',1,'2010-11-02 00:00:00','0001','Y','848'),
 (119,122,'002953','Hepatitis A IgM antibodies','HAVABM_2',1,'2010-11-02 00:00:00','0001','Y','140'),
 (120,123,'002945','Hepatitis B Core IgM','COREM',1,'2010-11-02 00:00:00','0001','Y','165'),
 (121,124,'001717','Myoglobin','Myoglobin',1,'2010-11-02 00:00:00','0001','Y','350'),
 (122,125,'002872','P Vancomycin Level (Peak)','Vanco_II',1,'2010-11-02 00:00:00','0001','Y','668'),
 (123,125,'002872','P Vancomycin Level (Peak)','Vanco_II',1,'2010-11-02 00:00:00','0001','Y','668'),
 (124,125,'002873','P Vancomycin Level (Trough)','Vanco_II (Trough)',1,'2010-11-02 00:00:00','0001','Y','668'),
 (125,125,'002872','P Vancomycin Level (Peak)','Vanco_II (Peak)',1,'2010-11-02 00:00:00','0001','Y','668'),
 (126,125,'002873','P Vancomycin Level (Trough)','Vanco_II (Trough)',1,'2010-11-02 00:00:00','0001','Y','668');
INSERT INTO `mi_ttestattribute` (`AttributeID`,`Machine_testid`,`LIMSAttributeID`,`LIMSAttributeName`,`MachineAttributeName`,`EnteredBy`,`EnteredOn`,`ClientId`,`Active`,`MachineAttributeCode`) VALUES 
 (127,126,'001921','P Gentamicin Level (Peak)','Gent (Peak)',1,'2010-11-02 00:00:00','0001','Y','645'),
 (128,126,'001921','P Gentamicin Level (Peak)','Gent (Peak)',1,'2010-11-02 00:00:00','0001','Y','645'),
 (129,126,'002877','P Gentamicin Level (Random)','Gent (Random)',1,'2010-11-02 00:00:00','0001','Y','645'),
 (130,126,'001921','P Gentamicin Level (Peak)','Gent (Peak)',1,'2010-11-02 00:00:00','0001','Y','645'),
 (131,126,'002877','P Gentamicin Level (Random)','Gent (Random)',1,'2010-11-02 00:00:00','0001','Y','645'),
 (132,126,'002876','P Gentamicin Level (Trough)','Gent (Trough)',1,'2010-11-02 00:00:00','0001','Y','645'),
 (133,10,'001692','Alkaline Phosphatase','Alkaline Phosphatase',1,'2010-11-02 00:00:00','0001','Y','ALP'),
 (134,127,'001690','Alanine Transaminase (ALT)','ALT',1,'2010-11-02 00:00:00','0001','Y','ALT'),
 (135,128,'002951','Anti-HCV (Antibodies)','aHCV',1,'2010-12-05 00:00:00','0001','Y','aHCV');
INSERT INTO `mi_ttestattribute` (`AttributeID`,`Machine_testid`,`LIMSAttributeID`,`LIMSAttributeName`,`MachineAttributeName`,`EnteredBy`,`EnteredOn`,`ClientId`,`Active`,`MachineAttributeCode`) VALUES 
 (136,129,'003357','Troponin-I','TropI',1,'2010-12-05 00:00:00','0001','Y','0544'),
 (137,130,'001721','TSH','TSH',1,'2010-12-05 00:00:00','0001','Y','001'),
 (138,131,'001720','T3 Total','TT3',1,'2010-12-05 00:00:00','0001','Y','003'),
 (139,132,'001722','fT4','FT4',1,'2010-12-05 00:00:00','0001','Y','004'),
 (140,133,'003421','Estradiol','E2',1,'2010-12-05 00:00:00','0001','Y','008'),
 (141,134,'003422','LH','LH',1,'2010-12-05 00:00:00','0001','Y','009'),
 (142,135,'003423','FSH','FSH',1,'2010-12-05 00:00:00','0001','Y','010'),
 (143,136,'001727','Prolactin','PROL',1,'2010-12-05 00:00:00','0001','Y','011'),
 (144,137,'003424','Progesterone','PROG',1,'2010-12-05 00:00:00','0001','Y','012'),
 (145,138,'003123','ß-hCG','B-hCG',1,'2010-12-05 00:00:00','0001','Y','013'),
 (146,139,'003412','Testosterone (ARC)','TESTO',1,'2010-12-05 00:00:00','0001','Y','014'),
 (147,140,'003125','Alpha -fetoprotiens (AFP)','AFP',1,'2010-12-05 00:00:00','0001','Y','015');
INSERT INTO `mi_ttestattribute` (`AttributeID`,`Machine_testid`,`LIMSAttributeID`,`LIMSAttributeName`,`MachineAttributeName`,`EnteredBy`,`EnteredOn`,`ClientId`,`Active`,`MachineAttributeCode`) VALUES 
 (148,141,'001698','CEA (Carcinoemberyonic Antigen)','CEA',1,'2010-12-05 00:00:00','0001','Y','016'),
 (149,142,'002948','Hepatitis B Surface Antigen','HBsAg',1,'2010-12-05 00:00:00','0001','Y','HBsAg'),
 (150,143,'002947','Hepatitis B Surface Antibody','aHBS',1,'2010-12-05 00:00:00','0001','Y','018'),
 (151,144,'003448','Hepatitis B core Total (Antibodies)','aHBC',1,'2010-12-05 00:00:00','0001','Y','019'),
 (152,145,'002945','Hepatitis B Core IgM','aHBcM',1,'2010-12-05 00:00:00','0001','Y','020'),
 (153,146,'002950','Hepatitis Be Antigen','HBeAg',1,'2010-12-05 00:00:00','0001','Y','021'),
 (154,147,'002952','Anti-HIV I & II (Antibodies)','aHIV',1,'2010-12-05 00:00:00','0001','Y','aHIV'),
 (155,148,'001859','Rubella IgG','Rub G',1,'2010-12-05 00:00:00','0001','Y','025'),
 (156,149,'001860','Rubella IgM','Rub M',1,'2010-12-05 00:00:00','0001','Y','026'),
 (157,150,'001799','Toxoplasma IgG Antibodies','TOX G',1,'2010-12-05 00:00:00','0001','Y','027');
INSERT INTO `mi_ttestattribute` (`AttributeID`,`Machine_testid`,`LIMSAttributeID`,`LIMSAttributeName`,`MachineAttributeName`,`EnteredBy`,`EnteredOn`,`ClientId`,`Active`,`MachineAttributeCode`) VALUES 
 (158,151,'001800','Toxplasma IgM Antibodies','TOX M',1,'2010-12-05 00:00:00','0001','Y','028'),
 (159,152,'001674','Serum Ferritin','FERR',1,'2010-12-05 00:00:00','0001','Y','031'),
 (160,153,'001750','Vit B12','B12',1,'2010-12-05 00:00:00','0001','Y','032'),
 (161,154,'001987','S. Folate','FOLAT',1,'2010-12-05 00:00:00','0001','Y','033'),
 (162,155,'003483','Plasma Cortisol (evening)','CORT E',1,'2010-12-05 00:00:00','0001','Y','034'),
 (163,155,'003483','Plasma Cortisol (evening)','CORT E',1,'2010-12-05 00:00:00','0001','Y','034'),
 (164,155,'003484','Plasma Cortisol (morning)','CORT M',1,'2010-12-05 00:00:00','0001','Y','034'),
 (165,156,'001699','Prostate Specific Antigen ','PSA',1,'2010-12-05 00:00:00','0001','Y','036'),
 (166,157,'001697','CA -125','CA125',1,'2010-12-05 00:00:00','0001','Y','038'),
 (167,158,'002970','CA 15-3','CA153',1,'2010-12-05 00:00:00','0001','Y','039');
INSERT INTO `mi_ttestattribute` (`AttributeID`,`Machine_testid`,`LIMSAttributeID`,`LIMSAttributeName`,`MachineAttributeName`,`EnteredBy`,`EnteredOn`,`ClientId`,`Active`,`MachineAttributeCode`) VALUES 
 (168,158,'002970','CA 15-3','CA153',1,'2010-12-05 00:00:00','0001','Y','039');
/*!40000 ALTER TABLE `mi_ttestattribute` ENABLE KEYS */;


--
-- Table structure for table `mi`.`mi_ttests`
--

DROP TABLE IF EXISTS `mi_ttests`;
CREATE TABLE `mi_ttests` (
  `Machine_testid` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Lims_testid` char(6) NOT NULL,
  `Lims_test_name` varchar(200) NOT NULL,
  `Machine_Test_name` varchar(200) NOT NULL,
  `LOINC_code` varchar(45) DEFAULT NULL,
  `Instrumentid` int(10) unsigned NOT NULL,
  `EnteredBy` int(10) unsigned NOT NULL,
  `EnteredOn` datetime NOT NULL,
  `ClientID` char(4) NOT NULL,
  `Active` char(1) NOT NULL,
  `DeptID` char(3) DEFAULT NULL,
  `MachineTestCode` varchar(20) NOT NULL,
  PRIMARY KEY (`Machine_testid`)
) ENGINE=InnoDB AUTO_INCREMENT=159 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `mi`.`mi_ttests`
--

/*!40000 ALTER TABLE `mi_ttests` DISABLE KEYS */;
INSERT INTO `mi_ttests` (`Machine_testid`,`Lims_testid`,`Lims_test_name`,`Machine_Test_name`,`LOINC_code`,`Instrumentid`,`EnteredBy`,`EnteredOn`,`ClientID`,`Active`,`DeptID`,`MachineTestCode`) VALUES 
 (9,'000805','Albumin','Albumin','',10,1,'2010-10-13 00:00:00','0001','Y','006','ALB'),
 (10,'000804','Alkaline Phosphatase','Alkaline Phosphatase','',10,1,'2010-10-13 00:00:00','0001','Y','006','ALP'),
 (11,'001081','Plasma Ammonia','Plasma Ammonia','',10,1,'2010-10-13 00:00:00','0001','Y','006','AMON'),
 (12,'000919','Serum Amylase','Serum Amylase','',10,1,'2010-10-13 00:00:00','0001','Y','006','AMY'),
 (13,'000859','Aspartate transaminase (AST)','Aspartate transaminase (AST)','',10,1,'2010-10-13 00:00:00','0001','Y','006','AST'),
 (14,'000813','Blood Urea','Blood Urea','',10,1,'2010-10-13 00:00:00','0001','Y','006','BUN'),
 (15,'000798','Calcium Total','Calcium Total','',10,1,'2010-10-13 00:00:00','0001','Y','006','CA'),
 (16,'000817','Total Cholesterol','Total Cholesterol','',10,1,'2010-10-13 00:00:00','0001','Y','006','CHOL'),
 (17,'000809','CPK','CPK','',10,1,'2010-10-13 00:00:00','0001','Y','006','CK');
INSERT INTO `mi_ttests` (`Machine_testid`,`Lims_testid`,`Lims_test_name`,`Machine_Test_name`,`LOINC_code`,`Instrumentid`,`EnteredBy`,`EnteredOn`,`ClientID`,`Active`,`DeptID`,`MachineTestCode`) VALUES 
 (18,'000814','Serum Creatinine','Serum Creatinine','',10,1,'2010-10-13 00:00:00','0001','Y','006','Crea'),
 (19,'000915','Bilirubin Direct','Bilirubin Direct','',10,1,'2010-10-13 00:00:00','0001','Y','006','DBIL'),
 (20,'000898','Glucose (R)','Glucose (R)','',10,1,'2010-10-13 00:00:00','0001','Y','006','GLU'),
 (21,'000812','LDH','LDH','',10,1,'2010-10-13 00:00:00','0001','Y','006','LDH'),
 (22,'000801','Magnesium','Magnesium','',10,1,'2010-10-13 00:00:00','0001','Y','006','MG'),
 (23,'000800','Phosphorous (PO4```)','Phosphorous (PO4```)','',10,1,'2010-10-13 00:00:00','0001','Y','006','PHOS'),
 (24,'000802','Bilirubin Total','Bilirubin Total','',10,1,'2010-10-13 00:00:00','0001','Y','006','TBIL'),
 (25,'000818','Triglyceride','Triglyceride','',10,1,'2010-10-13 00:00:00','0001','Y','006','TGL'),
 (26,'000815','Uric Acid','Uric Acid','',10,1,'2010-10-13 00:00:00','0001','Y','006','URCA');
INSERT INTO `mi_ttests` (`Machine_testid`,`Lims_testid`,`Lims_test_name`,`Machine_Test_name`,`LOINC_code`,`Instrumentid`,`EnteredBy`,`EnteredOn`,`ClientID`,`Active`,`DeptID`,`MachineTestCode`) VALUES 
 (27,'000822','CEA (Carcinoemberyonic Antigen)','CEA (Carcinoemberyonic Antigen)','',9,1,'2010-10-13 00:00:00','0001','Y','006','CEA'),
 (28,'001136','Cytomegalovirus  (CMV) IgG','Cytomegalovirus (CMV) IgG','',9,1,'2010-10-13 00:00:00','0001','Y','002','CVG'),
 (29,'001137','Cytomegalovirus  (CMV) IgM','Cytomegalovirus (CMV) IgM','',9,1,'2010-10-13 00:00:00','0001','Y','002','CMM A'),
 (30,'001166','Plasma Cortisol','Plasma Cortisol','',9,1,'2010-10-13 00:00:00','0001','Y','010','COR'),
 (31,'001142','Estradiol ARC','Estradiol','',9,1,'2010-10-13 00:00:00','0001','Y','010','E2'),
 (32,'000868','fT4','fT4','',9,1,'2010-10-13 00:00:00','0001','Y','010','F4'),
 (33,'000848','Serum Ferritin','Serum Ferritin','',9,1,'2010-10-13 00:00:00','0001','Y','006','FER'),
 (34,'001144','FSH ARC','FSH','',9,1,'2010-10-13 00:00:00','0001','Y','010','FSH'),
 (35,'001067','ß-hCG (Roche)','ß-hCG','',9,1,'2010-10-13 00:00:00','0001','Y','010','HCG');
INSERT INTO `mi_ttests` (`Machine_testid`,`Lims_testid`,`Lims_test_name`,`Machine_Test_name`,`LOINC_code`,`Instrumentid`,`EnteredBy`,`EnteredOn`,`ClientID`,`Active`,`DeptID`,`MachineTestCode`) VALUES 
 (36,'001183','H.pylori lgG antibodies','H.pylori lgG antibodies','',9,1,'2010-10-13 00:00:00','0001','Y','002','HPG'),
 (37,'001147','Plasma Parathyroid Hormone (PTH) intact','Plasma Parathyroid Hormone (PTH) intact','',9,1,'2010-10-13 00:00:00','0001','Y','010','iPT'),
 (38,'001143','LH ARC','LH','',9,1,'2010-10-13 00:00:00','0001','Y','010','LH'),
 (39,'000821','CA -125','CA-125','',9,1,'2010-10-13 00:00:00','0001','Y','006','OV'),
 (40,'000823','Prostate Specific Antigen (PSA)','Prostate Specific Antigen (PSA)','',9,1,'2010-10-13 00:00:00','0001','Y','006','PSA'),
 (41,'000863','TSH','TSH','',9,1,'2010-10-13 00:00:00','0001','Y','010','RTH'),
 (42,'000865','T3 Total','T3 Total','',9,1,'2010-10-13 00:00:00','0001','Y','010','T3'),
 (43,'000871','Testosterone','Testosterone','',9,1,'2010-10-13 00:00:00','0001','Y','010','TES'),
 (44,'001182','Thyroglobulin','Thyroglobulin','',9,1,'2010-10-13 00:00:00','0001','Y','002','TG');
INSERT INTO `mi_ttests` (`Machine_testid`,`Lims_testid`,`Lims_test_name`,`Machine_Test_name`,`LOINC_code`,`Instrumentid`,`EnteredBy`,`EnteredOn`,`ClientID`,`Active`,`DeptID`,`MachineTestCode`) VALUES 
 (45,'001140','Troponin I','Troponin I','',9,1,'2010-10-13 00:00:00','0001','Y','006','TPI'),
 (46,'000845','Toxoplasma IgG Antibodies','Toxoplasma IgG Antibodies','',9,1,'2010-10-13 00:00:00','0001','Y','002','TXP'),
 (47,'000846','Toxoplasma IgM Antibodies','Toxoplasma IgM Antibodies','',9,1,'2010-10-13 00:00:00','0001','Y','002','TXU'),
 (48,'001181','Plasma Valproic Acid','Plasma Valproic Acid','',9,1,'2010-10-13 00:00:00','0001','Y','006','VAL'),
 (49,'000803','Alanine Transaminase (ALT)','ALT','',8,1,'2010-10-29 00:00:00','0001','Y','006','8'),
 (50,'000863','TSH','TSH','',5,1,'2010-11-02 00:00:00','0001','Y','010','10'),
 (51,'000871','Testosterone','Testo','',5,1,'2010-11-02 00:00:00','0001','Y','010','110'),
 (52,'001145','Progesterone ARC','Prog','',5,1,'2010-11-02 00:00:00','0001','Y','010','120'),
 (53,'000864','Prolactin','PRL','',5,1,'2010-11-02 00:00:00','0001','Y','010','130');
INSERT INTO `mi_ttests` (`Machine_testid`,`Lims_testid`,`Lims_test_name`,`Machine_Test_name`,`LOINC_code`,`Instrumentid`,`EnteredBy`,`EnteredOn`,`ClientID`,`Active`,`DeptID`,`MachineTestCode`) VALUES 
 (54,'001143','LH ARC','LH','',5,1,'2010-11-02 00:00:00','0001','Y','010','140'),
 (55,'001144','FSH ARC','FSH','',5,1,'2010-11-02 00:00:00','0001','Y','010','150'),
 (56,'001067','ß-hCG (Roche)','HCG-BETA','',5,1,'2010-11-02 00:00:00','0001','Y','010','761'),
 (57,'000868','fT4','FT4','',5,1,'2010-11-02 00:00:00','0001','Y','010','30'),
 (58,'000822','CEA (Carcinoemberyonic Antigen)','CEA','',5,1,'2010-11-02 00:00:00','0001','Y','006','301'),
 (59,'001069','Alpha-fetoproteins (AFP)','AFP','',5,1,'2010-11-02 00:00:00','0001','Y','006','311'),
 (60,'000823','Prostate Specific Antigen (PSA)','PSA','',5,1,'2010-11-02 00:00:00','0001','Y','006','320'),
 (61,'000821','CA -125','CA-125','',5,1,'2010-11-02 00:00:00','0001','Y','006','341'),
 (62,'001032','Hepatitis B Surface Antigen','HBSAG','',5,1,'2010-11-02 00:00:00','0001','Y','002','400'),
 (63,'001039','Anti-Hepatitis C (HCV) Antibodies','A-HCV','',5,1,'2010-11-02 00:00:00','0001','Y','002','420');
INSERT INTO `mi_ttests` (`Machine_testid`,`Lims_testid`,`Lims_test_name`,`Machine_Test_name`,`LOINC_code`,`Instrumentid`,`EnteredBy`,`EnteredOn`,`ClientID`,`Active`,`DeptID`,`MachineTestCode`) VALUES 
 (64,'001040','Anti-HIV Antibodies','A-HIV','',5,1,'2010-11-02 00:00:00','0001','Y','002','490'),
 (65,'000865','T3 Total','T3','',5,1,'2010-11-02 00:00:00','0001','Y','010','50'),
 (66,'000891','Vitamin B12','B12','',5,1,'2010-11-02 00:00:00','0001','Y','006','600'),
 (67,'000849','Serum Folate','FOL','',5,1,'2010-11-02 00:00:00','0001','Y','006','612'),
 (68,'001113','Immunoglobulin E (IgE)','IGE','',5,1,'2010-11-02 00:00:00','0001','Y','006','630'),
 (69,'001145','Progesterone ARC','PROG','',4,1,'2010-11-02 00:00:00','0001','Y','010','191'),
 (70,'001034','Hepatitis Be Antigen','HBeAg','',4,1,'2010-11-02 00:00:00','0001','Y','002','301'),
 (71,'001033','Anti-Hepatitis B Suface Antibodies','Anti-HBs','',4,1,'2010-11-02 00:00:00','0001','Y','002','131'),
 (72,'000848','Serum Ferritin','Ferritin','',4,1,'2010-11-02 00:00:00','0001','Y','006','61'),
 (73,'000871','Testosterone','Testost','',4,1,'2010-11-02 00:00:00','0001','Y','010','231');
INSERT INTO `mi_ttests` (`Machine_testid`,`Lims_testid`,`Lims_test_name`,`Machine_Test_name`,`LOINC_code`,`Instrumentid`,`EnteredBy`,`EnteredOn`,`ClientID`,`Active`,`DeptID`,`MachineTestCode`) VALUES 
 (74,'001175','Anti-TP antibodies','Syphilis','',4,1,'2010-11-02 00:00:00','0001','Y','002','561'),
 (75,'001069','Alpha-fetoproteins (AFP)','AFP_2','',4,1,'2010-11-02 00:00:00','0001','Y','006','2'),
 (76,'001032','Hepatitis B Surface Antigen','HBsAg','',4,1,'2010-11-02 00:00:00','0001','Y','002','149'),
 (77,'001143','LH ARC','_LH','',4,1,'2010-11-02 00:00:00','0001','Y','010','641'),
 (78,'001037','Anti-Hepatitis B core IgM Antibodies','HBcAb-IgM','',4,1,'2010-11-02 00:00:00','0001','Y','002','281'),
 (79,'001144','FSH ARC','FSH','',4,1,'2010-11-02 00:00:00','0001','Y','010','81'),
 (80,'001035','Anti-Hepatitis Be Antibodies','Anti-HBe','',4,1,'2010-11-02 00:00:00','0001','Y','002','291'),
 (81,'001067','ß-hCG (Roche)','BETAhCG','',4,1,'2010-11-02 00:00:00','0001','Y','010','651'),
 (82,'001166','Plasma Cortisol','Cortisol','',4,1,'2010-11-02 00:00:00','0001','Y','010','601'),
 (83,'001040','Anti-HIV Antibodies','HIV AG/AB','',4,1,'2010-11-02 00:00:00','0001','Y','002','721');
INSERT INTO `mi_ttests` (`Machine_testid`,`Lims_testid`,`Lims_test_name`,`Machine_Test_name`,`LOINC_code`,`Instrumentid`,`EnteredBy`,`EnteredOn`,`ClientID`,`Active`,`DeptID`,`MachineTestCode`) VALUES 
 (84,'000823','Prostate Specific Antigen (PSA)','Total PSA','',4,1,'2010-11-02 00:00:00','0001','Y','006','211'),
 (85,'001153','Anti-Hepatitis B core Total Antibodies ARC','Anti-HBcII','',4,1,'2010-11-02 00:00:00','0001','Y','002','580'),
 (86,'000864','Prolactin','Prolactin','',4,1,'2010-11-02 00:00:00','0001','Y','010','201'),
 (87,'001039','Anti-Hepatitis C (HCV) Antibodies','Anti-HCV','',4,1,'2010-11-02 00:00:00','0001','Y','002','161'),
 (88,'000970','BNP','_BNP','',4,1,'2010-11-02 00:00:00','0001','Y','006','739'),
 (89,'001038','Anti-Hepatitis A IgM Antibodies','HAVAB IgM','',4,1,'2010-11-02 00:00:00','0001','Y','002','321'),
 (90,'001040','Anti-HIV Antibodies','HIV1_2_g0','',3,1,'2010-11-02 00:00:00','0001','Y','002','829'),
 (91,'001136','Cytomegalovirus  (CMV) IgG','CMV-G','',3,1,'2010-11-02 00:00:00','0001','Y','002','743'),
 (92,'001137','Cytomegalovirus  (CMV) IgM','CMV-M','',3,1,'2010-11-02 00:00:00','0001','Y','002','744');
INSERT INTO `mi_ttests` (`Machine_testid`,`Lims_testid`,`Lims_test_name`,`Machine_Test_name`,`LOINC_code`,`Instrumentid`,`EnteredBy`,`EnteredOn`,`ClientID`,`Active`,`DeptID`,`MachineTestCode`) VALUES 
 (93,'000863','TSH','TSH_3GEN','',3,1,'2010-11-02 00:00:00','0001','Y','010','213'),
 (94,'000865','T3 Total','TT3-Mab','',3,1,'2010-11-02 00:00:00','0001','Y','010','276'),
 (95,'001039','Anti-Hepatitis C (HCV) Antibodies','HCV-3','',3,1,'2010-11-02 00:00:00','0001','Y','002','841'),
 (96,'001144','FSH ARC','FSH','',3,1,'2010-11-02 00:00:00','0001','Y','010','37'),
 (97,'001143','LH ARC','LH','',3,1,'2010-11-02 00:00:00','0001','Y','010','25'),
 (98,'000864','Prolactin','PRL-3IS','',3,1,'2010-11-02 00:00:00','0001','Y','010','55'),
 (99,'001145','Progesterone ARC','PROG','',3,1,'2010-11-02 00:00:00','0001','Y','010','92'),
 (100,'000892','D-dimer','D-dimer','',3,1,'2010-11-02 00:00:00','0001','Y','007','868'),
 (101,'001033','Anti-Hepatitis B Suface Antibodies','AUSAB','',3,1,'2010-11-02 00:00:00','0001','Y','002','118'),
 (102,'000823','Prostate Specific Antigen (PSA)','PSA-Total','',3,1,'2010-11-02 00:00:00','0001','Y','006','443');
INSERT INTO `mi_ttests` (`Machine_testid`,`Lims_testid`,`Lims_test_name`,`Machine_Test_name`,`LOINC_code`,`Instrumentid`,`EnteredBy`,`EnteredOn`,`ClientID`,`Active`,`DeptID`,`MachineTestCode`) VALUES 
 (103,'001067','ß-hCG (Roche)','B-hCG','',3,1,'2010-11-02 00:00:00','0001','Y','010','16'),
 (104,'000871','Testosterone','Testostrn','',3,1,'2010-11-02 00:00:00','0001','Y','010','90'),
 (105,'001035','Anti-Hepatitis Be Antibodies','Anti-HBe','',3,1,'2010-11-02 00:00:00','0001','Y','002','192'),
 (106,'000848','Serum Ferritin','Ferritin','',3,1,'2010-11-02 00:00:00','0001','Y','006','321'),
 (107,'001153','Anti-Hepatitis B core Total Antibodies ARC','CORE','',3,1,'2010-11-02 00:00:00','0001','Y','002','126'),
 (108,'001034','Hepatitis Be Antigen','HBe_2','',3,1,'2010-11-02 00:00:00','0001','Y','002','193'),
 (109,'001032','Hepatitis B Surface Antigen','HBsAg','',3,1,'2010-11-02 00:00:00','0001','Y','002','106'),
 (110,'001070','Plasma Cyclosporin (CsA) level','Cyclo','',3,1,'2010-11-02 00:00:00','0001','Y','006','693'),
 (111,'000868','fT4','Free-T4','',3,1,'2010-11-02 00:00:00','0001','Y','010','219');
INSERT INTO `mi_ttests` (`Machine_testid`,`Lims_testid`,`Lims_test_name`,`Machine_Test_name`,`LOINC_code`,`Instrumentid`,`EnteredBy`,`EnteredOn`,`ClientID`,`Active`,`DeptID`,`MachineTestCode`) VALUES 
 (112,'001069','Alpha-fetoproteins (AFP)','AFP','',3,1,'2010-11-02 00:00:00','0001','Y','006','428'),
 (113,'000807','Plasma Digoxin Level','Digoxin','',3,1,'2010-11-02 00:00:00','0001','Y','006','601'),
 (114,'001142','Estradiol ARC','Estradiol','',3,1,'2010-11-02 00:00:00','0001','Y','010','71'),
 (115,'000970','BNP','BNP','',3,1,'2010-11-02 00:00:00','0001','Y','006','377'),
 (116,'000923','Rubella IgG','RubellaG','',3,1,'2010-11-02 00:00:00','0001','Y','002','723'),
 (117,'000924','Rubella IgM','RubellaM','',3,1,'2010-11-02 00:00:00','0001','Y','002','754'),
 (118,'000845','Toxoplasma IgG Antibodies','TOXO-G','',3,1,'2010-11-02 00:00:00','0001','Y','002','712'),
 (119,'000846','Toxoplasma IgM Antibodies','TOXO-M','',3,1,'2010-11-02 00:00:00','0001','Y','002','734'),
 (120,'001140','Troponin I','TnI-ADV','',3,1,'2010-11-02 00:00:00','0001','Y','006','313'),
 (121,'001166','Plasma Cortisol','Cortiso','',3,1,'2010-11-02 00:00:00','0001','Y','010','848');
INSERT INTO `mi_ttests` (`Machine_testid`,`Lims_testid`,`Lims_test_name`,`Machine_Test_name`,`LOINC_code`,`Instrumentid`,`EnteredBy`,`EnteredOn`,`ClientID`,`Active`,`DeptID`,`MachineTestCode`) VALUES 
 (122,'001038','Anti-Hepatitis A IgM Antibodies','HAVABM_2','',3,1,'2010-11-02 00:00:00','0001','Y','002','140'),
 (123,'001037','Anti-Hepatitis B core IgM Antibodies','COREM','',3,1,'2010-11-02 00:00:00','0001','Y','002','165'),
 (124,'000860','Myoglobin','Myoglobin','',3,1,'2010-11-02 00:00:00','0001','Y','006','350'),
 (125,'000969','Plasma Vancomycin Level','Vanco_II','',3,1,'2010-11-02 00:00:00','0001','Y','006','668'),
 (126,'000968','Plasma Gentamicin Level','Gent','',3,1,'2010-11-02 00:00:00','0001','Y','006','645'),
 (127,'000803','Alanine Transaminase (ALT)','ALT','',10,1,'2010-11-02 00:00:00','0001','Y','006','ALT'),
 (128,'001039','Anti-Hepatitis C (HCV) Antibodies','aHCV','',17,1,'2010-12-05 00:00:00','0001','Y','002','023'),
 (129,'001140','Troponin I','TropI','',17,1,'2010-12-05 00:00:00','0001','Y','006','030'),
 (130,'000863','TSH','TSH','',17,1,'2010-12-05 00:00:00','0001','Y','010','001');
INSERT INTO `mi_ttests` (`Machine_testid`,`Lims_testid`,`Lims_test_name`,`Machine_Test_name`,`LOINC_code`,`Instrumentid`,`EnteredBy`,`EnteredOn`,`ClientID`,`Active`,`DeptID`,`MachineTestCode`) VALUES 
 (131,'000865','T3 Total','TT3','',17,1,'2010-12-05 00:00:00','0001','Y','010','003'),
 (132,'000868','fT4','FT4','',17,1,'2010-12-05 00:00:00','0001','Y','010','004'),
 (133,'001142','Estradiol ARC','E2','',17,1,'2010-12-05 00:00:00','0001','Y','010','008'),
 (134,'001143','LH ARC','LH','',17,1,'2010-12-05 00:00:00','0001','Y','010','009'),
 (135,'001144','FSH ARC','FSH','',17,1,'2010-12-05 00:00:00','0001','Y','010','010'),
 (136,'000864','Prolactin','PROL','',17,1,'2010-12-05 00:00:00','0001','Y','010','011'),
 (137,'001145','Progesterone ARC','PROG','',17,1,'2010-12-05 00:00:00','0001','Y','010','012'),
 (138,'001067','ß-hCG (Roche)','B-hCG','',17,1,'2010-12-05 00:00:00','0001','Y','010','013'),
 (139,'000871','Testosterone','TESTO','',17,1,'2010-12-05 00:00:00','0001','Y','010','014'),
 (140,'001069','Alpha-fetoproteins (AFP)','AFP','',17,1,'2010-12-05 00:00:00','0001','Y','006','015');
INSERT INTO `mi_ttests` (`Machine_testid`,`Lims_testid`,`Lims_test_name`,`Machine_Test_name`,`LOINC_code`,`Instrumentid`,`EnteredBy`,`EnteredOn`,`ClientID`,`Active`,`DeptID`,`MachineTestCode`) VALUES 
 (141,'000822','CEA (Carcinoemberyonic Antigen)','CEA','',17,1,'2010-12-05 00:00:00','0001','Y','006','016'),
 (142,'001032','Hepatitis B Surface Antigen','HBsAg','',17,1,'2010-12-05 00:00:00','0001','Y','002','017'),
 (143,'001033','Anti-Hepatitis B Suface Antibodies','aHBS','',17,1,'2010-12-05 00:00:00','0001','Y','002','018'),
 (144,'001153','Anti-Hepatitis B core Total Antibodies ARC','aHBC','',17,1,'2010-12-05 00:00:00','0001','Y','002','019'),
 (145,'001037','Anti-Hepatitis B core IgM Antibodies','aHBcM','',17,1,'2010-12-05 00:00:00','0001','Y','002','020'),
 (146,'001034','Hepatitis Be Antigen','HBeAg','',17,1,'2010-12-05 00:00:00','0001','Y','002','021'),
 (147,'001040','Anti-HIV Antibodies','aHIV','',17,1,'2010-12-05 00:00:00','0001','Y','002','024'),
 (148,'000923','Rubella IgG','Rub G','',17,1,'2010-12-05 00:00:00','0001','Y','002','025'),
 (149,'000924','Rubella IgM','Rub M','',17,1,'2010-12-05 00:00:00','0001','Y','002','026');
INSERT INTO `mi_ttests` (`Machine_testid`,`Lims_testid`,`Lims_test_name`,`Machine_Test_name`,`LOINC_code`,`Instrumentid`,`EnteredBy`,`EnteredOn`,`ClientID`,`Active`,`DeptID`,`MachineTestCode`) VALUES 
 (150,'000845','Toxoplasma IgG Antibodies','TOX G','',17,1,'2010-12-05 00:00:00','0001','Y','002','027'),
 (151,'000846','Toxoplasma IgM Antibodies','TOX M','',17,1,'2010-12-05 00:00:00','0001','Y','002','028'),
 (152,'000848','Serum Ferritin','FERR','',17,1,'2010-12-05 00:00:00','0001','Y','006','031'),
 (153,'000891','Vitamin B12','B12','',17,1,'2010-12-05 00:00:00','0001','Y','006','032'),
 (154,'000849','Serum Folate','FOLAT','',17,1,'2010-12-05 00:00:00','0001','Y','006','033'),
 (155,'001166','Plasma Cortisol','CORT','',17,1,'2010-12-05 00:00:00','0001','Y','010','034'),
 (156,'000823','Prostate Specific Antigen (PSA)','PSA','',17,1,'2010-12-05 00:00:00','0001','Y','006','036'),
 (157,'000821','CA -125','CA125','',17,1,'2010-12-05 00:00:00','0001','Y','006','038'),
 (158,'001047','CA 15-3','CA153','',17,1,'2010-12-05 00:00:00','0001','Y','006','039');
/*!40000 ALTER TABLE `mi_ttests` ENABLE KEYS */;


--
-- Function `mi`.`isnumeric`
--

DROP FUNCTION IF EXISTS `isnumeric`;
DELIMITER $$

CREATE DEFINER=`mi`@`%` FUNCTION `isnumeric`(val varchar(1024)) RETURNS tinyint(1)
    DETERMINISTIC
return val regexp '^(-|\\+)?([0-9]+\\.[0-9]*|[0-9]*\\.[0-9]+|[0-9]+)$' $$

DELIMITER ;
/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
