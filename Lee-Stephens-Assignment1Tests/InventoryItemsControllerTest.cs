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
            var result = (ViewResult)controller.Details(0).Result;
            Assert.AreEqual("Error404", result.ViewName);
        }

        [TestMethod]
        public void DetailsIDValidViewResult()
        {
            var result = (ViewResult)controller.Details(002).Result;
            Assert.AreEqual("Details", result.ViewName);
        }

        [TestMethod]
        public void DetailsLoadsInventoryItem()
        {
            var result = (ViewResult)controller.Details(002).Result;
            InventoryItem inventoryItem = (InventoryItem)result.Model;

            Assert.AreEqual(inventoryItems[0], inventoryItem);
        }

        #endregion
        #region Create

        [TestMethod]
        public void  CreateLoadView()
        {
            //act
            var result = (ViewResult)controller.Create();

            //arrange
            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void CreateValidListReturned()
        {
            var result = controller.ViewData["ItemId"];

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CreateIfNotNull()
        {
            //Creating a test inventory item
            var inventoryItem = new InventoryItem
            {
                InventoryItemId = 003,
                ItemName = "Samsung A21",
                Quantity = 0,
                InStock = true,
                ItemId = 01,
                StoreLocation = "1350 2nd ave east",
                Item = new Item
                {
                    ItemId = 01,
                    ItemName = "iPhone 13 128gb",
                    Brand = "Apple",
                    Section = "Cellphones"
                }
            };

            //Handles errors in model validation
            controller.ModelState.AddModelError("put a descriptive key name here", "add an appropriate key value here");

            var result = (ViewResult)controller.Create(inventoryItem).Result;

            Assert.IsNotNull(result);
        }


        public void CreatePostToDB()
        {
            //Creating a new inventory item to pass in
            var inventoryItem = new InventoryItem
            {
                InventoryItemId = 002,
                ItemName = "Samsung s21",
                Quantity = 0,
                InStock = false,
                ItemId = 01,
                StoreLocation = "1350 2nd ave east",
                Item = new Item
                {
                    ItemId = 01,
                    ItemName = "iPhone 13 128gb",
                    Brand = "Apple",
                    Section = "Cellphones"
                }
            };

            _context.InventoryItems.Add(inventoryItem);

            //comparing the inventory item i just added to the one declared in my initialize method
            Assert.AreEqual(inventoryItem, inventoryItems[0]);
        }



        [TestMethod]
        public void CreatePostReturns()
        {

            //Creating a test inventory item
            var inventoryItem = new InventoryItem
            {
                InventoryItemId = 003,
                ItemName = "Samsung A21",
                Quantity = 0,
                InStock = true,
                ItemId = 01,
                StoreLocation = "1350 2nd ave east",
                Item = new Item
                {
                    ItemId = 01,
                    ItemName = "iPhone 13 128gb",
                    Brand = "Apple",
                    Section = "Cellphones"
                }
            };

            //Handles errors in model validation
            controller.ModelState.AddModelError("put a descriptive key name here", "add an appropriate key value here");

            var result = (ViewResult)controller.Create(inventoryItem).Result;

            Assert.AreEqual("Create", result.ViewName);
        }

        #endregion
        #region Edit

        [TestMethod]
        public void EditValidId()
        {
            var result = (ViewResult)controller.Edit(002).Result;

            Assert.AreEqual("Edit", result.ViewName);
        }

        [TestMethod]
        public void EditInvalidId()
        {
            var result = (ViewResult)controller.Edit(0).Result;
            Assert.AreEqual("Error404", result.ViewName);
        }

        [TestMethod]
        public void EditNullId()
        {
            var result = (ViewResult)controller.Edit(null).Result;
            Assert.AreEqual("Error404", result.ViewName);
        }

        [TestMethod]
        public void EditLoadsInventoryItem()
        {
            var result = (ViewResult)controller.Edit(002).Result;
            InventoryItem inventoryItem = (InventoryItem)result.Model;

            Assert.AreEqual(inventoryItems[0], inventoryItem);
        }

        [TestMethod]
        public void EditSaveToDb()
        {
            var inventoryItem = inventoryItems[0];
            inventoryItem.ItemName = "iPhone 11";
            var result = (RedirectToActionResult)controller.Edit(inventoryItem.InventoryItemId, inventoryItem).Result;
            Assert.AreEqual("Index", result.ActionName);
        }

        [TestMethod]
        public void EditIdReturned()
        {
            var result = (ViewResult)controller.Edit(34, inventoryItems[0]).Result;
            Assert.AreEqual("Error404", result.ViewName);
        }

        #endregion
        #region Delete

        [TestMethod]
        public void DeleteIsValid()
        {
            var result = (ViewResult)controller.Delete(002).Result;
            Assert.AreEqual("Delete", result.ViewName);
        }

        [TestMethod]
        public void DeleteIdNotValid()
        {
            var result = (ViewResult)controller.Delete(003).Result;
            Assert.AreEqual("Error404", result.ViewName);
        }

        [TestMethod]
        public void DeleteIdIsNull()
        {
            var result = (ViewResult)controller.Delete(null).Result;
            Assert.AreEqual("Error404", result.ViewName);
        }

        [TestMethod]
        public void DeleteInventoryItem()
        {
            var result = (ViewResult)controller.Delete(002).Result;
            InventoryItem inventoryItem = (InventoryItem)result.Model;
            Assert.AreEqual(inventoryItem, inventoryItems[0]);
      
        }

        [TestMethod] 
        public void DeleteSuccessBackToIndex()
        {
            var result = (RedirectToActionResult)controller.DeleteConfirmed(002).Result;
            Assert.AreEqual("Index", result.ActionName);
        }

        [TestMethod]
        public void DeleteConfirmed()
        {
            var result = _context.InventoryItems.Find(0);

            Assert.AreEqual(result, null);
        }
 
        #endregion
    }
}
