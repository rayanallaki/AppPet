using System.Collections.ObjectModel;

namespace AppPet.Views;

public partial class FrmAgendamento : ContentPage
{
	private const int DiasPermitidosParaAgendamento = 7;
	private int proximoIdAgendamento = 1;

	public ObservableCollection<Agendamento> Agendamentos { get; } = new();

	public FrmAgendamento()
	{
		InitializeComponent();

		ConfigurarDataDoAgendamento();

		BindingContext = this;
	}

	private async void ButtonAdicionarAgendamento(object? sender, EventArgs e)
	{
		var hoje = DateTime.Today;
		var dataLimite = hoje.AddDays(DiasPermitidosParaAgendamento);
		var dataSelecionada = (datePickerAgendamento.Date ?? hoje).Date;
		var horario = timePickerAgendamento.Time ?? TimeSpan.Zero;

		if (dataSelecionada < hoje || dataSelecionada > dataLimite)
		{
			await DisplayAlertAsync(
				"Atenção",
				$"O agendamento só pode ser marcado de {hoje:dd/MM/yyyy} até {dataLimite:dd/MM/yyyy}.",
				"OK");

			datePickerAgendamento.Date = hoje;
			return;
		}

		var dataHora = dataSelecionada.Add(horario);

		Agendamentos.Add(new Agendamento
		{
			Id = proximoIdAgendamento,
			DataHora = dataHora
		});

		proximoIdAgendamento++;
		ConfigurarDataDoAgendamento();
	}

	private async void ButtonCadastrarPet(object? sender, EventArgs e)
	{
		var agendamentoSelecionado = collectionViewAgendamento.SelectedItem as Agendamento;

		if (agendamentoSelecionado == null)
		{
			await DisplayAlertAsync(
				"Atenção",
				"Por favor, selecione um agendamento para cadastrar seu pet",
				"OK");

			return;
		}
		await Navigation.PushAsync(new FrmCadPet(agendamentoSelecionado));

	}

	private void ConfigurarDataDoAgendamento()
	{
		var hoje = DateTime.Today;

		datePickerAgendamento.MinimumDate = hoje;
		datePickerAgendamento.MaximumDate = hoje.AddDays(DiasPermitidosParaAgendamento);
		datePickerAgendamento.Date = hoje;
		timePickerAgendamento.Time = DateTime.Now.TimeOfDay;
	}
}
