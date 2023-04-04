using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    public ProjectileBase prefabProjectile;

    public Transform positionToShoot;
    
    
    void Update()
    {
        if(Input.GetKey(KeyCode.S))
        {
            Shoot();
        }
    }


    public void Shoot()
    {
        var projectile = Instantiate(prefabProjectile);
        projectile.transform.position = positionToShoot.position;
    } 
}
