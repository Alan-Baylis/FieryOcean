using Entitas;
using Entitas.Utils;
using UnityEngine;

[Game, Bullets]
public sealed class ViewObjectPoolComponent : IComponent {

    public ObjectPool<GameObject> pool;
}
