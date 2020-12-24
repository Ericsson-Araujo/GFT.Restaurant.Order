using GFT.Restaurant.Order.API.Controllers;
using GFT.Restaurant.Order.Model;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFT.Restaurant.Order.Tests
{
    public class UnitTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("morining", new short[]{ 1, 2, 3})]        
        [TestCase("morining", new short[] { 2, 1, 3 })]
        [TestCase("morining", new short[] { 1, 2, 3, 4 })]
        [TestCase("morining", new short[] { 1, 2, 3, 3, 3 })]
        [TestCase("night", new short[] { 1, 2, 3, 4 })]
        [TestCase("night", new short[] { 1, 2, 2, 4 })]
        [TestCase("night", new short[] { 1, 2, 3, 5 })]
        [TestCase("night", new short[] { 1, 1, 2, 3, 5 })]
        [TestCase("afternoon", new short[] { 1, 2, 3 })]
        [TestCase(null, new short[] { 1, 1, 2 })]
        [TestCase("morining", null)]
        public async Task SearchDisherAsync_ShouldReturnCorrect(string timeOfDay, short[] arrayTypes)
        {
            try
            {
                var listTypes = new List<short>(arrayTypes);
                var controller = new DishController();

                var response = await controller
                    .SearchDishes(new DishFilter { TimeOfDay = timeOfDay, Types = listTypes }) as List<Dish>;

                Assert.NotNull(response, "The searched dish returned null");

                //if (listTypes.Any(t => t < 1 || t > 4))
                //    Assert.Contains(response, )
                
                Assert.Pass();
            }
            catch (System.Exception ex)
            {
                Assert.Fail("Exceção: " + ex.Message);
            }
            
        }

        [Test]
        public async Task GetAllDishesAsync_ShouldReturnNotNull()
        {
            try
            {
                var controller = new DishController();

                var response = await controller.Get() as List<Dish>;

                Assert.NotNull(response, "The dishes returned null");
                
                Assert.Pass();
            }
            catch (System.Exception ex)
            {
                Assert.Fail("Exceção: " + ex.Message);
            }
            
        }
    }
}