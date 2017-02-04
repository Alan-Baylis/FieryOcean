using Entitas;
using Entitas.Unity.Serialization.Blueprints;
using UnityEngine;
using System.Collections;


public class GameController : MonoBehaviour {

    public Blueprints blueprints;
    public Camera cam;
    public PlayerInputController playerControlController;
    Systems _systems;
    // Use this for initialization
    void Start () {
        GameRandom.core = new Rand(0);
        GameRandom.view = new Rand(0);
        //Pools.sharedInstance.core.CreateEntity().AddCamera(cam).AddCameraPosition(cam.transform.position);
        var pools = Pools.sharedInstance;
        pools.SetAllPools();
        pools.AddEntityIndices();

        pools.blueprints.SetBlueprints(blueprints);

        _systems = createSystems(pools);

        _systems.Initialize();
    }
	
	// Update is called once per frame
	void Update () {
        _systems.Execute();
        _systems.Cleanup();
    }

    void OnDestroy()
    {
        _systems.TearDown();
    }

    Systems createSystems(Pools pools)
    {
        return new Feature("Systems")
             // Initialize
            .Add(pools.core.CreateSystem(new CreateCameraSystem(cam)))
            .Add(pools.CreateSystem(new IncrementTickSystem()))
            .Add(pools.CreateSystem(new CreatePlayerSystem()))
            .Add(pools.core.CreateSystem(new AddViewSystem()))
            .Add(pools.bullets.CreateSystem(new AddViewFromObjectPoolSystem()))
           
            // Input
            .Add(pools.CreateSystem(new InputSystem(playerControlController)))
            .Add(pools.input.CreateSystem(new ProcessMoveInputSystem()))
            .Add(pools.core.CreateSystem(new CameraSystem()))
            
            // Update
            //.Add(pools.core.CreateSystem(new BulletCoolDownSystem()))
            //.Add(pools.core.CreateSystem(new StartEnemyWaveSystem()))

            .Add(pools.CreateSystem(new VelocitySystem()))
            .Add(pools.CreateSystem(new RenderPositionSystem(playerControlController.joystick, playerControlController.speedMap)))
            .Add(pools.core.CreateSystem(new AddRigidbodySystem()))
            // Destroy
            .Add(pools.CreateSystem(new DestroyEntitySystem()));
    }
}
