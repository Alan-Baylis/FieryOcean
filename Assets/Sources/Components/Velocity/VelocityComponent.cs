using Entitas;
using UnityEngine;

[Game, Bullets]
public sealed class VelocityComponent : IComponent {
    public Vector3 value;
    public float vX;
    public float vY;
}
