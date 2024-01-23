using AutoMapper;
using DeviceApp.Repo.Classes;
using DeviceApp.Repo.Interface;
using EcommerceLib.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceServer.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController()]
    public class ProductController:ControllerBase
    {
        private readonly ProductRepository _repository;
        private readonly LikedProductRepository _likedProductRepository;
        private readonly ReviewRepository _reviewRepository;
        public ProductController(ProductRepository repository, ReviewRepository reviewRepository, 
                                 LikedProductRepository likedProductRepository)
        {
            _repository = repository;
            _reviewRepository = reviewRepository;
            _likedProductRepository = likedProductRepository;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] ProductDto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.AddAsync(model);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(new { Description = ex.Message });
                }
            }
            return BadRequest(new { Description = "Some field are empty" });
        }

        [HttpPost("AddReview")]
        [Authorize]
        public async Task<IActionResult> AddReview([FromBody] ReviewDto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _reviewRepository.AddAsync(model);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(new { Description = ex.Message });
                }
            }
            return BadRequest(new { Description = "Some field are empty" });
        }

        [HttpPost("AddLikedProduct")]
        [Authorize]
        public async Task<IActionResult> AddLikedProduct([FromBody] UserLikedProductDto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var lastLikedProduct = await _likedProductRepository.AddAsync(model);
                    return Ok(lastLikedProduct);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { Description = ex.Message });
                }
            }
            return BadRequest(new { Description = "Some field are empty" });
        }


        [HttpPost("GetRecomendation/{categoryId}/{productId:int}")]
        public async Task<IActionResult> GetRecomendation(
                           [FromBody] IEnumerable<int> viewedProducts,
                           int categoryId, int productId)
        {
            try
            {
                var products = await _repository.GetRecomendation(categoryId, productId, viewedProducts);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetAllLikedProducts/{userId}")]
        public async Task<IActionResult> GetAllLikedProducts(string userId)
        {
            try
            {
                var products = await _likedProductRepository.GetAllLikedProductsAsync(userId);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetNewestProducts")]
        public async Task<IActionResult> GetNewestProducts()
        {
            try
            {
                var products = await _repository.GetNewProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetByPage/{page}/{takeCount:int}")]
        public async Task<IActionResult> GetByPage(int page, int takeCount = 10)
        {
            try
            {
                var products = await _repository.GetByPage(page, takeCount);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return NotFound(new {Description = ex.Message});
            }

   
        }

        [HttpGet("GetProductById/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var products = await _repository.GetFullProductById(id);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("GetProductsById/{id:int}")]
        public async Task<IActionResult> GetLikedProducts(int id)
        {
            try
            {
                var products = await _repository.GetById(id);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("GetCategory/{categoryId:}/{page:int}")]
        public async Task<IActionResult> GetWithCategoryId(int categoryId,int page)
        {
            try
            {
                var products = await _repository.GetWithCategoryId(categoryId,page, 6);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("GetSubCategory/{subCategoryId}/{page:int}")]
        public async Task<IActionResult> GetWithSubCategoryId(int subCategoryId,int page)
        {
            try
            {
                var products = await _repository.GetWithSubCategoryId(subCategoryId,page, 6);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }


        }

        [HttpDelete("Delete/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await  _repository.RemoveAsync(id);
                return Ok(new { Description = "Succeed" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Description = ex.Message });
            }
        }

        [HttpDelete("DeleteFromLikedProduct/{id:int}/{userId}")]
        public async Task<IActionResult> DeleteFromLikedProducts(int id, string userId)
        {
            try
            {
                await _likedProductRepository.RemoveAsync(id, userId);
                return Ok(new { Description = "Succeed" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Description = ex.Message });
            }
        }


        [HttpPut("Update/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(ProductDto model, int id)
        {
            try
            {
                var entity = await _repository.Update(model,id);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
