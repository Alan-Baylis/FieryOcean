using Entitas;
using UnityEngine;

public sealed class CreatePlayerSystem : ISetPools, IInitializeSystem {
    private Vector3 _position;
    Pools _pools;

    public void SetPools(Pools pools) {
        _pools = pools;
    }
    public CreatePlayerSystem(Vector3 playerStartPosition)
    {
        _position = playerStartPosition;
    }

    public void Initialize() {
        _pools.blueprints.blueprints.instance
              .ApplyPlayer1(_pools.core.CreateEntity(), _position);
    }
}
