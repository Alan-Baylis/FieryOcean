using Entitas;
using UnityEngine;

[Bullets]
public sealed class BulletComponent : IComponent {
    public float firingAngle;
    public float gravity;
    public Vector3 position;
    public Vector3 target;

    public float flightDuration;
    public float Vx;
    public float Vy;
    
}
