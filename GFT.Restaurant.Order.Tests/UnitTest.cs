using GFT.Restaurant.Order.API;
using GFT.Restaurant.Order.API.Controllers;
using GFT.Restaurant.Order.BLL;
using GFT.Restaurant.Order.DAL;
using GFT.Restaurant.Order.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
            _connDB = "Data Source=../GFT.Restaurant.Order.API/GFTRestaurantOrder.db";
        }

        private string _connDB;

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
                DishController controller;
                var listTypes = new List<short>(arrayTypes);

                var options = new DbContextOptionsBuilder<Context>()
                                .UseSqlite(_connDB)
                                .Options;

                using (var context = new Context(options))
                {
                    controller = new DishController(
                                        new DishBLL(
                                            new DishDAL(context)
                                           ));

                    var response = await controller
                    .SearchDishes(new DishFilter { TimeOfDay = timeOfDay, Types = listTypes }) as List<Dish>;

                    Assert.NotNull(response, "The searched dish returned null");
                    Assert.Pass();
                }                
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
                var options = new DbContextOptionsBuilder<Context>()
                                .UseSqlite(_connDB)
                                .Options;

                using (var context = new Context(options))
                {
                    var controller = new DishController(
                                    new DishBLL(
                                        new DishDAL(
                                            context)
                                        ));

                    var response = await controller.Get() as List<Dish>;

                    Assert.NotNull(response, "The dishes returned null");

                    Assert.Pass();
                }
            }
            catch (System.Exception ex)
            {
                Assert.Fail("Exceção: " + ex.Message);
            }
            
        }
    }
}