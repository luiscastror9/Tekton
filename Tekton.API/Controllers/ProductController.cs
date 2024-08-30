using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tekton.Application.Base;
using Tekton.Application.Interfaces;
using Tekton.Domain.Entity;

namespace Tekton_Labs.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductController : ControllerBase
    {
        private readonly IBaseRepository<Product> _baseRepository;
        private readonly IProductRepository _productRepository;

        public ProductController(IBaseRepository<Product> baseRepository, IProductRepository productRepository)
        {
            _baseRepository = baseRepository;
            _productRepository = productRepository;
        }
        /// <summary>
        /// Obtiene los datos del filtros de busqueda .
        /// </summary>
        /// <returns>Lista de datos del dashboard.</returns>
        /// <response code="200">Datos  obtenidos correctamente.</response>
        /// <response code="404">El archivo de datos no fue encontrado o está vacío.</response>
        /// <response code="500">Ocurrió un problema interno.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Index()
        {
            try
            {
               var getAll= _productRepository.GetAll();
                return  Ok(getAll);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un problema: ");

            }


        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Details(int id)
        {
            try
            {
                var get = _baseRepository.Filter(x=>x.ProductId==id);
                return Ok(get);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un problema: ");

            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return Ok();
            }
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return Ok();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return Ok();
            }
        }
    }
}
