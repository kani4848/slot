using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScreenClick : MonoBehaviour
{
    public BetButtonControl betButton;
    public RotateStart rotateButton;
    public RotateStop stopButton1;
    public RotateStop stopButton2;
    public RotateStop stopButton3;

    private float nextTime=0;
    private float interval = 0.2f;

    public CoinManager coinManager;

    private void Update()
    {
        /*
#if UNITY_EDITOR
        */
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        /*
    #else
        if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            return;
        }
    #endif
    */
        if (Input.GetMouseButtonDown(0))
        {
            OnClick();
        }
    }

    public void OnClick()
    {
        if (!betButton.pushed && Time.time > nextTime)
        {
            betButton.Onclick();
            nextTime = Time.time + interval;
            return;
        }
        if (!rotateButton.pushed && Time.time > nextTime)
        {
            rotateButton.Onclick();
            nextTime = Time.time + interval;
            return;
        }
        if (!stopButton1.pushed && Time.time > nextTime)
        {
            stopButton1.Onclick();
            nextTime = Time.time + interval;
            return;
        }
        if (!stopButton2.pushed && Time.time > nextTime)
        {
            stopButton2.Onclick();
            nextTime = Time.time + interval;
            return;
        }
        if (!stopButton3.pushed && Time.time > nextTime)
        {
            stopButton3.Onclick();
            nextTime = Time.time + interval;
            return;
        }
    }
}
