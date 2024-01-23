namespace DeviceApp.Services;

public class PageList<T>
{
    private int itemTakeCount;

    public ICollection<T> Items { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalItems / itemTakeCount);

    public int ItemTakeCount 
    { 
        get => itemTakeCount * CurrentPage; 
        set => itemTakeCount = value;
    }
    public int TotalItems { get => Items.Count; }

  

    public List<T> Get(List<T> item, int page, int takeCount) 
    {
        CurrentPage = page;
        Items = item;
        ItemTakeCount = takeCount;

        if (ItemTakeCount < TotalItems)
        {
            return Items.Take(ItemTakeCount)
                        .Skip(ItemTakeCount - ItemTakeCount / CurrentPage)
                        .ToList();
        }

        int skip = itemTakeCount * (CurrentPage - 1);
        return Items
                .Take(ItemTakeCount)
                .Skip(skip)
                .ToList();

    }

}
