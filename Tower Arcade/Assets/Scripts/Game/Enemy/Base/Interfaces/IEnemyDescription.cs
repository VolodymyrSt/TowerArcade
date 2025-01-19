using Game;

public interface IEnemyDescription
{
    public float GetCurrentHealth();
    public float GetSoulCost();
    public string GetEnemyName();
    public string GetEnemyDescription();
    public EnemyType GetEnemyType();
    public EnemyRank GetEnemyRank();
}
