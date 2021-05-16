using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnEnemies : MonoBehaviour
{

    public GameObject theEnemy;
    public int zombiesAlive = 1;
    public int maxNumEnemiesOnMap = 10;
    public int zombiesToSpawn = 1;
    public int numberOfSecondsToWait = 5;
    public TerrainCollider terrain;
    [SerializeField] TextMeshProUGUI zombiesDisplay;
    [SerializeField] Sun sun;
    [SerializeField] Transform zombiesParentObj;
    void Start()
    {
        StartCoroutine(EnemyDrop());
    }

    Vector3 GenerateSpawnPoint()
    {
        RaycastHit hit;
        Vector3 vec = new Vector3(Random.Range(0, 990), 300, Random.Range(0, 990));
        if (Physics.Raycast(vec, -Vector3.up, out hit))
        {
            vec.y = hit.point.y;
        }
        return vec;
    }
    private void Update()
    {
        int numHunting = 0;
        zombiesAlive = 0;
        ZombieAI[] allZombmies= FindObjectsOfType<ZombieAI>();
        foreach (ZombieAI z in allZombmies)
        {
            if (z.isProvoked && !z.health.IsDead())
            {
                numHunting++;
            }
            if (!z.health.IsDead())
            {
                zombiesAlive++;
            }
        }
        if (sun.isDay)
        {
            numHunting = 0;
        }
        zombiesDisplay.text = zombiesAlive.ToString() + " Alive, " + numHunting.ToString() + " Hunting, "
            + zombiesToSpawn.ToString() + " not spawned";

        if (zombiesToSpawn == 0 && zombiesAlive == 0)
        {
            // if player beat level 1
            FindObjectOfType<SceneLoader>().LevelComplete();
        }
    }

    IEnumerator EnemyDrop()
    {
        // choose a random spawnpoint
        

        while (zombiesAlive < maxNumEnemiesOnMap && zombiesToSpawn > 0)
        {
            if (sun.isDay)
            {
                yield return new WaitForSeconds(numberOfSecondsToWait);
            }
            Vector3 spawn_pos = GenerateSpawnPoint();
            GameObject newZombie = Instantiate(theEnemy, spawn_pos, Quaternion.identity);
            newZombie.transform.SetParent(zombiesParentObj);
            yield return new WaitForSeconds(numberOfSecondsToWait);
            zombiesToSpawn--;
        }
    }

}
