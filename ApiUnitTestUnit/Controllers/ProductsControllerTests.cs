using CorePlusMongoDBApi.Controllers;
using Moq;
using CorePlusMongoDBApi.Services;
using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;
using CorePlusMongoDBApi.Models;
using FakeItEasy;
using CorePlusMongoDBApi.Interfaces;
using MongoDB.Driver;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace ApiUnitTestUnit.Controllers
{  
    public class ProductsControllerTests
    {
        private readonly ProductsService _productsService;
        private readonly CategoryService _categoryService;
        private readonly ProductsController _controller;     

        
        public ProductsControllerTests()
        {
            var settings = DBSettings();
            _productsService = new ProductsService(settings);
            _categoryService = new CategoryService(settings);

            //arrange
            _controller = new ProductsController(_productsService, _categoryService);
        }

        public IEcommerceDBSettings DBSettings() {
            //set up db connection variables here
            IEcommerceDBSettings settings = new EcommerceDBSettings();
            settings.ConnectionString = "mongodb://localhost:27017";
            settings.ProductsCollectionName = "products";
            settings.CategoryCollectionName = "category";
            settings.DatabaseName = "ecommercestore";
            return settings;
        }

        [Fact]
        public async void Task_GetPproducts_Return_OkResult()
        { 
            //Act  
            var data = await _controller.GetAll();

            //Assert  
            Assert.IsType<OkObjectResult>(data.Result);
        }

        [Theory]
        [InlineData("61d8566a5824000035003397")]
        public async void Task_GetProductByIdTest(string id)
        {
            //id 1: valid
            //id 2: invalid

            //Act
            var okResult = await _controller.GetById(id);
            
            //Assert
            Assert.IsType<OkObjectResult>(okResult.Result);

            //check the value of ok object result.
            var item = okResult.Result as OkObjectResult;

            //We Expect to return a single product
            Assert.IsType<Products>(item.Value);
            if (item != null) {
                //Now, let us check the value itself.
                var productItem = item.Value as Products;
                Assert.Equal(id, productItem.Id);
                Assert.Equal("Olive Oil Shampoo", productItem.product_name);
            }
            else
            {
                Assert.Null(item.Value);
            }           
        }

        [Theory]
        [InlineData("61d8566a5824000035003397", "61d8566a5824000035113397")]
        public async void GetProductByIdTest(string valid_id, string invalid_id) {
            //id 1: valid
            //id 2: invalid

            //Act
            var okResult = await _controller.GetById(valid_id);
            var notFoundResult = await _controller.GetById(invalid_id);

            //Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
            Assert.IsType<NotFoundResult>(notFoundResult.Result);  

            //check the value of ok object result.
            var item = okResult.Result as OkObjectResult;

            //We Expect to return a single product
            Assert.IsType<Products>(item.Value);

            //check the expected value itself.
            var productItem = item.Value as Products;
            Assert.Equal(valid_id, productItem.Id);
            Assert.Equal("Olive Oil Shampoo", productItem.product_name); //check against expected product name output
        }

    }
    
}
        
    

