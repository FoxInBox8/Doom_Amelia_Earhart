using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    [SerializeField] int DMG;

    private GameObject gunModel;

    private float shotTimer;
    [SerializeField] float shotCD;

    private bool shotReady;

    private void Awake()
    {
        gunModel = gameObject.GetComponent<GameObject>();
    }

    public MeshRenderer GetGunModel()
    {
        return gunModel.GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        shotTimer = shotCD;
    }

    void Update()
    {
        ShotBuffer();
    }

    public void DealDMG(RaycastHit raycast)
    {
        if (shotReady)
        {
            GameObject hit = raycast.collider.gameObject;

            if (hit.tag == "Enemy")
            {
                hit.GetComponent<Crab>().TakeDamage(DMG);
            }
            shotReady = false;
        }
    }

    private void ShotBuffer()
    {
        if(shotReady == false)
        {
            if (shotTimer > 0)
            {
                shotTimer -= Time.deltaTime;
            }
            else
            {
                shotReady = true;
            }
        }
    }

}
