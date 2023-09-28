﻿using Microsoft.AspNetCore.Mvc;
using Versionamento.Application.DTOs;
using Versionamento.Application.Interfaces;

namespace Versionamento.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioServices _services;

        public UsuariosController(IUsuarioServices services)
        {
            _services = services;
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<UsuariosDto>>> GetAll()
        {
            try
            {
                string typeFormat = Request.Headers.Accept.ToString();

                var usuarios = await _services.GetAll(typeFormat);
                if (usuarios is null)
                    return BadRequest();

                return Ok(usuarios);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetByCodigo/{codigo:Guid}")]
        public async Task<ActionResult<IEnumerable<UsuariosDto>>> GetAll(Guid codigo)
        {
            try
            {
                string typeFormat = Request.Headers.Accept.ToString();

                var usuario = await _services.GetByCodigo(codigo, typeFormat);
                if (usuario is null)
                    return BadRequest();

                return Ok(usuario);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}