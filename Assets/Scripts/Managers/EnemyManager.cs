using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private void Start() {
        StartCoroutine(destroyEnemey());
    }

    IEnumerator destroyEnemey() {
        yield return new WaitForSeconds(LevelManager.s_instance.getTime() * (PlatformSpawner.s_instance.getPoolLimit() +1) );  // El numero que escala el tiempo es equivalente a PoolLimit + 1.
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
