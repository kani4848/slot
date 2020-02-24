using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkControl : MonoBehaviour
{
    public float speed;
    public bool spinOn = false;
    public bool spinOff = false;

    public bool flash = false;
    private float nextTime= 0;
    private readonly float interval = 0.3f;
    public new Renderer renderer;

    public string markName;

    public Vector3 startPos;
    public Vector3 endPos;
    
    public int markNum;
    private float markHeight;

    private void Start()
    {
        markHeight = gameObject.GetComponent<Renderer>().bounds.size.y;
    }

    private void FixedUpdate()
    {
        //リール停止の処理
        if (spinOff)
        {
            speed = 0;
            spinOff = false;
        }

        //下まで来た時の処理
        if (gameObject.transform.position.y < endPos.y && !spinOff)
        {
            gameObject.transform.parent.GetComponent<CreateReel>().MarkBackStartPos(markNum, gameObject);
        }

        //リール回転時
        if (spinOn)
        {
            flash = false;
            renderer.material.color = new Color(1, 1, 1, 1);
            speed = markHeight*21*0.02f*80/60;
            spinOn = false;
        }

        gameObject.transform.Translate(0, -speed, 0);
        
        if (flash)
        {
            FlashMark();
        }
    }

    void FlashMark()
    {
        if (nextTime == 0)
        {
            nextTime = Time.time;
        }
        
        if (Time.time > nextTime)
        {
            if(renderer.material.color.b == 1)
            {
                renderer.material.color = new Color(1, 1, 0, 1);
            }
            else
            {
                renderer.material.color = new Color(1, 1, 1, 1);
            }

            nextTime += interval;
        }
    }
}
