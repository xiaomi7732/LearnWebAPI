using System.Net;
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

    // GET http://localhost:5222/fishitems
    [HttpGet]
    public IEnumerable<FishItem> GetAll()
    {
        return _repo.GetFishItems();
    }

    // GET http://localhost:5222/fishitems/{a-guid}
    [Route("{fishId}")]
    [HttpGet]
    [ProducesResponseType(typeof(FishItem), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(NotFoundResult), (int)HttpStatusCode.NotFound)]
    public IActionResult Get([FromRoute] Guid fishId)
    {
        FishItem? fishItem = _repo.GetFishItem(fishId);
        if(fishItem is null)
        {
            return NotFound();
        }
        return Ok(fishItem);
    }

    // POST http://localhost:5222/fishitems with body
    [HttpPost]
    [ProducesResponseType(typeof(FishItem), (int)HttpStatusCode.Created)]
    public ActionResult<FishItem> Add([FromBody] FishItem fishItem)
    {
        FishItem newItem = _repo.AddFishItem(fishItem);
        return CreatedAtAction(nameof(Get), new { fishId = newItem.Id }, newItem);
    }

    // PUT http://localhost:5222/fishitems/{a-guid}
    [HttpPut("{fishId}")]
    public FishItem Update(Guid fishId, FishItem newItem)
    {
        newItem.Id = fishId;
        return _repo.Update(newItem);
    }

    // DELETE http://localhost:5222/fishitems/{a-guid}
    [HttpDelete("{fishId}")]
    public void Delete(Guid fishId)
    {
        _ = _repo.Delete(fishId);
    }

    // [HttpGet]
    // [Route("/forwards/{*value}")]
    // public IActionResult SlugDemo(string value)
    // {
    //     return Ok(value);
    // }
}
