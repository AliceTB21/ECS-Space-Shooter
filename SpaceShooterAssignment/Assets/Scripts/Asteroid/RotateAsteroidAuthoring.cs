using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class RotateAsteroidAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    [SerializeField] private float degreesPerSecond;
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new RotateAsteroid { radiansPerSecond = math.radians(degreesPerSecond) });
        dstManager.AddComponentData(entity, new RotationEulerXYZ());
    }
}
