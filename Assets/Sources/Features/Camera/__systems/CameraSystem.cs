using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;
using System;

public sealed class CameraSystem : IExecuteSystem, ICleanupSystem
{
    const string PLAYER_ID = "Player1";

    IGroup<GameEntity> _cameraGroup;
    IGroup<GameEntity> _playerView;

    public CameraSystem(Contexts contexts)
    {
        _cameraGroup = contexts.game.GetGroup(GameMatcher.Camera);
        _playerView = contexts.game.GetGroup(GameMatcher.PlayerView);
    }

    private Transform target;
    private Transform transform;
    private float distance = 70.0f;
    private float height = 45.0f;
    private float rotationDamping=1.5f;
    private float damping=0.5f;

    public void Execute()
    {
        foreach (var e1 in _cameraGroup.GetEntities())
        {
            if (_playerView.count == 0)
                return;

            target = _playerView.GetEntities()[0].playerView.controller.gameObject.transform;
            transform = e1.camera.cam.transform;

            // Early out if we don't have a target
            if (!target)
                return;

            // Calculate the current rotation angles
            var wantedRotationAngle = target.eulerAngles.y;
            var wantedHeight = target.position.y + height;

            var currentRotationAngle = transform.eulerAngles.y;
            var currentHeight = transform.position.y;

            // Damp the rotation around the y-axis
            currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

            // Damp the height
            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, damping * Time.deltaTime);

            // Convert the angle into a rotation
            var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

            // Set the position of the camera on the x-z plane to:
            // distance meters behind the target

            var currentZ = transform.position.z;
            var currentX = transform.position.x;

            transform.position = target.position;
            transform.position -= currentRotation * Vector3.forward * distance;

            var nextZ = Mathf.Lerp(currentZ, transform.position.z, damping * Time.deltaTime);
            var nextX = Mathf.Lerp(currentX, transform.position.x, damping * Time.deltaTime);

            // Set the height of the camera
            //transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
            transform.position = new Vector3(nextX, currentHeight, nextZ);

            // Always look at the target
            transform.LookAt(target);
        }
    }

    public void Cleanup()
    {
       //throw new NotImplementedException();
    }
}
