using Entitas;

public sealed class IncrementTickSystem : IInitializeSystem, IExecuteSystem {

    Contexts _pools;

    // TODO Entitas 0.36.0 Migration (constructor)
    public void SetPools(Contexts pools) {
        _pools = pools;
    }

    public void Initialize() {
        _pools.input.SetTick(0);
    }

    public void Execute() {
        _pools.input.ReplaceTick(_pools.input.tick.value + 1);
    }
}
