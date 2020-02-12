using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateStart : MonoBehaviour
{
    public bool pushed = false;
    public bool reelAllStoped = false;

    public Renderer flashEffect;

    public RotateStop stopButtom1;
    public RotateStop stopButtom2;
    public RotateStop stopButtom3;

    public AudioSource rotateSound;

    private void Update()
    {
        if (stopButtom1.pushed && stopButtom2.pushed && stopButtom3.pushed)
        {
            flashEffect.enabled = true;
            reelAllStoped = true;

            pushed = false;
            stopButtom1.pushed = false;
            stopButtom2.pushed = false;
            stopButtom3.pushed = false;
        }
    }

    public void Onclick()
    {
        if (!pushed)
        {
            flashEffect.enabled = false;

            rotateSound.Play();

            pushed = true;

            stopButtom1.flashEffect.GetComponent<Renderer>().enabled = true;
            stopButtom2.flashEffect.GetComponent<Renderer>().enabled = true;
            stopButtom3.flashEffect.GetComponent<Renderer>().enabled = true;

            GameObject[] marks = GameObject.FindGameObjectsWithTag("Mark");
            foreach (GameObject mark in marks)
            {
                mark.GetComponent<MarkControl>().spinOn = true;
            }
        }
    }
}