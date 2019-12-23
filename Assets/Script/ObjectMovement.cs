using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float XAxis = Input.GetAxis("Mouse X") * 100f * Time.deltaTime;
        float YAxis = Input.GetAxis("Mouse Y") * 100f * Time.deltaTime;


        transform.Rotate(Vector3.down * XAxis);
        transform.Rotate(Vector3.right * YAxis);
    }
}
