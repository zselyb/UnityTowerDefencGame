using System.Collections;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    private static int aliveEnemyCount = 0;

    [SerializeField]
    private Transform spawner;
    [SerializeField]
    private int startTime;
    [SerializeField]
    private TextMeshProUGUI countDownText;
    [SerializeField]
    private WaveData[] waves;
    [SerializeField]
    private GameLogic gameLogic;
    private float waveTimer;
    private int waveIndex = 0;

    void Start()
    {
        aliveEnemyCount = 0;
        waveTimer = startTime;
    }

    public static int AliveEnemyCount
    {
        get { return aliveEnemyCount; }
        set { aliveEnemyCount = value; }
    }

    void Update()
    {
        if(aliveEnemyCount > 0)
        {
            return;
        }

        if (waveIndex == waves.Length)
        {
            gameLogic.WinGame();
            this.enabled = false;
        }

        if (waveTimer <= 0)
        {
            StartCoroutine(sendWave());
            waveTimer = startTime;
        }
        else
        {
            waveTimer -= Time.deltaTime;
        }

        waveTimer = Mathf.Clamp(waveTimer, 0f, Mathf.Infinity);
        countDownText.text = $"{waveTimer:00.00}";
    }

    private IEnumerator sendWave()
    {
        WaveData currentWave = waves[waveIndex];
        aliveEnemyCount = currentWave.Amount;

        for(int i = 0; i < currentWave.Amount; i++)
        {
            SpawnEnemy(currentWave.EnemyPrefab);
            yield return new WaitForSeconds(1.0f / currentWave.SpawnRate);
        }

        waveIndex++;
        PlayerData.RoundsSurvived = waveIndex;
    }

    private void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawner.position, spawner.rotation);
    }
}
