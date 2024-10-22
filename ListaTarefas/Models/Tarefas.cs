using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ListaTarefas.Models
{
	public class Tarefas
	{
		[Key]
		public string ID { get; set; } = Guid.NewGuid().ToString(); 

		[Required(ErrorMessage = "Preencha a descrição!")]
		public string Descricao { get; set; }

		[Required(ErrorMessage = "Preencha a data de vencimento!")]
		public DateTime? DataDeVencimento { get; set; }

		[Required(ErrorMessage = "Selecione uma categoria!")]
		public string CategoriaID { get; set; }

		[ValidateNever]
		public Categoria Categoria { get; set; }

		[Required(ErrorMessage = "Selecione um status!")]
		public string StatusID { get; set; }

		[ValidateNever]
		public Status Status { get; set; }

		public bool Atrasado => StatusID == "aberto" && DataDeVencimento < DateTime.Today;
	}

}

