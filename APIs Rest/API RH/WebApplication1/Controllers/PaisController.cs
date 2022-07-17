

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RecursosHumanos.API.DataAccess;
using RecursosHumanos.API.Models.ResponseModels;
using RecursosHumanos.API.DataAccess.Models;
using RecursosHumanos.API.Models.CreateModels;
using RecursosHumanos.API.Models.ModifyModel;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Controllers
{

    //ESTE CONTROLADOR USA ENTITY FRAMEWORK CORE 6
    [Route("api/[controller]")]
    [ApiController]
    public class PaisController : ControllerBase
    {
        //aplicamos inyeccion de dependencia
        private readonly RH_Context rh_context;

        public PaisController(RH_Context _context)
        {
            rh_context = _context;
        }


        //metodo get que devuelve una lista con los paises de la BD
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetPaises()
        {

            try
            {
                PaisesResponseModel paises = new();
                
                 paises.Paises  = await rh_context.PAIS.Select(pais => new PaisModel {
                                                                            pais_ID = pais.pais_ID,
                                                                            pais_nombre = pais.pais_nombre
                                                                            })
                                            .ToListAsync();

                if (paises.Paises == null)  return StatusCode(StatusCodes.Status404NotFound, "NO DATA FOUND");

                return Ok(paises);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //metodo que devuelve el pais seleccionado por medio del id
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetPais(string Id)
        {

            //cojemos las 3 primeras letras y las pasamos a mayusculas
            Id = Id.Substring(0, 3).ToUpper();

            try
            {
                //buscamos el pais con LINQ en la BD
                PAIS? pais = await rh_context.PAIS.FirstOrDefaultAsync(s => s.pais_ID == Id);

                if (pais == null)
                {
                    return NotFound();
                }
                else
                {
                    //generamos la respuesta
                    PaisResponseModel paisResponse = new PaisResponseModel
                    {
                        pais_ID = pais.pais_ID,
                        pais_nombre = pais.pais_nombre
                    };
                    return Ok(paisResponse);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }







        //metodo para agregar un pais, usando como parametro un modelo de creacion de pais
        [HttpPost]
        public async Task<IActionResult> PostPais(PaisCreateModel paisName)
        {
            try
            {
                PAIS pais = new();
                pais.pais_nombre = paisName.Pais_nombre.ToUpper();
                pais.pais_ID = paisName.Pais_Id!.ToUpper();

                rh_context.PAIS.Add(pais);
                await rh_context.SaveChangesAsync();

                //generamos la respuesta
                PaisResponseModel response = new PaisResponseModel
                {
                    pais_ID = pais.pais_ID,
                    pais_nombre = pais.pais_nombre
                };

                //devolvemos la url con el nuevo pais, y sus datos
                return Created("api/localizaciones/pais/" + response.pais_ID, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        //metodo para actualizar un pais, como parametro exige el id de pais y los datos  a cambiar, en este caso el nombre
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

                    //generamos la respuesta
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

       //metodo para eliminar un pais de la bd, recibe como parametro el id de pais
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeletePais(string Id)
        {
            //toma desde posicion 0 hasta la 2 (osea 3 lugares)
            Id = Id[..3].ToUpper();

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
                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
            }
        }

    }

}

