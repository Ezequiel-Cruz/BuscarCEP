using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BuscarCep
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private async void BtnBuscarCep_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCep.Text))
                    throw new InvalidOperationException("Informe o CEP");

                using (var client = new HttpClient())
                {
                    using (var response = await client.GetAsync($"http://viacep.com.br/ws/{txtCep.Text}/json/"))
                    {
                        if (!response.IsSuccessStatusCode)
                            throw new InvalidOperationException("Algo aconteceu com a pesquisa de CEP");

                        var result = await response.Content.ReadAsStringAsync();

                        if (!string.IsNullOrWhiteSpace(result))
                            await DisplayAlert("Cep Encontrado", result, "OK");
                    }
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Ah não!", ex.Message, "OK");
            }

        }
    }
}
