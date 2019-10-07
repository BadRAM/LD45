using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour {
    [SerializeField] private float _health = 10;
    [SerializeField] private Canvas PauseMenu;
    private bool _hasItem;
    private bool _audioPlay;
    private int _killCount;
    private int activeGun = 0;
    private int ammoleft = 0;

	void Start ()
    {
        PauseMenu.enabled = false;
        activeGun = 0;
        GameInfo.Player = this;
        //_health = 10;
        //_hasItem = false;
        //_audioPlay = false;
        //_killCount = 0;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            TogglePause();
        }
    }

    public void Hurt(float damage) // allows the player to take damage
    {
        _health -= damage;
        if(_health < 0)
        {
            _health = 0;
        }
        Debug.Log("Health: " + _health);
    }

    public float Health() //returns the value of health since its a private int
    {
        return _health;
    }

  
    public void RestoreHealth(int amount)
    {
        _health += amount;
        Debug.Log("Health: " + _health);
    }

    public void ChangetoGun(int gunNum)
    {
        activeGun = gunNum;
        Debug.Log("1=revover, 2=shotgun, 3=flamethrower 4=machinegun Gun Type: " + gunNum);
    }

    public int returnGunType() //returns the value of health since its a private int
    {
        return activeGun;
    }

    public int returnAmmoLeft() //returns the value of ammo since its a private int
    {
        return ammoleft;
    }

    public void useOneAmmo()
    {
        ammoleft--;
    }

    public void ResetHealth() //set player health back to 5
    {
        _health = 10;
        Debug.Log("Health: " + _health);
    }

    public void PickupItem()
    {
        _hasItem = true;
    }

    public void ResetItem()
    {
        _hasItem = false;
    }

    public bool HasItem()
    {
        return _hasItem;
    }

    public bool isDead()
    {
        if (_health <= 0 && _audioPlay == false)
        {
            
           // FindObjectOfType<AudioManager>().Play("PlayerDeath");
            _audioPlay = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetAudio()
    {
        _audioPlay = false;
    }

    public void IncreaseKill()
    {
        _killCount++;
        Debug.Log("Kill: " + _killCount);
    }

    public void ResetKill()
    {
        _killCount = 0;
    }

    public int ReturnKill()
    {
        return _killCount;
    }

    public void load()
    {
        transform.position = new Vector3(PlayerPrefs.GetFloat("player_x"), PlayerPrefs.GetFloat("player_y"), PlayerPrefs.GetFloat("player_y"));
        _health = PlayerPrefs.GetInt("_health");
        _killCount = PlayerPrefs.GetInt("_killCount");
        //LoadCheck.isLoad = false;
    }

    public void save()
    {
        PlayerPrefs.SetFloat("player_x", transform.position.x);
        PlayerPrefs.SetFloat("player_y", transform.position.y);
        PlayerPrefs.SetFloat("player_z", transform.position.z);
        PlayerPrefs.SetFloat("_health", _health);
        PlayerPrefs.SetInt("_killCount", _killCount);
        PlayerPrefs.SetString("_hasItem", _hasItem.ToString());
    }

    public void TogglePause()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            PauseMenu.enabled = true;
        }
        else
        {
            Time.timeScale = 1;
            PauseMenu.enabled = false;
        }
    }
}
