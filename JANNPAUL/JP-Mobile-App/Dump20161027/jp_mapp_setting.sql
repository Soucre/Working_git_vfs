CREATE DATABASE  IF NOT EXISTS `jp_mapp` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `jp_mapp`;
-- MySQL dump 10.13  Distrib 5.7.12, for Win64 (x86_64)
--
-- Host: localhost    Database: jp_mapp
-- ------------------------------------------------------
-- Server version	5.7.15-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `setting`
--

DROP TABLE IF EXISTS `setting`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `setting` (
  `key` varchar(60) CHARACTER SET utf16 COLLATE utf16_unicode_ci NOT NULL,
  `value` text CHARACTER SET ucs2 COLLATE ucs2_unicode_ci NOT NULL,
  `description` varchar(120) CHARACTER SET ucs2 COLLATE ucs2_unicode_ci NOT NULL,
  `title` varchar(120) CHARACTER SET utf32 COLLATE utf32_unicode_ci NOT NULL,
  `regex` varchar(520) NOT NULL,
  `required` tinyint(1) NOT NULL,
  `group_name` varchar(120) CHARACTER SET utf16 COLLATE utf16_unicode_ci NOT NULL DEFAULT 'General',
  `order_by` int(11) NOT NULL,
  PRIMARY KEY (`key`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COMMENT='Application system settings';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `setting`
--

LOCK TABLES `setting` WRITE;
/*!40000 ALTER TABLE `setting` DISABLE KEYS */;
INSERT INTO `setting` VALUES ('settings-date-format','M. Y','Short date display format (ex: M/D/Y)','Date format','[mMyYdD .]{1,10}',1,'Display Format',0),('default-module-name','dashboard','Default start-up page','Start-up','[a-z]{1,20}',1,'Application',1),('application-name','JannPaul Diamonds','Application full name','Name','',1,'General',0),('company','JannPaul Diamonds','Enter your company\'s name','Company','',1,'General',1),('slogan','Super Ideal Cut Hearts & Arrows Diamond','Your company slogan','Slogan','',0,'General',2),('website','http://www.jannpaul.com/','Enter your company website','Website','',1,'General',3),('logo-mini','<b>JP</b>','Short application\'s name as logo','Short logo','',1,'General',4),('logo-lg','<b>JANNPAUL</b> DIAMONS','Long application\'s name as logo','Long Logo','',1,'General',5),('mobile-footer','Please email us at sales@jannpaul.com or call +65 6733 2925 or please visit us at 545 Orchard Road #01-23, Far East Shopping Centre, SG 238882','The footer text will be displayed on the mobile application','Footer','',1,'Mobile',1);
/*!40000 ALTER TABLE `setting` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2016-10-27 22:48:54
