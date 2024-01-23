using AutoMapper;
using DeviceApp.Cache;
using DeviceApp.Repo.Interface;
using DeviceApp.Services;
using EcommerceLib.DTO;
using EcommerceLib.Models.ProductModel;

namespace DeviceApp.Repo.Classes;

public class ReviewRepository: IRepository
{
    private readonly IGenericRepository<Review, ReviewDto> _repository;

    public ReviewRepository(IGenericRepository<Review, ReviewDto> repository )
    {
        _repository = repository;
    }

    public async Task AddAsync(ReviewDto review)
    {
        var repeatRateCheck = await _repository
            .GetAllAsync(filter: x => x.AppUserId == review.AppUserId && x.ProductId == review.ProductId);

        if( repeatRateCheck == null || repeatRateCheck.Count() ==0 )
        {
            await _repository.AddAsync(review);
            await _repository.SaveChangesAsync();
        }
        else
        {
            throw new Exception("You alredy rate this product");
        }
    }
}
