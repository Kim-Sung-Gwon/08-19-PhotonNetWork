using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : MonoBehaviour, IItem
{
    public int ammo = 30;

    public void Use(GameObject target)
    {
        PlayerShooter shooter = target.GetComponent<PlayerShooter>();
        if (shooter != null && shooter.gun != null)
        {
            shooter.gun.ammoRemain += ammo;
        }
        Destroy(gameObject);
        // target에 탄알을 추가하는 처리
        //Debug.Log("탄알이 증가 했다 : " + ammo);
    }
}
