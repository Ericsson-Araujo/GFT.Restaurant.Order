using GFT.Restaurant.Order.Interface;
using GFT.Restaurant.Order.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFT.Restaurant.Order.BLL
{
    public class DishBLL : IDishBLL
    {
        private readonly IDishDAL _dishDAL;

        public DishBLL(IDishDAL dishDAL)
        {
            _dishDAL = dishDAL;
        }

        public async Task<List<Dish>> FilterDishes(DishFilter filter)
        {
            if (ValidateFilter(filter))
                return await _dishDAL.FilterDishes(filter);

            return null;
        }

        public async Task<Dish[]> GetAllDishes()
        {
            return await _dishDAL.GetAllDishes();
        }

        private bool ValidateFilter(DishFilter filter)
        {
            bool validFilter = filter != null;

            if (!validFilter)
                throw new Exception("There a invalid text in search.");

            validFilter = validFilter && (
                            filter.Types.Count() > 0
                            && !string.IsNullOrWhiteSpace(filter.TimeOfDay));

            if (!validFilter)
                throw new Exception("Some values is incorret or void");

            validFilter = validFilter && (
                            filter.TimeOfDay.ToLower() == "morning"
                            || filter.TimeOfDay.ToLower() == "night");

            if (!validFilter)
                throw new Exception("The first word is invalid, it should be: 'morning' or 'night'.");

            return validFilter;
        }
    }
}
