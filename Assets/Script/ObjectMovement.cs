using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour {

    float RotationXClamp;
    float RotationyYClamp;

    void Start()  {
        
    }


    void Update() {
        float XAxis = Input.GetAxis("Mouse X") * 100f * Time.deltaTime;
        float YAxis = Input.GetAxis("Mouse Y") * 100f * Time.deltaTime;

        RotationXClamp -= YAxis;
        RotationyYClamp -= XAxis;

        transform.localRotation = Quaternion.Euler(RotationXClamp, RotationyYClamp, 0f);
    }
}
