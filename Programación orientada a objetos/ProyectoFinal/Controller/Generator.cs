using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Controller
{
    public static class Generator
    {
        //Generador de PDF de los datos del ciudadano y su cita
        public static void GeneratorPDF(string name, string dui, string direction, string phone, string Email, DateTime datetime, string place)
        {
            FileStream fs = new FileStream($"c://pdf/reporte_Citizen_{name}.pdf", FileMode.Create);
            Document document = new Document(iTextSharp.text.PageSize.LETTER, 3, 3, 5, 5);
            PdfWriter pw = PdfWriter.GetInstance(document, fs);

            document.Open();

            document.Add(new Paragraph("<<<<<<<<<<<<<<<<<<<<<<<<DATOS DEL CIUDADANO>>>>>>>>>>>>>>>>>>>>>>>>>>\n" + //Contenido del PDF
                $"Nombre del ciudadano: {name}\n" +
                $"DUI : {dui} \n" +
                $"Direccion: {direction} \n" +
                $"Telefono: {phone}\n " +
                $"Correo Electronico: {Email} \n" +
                "<<<<<<<<<<<<<DATOS DE LA VACUNACION DEL CIUDADANO>>>>>>>>>>>\n" +
                $"Fecha y Hora de la Vacunacion: {datetime} \n" +
                $"Lugar Vacunacion: {place} "));

            document.Close();
        }
    }
}
