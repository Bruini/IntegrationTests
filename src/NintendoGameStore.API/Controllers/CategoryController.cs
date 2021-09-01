using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NintendoGameStore.Aplication.Inputs;
using NintendoGameStore.Aplication.Interfaces;
using NintendoGameStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NintendoGameStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<ActionResult<List<Category>>> Get()
        {
            var categories = await _categoryService.GetAllAsync();
            if(categories != null && categories.Any())
            {
                return categories.ToList();
            }
            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Route("{id}")]
        public async Task<ActionResult<Category>> GetById(Guid id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category != null)
            {
                return category;
            }
            return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Category))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Category>> Post([FromBody] CategoryInput input)
        {
            var newCategory = await _categoryService.InsertOrUpdateAsync(input);
            if(newCategory != null)
            {
                return CreatedAtAction(nameof(Get), new { id = newCategory.Id }, newCategory);
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Category))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Category>> Put(Guid id, [FromBody] string name)
        {
            var input = new CategoryInput { Id = id, Name = name };
            var updatedCategory = await _categoryService.InsertOrUpdateAsync(input);
            if (updatedCategory != null)
            {
                return updatedCategory;
            }
            return BadRequest();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _categoryService.DeleteAsync(id);
            return NoContent();
        }
    }
}
