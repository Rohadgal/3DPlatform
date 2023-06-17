using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] int objectPoolLimit;
    [SerializeField] GameObject platformPrefab;
    [SerializeField] GameObject enemyPrefab;
    // [SerializeField] GameObject enemyPrefab;

    public static PlatformSpawner s_instance;

    Queue<GameObject> platformPool;
    bool canInstantiate = true;
    float lastZPosition;
    float zDisplacementValue = 10;
    float minX = -10;
    float maxX = 10;

    void Start()
    {
        s_instance = this;
        platformPool = new Queue<GameObject>();
        if( platformPrefab != null ) {
            lastZPosition = -zDisplacementValue;
            StartCoroutine(spawnPlatform());
        }
    }

    IEnumerator spawnPlatform() {
        int randEnemySpawn;

        while (GameManager.s_instance.getGameState() != GameState.GameOver) {
            if (GameManager.s_instance.getGameState() == GameState.GameFinished) {
                break;
            }
            float rndX = Random.Range(minX, maxX);
            Vector3 newPos = new Vector3(rndX, 2, lastZPosition + zDisplacementValue);  // creamos la nueva posición
            Debug.Log("creamos newPos: " + newPos);

            yield return new WaitForSeconds(LevelManager.s_instance.getTime());

            if(canInstantiate) {
               platformPool.Enqueue(Instantiate(platformPrefab, newPos, Quaternion.identity));
                randEnemySpawn = Random.Range(1, 6);
                Debug.Log("rand: " + randEnemySpawn);

                if(randEnemySpawn < 3) {
                    Instantiate(enemyPrefab, newPos, Quaternion.identity);
                    enemyPrefab.SetActive(true);
                }
  
                lastZPosition = newPos.z;         // uso de la nueva posición
               if(platformPool.Count > objectPoolLimit ) {
                    canInstantiate = false; 
               }
            }
            else {
                GameObject tempGO = platformPool.Dequeue();
                //rndX = Random.Range(minX, maxX);
                tempGO.transform.position = new Vector3(rndX, newPos.y, newPos.z);
                lastZPosition = tempGO.transform.position.z;

                Debug.Log("esta posición ahora: " + tempGO.transform.position);
                //tempGO.SetActive(true);
                platformPool.Enqueue(tempGO);
                randEnemySpawn = Random.Range(1, 6);
                if (randEnemySpawn < 3) {
                    Instantiate(enemyPrefab, newPos, Quaternion.identity);
                    enemyPrefab.SetActive(true);
                }
            }
        }
    }

    public int getPoolLimit () {
        return objectPoolLimit;
    }

    
}
