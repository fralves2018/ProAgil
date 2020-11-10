using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.WebAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IProAgilRepository _repo;

        public EventoController(IProAgilRepository repo)
        {
            this._repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await _repo.GetAllEventoAsync(true);
                return Ok(results);    
            }
            catch (System.Exception)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }
            
        }

        [HttpGet("{EventoId}")]
        public async Task<IActionResult> Get(int EventoId)
        {
            try
            {
                var results = await _repo.GetAllEventoAsyncById(EventoId, true);
                return Ok(results);    
            }
            catch (System.Exception)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }
            
        }

        
        [HttpGet("getByTema/{tema}")]
        public async Task<IActionResult> Get(string tema)
        {
            try
            {
                var results = await _repo.GetAllEventoAsyncByTema(tema, true);
                return Ok(results);    
            }
            catch (System.Exception)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                //Mudança de estado
                _repo.Add(model);

                //Salva a mudança de estado anterior. 
                if(await _repo.SaveChangesAsync()){

                    //No OK, ele chama o metodo de evento por id. 
                    return Created($"/api/evento/{model.Id}", model);

                }

            }
            catch (System.Exception)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }

            return BadRequest();
            
        }

        [HttpPut("{EventoId}")]
        public async Task<IActionResult> Put(int EventoId, Evento model)
        {
            try
            {

                var evento = await _repo.GetAllEventoAsyncById(EventoId, false);

                //Se nenhum evento for encontrado, interrompe o PUT e retorna o status. 
                if (evento == null) return NotFound();

                //Mudança de estado
                _repo.Update(model);

                //Salva a mudança de estado anterior. 
                if(await _repo.SaveChangesAsync()){

                    //No OK, ele chama o metodo de evento por id. 
                    return Created($"/api/evento/{model.Id}", model);

                }

            }
            catch (System.Exception)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }

            return BadRequest();
            
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int EventoId)
        {
            try
            {

                var evento = await _repo.GetAllEventoAsyncById(EventoId, false);

                //Se nenhum evento for encontrado, interrompe o DELETE e retorna o status. 
                if (evento == null) return NotFound();

                //Mudança de estado
                _repo.Delete(evento);

                //Salva a mudança de estado anterior. 
                if(await _repo.SaveChangesAsync()){

                    //No OK
                    return Ok();

                }

            }
            catch (System.Exception)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }

            return BadRequest();
            
        }
    }
}