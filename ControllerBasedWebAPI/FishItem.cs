namespace ControllerBasedWebAPI;

public class FishItem
{
    public Guid Id { get; set; } = Guid.Empty;

    public string Name { get; set; } = default!;

    public int RecommendationLevel { get; set; } = 2;
}