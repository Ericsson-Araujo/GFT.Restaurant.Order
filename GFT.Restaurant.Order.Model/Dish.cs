using GFT.Restaurant.Order.Model.Enums;

namespace GFT.Restaurant.Order.Model
{
    public class Dish
    {
        public int Id { get; set; }
        public string Description { get; set; }              
        public short Type { get; set; }
        public string TimeOfDay { get; set; }
    }
}
