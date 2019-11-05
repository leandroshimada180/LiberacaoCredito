using System;

namespace LiberacaoCredito
{
    public class DadosCreditoDTO
    {
        public decimal ValorCredito { get; set; }
        public decimal ValorJuros { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal TaxaJuros { get; set; }
        public int TipoCredito { get; set; }
        public int QtdParcelas { get; set; }
        public DateTime DataVencimento { get; set; }
        public string Status { get; set; }
    }
}
