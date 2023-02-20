namespace ControllerBasedWebAPI;

public class FishItemRepo
{
    private Dictionary<Guid, FishItem> _inMemory = new Dictionary<Guid, FishItem>();

    // Create
    public FishItem AddFishItem(FishItem fishItem)
    {
        // Assign a new id
        fishItem.Id = Guid.NewGuid();

        _inMemory.Add(fishItem.Id, fishItem);

        return fishItem;
    }

    // Read
    public FishItem? GetFishItem(Guid id)
    {
        if (_inMemory.ContainsKey(id))
        {
            return _inMemory[id];
        }
        return null;
    }

    public IEnumerable<FishItem> GetFishItems()
        => _inMemory.Values;

    // Update
    public FishItem? Update(FishItem newFishItem)
    {
        if (_inMemory.ContainsKey(newFishItem.Id))
        {
            _inMemory[newFishItem.Id] = newFishItem;
            return newFishItem;
        }
        return null;
    }

    // Delete
    public bool Delete(Guid id)
    {
        if (_inMemory.ContainsKey(id))
        {
            _inMemory.Remove(id);
            return true;
        }
        return false;
    }
}