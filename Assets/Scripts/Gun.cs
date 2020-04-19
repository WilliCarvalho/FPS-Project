using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Gun configurartion")]
    public float damage;
    public float range;
    public float firerate;
    public float waitToFirerate;
    public Camera cam;
    public ParticleSystem ammoParticle;
    public GameObject impact;
    public bool hold = false;
    
    [Space]
    [Header("Ammo")]
    public int maxAmmoInMagazine;
    public int ammoInMagazine;
    public int ammo;
    public int timeToReload;
    private int timeTr;

    private bool reload = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            hold = true;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            hold = false;
        }

        if (hold == true && ammoInMagazine > 0)
        {
            waitToFirerate += 1;
        }

        if (waitToFirerate > firerate && ammoInMagazine > 0)
        {
            Shoot();
        }

        if (Input.GetButtonDown("Reload") && ammoInMagazine != maxAmmoInMagazine && ammo != 0 && reload == false)
        {
            reload = true;
        }

        if(reload == true)
        {
            if(timeTr > timeToReload)
            {
                for (int i = 0; i < maxAmmoInMagazine; i++)
                {
                    if(ammoInMagazine < maxAmmoInMagazine && ammo > 0)
                    {
                        ammo -= 1;
                        ammoInMagazine += 1;
                    }
                    else
                    {
                        break;
                    }
                    reload = false;
                    timeTr = 0;
                }
            }
            else
            {
                timeTr += 1;
            }
        }
    }



    void Shoot()
    {
        waitToFirerate = 0;
        ammoInMagazine -= 1;
        ammoParticle.Play();
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Debug.Log("Aiming on: " + hit.transform);

            ObjectDestroyable od = hit.transform.GetComponent<ObjectDestroyable>();
            if(od != null)
            {
                od.TakeDamage(damage);
            }

            GameObject impactGo = Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }
}
