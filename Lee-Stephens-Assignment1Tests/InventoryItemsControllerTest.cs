using Lee_Stephens_Assignment1_COMP2084.Controllers;
using Lee_Stephens_Assignment1_COMP2084.Data;
using Lee_Stephens_Assignment1_COMP2084.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lee_Stephens_Assignment1Tests
{
    [TestClass]
    public class InventoryItemsControllerTest
    {

        //Vars for db
        private ApplicationDbContext _context; //in memory db connection
        private InventoryItemsController controller;
        List<InventoryItem> inventoryItems = new List<InventoryItem>();

        [TestInitialize]

        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _context = new ApplicationDbContext(options);

            //Mock data
            var item = new Item
            {
                ItemId = 01, ItemName = "iPhone 13 128gb", Brand = "Apple", Section = "Cellphones"
            };

            inventoryItems.Add(new InventoryItem
            {
                InventoryItemId = 002, ItemName = "Samsung s21", Quantity = 0, InStock = false, ItemId = 01
            });

            controller = new InventoryItemsController(_context);
        }

        [TestMethod]
        public void IndexViewResult()
        {
            //arrange

            //act
            var result = (ViewResult)controller.Index().Result;

            //arrange
            Assert.AreEqual("Index", result.ViewName);
        }
    }
}
