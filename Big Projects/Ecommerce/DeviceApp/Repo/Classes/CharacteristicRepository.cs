using DeviceApp.Repo.Interface;
using EcommerceLib.DTO;
using EcommerceLib.Models.ProductModel;


namespace DeviceApp.Repo.Classes;

public class CharacteristicRepository: IRepository
{
    IGenericRepository<Characteristic, CharacteristicDto> _genericRepository;

    public CharacteristicRepository(IGenericRepository<Characteristic, CharacteristicDto> genericRepository)
    {
        _genericRepository = genericRepository;
    }


    public async Task<IEnumerable<Characteristic>> GetAllCharacteristicsBySubCategoryId(int id)
    {
        return await _genericRepository.GetAllAsync(filter: x => x.SubCategoryId == id);
    }
}
