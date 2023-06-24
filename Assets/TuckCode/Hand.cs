using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class Hand : MonoBehaviour
{

    [SerializeField] GameObject[] guns;
    [SerializeField] GameObject currentGun;
    public int selectedGun;
    private bool gunExist;

    void Start()
    {
        selectedGun = 0;
        gunExist = false;
        ChangeGun();
    }

    void Update()
    {
        
    }

    private void ChangeGun()
    {
        if (gunExist)
        {
            Destroy(currentGun);
        }
        currentGun = Instantiate(guns[selectedGun], gameObject.transform);
        currentGun.transform.position = gameObject.transform.position;
    }

    private void Fire()
    {
        RaycastHit hit;
        //IN PROGRESS
    }
}
