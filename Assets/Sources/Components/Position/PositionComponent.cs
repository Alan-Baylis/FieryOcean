using Entitas;
using UnityEngine;

[Game, Bullets]
public sealed class PositionComponent : IComponent {
    public Vector3 value;
}
