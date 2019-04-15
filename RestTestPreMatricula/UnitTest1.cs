using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Script.Serialization;
using System.Text;
using System.Net;
using System.IO;
using System.Collections.Generic;

namespace RestTestPreMatricula
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCrear()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            PreMatriculaAlumno alumno = new PreMatriculaAlumno()
            {
                DNIAlumno = "99988877",
                NombreAlumno = "María",
                ApellidoAlumno = "Garcia",
                Colegio = "Santa María",
                Estado = "Activo"

            };

            string jsonSolicitud = js.Serialize(alumno);
            byte[] ByteMatricula = Encoding.UTF8.GetBytes(jsonSolicitud);
            HttpWebRequest request = WebRequest.Create("http://localhost:8915/PreMatricula.svc/PreMatriculaAlumno") as HttpWebRequest;
            request.Method = "POST";
            request.ContentLength = ByteMatricula.Length;
            request.ContentType = "application/json";
            var rqt = request.GetRequestStream();
            rqt.Write(ByteMatricula, 0, ByteMatricula.Length);
            HttpWebResponse rsp = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(rsp.GetResponseStream());
            string tramaJson = reader.ReadToEnd();
            PreMatriculaAlumno alumnoCreado = js.Deserialize<PreMatriculaAlumno>(tramaJson);
            Assert.AreEqual("99988877", alumnoCreado.DNIAlumno);
            Assert.AreEqual("María", alumnoCreado.NombreAlumno);
            Assert.AreEqual("Garcia", alumnoCreado.ApellidoAlumno);
            Assert.AreEqual("Santa María", alumnoCreado.Colegio);
            Assert.AreEqual("Activo", alumnoCreado.Estado);

        }
        [TestMethod]
        public void TestCrearRepetido()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            try
            {
                PreMatriculaAlumno alumno = new PreMatriculaAlumno()
                    {
                        DNIAlumno = "99988877",
                        NombreAlumno = "María",
                        ApellidoAlumno = "Garcia",
                        Colegio = "Santa María",
                        Estado = "Activo"

                    };

                string jsonSolicitud = js.Serialize(alumno);
                byte[] ByteMatricula = Encoding.UTF8.GetBytes(jsonSolicitud);
                HttpWebRequest request = WebRequest.Create("http://localhost:8915/PreMatricula.svc/PreMatriculaAlumno") as HttpWebRequest;
                request.Method = "POST";
                request.ContentLength = ByteMatricula.Length;
                request.ContentType = "application/json";
                var rqt = request.GetRequestStream();
            }
            catch (WebException error)
            {
                HttpStatusCode codigo = ((HttpWebResponse)error.Response).StatusCode;
                StreamReader reader = new StreamReader(error.Response.GetResponseStream());
                string tramaJson = reader.ReadToEnd();
                RepetidoException repetidoException = js.Deserialize<RepetidoException>(tramaJson);
                Assert.AreEqual(HttpStatusCode.Conflict, codigo);
                Assert.AreEqual("101", repetidoException.Codigo);
                Assert.AreEqual("El alumno ya existe", repetidoException.Descripcion);
            }
        }
        [TestMethod]
        public void TestModificar()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            PreMatriculaAlumno alumno = new PreMatriculaAlumno()
            {
                DNIAlumno = "99988877",
                NombreAlumno = "María 2",
                ApellidoAlumno = "Garcia 2",
                Colegio = "Santa María",
                Estado = "Inactivo"

            };

            string jsonSolicitud = js.Serialize(alumno);
            byte[] ByteMatricula = Encoding.UTF8.GetBytes(jsonSolicitud);
            HttpWebRequest request = WebRequest.Create("http://localhost:8915/PreMatricula.svc/PreMatriculaAlumno") as HttpWebRequest;
            request.Method = "PUT";
            request.ContentLength = ByteMatricula.Length;
            request.ContentType = "application/json";
            var rqt = request.GetRequestStream();
            rqt.Write(ByteMatricula, 0, ByteMatricula.Length);
            HttpWebResponse rsp = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(rsp.GetResponseStream());
            string tramaJson = reader.ReadToEnd();
            PreMatriculaAlumno alumnoCreado = js.Deserialize<PreMatriculaAlumno>(tramaJson);
            Assert.AreEqual("99988877", alumnoCreado.DNIAlumno);
            Assert.AreEqual("María 2", alumnoCreado.NombreAlumno);
            Assert.AreEqual("Garcia 2", alumnoCreado.ApellidoAlumno);
            Assert.AreEqual("Santa María", alumnoCreado.Colegio);
            Assert.AreEqual("Inactivo", alumnoCreado.Estado);

        }
        [TestMethod]
        public void TestObtener()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            HttpWebRequest request = WebRequest.Create("http://localhost:8915/PreMatricula.svc/PreMatriculaAlumno/99988877") as HttpWebRequest;
            request.Method = "GET";
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string tramaJson = reader.ReadToEnd();
            PreMatriculaAlumno alumno = js.Deserialize<PreMatriculaAlumno>(tramaJson);
            Assert.AreEqual("99988877", alumno.DNIAlumno);
            Assert.AreEqual("María 2", alumno.NombreAlumno);
            Assert.AreEqual("Garcia 2", alumno.ApellidoAlumno);
            Assert.AreEqual("Santa María", alumno.Colegio);
            Assert.AreEqual("Inactivo", alumno.Estado);
        }
        [TestMethod]
        public void TestListar()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            HttpWebRequest request = WebRequest.Create("http://localhost:8915/PreMatricula.svc/PreMatriculaAlumno") as HttpWebRequest;
            request.Method = "GET";
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string tramaJson = reader.ReadToEnd();
            List<PreMatriculaAlumno> alumno = js.Deserialize<List<PreMatriculaAlumno>>(tramaJson);
            Assert.AreEqual(2, alumno.Count);
        }
        [TestMethod]
        public void TestListarColegioEstado()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            HttpWebRequest request = WebRequest.Create("http://localhost:8915/PreMatricula.svc/PreMatriculaAlumno/San%20Vicente/Inactivo") as HttpWebRequest;
            request.Method = "GET";
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string tramaJson = reader.ReadToEnd();
            List<PreMatriculaAlumno> alumno = js.Deserialize<List<PreMatriculaAlumno>>(tramaJson);
            Assert.AreEqual(1, alumno.Count);
        }
        [TestMethod]
        public void TestEliminar()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            HttpWebRequest requestD = WebRequest.Create("http://localhost:8915/PreMatricula.svc/PreMatriculaAlumno/87654321") as HttpWebRequest;
            requestD.Method = "DELETE";
            HttpWebResponse responseD = requestD.GetResponse() as HttpWebResponse;

            HttpWebRequest request = WebRequest.Create("http://localhost:8915/PreMatricula.svc/PreMatriculaAlumno/87654321") as HttpWebRequest;
            request.Method = "GET";
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string tramaJson = reader.ReadToEnd();
            PreMatriculaAlumno solicitud = js.Deserialize<PreMatriculaAlumno>(tramaJson);
            Assert.IsNull(solicitud);
        }
        [TestMethod]
        public void TestEliminarInactivo()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            try
            {
                HttpWebRequest requestD = WebRequest.Create("http://localhost:8915/PreMatricula.svc/PreMatriculaAlumno/87654321") as HttpWebRequest;
                requestD.Method = "DELETE";
                HttpWebResponse responseD = requestD.GetResponse() as HttpWebResponse;
            }
            catch (WebException error)
            {
                HttpStatusCode codigo = ((HttpWebResponse)error.Response).StatusCode;
                StreamReader reader = new StreamReader(error.Response.GetResponseStream());
                string tramaJson = reader.ReadToEnd();
                RepetidoException repetidoException = js.Deserialize<RepetidoException>(tramaJson);
                Assert.AreEqual(HttpStatusCode.Conflict, codigo);
                Assert.AreEqual("102", repetidoException.Codigo);
                Assert.AreEqual("No se puede eliminar el alumno, se encuentra inactivo.", repetidoException.Descripcion);
            }
        }
    }
}
