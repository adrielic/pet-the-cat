using System.Collections;
using UnityEngine;

public class ObstacleSpawn : MonoBehaviour
{
    public GameObject obstaclePrefab;

    void Start()
    {
        StartCoroutine(SpawnObstacles());
    }

    private IEnumerator SpawnObstacles()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1f, 3f));

            Instantiate(obstaclePrefab);
        }
    }
}
