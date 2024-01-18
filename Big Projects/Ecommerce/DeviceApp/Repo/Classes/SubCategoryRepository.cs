using AutoMapper;
using DeviceApp.Cache;
using DeviceApp.Repo.General;
using DeviceApp.Repo.Interface;
using EcommerceLib.Context;
using EcommerceLib.DTO;
using EcommerceLib.Models.ProductModel;
using Microsoft.EntityFrameworkCore;

namespace DeviceApp.Repo.Classes;

public class SubCategoryRepository : IRepository
{
    IGenericRepository<SubCategory, SubCategoryDto> _repository;
    public SubCategoryRepository(IGenericRepository<SubCategory, SubCategoryDto> repository)
    {
        _repository = repository;
    }

    public async Task AddAsync(SubCategoryDto model)
    {
        await _repository.AddAsync(model);
        await _repository.SaveChangesAsync();

    }
    public async Task<IEnumerable<SubCategory>> GetAllAsync()
    {
        var subcategories = await _repository.GetAllAsync(null, source => source.Include(x => x.Products));
        return subcategories;
    }

    public async Task<SubCategory> GetByIdAsync(int id)
    {
        try
        {
            var subcategory = await _repository.GetFirstOrDefaultAsync(x => x.Id == id, 
                                    false,
                                    source => source.Include(x => x.Characteristics));
            return subcategory;
        }
        catch (Exception ex)
        {
            throw new ArgumentNullException(id + " is not found");
        }

    }

    public async Task<IEnumerable<SubCategory>> GetByNameAsync(string name)
    {
        try
        {
            var subcategory = await _repository.GetAllAsync(filter:x => x.Name == name);
            return subcategory;
        }
        catch (Exception ex)
        {
            throw new ArgumentNullException(name + " is not found");
        }
    }

    public async Task  RemoveAsync(int id)
    {
        try
        {
            await _repository.RemoveAsync(id);
            await _repository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new ArgumentNullException(ex.Message);
        }
    }

    public async Task<SubCategory> UpdateAsync(SubCategoryDto model)
    {
        try
        {
            var entity = await _repository.Update(model,
                x=>x.Include(x=>x.Characteristics),
                x=>x.Id==model.Id);
         
            return entity;

        }
        catch (Exception ex)
        {
            throw new ArgumentNullException(ex.Message);

        }
    }
}
