using Entitas;
using Entitas.Unity.Serialization.Blueprints;
using UnityEngine;
using System.Collections;


public class GameController : MonoBehaviour {

    public Blueprints blueprints;
    public Camera cam;
    public PlayerInputController playerInputController;
    public EnemiesStartPositions enemisStartPositions;
    
    Systems _systems;
    // Use this for initialization
    void Start () {
        GameRandom.core = new Rand(0);
        GameRandom.view = new Rand(0);
        //Pools.sharedInstance.core.CreateEntity().AddCamera(cam).AddCameraPosition(cam.transform.position);
        var pools = Contexts.sharedInstance;
       // pools.SetAllContexts();
        //pools.AddEntityIndices();

        //pools.blueprints.SetBlueprints(blueprints);

        _systems = createSystems(pools);

        _systems.Initialize();

        float selAvatarDBID = PlayerPrefs.GetFloat("selAvatarDBID",0);

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

        CreateEnemySystem es = new CreateEnemySystem(contexts);
        es.SetPosition(new Vector3[] { enemisStartPositions.startPoint.position });

        WorldSystem ws = new WorldSystem(contexts);
        ws.SetOcean(enemisStartPositions.startPoint.position.y);


        return new Feature("Systems")
            // Initialize
            .Add(ws)
            .Add(new IncrementTickSystem())
            .Add(new CreatePlayerSystem(playerInputController.Position()))
            .Add(new CreateCameraSystem(cam))
            .Add(new AddViewSystems(contexts))
            .Add(new AddViewFromObjectPoolSystem(contexts))


            // Initialize and Reactive
            .Add(es)

            // Input
            .Add(new InputSystem(playerInputController))
            .Add(new ProcessMoveInputSystem(contexts))
            .Add(new CameraSystem())

            // Update
            //.Add(pools.core.CreateSystem(new BulletCoolDownSystem()))
            //.Add(pools.core.CreateSystem(new StartEnemyWaveSystem()))

            .Add(new VelocitySystem())
            .Add(new AddPlayerStartPosition(contexts))
            .Add(new PlayerPositionSystem(playerInputController.joystick, playerInputController.speedMap, playerInputController.Position()))
            .Add(new AddEnemyStartPositionSystem(contexts))
            .Add(new EnemyPositionSystem(contexts))

            // Destroy
            //.Add(pools.CreateSystem(new DestroyEntitySystem()));
            //.Add(pools.CreateSystem(new DestroyEnemySystem()));
            .Add(new DestroyRemotePlayerSystem(contexts));
    }
}
