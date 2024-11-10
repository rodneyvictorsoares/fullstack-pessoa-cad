using apppessoa.Models;
using apppessoa.Services;
using System.Collections.ObjectModel;

namespace apppessoa;

public partial class PessoaDetailPage : ContentPage
{
	private readonly ApiPessoaService _pessoaService;
	private Pessoa _pessoa;

	public PessoaDetailPage(ApiPessoaService pessoaService)
	{
		InitializeComponent();
		_pessoaService = pessoaService;
		_pessoa = new Pessoa
		{
			EnderecoObj = new Endereco(),
			Telefones = new List<Telefone> { new Telefone() }
		};
		BindingContext = _pessoa;
	}

    public PessoaDetailPage(ApiPessoaService pessoaService, Pessoa pessoa)
    {
		InitializeComponent();
		_pessoaService = pessoaService;
		_pessoa = pessoa;
		BindingContext = _pessoa;
    }

	private async void OnSaveClicked(object sender, EventArgs e)
	{
		try
		{
			if (_pessoa.Id == 0)
			{
				await _pessoaService.CreateAsync(_pessoa);
			}
			else
			{
				await _pessoaService.UpdateAsync(_pessoa);
			}
			await Navigation.PopAsync();
		}
		catch (Exception ex)
		{

            await DisplayAlert("Erro", $"Não foi possivel salvar os dados da pessoa: {ex.Message}", "OK");
        }
	}

	private async void OnDeleteClicked(object sender, EventArgs e)
	{
		if (_pessoa.Id == 0)
		{
			await DisplayAlert("Aviso", "Não é possivel excluir uma pessoa ainda não cadastrada.","OK");
			return;
		}

		bool confirm = await DisplayAlert("Confirmar", "Deseja realmente excluir a pessoa?", "Sim", "Não");
		if (confirm)
		{
			try
			{
				await _pessoaService.DeleteAsync(_pessoa.Id);
				await Navigation.PopAsync();
			}
			catch (Exception ex)
			{

                await DisplayAlert("Erro", $"Não foi possivel excluir a pessoa: {ex.Message}", "OK");
            }
		}
	}

	private void OnAddTelefoneClicked(object sender, EventArgs e)
	{
		_pessoa.Telefones.Add(new Telefone());
	}
}