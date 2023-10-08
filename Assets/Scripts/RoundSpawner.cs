using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Script started out from a YouTube tutorial but has been iterated on as the game was developed.
//Tutorial: https://www.youtube.com/watch?v=beuoNuK2tbk&list=PLPV2KyIb3jR4u5jX8za5iU1cqnQPmbzG0


public class RoundSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameController gameController;
    [SerializeField] RoundControls roundControls;
    [SerializeField] RoundInfo roundInfo;

    [Header("Enemy Spawns")]
    [SerializeField] bool isDualSpawns = false;
    public Transform spawnPointV1;
    public Transform spawnPointV2;
    private Transform currentSpawnPoint;
    public static int currentSpawn = 1;

    [Header("Enemy Prefabs")]
    public GameObject standardEnemy;
    public GameObject lightEnemy;
    public GameObject tankEnemy;
    public GameObject flyingEnemy;

    [Header("Round Status")]
    public static int enemiesAlive = 0;
    private bool roundFinished = true;

    [Header("Round Setup")]
    public Round[] rounds;
    private int roundIndex = 0;

    private int totalEnemies = 0;
    private int standardEnemies = 0;
    private int lightEnemies = 0;
    private int tankEnemies = 0;
    private int flyingEnemies = 0;

    private int nextStandardEnemies, nextLightEnemies, nextTankEnemies, nextFlyingEnemies = 0;

    private int random;
    private int lastRandom;

    private int enemyIndex;

    private bool doOnce = false;

    private void Start()
    {
        currentSpawnPoint = spawnPointV1;
        enemiesAlive = 0;
        
        if (SceneManager.GetActiveScene().name != "Tutorial")
        {
            UpdateRoundInfo(rounds[0]);
        }
    }

    private void Update()
    {
        if (roundIndex == rounds.Length && enemiesAlive <= 0 && SceneManager.GetActiveScene().name != "Tutorial")
        {
            gameController.WinLevel();
            this.enabled = false;
        }

        if (roundFinished && enemiesAlive <= 0)
        {
            roundControls.ResetButtons();

            if (roundIndex != rounds.Length)
            {
                nextStandardEnemies = nextLightEnemies = nextTankEnemies = nextFlyingEnemies = 0;
                UpdateRoundInfo(rounds[roundIndex]);

                if (roundIndex > 0 && doOnce)
                {
                    GameStats.currentUpgrades += rounds[roundIndex - 1].upgrades;
                    doOnce = false;
                }
            }
        }      
    }

    public void SpawnEnemies()
    {
        if (roundIndex >= rounds.Length)
        {
            return;
        }

        if (roundFinished)
        {
            GameStats.roundReached++;
            StartCoroutine(SpawnRound(rounds[roundIndex]));
        }
    }

    IEnumerator SpawnRound(Round currentRound)
    {
        GameStats.wavesCompleted++;

        for (int i = 0; i < currentRound.waves.Length; i++)
        {
            float spawnRate = currentRound.waves[i].spawnRate;

            standardEnemies = rounds[roundIndex].waves[i].standardAmount;
            lightEnemies = rounds[roundIndex].waves[i].lightAmount;
            tankEnemies = rounds[roundIndex].waves[i].tankAmount;
            flyingEnemies = rounds[roundIndex].waves[i].flyingAmount;

            totalEnemies = standardEnemies + lightEnemies + tankEnemies + flyingEnemies;

            enemiesAlive += totalEnemies;

            yield return new WaitForSeconds(1f * currentRound.waves[i].startDelay);

            //Randomises which enemy will spawn next in the group
            while (totalEnemies > 0)
            {
                random = Random.Range(1, 5);

                while (random == lastRandom)
                {
                    random = Random.Range(1, 5);

                    while (random == 1 && standardEnemies <= 0)
                    {
                        random = Random.Range(1, 5);
                    }
                    while (random == 2 && lightEnemies <= 0)
                    {
                        random = Random.Range(1, 5);
                    }
                    while (random == 3 && tankEnemies <= 0)
                    {
                        random = Random.Range(1, 5);
                    }
                    while (random == 4 && flyingEnemies <= 0)
                    {
                        random = Random.Range(1, 5);
                    }
                    while (random == 5)
                    {
                        random = Random.Range(1, 5);
                    }
                }

                lastRandom = random;

                //Spawns the random enemy

                //Standard Enemy
                if (random == 1 && standardEnemies > 0)
                {
                    standardEnemies--;
                    totalEnemies--;
                    SpawnEnemy(standardEnemy);
                    yield return new WaitForSeconds(1f * spawnRate);
                }
                //Light Enemy
                else if (random == 2 && lightEnemies > 0)
                {
                    lightEnemies--;
                    totalEnemies--;
                    SpawnEnemy(lightEnemy);
                    yield return new WaitForSeconds(1f * spawnRate);
                }
                //Tank Enemy
                else if (random == 3 && tankEnemies > 0)
                {
                    tankEnemies--;
                    totalEnemies--;
                    SpawnEnemy(tankEnemy);
                    yield return new WaitForSeconds(1f * spawnRate);
                }
                //Flying Enemy
                else if (random == 4 && flyingEnemies > 0)
                {
                    flyingEnemies--;
                    totalEnemies--;
                    SpawnEnemy(flyingEnemy);
                    yield return new WaitForSeconds(1f * spawnRate);
                }
            }
        }

        roundIndex++;

        roundFinished = true;
        doOnce = true;
    }

    private void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, currentSpawnPoint.position, currentSpawnPoint.rotation);

        Enemy currentEnemy = enemy.GetComponent<Enemy>();

        currentEnemy.SetValue(rounds[roundIndex].enemyValue);
        currentEnemy.SetIndex(enemyIndex);
        enemyIndex++;

        if (isDualSpawns)
        {
            if (currentSpawnPoint == spawnPointV1)
            {
                currentSpawnPoint = spawnPointV2;
                currentSpawn = 1;
            }
            else if (currentSpawnPoint == spawnPointV2)
            {
                currentSpawnPoint = spawnPointV1;
                currentSpawn = 2;
            }
        }
    }

    public bool GetRoundFinished()
    {
        if (enemiesAlive <= 0)
        {
            return roundFinished;
        }
        return false;
    }

    public int GetRoundIndex()
    {
        return roundIndex;
    }

    private void UpdateRoundInfo(Round currentRound)
    {
        for (int i = 0; i < currentRound.waves.Length; i++)
        {
            nextStandardEnemies += currentRound.waves[i].standardAmount;
            nextLightEnemies += currentRound.waves[i].lightAmount;
            nextTankEnemies += currentRound.waves[i].tankAmount;
            nextFlyingEnemies += currentRound.waves[i].flyingAmount;
        }

        if (nextStandardEnemies > 0)
            roundInfo.isStandard = true;
        else
            roundInfo.isStandard = false;

        if (nextLightEnemies > 0)
            roundInfo.isLight = true;
        else
            roundInfo.isLight = false;

        if (nextTankEnemies > 0)
            roundInfo.isTank = true;
        else
            roundInfo.isTank = false;

        if (nextFlyingEnemies > 0)
            roundInfo.isFlying = true;
        else
            roundInfo.isFlying = false;

        roundInfo.SetEnemies();
    }
}
