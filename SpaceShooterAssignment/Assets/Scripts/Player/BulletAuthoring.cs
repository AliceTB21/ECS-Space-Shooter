using Unity.Entities;

[GenerateAuthoringComponent]
public class BulletAuthoring : IComponentData
{
    public Entity bulletPrefab;
}
