using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;
using System.Runtime.InteropServices;
using WebApplication14.DTOS.Reques;
using WebApplication14.DTOS.Response;
using WebApplication14.Pagination;
using WebApplication14.Repository;

namespace WebApplication14.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BattleController : ControllerBase
    {
        private readonly IbattleCommand _Command;
        private readonly IBattleQuery _Query;
        public BattleController(IbattleCommand command, IBattleQuery query)
        {
            _Command = command;
            _Query = query;
        }
        [HttpGet]
        public async Task<ActionResult<List<Battlereponse>>> GetAll([FromQuery] PaginationRequest pagination)
        {
            try
            {
                var result = await _Query.GetAll(pagination);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
                throw;

            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Battlereponse>>> Getporid(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
     
                var result = await _Query.GetById(id);
            if (result == null)
                return NotFound("No existe la batalla");

            return Ok(result);



        }
        [HttpGet("filtrar")]
        public async Task<ActionResult<List<Battlereponse>>> Filtrar([FromQuery] string nombre, [FromQuery] PaginationRequest pagination)
        {
            if (nombre is null)
            {
                return BadRequest();
            }
            try
            {
                var result = await _Query.FiltrarPornombre(nombre, pagination);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message); throw;

            }
        }
        [HttpGet("OrdenarAsc")]

        public async Task<ActionResult<List<Battlereponse>>> OrdenarAsc([FromQuery]PaginationRequest pagination)
        {
            try
            {
                var result = await _Query.OrdenarAsc(pagination);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message); throw;

            }
        }
        [HttpGet("OrdenarDesc")]
        public async Task<ActionResult<List<Battlereponse>>> OrdenarDesc([FromQuery]PaginationRequest pagination)
        {
            try
            {
                var result = await
                    _Query.OrdenarDesc(pagination);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBattleRequest dto)
        {
            if (dto is null)
            {
                return BadRequest();
            }
           
                var Create = await _Command.CrearAsync(dto);
                return Ok(Create);
          
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBattle dto)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            if (dto is null)
            {
                return BadRequest();
            }
          
                var result = await _Command.UpdateAsync(id, dto);
             if (!result)
            {
                return NotFound();
            }
            return Ok();
            }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

                var Delete = await _Command.DeleteAsync(id);
if (!Delete)
            {
                return NotFound();
            }
            return Ok();
          
        }
    }
}
