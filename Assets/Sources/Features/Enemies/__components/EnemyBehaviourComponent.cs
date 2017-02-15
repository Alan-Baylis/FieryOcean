using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Core]
public sealed class EnemyBehaviourComponent : IComponent
{
    //private Movement1 _pathController;
     public void Process(Vector3 playerPosition)
     {
         // Vector3 shipDirect, Rigidbody rb, Vector2 jpystickPos, float lastAc
         //_pathController.Move()
     }

    /* public enum Algorithm
     {
         Angry,
         Retreat
     }*/
    public string id;
}

