using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShield : MonoBehaviour
{
    private float waitTime;

    private void Start()
    {
        this.transform.position = new Vector3(0,500,0);
    }

    private void Update()
    {
        if (Time.time > waitTime)
        {
            this.transform.position = new Vector3(0, 500, 0);
        }
    }

    public void ShieldOn(float interval)
    {
        this.transform.position = new Vector3(0, 0, 0);
        waitTime = Time.time + interval;
    }
}
