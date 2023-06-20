using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstSectionCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LevelManager.s_instance.setSpawnerSection(1);
            Debug.LogWarning("cambio");
        }
    }
}
