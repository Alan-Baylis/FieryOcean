using System;
using Entitas;

[Game, Bullets]

public sealed class PlayerViewComponent : IComponent
{
    public IPlayerController controller;
  
}
