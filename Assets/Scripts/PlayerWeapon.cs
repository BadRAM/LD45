using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private CameraMouseCharacter _camera;
    [SerializeField] private GameObject projectile; //revolver
    [SerializeField] private GameObject projectileShotgun; //shotgun
    [SerializeField] private GameObject projectileFlamethrower; //flamethrower


    //begin shotgun logic
    public int pelletCount;
    public float spreadAngle;
    
    public GameObject pellet;
    public Transform BarrelExit;
    List<Quaternion> pellets;

    private void Awake()
    {
        pellets = new List<Quaternion>(pelletCount);
        for(int i = 0; i<pelletCount; i++)
        {
            pellets.Add(Quaternion.Euler(Vector3.zero));
        }
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
        if (Input.GetButtonDown("Fire1")) //FIRE GUN MECHANICS. CHANGE PROJECTILE ONLY HERE AND HOW U FIRE
        {
            if (_playerGun.returnAmmoLeft() > 0)// make sure u got ammo before firing
            {
                switch(_playerGun.returnGunType())
                {
                    case 1:
                        Instantiate(projectile, transform.position, Quaternion.LookRotation(GameInfo.Camera.GetMouseCastPos() - transform.position, transform.up)); //REGULAR REVOLVER PROJECTILE
                        _playerGun.useOneAmmo();
                        break;
                    case 2:
                        fireSG();
                     //   Instantiate(projectile, transform.position, Quaternion.LookRotation(_camera.GetMouseCastPos() - transform.position, transform.up)); //SHOTGUN PROJECTILE CHANGE NEEDED

                        _playerGun.useOneAmmo();
                        break;
                    case 3:
                        Instantiate(projectile, transform.position, Quaternion.LookRotation(GameInfo.Camera.GetMouseCastPos() - transform.position, transform.up)); //FLAMETHROWER PROJECTILE CHANGE NEEDED
                        _playerGun.useOneAmmo();
                        break;
                }
                
            }
           else
            {
                //swap back to revolver
                _playerGun.ChangetoGun(1);
            }



        }
    }
    void fireSG()
    {
        for (int i = 0; i < pelletCount; i++)
        {
            //pellets[i] = Random.rotation;

            pellets[i] = Quaternion.Euler(Random.Range(0,90),0,0);
            // GameObject p = Instantiate(projectile, BarrelExit.position, BarrelExit.rotation);

            GameObject p = Instantiate(projectile, BarrelExit.position, Quaternion.LookRotation(GameInfo.Camera.GetMouseCastPos() - BarrelExit.position, transform.up));
            p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, pellets[i], spreadAngle);
          //  p.transform.
        }
    }
}
