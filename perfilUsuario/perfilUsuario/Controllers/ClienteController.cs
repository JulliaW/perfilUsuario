using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using perfilUsuario.Models;

namespace perfilUsuario.Controllers
{
    public class ClienteController : Controller
    {
        private readonly  perfilUsuario.Data.Context contexto;

        public ClienteController(perfilUsuario.Data.Context context)
        {
            contexto = context;
        }

        public async Task<ActionResult> Index()
        {
            var allClientes = await contexto.Clientes.ToListAsync();
            return View(allClientes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cliente cliente, IList<IFormFile> img)
        {
            IFormFile uploadedImage = img.FirstOrDefault();
            MemoryStream ms = new MemoryStream();
            if (img.Count > 0)
            {
                uploadedImage.OpenReadStream().CopyTo(ms);
                cliente.Foto = ms.ToArray();
            }

            contexto.Clientes.Add(cliente);
            contexto.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var cliente = await contexto.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return BadRequest();
            }
            return View(cliente);
        }

        [HttpGet]
        public async Task<IActionResult>Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var cliente = await contexto.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return BadRequest();
            }
            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Cliente cliente, IList<IFormFile> img)
        {
            if (id == null)
            {
                return NotFound();
            }
            var dadosAntigos = contexto.Clientes.AsNoTracking().FirstOrDefault(p => p.Id==id);

            IFormFile uploadedImage = img.FirstOrDefault();
            MemoryStream ms = new MemoryStream();
            if (img.Count > 0)
            {
                uploadedImage.OpenReadStream().CopyTo(ms);
                cliente.Foto = ms.ToArray();
            } else
            {
                cliente.Foto = dadosAntigos.Foto;
            }
            if (ModelState.IsValid)
            {
                contexto.Update(cliente);
                await contexto.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }


    }
}
