using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;

public partial class CollisionSystem : SystemBase
{
    
    private StepPhysicsWorld stepPhysicsWorld;

    private EndSimulationEntityCommandBufferSystem ecbSystem;

    protected override void OnCreate()
    {
        stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
        ecbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }

    [BurstCompile]
    struct CollisionTriggerJob : ITriggerEventsJob
    {

        [ReadOnly] public ComponentDataFromEntity<BulletTag> bullet;
        [ReadOnly] public ComponentDataFromEntity<CanBeDestroyedTag> hitObject;

        public EntityCommandBuffer ecb;
        public void Execute(TriggerEvent triggerEvent)
        {
            if (bullet.HasComponent(triggerEvent.EntityA) && hitObject.HasComponent(triggerEvent.EntityB))
            {
                ecb.DestroyEntity(triggerEvent.EntityA);
                ecb.DestroyEntity(triggerEvent.EntityB);
            }
        }
    }
    
    protected override void OnUpdate()
    {
        CollisionTriggerJob collisionJob = new CollisionTriggerJob()
        {
            bullet = GetComponentDataFromEntity<BulletTag>(true),
            hitObject = GetComponentDataFromEntity<CanBeDestroyedTag>(true),
            ecb = ecbSystem.CreateCommandBuffer()
        };

        Dependency = collisionJob.Schedule(stepPhysicsWorld.Simulation,Dependency);
        
        this.CompleteDependency();
    }
}
