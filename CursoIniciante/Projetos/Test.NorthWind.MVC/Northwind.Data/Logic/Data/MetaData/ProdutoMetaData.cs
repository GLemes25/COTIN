using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Data.Logic.Data.MetaData
{
    public partial class ProdutoMetaData
    {
        public int ProductID { get; set; }
        [DisplayName("Nome do Produto")]
        [Required(ErrorMessage = "Nome do Produto é Obrigatório")]
        [MaxLength(40, ErrorMessage = "Nome do Produto não pode ser maior que 40 caracteres")]
        public string ProductName { get; set; }


        [DisplayName("Fornecedor")]
        public Nullable<int> SupplierID { get; set; }

        [DisplayName("Categoria")]
        public Nullable<int> CategoryID { get; set; }

        [DisplayName("Quantidade por Unidade")]
        [Required(ErrorMessage = "Quantidade por Unidade é Obrigatório")]

        public string QuantityPerUnit { get; set; }

        [DisplayName("Preço Unitário")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        [Required(ErrorMessage = "Preço Unitário é Obrigatório")]

        public Nullable<decimal> UnitPrice { get; set; }

        [DisplayName("Unidades em Estoque")]

        [Required(ErrorMessage = "Unidades em Estoque é Obrigatório")]

        public Nullable<short> UnitsInStock { get; set; }

        [DisplayName("Unidades em Pedido")]

        [Required(ErrorMessage = "Unidades em Pedido é Obrigatório")]

        public Nullable<short> UnitsOnOrder { get; set; }

        [DisplayName("Nível de Reabastecimento")]

        [Required(ErrorMessage = "Nível de Reabastecimento é Obrigatório")]

        public Nullable<short> ReorderLevel { get; set; }

        [DisplayName("Descontinuado ")]
        public bool Discontinued { get; set; }
    }
}
