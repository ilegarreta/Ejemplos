using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net;
using System.IO;

namespace Ejemplo_consulta_Factura_por_API
{
    public partial class Form1 : Form
    {
        //Refresh token generado según el proceso indicado en el manual de Dragonfish
        public string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c3VhcmlvIjoiQURNSU4iLCJwYXNzd29yZCI6ImQzNTc2NmY0NDdjMzZmMTAxZGZmYTU1YzkwZDlhZTVmODJmMDEwMzRmZTJiNWU2N2JmODM0NmQ2ZGU4NjRmYzMiLCJleHAiOiIxODcxODQyOTg0In0.5xJbbuxnmJnHrNC3jjn89IxiOeLHpCsPnTgw27_HLW4";
        //Nombre del servicio configurado en Dragonfish
        public string servicio = "PRUEBAREST";
        //Dirección del servidor donde está corriendo Dragonfish y puerto definido para el servicio.
        public string servidor = "http://localhost:8008/";
        //Base de datos a consultar.
        public string baseDatosDF = "DEMO";
        //Ejemplo de un posible filtro a incluir (optativo)
        public string fecha = "/?Fecha=2018-06-19";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(servidor + "api.Dragonfish/Factura/" + fecha );
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";
            httpWebRequest.Headers.Add("IdCliente", servicio);
            httpWebRequest.Headers.Add("Authorization", token);
            httpWebRequest.Headers.Add("basededatos", baseDatosDF);

            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var responseText = streamReader.ReadToEnd();
                    textBox1.Text = responseText.ToString();
                }

            }

            catch (WebException ex)
            {
                HttpWebResponse respuesta = ex.Response as HttpWebResponse;
                if (respuesta != null)
                {
                    MessageBox.Show(respuesta.StatusDescription, "ATENCION!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ex.Response.Close();
                }
                else
                {
                    throw;
                }

            }
        }
    }
}
