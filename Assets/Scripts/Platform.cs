using System.Collections;
using UnityEngine;

public class Platform : MonoBehaviour
{

    private void Start() {
        Debug.Log("la plataforma existe1");
        //StartCoroutine(deactivatePlatform());
    }

    //void Update()
    //{
    //    //if (GameManager.s_instance.getGameState() != GameState.Playing) {
    //    //    return;
    //    //}
    //    //deactivatePlatform();
    //}

    //IEnumerator deactivatePlatform() {
    //    float timeToDisappear = LevelManager.s_instance.getTime() + 2f;
    //    yield return new WaitForSeconds(timeToDisappear);
    //    Debug.Log("tiempo para desaparecer" + timeToDisappear);
    //    gameObject.SetActive(false);
    //}
}
