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
                InventoryItemId = 002,
                ItemName = "Samsung s21",
                Quantity = 0,
                InStock = false,
                ItemId = 01,
                StoreLocation = "1350 2nd ave east",
                Item = item
            });

            foreach (var inventoryItem in inventoryItems)
            {
                _context.InventoryItems.Add(inventoryItem);
            }

            _context.SaveChanges();

            controller = new InventoryItemsController(_context);
        }

        #region Index

        [TestMethod]
        public void IndexViewResult()
        {
            //arrange

            //act
            var result = (ViewResult)controller.Index().Result;

            //arrange
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void IndexGetsInventoryItems()
        {
            // act
            var result = (ViewResult)controller.Index().Result;
            List<InventoryItem> model = (List <InventoryItem>) result.Model;

            //assert
            CollectionAssert.AreEqual(inventoryItems, model);
        }
        #endregion
        #region Details

        [TestMethod]
        public void DetailsNoID()
        {
            var result = (ViewResult)controller.Details(null).Result;
            Assert.AreEqual("Error404", result.ViewName);
        }

        [TestMethod]
        public void DeatilasInvalidID()
        {
            var result = (ViewResult)controller.Details(-1).Result;
            Assert.AreEqual("Error404", result.ViewName);
        }

        #endregion
    }
}
