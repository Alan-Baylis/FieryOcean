using Entitas;
using UnityEngine;

public sealed class CreatePlayerSystem : ISetPools, IInitializeSystem {

    Pools _pools;

    public void SetPools(Pools pools) {
        _pools = pools;
    }

    public void Initialize() {
        Vector3 pos = Vector3.zero;
        pos.y = 2.22f;
        _pools.blueprints.blueprints.instance
              .ApplyPlayer1(_pools.core.CreateEntity(), pos);
    }
}
