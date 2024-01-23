using AutoMapper;
using DeviceApp.Cache;
using DeviceApp.Repo.Interface;
using EcommerceLib.DTO;
using EcommerceLib.Models.ProductModel;
using Microsoft.EntityFrameworkCore;


namespace DeviceApp.Repo.Classes;

public class ProductСharacteristicRepository: IRepository
{
    private IGenericRepository<ProductСharacteristic, ProductСharacteristicDto> _genericRepository;
    private readonly ICacheService _cacheService;
    private readonly IMapper _mapper;
    public ProductСharacteristicRepository(IGenericRepository<ProductСharacteristic, ProductСharacteristicDto> genericRepository,
                                           ICacheService cacheService, IMapper mapper)
    {
        _genericRepository = genericRepository;
        _cacheService = cacheService;
        _mapper = mapper;
    }


    public async Task<List<KeyValuePair<string, int>>> GetCharacteristicValuesById(int id)
    {
        var productСharacteristics =  await _genericRepository
                                            .GetAllAsync(filter: x => x.CharacteristicId == id);

        Dictionary<string, int>  valueAndCount = new();

        foreach (var productChar in productСharacteristics)
        {
            if(!valueAndCount.Any(x=>x.Key == productChar.Value))
            {
                valueAndCount.Add(key: productChar.Value, value: 1);
            }
            else
            {
                int valueCount = valueAndCount.GetValueOrDefault(productChar.Value);
                valueAndCount[productChar.Value] = ++valueCount;

            }
        }

        return valueAndCount.ToList();

    }
   
}
