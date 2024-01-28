using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using TestsApp.Repository.Generic;
using TestsApp.Repository.İnterfaces;
using TestsLib.DbContexts;
using TestsLib.Dto;
using TestsLib.Models;
using TestsLib.Models.UserModels;

namespace TestsApp.Repository.ModelReposotry;

public class UserRepository
{
    private IGenericRepository<User,UserDto> _repository;
    public UserRepository(IGenericRepository<User, UserDto> repository)
    {   
        _repository = repository;
    }
    public async Task<User> GetUserById (string id)
    {
        return await _repository.FindOneAsync(x=>x.Id == id);
    }
    public async Task<User> GetUser(RequestUser userDto)
    {
        User userEmailCheck =  await _repository
            .FindOneAsync(x => x.Email == userDto.Email,
            include:x=>x.Include(x=>x.Tests));

        if(userEmailCheck == null) 
        {
            throw new ArgumentNullException("Email is wrong");
        }

        User userPasswordCheck = await _repository.FindOneAsync(x => x.Email == userDto.Email && x.Password == userDto.Password);
        
        if(userPasswordCheck == null)
        {
            throw new ArgumentNullException("Password is wrong");
        }
        userEmailCheck.isAuth = true;
        return userEmailCheck;
    }
    public async Task CreateAsync(UserDto user)
    {
        user.Id = _repository.GenerateNewId();

        await _repository.CreateAsync(user);
        await _repository.SaveAsync();
    }
}
