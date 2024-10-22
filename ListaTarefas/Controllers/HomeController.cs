using ListaTarefas.Data;
using ListaTarefas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Diagnostics;

namespace ListaTarefas.Controllers
{
    public class HomeController : Controller
    {


        private readonly AppDbContext _context;
        public HomeController(AppDbContext context) 
        {
            _context = context;
        }

        public IActionResult Index(string id)
        {

            var filtros = new Filtros(id);

            ViewBag.Filtros = filtros;
            ViewBag.Categorias = _context.Categorias.ToList();
            ViewBag.Status = _context.Statuses.ToList();
            ViewBag.VencimentoValores = Filtros.VencimentoValoresFiltro;

            IQueryable<Tarefas> consulta = _context.Tarefas.Include(c => c.Categoria).Include(s => s.Status);


            if (filtros.TemCategoria)
            {
                consulta = consulta.Where(t => t.CategoriaID == filtros.CategoriaID);
            }


            if (filtros.TemStatus)
            {
                consulta = consulta.Where(t => t.StatusID == filtros.StatusID);
            }


            if (filtros.TemVencimento)
            {
                var hoje = DateTime.Today;

                if (filtros.Epassado)
                {
                    consulta = consulta.Where(t => t.DataDeVencimento < hoje);
                }


                if (filtros.EFuturo)
                {
                    consulta = consulta.Where(t => t.DataDeVencimento > hoje);
                }



                if (filtros.EHoje)
                {
                    consulta = consulta.Where(t => t.DataDeVencimento == hoje);
                }

            }

            var tarefas = consulta.OrderBy(t => t.DataDeVencimento).ToList();

                return View(tarefas);
        }


        public IActionResult Adicionar()
        {
			ViewBag.Categorias = _context.Categorias.ToList();
			ViewBag.Status = _context.Statuses.ToList();

            var tarefa = new Tarefas { StatusID = "aberto"};

            return View(tarefa);
		}



		[HttpPost]

        public IActionResult Filtrar(string[] filtro)
        {

            string id = string.Join('-', filtro);
            return RedirectToAction("Index", new { ID = id });

        }

			[HttpPost]

			public IActionResult MarcarCompleto([FromRoute] string id, Tarefas tarefaSelecionada)
            {
                tarefaSelecionada = _context.Tarefas.Find(tarefaSelecionada.ID);

                if (tarefaSelecionada != null)
                {
                    tarefaSelecionada.StatusID = "completo";
                    _context.SaveChanges();
                }

			return RedirectToAction("Index", new { ID = id });

		}



		[HttpPost]
		public IActionResult Adicionar(Tarefas tarefa)
		{
			Console.WriteLine($"Descricao: {tarefa.Descricao}, DataDeVencimento: {tarefa.DataDeVencimento}, CategoriaID: {tarefa.CategoriaID}, StatusID: {tarefa.StatusID}");

			if (ModelState.IsValid)
			{
				_context.Tarefas.Add(tarefa);
				_context.SaveChanges();

				return RedirectToAction("Index");
			}
			else
			{
				ViewBag.Categorias = _context.Categorias.ToList();
				ViewBag.Status = _context.Statuses.ToList();

				return View(tarefa);
			}
		}



		[HttpPost]

		public IActionResult DeletarCompletos(string id)
        {
            var paraDeletar = _context.Tarefas.Where(s => s.StatusID == "completo").ToList();

           foreach(var tarefa in paraDeletar)
            {
                _context.Tarefas.Remove(tarefa);
            }

            _context.SaveChanges();

            return RedirectToAction("Index", new { ID = id });
        }


		}

    }
