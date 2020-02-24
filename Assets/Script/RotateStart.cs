using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateStart : MonoBehaviour
{
    public bool pushed = false;

    public Renderer flashEffect;

    public BetButtonControl betButton;
    public RotateStop stopButtom1;
    public RotateStop stopButtom2;
    public RotateStop stopButtom3;

    public AudioSource rotateSound;

    private void Start()
    {
        flashEffect.enabled = false;
    }

    private void FixedUpdate()
    {
        if (betButton.pushed && !pushed)
        {
            flashEffect.enabled = true;
        }
    }

    public void Onclick()
    {
        if (!pushed && betButton.pushed)
        {
            flashEffect.enabled = false;

            rotateSound.Play();

            pushed = true;

            stopButtom1.pushed = false;
            stopButtom2.pushed = false;
            stopButtom3.pushed = false;

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