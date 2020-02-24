using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetButtonControl : MonoBehaviour
{
    public bool pushed = false;
    public bool reelAllStoped = false;

    public Renderer flashEffect;

    public RotateStart startButton;
    public RotateStop stopButtom1;
    public RotateStop stopButtom2;
    public RotateStop stopButtom3;

    public AudioSource betSound;

    private void Update()
    {
        if (stopButtom1.pushed && stopButtom2.pushed && stopButtom3.pushed && pushed && startButton.pushed)
        {
            flashEffect.enabled = true;
            reelAllStoped = true;
            pushed = false;
        }
    }

    public void Onclick()
    {
        if (!pushed)
        {
            flashEffect.enabled = false;
            startButton.pushed = false;
            betSound.Play();
            pushed = true;
        }
    }
}
