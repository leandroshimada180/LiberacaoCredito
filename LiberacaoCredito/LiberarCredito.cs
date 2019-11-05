using System;
using System.Collections.Generic;

namespace LiberacaoCredito
{
    public class LiberarCredito
    {
        public void Liberar()
        {
            List<DadosCreditoDTO> dados = new List<DadosCreditoDTO>();
            dados = ObterDados(dados);
            dados = Validacoes(dados);
            Retorno(dados);

            Console.Write("Digite: \n" +
                          "1 - Sair \n" +
                          "2 - Simular \n");

            if (Convert.ToInt32(Console.ReadLine()) == 1)
                Environment.Exit(0);
            else
            {
                Console.Clear();
                Program.Main(null);
            }

        }

        private DadosCreditoDTO CalcularTaxaJuros(DadosCreditoDTO item)
        {
            switch (item.TipoCredito)
            {
                case (int)TipoCredito.CREDITO_DIRETO:
                    item.TaxaJuros = 2;
                    break;
                case (int)TipoCredito.CREDITO_CONSIGNADO:
                    item.TaxaJuros = 1;
                    break;
                case (int)TipoCredito.CREDITO_PESSOA_JURIDICA:
                    item.TaxaJuros = 5;
                    break;
                case (int)TipoCredito.CREDITO_PESSOA_FISICA:
                    item.TaxaJuros = 3;
                    break;
                case (int)TipoCredito.CREDITO_IMOBILIARIO:
                    item.TaxaJuros = 9;
                    break;
            }

            item.ValorJuros = item.ValorCredito * item.TaxaJuros / 100;
            item.ValorTotal = item.ValorCredito + item.ValorJuros;

            return item;
        }

        private List<DadosCreditoDTO> Validacoes(List<DadosCreditoDTO> param)
        {
            string msg = string.Empty;

            try
            {
                foreach (var item in param)
                {     
                    if(item.ValorCredito == 0)
                        msg = "\nValor não pode ser zero.\n";

                    if (item.ValorCredito > 1000000)
                        msg = "\nValor superior ao máximo permitido \n";

                    if (item.QtdParcelas > 72 || item.QtdParcelas < 5)
                        msg += "\nQuantidade de parcelas inválida \n";

                    if (item.TipoCredito == (byte)TipoCredito.CREDITO_PESSOA_JURIDICA)
                    {
                        if (item.ValorCredito < 15000)
                            msg += "\nValor abaixo do minimo permitido \n";
                    }

                    if(item.TipoCredito == 0)
                        msg += "\nTipo de Crédito inválido \n";

                    if (item.DataVencimento < DateTime.Now.AddDays(15) || item.DataVencimento > DateTime.Now.AddDays(40))
                        msg += "\nData de Vencimento inválido \n ";

                    if (string.IsNullOrEmpty(msg))
                        item.Status = "Aprovado.";
                    else
                    {
                        item.Status = "Reprovado.";
                        Console.Write(msg);
                    }
                }
                return param;

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return param;
            }
        }

        private List<DadosCreditoDTO> ObterDados(List<DadosCreditoDTO> param)
        {
            DadosCreditoDTO dados = new DadosCreditoDTO();
            decimal valorCredito = 0;
            int tipoCredito = 0;
            int qtdParcelas = 0;
            DateTime dataVenc = DateTime.MinValue;

            Console.Write("Valor do Crédito: ");            

            dados.ValorCredito = (decimal.TryParse(Console.ReadLine(), out valorCredito) ? valorCredito : 0);

            Console.Write("Informe o Tipo de Crédito \n" +
                          "1 - Crédito Direto \n" +
                          "2 - Crédito Consignado \n" +
                          "3 - Crédito Pessoa Jurídica \n" +
                          "4 - Crédito Pessoa Física \n" +
                          "5 - Crédito Imobiliário \n" +
                          "\nTipo: ");

            dados.TipoCredito = (int.TryParse(Console.ReadLine(), out tipoCredito) ? tipoCredito : 0);

            Console.Write("Quantidade de parcelas: ");
            dados.QtdParcelas = (int.TryParse(Console.ReadLine(), out qtdParcelas) ? qtdParcelas : 0);

            Console.Write("Data do primeiro Vencimento: ");
            dados.DataVencimento = (DateTime.TryParse(Console.ReadLine(), out dataVenc) ? dataVenc : DateTime.MinValue);

            param.Add(dados);

            return param;
        }

        private void Retorno(List<DadosCreditoDTO> param)
        {
            DadosCreditoDTO dados = new DadosCreditoDTO();
            foreach (var item in param)
            {
                dados = CalcularTaxaJuros(item);

                Console.Write("\n\nSua Liberação de crédito está " + dados.Status + "\n\n");
                if (dados.Status.Equals("Aprovado."))
                {                 
                    Console.Write("Valor Total de R$ " + dados.ValorTotal + "\n\n");
                    Console.Write("Valor de Juros R$ " + dados.ValorJuros + "\n\n");
                }                
            }
        }        
    }
}
