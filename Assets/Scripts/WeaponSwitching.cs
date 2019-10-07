using UnityEngine;

public class WeaponSwitching : MonoBehaviour {

    public int selectedWeapon = 0;
    private GameObject _player; //GameObject variable to grab the object with Player tag
    private PlayerCharacter _playerGun; //Instance of PlayerCharacter so we can directly see what guntype they got and change it

    // Use this for initialization
    void Start () {
        SelectWeapon();
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerGun = _player.GetComponent<PlayerCharacter>();

    }
	
	// Update is called once per frame
	void Update () {
        /*int previousSelectedWeapon = selectedWeapon;
        if (Input.GetMouseButtonDown(1))
        {
            if (selectedWeapon >= transform.childCount - 1)
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon++;
            }
        }

        if(previousSelectedWeapon!= selectedWeapon)
        {
            SelectWeapon();
        }*/
        
        selectedWeapon = _playerGun.returnGunType();
        SelectWeapon();
    }
    void SelectWeapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if(i==selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
