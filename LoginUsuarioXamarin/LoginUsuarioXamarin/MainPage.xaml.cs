using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LoginUsuarioXamarin
{
    public partial class MainPage : ContentPage
    {
        private JArray arrData;

        public MainPage()
        {
            InitializeComponent();
            // loadJSONAsync();
            //postJson();
        }

        private void postJson()
        {
            throw new NotImplementedException();
        }

        private async void loadJSONAsync()
        {
            var client = new HttpClient()
            {
                BaseAddress = new Uri("http://area51.pe/sol/")
            };
            string url = string.Format("tienda.json");
            var resp = await client.GetAsync(url);
            var result = resp.Content.ReadAsStringAsync().Result;


            JObject values = JObject.Parse(result);
            arrData = (JArray)values["tienda"];
            Debug.WriteLine(arrData);
        }
        private async void btn_enviar(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(input_email.Text))
            {
                await DisplayAlert("error", "ingrese un email","ok");
                input_email.Focus();
                return;
            }
            if (string.IsNullOrEmpty(input_password.Text))
            {
                await DisplayAlert("error", "ingrese un password","ok");
                input_email.Focus();
                return;
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://www.lans.com.mx");
                var content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("fecha", "2018-12-28"),
                 new KeyValuePair<string, string>("origen", "g"),
                 new KeyValuePair<string, string>("destino", "g"),
                 new KeyValuePair<string, string>("encomienda", "g"),
                 new KeyValuePair<string, string>("monto", "g"),
                 new KeyValuePair<string, string>("colaborador", "g"),
                 new KeyValuePair<string, string>("accion", "insertar")
            });
                var result = await client.PostAsync("marketing/admin/gastos_transportes/procesafotos.php", content);
                string resultContent = await result.Content.ReadAsStringAsync();
                Console.WriteLine(resultContent);
                await DisplayAlert("Alert", resultContent, "OK");
            }

        }

    }
}
