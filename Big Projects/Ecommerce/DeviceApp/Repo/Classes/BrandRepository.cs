using AutoMapper;
using DeviceApp.Cache;
using DeviceApp.Repo.General;
using DeviceApp.Repo.Interface;
using EcommerceLib.Context;
using EcommerceLib.DTO;
using EcommerceLib.Models.ProductModel;
using Microsoft.EntityFrameworkCore;

namespace DeviceApp.Repo.Classes;

public class BrandRepository 
{
    private readonly IRepository<Brand,BrandDto> _repository;
    public BrandRepository(IRepository<Brand,BrandDto> repository)
    {
        _repository = repository;
    }

    public async Task AddAsync(BrandDto model)
    {
        await _repository.AddAsync(model);
        await _repository.SaveChangesAsync();

    }
    public async Task<IEnumerable<Brand>> GetAllAsync()
    {
        var brands = await _repository.GetAllAsync();
        return brands;
    }
    public async Task<Brand> GetByIdAsync(int id)
    {
        try
        {
            var brand = await _repository.GetFirstOrDefaultAsync(x => x.Id == id, false,
                                    source => source.Include(x => x.Products));
            return brand;
        }
        catch (Exception ex)
        {
            throw new ArgumentNullException(id + " is not found");
        }

    }

    public async Task RemoveAsync(int id)
    {
        try
        {
            await _repository.RemoveAsync(id,include:x=>x
            .Include(x=>x.Products)
            .ThenInclude(x=>x.ProductСharacteristics),
            filter:x=>x.Id == id);
            await _repository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new ArgumentNullException(ex.Message);
        }
    }
    public async Task<Brand> UpdateAsync(BrandDto model)
    {
        try
        {
            var entity = await _repository.Update(model, source => source.Include(x => x.Products));
            await _repository.SaveChangesAsync();
            return entity;

        }
        catch (Exception ex)
        {
            throw new ArgumentNullException("Not found");

        }
    }



}
