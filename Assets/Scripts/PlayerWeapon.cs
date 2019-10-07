using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private CameraMouseCharacter _camera;
    [SerializeField] private GameObject projectileRevolver; //revolver
    [SerializeField] private float RevolverCoolDown;
    [SerializeField] private GameObject projectileShotgun; //shotgun
    [SerializeField] private float ShotgunCoolDown;
    [SerializeField] private GameObject projectileFlamethrower; //flamethrower
    [SerializeField] private float FlamethrowerCoolDown;
    [SerializeField] private GameObject projectileMachinegun; // salty gun
    [SerializeField] private float MachineGunCoolDown;

    private float _heat;


    //begin shotgun logic
    public int pelletCount;
    public float spreadAngle;
    

    private void Awake()
    {
        
    }
    
    private GameObject _player; //GameObject variable to grab the object with Player tag
    private PlayerCharacter _playerGun; //Instance of PlayerCharacter so we can directly see what guntype they got and change it

    // Start is called before the first frame update
    void Start()
    {
        _camera = FindObjectOfType<CameraMouseCharacter>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerGun = _player.GetComponent<PlayerCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        _heat = Mathf.Max(0, _heat - Time.deltaTime);
        
        if (Input.GetButtonDown("Fire1"))
        {
            switch (_playerGun.returnGunType())
            {
                case 1:
                  //  ani.Play("gunplay");
                    fireRevolver();
                    break;
                case 2:
                    fireSG();
                    break;
            }
        }
        
        if (Input.GetButton("Fire1"))
        {
            switch (_playerGun.returnGunType())
            {
                case 3:
                    fireFlamethrower();
                    break;
                case 4:
                    fireMachinegun();
                    break;
            }
        }
    }

    private void fireRevolver()
    {
        if (_heat == 0)
        {
            Instantiate(projectileRevolver, transform.position, 
                Quaternion.LookRotation(_camera.GetMouseCastPos() - transform.position, transform.up));
            _heat = RevolverCoolDown;
            FindObjectOfType<AudioManager>().Play("revolvershot");
        }

    }
    
    
    private void fireSG()
    {
        Debug.Log("fired shotgun");
        if (_heat == 0)
        {
            for (int i = 0; i < pelletCount; i++)
            {
                Vector2 r = Random.insideUnitCircle;
                Vector3 r3 = new Vector3(r.x, 0, r.y) * spreadAngle;
                Quaternion AimAngle = Quaternion.LookRotation(
                    (_camera.GetMouseCastPos() - transform.position).normalized * 10 + r3, 
                    transform.up);
                
                Instantiate(projectileShotgun, transform.position, AimAngle);
            }
            FindObjectOfType<AudioManager>().Play("shotgunshot");
        }
    }

    private void fireFlamethrower()
    {
        if (_heat == 0)
        {
            Instantiate(projectileFlamethrower, transform.position, 
                Quaternion.LookRotation(_camera.GetMouseCastPos() - transform.position, transform.up));
            _heat = FlamethrowerCoolDown;
            FindObjectOfType<AudioManager>().Play("shotgunshot");
        }
    }

    private void fireMachinegun()
    {
        if (_heat == 0)
        {
            Instantiate(projectileMachinegun, transform.position, 
                Quaternion.LookRotation(_camera.GetMouseCastPos() - transform.position, transform.up));
            _heat = MachineGunCoolDown;
            FindObjectOfType<AudioManager>().Play("machinegunshot");
        }
    }
}

