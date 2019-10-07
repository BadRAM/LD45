using System;
using UnityEngine;
using System.Collections;

// basic WASD-style movement control


[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{
    //This set of public variables is for movement speed
    public float speed = 100.0f;
    float stopspeed = 0;// do not touch, responsible for stopping player
    float maxspeed = 10; // how fast to move
    float acceleration = 10; //how long to get to max speed
    float deceleration = 10; //how long to get speed of 0
    private Vector3 velocity;
    [SerializeField] private float accelFactor = 0.1f;
    
    private bool _canMove = true;

    private CharacterController _charController;

    private float _tackleTimer;
    [SerializeField] private float tackleDuration;
    [SerializeField] private float tackleRecharge;
    [SerializeField] private float tackleSpeed;
    private Vector3 _tackleDir;
    
    private Collider hitbox;

    void Start()
    {
        _charController = GetComponent<CharacterController>();
        hitbox = GetComponent<Collider>();
        hitbox.enabled = false;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        
        // should this be in FixedUpdate?
        if (_canMove == true)
        {
            _tackleTimer = Mathf.Max(0, _tackleTimer - Time.deltaTime);
            if (_tackleTimer >= tackleRecharge)
            {
                _charController.Move(_tackleDir * tackleSpeed * Time.deltaTime);
            }
            else
            {
                Vector3 desiredMove =
                    new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized * speed;

                velocity = Vector3.Lerp(velocity, desiredMove, accelFactor * Time.deltaTime);

                _charController.Move(velocity * Time.deltaTime); //Last line of code related to regular movement

                //Start of block of code related to running
                if (Input.GetButtonDown("Fire1") &&
                    GetComponent<PlayerCharacter>().returnGunType() == 0 &&
                    _tackleTimer == 0)
                {
                    _tackleDir = (GameInfo.Camera.GetMouseCastPos() - transform.position).normalized;
                    _tackleTimer = tackleDuration + tackleRecharge;
                    velocity = Vector3.zero;
                }
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        
        if (_tackleTimer >= tackleRecharge && hit.transform.parent.CompareTag("Enemy") && GameInfo.Player.returnGunType() == 0)
        {
            hit.transform.parent.GetComponent<Enemy>().Die();
            GetComponent<PlayerCharacter>().ChangetoGun(hit.transform.parent.GetComponent<Enemy>().WeaponDropID);
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

    public void SetMove(bool move)
    {
        _canMove = move;
    }


}