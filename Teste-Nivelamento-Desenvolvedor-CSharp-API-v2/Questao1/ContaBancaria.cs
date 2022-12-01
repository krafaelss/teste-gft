using System.Globalization;

namespace Questao1
{
    class ContaBancaria {

        public int NumeroConta { get; }
        public string NomeTitular { get; set; }
        public double Saldo { get; private set; }


        public ContaBancaria(int numero, string nome, double deposito = 0)
        {
            NumeroConta = numero;
            NomeTitular = nome;
            Saldo = deposito;
        }

        public void Saque(double quantia)
        {
            Saldo -= quantia + 3.50;
        }

        public void Deposito(double quantia)
        {
            Saldo += quantia;
        }

        public override string ToString()
        {
            return $"Conta {NumeroConta}, Titular: {NomeTitular}, Saldo: $ {Saldo:F}";
        }
    }
}
