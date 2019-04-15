using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RestPrematricula.Dominio
{
    [DataContract]
    public class PreMatriculaAlumno
    {
        [DataMember]
        public int IDRegistro { get; set; }
        [DataMember]
        public string DNIAlumno { get; set; }
        [DataMember]
        public string NombreAlumno { get; set; }
        [DataMember]
        public string ApellidoAlumno { get; set; }
        [DataMember]
        public string Colegio { get; set; }
         [DataMember]
        public string Estado { get; set; }
    }
}