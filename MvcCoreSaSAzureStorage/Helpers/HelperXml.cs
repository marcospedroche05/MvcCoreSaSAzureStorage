using MvcCoreSaSAzureStorage.Models;
using System.Xml.Linq;

namespace MvcCoreSaSAzureStorage.Helpers
{
    public class HelperXml
    {
        private XDocument document;
        public HelperXml()
        {
            string pathXML = "MvcCoreSaSAzureStorage.Documents.alumnos_tables.xml";
            Stream stream = this.GetType().Assembly.GetManifestResourceStream(pathXML);
            this.document = XDocument.Load(stream);
        }

        public List<Alumno> GetAlumnos()
        {
            var consulta = from datos in
                               this.document.Descendants("alumno")
                           select new Alumno
                           {
                               IdAlumno = int.Parse(datos.Element("idalumno").Value),
                               Nombre = datos.Element("nombre").Value,
                               Curso = datos.Element("curso").Value,
                               Apellidos = datos.Element("apellidos").Value,
                               Nota = int.Parse(datos.Element("nota").Value)
                           };
            return consulta.ToList();
        }
    }
}
