using Microsoft.AspNetCore.Mvc;

namespace ControllerBasedWebAPI;

[ApiController]
[Route("[controller]")]
public class FishItemsController : ControllerBase
{
    private readonly FishItemRepo _repo;

    public FishItemsController(FishItemRepo repo)
    {
        _repo = repo ?? throw new ArgumentNullException(nameof(repo));
    }

    // GET http://localhost:5222
    [HttpGet]
    public IEnumerable<FishItem> GetAll()
    {
        return _repo.GetFishItems();
    }

    // GET http://localhost:5222/{a-guid}
    [HttpGet("{fishId}")]
    public FishItem Get([FromRoute] Guid fishId)
    {
        return _repo.GetFishItem(fishId);
    }

    // POST http://localhost:5222/ with body
    [HttpPost]
    public ActionResult<FishItem> Add([FromBody] FishItem fishItem)
    {
        FishItem newItem = _repo.AddFishItem(fishItem);
        return CreatedAtAction(nameof(Get), new { fishId = newItem.Id }, newItem);
    }

    [HttpPut("{fishId}")]
    public FishItem Update(Guid fishId, FishItem newItem)
    {
        newItem.Id = fishId;
        return _repo.Update(newItem);
    }

    [HttpDelete("{fishId}")]
    public void Delete(Guid fishId)
    {
        _ = _repo.Delete(fishId);
    }
}