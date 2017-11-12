using Entitas;
using UnityEngine;

[Bullets]
public sealed class BulletComponent : IComponent {
    public float firingAngle; // barrel firing angel
    public float gravity;
    public float speed; // magnitude of speed vector
    public Vector3 position;
    public Vector3 target;

    public float flightDuration;
    public float Vx;
    public float Vy;
    public bool flagDestroy;
}
