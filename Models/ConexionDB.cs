using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Web;
using MySql.Data.MySqlClient;
using proyectoInmobiliariaNuevo.Models;


namespace proyectoInmobiliariaNuevo.Models
{
    public class ConexionDB
    {
        private MySqlConnection conexion;
        private string _connectionString;

        // Constructor donde inicializamos la conexión
        public ConexionDB()
        {
            string servidor = "localhost";
            string baseDatos = "inmobiliaria";  // Nombre de tu base de datos
            string usuario = "root";  // Usuario por defecto en XAMPP
            string contrasena = "";   // En XAMPP no suele tener contraseña por defecto

            _connectionString = $"Server={servidor}; database={baseDatos}; UID={usuario}; password={contrasena};";
            conexion = new MySqlConnection(_connectionString);
        }

        public ConexionDB(string cadenaConexion)
{
    _connectionString = cadenaConexion;
    conexion = new MySqlConnection(_connectionString);
}



        // Método para obtener todos los propietarios desde la base de datos
        public List<Propietario> ObtenerPropietarios()
        {
            List<Propietario> propietarios = new List<Propietario>();

            try
            {
                conexion.Open();
                string query = "SELECT * FROM propietario";  // Consulta SQL para obtener los propietarios
                MySqlCommand cmd = new MySqlCommand(query, conexion);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Propietario propietario = new Propietario
                    {
                        IdPropietario = reader.GetInt32("idPropietario"),
                        DniPropietario = reader.GetString("dniPropietario"),
                        ApellidoPropietario = reader.GetString("apellidoPropietario"),
                        NombrePropietario = reader.GetString("nombrePropietario"),
                        ContactoPropietario = reader.GetString("contactoPropietario")
                    };
                    propietarios.Add(propietario);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error al obtener los propietarios: " + ex.Message);  // Para depurar
            }
            finally
            {
                conexion.Close();
            }

            return propietarios;
        }



        // Método para obtener todos los inquilinos desde la base de datos
        public List<Inquilino> ObtenerInquilinos()
        {
            List<Inquilino> inquilinos = new List<Inquilino>();

            try
            {
                conexion.Open();
                string query = "SELECT * FROM inquilino";  // Consulta SQL para obtener los inquilinos
                MySqlCommand cmd = new MySqlCommand(query, conexion);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Inquilino inquilino = new Inquilino
                    {
                        IdInquilino = reader.GetInt32("idInquilino"),
                        DniInquilino = reader.GetString("dniInquilino"),
                        ApellidoInquilino = reader.GetString("apellidoInquilino"),
                        NombreInquilino = reader.GetString("nombreInquilino"),
                        ContactoInquilino = reader.GetString("contactoInquilino")
                    };
                    inquilinos.Add(inquilino);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error al obtener los inquilinos: " + ex.Message);  // Para depurar
            }
            finally
            {
                conexion.Close();
            }

            return inquilinos;
        }

        // Obtener propietario por ID
        public Propietario ObtenerPropietarioPorId(int id)
        {
            Propietario propietario = null;

            using (MySqlConnection conexion = new MySqlConnection(_connectionString))
            {
                string query = "SELECT * FROM propietario WHERE idPropietario = @id";
                MySqlCommand cmd = new MySqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@id", id);

                conexion.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    propietario = new Propietario
                    {
                        IdPropietario = reader.GetInt32("idPropietario"),
                        DniPropietario = reader.GetString("dniPropietario"),
                        ApellidoPropietario = reader.GetString("apellidoPropietario"),
                        NombrePropietario = reader.GetString("nombrePropietario"),
                        ContactoPropietario = reader.GetString("contactoPropietario")
                    };
                }
            }
            return propietario;
        }

        // Obtener inquilino por ID
        public Inquilino ObtenerInquilinoPorId(int id)
        {
            Inquilino inquilino = null;

            using (MySqlConnection conexion = new MySqlConnection(_connectionString))
            {
                string query = "SELECT * FROM inquilino WHERE idInquilino = @id";
                MySqlCommand cmd = new MySqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@id", id);

                conexion.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    inquilino = new Inquilino
                    {
                        IdInquilino = reader.GetInt32("idInquilino"),
                        DniInquilino = reader.GetString("dniInquilino"),
                        ApellidoInquilino = reader.GetString("apellidoInquilino"),
                        NombreInquilino = reader.GetString("nombreInquilino"),
                        ContactoInquilino = reader.GetString("contactoInquilino")
                    };
                }
            }
            return inquilino;
        }

        // Agregar un nuevo propietario
        public void AgregarPropietario(Propietario propietario)
        {
            using (MySqlConnection conexion = new MySqlConnection(_connectionString))
            {
                string query = "INSERT INTO propietario (dniPropietario, apellidoPropietario, nombrePropietario, contactoPropietario) VALUES (@dni, @apellido, @nombre, @contacto)";
                MySqlCommand cmd = new MySqlCommand(query, conexion);

                cmd.Parameters.AddWithValue("@dni", propietario.DniPropietario);
                cmd.Parameters.AddWithValue("@apellido", propietario.ApellidoPropietario);
                cmd.Parameters.AddWithValue("@nombre", propietario.NombrePropietario);
                cmd.Parameters.AddWithValue("@contacto", propietario.ContactoPropietario);

                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Agregar un nuevo inquilino
        public void AgregarInquilino(Inquilino inquilino)
        {
            using (MySqlConnection conexion = new MySqlConnection(_connectionString))
            {
                string query = "INSERT INTO inquilino (dniInquilino, apellidoInquilino, nombreInquilino, contactoInquilino) VALUES (@dni, @apellido, @nombre, @contacto)";
                MySqlCommand cmd = new MySqlCommand(query, conexion);

                cmd.Parameters.AddWithValue("@dni", inquilino.DniInquilino);
                cmd.Parameters.AddWithValue("@apellido", inquilino.ApellidoInquilino);
                cmd.Parameters.AddWithValue("@nombre", inquilino.NombreInquilino);
                cmd.Parameters.AddWithValue("@contacto", inquilino.ContactoInquilino);

                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Actualizar un propietario existente
        public void ActualizarPropietario(Propietario propietario)
        {
            using (MySqlConnection conexion = new MySqlConnection(_connectionString))
            {
                string query = "UPDATE propietario SET dniPropietario = @dni, apellidoPropietario = @apellido, nombrePropietario = @nombre, contactoPropietario = @contacto WHERE idPropietario = @id";
                MySqlCommand cmd = new MySqlCommand(query, conexion);

                cmd.Parameters.AddWithValue("@id", propietario.IdPropietario);
                cmd.Parameters.AddWithValue("@dni", propietario.DniPropietario);
                cmd.Parameters.AddWithValue("@apellido", propietario.ApellidoPropietario);
                cmd.Parameters.AddWithValue("@nombre", propietario.NombrePropietario);
                cmd.Parameters.AddWithValue("@contacto", propietario.ContactoPropietario);

                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Actualizar un inquilino existente
        public void ActualizarInquilino(Inquilino inquilino)
        {
            using (MySqlConnection conexion = new MySqlConnection(_connectionString))
            {
                string query = "UPDATE inquilino SET dniInquilino = @dni, apellidoInquilino = @apellido, nombreInquilino = @nombre, contactoInquilino = @contacto WHERE idInquilino = @id";
                MySqlCommand cmd = new MySqlCommand(query, conexion);

                cmd.Parameters.AddWithValue("@id", inquilino.IdInquilino);
                cmd.Parameters.AddWithValue("@dni", inquilino.DniInquilino);
                cmd.Parameters.AddWithValue("@apellido", inquilino.ApellidoInquilino);
                cmd.Parameters.AddWithValue("@nombre", inquilino.NombreInquilino);
                cmd.Parameters.AddWithValue("@contacto", inquilino.ContactoInquilino);

                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Eliminar un propietario
        public void EliminarPropietario(int id)
        {
            using (MySqlConnection conexion = new MySqlConnection(_connectionString))
            {
                string query = "DELETE FROM propietario WHERE idPropietario = @id";
                MySqlCommand cmd = new MySqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@id", id);

                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Eliminar un inquilino
        public void EliminarInquilino(int id)
        {
            using (MySqlConnection conexion = new MySqlConnection(_connectionString))
            {
                string query = "DELETE FROM inquilino WHERE idInquilino = @id";
                MySqlCommand cmd = new MySqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@id", id);

                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }
        // private string connectionString = "server=localhost;database=inmobiliaria;uid=root;pwd=;";

        // Obtener todos los inmuebles
        public List<Inmueble> ObtenerInmuebles()
        {
            List<Inmueble> inmuebles = new List<Inmueble>();

            conexion.Open();
            string query = "SELECT * FROM inmueble";
            MySqlCommand cmd = new MySqlCommand(query, conexion);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Inmueble inmueble = new Inmueble
                {
                    IdInmueble = reader.GetInt32("idInmueble"),
                    DniPropietario = reader.GetString("dniPropietario"),
                    Calle = reader.GetString("calle"),
                    Nro = reader.GetInt32("nro"),
                    Piso = reader.IsDBNull(reader.GetOrdinal("piso")) ? 0 : reader.GetInt32("piso"),
                    Dpto = reader.IsDBNull(reader.GetOrdinal("dpto")) ? null : reader.GetString("dpto"),
                    Localidad = reader.GetString("localidad"),
                    Provincia = reader.GetString("provincia"),
                    Uso = reader.GetString("uso"),
                    Tipo = reader.GetString("tipo"),
                    Ambientes = reader.GetInt32("ambientes"),
                    Pileta = reader["pileta"].ToString() == "1",
                    Parrilla = reader["parrilla"].ToString() == "1",
                    Garage = reader["garage"].ToString() == "1",
                    Latitud = Convert.ToDouble(reader["latitud"]),
                    Longitud = Convert.ToDouble(reader["longitud"]),
                    Precio = Convert.ToDouble(reader["precio"]),
                    Vigente = reader["vigente"].ToString() == "1",
                    ImagenPortada = reader["ImagenPortada"] != DBNull.Value ? reader["ImagenPortada"].ToString() : null,
                    FotosCarruselLista = new List<string>()
                };

                inmuebles.Add(inmueble);
            }

            reader.Close();
            conexion.Close();

            return inmuebles;
        }

        public List<InmuebleFotoCarrusel> ObtenerFotosCarruselPorInmueble(int id)
        {
            var fotosCarrusel = new List<InmuebleFotoCarrusel>();

            using (var conexion = new MySqlConnection(_connectionString))
            {
                string query = "SELECT * FROM InmuebleFotoCarrusel WHERE IdInmueble = @id";
                MySqlCommand cmd = new MySqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@id", id);

                conexion.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        fotosCarrusel.Add(new InmuebleFotoCarrusel
                        {
                            Id = reader.GetInt32("Id"),
                            IdInmueble = reader.GetInt32("IdInmueble"),
                            RutaFoto = reader.GetString("RutaFoto")
                        });
                    }
                }
            }

            return fotosCarrusel;
        }




        public void InsertarFotoCarrusel(int idInmueble, string rutaFoto)
        {
            using (MySqlConnection conexion = new MySqlConnection(_connectionString))
            {
                string query = "INSERT INTO InmuebleFotoCarrusel (IdInmueble, RutaFoto) VALUES (@idInmueble, @ruta)";
                MySqlCommand cmd = new MySqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@idInmueble", idInmueble);
                cmd.Parameters.AddWithValue("@ruta", rutaFoto);

                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }



        public void AgregarFotoCarrusel(int idInmueble, string rutaFoto)
        {
            using (var conn = ObtenerConexion())
            {
                var cmd = new MySqlCommand("INSERT INTO InmuebleFotoCarrusel (IdInmueble, RutaFoto) VALUES (@id, @ruta)", conn);
                cmd.Parameters.AddWithValue("@id", idInmueble);
                cmd.Parameters.AddWithValue("@ruta", rutaFoto);
                cmd.ExecuteNonQuery();
            }
        }

        public Inmueble ObtenerInmueblePorId(int id)
        {
            Inmueble inmueble = null;

            try
            {
                using (MySqlConnection conexion = new MySqlConnection(_connectionString))
                {
                    conexion.Open();

                    // 1. Obtener datos del inmueble
                    string query = "SELECT * FROM inmueble WHERE idInmueble = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            inmueble = new Inmueble
                            {
                                IdInmueble = reader.GetInt32("idInmueble"),
                                DniPropietario = reader.GetString("dniPropietario"),
                                Calle = reader.GetString("calle"),
                                Nro = reader.GetInt32("nro"),
                                Piso = reader.IsDBNull(reader.GetOrdinal("piso")) ? 0 : reader.GetInt32("piso"),
                                Dpto = reader["dpto"] != DBNull.Value ? reader["dpto"].ToString() : "",
                                Localidad = reader.GetString("localidad"),
                                Provincia = reader.GetString("provincia"),
                                Uso = reader.GetString("uso"),
                                Tipo = reader.GetString("tipo"),
                                Ambientes = reader.GetInt32("ambientes"),
                                Pileta = reader["pileta"].ToString() == "1",
                                Parrilla = reader["parrilla"].ToString() == "1",
                                Garage = reader["garage"].ToString() == "1",
                                Latitud = Convert.ToDouble(reader["latitud"]),
                                Longitud = Convert.ToDouble(reader["longitud"]),
                                Precio = Convert.ToDouble(reader["precio"]),
                                Vigente = reader["vigente"].ToString() == "1",
                                ImagenPortada = reader["ImagenPortada"] != DBNull.Value ? reader["ImagenPortada"].ToString() : null,
                                FotosCarruselLista = new List<string>()
                            };
                        }
                    }

                    // 2. Obtener fotos del carrusel (misma conexión, aún abierta)
                    if (inmueble != null)
                    {
                        string fotosQuery = "SELECT RutaFoto FROM InmuebleFotoCarrusel WHERE IdInmueble = @id";
                        MySqlCommand cmdFotos = new MySqlCommand(fotosQuery, conexion);
                        cmdFotos.Parameters.AddWithValue("@id", id);

                        using (var readerFotos = cmdFotos.ExecuteReader())
                        {
                            while (readerFotos.Read())
                            {
                                inmueble.FotosCarruselLista.Add(readerFotos["RutaFoto"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener inmueble: " + ex.Message);
            }

            return inmueble;
        }

        public List<proyectoInmobiliariaNuevo.Models.FotoCarrusel> ObtenerFotosCarrusel(int idInmueble)
        {
            var lista = new List<proyectoInmobiliariaNuevo.Models.FotoCarrusel>();

            using (var conexion = ObtenerConexion())
            {
                conexion.Open();
                var cmd = new MySqlCommand("SELECT Id, RutaFoto FROM InmuebleFotoCarrusel WHERE IdInmueble = @id", conexion);
                cmd.Parameters.AddWithValue("@id", idInmueble);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new proyectoInmobiliariaNuevo.Models.FotoCarrusel
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        RutaFoto = reader["RutaFoto"].ToString()
                    });
                }
            }

            return lista;
        }


        public string ObtenerRutaFotoCarrusel(int idFoto)
        {
            string ruta = "";
            using (var conn = ObtenerConexion())
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT RutaFoto FROM InmuebleFotoCarrusel WHERE Id = @id", conn);
                cmd.Parameters.AddWithValue("@id", idFoto);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    ruta = reader["RutaFoto"].ToString();
                }
            }
            return ruta;
        }


        public void EliminarFotoCarrusel(int id)
        {
            using (MySqlConnection conexion = new MySqlConnection(_connectionString))
            {
                string query = "DELETE FROM InmuebleFotoCarrusel WHERE Id = @id";
                MySqlCommand cmd = new MySqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@id", id);
                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }



        public InmuebleFotoCarrusel ObtenerFotoCarruselPorId(int id)
        {
            InmuebleFotoCarrusel? foto = null;
            using (MySqlConnection conexion = new MySqlConnection(_connectionString))
            {
                string query = "SELECT * FROM InmuebleFotoCarrusel WHERE Id = @id";
                MySqlCommand cmd = new MySqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@id", id);
                conexion.Open();
                Console.WriteLine("Foto encontrada: " + foto?.RutaFoto);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        foto = new InmuebleFotoCarrusel
                        {
                            Id = reader.GetInt32("Id"),
                            IdInmueble = reader.GetInt32("IdInmueble"),
                            RutaFoto = reader.GetString("RutaFoto")
                        };
                    }
                }
            }
            // Verificar si 'foto' no es nula antes de imprimir
    if (foto != null)
    {
        Console.WriteLine("Foto encontrada: " + foto.RutaFoto);
    }
    else
    {
        Console.WriteLine("No se encontró la foto.");
    }

    return foto;
        }





        public class InmuebleViewModel
        {
            // Otros campos del inmueble...
            public List<InmuebleFotoCarrusel>? ImagenesCarrusel { get; set; }
        }


        // Agregar nuevo inmueble
     public int AgregarInmueble(Inmueble inmueble)
{
    int idInmueble = 0;

    using (MySqlConnection conexion = new MySqlConnection(_connectionString))
    {
        string query = @"
            INSERT INTO Inmueble 
                (DniPropietario, Calle, Nro, Piso, Dpto, Localidad, Provincia, Uso, Tipo, Ambientes, Precio, Latitud, Longitud, Pileta, Parrilla, Garage, ImagenPortada, vigente)
            VALUES 
                (@DniPropietario, @Calle, @Nro, @Piso, @Dpto, @Localidad, @Provincia, @Uso, @Tipo, @Ambientes, @Precio, @Latitud, @Longitud, @Pileta, @Parrilla, @Garage, @ImagenPortada, @vigente);
            SELECT LAST_INSERT_ID();";

        MySqlCommand cmd = new MySqlCommand(query, conexion);
        cmd.Parameters.AddWithValue("@DniPropietario", inmueble.DniPropietario);
        cmd.Parameters.AddWithValue("@Calle", inmueble.Calle);
        cmd.Parameters.AddWithValue("@Nro", inmueble.Nro);
        cmd.Parameters.AddWithValue("@Piso", inmueble.Piso);
        cmd.Parameters.AddWithValue("@Dpto", inmueble.Dpto);
        cmd.Parameters.AddWithValue("@Localidad", inmueble.Localidad);
        cmd.Parameters.AddWithValue("@Provincia", inmueble.Provincia);
        cmd.Parameters.AddWithValue("@Uso", inmueble.Uso);
        cmd.Parameters.AddWithValue("@Tipo", inmueble.Tipo);
        cmd.Parameters.AddWithValue("@Ambientes", inmueble.Ambientes);
        cmd.Parameters.AddWithValue("@Precio", inmueble.Precio);
        cmd.Parameters.AddWithValue("@Latitud", inmueble.Latitud);
        cmd.Parameters.AddWithValue("@Longitud", inmueble.Longitud);
        cmd.Parameters.AddWithValue("@Pileta", inmueble.Pileta);
        cmd.Parameters.AddWithValue("@Parrilla", inmueble.Parrilla);
        cmd.Parameters.AddWithValue("@Garage", inmueble.Garage);
        cmd.Parameters.AddWithValue("@ImagenPortada", inmueble.ImagenPortada);
        cmd.Parameters.AddWithValue("@vigente", inmueble.Vigente);

        conexion.Open();
        idInmueble = Convert.ToInt32(cmd.ExecuteScalar());

        // Guardar fotos del carrusel si hay
        if (!string.IsNullOrEmpty(inmueble.FotosCarrusel))
        {
            var rutas = inmueble.FotosCarrusel.Split(';');

            foreach (var ruta in rutas)
            {
                if (!string.IsNullOrWhiteSpace(ruta))
                {
                    var cmdCarrusel = new MySqlCommand(
                        "INSERT INTO InmuebleFotoCarrusel (IdInmueble, RutaFoto) VALUES (@IdInmueble, @RutaFoto)", conexion);
                    cmdCarrusel.Parameters.AddWithValue("@IdInmueble", idInmueble);
                    cmdCarrusel.Parameters.AddWithValue("@RutaFoto", ruta);
                    cmdCarrusel.ExecuteNonQuery();
                }
            }
        }
    }

    return idInmueble;
}





        // Actualizar inmueble existente
        public void ActualizarInmueble(Inmueble inmueble)
        {
             using (MySqlConnection conexion = new MySqlConnection(_connectionString))
             {
            string query = @"UPDATE inmueble SET 
            dniPropietario = @dni,
            calle = @calle,
            nro = @nro,
            piso = @piso,
            dpto = @dpto,
            localidad = @localidad,
            provincia = @provincia,
            uso = @uso,
            tipo = @tipo,
            ambientes = @ambientes,
            pileta = @pileta,
            parrilla = @parrilla,
            garage = @garage,
            latitud = @latitud,
            longitud = @longitud,
            precio = @precio,
            ImagenPortada=@ImagenPortada,
            vigente=@vigente
        WHERE idInmueble = @id";

            MySqlCommand cmd = new MySqlCommand(query, conexion);

            cmd.Parameters.AddWithValue("@id", inmueble.IdInmueble);
            cmd.Parameters.AddWithValue("@dni", inmueble.DniPropietario);
            cmd.Parameters.AddWithValue("@calle", inmueble.Calle);
            cmd.Parameters.AddWithValue("@nro", inmueble.Nro);
            cmd.Parameters.AddWithValue("@piso", inmueble.Piso);
            cmd.Parameters.AddWithValue("@dpto", inmueble.Dpto);
            cmd.Parameters.AddWithValue("@localidad", inmueble.Localidad);
            cmd.Parameters.AddWithValue("@provincia", inmueble.Provincia);
            cmd.Parameters.AddWithValue("@uso", inmueble.Uso);
            cmd.Parameters.AddWithValue("@tipo", inmueble.Tipo);
            cmd.Parameters.AddWithValue("@ambientes", inmueble.Ambientes);
            cmd.Parameters.AddWithValue("@pileta", inmueble.Pileta);
            cmd.Parameters.AddWithValue("@parrilla", inmueble.Parrilla);
            cmd.Parameters.AddWithValue("@garage", inmueble.Garage);
            cmd.Parameters.AddWithValue("@latitud", inmueble.Latitud);
            cmd.Parameters.AddWithValue("@longitud", inmueble.Longitud);
            cmd.Parameters.AddWithValue("@precio", inmueble.Precio);
            cmd.Parameters.AddWithValue("@ImagenPortada", inmueble.ImagenPortada);
            cmd.Parameters.AddWithValue("@vigente", inmueble.Vigente);
            conexion.Open();

            cmd.ExecuteNonQuery();
        }
        }

        public void EliminarInmueble(int id)
        {
            using (MySqlConnection conexion = new MySqlConnection(_connectionString))
            {
                string query = "DELETE FROM inmueble WHERE idInmueble = @id";
                MySqlCommand cmd = new MySqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@id", id);

                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public Propietario ObtenerPropietarioPorDni(int dni)
        {
            Propietario propietario = null;

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM propietario WHERE dniPropietario = @dni";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@dni", dni);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        propietario = new Propietario
                        {
                            DniPropietario = reader.GetString("DniPropietario"),
                            ApellidoPropietario = reader.GetString("ApellidoPropietario"),
                            NombrePropietario = reader.GetString("NombrePropietario"),
                            ContactoPropietario = reader.GetString("ContactoPropietario")

                        };
                    }
                }
            }

            return propietario;
        }

        public MySqlConnection ObtenerConexion()
        {
            return new MySqlConnection(_connectionString);
        }

        public List<Propietario> BuscarPropietariosPorNombreODni(string filtro)
        {
            List<Propietario> lista = new List<Propietario>();

            using (var connection = ObtenerConexion())
            {
                connection.Open();
                string query = @"SELECT *
                         FROM Propietario 
                         WHERE dniPropietario LIKE @filtro OR 
                               apellidoPropietario LIKE @filtro OR 
                               nombrePropietario LIKE @filtro";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@filtro", "%" + filtro + "%");
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Propietario
                            {
                                DniPropietario = reader.GetString("DniPropietario"),
                                ApellidoPropietario = reader.GetString("ApellidoPropietario"),
                                NombrePropietario = reader.GetString("NombrePropietario"),
                                ContactoPropietario = reader.GetString("ContactoPropietario")
                            });
                        }
                    }
                }
            }

            return lista;
        }

        public List<Propietario> BuscarPropietarios(string termino)
        {
            List<Propietario> lista = new List<Propietario>();

            if (string.IsNullOrWhiteSpace(termino))
            {
                return lista; // Retorna una lista vacía si el término es vacío o solo espacios
            }

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"SELECT * FROM Propietario 
                         WHERE dniPropietario LIKE @termino OR 
                               nombrePropietario LIKE @termino OR 
                               apellidoPropietario LIKE @termino";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@termino", "%" + termino + "%");

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Propietario
                            {
                                DniPropietario = reader.GetString("dniPropietario"),
                                ApellidoPropietario = reader.GetString("apellidoPropietario"),
                                NombrePropietario = reader.GetString("nombrePropietario"),
                                ContactoPropietario = reader.GetString("contactoPropietario")
                            });
                        }
                    }
                }
            }

            return lista;
        }



        public List<Propietario> ListarPropietarios()
        {
            List<Propietario> lista = new List<Propietario>();
            using (var conexion = ObtenerConexion())
            {
                conexion.Open();
                var comando = new MySqlCommand("SELECT * FROM propietario", conexion);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Propietario
                    {
                        DniPropietario = reader.GetString("dniPropietario"),
                        ApellidoPropietario = reader.GetString("apellidoPropietario"),
                        NombrePropietario = reader.GetString("nombrePropietario"),
                        ContactoPropietario = reader.GetString("contactoPropietario")
                    });
                }
            }
            return lista;
        }

        public void AgregarFotosCarrusel(int idInmueble, List<string> rutas)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                foreach (var ruta in rutas)
                {
                    var command = new MySqlCommand("INSERT INTO InmuebleFotoCarrusel (IdInmueble, RutaFoto) VALUES (@id, @ruta)", connection);
                    command.Parameters.AddWithValue("@id", idInmueble);
                    command.Parameters.AddWithValue("@ruta", ruta);
                    command.ExecuteNonQuery();
                }
            }
        }


        public List<Contrato> ObtenerTodosLosContratos()
        {
            var contratos = new List<Contrato>();

            using (var conexion = new MySqlConnection(_connectionString))
            {
                conexion.Open();
                var query = "SELECT * FROM Contrato"; // Asegúrate de que la tabla se llama Contratos en tu base de datos
                var comando = new MySqlCommand(query, conexion);
                var reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    contratos.Add(new Contrato
                    {
                        IdContrato = reader.GetInt32("idContrato"),
                        NombrePropietario = reader.GetString("nombrePropietario"),
                        NombreInquilino = reader.GetString("nombreInquilino"),
                        Direccion = reader.GetString("direccion"),
                        FechaInicio = reader.GetDateTime("fechaInicio"),
                        FechaFinal = reader.GetDateTime("fechaFinal"),
                        Monto = reader.GetDecimal("monto"),
                        Vigente = reader.GetBoolean("vigente")
                    });
                }
            }

            return contratos;
        }

        public Contrato ObtenerContratoPorId(int id)
        {
            Contrato contrato = null;

            using (var conexion = new MySqlConnection(_connectionString))
            {
                conexion.Open();
                var query = "SELECT * FROM Contrato WHERE idContrato = @id";
                var comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@id", id);
                var reader = comando.ExecuteReader();

                if (reader.Read())
                {
                    contrato = new Contrato
                    {
                        IdContrato = reader.GetInt32("idContrato"),
                        IdInmueble = reader.GetInt32("idInmueble"), // 💥 esto es lo que faltaba
                        DniPropietario = reader.GetString("dniPropietario"),
                        NombrePropietario = reader.GetString("nombrePropietario"),
                        DniInquilino = reader.GetString("dniInquilino"),
                        NombreInquilino = reader.GetString("nombreInquilino"),
                        Direccion = reader.GetString("direccion"),
                        FechaInicio = reader.GetDateTime("fechaInicio"),
                        FechaFinal = reader.GetDateTime("fechaFinal"),
                        Monto = reader.GetDecimal("monto"),
                        Vigente = reader.GetBoolean("vigente")
                    };
                }
            }

            return contrato;
        }


        // Método para agregar un nuevo contrato
        public bool AgregarContrato(Contrato contrato)
        {
            using (var conexion = new MySqlConnection(_connectionString))
            {
                conexion.Open();
                var query = @"INSERT INTO Contrato 
        (dniPropietario, nombrePropietario, dniInquilino, nombreInquilino, fechaInicio, fechaFinal, monto, idInmueble, direccion, vigente)
        VALUES 
        (@dniPropietario, @nombrePropietario, @dniInquilino, @nombreInquilino, @fechaInicio, @fechaFinal, @monto, @idInmueble, @direccion, @vigente)";

                var comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@dniPropietario", contrato.DniPropietario);
                comando.Parameters.AddWithValue("@nombrePropietario", contrato.NombrePropietario);
                comando.Parameters.AddWithValue("@dniInquilino", contrato.DniInquilino);
                comando.Parameters.AddWithValue("@nombreInquilino", contrato.NombreInquilino);
                comando.Parameters.AddWithValue("@fechaInicio", contrato.FechaInicio);
                comando.Parameters.AddWithValue("@fechaFinal", contrato.FechaFinal);
                comando.Parameters.AddWithValue("@monto", contrato.Monto);
                comando.Parameters.AddWithValue("@idInmueble", contrato.IdInmueble);
                comando.Parameters.AddWithValue("@direccion", contrato.Direccion);
                comando.Parameters.AddWithValue("@vigente", contrato.Vigente);

                var filasAfectadas = comando.ExecuteNonQuery();
                return filasAfectadas > 0;
            }
        }


        // Método para actualizar un contrato existente
        public bool ActualizarContrato(Contrato contrato)
        {
            using (var conexion = new MySqlConnection(_connectionString))
            {
                conexion.Open();
                var query = "UPDATE Contrato SET nombrePropietario = @nombrePropietario, nombreInquilino = @nombreInquilino, direccion = @direccion, " +
                            "fechaInicio = @fechaInicio, fechaFinal = @fechaFinal, monto = @monto, vigente = @vigente WHERE idContrato = @idContrato";
                var comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@nombrePropietario", contrato.NombrePropietario);
                comando.Parameters.AddWithValue("@nombreInquilino", contrato.NombreInquilino);
                comando.Parameters.AddWithValue("@direccion", contrato.Direccion);
                comando.Parameters.AddWithValue("@fechaInicio", contrato.FechaInicio);
                comando.Parameters.AddWithValue("@fechaFinal", contrato.FechaFinal);
                comando.Parameters.AddWithValue("@monto", contrato.Monto);
                comando.Parameters.AddWithValue("@vigente", contrato.Vigente);
                comando.Parameters.AddWithValue("@idContrato", contrato.IdContrato);

                var filasAfectadas = comando.ExecuteNonQuery();
                return filasAfectadas > 0;
            }
        }

        // Método para eliminar un contrato por ID
        public void EliminarContrato(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "DELETE FROM contrato WHERE idContrato = @id";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }




        // Método para buscar inquilinos
        public List<Inquilino> BuscarInquilinos(string termino)
        {
            var lista = new List<Inquilino>();

            using (var conexion = ObtenerConexion())
            {
                conexion.Open();
                string query = @"SELECT * FROM inquilino 
                         WHERE dniInquilino LIKE @termino OR nombreInquilino LIKE @termino OR apellidoInquilino LIKE @termino";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@termino", "%" + termino + "%");

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Inquilino
                            {
                                DniInquilino = reader["dniInquilino"].ToString(),
                                NombreInquilino = reader["nombreInquilino"].ToString(),
                                ApellidoInquilino = reader["apellidoInquilino"].ToString()

                            });
                        }
                    }
                }
            }

            return lista;
        }


        public List<Inmueble> ObtenerInmueblesPorDni(string dni)
        {
            List<Inmueble> lista = new List<Inmueble>();
            string query = "SELECT * FROM Inmueble WHERE dniPropietario = @dni AND vigente = 1" +
                "";

            using (var con = ObtenerConexion())
            {
                con.Open();
                using (var cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@dni", dni);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Inmueble
                            {
                                IdInmueble = Convert.ToInt32(reader["idInmueble"]),
                                Calle = reader["calle"].ToString(),
                                Nro = Convert.ToInt32(reader["nro"]),
                                Piso = reader["piso"] != DBNull.Value ? Convert.ToInt32(reader["piso"]) : 0,
                                Dpto = reader["dpto"] != DBNull.Value ? reader["dpto"].ToString() : "",
                                Localidad = reader["localidad"].ToString()
                            });
                        }
                    }
                }
            }

            return lista;
        }

        public bool EditarContrato(int idContrato, DateTime fechaInicio, DateTime fechaFinal, decimal monto)
        {
            using (var conexion = new MySqlConnection(_connectionString))
            {
                conexion.Open();
                var query = "UPDATE Contrato SET fechaInicio = @fechaInicio, fechaFinal = @fechaFinal, monto = @monto WHERE idContrato = @idContrato";
                var comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                comando.Parameters.AddWithValue("@fechaFinal", fechaFinal);
                comando.Parameters.AddWithValue("@monto", monto);
                comando.Parameters.AddWithValue("@idContrato", idContrato);

                var filasAfectadas = comando.ExecuteNonQuery();
                return filasAfectadas > 0;
            }
        }


        public bool ContratoTieneConflictoDeFechas(int idInmueble, DateTime fechaInicio, DateTime fechaFinal)
        {
            using (var conexion = new MySqlConnection(_connectionString))
            {
                conexion.Open();
                var query = @"SELECT COUNT(*) 
                      FROM Contrato 
                      WHERE idInmueble = @idInmueble
                        AND (
                             (@fechaInicio BETWEEN FechaInicio AND FechaFinal)
                             OR (@fechaFinal BETWEEN FechaInicio AND FechaFinal)
                             OR (FechaInicio BETWEEN @fechaInicio AND @fechaFinal)
                            )";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idInmueble", idInmueble);
                    cmd.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                    cmd.Parameters.AddWithValue("@fechaFinal", fechaFinal);

                    var resultado = Convert.ToInt32(cmd.ExecuteScalar());
                    return resultado > 0;
                }
            }
        }

        public List<Contrato> ObtenerContratosPorInmueble(int idInmueble)
        {
            List<Contrato> lista = new List<Contrato>();
            using (var conn = ObtenerConexion())
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT * FROM Contrato WHERE idInmueble = @id", conn);
                cmd.Parameters.AddWithValue("@id", idInmueble);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Contrato
                        {
                            IdContrato = Convert.ToInt32(reader["idContrato"]),
                            FechaInicio = Convert.ToDateTime(reader["fechaInicio"]),
                            FechaFinal = Convert.ToDateTime(reader["fechaFinal"]),
                            // podés sumar más propiedades si necesitás
                        });
                    }
                }
            }
            return lista;
        }

        public bool ExisteContratoEnFechas(int idInmueble, DateTime fechaInicio, DateTime fechaFinal)
        {
            bool existe = false;

            using (var conexion = ObtenerConexion())
            {
                conexion.Open();

                string query = @"
            SELECT COUNT(*) FROM Contrato
            WHERE idInmueble = @idInmueble
              AND vigente = 1
              AND (
                    (@fechaInicio BETWEEN fechaInicio AND fechaFinal)
                 OR (@fechaFinal BETWEEN fechaInicio AND fechaFinal)
                 OR (fechaInicio BETWEEN @fechaInicio AND @fechaFinal)
                 OR (fechaFinal BETWEEN @fechaInicio AND @fechaFinal)
              );";

                using (var comando = new MySqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@idInmueble", idInmueble);
                    comando.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                    comando.Parameters.AddWithValue("@fechaFinal", fechaFinal);

                    int cantidad = Convert.ToInt32(comando.ExecuteScalar());
                    existe = cantidad > 0;
                }
            }

            return existe;
        }

        public bool VerificarDisponibilidad(Contrato nuevoContrato)
        {
            using (var conexion = ObtenerConexion())
            {
                conexion.Open();
                var comando = conexion.CreateCommand();
                comando.CommandText = @"
            SELECT COUNT(*) 
            FROM Contrato 
            WHERE IdInmueble = @idInmueble 
            AND (
                (@inicio BETWEEN FechaInicio AND FechaFinal)
                OR (@final BETWEEN FechaInicio AND FechaFinal)
                OR (FechaInicio BETWEEN @inicio AND @final)
                OR (FechaFinal BETWEEN @inicio AND @final)
            )
        ";

                comando.Parameters.AddWithValue("@idInmueble", nuevoContrato.IdInmueble);
                comando.Parameters.AddWithValue("@inicio", nuevoContrato.FechaInicio);
                comando.Parameters.AddWithValue("@final", nuevoContrato.FechaFinal);

                int cantidad = Convert.ToInt32(comando.ExecuteScalar());

                return cantidad == 0; // Devuelve true si está disponible
            }
        }
        //---------------------- PAGOS  ------------------------------------
        public List<Pago> ObtenerPagosPorContrato(int idContrato)
        {
            List<Pago> lista = new List<Pago>();

            using (MySqlConnection conexion = new MySqlConnection(_connectionString))
            {
                conexion.Open();

                string query = "SELECT * FROM Pagos WHERE idContrato = @idContrato ORDER BY nroPago ASC";
                MySqlCommand cmd = new MySqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@idContrato", idContrato);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Pago
                    {
                        IdPago = Convert.ToInt32(reader["idPago"]),
                        NroPago = Convert.ToInt32(reader["nroPago"]),
                        FechaPago = Convert.ToDateTime(reader["fechaPago"]),
                        Importe = Convert.ToDecimal(reader["importe"]),
                        Detalle = reader["detalle"].ToString(),
                        Estado = reader["estado"].ToString(),
                        IdContrato = Convert.ToInt32(reader["idContrato"])
                    });
                }
            }

            return lista;
        }


        public Pago ObtenerPagoPorId(int idPago)
        {
            Pago pago = null;
            using (var conn = ObtenerConexion())
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT * FROM pagos WHERE idPago = @idPago ORDER BY nroPago ASC", conn);
                cmd.Parameters.AddWithValue("@idPago", idPago);
                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    pago = new Pago
                    {
                        IdPago = reader.GetInt32("idPago"),
                        IdContrato = reader.GetInt32("idContrato"),
                        NroPago = reader.GetInt32("nroPago"),
                        FechaPago = reader.GetDateTime("fechaPago"),
                        Importe = reader.GetDecimal("importe"),
                        Detalle = reader.GetString("detalle"),
                        Estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? null : reader.GetString("estado")
                    };
                }
            }
            return pago;
        }

        public void ActualizarPago(Pago pago)
        {
            using (var conn = ObtenerConexion())
            {
                conn.Open();
                var cmd = new MySqlCommand("UPDATE pagos SET detalle = @detalle, estado = @estado WHERE idPago = @idPago", conn);
                cmd.Parameters.AddWithValue("@detalle", pago.Detalle);
                cmd.Parameters.AddWithValue("@estado", pago.Estado);
                cmd.Parameters.AddWithValue("@idPago", pago.IdPago);
                cmd.ExecuteNonQuery();
            }
        }


        public bool AgregarPago(Pago pago)
        {
            try
            {
                using (var conexion = ObtenerConexion())
                {
                    conexion.Open(); // 

                    var comando = new MySqlCommand(@"INSERT INTO pagos 
                (idContrato, nroPago, fechaPago, importe, detalle, estado) 
                VALUES 
                (@idContrato, @nroPago, @fechaPago, @importe, @detalle, @estado)", conexion);

                    comando.Parameters.AddWithValue("@idContrato", pago.IdContrato);
                    comando.Parameters.AddWithValue("@nroPago", pago.NroPago);
                    comando.Parameters.AddWithValue("@fechaPago", pago.FechaPago);
                    comando.Parameters.AddWithValue("@importe", pago.Importe);
                    comando.Parameters.AddWithValue("@detalle", pago.Detalle ?? "");
                    comando.Parameters.AddWithValue("@estado", pago.Estado ?? "Pagado");

                    int filasAfectadas = comando.ExecuteNonQuery();

                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al agregar pago: " + ex.Message);
                Console.WriteLine("StackTrace: " + ex.StackTrace);
                return false;
            }
        }


        public void AnularPago(int idPago)
        {
            using (var db = new MySqlConnection(_connectionString))
            {
                var query = "UPDATE Pagos SET Estado = 'Anulado' WHERE IdPago = @IdPago";

                var cmd = new MySqlCommand(query, db);
                cmd.Parameters.AddWithValue("@IdPago", idPago);

                db.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public void ActualizarDetalle(int idPago, string nuevoDetalle)
        {
            using (MySqlConnection conexion = new MySqlConnection(_connectionString))
            {
                conexion.Open();
                string query = "UPDATE Pagos SET detalle = @detalle WHERE idPago = @idPago";

                using (MySqlCommand comando = new MySqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@detalle", nuevoDetalle);
                    comando.Parameters.AddWithValue("@idPago", idPago);
                    comando.ExecuteNonQuery();
                }
            }
        }



        public List<Contrato> ObtenerContratos()
        {
            var lista = new List<Contrato>();
            using (var conn = ObtenerConexion())
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT idContrato, idInmueble, fechaInicio, fechaFinal FROM Contrato", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Contrato
                    {
                        IdContrato = Convert.ToInt32(reader["idContrato"]),
                        IdInmueble = Convert.ToInt32(reader["idInmueble"]),
                        FechaInicio = Convert.ToDateTime(reader["fechaInicio"]),
                        FechaFinal = Convert.ToDateTime(reader["fechaFinal"])
                    });
                }
            }
            return lista;
        }


       public List<Inmueble> ObtenerTodosLosInmuebles()
{
    var lista = new List<Inmueble>();
    using (var conn = ObtenerConexion())
    {
        conn.Open();
        var cmd = new MySqlCommand("SELECT * FROM Inmueble", conn);
        var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            lista.Add(new Inmueble
            {
                // Usamos el operador ?? para asignar un valor por defecto en caso de null
                IdInmueble = reader["idInmueble"] != DBNull.Value ? Convert.ToInt32(reader["idInmueble"]) : 0,
                DniPropietario = reader["dniPropietario"] as string ?? string.Empty,  // Usa "as" para evitar errores de tipo
                Calle = reader["calle"] as string ?? string.Empty,
                Nro = reader["nro"] != DBNull.Value ? Convert.ToInt32(reader["nro"]) : 0,
                Piso = reader["piso"] != DBNull.Value ? Convert.ToInt32(reader["piso"]) : 0,
                Dpto = reader["dpto"] as string ?? string.Empty,
                Localidad = reader["localidad"] as string ?? string.Empty,
                Provincia = reader["provincia"] as string ?? string.Empty,
                Uso = reader["uso"] as string ?? string.Empty,
                Tipo = reader["tipo"] as string ?? string.Empty,
                Ambientes = reader["ambientes"] != DBNull.Value ? Convert.ToInt32(reader["ambientes"]) : 0,
                Pileta = reader["pileta"] != DBNull.Value ? Convert.ToBoolean(reader["pileta"]) : false,
                Parrilla = reader["parrilla"] != DBNull.Value ? Convert.ToBoolean(reader["parrilla"]) : false,
                Garage = reader["garage"] != DBNull.Value ? Convert.ToBoolean(reader["garage"]) : false,
                Precio = reader["precio"] != DBNull.Value ? Convert.ToDouble(reader["precio"]) : 0.0,
                Vigente = reader["vigente"] != DBNull.Value ? Convert.ToBoolean(reader["vigente"]) : false,
            });
        }
    }
    return lista;
}


public List<Inmueble> ObtenerInmueblesOcupados(DateTime desde, DateTime hasta)       
{
    List<Inmueble> lista = new List<Inmueble>();
    using (var conexion = ObtenerConexion())
    {
        conexion.Open();
        string query = @"
        SELECT *
        FROM Inmueble i
        WHERE NOT EXISTS (
            SELECT 1
            FROM Contrato c
            WHERE c.idInmueble = i.idInmueble
              AND c.fechaInicio <= @hasta
              AND c.fechaFinal >= @desde
        )";

        using (var comando = new MySqlCommand(query, conexion))
        {
            comando.Parameters.AddWithValue("@desde", desde);
            comando.Parameters.AddWithValue("@hasta", hasta);

            using (var reader = comando.ExecuteReader())
            {
                while (reader.Read())
                {
                    lista.Add(new Inmueble
                    {
                        IdInmueble = reader["idInmueble"] != DBNull.Value ? Convert.ToInt32(reader["idInmueble"]) : 0,
                        DniPropietario = reader["dniPropietario"] as string ?? string.Empty,
                        Calle = reader["calle"] as string ?? string.Empty,
                        Nro = reader["nro"] != DBNull.Value ? Convert.ToInt32(reader["nro"]) : 0,
                        Piso = reader["piso"] != DBNull.Value ? Convert.ToInt32(reader["piso"]) : 0,
                        Dpto = reader["dpto"] as string ?? string.Empty,
                        Localidad = reader["localidad"] as string ?? string.Empty,
                        Provincia = reader["provincia"] as string ?? string.Empty,
                        Uso = reader["uso"] as string ?? string.Empty,
                        Tipo = reader["tipo"] as string ?? string.Empty,
                        Ambientes = reader["ambientes"] != DBNull.Value ? Convert.ToInt32(reader["ambientes"]) : 0,
                        Latitud = reader["latitud"] != DBNull.Value ? Convert.ToDouble(reader["latitud"]) : 0.0,
                        Longitud = reader["longitud"] != DBNull.Value ? Convert.ToDouble(reader["longitud"]) : 0.0,
                        Precio = reader["precio"] != DBNull.Value ? Convert.ToDouble(reader["precio"]) : 0.0,
                        ImagenPortada = reader["imagenPortada"] as string ?? string.Empty,
                        // Cambié la lógica para verificar "1" y "true" de forma más segura
                        Pileta = reader["pileta"] != DBNull.Value && (reader["pileta"].ToString() == "1" || reader["pileta"].ToString().ToLower() == "true"),
                        Parrilla = reader["parrilla"] != DBNull.Value && (reader["parrilla"].ToString() == "1" || reader["parrilla"].ToString().ToLower() == "true"),
                        Garage = reader["garage"] != DBNull.Value && (reader["garage"].ToString() == "1" || reader["garage"].ToString().ToLower() == "true"),
                        Vigente = reader["vigente"] != DBNull.Value && (reader["vigente"].ToString() == "1" || reader["vigente"].ToString().ToLower() == "true"),
                    });
                }
            }
        }
    }
    return lista;
}



        public Propietario ObtenerPropietarioPorDni(string dni)
        {
            Propietario propietario = null;

            using (MySqlConnection conexion = new MySqlConnection(_connectionString))
            {
                conexion.Open();
                string consulta = "SELECT * FROM propietario WHERE dniPropietario = @dni";
                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@dni", dni);
                    using (MySqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            propietario = new Propietario
                            {
                                DniPropietario = reader["dniPropietario"].ToString(),
                                NombrePropietario = reader["nombrePropietario"].ToString(),
                                ApellidoPropietario = reader["apellidoPropietario"].ToString(),

                            };
                        }
                    }
                }
            }

            return propietario;
        }

        public Inquilino ObtenerInquilinoPorDni(string dni)
        {
            Inquilino inquilino = null;

            using (MySqlConnection conexion = new MySqlConnection(_connectionString))
            {
                conexion.Open();
                string consulta = "SELECT * FROM inquilino WHERE dniInquilino = @dni";
                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@dni", dni);
                    using (MySqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            inquilino = new Inquilino
                            {
                                DniInquilino = reader["dniInquilino"].ToString(),
                                NombreInquilino = reader["nombreInquilino"].ToString(),
                                ApellidoInquilino = reader["apellidoInquilino"].ToString(),

                            };
                        }
                    }
                }
            }

            return inquilino;
        }



        public List<Contrato> BuscarContratos(DateTime desde, DateTime hasta)
        {
            List<Contrato> contratos = new List<Contrato>();

            using (MySqlConnection conexion = new MySqlConnection(_connectionString))
            {
                conexion.Open();
                string query = @"SELECT * FROM Contrato 
                         WHERE FechaInicio <= @hasta AND FechaFinal >= @desde";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@desde", desde);
                    cmd.Parameters.AddWithValue("@hasta", hasta);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Contrato contrato = new Contrato
                            {
                                IdContrato = Convert.ToInt32(reader["IdContrato"]),
                                DniPropietario = reader["DniPropietario"].ToString(),
                                DniInquilino = reader["DniInquilino"].ToString(),
                                IdInmueble = Convert.ToInt32(reader["IdInmueble"]),
                                Direccion = reader["Direccion"].ToString(),
                                FechaInicio = Convert.ToDateTime(reader["FechaInicio"]),
                                FechaFinal = Convert.ToDateTime(reader["FechaFinal"]),
                                Monto = Convert.ToDecimal(reader["Monto"]),
                                Vigente = Convert.ToBoolean(reader["Vigente"])
                            };
                            contratos.Add(contrato);
                        }
                    }
                }
            }

            return contratos;
        }


        public void MarcarComoNoVigente(int idContrato)
        {
            using (var db = new MySqlConnection (_connectionString))  // Asegúrate de usar tu cadena de conexión
            {
                db.Open();
                var query = "UPDATE Contrato SET Vigente = 0 WHERE IdContrato = @IdContrato";

                var command = new MySqlCommand(query, db);
                command.Parameters.AddWithValue("@IdContrato", idContrato);

                command.ExecuteNonQuery();
            }
        }

        public void MarcarContratoComoNoVigente(int idContrato)
        {
            using (var conn = ObtenerConexion())
            {
                conn.Open();
                string query = "UPDATE Contrato SET Vigente = 0 WHERE idContrato = @id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idContrato);
                    cmd.ExecuteNonQuery();
                }
            }
        }



    }
}

