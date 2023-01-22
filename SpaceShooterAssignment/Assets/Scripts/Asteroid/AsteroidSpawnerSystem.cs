using System.Diagnostics;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial class AsteroidSpawnerSystem : SystemBase
{
    private EntityQuery asteroidQuery;

    private BeginSimulationEntityCommandBufferSystem beginECB;

    private Entity asteroidPrefab;

    protected override void OnCreate()
    {
        asteroidQuery = GetEntityQuery(ComponentType.ReadWrite<AsteroidTag>());

        beginECB = this.World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>();
        
    }

    protected override void OnUpdate()
    {

        if (asteroidPrefab == Entity.Null)
        {
            asteroidPrefab = GetSingleton<AsteroidAuthoringComponent>().asteroidPrefab;
            return;
        }

        var commandBuffer = beginECB.CreateCommandBuffer();

        int count = asteroidQuery.CalculateEntityCountWithoutFiltering();

        Entity aPrefab = asteroidPrefab;
        
        var rand = new Random((uint)Stopwatch.GetTimestamp());

        Job.WithCode(() =>
        {
            for (int i = count; i < 1000; i++)
            {
                float3 spawnPos;
                spawnPos.x = rand.NextFloat(-1f*((800)/2-0.1f), (800)/2-0.1f);
                spawnPos.y = 0;
                spawnPos.z = rand.NextFloat(-1f*((800)/2-0.1f), (800)/2-0.1f);
                Translation pos = new Translation { Value = new float3(spawnPos) };

                var spawnedEntity = commandBuffer.Instantiate(aPrefab);
                
                commandBuffer.SetComponent(spawnedEntity,pos);
            }

        }).Schedule();
        
        beginECB.AddJobHandleForProducer(Dependency);

    }
}
