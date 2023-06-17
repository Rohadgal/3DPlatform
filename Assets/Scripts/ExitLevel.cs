using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExitLevel : MonoBehaviour {
   
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            LevelManager.s_instance.changeLevelState(LevelState.LevelFinished);
            Debug.Log("LevelFinished");
        }
    }
}
