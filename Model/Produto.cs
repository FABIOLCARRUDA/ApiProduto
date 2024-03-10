using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Produto
    {
        public int ID { get; set; }
        public string? Codigo { get; set; }
        public string? Descricao { get; set; }
        public string? Departamento { get; set; }
        public decimal Preco { get; set; }
        public bool Status { get; set; }
    }
}
