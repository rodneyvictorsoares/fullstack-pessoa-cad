using Microsoft.AspNetCore.Mvc;
using webpessoa.Models;
using webpessoa.Services;

namespace webpessoa.Controllers
{
    public class PessoaController : Controller
    {
        private readonly apiPessoaService _apiPessoaService;

        public PessoaController(apiPessoaService apiPessoaService)
        {
            _apiPessoaService = apiPessoaService;
        }

        //GET: Pessoa
        public async Task<IActionResult> Index()
        {
            var pessoas = await _apiPessoaService.GetPessoasAsync();
            return View(pessoas);
        }

        //GET: Pessoa/Details/2
        public async Task<IActionResult> Details(int id)
        {
            var pessoa = await _apiPessoaService.GetPessoaAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }
            return View(pessoa);
        }
        
        // GET: Pessoa/Create
        public IActionResult Create()
        {
            var pessoa = new Pessoa
            {
                EnderecoObj = new Endereco(),
                Telefones = new List<Telefone> { new Telefone() }
            };
            return View(pessoa);
        }

        // POST: Pessoa/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateConfirmed(Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                await _apiPessoaService.CreateAsync(pessoa);
                return RedirectToAction(nameof(Index));
            }
            return View(pessoa);
        }

        // GET: Pessoa/Edit/2
        public async Task<IActionResult> Edit(int id)
        {
            var pessoa = await _apiPessoaService.GetPessoaAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }
            return View(pessoa);
        }

        // POST: Pessoa/Edit/2
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Pessoa pessoa)
        {
            if (id != pessoa.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _apiPessoaService.UpdateAsync(pessoa);
                return RedirectToAction(nameof(Index));
            }
            return View(pessoa);
        }

        // GET: Pessoa/Delete/2
        public async Task<IActionResult> Delete(int id)
        {
            var pessoa = await _apiPessoaService.GetPessoaAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }
            return View(pessoa);
        }

        // POST: Pessoa/Delete/2
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _apiPessoaService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
