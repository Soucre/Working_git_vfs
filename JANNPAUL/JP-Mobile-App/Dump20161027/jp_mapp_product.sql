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
-- Table structure for table `product`
--

DROP TABLE IF EXISTS `product`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `product` (
  `product_id` int(11) NOT NULL AUTO_INCREMENT,
  `title` varchar(250) CHARACTER SET ucs2 COLLATE ucs2_unicode_ci NOT NULL,
  `description` text CHARACTER SET ucs2 COLLATE ucs2_unicode_ci NOT NULL,
  `picture` varchar(250) CHARACTER SET ucs2 COLLATE ucs2_unicode_ci NOT NULL,
  `visible` tinyint(1) DEFAULT NULL,
  `category` varchar(120) DEFAULT NULL,
  `metal` varchar(120) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `sharing_url` varchar(520) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`product_id`)
) ENGINE=MyISAM AUTO_INCREMENT=23 DEFAULT CHARSET=latin1 COMMENT='Products';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product`
--

LOCK TABLES `product` WRITE;
/*!40000 ALTER TABLE `product` DISABLE KEYS */;
INSERT INTO `product` VALUES (1,'WHITE GOLD CLASSIC CATHEDRAL SOLITAIRE RING','A classic 4 prongs solitaire ring design. A perfect way to showcase your main diamond and stack with your wedding band.','dist/img/product/band_Front_3.jpg',1,'1','Yellow Gold','http://facebook.com'),(2,'ROSE GOLD CLASSIC CATHEDRAL SOLITAIRE RING','A classic 4 prongs solitaire ring design. A perfect way to showcase your main diamond and stack with your wedding band.','dist/img/product/band_Front_1.jpg',0,'1','Rose Gold',''),(3,'YELLOW GOLD CLASSIC CATHEDRAL SOLITAIRE RING','A classic 4 prongs solitaire ring design. A perfect way to showcase your main diamond and stack with your wedding band.','dist/img/product/band_Front_2.jpg',0,'1','Yellow Gold',''),(4,'ROSE GOLD ROPE TWIST RING','An intricate rope twist design adding some charm to your design. A beautiful setting that can be uniquely yours.','dist/img/product/band_Front_1.jpg',0,'3','Rose Gold',''),(5,'YELLOW GOLD ROPE TWIST RING','An intricate rope twist design adding some charm to your design. A beautiful setting that can be uniquely yours.','dist/img/product/band_Front_2.jpg',0,'3','Yellow Gold',''),(6,'WHITE GOLD ROPE TWIST RING','An intricate rope twist design adding some charm to your design. A beautiful setting that can be uniquely yours.','dist/img/product/123.jpg',1,'3','White Gold',''),(7,'ROSE GOLD DOUBLE BAND RING','This contemporary design has a double flattened band, giving a bold look without being heavy. It is a blend between modern and classic styles.','dist/img/product/band_Front_1.jpg',0,'1','Rose Gold',''),(8,'YELLOW GOLD DOUBLE BAND RING','This contemporary design has a double flattened band, giving a bold look without being heavy. It is a blend between modern and classic styles.','dist/img/product/band_Front_2.jpg',0,'1','Yellow Gold',''),(9,'WHITE GOLD DOUBLE BAND RING','This contemporary design has a double flattened band, giving a bold look without being heavy. It is a blend between modern and classic styles.','dist/img/product/band_Front_3.jpg',1,'1','White Gold',''),(10,'ROSE GOLD DOUBLE BAND FRONT TWIST RING','This double band twist design is a clean and modern version of the classic twist ring. It showcases the full side profile view of your main diamond.','dist/img/product/Diamond_frontNoSide 4.jpg',0,'3','Platinum',''),(11,'YELLOW GOLD DOUBLE BAND FRONT TWIST RING','This double band twist design is a clean and modern version of the classic twist ring. It showcases the full side profile view of your main diamond.','dist/img/product/Diamond_frontNoSide 5 Y.jpg',0,'1','Yellow Gold',''),(12,'WHITE GOLD DOUBLE BAND FRONT TWIST RING','This double band twist design is a clean and modern version of the classic twist ring. It showcases the full side profile view of your main diamond.','dist/img/product/Diamond_frontNoSide 6 W.jpg',1,'1','White Gold',''),(13,'ROSE GOLD INTERWINED RING WITH SIDE DIAMONDS','A trendy and modern intertwined ring with pave side diamonds. It showcases the full side profile view of your main diamond.','dist/img/product/band_Front_1.jpg',0,'1','Rose Gold',''),(14,'YELLOW GOLD INTERWINED RING WITH SIDE DIAMONDS','A trendy and modern intertwined ring with pave side diamonds. It showcases the full side profile view of your main diamond.','dist/img/product/band_Front_2.jpg',0,'1','Yellow Gold',''),(15,'WHITE GOLD INTERWINED RING WITH SIDE DIAMONDS','A trendy and modern intertwined ring with pave side diamonds. It showcases the full side profile view of your main diamond.','dist/img/product/band_Front_3.jpg',1,'1','White Gold',''),(16,'ROSE GOLD CLASSIC RING WITH HEART SHAPE FILLIGREE','A classic ring design with an intricate heart shape filigree design on the side of the prongs. Securing the main diamond are 4 heart shape prongs. Express your love with this design and win her heart!','dist/img/product/band_Front_1.jpg',0,'1','Rose Gold',''),(17,'WHITE GOLD CLASSIC RING WITH HEART SHAPE FILLIGREE','A classic ring design with an intricate heart shape filigree design on the side of the prongs. Securing the main diamond are 4 heart shape prongs. Express your love with this design and win her heart!','dist/img/product/band_Front_3.jpg',1,'1','White Gold',''),(18,'YELLOW GOLD CLASSIC RING WITH HEART SHAPE FILLIGREE','A classic ring design with an intricate heart shape filigree design on the side of the prongs. Securing the main diamond are 4 heart shape prongs. Express your love with this design and win her heart!','dist/img/product/band_Front_2.jpg',0,'1','Yellow Gold',''),(19,'WHITE GOLD DOUBLE BAND SIDE TWIST RING','This contemporary ring style has an elegant twist on its double band. There is a beautiful heart shape filigree between the prongs. Accompanying the main diamond are 4 heart shape prongs','dist/img/product/band_Front_3W.jpg',1,'1','White Gold',''),(20,'WHITE GOLD DOUBLE BAND TWIST RING WITH SIDE DIAMONDS','A beautiful ring featuring a crossover band with pave side diamonds. The bands spirals up to the main diamond, accentuating the centre diamond. Accompanying the main diamond are 4 heart shape prongs','dist/img/product/band_Front_3 - White.jpg',1,'1','White Gold',''),(21,'WHITE GOLD CLASSIC KNIFE EDGE SOLITAIRE RING','A classic and timeless style to perfectly showcase your diamond. It reveals the full side profile view of your main diamond.','dist/img/product/band_Front_3 T6P4P.jpg',1,'1','White Gold',''),(22,'WHITE GOLD FLUSH FIT TAPERED SOLITAIRE RING','This cathedral solitaire ring has a slightly thicker rounded band that creates a beautiful silhouette. The organic style gives a bold look to your ring without it being heavy.','dist/img/product/band_Front_3 T6P4P.jpg',1,'1','White Gold','');
/*!40000 ALTER TABLE `product` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2016-10-27 22:48:53
