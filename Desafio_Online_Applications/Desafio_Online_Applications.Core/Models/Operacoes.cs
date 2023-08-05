using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Desafio_Online_Applications.Core.Models
{
    public class Operacoes
    {
        public string tipo;
        public string data;
        public decimal valor;
        public string cpf;
        public string cartao;
        public string donoLoja;
        public string nomeLoja;
        public Operacoes(string tipo, string data, decimal valor, string cpf, string cartao, string donoLoja, string nomeLoja)
        {
            this.tipo = tipo;
            this.data = data;
            this.valor = valor;
            this.cpf = cpf;
            this.cartao = cartao;
            this.donoLoja = donoLoja;
            this.nomeLoja = nomeLoja;
        }
    }
}
