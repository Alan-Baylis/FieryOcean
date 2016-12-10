using Entitas;

[Core, Bullets]

public sealed class PlayerViewComponent : IComponent
{
    public IPlayerController controller;
}
