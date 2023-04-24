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
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public ActionResult<FishItem> Get([FromRoute] Guid fishId)
    {
        FishItem? fishItem = _repo.GetFishItem(fishId);
        if (fishItem is null)
        {
            return NotFound();
        }
        // fishItem will be implicitly converted to ActionResult<FishItem>.
        return fishItem;
    }

    // POST http://localhost:5222/fishitems with body
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    public ActionResult<FishItem> Add([FromBody] FishItem fishItem)
    {
        FishItem newItem = _repo.AddFishItem(fishItem);
        return CreatedAtAction(nameof(Get), new { fishId = newItem.Id }, newItem);
    }

    // PUT http://localhost:5222/fishitems/{a-guid}
    [HttpPut("{fishId}")]
    public ActionResult<FishItem> Update(Guid fishId, FishItem newItem)
    {
        newItem.Id = fishId;
        FishItem? updatedFishItem = _repo.Update(newItem);
        if(updatedFishItem is null)
        {
            return BadRequest();
        }
        return updatedFishItem;
    }

    // DELETE http://localhost:5222/fishitems/{a-guid}
    [HttpDelete("{fishId}")]
    public ActionResult Delete(Guid fishId)
    {
        if(_repo.Delete(fishId))
        {
            return NoContent();
        }
        return NotFound();
    }

    // [HttpGet]
    // [Route("/forwards/{*value}")]
    // public IActionResult SlugDemo(string value)
    // {
    //     return Ok(value);
    // }
}
