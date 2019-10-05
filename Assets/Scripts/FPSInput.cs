using UnityEngine;
using System.Collections;

// basic WASD-style movement control


[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{
    //This set of public variables is for movement speed
    public float speed = 10.0f;
    float stopspeed = 0;// do not touch, responsible for stopping player
    float maxspeed = 10; // how fast to move
    float acceleration = 10; //how long to get to max speed
    float deceleration = 10; //how long to get speed of 0
    



    public float gravity = -9.8f;
    public float runSpeed = 15.0f;
    public float normalSpeed = 6.0f;
    public bool isRunning = false;
    public float crchSpeed = 4.0f;
    public float jumpRate = 0.5f;
    // private float nextJump = 0.5f;
    public Camera mainCamera;
    private bool _canMove = true;

    //These private variables relate to crouching
    private Transform tr;
    private float height;

    private CharacterController _charController;

    void Start()
    {
        _charController = GetComponent<CharacterController>();
        tr = transform;
        height = _charController.height;
    }

    void Update()
    {
        
        // should this be in FixedUpdate?
        if (_canMove == true)
        {
            Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayLength;
            if (groundPlane.Raycast(cameraRay, out rayLength))
            {
                Vector3 pointToLook = cameraRay.GetPoint(rayLength);
                Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);

              //  transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            }
            //Start of block of code related to regular movement
            float h = height;
            
            stopspeed = stopspeed - acceleration * Time.deltaTime;
            if ((Input.GetAxis("Horizontal") > 0f) && (stopspeed < maxspeed))
            {
                stopspeed = stopspeed - acceleration * Time.deltaTime;

            }


    //        float deltaX = Input.GetAxis("Horizontal") * speed;
  //          float deltaZ = Input.GetAxis("Vertical") * speed;




            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");



//            Vector3 movement = new Vector3(deltaX, 0, deltaZ);
          //  Vector3 movement = new Vector3(horizontal*speed*Time.deltaTime, 0, transform.Translate("");

            movement = Vector3.ClampMagnitude(movement, speed);

            movement.y = gravity;

            movement *= Time.deltaTime;

            movement = transform.TransformDirection(movement);

            
            _charController.Move(movement); //Last line of code related to regular movement

            //Start of block of code related to running
            if (Input.GetKey(KeyCode.LeftShift))
            {
                isRunning = true;
                speed = runSpeed; //When holding W and Shift, it changed the speed value to the run speed value to make the char move faster
            }
            else
            {
                isRunning = false;
                speed = normalSpeed; //When holding any other combos of keys, the speed value is set to the normal speed value
            }
        }
    }

    public void SetMove()
    {
        if (_canMove == true)
        {
            _canMove = false;
        }
        else
        {
            _canMove = true;
        }
    }


}