using RestPrematricula.Dominio;
using RestPrematricula.Errores;
using RestPrematricula.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RestPrematricula
{
    public class PreMatricula : IPreMatricula
    {
        PreMatriculaAlumnoDAO objPreMatricula = new PreMatriculaAlumnoDAO();

        public Dominio.PreMatriculaAlumno Crear(PreMatriculaAlumno alumno)
        {
            if (objPreMatricula.Obtener(alumno.DNIAlumno) != null)//VALIDA QUE EL DNI NO SEA REPETIDO
            {
                throw new FaultException<RepetidoException>(new RepetidoException()
                {
                    Codigo = "101",
                    Descripcion = "El alumno ya existe"
                }, new FaultReason("Error al intentar creacion"));
            }
            return objPreMatricula.Crear(alumno);
        }

        public Dominio.PreMatriculaAlumno Modificar(Dominio.PreMatriculaAlumno alumno)
        {
            return objPreMatricula.Modificar(alumno);
        }

        public PreMatriculaAlumno Obtener(string DNIAlumno)
        {
            return objPreMatricula.Obtener(DNIAlumno);
        }

        public void Eliminar(string DNIAlumno)
        {
            PreMatriculaAlumno alumno = objPreMatricula.Obtener(DNIAlumno);
            if (alumno != null && alumno.Estado != "Inactivo")//VALIDAMOS QUE EL ALUMNO NO ESTÉ CON ESTADO
                                                              //INACTIVO, DE SER EL CASO GENERA UN ERROR.
            {
                throw new FaultException<RepetidoException>(new RepetidoException()
                {
                    Codigo = "102",
                    Descripcion = "No se puede eliminar el alumno, se encuentra inactivo."
                }, new FaultReason("Error al intentar creacion"));
            }
            objPreMatricula.Eliminar(DNIAlumno);
        }

        public List<PreMatriculaAlumno> Listar()
        {
            return objPreMatricula.Listar();
        }

        public List<PreMatriculaAlumno> ListarColegioEstado(string colegio, string estado)
        {
            return objPreMatricula.ListarColegioEstado(colegio, estado);
        }
    }
}
