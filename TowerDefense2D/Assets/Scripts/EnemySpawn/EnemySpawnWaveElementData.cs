using Unity.Entities;

public struct EnemySpawnWaveElementData : IBufferElementData
{
    public Entity enemyPrefab;
    public int enemyCount;

    public float delayTime;
    public float intervalTime;
}