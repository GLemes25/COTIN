using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Data.Logic.Data.MetaData
{
    public partial class CustomerMetaDa
    {
        [DisplayName("Código")]
        public string CustomerID { get; set; }
        //--------------------------------------------------------------------
        [DisplayName("Nome da Empresa")]
        [Required(ErrorMessage = "*Campo obrigatório")]
        [MaxLength(40, ErrorMessage = "Nome da Empresa não pode ser maior que 40 caracteres!")]
        public string CompanyName { get; set; }
        //--------------------------------------------------------------------

        [DisplayName("Nome do Contato")]
        [Required(ErrorMessage = "*Campo obrigatório")]
        public string ContactName { get; set; }
        //--------------------------------------------------------------------

        [DisplayName("Título")]
        [Required(ErrorMessage = "*Campo obrigatório")]
        public string ContactTitle { get; set; }
        //--------------------------------------------------------------------

        [DisplayName("Cidade")]
        [Required(ErrorMessage = "*Campo obrigatório")]
        public string City { get; set; }
        //--------------------------------------------------------------------

        [DisplayName("Região")]
        [Required(ErrorMessage = "*Campo obrigatório")]
        public string Region { get; set; }
        //--------------------------------------------------------------------

        [DisplayName("Caixa Postal")]
        [Required(ErrorMessage = "*Campo obrigatório")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Apenas valor numérico")] //PARA ACEITAR APENAS Nº
        public string PostalCode { get; set; }
        //--------------------------------------------------------------------

        [DisplayName("País")]
        [Required(ErrorMessage = "*Campo obrigatório")]
        public string Country { get; set; }
        //--------------------------------------------------------------------

        [DisplayName("Telefone")]
        [Required(ErrorMessage = "*Campo obrigatório")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Apenas valor numérico")] //PARA ACEITAR APENAS Nº
        public string Phone { get; set; }
        //--------------------------------------------------------------------

        [DisplayName("Fax")]
        public string Fax { get; set; }

    }
}


