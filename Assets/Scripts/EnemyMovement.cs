using UnityEngine;

//Script started out from a YouTube tutorial but has been iterated on as the game was developed.
//Tutorial: https://www.youtube.com/watch?v=beuoNuK2tbk&list=PLPV2KyIb3jR4u5jX8za5iU1cqnQPmbzG0


[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private int waypointIndex = 0;

    private Enemy enemy;

    private int currentSpawn = 1;

    [SerializeField] bool flyingEnemy = false;
    [SerializeField] Transform end;

    private void Start()
    {
        enemy = GetComponent<Enemy>();

        currentSpawn = RoundSpawner.currentSpawn;

        end = GameObject.FindGameObjectWithTag("End").transform;

        if (flyingEnemy)
        {
            if (currentSpawn == 1)
                target = WaypointsV1.waypoints[WaypointsV1.waypoints.Length - 1];
            else if (currentSpawn == 2)
                target = WaypointsV2.waypoints[WaypointsV2.waypoints.Length - 1];

        }
        else
        {
            if (currentSpawn == 1)
            {
                target = WaypointsV1.waypoints[0];
            }
            else if (currentSpawn == 2)
            {
                target = WaypointsV2.waypoints[0];
            }
        }
    }

    private void Update()
    {
        if (flyingEnemy)
        {
            MoveEnemyFlying();
            return;
        }


        if (currentSpawn == 1)
        {
            MoveEnemyV1();
        }
        else if (currentSpawn == 2)
        {
            MoveEnemyV2();
        }
    }

    private void MoveEnemyV1()
    {
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * enemy.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypointV1();
        }

        enemy.speed = enemy.startSpeed;
    }

    private void MoveEnemyV2()
    {
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * enemy.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypointV2();
        }

        enemy.speed = enemy.startSpeed;
    }

    private void MoveEnemyFlying()
    {
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * enemy.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            ReachedEnd();
        }
    }

    private void GetNextWaypointV1()
    {
        if (flyingEnemy)
        {
            return;
        }

        if (waypointIndex >= WaypointsV1.waypoints.Length - 1)
        {
            ReachedEnd();
            return;
        }

        waypointIndex++;
        target = WaypointsV1.waypoints[waypointIndex];
    }

    private void GetNextWaypointV2()
    {
        if (flyingEnemy)
        {
            return;
        }

        if (waypointIndex >= WaypointsV2.waypoints.Length - 1)
        {
            ReachedEnd();
            return;
        }

        waypointIndex++;
        target = WaypointsV2.waypoints[waypointIndex];
    }

    private void ReachedEnd()
    {
        RoundSpawner.enemiesAlive--;
        GameStats.totalEnemiesAtEnd++;

        if (enemy.enemyType == "Light")
            GameStats.lightEnemiesAtEnd++;
        else if (enemy.enemyType == "Standard")
            GameStats.standardEnemiesAtEnd++;
        else if (enemy.enemyType == "Tank")
            GameStats.tankEnemiesAtEnd++;
        else if (enemy.enemyType == "Flying")
            GameStats.flyingEnemiesAtEnd++;

        GameStats.healthRemaining--;
        GameStats.healthLost++;

        Destroy(gameObject);
    }
}
