using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform shootPos;
    public GameObject bullet;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Instantiate(bullet, shootPos.transform.position, transform.rotation);
        }
    }
}
