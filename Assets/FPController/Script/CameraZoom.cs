using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {

    private Camera PlayerCamera;

    public float ZoomValue = 2f;
    private float DefaultValue;

    void Start(){
        PlayerCamera = GetComponent<Camera>();
        DefaultValue = PlayerCamera.fieldOfView;
    }

    void Update(){

        if (PlayerCamera == null) {
            Debug.LogError("<color=red>GameObject <color=blue>" + gameObject.name + "</color> error:  no camera found</color>");
            enabled = false;
        }

        if (Input.GetButton("Zoom")) {
            PlayerCamera.fieldOfView = Mathf.Lerp(PlayerCamera.fieldOfView, ZoomValue, Time.deltaTime*5);
        }
        PlayerCamera.fieldOfView = Mathf.Lerp(PlayerCamera.fieldOfView, DefaultValue, Time.deltaTime * 5);
    }
}
