using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AppPet.Models
{
    public class Pet
    {
        public int id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Especie { get; set; } = string.Empty;

        public string Raca { get; set; } = string.Empty;

        public double Peso { get; set; }

        public DateTime DataNascimento { get; set; } = DateTime.Today;

        public int Idade
        {
            get
            {
                var hoje = DateTime.Today;
                var idade = hoje.Year - DataNascimento.Year;

                if (DataNascimento.Date > hoje.AddYears(-idade))
                {
                    idade--;
                }

                return idade;
            }
        }

        public string Porte { get; set; } = string.Empty;

        public ObservableCollection<Servico> Servicos { get; } = new();

    }
}
