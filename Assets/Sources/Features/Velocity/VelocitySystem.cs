using Entitas;
using UnityEngine;

public sealed class VelocitySystem : ISetPools, IExecuteSystem {

    //Group[] _movableGroups;

    Group _camera;
    
    public void SetPools(Contexts pools) {
        /*var matcher = Matcher.AllOf(CoreMatcher.Velocity, CoreMatcher.Position);
        _movableGroups = new [] {
            pools.core.GetGroup(matcher),
            pools.bullets.GetGroup(matcher)
        };*/

        var matcher = Matcher.AllOf(CoreMatcher.CameraPosition);
        _camera = pools.core.GetGroup(matcher);
    }

    public void Execute() {
        /*foreach(var group in _movableGroups) {
            foreach(var e in group.GetEntities())
            {
                //var pos = e.position.value;
                //e.ReplacePosition(Vector3.zero);
            }
        }*/
    }
}
