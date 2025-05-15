-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 15-05-2025 a las 22:00:53
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
  `vigente` tinyint(1) DEFAULT NULL,
  `realizadoPor` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `contrato`
--

INSERT INTO `contrato` (`idContrato`, `dniPropietario`, `nombrePropietario`, `dniInquilino`, `nombreInquilino`, `fechaInicio`, `fechaFinal`, `monto`, `idInmueble`, `direccion`, `vigente`, `realizadoPor`) VALUES
(1, '33456788', 'Selene Nicole Farias', '22321456', 'Jorge Suarez', '2025-04-18', '2025-05-01', 118000, 4, 'Pte. Perón 4552 Dto. 0 , Capital Federal', 1, 'Selene Balestrieri'),
(4, '20922882', 'Analia Farias', '22321456', 'Jorge Suarez', '2025-04-25', '2025-05-01', 120009, 23, 'Pública 0 Dto. 0 , La Paz', 1, 'Maximiliano Balestrieri'),
(5, '22666454', 'Cristina Diaz', '27444236', 'Paula Fernanda Gonzalez', '2025-05-01', '2025-05-03', 250000, 24, 'Av. España 1250 Piso 1 Dto. a , Villa Dolores', 1, 'Selene Balestrieri'),
(6, '20922882', 'Analia Farias', '27444236', 'Paula Fernanda Gonzalez', '2025-04-27', '2025-05-01', 100000, 23, 'Pública 0 Dto. 0 , La Paz', 1, 'Selene Balestrieri'),
(7, '20922882', 'Analia Farias', '27444236', 'Paula Fernanda Gonzalez', '2025-04-25', '2025-04-26', 245000, 9, 'Thorne 1439 Piso 10 Dto. 0 , Ituzaingo', 1, 'Selene Balestrieri'),
(19, '20922882', 'Analia Farias', '27444236', 'Paula Fernanda Gonzalez', '2025-05-10', '2025-05-31', 88000, 9, 'Thorne 1439 Piso 10 Dto. 0 , Ituzaingo', 1, 'Sol Anabela Balestrieri'),
(21, '22666454', 'Cristina Diaz', '27444236', 'Paula Fernanda Gonzalez', '2025-05-09', '2025-05-16', 100000, 24, 'Av. España 1250 Piso 1 Dto. a , Villa Dolores', 1, 'Selene Balestrieri'),
(23, '33456788', 'Selene Nicole Farias', '27444236', 'Paula Fernanda Gonzalez', '2025-05-02', '2025-06-30', 58000, 4, 'Pte. Perón 4552 Piso 4 Dto. 0 , Capital Federal', 1, 'Sol Anabela Balestrieri'),
(24, '10884239', 'Teresa Lisbon', '27444236', 'Paula Fernanda Gonzalez', '2025-05-01', '2025-07-10', 354000, 6, 'Rivadavia 10200 Piso 10 Dto. A , Merlo', 1, 'Sol Anabela Balestrieri'),
(25, '20922882', 'Analia Farias', '22321456', 'Jorge Suarez', '2025-05-10', '2025-08-10', 333555, 23, 'Pública 0 Dto. 0 , La Paz', 1, 'Jose Garcia Hernandez'),
(26, '22666454', 'Cristina Diaz', '27444236', 'Paula Fernanda Gonzalez', '2025-05-10', '2025-05-31', 100000, 27, '1 1 Piso 1 Dto. 1 , 1', 1, 'Sol Anabela Balestrieri'),
(27, '27344555', 'Maria Celeste  Balestrieri', '27444236', 'Paula Fernanda Gonzalez', '2025-05-01', '2025-12-12', 254555, 25, 'Pje Bianchi 45 Dto. 0 , Villa Dolores', 1, 'Selene Balestrieri'),
(29, '20922882', 'Analia Farias', '27444236', 'Paula Fernanda Gonzalez', '2025-05-06', '2025-05-09', 100000, 23, 'Pública 0 Dto. 0 , La Paz', 1, 'Selene Balestrieri'),
(30, '20922882', 'Analia Farias', '27444236', 'Paula Fernanda Gonzalez', '2025-05-01', '2025-05-05', 100000, 9, 'Thorne 1439 Piso 10 Dto. 0 , Ituzaingo', 1, 'Maximiliano Balestrieri'),
(31, '10884239', 'Teresa Lisbon', '27444236', 'Paula Fernanda Gonzalez', '2025-05-01', '2025-05-07', 100000, 26, 'Francia 1345 Piso 1 Dto. C , Villa Mercedes', 1, 'Maximiliano Balestrieri'),
(32, '25748654', 'Patrick Jane', '33488655', 'Mariano Ríos', '2025-05-06', '2025-05-07', 100000, 22, 'Belgrano 12 , Villa Dolores', 1, 'Selene Balestrieri'),
(33, '22666454', 'Cristina Diaz', '27444236', 'Paula Fernanda Gonzalez', '2025-05-01', '2025-05-08', 520000, 27, '1 1 Piso 1 Dto. 1 , 1', 1, 'Sol Anabela Balestrieri');

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
(6, '10884239', 'Rivadavia', 10200, 10, 'A', 'Merlo', 'San Luis', 'Residencial', 'Casa', 2, '0', '1', '1', 25, 44, 98000, '/imagenes/inmuebles/casa22_cc7ae036-1c53-4706-9c13-768fa1770979.webp', 1),
(8, '25748654', 'Republica del Líbano', 450, 1, 'A', 'San Martin', 'Buenos Aires', 'Residencial', 'Casa', 4, '0', '0', '1', 35, 44, 105000, NULL, 0),
(9, '20922882', 'Thorne', 1439, 10, '0', 'Ituzaingo', 'Buenos Aires', 'Residencial', 'Casa', 3, '1', '1', '1', 33, 13, 85000, '/imagenes/inmuebles/casa22_18650972-8a05-4686-bf09-8427c78e9269.webp', 1),
(16, '20922882', 'Europa', 321, 2, 'A', 'Ituzaingó', 'Buenos Aires', 'Comercial', 'Casa', 3, '1', '0', '1', 35, 32, 89000, NULL, 0),
(18, '20922882', 'Chacabuco', 648, 0, NULL, 'Buenos Aires', 'Buenos Aires', 'Residencial', 'Casa', 2, '0', '0', '0', 12, 321, 33000, NULL, 0),
(21, '25748654', 'Perú', 456, 6, 'A', 'Merlo', 'San Luis', '5', '5', 2, '0', '0', '0', 5, 511, 33000, '/imagenes/inmuebles/casa22_4ab8d66a-e92e-449f-8a0f-4d53294b01bb.webp', 0),
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
(35, 4, '/Imagenes/Carrusel/casa4Interior1_2efe818c-60ef-4535-bb6c-815198027170.jpg'),
(36, 4, '/Imagenes/Carrusel/casa4Interior2_31e951b9-947a-4fb2-a020-7b35dc15fa61.jpg'),
(37, 26, '/Imagenes/Carrusel/casa1Interior2_ff581d6b-bcae-457c-a25d-ad16ec22578c.jpg'),
(38, 9, '/Imagenes/Carrusel/casa1Interior2_8feba3ea-c2b0-49d8-885f-0263655b4cbe.jpg'),
(39, 6, '/Imagenes/Carrusel/casa3Interior2_9b305656-682d-4c89-ad5f-f5b1b155acf7.jpg'),
(40, 21, '/Imagenes/Carrusel/casa2Interior2_adbfeb80-c6a3-4f3f-a978-b7b14961df00.jpg'),
(41, 21, '/Imagenes/Carrusel/casa3Interior1_2507877f-e1a5-4159-9f70-bfbb362083ec.jpg'),
(42, 21, '/Imagenes/Carrusel/casa3Interior2_3027a55c-aa50-40a2-bb27-5fddc3b874b8.jpg'),
(43, 6, '/Imagenes/Carrusel/casa1Interior2_e968e7bb-0f97-44ef-abb3-1d491520b55d.jpg');

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
  `estado` varchar(50) DEFAULT NULL,
  `pagadoPor` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `pagos`
--

INSERT INTO `pagos` (`idPago`, `idContrato`, `nroPago`, `fechaPago`, `importe`, `detalle`, `estado`, `pagadoPor`) VALUES
(2, 7, 1, '2025-05-06', 20000, 'mes de mayo', 'Pagado', 'Maximiliano Balestrieri'),
(3, 1, 2, '2025-04-15', 120000, 'mes de abril 2025.', 'Pagado', 'Maximiliano Balestrieri'),
(7, 1, 3, '2025-04-29', 11000, 'mes de mayo 2025', 'Anulado', 'Maximiliano Balestrieri'),
(8, 5, 1, '2025-04-19', 150000, 'Abril de 2025', 'Pagado', 'Maximiliano Balestrieri'),
(9, 5, 2, '2025-04-25', 150500, 'mes de mayo 2025', 'Anulado', 'Maximiliano Balestrieri'),
(10, 7, 2, '2025-04-22', 125000, 'Abono por un mes.', 'Pagado', 'Maximiliano Balestrieri'),
(11, 1, 1, '2025-03-01', 120000, 'Mes de marzo 2025..', 'Pagado', 'Maximiliano Balestrieri'),
(12, 1, 4, '2025-05-20', 120000, 'mes de junio 2025.', 'Anulado', 'Maximiliano Balestrieri'),
(13, 7, 3, '2025-06-01', 125000, 'Mes de junio de 2025', 'Pagado', 'Maximiliano Balestrieri'),
(15, 1, 5, '2025-04-21', 1200000, 'Mes de abril', 'Pagado', 'Maximiliano Balestrieri'),
(16, 1, 6, '2025-04-28', 12444, 'Abono por un mes', 'Pagado', 'Maximiliano Balestrieri'),
(17, 5, 2, '2025-05-01', 150000, 'Mayo 2025', 'Pagado', 'Maximiliano Balestrieri'),
(86, 7, 4, '2025-07-28', 125500, 'julio.', 'Pagado', 'Maximiliano Balestrieri'),
(87, 11, 1, '2025-04-27', 255000, 'mes de abril 2025', 'Pagado', 'Maximiliano Balestrieri'),
(88, 11, 2, '2025-05-27', 255000, 'mes de mayo 2025', 'Pagado', 'Maximiliano Balestrieri'),
(89, 10, 1, '2025-04-28', 120000, 'Abono por un mes', 'Pagado', 'Maximiliano Balestrieri'),
(90, 4, 1, '2025-04-28', 110000, 'mes de abril 2025.', 'Pagado', 'Maximiliano Balestrieri'),
(91, 6, 1, '2025-05-01', 120000, 'Mayo 2025', 'Pagado', 'Maximiliano Balestrieri'),
(96, 4, 2, '2025-05-01', 115000, 'mes de mayo 2025', 'Anulado', 'Jose Garcia Hernandez'),
(98, 4, 3, '2025-05-08', 117000, 'mes del año', 'Anulado', 'Jose Garcia Hernandez'),
(99, 4, 4, '2025-05-08', 117500, 'mes de abono.', 'Anulado', 'Jose Garcia Hernandez'),
(102, 17, 1, '2025-05-10', 100000, 'mes de mayo 2025', 'Pagado', 'Maximiliano Balestrieri'),
(103, 17, 2, '2025-06-10', 100000, 'mes de junio de 2025', 'Pagado', 'Maximiliano Balestrieri');

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

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuarios`
--

CREATE TABLE `usuarios` (
  `idUsuario` int(11) NOT NULL,
  `Usuario` varchar(50) NOT NULL,
  `contraseña` varchar(255) NOT NULL,
  `rol` varchar(50) NOT NULL,
  `nombreyApellido` varchar(50) NOT NULL,
  `FotoPerfil` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `usuarios`
--

INSERT INTO `usuarios` (`idUsuario`, `Usuario`, `contraseña`, `rol`, `nombreyApellido`, `FotoPerfil`) VALUES
(1, 'Admin', '123', 'Administrador', 'Jose Garcia Hernandez', '/imagenes/usuarios/e601a709-ae06-4778-912a-38d08d9d3ce3.jpg'),
(2, 'Mbalestrieri', '123', 'Usuario', 'Maximiliano Balestrieri', '/imagenes/usuarios/f3890a2e-ceb5-45b1-b616-ff38e740ec11.PNG'),
(3, 'Selene', '123', 'Administrador', 'Selene Balestrieri', '/imagenes/usuarios/34c1d0e8-6eab-4331-9549-af5b5adc751c.jpg'),
(4, 'Solcy', '123', 'Usuario', 'Sol Anabela Balestrieri', '/imagenes/usuarios/8540677f-2554-4148-aef8-2625ebcf4d17.jpeg'),
(6, 'ADFarias', '123', 'Usuario', 'Analia Dina Farias', '/imagenes/usuarios/300ab345-0ba3-46a0-b17d-16089bc5609e.jpg'),
(13, 'Angie', '123', 'Usuario', 'Angie Pereyra', '/imagenes/usuarios/default.png');

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
-- Indices de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  ADD PRIMARY KEY (`idUsuario`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `contrato`
--
ALTER TABLE `contrato`
  MODIFY `idContrato` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=34;

--
-- AUTO_INCREMENT de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  MODIFY `idInmueble` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=28;

--
-- AUTO_INCREMENT de la tabla `inmueblefotocarrusel`
--
ALTER TABLE `inmueblefotocarrusel`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=44;

--
-- AUTO_INCREMENT de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  MODIFY `idInquilino` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT de la tabla `pagos`
--
ALTER TABLE `pagos`
  MODIFY `idPago` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=104;

--
-- AUTO_INCREMENT de la tabla `propietario`
--
ALTER TABLE `propietario`
  MODIFY `idPropietario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  MODIFY `idUsuario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

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
