using Unity.Entities;
using UnityEngine;

[GenerateAuthoringComponent]
public struct BulletTag : IComponentData
{
    public float bulletSpeed;
    public Vector3 direction;
}
