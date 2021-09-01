using Microsoft.AspNetCore.Mvc;
using Moq;
using NintendoGameStore.API.Controllers;
using NintendoGameStore.Aplication.Inputs;
using NintendoGameStore.Aplication.Interfaces;
using NintendoGameStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;


namespace NintendoGameStore.UnitTests.Controllers
{
    [ExcludeFromCodeCoverage]
    public class CategoryControllerTests
    {

        private Mock<ICategoryService> _mockCategoryService;

        public CategoryControllerTests()
        {
            this._mockCategoryService = new Mock<ICategoryService>();
        }

        private CategoryController CreateCategoryController()
        {
            return new CategoryController(this._mockCategoryService.Object);
        }

        [Fact]
        public async Task Get_Should_Returns_Ok()
        {
            var categories = new List<Category>
            {
                new Category("Puzzle"),
                new Category("Action"),
                new Category("Sport")
            };

            this._mockCategoryService.Setup(x => x.GetAllAsync()).ReturnsAsync(categories);
            var categoryController = this.CreateCategoryController();

            var result = await categoryController.Get();

            var actionResult = Assert.IsType<ActionResult<List<Category>>>(result);
            var returnValue = Assert.IsType<List<Category>>(actionResult.Value);
            Assert.Equal(categories.Count, returnValue.Count);
            this._mockCategoryService.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task Get_Should_Should_Returns_NoContent()
        {

            var categoryController = this.CreateCategoryController();

            var result = await categoryController.Get();

            var actionResult = Assert.IsType<ActionResult<List<Category>>>(result);
            Assert.IsType<NoContentResult>(actionResult.Result);
            this._mockCategoryService.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetById_Should_Returns_Ok()
        {
            var id = Guid.NewGuid();
            var category = new Category("Sport");
            category.Id = id;

            this._mockCategoryService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(category);

            var categoryController = this.CreateCategoryController();
            var result = await categoryController.GetById(id);

            var actionResult = Assert.IsType<ActionResult<Category>>(result);
            var returnValue = Assert.IsType<Category>(actionResult.Value);
            Assert.Equal(category, returnValue);
            this._mockCategoryService.Verify(x => x.GetByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetById_Should_Returns_NotFound()
        {
            var categoryController = this.CreateCategoryController();
            var id = Guid.NewGuid();

            var result = await categoryController.GetById(id);

            var actionResult = Assert.IsType<ActionResult<Category>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result);
            this._mockCategoryService.Verify(x => x.GetByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task Post_Should_Returns_Created()
        {
            var input = new CategoryInput
            {
                Name = "Arcade"
            };

            var category = new Category(input.Name)
            {
                Id = Guid.NewGuid()
            };

            this._mockCategoryService.Setup(x => x.InsertOrUpdateAsync(input)).ReturnsAsync(category);

            var categoryController = this.CreateCategoryController();
            var result = await categoryController.Post(input);

            var actionResult = Assert.IsType<ActionResult<Category>>(result);
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var returnValue = Assert.IsType<Category>(createdAtActionResult.Value);
            
            Assert.Equal(input.Name, returnValue.Name);
            Assert.NotEqual(Guid.Empty, returnValue.Id);
            this._mockCategoryService.Verify(x => x.InsertOrUpdateAsync(input), Times.Once);
        }

        [Fact]
        public async Task Post_Should_Returns_BadRequest()
        {
            var input = new CategoryInput
            {
                Name = "Arcade"
            };

            var categoryController = this.CreateCategoryController();
            var result = await categoryController.Post(input);

            var actionResult = Assert.IsType<ActionResult<Category>>(result);
            Assert.IsType<BadRequestResult>(actionResult.Result);

            this._mockCategoryService.Verify(x => x.InsertOrUpdateAsync(input), Times.Once);
        }

        [Fact]
        public async Task Put_Should_Returns_Ok()
        {
            string name = "Arcade";
            Guid id = Guid.NewGuid();

            var input = new CategoryInput
            {
                Id = id,
                Name = name
            };

            var category = new Category(input.Name)
            {
                Id = input.Id.Value
            };

            this._mockCategoryService.Setup(x => x.InsertOrUpdateAsync(It.IsAny<CategoryInput>())).ReturnsAsync(category);

            var categoryController = this.CreateCategoryController();
            var result = await categoryController.Put(id, name);

            var actionResult = Assert.IsType<ActionResult<Category>>(result);
            var returnValue = Assert.IsType<Category>(actionResult.Value);

            Assert.Equal(category, returnValue);
            this._mockCategoryService.Verify(x => x.InsertOrUpdateAsync(It.IsAny<CategoryInput>()), Times.Once);
        }

        [Fact]
        public async Task Put_Should_Returns_BadRequest()
        {
            var categoryController = this.CreateCategoryController();
            var result = await categoryController.Put(Guid.Empty, "Music");

            var actionResult = Assert.IsType<ActionResult<Category>>(result);
            Assert.IsType<BadRequestResult>(actionResult.Result);
            this._mockCategoryService.Verify(x => x.InsertOrUpdateAsync(It.IsAny<CategoryInput>()), Times.Once);
        }

        [Fact]
        public async Task Delete_Should_Returns_NoContent()
        {
            this._mockCategoryService.Setup(x => x.DeleteAsync(It.IsAny<Guid>())).Verifiable();
            var categoryController = this.CreateCategoryController();

            var result = await categoryController.Delete(Guid.NewGuid());

            Assert.IsType<NoContentResult>(result);
        }
    }
}
