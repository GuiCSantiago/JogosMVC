using MVCJogos.DAO;
using MVCJogos.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCJogos.Controllers
{
    public class JogoController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                JogoDAO dao = new JogoDAO();
                var list = dao.Lista();
                return View("Listagem", list);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public IActionResult Create()
        {
            try
            {
                JogoViewModel jogo = new JogoViewModel();
                jogo.DataAquisicao = DateTime.Now;
                JogoDAO dao = new JogoDAO();
                jogo.Id = dao.ProximoId();
                return View("Form", jogo);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public IActionResult Salvar(JogoViewModel jogo)
        {
            try
            {
                JogoDAO dao = new JogoDAO();
                if (dao.Consulta(jogo.Id) == null)
                    dao.Inserir(jogo);
                else
                    dao.Alterar(jogo);
                return RedirectToAction("index");
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public IActionResult Edit(int id)
        {
            try
            {
                JogoDAO dao = new JogoDAO();
                JogoViewModel jogo = dao.Consulta(id);
                if (jogo == null)
                    return RedirectToAction("index");
                else
                    return View("Form", jogo);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                JogoDAO dao = new JogoDAO();
                dao.Excluir(id);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
            return RedirectToAction("index");
        }
    }
}