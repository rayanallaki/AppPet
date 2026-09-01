using System.Collections.ObjectModel;
using AppPet.Models;

namespace AppPet.Views;

public partial class FrmCadServico : ContentPage
{

	private readonly Pet petSelecionado;
	private readonly Agendamento agendamentoSelecionado;
	private readonly Dictionary<string, (string Nome, decimal Valor, int DuracaoMinutos)> servicosDisponiveis = new()
	{
		{ "Banho - R$ 60,00 - 40 minutos", ("Banho", 60m, 40) },
		{ "Tosa - R$ 80,00 - 60 minutos", ("Tosa", 80m, 60) },
		{ "Consulta veterinária - R$ 120,00 - 30 minutos", ("Consulta veterinária", 120m, 30) },
		{ "Vacinação - R$ 90,00 - 20 minutos", ("Vacinação", 90m, 20) }
	};

	public ObservableCollection<Servico> ServicosCadastrados { get; } = new();

	public FrmCadServico(Pet pet, Agendamento agendamento)
	{
		InitializeComponent();

		petSelecionado = pet;
		agendamentoSelecionado = agendamento;

		BindingContext = this;
		lblPetSelecionado.Text = $"Serviço para: {pet.Nome}";

		foreach (var servico in servicosDisponiveis.Keys)
		{
			pickerServico.Items.Add(servico);
		}
	}

	private void PickerServico_SelectedIndexChanged(object? sender, EventArgs e)
	{
		var servicoSelecionado = pickerServico.SelectedItem?.ToString();

		if (servicoSelecionado == null ||
			!servicosDisponiveis.TryGetValue(servicoSelecionado, out var dadosServico))
		{
			lblValorServico.Text = "Selecione um serviço";
			lblDuracaoServico.Text = "Selecione um serviço";
			return;
		}

		lblValorServico.Text = $"R$ {dadosServico.Valor:F2}";
		lblDuracaoServico.Text = $"{dadosServico.DuracaoMinutos} minutos";
	}

	private async void ButtonCadastrarServico(object? sender, EventArgs e)
	{
		var servicoSelecionado = pickerServico.SelectedItem?.ToString() ?? "";
		var descricao = txtDescricao.Text?.Trim() ?? "";

		if (!servicosDisponiveis.TryGetValue(servicoSelecionado, out var dadosServico) ||
			string.IsNullOrWhiteSpace(descricao))
		{
			await DisplayAlertAsync(
				"Atenção",
				"Por favor, preencha todos os campos corretamente.",
				"OK");
			return;
		}

		var servico = new Servico
		{
			Id = ServicosCadastrados.Count + 1,
			Nome = dadosServico.Nome,
			Descricao = descricao,
			Valor = dadosServico.Valor,
			DuracaoMinutos = dadosServico.DuracaoMinutos,
			Pet = petSelecionado,
			Agendamento = agendamentoSelecionado
		};

		ServicosCadastrados.Add(servico);
		txtDescricao.Text = string.Empty;

		await DisplayAlertAsync(
			"Sucesso",
			"Serviço cadastrado com sucesso!",
			"OK");
	}
}
