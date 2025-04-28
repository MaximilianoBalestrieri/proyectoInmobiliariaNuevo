-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 28-04-2025 a las 23:05:34
-- Versión del servidor: 10.4.32-MariaDB
-- Versión de PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `proyectoinmobiliaria`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `contrato`
--

CREATE TABLE `contrato` (
  `idContrato` int(11) NOT NULL,
  `dniPropietario` varchar(11) NOT NULL,
  `nombrePropietario` varchar(255) NOT NULL,
  `dniInquilino` varchar(11) NOT NULL,
  `nombreInquilino` varchar(255) NOT NULL,
  `fechaInicio` date NOT NULL,
  `fechaFinal` date NOT NULL,
  `monto` decimal(15,0) NOT NULL,
  `idInmueble` int(11) NOT NULL,
  `direccion` varchar(255) NOT NULL,
  `vigente` tinyint(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `contrato`
--

INSERT INTO `contrato` (`idContrato`, `dniPropietario`, `nombrePropietario`, `dniInquilino`, `nombreInquilino`, `fechaInicio`, `fechaFinal`, `monto`, `idInmueble`, `direccion`, `vigente`) VALUES
(1, '33456788', 'Selene Nicole Farias', '22321456', 'Jorge Suarez', '2025-04-18', '2025-05-01', 118000, 4, 'Pte. Perón 4552 Dto. 0 , Capital Federal', 1),
(4, '20922882', 'Analia Farias', '22321456', 'Jorge Suarez', '2025-04-25', '2025-05-01', 120009, 23, 'Pública 0 Dto. 0 , La Paz', 1),
(5, '22666454', 'Cristina Diaz', '27444236', 'Paula Fernanda Gonzalez', '2025-05-01', '2025-05-03', 250000, 24, 'Av. España 1250 Piso 1 Dto. a , Villa Dolores', 0),
(6, '20922882', 'Analia Farias', '27444236', 'Paula Fernanda Gonzalez', '2025-04-27', '2025-05-01', 100000, 23, 'Pública 0 Dto. 0 , La Paz', 0),
(7, '20922882', 'Analia Farias', '27444236', 'Paula Fernanda Gonzalez', '2025-04-25', '2025-04-26', 245000, 9, 'Thorne 1439 Piso 10 Dto. 0 , Ituzaingo', 0),
(8, '20922882', 'Analia Farias', '33488655', 'Mariano Ríos', '2025-04-09', '2025-04-15', 100000, 23, 'Pública 0 Dto. 0 , La Paz', 0),
(9, '27344555', 'Maria Celeste  Balestrieri', '33488655', 'Mariano Ríos', '2025-04-21', '2025-06-21', 244000, 25, 'Pje Bianchi 45 Dto. 0 , Villa Dolores', 0),
(10, '20922882', 'Analia Farias', '22321456', 'Jorge Suarez', '2025-04-25', '2025-04-26', 120000, 23, 'Pública 0 Dto. 0 , La Paz', 1),
(11, '20922882', 'Analia Farias', '27444236', 'Paula Fernanda Gonzalez', '2026-01-01', '2027-04-01', 255000, 9, 'Thorne 1439 Piso 10 Dto. 0 , Ituzaingo', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inmueble`
--

CREATE TABLE `inmueble` (
  `idInmueble` int(11) NOT NULL,
  `dniPropietario` varchar(11) NOT NULL,
  `calle` varchar(50) NOT NULL,
  `nro` int(10) NOT NULL,
  `piso` int(2) DEFAULT NULL,
  `dpto` varchar(11) DEFAULT NULL,
  `localidad` varchar(50) NOT NULL,
  `provincia` varchar(50) NOT NULL,
  `uso` varchar(20) NOT NULL,
  `tipo` varchar(20) NOT NULL,
  `ambientes` int(2) NOT NULL,
  `pileta` varchar(2) NOT NULL,
  `parrilla` varchar(2) NOT NULL,
  `garage` varchar(2) NOT NULL,
  `latitud` decimal(11,0) NOT NULL,
  `longitud` decimal(11,0) NOT NULL,
  `precio` int(11) NOT NULL,
  `ImagenPortada` varchar(255) DEFAULT NULL,
  `vigente` tinyint(2) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inmueble`
--

INSERT INTO `inmueble` (`idInmueble`, `dniPropietario`, `calle`, `nro`, `piso`, `dpto`, `localidad`, `provincia`, `uso`, `tipo`, `ambientes`, `pileta`, `parrilla`, `garage`, `latitud`, `longitud`, `precio`, `ImagenPortada`, `vigente`) VALUES
(4, '33456788', 'Pte. Perón', 4552, 4, '0', 'Capital Federal', 'Buenos Aires', 'Residencial', 'Casa', 2, '1', '1', '1', 15, 13, 38000, '/imagenes/inmuebles/casa1_79ea2193-86e5-445e-b706-48eda8870f16.jpg', 1),
(6, '10884239', 'Rivadavia', 10200, 10, 'A', 'Merlo', 'San Luis', 'Residencial', 'Casa', 2, '0', '1', '1', 25, 44, 98000, NULL, 1),
(8, '25748654', 'Republica del Líbano', 450, 1, 'A', 'San Martin', 'Buenos Aires', 'Residencial', 'Casa', 4, '0', '0', '1', 35, 44, 105000, NULL, 0),
(9, '20922882', 'Thorne', 1439, 10, '0', 'Ituzaingo', 'Buenos Aires', 'Residencial', 'Casa', 3, '1', '1', '1', 33, 13, 85000, '/imagenes/inmuebles/casa22_18650972-8a05-4686-bf09-8427c78e9269.webp', 1),
(16, '20922882', 'Europa', 321, 2, 'A', 'Ituzaingó', 'Buenos Aires', 'Comercial', 'Casa', 3, '1', '0', '1', 35, 32, 89000, NULL, 0),
(18, '20922882', 'Chacabuco', 648, 0, NULL, 'Buenos Aires', 'Buenos Aires', 'Residencial', 'Casa', 2, '0', '0', '0', 12, 321, 33000, NULL, 0),
(21, '25748654', 'Perú', 456, 6, 'A', 'Merlo', 'San Luis', '5', '5', 2, '0', '0', '0', 5, 511, 33000, NULL, 0),
(22, '25748654', 'Belgrano', 12, 0, NULL, 'Villa Dolores', 'Córdoba', 'Comercial', 'Casa', 6, '1', '1', '0', 25, 125, 74000, NULL, 1),
(23, '20922882', 'Pública', 0, 0, '0', 'La Paz', 'Córdoba', 'Residencial', 'Casa', 4, '1', '1', '1', 123, 25, 100000, NULL, 1),
(24, '22666454', 'Av. España', 1250, 1, 'a', 'Villa Dolores', 'Córdoba', 'Residencial', 'Casa', 3, '1', '1', '1', 25, 125, 88000, NULL, 1),
(25, '27344555', 'Pje Bianchi', 45, 0, '0', 'Villa Dolores', 'Córdoba', 'Residencial', 'Casa', 4, '0', '1', '1', 12, 44, 450000, NULL, 1),
(26, '10884239', 'Francia', 1345, 1, 'C', 'Villa Mercedes', 'San Luis', 'casa', 'residencial', 1, '1', '1', '1', 15, 13, 120500, '/Uploads/Portadas/casa1Interior1.jpg', 1),
(27, '22666454', '1', 1, 1, '1', '1', '1', '1', '1', 1, '1', '0', '0', 1, 1, 1, '/Uploads/Portadas/casa1Interior1.jpg', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inmueblefotocarrusel`
--

CREATE TABLE `inmueblefotocarrusel` (
  `Id` int(11) NOT NULL,
  `IdInmueble` int(11) NOT NULL,
  `RutaFoto` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inmueblefotocarrusel`
--

INSERT INTO `inmueblefotocarrusel` (`Id`, `IdInmueble`, `RutaFoto`) VALUES
(15, 22, NULL),
(17, 6, NULL),
(20, 21, NULL),
(21, 21, NULL),
(23, 6, NULL),
(25, 23, NULL),
(26, 8, NULL),
(28, 8, NULL),
(29, 8, NULL),
(31, 24, NULL),
(33, 25, NULL),
(34, 25, NULL),
(35, 4, '/Imagenes/Carrusel/casa4Interior1_2efe818c-60ef-4535-bb6c-815198027170.jpg'),
(36, 4, '/Imagenes/Carrusel/casa4Interior2_31e951b9-947a-4fb2-a020-7b35dc15fa61.jpg');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inquilino`
--

CREATE TABLE `inquilino` (
  `idInquilino` int(11) NOT NULL,
  `dniInquilino` varchar(11) NOT NULL,
  `apellidoInquilino` varchar(50) NOT NULL,
  `nombreInquilino` varchar(50) NOT NULL,
  `contactoInquilino` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inquilino`
--

INSERT INTO `inquilino` (`idInquilino`, `dniInquilino`, `apellidoInquilino`, `nombreInquilino`, `contactoInquilino`) VALUES
(1, '22321456', 'Suarez', 'Jorge', '1162105442'),
(2, '33488655', 'Ríos', 'Mariano', '3544226644'),
(3, '27444236', 'Gonzalez', 'Paula Fernanda', '2664235465');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pagos`
--

CREATE TABLE `pagos` (
  `idPago` int(11) NOT NULL,
  `idContrato` int(11) NOT NULL,
  `nroPago` int(11) NOT NULL,
  `fechaPago` date NOT NULL,
  `importe` decimal(15,0) NOT NULL,
  `detalle` varchar(255) NOT NULL,
  `estado` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `pagos`
--

INSERT INTO `pagos` (`idPago`, `idContrato`, `nroPago`, `fechaPago`, `importe`, `detalle`, `estado`) VALUES
(2, 7, 1, '2025-05-06', 20000, 'mes de mayo', 'Pagado'),
(3, 1, 2, '2025-04-15', 120000, 'mes de abril 2025.', 'Pagado'),
(7, 1, 3, '2025-04-29', 11000, 'mes de mayo 2025', 'Anulado'),
(8, 5, 1, '2025-04-19', 150000, 'Abril de 2025', 'Pagado'),
(9, 5, 2, '2025-04-25', 150500, 'mes de mayo 2025', 'Anulado'),
(10, 7, 2, '2025-04-22', 125000, 'Abono por un mes.', 'Pagado'),
(11, 1, 1, '2025-03-01', 120000, 'Mes de marzo 2025..', 'Pagado'),
(12, 1, 4, '2025-05-20', 120000, 'mes de junio 2025.', 'Anulado'),
(13, 7, 3, '2025-06-01', 125000, 'Mes de junio de 2025', 'Pagado'),
(15, 1, 5, '2025-04-21', 1200000, 'Mes de abril', 'Pagado'),
(16, 1, 6, '2025-04-28', 12444, 'Abono por un mes', 'Pagado'),
(17, 5, 2, '2025-05-01', 150000, 'Mayo 2025', 'Pagado'),
(86, 7, 4, '2025-07-28', 125500, 'julio.', 'Pagado'),
(87, 11, 1, '2025-04-27', 255000, 'mes de abril 2025', 'Pagado'),
(88, 11, 2, '2025-05-27', 255000, 'mes de mayo 2025', 'Pagado'),
(89, 10, 1, '2025-04-28', 120000, 'Abono por un mes', 'Pagado'),
(90, 4, 1, '2025-04-28', 110000, 'mes de junio de 2025', 'Pagado');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `propietario`
--

CREATE TABLE `propietario` (
  `idPropietario` int(11) NOT NULL,
  `dniPropietario` varchar(11) NOT NULL,
  `apellidoPropietario` varchar(50) NOT NULL,
  `nombrePropietario` varchar(50) NOT NULL,
  `contactoPropietario` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `propietario`
--

INSERT INTO `propietario` (`idPropietario`, `dniPropietario`, `apellidoPropietario`, `nombrePropietario`, `contactoPropietario`) VALUES
(1, '10884239', 'Lisbon', 'Teresa', '1146276586'),
(2, '25748654', 'Jane', 'Patrick', '1144891234'),
(3, '22666454', 'Diaz', 'Cristina', '2661222363'),
(4, '33456788', 'Farias', 'Selene Nicole', '3544616228'),
(8, '27344555', 'Balestrieri', 'Maria Celeste ', '354438887'),
(9, '20922882', 'Farias', 'Analia', '1169761345');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `contrato`
--
ALTER TABLE `contrato`
  ADD PRIMARY KEY (`idContrato`);

--
-- Indices de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  ADD PRIMARY KEY (`idInmueble`);

--
-- Indices de la tabla `inmueblefotocarrusel`
--
ALTER TABLE `inmueblefotocarrusel`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IdInmueble` (`IdInmueble`);

--
-- Indices de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  ADD PRIMARY KEY (`idInquilino`),
  ADD UNIQUE KEY `dniInquilino` (`dniInquilino`);

--
-- Indices de la tabla `pagos`
--
ALTER TABLE `pagos`
  ADD PRIMARY KEY (`idPago`);

--
-- Indices de la tabla `propietario`
--
ALTER TABLE `propietario`
  ADD PRIMARY KEY (`idPropietario`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `contrato`
--
ALTER TABLE `contrato`
  MODIFY `idContrato` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  MODIFY `idInmueble` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=28;

--
-- AUTO_INCREMENT de la tabla `inmueblefotocarrusel`
--
ALTER TABLE `inmueblefotocarrusel`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=37;

--
-- AUTO_INCREMENT de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  MODIFY `idInquilino` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT de la tabla `pagos`
--
ALTER TABLE `pagos`
  MODIFY `idPago` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=91;

--
-- AUTO_INCREMENT de la tabla `propietario`
--
ALTER TABLE `propietario`
  MODIFY `idPropietario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `inmueblefotocarrusel`
--
ALTER TABLE `inmueblefotocarrusel`
  ADD CONSTRAINT `inmueblefotocarrusel_ibfk_1` FOREIGN KEY (`IdInmueble`) REFERENCES `inmueble` (`idInmueble`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
