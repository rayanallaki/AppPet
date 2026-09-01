namespace AppPet.Models;

public class Servico
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string Descricao { get; set; } = string.Empty;

    public decimal Valor { get; set; }

    public int DuracaoMinutos { get; set; }

    public Pet? Pet { get; set; }

    public Agendamento? Agendamento { get; set; }

}