using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System;
using System.Net;
using System.Net.Http.Headers;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProAgil.API.Dtos;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventosController : ControllerBase
    {
        private IProAgilRepository Context { get; }
        private readonly IMapper _mapper;

        public EventosController(IProAgilRepository context, IMapper mapper)
        {
            this._mapper = mapper;
            this.Context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await Context.GetAllEventoAsync(true);

                var result = _mapper.Map<EventoDto[]>(eventos);

                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro Interno API - " + ex.Message);
            }

        }

        [HttpPost("upload")]
        public async Task<IActionResult> upload()
        {
            try
            {   
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources","Images");               
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;
                    var fullPath = Path.Combine(pathToSave, fileName.Replace("\"", " ").Trim());

                     using(var stream = new FileStream(fullPath, FileMode.Create))   
                     {
                        file.CopyTo(stream);
                     }

                }

                return Ok();
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro Interno API - " + ex.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var results = await Context.GetAllEventoAsyncById(id, true);
                return Ok(results);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro Interno API - {ex.Message} ");
            }

        }

        [HttpGet("getByTema{tema}")]
        public async Task<IActionResult> Get(string tema)
        {
            try
            {
                var results = await Context.GetAllEventoAsyncByTema(tema, true);
                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro Interno API");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Post(EventoDto model)
        {
            try
            {

                var eventos = _mapper.Map<Evento>(model);

                Context.Add(eventos);

                if (await Context.SaveChangesAsync())
                {
                    return Created($"eventos{model.Id}", model);
                }

            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro Interno API -  {ex.Message} ");
            }

            return BadRequest();
        }

        [HttpPut("{EventoId}")]
        public async Task<IActionResult> Put(int EventoId, Evento model)
        {
            try
            {
                var evento = await Context.GetAllEventoAsyncById(EventoId, false);

                if (evento == null)
                {
                    return NotFound();
                }

                _mapper.Map(model, evento);

                Context.Update(model);

                if (await Context.SaveChangesAsync())
                {
                    return Created($"eventos{model.Id}", _mapper.Map<EventoDto>(evento));
                }

            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro Interno API - {ex.Message} ");
            }

            return BadRequest();
        }

        [Route("{EventoId:int}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int EventoId)
        {
            try
            {
                var evento = await Context.GetAllEventoAsyncById(EventoId, false);

                if (evento == null)
                {
                    return NotFound();
                }

                Context.Delete(evento);

                if (await Context.SaveChangesAsync())
                {
                    return Created($"eventos{evento.Id}", evento);
                }

            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro Interno API");
            }

            return BadRequest();
        }

    }
}