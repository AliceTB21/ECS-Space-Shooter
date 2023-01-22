using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[GenerateAuthoringComponent]
public struct AsteroidAuthoringComponent : IComponentData
{
    public Entity asteroidPrefab;
    public int asteroidAmount;
}
