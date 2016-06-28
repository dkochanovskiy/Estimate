-- phpMyAdmin SQL Dump
-- version 4.0.10.6
-- http://www.phpmyadmin.net
--
-- Хост: 127.0.0.1:3306
-- Время создания: Июн 28 2016 г., 14:08
-- Версия сервера: 5.5.41-log
-- Версия PHP: 5.4.35

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- База данных: `estimate`
--

-- --------------------------------------------------------

--
-- Структура таблицы `AIS`
--

CREATE TABLE IF NOT EXISTS `AIS` (
  `AIS_id` int(11) NOT NULL AUTO_INCREMENT,
  `AIS_Name` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`AIS_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=9 ;

-- --------------------------------------------------------

--
-- Структура таблицы `EdIzmer`
--

CREATE TABLE IF NOT EXISTS `EdIzmer` (
  `id_EdIzmer` int(11) NOT NULL AUTO_INCREMENT,
  `EdIzmer` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`id_EdIzmer`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=4 ;

-- --------------------------------------------------------

--
-- Структура таблицы `estimates_meta`
--

CREATE TABLE IF NOT EXISTS `estimates_meta` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `estimate_name` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `type` varchar(127) COLLATE utf8_unicode_ci NOT NULL,
  `ais` varchar(127) COLLATE utf8_unicode_ci NOT NULL,
  `year` int(11) NOT NULL,
  `month` varchar(127) COLLATE utf8_unicode_ci NOT NULL,
  `day` int(11) NOT NULL,
  `hour` int(11) NOT NULL,
  `minute` int(11) NOT NULL,
  `second` int(11) NOT NULL,
  `create_date` datetime NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=5 ;

-- --------------------------------------------------------

--
-- Структура таблицы `Sections`
--

CREATE TABLE IF NOT EXISTS `Sections` (
  `Section_id` int(11) NOT NULL AUTO_INCREMENT,
  `Section_Name` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`Section_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=12 ;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
