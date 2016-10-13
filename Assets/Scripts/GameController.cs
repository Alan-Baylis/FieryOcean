using Entitas;
using Entitas.Unity.Serialization.Blueprints;
using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public Blueprints blueprints;

    Systems _systems;
    // Use this for initialization
    void Start () {

        var pools = Pools.sharedInstance;
        pools.SetAllPools();
        pools.AddEntityIndices();

        //pools.blueprints.SetBlueprints(blueprints);

        _systems = createSystems(pools);

        

        _systems.Initialize();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDestroy()
    {
        _systems.TearDown();
    }

    Systems createSystems(Pools pools)
    {
        return new Feature("Systems");

            // Initialize
           
            

           

            // Destroy
            
    }
}
