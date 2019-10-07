using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    
    [SerializeField] private float startDelay;
    [SerializeField] private float spawnRadius;
    [SerializeField] private float spawnHeight;
    [SerializeField] private int spawnQuota;
    [SerializeField] private GameObject RevolverEnemy;
    private int _revolverStatus; // Status variable, 0 means present, 1 active, -1 defeated.
    [SerializeField] private GameObject ShotgunEnemy;
    private int _shotgunStatus;
    [SerializeField] private GameObject FlamethrowerEnemy;
    private int _flamethrowerStatus;
    [SerializeField] private GameObject MachinegunEnemy;
    private int _machinegunStatus;
    private float _startTime;
    [SerializeField] private string winScreenName;
    
    
    // Start is called before the first frame update
    void Start()
    {
        GameInfo.EnemySpawner = this;
        _startTime = Time.time;
    }

    private void FixedUpdate()
    {
        if (_revolverStatus == -1 &&
            _shotgunStatus == -1 &&
            _flamethrowerStatus == -1 &&
            _machinegunStatus == -1)
        {
            SceneManager.LoadSceneAsync(winScreenName);
        }
        
        if (Time.time - _startTime > startDelay)
        {
            int[] t = tally();

            if (_revolverStatus == 0 && t[0] < spawnQuota)
            {
                Spawn(RevolverEnemy);
            }
            else if (_revolverStatus == 1 && t[0] == 0)
            {
                _revolverStatus = -1;
                GameInfo.Player.ChangetoGun(0);
            }
            
            
            if (_shotgunStatus == 0 && t[1] < spawnQuota)
            {
                Spawn(ShotgunEnemy);
            }
            else if (_shotgunStatus == 1 && t[1] == 0)
            {
                _shotgunStatus = -1;
                GameInfo.Player.ChangetoGun(0);
            }
            
            
            if (_flamethrowerStatus == 0 && t[2] < spawnQuota)
            {
                Spawn(FlamethrowerEnemy);
            }
            else if (_flamethrowerStatus == 1 && t[2] == 0)
            {
                _flamethrowerStatus = -1;
                GameInfo.Player.ChangetoGun(0);
            }
            
            
            if (_machinegunStatus == 0 && t[3] < spawnQuota)
            {
                Spawn(MachinegunEnemy);
            }
            else if (_machinegunStatus == 1 && t[3] == 0)
            {
                _machinegunStatus = -1;
                GameInfo.Player.ChangetoGun(0);
            }
        }
    }

    private int[] tally()
    {
        int[] t = new int[4];

        foreach (Enemy e in GameInfo.Enemies)
        {
            t[e.WeaponDropID - 1]++;
        }

        return t;
    }

    public void Spawn(GameObject prefab)
    {
        Vector2 spot = spawnRadius * Random.insideUnitCircle;
        Vector3 pos = transform.position + new Vector3(spot.x, Random.Range(0, spawnHeight), spot.y);
        Instantiate(prefab, pos, Quaternion.identity);
    }

    public void SetActiveWeapon(int weapon)
    {
        int[] t = tally();

        switch (weapon)
        {
            case 1:
                _revolverStatus = 1;
                while (t[0] < 15)
                {
                    Spawn(RevolverEnemy);
                    t[0]++;
                }
                break;
            case 2:
                _shotgunStatus = 1;
                while (t[1] < 15)
                {
                    Spawn(ShotgunEnemy);
                    t[1]++;
                }
                break;
            case 3:
                _flamethrowerStatus= 1;
                while (t[2] < 15)
                {
                    Spawn(FlamethrowerEnemy);
                    t[2]++;
                }
                break;
            case 4:
                _machinegunStatus = 1;
                while (t[3] < 15)
                {
                    Spawn(MachinegunEnemy);
                    t[3]++;
                }
                break;
        }
    }
}
