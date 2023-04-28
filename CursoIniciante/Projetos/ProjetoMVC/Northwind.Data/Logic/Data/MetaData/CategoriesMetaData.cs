using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Data.Logic.Data.MetaData
{
    public partial class CategoriesMetaData
    {
        [Key]
        public int CategoryID { get; set; }

        [DisplayName("Nome da Categoria")]
        [Required(ErrorMessage = "Nome da Categoria é Obrigatório")]
        [MaxLength(15, ErrorMessage = "Nome da Categoria não pode ser maior que 15 caracteres")]
        public string CategoryName { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "Descrição é Obrigatória")]
        [MaxLength(300, ErrorMessage = "Descrição não pode ser maior que 300 caracteres")]
        public string Description { get; set; }
    }
}
