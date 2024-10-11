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
    public CheeseController(ICheeseService cheeseService) {
        _cheeseService = cheeseService;
    }

    // GET: api/<CheeseController>
        [HttpGet("all")]
        public List<CheeseDTO> GetAll()
        {   
        DataTable cheeseTable = _cheeseService.GetCheeseTable();
        List<CheeseDTO> cheeseList = new List<CheeseDTO>();
        foreach (DataRow row in cheeseTable.Rows)
        {   int id = int.Parse(row["Id"].ToString());
            string name = row["Name"].ToString();
            string imageUrl = row["ImageUrl"].ToString();
            decimal pricePerKilo = decimal.Parse(row["PricePerKilo"].ToString());
            string color = row["Color"].ToString();
            cheeseList.Add(new CheeseDTO(id, name, imageUrl, pricePerKilo, color) );
        }
        return cheeseList;
        }

        // GET api/<CheeseController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CheeseController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        
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

