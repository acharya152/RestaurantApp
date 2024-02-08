using ClassLibraryForRestro.Data;
using ClassLibraryForRestro.Infrastructure;
using ClassLibraryForRestro.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Runtime.Intrinsics.X86;
using System.Xml.Linq;

namespace RestroApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailsRestroController : ControllerBase
    {
        private readonly IRestro _restro;
        private readonly IComments _ucomment;
        private readonly IRatings _rate;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ApplicationDbContext2 _context;
        public DetailsRestroController(IRestro restro, IWebHostEnvironment hostEnvironment, IComments ucomment, IRatings rate, ApplicationDbContext2 context)
        {
            _restro = restro;
            _hostEnvironment = hostEnvironment;
            _ucomment = ucomment;
            _rate = rate;
            _context = context;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetIndex()
        {
            var restro = _restro.GetAll();
            return Ok(restro);
        }
        [HttpGet("Details")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var restro = _restro.GetByID(id);
            return Ok(restro);
        }
        [HttpPost("Create")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] DetailsRestro detailsRestro, IFormFile photoFile)
        {
            if (ModelState.IsValid)
            {
                if (photoFile != null && photoFile.Length > 0)
                {
                    // Generate a unique file name based on GUID
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(photoFile.FileName);

                    // Set the path where you want to save the photo
                    string path = Path.Combine("Images", fileName);

                    // Save the file to the server
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await photoFile.CopyToAsync(fileStream);
                    }

                    detailsRestro.Photo = fileName;
                }

                _restro.Insert(detailsRestro);
                _restro.save();

                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("Delete")]

        public async Task<IActionResult> PostDelete(int id)
        {
            var detailsRestro = _restro.GetByID(id);

            //var detailsRestro = await _context.DetailsRestroo.FindAsync(id);
            if (detailsRestro != null)
            {
                _ucomment.DelById(id);
                _ucomment.save();
                _rate.Delete(id);
                _rate.save();
                _restro.Delete(detailsRestro);
                _restro.save();
            }
            return Ok();
        }
        [HttpPut("Edit")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> PutEdit(int id, [FromForm] DetailsRestro detailsRestro, IFormFile? photoFile)
        {
            var existingrestro = _restro.GetByID(id);
            if (existingrestro == null)
            {
                // Handle the case where the specified ID does not exist
                return NotFound($"Restaurant with ID {id} not found.");
            }
           
            else
            {
                if (ModelState.IsValid)
                {

                    if (photoFile != null && photoFile.Length > 0)
                    {
                        // Generate a unique file name based on GUID
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(photoFile.FileName);

                        // Set the path where you want to save the photo
                        string path = Path.Combine("Images", fileName);

                        // Save the file to the server
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await photoFile.CopyToAsync(fileStream);
                        }

                        detailsRestro.Photo = fileName;
                    }
                    else
                    {
                        // If no new photo is provided, retain the existing photo
                        detailsRestro.Photo = existingrestro.Photo;
                    }
                    _restro.Update(detailsRestro);
                    return Ok();
                }
                return BadRequest(ModelState);

            }
        }





    }
    }


    
