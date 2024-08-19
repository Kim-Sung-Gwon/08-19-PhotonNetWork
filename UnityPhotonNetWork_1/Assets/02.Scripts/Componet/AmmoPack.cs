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
        // target�� ź���� �߰��ϴ� ó��
        //Debug.Log("ź���� ���� �ߴ� : " + ammo);
    }
}
