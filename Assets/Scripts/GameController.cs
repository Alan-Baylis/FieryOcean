using Entitas;
//using Entitas.Unity.Serialization.Blueprints;
using UnityEngine;
using KBEngine;
using System.Collections;
//using Entitas.Unity.Serialization.Blueprints;

public class GameController : MonoBehaviour {

    //public Blueprints blueprints;
    public Camera cam;
    public PlayerInputController playerInputController;
    public EnemiesStartPositions enemisStartPositions;
    public ProjectorColorController projectorColorController;
    
    Systems _systems;
    // Use this for initialization
    void Start () {
        GameRandom.core = new Rand(0);
        GameRandom.view = new Rand(0);
        //Pools.sharedInstance.core.CreateEntity().AddCamera(cam).AddCameraPosition(cam.transform.position);
        var pools = Contexts.sharedInstance;
        
        //pools.SetAllContexts();
        pools.AddEntityIndices();

        //pools.blueprints.SetBlueprints(blueprints);

        _systems = createSystems(pools);

        _systems.Initialize();

        float selAvatarDBID = PlayerPrefs.GetFloat("selAvatarDBID", 0);

        if(selAvatarDBID!=0)
            KBEngine.Event.fireIn("selectAvatarGame", (ulong)selAvatarDBID);
        
        //else
            // TODO : pring error
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

    Systems createSystems(Contexts contexts)
    {
        //CreateEnemySystem es = new CreateEnemySystem(contexts, new Vector3[] { enemisStartPositions.startPoint.position });
        //es.SetPosition(new Vector3[] { enemisStartPositions.startPoint.position });

        WorldSystem ws = new WorldSystem(contexts);
        ws.SetOcean(enemisStartPositions.startPoint.position.y);

        ISystem playerPositionSystem = null;

        if (KBEngineApp.app == null)
            playerPositionSystem = new PlayerPositionSystemLocal(contexts, playerInputController.joystick, playerInputController.speedMap, playerInputController.Position());
        else
            playerPositionSystem = new PlayerPositionSystem(contexts, playerInputController.joystick, playerInputController.speedMap, playerInputController.Position());


        return new Feature("Systems")
        // Input
        .Add(new InputSystem(playerInputController, contexts))
        .Add(new ProcessMoveInputSystem(contexts))
        .Add(new ShootSystem(contexts))
        // Initialize
        .Add(ws)
        .Add(new AnimateDestroyViewSystem(contexts))
        .Add(new TurnTurretSystem(contexts))
        .Add(new TurretSystem(contexts))
        
        //.Add(new IncrementTickSystem())
        .Add(new CreatePlayerSystem(contexts, playerInputController.Position()))
        .Add(new CreateCameraSystem(contexts, cam))
        .Add(new AddViewSystems(contexts, projectorColorController.projectorColors))
        .Add(new AddViewFromObjectPoolSystem(contexts))

        // Initialize and Reactive
        .Add(new CreateEnemySystem(contexts, new Vector3[] { enemisStartPositions.startPoint.position , new Vector3(200,1,30)}))
        //
        .Add(new CameraSystem(contexts))
        
        //
        .Add(new RenderStartPositionSystem(contexts))
        .Add(new BulletsThrowSystem(contexts))
        .Add(new RenderPositionSystem(contexts))
        .Add(new VelocitySystem(contexts))
        // Update
        //.Add(pools.core.CreateSystem(new BulletCoolDownSystem()))
        //.Add(pools.core.CreateSystem(new StartEnemyWaveSystem()))

        //.Add(new VelocitySystem())
        .Add(new AddPlayerStartPosition(contexts))
        .Add(playerPositionSystem)
        .Add(new AddEnemyStartPositionSystem(contexts))
        .Add(new EnemyPositionSystem(contexts))

        .Add(new ProcessCollisionSystem(contexts))
        .Add(new CheckHealthSystem(contexts))
        // Destroy
        .Add(new DestroyEntitySystem(contexts));
        //.Add(pools.CreateSystem(new DestroyEnemySystem()));
        //.Add(new DestroyRemotePlayerSystem(contexts));
    }
}
