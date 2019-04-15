using RestPrematricula.Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RestPrematricula.Persistencia
{
    public class PreMatriculaAlumnoDAO
    {
        private string strConection = "Data Source=.;Initial Catalog=PreMatricula;Integrated Security=True";
        public PreMatriculaAlumno Crear(PreMatriculaAlumno Creado)
        {
            using (SqlConnection Conexion = new SqlConnection(strConection))
            {
                Conexion.Open();
                string sql = "INSERT INTO [dbo].[Alumno] ([DNIAlumno], [NombreAlumno], [ApellidoAlumno],[Colegio],[Estado]) VALUES (@DNIAlumno,@NombreAlumno, @ApellidoAlumno, @Colegio,@Estado)";
                using (SqlCommand Comando = new SqlCommand(sql, Conexion))
                {
                    Comando.Parameters.Add(new SqlParameter("@DNIAlumno", Creado.DNIAlumno));
                    Comando.Parameters.Add(new SqlParameter("@NombreAlumno", Creado.NombreAlumno));
                    Comando.Parameters.Add(new SqlParameter("@ApellidoAlumno", Creado.ApellidoAlumno));
                    Comando.Parameters.Add(new SqlParameter("@Colegio", Creado.Colegio));
                    Comando.Parameters.Add(new SqlParameter("@Estado", Creado.Estado));
                    Comando.ExecuteNonQuery();
                }
            }
            return Obtener(Creado.DNIAlumno);
        }

        public PreMatriculaAlumno Modificar(PreMatriculaAlumno Modificado)
        {
            using (SqlConnection Conexion = new SqlConnection(strConection))
            {
                Conexion.Open();
                string sql = "UPDATE dbo.Alumno SET [DNIAlumno]=@DNIAlumno, [NombreAlumno]=@NombreAlumno, [ApellidoAlumno]=@ApellidoAlumno, [Colegio]=@Colegio,[Estado]=@Estado WHERE DNIAlumno = @DNIAlumno";
                using (SqlCommand Comando = new SqlCommand(sql, Conexion))
                {
                    Comando.Parameters.Add(new SqlParameter("@DNIAlumno", Modificado.DNIAlumno));
                    Comando.Parameters.Add(new SqlParameter("@NombreAlumno", Modificado.NombreAlumno));
                    Comando.Parameters.Add(new SqlParameter("@ApellidoAlumno", Modificado.ApellidoAlumno));
                    Comando.Parameters.Add(new SqlParameter("@Colegio", Modificado.Colegio));
                    Comando.Parameters.Add(new SqlParameter("@Estado", Modificado.Estado));
                    Comando.ExecuteNonQuery();
                }
            }
            return Obtener(Modificado.DNIAlumno);
        }

        public PreMatriculaAlumno Obtener(string DNIAlumno)
        {
            PreMatriculaAlumno Alumno = null;
            using (SqlConnection Conexion = new SqlConnection(strConection))
            {
                Conexion.Open();
                using (SqlCommand Comando = new SqlCommand("SELECT * FROM dbo.Alumno WHERE DNIAlumno = @DNIAlumno", Conexion))
                {
                    Comando.Parameters.Add(new SqlParameter("@DNIAlumno", DNIAlumno));
                    using (SqlDataReader Read = Comando.ExecuteReader())
                    {
                        if (Read.Read())
                        {
                            Alumno = new PreMatriculaAlumno();
                            Alumno.DNIAlumno = (string)Read["DNIAlumno"];
                            Alumno.NombreAlumno = (string)Read["NombreAlumno"];
                            Alumno.ApellidoAlumno = (string)Read["ApellidoAlumno"];
                            Alumno.Colegio = (string)Read["Colegio"];
                            Alumno.Estado = (string)Read["Estado"];
                        }
                    }
                }
            }
            return Alumno;
        }

        public void Eliminar(string DNIAlumno)
        {
            using (SqlConnection Conexion = new SqlConnection(strConection))
            {
                Conexion.Open();
                using (SqlCommand Comando = new SqlCommand("DELETE FROM dbo.Alumno WHERE DNIAlumno = @DNIAlumno", Conexion))
                {
                    Comando.Parameters.Add(new SqlParameter("@DNIAlumno", DNIAlumno));
                    Comando.ExecuteNonQuery();
                }
            }
        }

        public List<PreMatriculaAlumno> Listar()
        {
            List<PreMatriculaAlumno> Alumnos = new List<PreMatriculaAlumno>();
            PreMatriculaAlumno Alumno = null;
            using (SqlConnection Conexion = new SqlConnection(strConection))
            {
                Conexion.Open();
                using (SqlCommand Comando = new SqlCommand("SELECT * FROM dbo.Alumno", Conexion))
                {
                    using (SqlDataReader Read = Comando.ExecuteReader())
                    {
                        while (Read.Read())
                        {
                            Alumno = new PreMatriculaAlumno();
                            Alumno.DNIAlumno = (string)Read["DNIAlumno"];
                            Alumno.NombreAlumno = (string)Read["NombreAlumno"];
                            Alumno.ApellidoAlumno = (string)Read["ApellidoAlumno"];
                            Alumno.Colegio = (string)Read["Colegio"];
                            Alumno.Colegio = (string)Read["Estado"];
                            Alumnos.Add(Alumno);
                        }
                    }
                }
            }
            return Alumnos;
        }
        public List<PreMatriculaAlumno> ListarColegioEstado(string colegio, string estado)
        {
            List<PreMatriculaAlumno> Alumnos = new List<PreMatriculaAlumno>();
            PreMatriculaAlumno Alumno = null;
            using (SqlConnection Conexion = new SqlConnection(strConection))
            {
                Conexion.Open();
                using (SqlCommand Comando = new SqlCommand("SELECT * FROM dbo.Alumno where Colegio=@Colegio and Estado=@Estado", Conexion))
                {
                    Comando.Parameters.Add(new SqlParameter("@Colegio", colegio));
                    Comando.Parameters.Add(new SqlParameter("@Estado", estado));
                    using (SqlDataReader Read = Comando.ExecuteReader())
                    {
                        while (Read.Read())
                        {
                            Alumno = new PreMatriculaAlumno();
                            Alumno.DNIAlumno = (string)Read["DNIAlumno"];
                            Alumno.NombreAlumno = (string)Read["NombreAlumno"];
                            Alumno.ApellidoAlumno = (string)Read["ApellidoAlumno"];
                            Alumno.Colegio = (string)Read["Colegio"];
                            Alumno.Colegio = (string)Read["Estado"];
                            Alumnos.Add(Alumno);
                        }
                    }
                }
            }
            return Alumnos;
        }
    }
}