using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateStop : MonoBehaviour
{
    public bool pushed = false;
    public Renderer flashEffect;
    public AudioSource pushSound;
    public CreateReel reel;

    private void Start()
    {
        flashEffect.enabled = false;
    }

    public void Onclick()
    {
        if (transform.parent.GetComponent<RotateStart>().pushed)
        {
            if (!pushed)
            {
                flashEffect.enabled = false;
                pushSound.Play();
                pushed = true;
                reel.getMarkStopPos();
            }
        }
    }
}