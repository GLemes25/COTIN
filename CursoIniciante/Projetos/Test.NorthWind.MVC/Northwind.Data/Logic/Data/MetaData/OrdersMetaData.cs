using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Data.Logic.Data.MetaData
{
    public partial class OrdersMetaData
    {

        [DisplayName("ID do Pedido")]
        [Required(ErrorMessage = "O ID do Pedido é obrigatório.")]
        public int OrderID { get; set; }

        [DisplayName("ID do Cliente")]
        [Required(ErrorMessage = "O ID do Cliente é obrigatório.")]
        public string CustomerID { get; set; }

        [DisplayName("ID do Funcionário")]
        public Nullable<int> EmployeeID { get; set; }

        [DisplayName("Data do Pedido")]
        public Nullable<System.DateTime> OrderDate { get; set; }

        [DisplayName("Data de Entrega")]
        public Nullable<System.DateTime> RequiredDate { get; set; }

        [DisplayName("Data de Envio")]
        public Nullable<System.DateTime> ShippedDate { get; set; }

        [DisplayName("Transportadora")]
        public Nullable<int> ShipVia { get; set; }

        [DisplayName("Frete")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public Nullable<decimal> Freight { get; set; }

        [DisplayName("Nome para Envio")]
        [Required(ErrorMessage = "O Nome para Envio é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O Nome para Envio não pode ser maior que 50 caracteres.")]
        public string ShipName { get; set; }

        [DisplayName("Endereço para Envio")]
        [Required(ErrorMessage = "O Endereço para Envio é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O Endereço para Envio não pode ser maior que 100 caracteres.")]
        public string ShipAddress { get; set; }

        [DisplayName("Cidade para Envio")]
        [Required(ErrorMessage = "A Cidade para Envio é obrigatória.")]
        [MaxLength(50, ErrorMessage = "A Cidade para Envio não pode ser maior que 50 caracteres.")]
        public string ShipCity { get; set; }

        [DisplayName("Região para Envio")]
        [MaxLength(50, ErrorMessage = "A Região para Envio não pode ser maior que 50 caracteres.")]
        public string ShipRegion { get; set; }

        [DisplayName("CEP para Envio")]
        [MaxLength(10, ErrorMessage = "O CEP para Envio não pode ser maior que 10 caracteres.")]
        public string ShipPostalCode { get; set; }

        [DisplayName("País para Envio")]
        [Required(ErrorMessage = "O País para Envio é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O País para Envio não pode ser maior que 50 caracteres.")]
        public string ShipCountry { get; set; }
    }
}