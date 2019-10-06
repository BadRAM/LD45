using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private CameraMouseCharacter _camera;
    [SerializeField] private GameObject projectile; //revolver
    [SerializeField] private GameObject projectileShotgun; //shotgun
    [SerializeField] private GameObject projectileFlamethrower; //flamethrower

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
                        Instantiate(projectile, transform.position, Quaternion.LookRotation(_camera.GetMouseCastPos() - transform.position, transform.up)); //REGULAR REVOLVER PROJECTILE
                        _playerGun.useOneAmmo();
                        break;
                    case 2:
                        Instantiate(projectile, transform.position, Quaternion.LookRotation(_camera.GetMouseCastPos() - transform.position, transform.up)); //SHOTGUN PROJECTILE CHANGE NEEDED
                        _playerGun.useOneAmmo();
                        break;
                    case 3:
                        Instantiate(projectile, transform.position, Quaternion.LookRotation(_camera.GetMouseCastPos() - transform.position, transform.up)); //FLAMETHROWER PROJECTILE CHANGE NEEDED
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
}
