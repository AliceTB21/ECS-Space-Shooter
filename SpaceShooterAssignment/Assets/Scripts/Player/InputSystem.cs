using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class InputSystem : SystemBase
{
    private EntityCommandBufferSystem ecbSystem;

    private float shootCooldown = 0.2f;
    public float shootTimer;

    protected override void OnCreate()
    {
        ecbSystem = World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        shootTimer -= World.Time.DeltaTime;

        Entities.WithName("MovementComponent").ForEach((ref MovementComponent movementComp) =>
        {
            movementComp.rotation = x;
            movementComp.movement = z;
        }).Schedule();

        if (Input.GetKey(KeyCode.Space) && shootTimer <= 0f)
        {
            shootTimer = shootCooldown;
            Entities.WithoutBurst().WithAny<PlayerTag>().ForEach((BulletAuthoring bulletComponent, Transform position) =>
                {
                    Debug.Log("Shot");
                    var commandBuffer = ecbSystem.CreateCommandBuffer();
                    Entity spawnedEntity = commandBuffer.Instantiate(bulletComponent.bulletPrefab);

                    var forwardPos = position.position + position.forward * 1f;

                    Translation pos = new Translation { Value = new float3(forwardPos) };
                    commandBuffer.SetComponent(spawnedEntity,pos);
                    commandBuffer.SetComponent(spawnedEntity, new BulletTag{direction = position.forward, bulletSpeed = 20f});
                }).Run();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
