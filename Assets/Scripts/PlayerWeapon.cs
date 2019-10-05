using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private CameraMouseCharacter _camera;
    [SerializeField] private GameObject projectile;
    
    // Start is called before the first frame update
    void Start()
    {
        _camera = FindObjectOfType<CameraMouseCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(projectile, transform.position,
                Quaternion.LookRotation(_camera.GetMouseCastPos() - transform.position, transform.up));
        }
    }
}
