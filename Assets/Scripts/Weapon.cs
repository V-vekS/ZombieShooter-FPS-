using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityStandardAssets.Effects;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField]float rcRange = 100f;
    [SerializeField] float damage = 30f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitVFX;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] float timeBtwShots = 0.5f;

    [SerializeField] TextMeshProUGUI ammoText;



    bool canShoot = true;
    void Update()
    {
        DisplayAmmo();
        if (Input.GetMouseButtonDown(0) && canShoot == true)
        {

            StartCoroutine(Fire());
        }
    }

    private void DisplayAmmo()
    {
        int currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);
        ammoText.text = currentAmmo.ToString();
    }

    IEnumerator Fire()
    {
        canShoot = false;
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            MuzzleVFX();
            ProcessRaycasting();
            ammoSlot.ReduceAmmoAmount(ammoType);

        }
        yield return new WaitForSeconds(timeBtwShots);
        canShoot = true;
    }

    void MuzzleVFX()
    {
        muzzleFlash.Play();
    }

    private void ProcessRaycasting()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, rcRange))//position from which ray casted,Direction,return hit information,range.
        {
            Debug.Log("Ray hit" + hit.transform.name); //Tells the object ray hits
            HitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();//Getting healthbar info of enemy.
            if (target == null) { return; }//if target is not enemy.
            target.TakingDamage(damage); //applying damage to enemy.
        }

        else
        {
            return;
        }
    }

     void HitImpact(RaycastHit hit)
    {
       GameObject impact = Instantiate(hitVFX, hit.point, Quaternion.LookRotation(hit.normal));//hit.point is location of hit.
        Destroy(impact, .1f);

    }
}
