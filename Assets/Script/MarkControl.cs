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

    public CoinManager coinManager;

    public Vector3 startPos;
    public Vector3 endPos;
    
    public int markNum;
    private float markHeight;

    private void Start()
    {
        markHeight = gameObject.GetComponent<Renderer>().bounds.size.y;
        coinManager = transform.root.GetComponent<CoinManager>();
    }

    private void FixedUpdate()
    {
        //リール停止の処理
        if (spinOff)
        {
            speed = 0;
            spinOff = false;

            if (2.45 > gameObject.transform.position.y && gameObject.transform.position.y > 2.35)
            {
                switch (gameObject.transform.parent.name)
                {
                    case "Reel1":
                        coinManager.reel1_upper = this;
                        break;
                    case "Reel2":
                        coinManager.reel2_upper = this;
                        break;
                    case "Reel3":
                        coinManager.reel3_upper = this;
                        break;
                }
            }
            if (1.85 > gameObject.transform.position.y && gameObject.transform.position.y > 1.75)
            {

                switch (gameObject.transform.parent.name)
                {
                    case "Reel1":
                        coinManager.reel1_middle = this;
                        break;
                    case "Reel2":
                        coinManager.reel2_middle = this;
                        break;
                    case "Reel3":
                        coinManager.reel3_middle = this;
                        break;
                }
            }
            if (1.25 > gameObject.transform.position.y && gameObject.transform.position.y > 1.15)
            {
                switch (gameObject.transform.parent.name)
                {
                    case "Reel1":
                        coinManager.reel1_bottom = this;
                        break;
                    case "Reel2":
                        coinManager.reel2_bottom = this;
                        break;
                    case "Reel3":
                        coinManager.reel3_bottom = this;
                        break;
                }
            }
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
            speed = 0.23f;
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
