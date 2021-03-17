using Unity.Entities;

[GenerateAuthoringComponent]
public struct EnemyAttackComponent : IComponentData
{
    public int damage;
    public float range;
    public float interval;

    internal float timeUntilReloaded;
}
