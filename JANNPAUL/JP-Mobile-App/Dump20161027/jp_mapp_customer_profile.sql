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
-- Table structure for table `customer_profile`
--

DROP TABLE IF EXISTS `customer_profile`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `customer_profile` (
  `mem_id` int(11) NOT NULL AUTO_INCREMENT,
  `email` varchar(120) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `fname` varchar(60) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `lname` varchar(60) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `address` varchar(250) CHARACTER SET utf8 NOT NULL DEFAULT '(Not Set)',
  `contact` varchar(60) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `picture` varchar(250) CHARACTER SET utf8 DEFAULT NULL,
  `gender` varchar(10) CHARACTER SET utf8 DEFAULT NULL,
  `member_since` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `country` varchar(80) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `udid` varchar(120) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `city` varchar(120) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`mem_id`),
  UNIQUE KEY `email` (`email`),
  UNIQUE KEY `email_3` (`email`),
  UNIQUE KEY `udid` (`udid`),
  KEY `email_2` (`email`)
) ENGINE=MyISAM AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `customer_profile`
--

LOCK TABLES `customer_profile` WRITE;
/*!40000 ALTER TABLE `customer_profile` DISABLE KEYS */;
INSERT INTO `customer_profile` VALUES (1,'johnny.appleseed@mac.com','iPhone','Airline','(Not Set)','314159265358979323',NULL,'Mr.','2016-07-21 06:00:07','Singapore','CB7587B3-3E40-4B30-8CB6-98948194EAB0','New York, NY'),(2,'314159265358979323','314159265358979323','314159265358979323','(Not Set)','314159265358979323',NULL,'Mr.','2016-09-01 06:25:42','Singapore','63CE2C38-9B58-4EDC-B21C-6A32BEA12D5E','314159265358979323'),(3,'','Richard','','(Not Set)','',NULL,'Mr.','2016-09-09 00:13:39','Singapore','6BE724A2-8789-492A-B037-2E6620A60D61','City Hall'),(4,'tnphmhh@msn.com','Tét','To','(Not Set)','809053652',NULL,'Mr.','2016-09-12 07:55:11','Singapore','2B232C6F-94E8-43D7-9E7B-D1B97205315D','Hjsh'),(5,'aliceinjannpaul@gmail.com','Alice','Cai','(Not Set)','9825203',NULL,'Mrs.','2016-09-15 07:11:14','Singapore','B6437825-67B4-4661-9C5D-08E98980796D',''),(6,'th@ph.com','t','p.','(Not Set)','09053187',NULL,'Mr.','2016-09-22 03:37:03','Singapore','11bd616a9cf6d145','hcm');
/*!40000 ALTER TABLE `customer_profile` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2016-10-27 22:48:56
