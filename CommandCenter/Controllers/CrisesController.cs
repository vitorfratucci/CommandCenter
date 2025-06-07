using Microsoft.AspNetCore.Mvc;
using CommandCenter.Models;
using CommandCenter.Repository;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CommandCenter.Controllers
{
    public class CrisesController : Controller
    {
        private readonly ICrisesRepository _crisesRepository;

        public CrisesController(ICrisesRepository crisesRepository)
        {
            _crisesRepository = crisesRepository;
        }

        // Exibir todas as crises no painel
        public async Task<IActionResult> Index()
        {
            var crises = await _crisesRepository.GetAllAsync();
            return View(crises);
        }

        // Exibe o formulário de criação (modal)
        public IActionResult Create()
        {
            return PartialView();
        }

        // Processa o envio do formulário para criar uma nova crise
        [HttpPost]
        public async Task<IActionResult> Create(CrisesModel crise)
        {
            if (ModelState.IsValid)
            {
                // Garante que ACN receba apenas um valor (ou do select ou do input manual)
                // ✅ Se `DataHoraAcionamento` não for enviado, preenche com a hora atual
                if (crise.DataHoraAcionamento == default)
                {
                    crise.DataHoraAcionamento = DateTime.Now;
                }

                await _crisesRepository.CreateAsync(crise);
                return RedirectToAction("Index");
            }

            return NotFound();
        }

        // Exibe o formulário de edição (modal)
        public async Task<IActionResult> Edit(Guid id)
        {
            var crise = await _crisesRepository.GetByIdAsync(id);
            if (crise == null)
                return NotFound();

            return PartialView(crise);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarNovaAcaoRealizada(Guid id, string novoHistorico)
        {
            await _crisesRepository.AtualizarHistoricoAsync(id, novoHistorico);
            return RedirectToAction("Index");

        }

        // ENCERRAMENTO FORMULARIO

        [HttpPost]
        public async Task<IActionResult> EncerrarCrise(Guid id, string novoHistorico)
        {
            await _crisesRepository.EncerrarCrise(id, novoHistorico);
            return RedirectToAction("Index");

        }

        // Atualiza uma crise existente
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, CrisesModel crise)
        {
            if (ModelState.IsValid)
            {
                // Garante que ACN seja atualizado corretamente
                var atualizado = await _crisesRepository.UpdateAsync(id, crise);
                if (atualizado)
                {
                    return Json(new { sucesso = true });
                }
                return Json(new { sucesso = false, mensagem = "Erro ao atualizar crise." });
            }

            return Json(new { sucesso = false, mensagem = "Dados inválidos." });
        }

        // Exibe a confirmação de exclusão (modal)
        public async Task<IActionResult> Delete(Guid id)
        {
            var crise = await _crisesRepository.GetByIdAsync(id);
            if (crise == null)
                return NotFound();

            return PartialView(crise);
        }

        // Exclui uma crise do banco de dados
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var deletado = await _crisesRepository.DeleteAsync(id);
            if (deletado)
            {
                return Json(new { sucesso = true });
            }

            return Json(new { sucesso = false, mensagem = "Erro ao excluir crise." });
        }

        // Testa a conexão com o MongoDB
        public async Task<IActionResult> TestDatabase()
        {
            bool isConnected = await _crisesRepository.TestConnectionAsync();
            return Json(new { success = isConnected });
        }
    }
}
