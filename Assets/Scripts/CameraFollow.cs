using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Class makes camera foolow the Player
    
    [SerializeField] private Vector3 cameraOffset;

    private float smoothSpeed = 12.5f;

    private void LateUpdate()
    {
        Vector3 desiredCameraPosition = PlayerCharacter.Instance.transform.position + cameraOffset;
        Vector3 smoothedCameraPosition = Vector3.Lerp(transform.position, desiredCameraPosition, smoothSpeed*Time.deltaTime);
        transform.position = smoothedCameraPosition;
    }


}
