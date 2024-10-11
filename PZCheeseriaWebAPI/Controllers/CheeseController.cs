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
  /*  private DataTable cheeseTable;
*/
    public CheeseController(ICheeseService cheeseService)
    {
        _cheeseService = cheeseService;
       /* cheeseTable = _cheeseService.GetCheeseTable();*/
    }

    // GET: api/<CheeseController>
    [HttpGet("all")]
    public List<CheeseDTO> GetAll()
    {
        return _cheeseService.GetCheeseList();
    }

    // GET api/<CheeseController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<CheeseController>
    [HttpPost]
    public IActionResult Post([FromBody] CheeseDTO cheese)
    {
         _cheeseService.AddCheeseToTable( cheese);

        return Ok(_cheeseService.GetCheeseList());
    }

    // PUT api/<CheeseController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<CheeseController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }

  
}