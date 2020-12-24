using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GFT.Restaurant.Order.Model
{
    public class DishFilter
    {
        [Required, JsonPropertyName("timeOfDay")]
        public string TimeOfDay { get; set; }
        
        [Required, JsonPropertyName("types")]
        public IEnumerable<short> Types { get; set; }
    }
}
