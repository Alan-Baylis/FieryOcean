using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;

class AddRigidbodySystem : IReactiveSystem, ISetPool
{
    Pool _pools;
    Group _moveInputs;

    public TriggerOnEvent trigger { get { return CoreMatcher.Rigidbody.OnEntityAdded(); } }

    public void Execute(List<Entity> entities)
    {
        /*foreach( var e in entities )
        {
            e.rigidbody.rigidbody.AddForce(UnityEngine.Vector3.zero);
        }*/
    }

    public void SetPool(Pool pool)
    {
        //Matcher.AllOf(CoreMatcher.Velocity, CoreMatcher.Position);
        _pools = pool;
        //_moveInputs = pool.GetGroup(InputMatcher.MoveInput);
    }
}

