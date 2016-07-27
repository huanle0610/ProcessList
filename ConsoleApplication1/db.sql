-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               5.5.24-log - MySQL Community Server (GPL)
-- Server OS:                    Win64
-- HeidiSQL Version:             9.3.0.5092
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Dumping database structure for test
CREATE DATABASE IF NOT EXISTS `process_info` /*!40100 DEFAULT CHARACTER SET utf8 COLLATE utf8_bin */;
USE `process_info`;

-- Dumping structure for table test.processes
CREATE TABLE IF NOT EXISTS `processes` (
  `pid` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `hostname` varchar(50) DEFAULT NULL,
  `event_id` int(10) unsigned DEFAULT NULL,
  `session_id` int(10) unsigned DEFAULT NULL,
  `process_id` int(10) unsigned DEFAULT NULL,
  `caption` varchar(90) DEFAULT NULL,
  `owner` varchar(90) DEFAULT NULL,
  `executable_path` varchar(255) DEFAULT NULL,
  `creation_date` datetime NOT NULL,
  `update_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`pid`)
) ENGINE=InnoDB AUTO_INCREMENT=2240 DEFAULT CHARSET=utf8;

-- Data exporting was unselected.
-- Dumping structure for table test.sessions
CREATE TABLE IF NOT EXISTS `sessions` (
  `sid` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `hostname` varchar(50) NOT NULL DEFAULT '0',
  `event_id` int(11) NOT NULL DEFAULT '0',
  `session_id` int(11) NOT NULL DEFAULT '0',
  `account` varchar(65) NOT NULL DEFAULT '0',
  `update_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`sid`)
) ENGINE=InnoDB AUTO_INCREMENT=34 DEFAULT CHARSET=utf8;

-- Data exporting was unselected.
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
