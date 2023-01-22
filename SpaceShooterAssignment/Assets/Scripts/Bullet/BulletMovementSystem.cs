using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class BulletMovementSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.WithoutBurst().ForEach((ref Translation translation, ref BulletTag bulletTag) =>
        {
            float3 newPos;
            newPos = bulletTag.direction * bulletTag.bulletSpeed * World.Time.DeltaTime;
            translation.Value += newPos;
        }).Run();

    }
}
