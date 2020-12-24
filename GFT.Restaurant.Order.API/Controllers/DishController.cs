using GFT.Restaurant.Order.Interface;
using GFT.Restaurant.Order.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GFT.Restaurant.Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishBLL _dishBLL;

        public DishController(IDishBLL dishBLL)
        {
            _dishBLL = dishBLL;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _dishBLL.GetAllDishes();

                if (result.Length > 0)
                    return Ok(result);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);                
            }            
        }

        [HttpPost("SearchDishes")]
        public async Task<IActionResult> SearchDishes(DishFilter filter)
        {
            try
            {
                List<Dish> result = await _dishBLL.FilterDishes(filter);

                if (result.Count > 0)
                    return Ok(result);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }
    }
}
