using AutoMapper;
using CommonGenericClasses;
using ElectronicsShop_service.Dtos;
using ElectronicsShop_service.Interfaces;
using ElectronicsShop_service.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectronicsShop_service.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ProductController : BaseController<Product, ProductDto>
{

    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    public ProductController(

        IProductRepository productRepository,

        ICategoryRepository categoryRepository,




        IProductUnitOfWork unitOfWork,
        IMapper mapper,
        IValidator<Product> validator) : base(unitOfWork, mapper, validator)
    {

        _productRepository = productRepository;

        _categoryRepository = categoryRepository;
    }














    /*public async override Task<IActionResult> Get(Guid ProductId)

    {
        Product product = (await _productRepository.Get(p => p.Id == ProductId, null, "")).FirstOrDefault()!;
        if (product == null)
        {
            return NotFound();
        }
        var category = await _categoryRepository.GetByIdAsync(product.categoryID);
        var newProductDto = new ProductDto()
        {
            Id=product.Id,
            code = product.code,
            Name=product.Name,
            categoryID= category.Id,
            CategoryName=category.Name,
            Brand=product.Brand,
            Manufacturer=product.Manufacturer,
            color=product.color,
            description=product.description,
            image=product.image,
            Rating=product.Rating,
            price=product.price,
            status=product.status

        };
        return Ok(newProductDto);
    }



*/
    [HttpGet]
    public async Task<string> GetProductCategory(Guid ProductId)

    {
        Product product = (await _productRepository.Get(p => p.Id == ProductId, null, "")).FirstOrDefault()!;
        if (product == null)
        {
            return "not found";
        }
        var category = await _categoryRepository.GetByIdAsync(product.categoryID);

        return category.Name;
    }




    [HttpGet]
    public async Task<IActionResult> FetchProductsByCategory(string category)
    {
        if (category == null)
        {
            return BadRequest(ModelState);
        }

        Category Category = (await _categoryRepository.Get(c => c.Name == category, null, "")).FirstOrDefault()!;

        if (Category == null)
        {
            return NotFound();
        }
        var products = await _productRepository.Get(p => p.categoryID == Category.Id, null, "");

        return Ok(_mapper.Map<List<ProductDto>>(products));
    }






  
}