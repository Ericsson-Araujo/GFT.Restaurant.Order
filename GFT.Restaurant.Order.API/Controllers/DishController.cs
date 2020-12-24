using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GFT.Restaurant.Order.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GFT.Restaurant.Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //return Ok(new DishFilter());
            throw new NotImplementedException("primeiro construir os testes");
        }

        [HttpPost("SearchDishes")]
        public async Task<IActionResult> SearchDishes(DishFilter filter)
        {
            //return Ok(filter);
            throw new NotImplementedException("primeiro construir os testes");
        }
    }
}
