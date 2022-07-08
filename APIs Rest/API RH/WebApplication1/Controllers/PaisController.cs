
using DataContext;
using DataContext.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.CreateModels;
using Models.ModifyModel;
using Models.ResponseModels;
using System.Data.SqlClient;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisController : ControllerBase
    {

        private readonly RH_Context rh_context;

        public PaisController(RH_Context _context)
        {
            rh_context = _context;
        }

        [HttpGet]
        public async Task<IActionResult> getPaises()
        {

            try
            {
                PaisesResponseModel paises = new();

                paises.Paises = rh_context.PAIS
                    .ToList()
                    .Select(pais => new PaisModel
                    {
                        Pais_ID = pais.pais_ID,
                        Pais_nombre = pais.pais_nombre

                    });

                if (paises.Paises == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "NO DATA FOUND");
                }

                return Ok(paises);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> getPais(string Id)
        {
            Id = Id.Substring(0, 3).ToUpper();

            try
            {
                PAIS? pais = rh_context.PAIS.SingleOrDefault(s => s.pais_ID == Id);


                if (pais == null)
                {
                    return NotFound();
                }
                else
                {

                    PaisResponseModel paisResponse = new PaisResponseModel
                    {
                        Pais_ID = pais.pais_ID,
                        Pais_nombre = pais.pais_nombre
                    };


                    return Ok(paisResponse);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> PostPais(PaisCreateModel paisName)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                PAIS pais = new();
                pais.pais_nombre = paisName.Pais_nombre.ToUpper();
                pais.pais_ID = paisName.Pais_Id!.ToUpper();

                rh_context.PAIS.Add(pais);

                await rh_context.SaveChangesAsync();

                PaisResponseModel response = new PaisResponseModel
                {
                    Pais_ID = pais.pais_ID,
                    Pais_nombre = pais.pais_nombre
                };
                return Created("api/localizaciones/pais/" + response.Pais_ID, response);
            }


            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }



        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdatePais(string Id, PaisModifyNameModel paisNameModify)
        {
            Id = Id.Substring(0, 3).ToUpper();

            try
            {
                PAIS? pais = rh_context.PAIS.SingleOrDefault(s => s.pais_ID == Id);

                if (pais == null)
                {
                    return NotFound();
                }
                else
                {

                    pais.pais_nombre = paisNameModify.Pais_nombre.ToUpper();

                    await rh_context.SaveChangesAsync();

                    PaisModifyModel paisResponse = new PaisModifyModel
                    {
                        Pais_Id = pais.pais_ID,
                        Pais_nombre = pais.pais_nombre
                    };

                    return Ok(paisResponse);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

       
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeletePais(string Id)
        {
            Id = Id.Substring(0, 3).ToUpper();

            try
            {
                PAIS? pais = rh_context.PAIS.SingleOrDefault(s => s.pais_ID == Id);

                if (pais == null)
                {
                    return NotFound();
                }
                else
                {

                    rh_context.PAIS.Remove(pais);

                    await rh_context.SaveChangesAsync();

                   

                    return NoContent();
                }
            }
            catch (Exception ex)
            {

                if(ex.InnerException != null)
                {

                }
                return StatusCode(StatusCodes.Status500InternalServerError, " mensaje : "+ex.Message+ ex.GetType()+ ex.GetBaseException());
            }
        }









    }


}

