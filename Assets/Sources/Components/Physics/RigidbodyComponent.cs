using UnityEngine;
using System.Collections;
using Entitas;

[Game, Input]
public sealed class RigidbodyComponent : IComponent
{
    public Rigidbody rigidbody;
	
}
