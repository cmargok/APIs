using ADO.Net;
using DataContext;
using DataContext.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.CreateModels;
using Models.ModifyModel;
using Models.ResponseModels;
//using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CiudadController : ControllerBase
    {
        private readonly IAdoRepository _context;
        public CiudadController(IAdoRepository context)
        {
            _context = context;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCiudadById(string Id)
        {

            CiudadResponseModel ciudadResponse = await _context.GetCiudadById(Id);

            if (!ciudadResponse.Succcess)
            {
                return NotFound(ciudadResponse);
            }
            else
            {
                return Ok(ciudadResponse);
            }

        }
        
        [HttpGet]
        public async Task<IActionResult> GetCiudades()
        {

            CiudadesResponseModel ciudadesResponse = await _context.GetCiudades();


            if (!ciudadesResponse.Succcess)
            {
                return BadRequest(ciudadesResponse);
            }
            else
            {
                return Ok(ciudadesResponse);
            }

        }
        
        [HttpPost]
        public async Task<IActionResult> PostCiudad(CiudadCreateModel createCiudad)
        {
       
            CiudadResponseModel ciudadResponse = await _context.PostCiudad(new CiudadCreateModel 
                                                                            { 
                                                                                ciud_ID= createCiudad.ciud_ID.ToUpper(),
                                                                                ciud_nombre=  createCiudad.ciud_nombre.ToUpper(),
                                                                                 pais_ID = createCiudad.pais_ID!.ToUpper()

                                                                             });
            

            if(!ciudadResponse.Succcess)
            {
                return BadRequest(ciudadResponse);
            }
            else
            {
                return Created("api/localizaciones/ciudad/"+ciudadResponse.ciud_ID, ciudadResponse);
            }
            
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateCiudad(string Id, CiudadModifyModel ciudadModify)
        {

            CiudadResponseModel ciudadResponse = await _context.UpdateCiudad(Id,new CiudadModifyModel
                                                                            {
              
                                                                                ciud_nombre = ciudadModify.ciud_nombre.ToUpper(),
                                                                                pais_ID = ciudadModify.pais_ID!.ToUpper()

                                                                            });


            if (!ciudadResponse.Succcess)
            {
                return BadRequest(ciudadResponse);
            }
            else
            {
                return Ok(ciudadResponse);
            }

        }

        
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCiudad(string Id)
        {

            CiudadResponseModel ciudadResponse = await _context.DeleteCiudad(Id);

            if (!ciudadResponse.Succcess)
            {
                return NotFound(ciudadResponse);
            }
            else
            {
                return NoContent();
            }

        }
        
    }
}
