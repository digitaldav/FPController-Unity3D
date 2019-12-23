using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectDetection : MonoBehaviour {

    private RaycastHit RHit;
    private Ray ray;

    public float distance = 3f;

    public Text ObjectText;

    public Movement MScript;
    public CameraControl CameraScript;
    public CameraZoom ZoomScript;

    private bool InspectMode;

    private Vector3 ObjectPosition;
    private Vector3 ObjectRotation;

    public float RotationSensibility=10f;
    float RotationXClamp;

    void Start() {
        
    }

    private void Inspect() {
        if (InspectMode) {

            MScript.enabled = false; ;
            CameraScript.enabled = false;

            ObjectText.text = "Press X to return";
            ObjectText.enabled = true;

            if (RHit.collider.gameObject.GetComponent<ObjectMovement>() != null) {
                RHit.collider.gameObject.GetComponent<ObjectMovement>().enabled = true;
            }

            if (Input.GetKeyDown(KeyCode.X) && InspectMode) {
                MScript.enabled = true; 
                CameraScript.enabled = true;

                if(RHit.collider.gameObject.GetComponent<ObjectMovement>()!=null){
                    RHit.collider.gameObject.GetComponent<ObjectMovement>().enabled = false;
                }

                RHit.transform.eulerAngles = ObjectRotation;
                RHit.transform.position = ObjectPosition;

                InspectMode = false;
            }
        }

    }


    /* Pending: 
     * - Add SelectedObject variable to avoid weird things
     * - Replace GetKeyDown -> GetButtonDown
     */
    void Update()  {

        if (!InspectMode) {
            ObjectText.text = "";
            ObjectText.enabled = false;
        }

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RHit, distance) && !InspectMode) {
            if(RHit.transform.tag == "Object" && !InspectMode) {

                ObjectText.text = "Press F to inspect "+RHit.collider.gameObject.name;
                ObjectText.enabled = true;

                if (Input.GetKeyDown(KeyCode.F) && !InspectMode) {
                    InspectMode = true;

                    ObjectPosition = RHit.transform.position;
                    ObjectRotation = RHit.transform.eulerAngles;
                    RHit.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 1f;
                }

            } else {
                ObjectText.text = "";
                ObjectText.enabled = false;
            }
        }

        Inspect();

 
    }
}
