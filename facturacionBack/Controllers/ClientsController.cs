using AutoMapper;
using facturacionBack.Data;
using facturacionBack.Dto;
using facturacionBack.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace facturacionBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly FacturaContexto _facturaContexto;
        private readonly IMapper _mapper;

        public ClientsController(FacturaContexto facturaContexto, IMapper mapper)
        {
            _facturaContexto = facturaContexto;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientDto>>> GetAllClients()
        {
            var re_client = await _facturaContexto.Clients.ToListAsync();
            var mapcli = _mapper.Map<IEnumerable<ClientDto>>(re_client);
            return Ok(mapcli);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientDto>> GetClient(int id)
        {
            var clientItem = await _facturaContexto.Clients.FindAsync(id);

            if (clientItem == null)
            {
                return NotFound();
            }
            var mapcli = _mapper.Map<ClientDto>(clientItem);
            return mapcli;
        }
        [HttpPost]
        public async Task<ActionResult<ClientDto>> PostClient(ClientDto item)
        {
            var cli = new Client();

            cli.Name = item.Name;
            cli.Telephone = item.Telephone;
            cli.Address = item.Address;

            _facturaContexto.Clients.Add(cli);
            await _facturaContexto.SaveChangesAsync();

            return CreatedAtAction(nameof(GetClient), new { id = item.Id }, item);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> PutClient(int id, ClientRequestDto item)
        {
            var user = await _facturaContexto.Clients.FindAsync(id);
            if (user == null)
            {
                return BadRequest();
            }
            user.Name = item.Name;
            user.Telephone = item.Telephone;
            user.Address = item.Address;
            _facturaContexto.Entry(user).State = EntityState.Modified;
            await _facturaContexto.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteClient(int id)
        {
            var clientItem = await _facturaContexto.Clients.FindAsync(id);
            if (clientItem == null)
            {
                return NotFound();
            }
            _facturaContexto.Clients.Remove(clientItem);
            await _facturaContexto.SaveChangesAsync();

            return NoContent();
        }
        //[HttpPost]
        //public async Task<ActionResult> Upser
    }
}

