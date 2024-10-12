using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PZCheeseriaWebAPI.DTO;
using PZCheeseriaWebAPI.Interfaces;
using PZCheeseriaWebAPI.Services;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PZCheeseriaWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CheeseController : ControllerBase
{
    private readonly ICheeseService _cheeseService;

    public CheeseController(ICheeseService cheeseService)
    {
        _cheeseService = cheeseService;
       /* cheeseTable = _cheeseService.GetCheeseTable();*/
    }
    // In the future, when user authentication is done. These endpoints can be applied with authorization policies that control whether a user can 
    // get access to, update, delete or add cheese.

    // GET: api/<CheeseController>
    [HttpGet("all")]
    public async Task<List<CheeseDTO>> GetAll()
    {
        return await _cheeseService.GetCheeseListAsync();
    }

    // GET api/<CheeseController>/5
    
    [HttpGet("{id}")]
    public async Task<CheeseDTO> GetCheese(int id)
    {
         return await _cheeseService.GetCheeseAsync(id);
    }

    // POST api/<CheeseController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CheeseDTO cheese)
    {
         _cheeseService.AddCheeseToTable( cheese);

        return Ok( await _cheeseService.GetCheeseListAsync());
    }

    // PUT api/<CheeseController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] CheeseDTO cheese)
    {
      CheeseDTO updatedCheese = await _cheeseService.UpdateCheeseAsync(id,cheese);

        return Ok(updatedCheese);
    }

    // DELETE api/<CheeseController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
       bool deleted = await _cheeseService.DeleteCheeseAsync(id);
        return Ok(deleted);
    }

  
}