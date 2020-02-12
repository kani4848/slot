using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateReel : MonoBehaviour
{
    public Dictionary<int,GameObject> markDic = new Dictionary<int,GameObject>();
    public GameObject mark;
    public float markHeight;
    public int reelNum;
    string[] reel1 = { "bar", "bud", "sai", "bud", "bel", "sev", "sai", "bud", "sai", "bud", "bar", "che", "bud", "sai", "bud", "sev", "pie", "bud", "sai", "bud", "che" };
    string[] reel2 = { "sai", "bel", "bud", "che", "sai", "sev", "bud", "che", "sai", "bel", "bud", "che", "sai", "bar", "bud", "che", "pie", "sai", "sev", "bud", "che" };
    string[] reel3 = { "bud", "pie", "bel", "sai", "bud", "sev", "bar", "bel", "sai", "bud", "pie", "bel", "sai", "bud", "pie", "bel", "sai", "bud", "pie", "bel", "sai" };

    void Start()
    {
        markHeight = mark.GetComponent<Renderer>().bounds.size.y;
        switch (reelNum)
        {
            case 1:
                ReelCreator(reel1);
                break;
            case 2:
                ReelCreator(reel2);
                break;
            case 3:
                ReelCreator(reel3);
                break;
        }
    }

    private void ReelCreator(string[] reel)
    {
        for (int i = 0; i < reel.Length; i++)
        {
            GameObject markIns = Instantiate(mark);
            markDic.Add(i,markIns);
            markIns.name = "Reel_"+reelNum+"_"+reel[i]+"_"+i;
            markIns.transform.parent = gameObject.transform;
            MarkControl markContorol = markIns.GetComponent<MarkControl>();
            markContorol.markNum = i;
            markContorol.startPos = new Vector3(gameObject.transform.position.x, Mathf.Ceil(reel.Length / 2) * markHeight, 0);
            markContorol.endPos = markContorol.startPos - new Vector3(0,markHeight*(reel.Length),0);

            markIns.transform.position = markContorol.startPos - new Vector3(0, markHeight * i, 0);

            markIns.GetComponent<MarkControl>().markName = reel[i];

            switch (reel[i])
            {
                case "bar":
                    markIns.GetComponent<SpriteRenderer>().sprite = Resources.Load("Image/mark_bar", typeof(Sprite)) as Sprite;
                    break;
                case "bud":
                    markIns.GetComponent<SpriteRenderer>().sprite = Resources.Load("Image/mark_budo", typeof(Sprite)) as Sprite;
                    break;
                case "che":
                    markIns.GetComponent<SpriteRenderer>().sprite = Resources.Load("Image/mark_chelly", typeof(Sprite)) as Sprite;
                    break;
                case "pie":
                    markIns.GetComponent<SpriteRenderer>().sprite = Resources.Load("Image/mark_piero", typeof(Sprite)) as Sprite;
                    break;
                case "sai":
                    markIns.GetComponent<SpriteRenderer>().sprite = Resources.Load("Image/mark_sai_2", typeof(Sprite)) as Sprite;
                    break;
                case "sev":
                    markIns.GetComponent<SpriteRenderer>().sprite = Resources.Load("Image/mark_seven", typeof(Sprite)) as Sprite;
                    break;
                case "bel":
                    markIns.GetComponent<SpriteRenderer>().sprite = Resources.Load("Image/mark_bell", typeof(Sprite)) as Sprite;
                    break;
            }
        }
    }

    public void getMarkStopPos()
    {
        float maxPos = 0;
        int maxNum = 0;
        

        for (int i = 0 ; i < reel1.Length;i++)
        {
            if(maxPos < markDic[i].transform.position.y)
            {
                maxPos = markDic[i].transform.position.y;
                maxNum = i;
            }
        }

        if (markDic[maxNum].transform.position.y != markDic[maxNum].GetComponent<MarkControl>().startPos.y)
        {
            maxNum -= 1;
            if (maxNum < 0)
            {
                maxNum = reel1.Length - 1;
            }

        }

        for (int i = 0; i < reel1.Length; i++)
        {

            MarkControl markControl = markDic[i].GetComponent<MarkControl>();
            int stopNum = i - maxNum;
            if(stopNum >= 0)
            {
                markDic[i].transform.position = markControl.startPos - new Vector3(0, Mathf.Abs(i - maxNum) * markHeight, 0);
            }
            else
            {
                markDic[i].transform.position = markControl.startPos - new Vector3(0, Mathf.Abs(reel1.Length + stopNum) * markHeight, 0);
            }
            markControl.spinOff = true;
            markControl.spinOn = false;
        }
    }

    public void MarkBackStartPos(int i,GameObject mark)
    {
        int num = i + 1;
        if(num > reel1.Length - 1)
        {
            num = 0;
        }
        mark.transform.position =markDic[num].transform.position + new Vector3(0,markHeight,0);
    }
}