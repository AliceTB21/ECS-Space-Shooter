using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public partial class MovementSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;
        
        Entities.WithoutBurst().ForEach((Transform transform, ref MovementComponent movementComponent) =>
        {
            float x = movementComponent.rotation * movementComponent.rotationSpeed * deltaTime;
            float z = movementComponent.movement * movementComponent.movementSpeed * deltaTime;

            transform.position += transform.forward * z;
            transform.localEulerAngles += new Vector3(0,x,0);
        }).Run();
    }
}
