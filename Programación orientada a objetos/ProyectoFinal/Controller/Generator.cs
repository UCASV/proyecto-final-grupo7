using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFinal.Controller
{
    public static class Generator
    {
        //Generador de PDF de los datos del ciudadano y su cita
        public static void GeneratorPDF(string name, string dui, string direction, string phone, string Email, DateTime datetime, string place)
        {
            SaveFileDialog svg = new SaveFileDialog();
            svg.ShowDialog();

            using (FileStream stream = new FileStream(svg.FileName + ".pdf", FileMode.Create))//guardando el nombre del pdf
            {
                string detalle = "<<<<<<<<<<<<<<<<<<<<<<<<DATOS DEL CIUDADANO>>>>>>>>>>>>>>>>>>>>>>>>>>\n" + //Contenido del PDF
                $"Nombre del ciudadano: {name}\n" +
                $"DUI : {dui} \n" +
                $"Direccion: {direction} \n" +
                $"Telefono: {phone}\n " +
                $"Correo Electronico: {Email} \n" +
                "<<<<<<<<<<<<<DATOS DE LA VACUNACION DEL CIUDADANO>>>>>>>>>>>\n" +
                $"Fecha y Hora de la Vacunacion: {datetime} \n" +
                $"Lugar Vacunacion: {place} ";
                Paragraph text = new Paragraph(detalle);

                Document pdfDoc = new Document(PageSize.LETTER, 10f, 10f, 10f, 0f);
                PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                pdfDoc.Add(text);
                pdfDoc.Close();
                stream.Close();
            }
        }
    }
}
