using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private void Start() {
        StartCoroutine(destroyEnemey());
    }

    IEnumerator destroyEnemey() {
        yield return new WaitForSeconds(LevelManager.s_instance.getTime() * 4);
        gameObject.SetActive(false);
    }
}
