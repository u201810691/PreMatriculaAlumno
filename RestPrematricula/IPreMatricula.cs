using RestPrematricula.Dominio;
using RestPrematricula.Errores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RestPrematricula
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IPreMatricula" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IPreMatricula
    {
        [FaultContract(typeof(RepetidoException))] // El manejador de errores
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "PreMatriculaAlumno", ResponseFormat = WebMessageFormat.Json)]
        PreMatriculaAlumno Crear(PreMatriculaAlumno alumno);
        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "PreMatriculaAlumno", ResponseFormat = WebMessageFormat.Json)]
        PreMatriculaAlumno Modificar(PreMatriculaAlumno alumno);
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "PreMatriculaAlumno/{DNIAlumno}", ResponseFormat = WebMessageFormat.Json)]
        PreMatriculaAlumno Obtener(string DNIAlumno);
        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "PreMatriculaAlumno/{DNIAlumno}", ResponseFormat = WebMessageFormat.Json)]
        void Eliminar(string DNIAlumno);
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "PreMatriculaAlumno", ResponseFormat = WebMessageFormat.Json)]
        List<PreMatriculaAlumno> Listar();
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "PreMatriculaAlumno/{colegio}/{estado}", ResponseFormat = WebMessageFormat.Json)]
        List<PreMatriculaAlumno> ListarColegioEstado(string colegio, string estado);
    }
}
