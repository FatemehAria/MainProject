-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Sep 02, 2024 at 11:50 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `main_project_db`
--

DELIMITER $$
--
-- Procedures
--
CREATE DEFINER=`root`@`localhost` PROCEDURE `prc_create_user` (IN `p_first_name` VARCHAR(50), IN `p_last_name` VARCHAR(50), IN `p_phone_number` VARCHAR(20), IN `p_password` VARCHAR(255))   BEGIN
    DECLARE existingUserId INT;
    SELECT id INTO existingUserId
    FROM users
    WHERE phone_number = p_phone_number
    LIMIT 1;

    IF existingUserId IS NOT NULL THEN
        SELECT -1 AS userId;
    ELSE
        INSERT INTO users (first_name, last_name, phone_number, password) 
        VALUES (p_first_name, p_last_name, p_phone_number, p_password);
        SELECT LAST_INSERT_ID() AS userId;
    END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `prc_delete_user` (IN `user_id` INT)   BEGIN
    UPDATE users
    SET is_deleted = 1
    WHERE id = user_id;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `prc_edit_user` (IN `user_id` INT, IN `new_first_name` VARCHAR(255), IN `new_last_name` VARCHAR(255), IN `new_phone_number` VARCHAR(20), IN `new_password` VARCHAR(255))   BEGIN
   UPDATE users
    SET
        first_name = CASE
                        WHEN new_first_name IS NULL OR new_first_name = '' OR new_first_name = 'string' THEN first_name
                        ELSE new_first_name
                     END,
        last_name = CASE
                        WHEN new_last_name IS NULL OR new_last_name = '' OR new_last_name = 'string' THEN last_name
                        ELSE new_last_name
                    END,
                    password = CASE
                        WHEN new_password IS NULL OR new_password = '' OR new_password = 'string' THEN password
                        ELSE new_password
                     END,
        phone_number = CASE
                        WHEN new_phone_number IS NULL OR new_phone_number = '' OR new_phone_number = 'string' THEN phone_number
                        ELSE new_phone_number
                       END
    WHERE id = user_id;

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `prc_get_users` ()   BEGIN
SELECT id as userId, first_name as firstName , last_name as LastName , phone_number as phoneNumber , is_deleted as is_deleted FROM users WHERE is_deleted = 0;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `prc_login_user` (IN `username` VARCHAR(255), IN `p_password` VARCHAR(255))   BEGIN
	DECLARE userCount INT;
    DECLARE validUser INT;
    DECLARE deletedUser INT;
    
    SELECT COUNT(*) INTO userCount FROM users WHERE phone_number = username;
    
    IF userCount > 0 THEN
    	SELECT COUNT(*) INTO validUser FROM users WHERE phone_number = username AND BINARY password = BINARY p_password;
        IF validUser > 0 THEN
        	SELECT COUNT(*) INTO deletedUser FROM users WHERE phone_number = username AND BINARY password = BINARY p_password AND is_deleted = 1;
            IF deletedUser > 0 THEN
            	SELECT -2 as userId;
            ELSE
                SELECT id as userId, first_name as firstName, last_name as lastName, phone_number as phoneNumber
                FROM users
                WHERE phone_number = username AND BINARY password = BINARY p_password AND is_deleted = 0;
             END IF;
         ELSE
         	SELECT 0 as userId;
         END IF;
    ELSE
    	SELECT -1 as userId;
    END IF;
END$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `id` int(11) NOT NULL,
  `first_name` varchar(255) NOT NULL,
  `last_name` varchar(255) NOT NULL,
  `phone_number` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `is_deleted` bit(1) NOT NULL DEFAULT b'0',
  `created_at` timestamp NOT NULL DEFAULT current_timestamp(),
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`id`, `first_name`, `last_name`, `phone_number`, `password`, `is_deleted`, `created_at`, `updated_at`) VALUES
(29, 'فاطمه', 'آریانی', '09199075288', 'Fa123456', b'0', '2024-09-02 09:43:29', NULL),
(30, 'Fateme', 'Ariani', '09217458037', 'Aa1234567', b'1', '2024-09-02 09:45:19', NULL);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=31;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
