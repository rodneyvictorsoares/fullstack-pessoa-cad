using apppessoa.Models;
using apppessoa.Services;

namespace apppessoa
{
    public partial class MainPage : ContentPage
    {
        private readonly ApiPessoaService _apiPessoaService;

        public MainPage(ApiPessoaService apiPessoaService)
        {
            InitializeComponent();
            _apiPessoaService = apiPessoaService;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadPessoasAsync();
        }

        private async Task LoadPessoasAsync()
        {
            try
            {
                var pessoas = await _apiPessoaService.GetPessoasAsync();
                PessoasCollectionView.ItemsSource = pessoas;
            }
            catch (Exception ex)
            {

                await DisplayAlert("Erro", $"Não foi possivel carregar a lista de pessoas: {ex.Message}", "OK");
            }
        }

        private async void OnAddPessoaClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PessoaDetailPage(_apiPessoaService));
        }

        private async void OnPessoaSelected(object sender, SelectionChangedEventArgs e)
        {
            var pessoa = e.CurrentSelection.FirstOrDefault() as Pessoa;
            if (pessoa != null)
            {
                await Navigation.PushAsync(new PessoaDetailPage(_apiPessoaService, pessoa));
            }

            ((CollectionView)sender).SelectedItem = null;
        }
    }

}
