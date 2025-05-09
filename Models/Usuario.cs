namespace proyectoInmobiliariaNuevo.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string UsuarioNombre { get; set; } // Usá este nombre si 'Usuario' da conflicto
        public string Contraseña { get; set; }
        public string Rol { get; set; }
        public string NombreyApellido { get; set; }
        public string? FotoPerfil { get; set; }
    }
}
