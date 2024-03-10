using UnityEngine;

[System.Serializable]
public class WaveData
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private int amount;
    [SerializeField]
    private float spawnRate;

    public GameObject EnemyPrefab
    {
        get { return enemyPrefab; }
        set { enemyPrefab = value; }
    }
    public int Amount
    {
        get { return amount;}
        set { amount = value; }
    }
    public float SpawnRate
    {
        get { return spawnRate; }
        set { spawnRate = value; }
    }
}
