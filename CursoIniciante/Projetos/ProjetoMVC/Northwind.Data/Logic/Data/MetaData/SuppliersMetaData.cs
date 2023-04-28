using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Data.Logic.Data.MetaData
{
    public partial class SuppliersMetaData

    {
        [Key]
        public int SupplierID { get; set; }

        [DisplayName("Nome da Empresa")]
        [Required(ErrorMessage = "Nome da Empresa é Obrigatório")]
        [MaxLength(40, ErrorMessage = "Nome da Empresa não pode ser maior que 40 caracteres")]
        public string CompanyName { get; set; }

        [DisplayName("Contato")]
        [Required(ErrorMessage = "Contato é Obrigatório")]
        [MaxLength(30, ErrorMessage = "Contato não pode ser maior que 30 caracteres")]
        public string ContactName { get; set; }

        [DisplayName("Título do Contato")]
        [Required(ErrorMessage = "Título do Contato é Obrigatório")]
        [MaxLength(30, ErrorMessage = "Título do Contato não pode ser maior que 30 caracteres")]
        public string ContactTitle { get; set; }

        [DisplayName("Endereço")]
        [Required(ErrorMessage = "Endereço é Obrigatório")]
        [MaxLength(60, ErrorMessage = "Endereço não pode ser maior que 60 caracteres")]
        public string Address { get; set; }

        [DisplayName("Cidade")]
        [Required(ErrorMessage = "Cidade é Obrigatória")]
        [MaxLength(15, ErrorMessage = "Cidade não pode ser maior que 15 caracteres")]
        public string City { get; set; }

        [DisplayName("Região")]
        [Required(ErrorMessage = "Região é Obrigatória")]
        [MaxLength(15, ErrorMessage = "Região não pode ser maior que 15 caracteres")]
        public string Region { get; set; }

        [DisplayName("CEP")]
        [Required(ErrorMessage = "CEP é Obrigatório")]
        [MaxLength(10, ErrorMessage = "CEP não pode ser maior que 10 caracteres")]
        public string PostalCode { get; set; }

        [DisplayName("País")]
        [Required(ErrorMessage = "País é Obrigatório")]
        [MaxLength(15, ErrorMessage = "País não pode ser maior que 15 caracteres")]
        public string Country { get; set; }

        [DisplayName("Telefone")]
        [Required(ErrorMessage = "Telefone é Obrigatório")]
        [MaxLength(24, ErrorMessage = "Telefone não pode ser maior que 24 caracteres")]
        public string Phone { get; set; }

        [DisplayName("Fax")]
        [Required(ErrorMessage = "Fax é Obrigatório")]
        [MaxLength(24, ErrorMessage = "Fax não pode ser maior que 24 caracteres")]
        public string Fax { get; set; }

        [DisplayName("Página Inicial")]
        [MaxLength(40, ErrorMessage = "Página Inicial não pode ser maior que 40 caracteres")]
        public string HomePage { get; set; }
    }
}
