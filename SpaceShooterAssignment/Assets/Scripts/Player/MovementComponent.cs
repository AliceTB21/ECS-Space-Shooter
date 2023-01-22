using Unity.Entities;
using UnityEngine;

[GenerateAuthoringComponent]
public struct MovementComponent : IComponentData
{
    public float movement;
    public float movementSpeed;
    public float rotation;
    public float rotationSpeed;
}
