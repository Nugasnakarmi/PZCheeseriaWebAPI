using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PZCheeseriaWebAPI.DTO;
using PZCheeseriaWebAPI.Helpers;
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
    }

    // In the future, when user authentication is done. These endpoints can be applied with authorization policies that control whether a user can
    // get access to, update, delete or add cheese.

    /// <summary>
    /// Gets a list of all cheeses.
    /// </summary>
    /// <returns>A List of Cheese or empty on exception thrown.</returns>
    [HttpGet("all")]
    public List<CheeseDTO> GetAll()
    {
        try
        {
            return _cheeseService.GetCheeseList();
        }
        catch (ExceptionHelper ex)
        {
            return [];
        }
    }

    /// <summary>
    /// Gets cheese information using id.
    /// </summary>
    /// <returns>A single cheese object.</returns>
    [HttpGet("{id}")]
    public CheeseDTO GetCheese(int id)
    {
        try
        {
            return _cheeseService.GetCheese(id);
        }
        catch (ExceptionHelper ex)
        {
            return null;
        }
    }

    /// <summary>
    /// Adds cheese to the list.
    /// </summary>
    /// <remarks>
    /// Example request:
    ///
    /// POST api/Cheese
    /// {
    ///     "Name": "Cheddar",
    ///     "ImageUrl": "https://www.cheese.com/media/img/cheese/cheddar_large.jpg",
    ///     "PricePerKilo": "85",
    ///     "Color": "Pale Yellow"
    /// }
    /// </remarks>
    /// <param name="cheese"></param>
    /// <returns>Newly created cheese</returns>
    /// <response code="201">Returns new cheese</response>
    /// <response code="500">Something went wrong in the server</response>

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Post([FromBody] CheeseDTO cheese)
    {
        try
        {
            CheeseDTO newCheese = _cheeseService.AddCheeseToTable(cheese);
            return CreatedAtAction("GetCheese", new { id = newCheese.Id }, newCheese);
          /*  return Ok(newCheese);*/
        }
        catch (ExceptionHelper ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Updates cheese information.
    /// </summary>
    /// <returns>Updated cheese</returns>
    /// <response code="200">Cheese updated</response>
    /// <response code="500">Something went wrong in the server</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Put(int id, [FromBody] CheeseDTO cheese)
    {
        try
        {
            CheeseDTO updatedCheese = _cheeseService.UpdateCheese(id, cheese);

            return Ok(updatedCheese);
        }
        catch (ExceptionHelper ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Deletes a cheese.
    /// </summary>
    /// <returns>Boolean value based on success of deletion. </returns>
    /// <response code="200">Cheese deleted</response>
    /// <response code="500">Something went wrong in the server</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Delete(int id)
    {
        try
        {
            return Ok(_cheeseService.DeleteCheese(id));
        }
        catch (ExceptionHelper ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}