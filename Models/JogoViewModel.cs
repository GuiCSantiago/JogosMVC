using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCJogos.Models
{
    public class JogoViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Descricao { get; set; }
        public double? Valor { get; set; }
        public DateTime DataAquisicao { get; set; }
        public int CategoriaID { get; set; }
    }
}
