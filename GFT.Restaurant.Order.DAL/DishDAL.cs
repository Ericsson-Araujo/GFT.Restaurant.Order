using GFT.Restaurant.Order.Interface;
using GFT.Restaurant.Order.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFT.Restaurant.Order.DAL
{
    public class DishDAL : IDishDAL
    {
        private readonly Context _context;

        public DishDAL(Context context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<List<Dish>> FilterDishes(DishFilter filter)
        {
            // search dishes where contains any type in filter and the same time os day
            List<Dish> listResult = new List<Dish>();

            foreach (var filterType in filter.Types)
            {
                var result = await _context.Dish
                               .Where(x => filter.TimeOfDay.ToLower() == x.TimeOfDay
                                    && x.Type == filterType)
                               .FirstOrDefaultAsync();

                // if not found add error e return
                if (result == null)
                {
                    result = new Dish 
                                { TimeOfDay = filter.TimeOfDay, Description = "error", Id = -1, Type = 99 };
                    
                    listResult.Add(result);
                    break;
                }

                listResult.Add(result);
            }
                                    
            return listResult.OrderBy(r => r.Type).ToList();
        }

        public async Task<Dish[]> GetAllDishes()
        {
            return await _context.Dish.ToArrayAsync();
        }
    }
}
