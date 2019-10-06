using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupFlamethrower : MonoBehaviour
{
    private GameObject _player; //GameObject variable to grab the object with Player tag
    private PlayerCharacter _playerGun; //Instance of PlayerCharacter so we can directly see what guntype they got and change it
    public int gunNum; //gun to change to

    // Use this for initialization
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerGun = _player.GetComponent<PlayerCharacter>();
        gunNum = 3; // 1 = revolver, 2 = shotgun, 3 = flamethrower
    }

    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") //Only works when the game object with tag Player enters it
        {
            FindObjectOfType<AudioManager>().Play("PickupGun"); //need 2 add pickup sound
            _playerGun.ChangetoGun(gunNum); //change gun or restore ammo function
            Destroy(this.gameObject); //Destroy the pickup
        }
    }
}
