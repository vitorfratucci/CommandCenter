using Microsoft.AspNetCore.Mvc;
using CommandCenter.Models;
using CommandCenter.Repository;
using System;
using System.Threading.Tasks;

namespace CommandCenter.Controllers
{
    public class InformativosController : Controller
    {
        private readonly IInformativosRepository _informativosRepository;

        public InformativosController(IInformativosRepository informativosRepository)
        {
            _informativosRepository = informativosRepository;
        }

        // Exibe todos os informativos no painel
        public async Task<IActionResult> Index()
        {
            var informativos = await _informativosRepository.GetAllAsync();
            return View(informativos);
        }

        // Exibe o formulário de criação via modal
        public IActionResult Create()
        {
            return PartialView();
        }

        // Processa a criação do informativo
        [HttpPost]
        public async Task<IActionResult> Create(InformativosModel informativo)
        {
            if (ModelState.IsValid)
            {
                await _informativosRepository.CreateAsync(informativo);
                return RedirectToAction("Index");
            }

            return NotFound();
        }


        // Exibe o formulário de edição
        public async Task<IActionResult> Edit(Guid id)
        {
            var informativo = await _informativosRepository.GetByIdAsync(id);
            if (informativo == null)
                return NotFound();

            return PartialView(informativo);
        }

        // Atualiza os dados de um informativo
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, InformativosModel informativo)
        {
            if (ModelState.IsValid)
            {
                var atualizado = await _informativosRepository.UpdateAsync(id, informativo);
                if (atualizado)
                    return RedirectToAction("Index");

                return Json(new { sucesso = false, mensagem = "Erro ao atualizar o informativo." });
            }

            return Json(new { sucesso = false, mensagem = "Dados inválidos." });
        }

        // Exibe a confirmação de exclusão (modal)
        public async Task<IActionResult> Delete(Guid id)
        {
            var informativo = await _informativosRepository.GetByIdAsync(id);
            if (informativo == null)
                return NotFound();

            return PartialView(informativo);
        }

        // Confirma exclusão
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var deletado = await _informativosRepository.DeleteAsync(id);
            if (deletado)
                return Json(new { sucesso = true });

            return Json(new { sucesso = false, mensagem = "Erro ao excluir o informativo." });
        }
    }
}
