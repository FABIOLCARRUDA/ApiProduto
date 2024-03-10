using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Model.Enum
{
    public enum Departamento
    {
        [Display(Name = "BEBIDAS")]
        Bebidas = 10,

        [Display(Name = "CONGELADOS")]
        Congelados = 20,

        [Display(Name = "LATICINIOS")]
        Laticinios = 30,

        [Display(Name = "VEGETAIS")]
        Vegetais = 40
    }

    public class DepartamentoInfo
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
    }
}
