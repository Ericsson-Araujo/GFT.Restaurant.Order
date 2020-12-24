using GFT.Restaurant.Order.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GFT.Restaurant.Order.Interface
{
    public interface IDishBLL
    {
        Task<Dish[]> GetAllDishes();

        Task<List<Dish>> FilterDishes(DishFilter filter);                
    }
}
