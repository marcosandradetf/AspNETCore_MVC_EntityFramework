using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InfnetMVC.Models
{
    [Table("Departamento")]
    public class Departamento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Local { get; set; }
        public ICollection<Funcionario> Funcionarios { get; } = new List<Funcionario>();

        public Departamento()
        {
        }

        public void AddFuncionario(Funcionario funcionario)
        {
            Funcionarios.Add(funcionario);
        }
    }
}
