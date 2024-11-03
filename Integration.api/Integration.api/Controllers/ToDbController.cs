using Integration.business.DTOs.FromDTOs;
using Integration.business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Integration.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDbController : ControllerBase
    {
        private readonly IToDataBase _ToDatabaseService;

        public ToDbController(IToDataBase ToDatabaseService)
        {
            _ToDatabaseService = ToDatabaseService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToDataBase([FromBody] DbToAddDTO ToDbToAddDTO)
        {
            var result = await _ToDatabaseService.AddToDataBase(ToDbToAddDTO);
            if (result)
                return Ok();
            return BadRequest("Error adding to database");
        }

        [HttpPut("edit")]
        public async Task<IActionResult> EditToDataBase([FromBody] DbToEditDTO ToDbToEditDTO)
        {
            var result = await _ToDatabaseService.EditToDataBase(ToDbToEditDTO);
            if (result)
                return Ok();
            return NotFound("Database entry not found");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetToById(int id)
        {
            var result = await _ToDatabaseService.GetToById(id);
            if (result != null)
                return Ok(result);
            return NotFound();
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetToList()
        {
            var result = await _ToDatabaseService.GetToList();
            return Ok(result);
        }

        [HttpPut("select/{id}")]
        public async Task<IActionResult> SelectToDataBase(int id)
        {
            var result = await _ToDatabaseService.SelectToDataBase(id);
            if (result)
                return Ok();
            return NotFound("Database entry not found");
        }
    }
}
