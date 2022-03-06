using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private WeaponInformator weaponInformator;
    [SerializeField] private PlayerAim playerAim;

    private Timer timer;
    
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            timer = new Timer(1/weaponInformator.weapon.fireRate);
        }
        
        if (Input.GetMouseButton(0))
        {
            if (timer.TimerDone())
            {
                var newAmmo = Instantiate(weaponInformator.weapon.ammoUsed.prefabAmmo, weaponInformator.weaponMuzzle.position,Quaternion.identity);
                newAmmo.transform.right = weaponInformator.weaponMuzzle.right * playerAim.GetDirection();
                timer.RestartTimer();
            }
            timer.UpdateTimer(Time.deltaTime);
        }
    }
}
