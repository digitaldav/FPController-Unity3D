using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    private CharacterController CController;

    public float PlayerSpeed = 12f;
    public float RunningMult = 1f;
    public float Gravity = -9.81f;

    private Vector3 FallVelocity;
    public float JumpHeight = 3f;

    public Transform GroundCheckTransform;
    public float GroundDistance = 0.4f;
    public LayerMask GroundMask;

    bool isGrounded;
    bool isRunning;

    private float PlayerHeight;
    public float CrouchHeight = 1.4f;
    public float CrouchSpeed = 6f;
    private float CrouchMult = 1f;

    private RaycastHit objectHit;
    bool isCrouch = false;

    void Start(){
        CController = GetComponent<CharacterController>();

        if (CController != null) {
            PlayerHeight = CController.height;
        }
    }


    void CrouchPress() {
        if ((Input.GetButtonDown("Crouch")) && !isRunning) {
            isCrouch = !isCrouch;
        }

        if (isCrouch) {
            CController.height = Mathf.Lerp(CController.height, CrouchHeight, Time.deltaTime * 5);
            CrouchMult = 0.5f;
        } else {

            Vector3 fwd = transform.TransformDirection(Vector3.up);
            if (Physics.Raycast(transform.position, fwd, out objectHit, (PlayerHeight - CrouchHeight))) {
                //isCrouch = true; //Auto crouch
            } else {
                CController.height = Mathf.Lerp(CController.height, PlayerHeight, Time.deltaTime * 5);
                CrouchMult = 1f;
            }

        }
    }


    void CrouchHold() {

        if ((Input.GetButton("Crouch")) && !isRunning) {
            CController.height = Mathf.Lerp(CController.height, CrouchHeight, Time.deltaTime * 5);
            CrouchMult = 0.5f;
        }else{
            if(CController.height+1 < PlayerHeight) {
                Vector3 fwd = transform.TransformDirection(Vector3.up);
                Debug.DrawRay(transform.position, fwd * (PlayerHeight - CrouchHeight), Color.green);
                if (Physics.Raycast(transform.position, fwd, out objectHit, (PlayerHeight - CrouchHeight))) {
                    CController.height = Mathf.Lerp(CController.height, CrouchHeight, Time.deltaTime * 5);
                    CrouchMult = 0.5f;
                } else {
                    CController.height = Mathf.Lerp(CController.height, PlayerHeight, Time.deltaTime * 5);
                    CrouchMult = 1f;
                }
            } else {
                CController.height = Mathf.Lerp(CController.height, PlayerHeight, Time.deltaTime * 5);
                CrouchMult = 1f;
            }
            
        }

    }

    void Update() {

        if (CController != null) {
            
            if(GroundCheckTransform == null) {
                Debug.LogError("<color=red>GameObject <color=blue>" + gameObject.name + "</color> error:  no GroundCheckTranform attached</color>");
                enabled = false;
            }

            //CrouchPress();
            CrouchHold();

            //Ground check
            isGrounded = Physics.CheckSphere(GroundCheckTransform.position, GroundDistance, GroundMask);
            if(isGrounded && FallVelocity.y < 0) {
                CController.slopeLimit = 45.0f;
                FallVelocity.y = -2f;
            }

            //Movement x, z
            float HorizontalAxis = Input.GetAxisRaw("Horizontal") ;
            float VerticalAxis = Input.GetAxisRaw("Vertical");

            if (Input.GetButtonDown("Run") && isGrounded) {
                RunningMult = 2f; isRunning = true;
            }
            if(Input.GetButtonUp("Run")){
                RunningMult = 1f; isRunning = false;
            }

            Vector3 MoveVector = transform.right * HorizontalAxis + transform.forward * VerticalAxis;
            CController.Move(MoveVector.normalized * (PlayerSpeed*RunningMult* CrouchMult) * Time.deltaTime);

            //Jump
            if(Input.GetButtonDown("Jump") && isGrounded) {
                CController.slopeLimit = 91.0f;
                FallVelocity.y = Mathf.Sqrt(JumpHeight * -1f * Gravity);
                if(isRunning){ RunningMult = 1f; isRunning = false; }
            }

            //Fall movement
            FallVelocity.y += Gravity * Time.deltaTime;
            CController.Move(FallVelocity * Time.deltaTime);

        }else{
            Debug.LogError("<color=red>GameObject <color=blue>" + gameObject.name + "</color> error:  no CharacterController found</color>");
            enabled = false;
        }
        
    }
}
