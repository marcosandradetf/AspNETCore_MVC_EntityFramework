using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace InfnetMVC.Models
{
    [Table("Funcionario")]
    public class Funcionario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataDeNascimento { get; set; }
        public int? DepartamentoId { get; set; } // Required foreign key property
        public Departamento? Departamento { get; set; } = null!; // Required reference navigation to principal

        public Funcionario()
        {
        }
        
    }
}
