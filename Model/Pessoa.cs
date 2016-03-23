using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Model
{
    public class Pessoa
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        
        [Display(Name = "Código:")]
        public int Codigo { get; set; }

        [Required(ErrorMessage = ("Informe o Nome."))]
        [Display(Name = "Nome:")]
        public String Nome { get; set; }

        [Required(ErrorMessage = ("Informe o Endereço."))]
        [Display(Name = "Endereço:")]
        public String Endereco { get; set; }

        [Display(Name = "Complemento:")]
        public String Complemento { get; set; }

        [Required(ErrorMessage = ("Informe o Estado."))]
        [Display(Name = "Estado:")]
        public int codigoEstado { get; set; }

        [Required(ErrorMessage = ("Informe a Cidade."))]
        [Display(Name = "Cidade:")]
        public int codigoCidade { get; set; }

        [Required(ErrorMessage = ("Informe o Bairro."))]
        [Display(Name = "Bairro:")]
        public String Bairro { get; set; }

        [Display(Name = "Cep:")]
        public String Cep { get; set; }

        [Display(Name = "Celular:")]
        public String Celular { get; set; }

        [Display(Name = "Telefone:")]
        public String Telefone { get; set; }

        [Display(Name = "E-mail:")]
        public String Email { get; set; }

        [Required(ErrorMessage = ("Informe o CNPJ/ CPF."))]
        [Display(Name = "CNPJ/ CPF:")]
        public String cnpjCPF { get; set; }

        [Required(ErrorMessage = ("Informe o RG/ IE."))]
        [Display(Name = "RG/ IE:")]
        public String rgIE { get; set; }

        [Required(ErrorMessage = ("Informe o Tipo."))]
        [Display(Name = "Tipo:")]
        public String Tipo { get; set; }
    }
}
