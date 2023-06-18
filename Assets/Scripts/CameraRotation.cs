using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private Transform playerShoulders;
    [SerializeField] private float rotationSensitivity = 5f; // rotationHorizontal = 0f, rotationVertical = 0f;

    private void Update() {
        //float mouseX = Input.GetAxis("Mouse X") * rotationSensitivity;
        //float mouseY = Input.GetAxis("Mouse Y") * rotationSensitivity;

        //playerShoulders.Rotate(Vector3.up, mouseX);

        //rotationHorizontal -= mouseY;
        //rotationHorizontal = Mathf.Clamp(rotationHorizontal, -90f, 90f);

        //rotationVertical += mouseX;
        //transform.localRotation = Quaternion.Euler(0f, rotationVertical, 0f);
        //playerShoulders.rotation = Quaternion.Euler(0f, rotationVertical, 0f);

        float mouseHorizontal = Input.GetAxis("Mouse X");
        playerShoulders.rotation *= Quaternion.AngleAxis(mouseHorizontal * rotationSensitivity, Vector3.up);
        if(PlayerManager.instance.GetState() != PlayerState.Idle) {

            transform.rotation = Quaternion.Euler(0, playerShoulders.rotation.eulerAngles.y, 0);
            playerShoulders.localEulerAngles = Vector3.zero;
        }
    }
}
