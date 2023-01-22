using Unity.Entities;
using Unity.Transforms;
using Unity.Jobs;


public class RotateAsteroidSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref RotateAsteroid rotatingAsteroid, ref RotationEulerXYZ euler) =>
        {
            euler.Value.y += rotatingAsteroid.radiansPerSecond * Time.DeltaTime;
            euler.Value.x += rotatingAsteroid.radiansPerSecond / 2 * Time.DeltaTime;
        });
    }
}
