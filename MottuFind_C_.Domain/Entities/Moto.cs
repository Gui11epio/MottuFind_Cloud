using System.ComponentModel.DataAnnotations;
using System.Numerics;
using MottuFind_C_.Domain.VO;
using Sprint1_C_.Domain.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Sprint1_C_.Domain.Entities
{
    public class Moto
    {
        [Key]
        public VOPlaca Placa { get; private set; }
        public MotoModelo Modelo { get; set; }
        public string Marca { get; set; }
        public MotoStatus Status { get; set; }

        // Relacionamento com a tabela de Patio(Many to one)
        public int PatioId { get; set; }
        public Patio Patio { get; set; }
        // Relacionamento com a tabela de TagRfid(One to one)
        public TagRfid TagRfid { get; set; }

        protected Moto() { } // EF Core

        public Moto(VOPlaca placa, MotoModelo modelo, string marca, MotoStatus status, int patioId)
        {
            Placa = placa;
            Modelo = modelo;
            Marca = marca;
            Status = status;
            PatioId = patioId;
        }


    }
}
