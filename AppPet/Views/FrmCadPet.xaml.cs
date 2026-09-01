using System.Collections.ObjectModel;
using AppPet.Models;

namespace AppPet.Views;

public partial class FrmCadPet : ContentPage
{
	private int proximoIdPet = 1;
	private readonly Agendamento agendamentoSelecionado;

	public ObservableCollection<Pet> CadastroPets { get; } = new();

	public FrmCadPet(Agendamento agendamentoSelecionado)
	{
		InitializeComponent();

		datePickerNascimento.MaximumDate = DateTime.Today;
		datePickerNascimento.Date = DateTime.Today;
		BindingContext = this;

		foreach (var especie in especiesERacas.Keys)
		{
			pickerEspecie.Items.Add(especie);
		}

		this.agendamentoSelecionado = agendamentoSelecionado;


	}

	private async void ButtonCadastrarPet(object? sender, EventArgs e)
	{
		var nome = txtNomePet.Text?.Trim() ?? "";
		var especie = pickerEspecie.SelectedItem?.ToString() ?? "";
		var raca = pickerRaca.SelectedItem?.ToString() ?? "";
		var peso = double.TryParse(txtPeso.Text, out var pesoValue) ? pesoValue : 0;
		var dataNascimento = datePickerNascimento.Date ?? DateTime.Today;
		var porte = pickerPorte.SelectedItem?.ToString() ?? "";

		if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(especie) ||
			string.IsNullOrWhiteSpace(raca) || peso <= 0 ||
			string.IsNullOrWhiteSpace(porte))
		{
			await DisplayAlertAsync(
				"Atenção",
				"Por favor, preencha todos os campos corretamente.",
				"OK");
			return;
		}

		var pet = new Pet
		{
			id = proximoIdPet,
			Nome = nome,
			Especie = especie,
			Raca = raca,
			Peso = peso,
			DataNascimento = dataNascimento,
			Porte = porte
		};

		CadastroPets.Add(pet);
		proximoIdPet++;

		await DisplayAlertAsync(
			"Sucesso",
			$"Pet cadastrado com sucesso! Idade: {pet.Idade} anos.",
			"OK");
	}

	private readonly Dictionary<string, List<string>> especiesERacas = new()
	{
		{ "Cachorro", new List<string> { "Labrador", "Poodle", "Bulldog" } },
		{ "Gato", new List<string> { "Siamês", "Persa", "Maine Coon" } }
	};

	private void PickerEspecie_SelectedIndexChanged(object? sender, EventArgs e)
	{
    pickerRaca.Items.Clear();

    var especieSelecionada = pickerEspecie.SelectedItem?.ToString();

    if (string.IsNullOrWhiteSpace(especieSelecionada))
    {
        pickerRaca.IsEnabled = false;
        return;
    }

    foreach (var raca in especiesERacas[especieSelecionada])
    {
        pickerRaca.Items.Add(raca);
    }

    pickerRaca.IsEnabled = true;
	}

	private async void ButtonCadastrarServico(object? sender, EventArgs e)
	{
		var petSelecionado = collectionViewCadastroPet.SelectedItem as Pet;

		if(petSelecionado == null)
		{
			await DisplayAlertAsync(
				"Atenção",
				"Por favor, selecione um pet para cadastrar o serviço.",
				"OK");
			return;
		}

		await Navigation.PushAsync(new FrmCadServico(petSelecionado, agendamentoSelecionado));
	}

	
}
