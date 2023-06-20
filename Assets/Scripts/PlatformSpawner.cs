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
    float lastXposition;
    float DisplacementValue = 10;
    float min = -10;
    float max = 10;
    Vector3 newPos;
    Vector3 lastPosition;
    float rndX;
    float rndZ;


    void Start()
    {
        s_instance = this;
        platformPool = new Queue<GameObject>();
        if( platformPrefab != null ) {
            lastZPosition = -DisplacementValue;
            StartCoroutine(spawnPlatform());
        }
    }

    IEnumerator spawnPlatform() {
        int randEnemySpawn;
        float yPos = 2;
        

        while (GameManager.s_instance.getGameState() != GameState.GameOver) {
            if (GameManager.s_instance.getGameState() == GameState.GameFinished) {
                break;
            }



            rndX = Random.Range(min, max);
            newPos = new Vector3(rndX, yPos, lastZPosition + DisplacementValue);  // creamos la nueva posici?n



            
            
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
  
                lastZPosition = newPos.z;         // uso de la nueva posici?n
                lastPosition = newPos;

               if(platformPool.Count > objectPoolLimit ) {
                    canInstantiate = false; 
               }
            }
            else {
                GameObject tempGO = platformPool.Dequeue();
                //rndX = Random.Range(minX, maxX);

                Debug.LogWarning("switch: " + LevelManager.s_instance.getSpawnSection());

                switch (LevelManager.s_instance.getSpawnSection())
                {
                    
                    case 0:
                        tempGO.transform.position = new Vector3(rndX, newPos.y, newPos.z);
                        lastZPosition = tempGO.transform.position.z;
                        break;
                    case 1:
                        rndZ = Random.Range(min, max);
                        tempGO.transform.position = new Vector3(lastPosition.x + DisplacementValue, yPos, lastZPosition + rndZ);
                        Debug.LogWarning("esto: " + tempGO.transform.position);
                        
                        break;
                    default:
                        throw new UnityException("Invalid Game State");
                }



                

                
                

                Debug.Log("esta posici?n ahora: " + tempGO.transform.position);
                //tempGO.SetActive(true);
                platformPool.Enqueue(tempGO);

                lastPosition = tempGO.transform.position;


                randEnemySpawn = Random.Range(1, 6);
                if (randEnemySpawn < 3) {
                    Instantiate(enemyPrefab, tempGO.transform.position, Quaternion.identity);
                    enemyPrefab.SetActive(true);
                }
            }
        }
    }

    public int getPoolLimit () {
        return objectPoolLimit;
    }

 
    
}
