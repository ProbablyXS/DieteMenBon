-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Mar 09, 2023 at 04:35 PM
-- Server version: 10.4.27-MariaDB
-- PHP Version: 8.1.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `dietemenbon`
--

-- --------------------------------------------------------

--
-- Table structure for table `consultation`
--

CREATE TABLE `consultation` (
  `Patient_Id` int(11) NOT NULL,
  `Consultation_Id` int(11) NOT NULL,
  `Consultation_Date` date NOT NULL,
  `Price` double DEFAULT NULL,
  `Payment_Means` text DEFAULT NULL,
  `Consultation_Comment` text DEFAULT NULL,
  `Food_plan_Change` text DEFAULT NULL,
  `Giving_Document` text DEFAULT NULL,
  `Solicitation` text DEFAULT NULL,
  `Weekly_Goals` text DEFAULT NULL,
  `Poids` double DEFAULT NULL,
  `Taille` double DEFAULT NULL,
  `Ventre` double DEFAULT NULL,
  `Hanche` double DEFAULT NULL,
  `Cuisse` double DEFAULT NULL,
  `Bras` double DEFAULT NULL,
  `IMC` double DEFAULT NULL,
  `Muscle` double DEFAULT NULL,
  `Hydratation` double DEFAULT NULL,
  `Graisse_Viscerale` double DEFAULT NULL,
  `Graisse_Sous_Cutanee` double DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `login`
--

CREATE TABLE `login` (
  `login` varchar(8) NOT NULL,
  `password` varchar(16) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `login`
--

INSERT INTO `login` (`login`, `password`) VALUES
('test', 'test');

-- --------------------------------------------------------

--
-- Table structure for table `patients`
--

CREATE TABLE `patients` (
  `Patient_Id` int(11) NOT NULL,
  `History` date NOT NULL,
  `Perso_Last_Name` text DEFAULT NULL,
  `Perso_First_Name` text DEFAULT NULL,
  `Perso_Gender` text DEFAULT NULL,
  `Perso_Date_Of_Birth` date DEFAULT NULL,
  `Perso_Age` int(11) DEFAULT NULL,
  `Perso_Place_Of_Birth` text DEFAULT NULL,
  `Perso_Height` int(11) DEFAULT NULL,
  `Perso_Phone` text DEFAULT NULL,
  `Perso_Email` text DEFAULT NULL,
  `Perso_Address` text DEFAULT NULL,
  `Perso_Postal_Address` int(11) DEFAULT NULL,
  `Perso_City` text DEFAULT NULL,
  `Perso_Job` text DEFAULT NULL,
  `Perso_Doctor` text DEFAULT NULL,
  `Perso_Referencing` text DEFAULT NULL,
  `Need_Past_Need` text DEFAULT NULL,
  `Need_Present_Need` text DEFAULT NULL,
  `Need_Futur_Need` text DEFAULT NULL,
  `Habit_Call_Info` text DEFAULT NULL,
  `Habit_Weight_History` text DEFAULT NULL,
  `Habit_Food_Habit` text DEFAULT NULL,
  `Habit_Work_Schedule` text DEFAULT NULL,
  `Patho_Familly_ATCD` text DEFAULT NULL,
  `Patho_PERSONAL_ATCD` text DEFAULT NULL,
  `Patho_Treatment` text DEFAULT NULL,
  `Physio_Hormonal` text DEFAULT NULL,
  `Physio_Urine_Color` text DEFAULT NULL,
  `Physio_Digestion_problem` text DEFAULT NULL,
  `Physio_Circulation_problems` text DEFAULT NULL,
  `Weight_Weight` double DEFAULT NULL,
  `Weight_Min_Weight` double DEFAULT NULL,
  `Weight_Max_Weight` double DEFAULT NULL,
  `Weight_Weight_Balance` double DEFAULT NULL,
  `Weight_Objective_Weight` double DEFAULT NULL,
  `Bio_Glycemie` text DEFAULT NULL,
  `Bio_Ch` text DEFAULT NULL,
  `Bio_Tsh` text DEFAULT NULL,
  `Bio_Acide_Urique` text DEFAULT NULL,
  `Bio_Tg` text DEFAULT NULL,
  `Bio_Other` text DEFAULT NULL,
  `Allergy_Allergy` text DEFAULT NULL,
  `Allergy_Intolerance` text DEFAULT NULL,
  `Psycho_Tabac` text DEFAULT NULL,
  `Psycho_Tca` text DEFAULT NULL,
  `Psycho_Activity` text DEFAULT NULL,
  `Psycho_Sleep` text DEFAULT NULL,
  `Psycho_Snacking` text DEFAULT NULL,
  `Hydratation_Water` text DEFAULT NULL,
  `Hydratation_Gaz_Water` text DEFAULT NULL,
  `Hydratation_Coffee` text DEFAULT NULL,
  `Hydratation_Infusion_Thea` text DEFAULT NULL,
  `Hydratation_Alcohol` text DEFAULT NULL,
  `Hydratation_Sweet_Drink` text DEFAULT NULL,
  `Aversion_Aversion` text DEFAULT NULL,
  `Aversion_Fetish_Food` text DEFAULT NULL,
  `24H_Breakfast` text DEFAULT NULL,
  `24H_Morning_Snack` text DEFAULT NULL,
  `24H_Lunch` text DEFAULT NULL,
  `24H_Afternoon_Snack` text DEFAULT NULL,
  `24H_Diner` text DEFAULT NULL,
  `24H_Night_Snack` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `consultation`
--
ALTER TABLE `consultation`
  ADD PRIMARY KEY (`Patient_Id`,`Consultation_Id`,`Consultation_Date`);

--
-- Indexes for table `login`
--
ALTER TABLE `login`
  ADD PRIMARY KEY (`login`);

--
-- Indexes for table `patients`
--
ALTER TABLE `patients`
  ADD PRIMARY KEY (`Patient_Id`,`History`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
