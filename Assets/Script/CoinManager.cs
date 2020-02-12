using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public int payOut = 0;
    public int coinCount = 0;

    public Text coinCountUI;
    public Text payOutUI;
    public Text hukidasi1;
    public Text hukidasi2;

    public AudioSource coinSE;

    public Animator yukarin;

    public MarkControl reel1_upper;
    public MarkControl reel1_middle;
    public MarkControl reel1_bottom;

    public MarkControl reel2_upper;
    public MarkControl reel2_middle;
    public MarkControl reel2_bottom;

    public MarkControl reel3_upper;
    public MarkControl reel3_middle;
    public MarkControl reel3_bottom;

    public RotateStart rotateStart;

    private bool goCount = false;

    public Text totalCoinCountUI;
    public Text gameCountUI;
    public Text bingoCountUI;
    private int totalCoinCount;
    private float gameCount;
    private float bingoCount;

    public UIShield uiShield;
    public bool isBingo;

    public GameObject winEffect;
    
    private void Start()
    {
        Screen.SetResolution(1280, 720, false, 60);
        winEffect.SetActive(false);
    }

    private void Update()
    {
        if (rotateStart.pushed)
        {
            if (isBingo)
            {
                yukarin.SetTrigger("toIdle");
                isBingo = false;
            }

            winEffect.SetActive(false);
            if (!goCount)
            {
                goCount = true;

                gameCount++;
                gameCountUI.text = gameCount.ToString();
                payOutUI.text = "0";
                totalCoinCount -= 3;
                totalCoinCountUI.text = totalCoinCount.ToString();
                bingoCountUI.text = (bingoCount / gameCount * 100).ToString("N1");
            }
        }
        if (rotateStart.reelAllStoped)
        {
            goCount = false;
            rotateStart.reelAllStoped = false;

            if (reel1_middle.markName == reel2_middle.markName && reel2_middle.markName == reel3_middle.markName)
            {
                Debug.Log("真ん中の列で当たり！");
                GiveReward(reel1_middle.markName);
                reel1_middle.flash = true;
                reel2_middle.flash = true;
                reel3_middle.flash = true;
            }else if (reel1_upper.markName == reel2_upper.markName && reel2_upper.markName == reel3_upper.markName)
            {
                Debug.Log("上の列で当たり！");
                GiveReward(reel1_upper.markName);
                reel1_upper.flash = true;
                reel2_upper.flash = true;
                reel3_upper.flash = true;
            }else if (reel1_bottom.markName == reel2_bottom.markName && reel2_bottom.markName == reel3_bottom.markName)
            {
                Debug.Log("下の列で当たり！");
                GiveReward(reel1_bottom.markName);
                reel1_bottom.flash = true;
                reel2_bottom.flash = true;
                reel3_bottom.flash = true;
            }else if (reel1_upper.markName == reel2_middle.markName && reel2_middle.markName == reel3_bottom.markName)
            {
                Debug.Log("左上から斜めの列で当たり！");
                GiveReward(reel1_upper.markName);
                reel1_upper.flash = true;
                reel2_middle.flash = true;
                reel3_bottom.flash = true;
            }else if (reel1_bottom.markName == reel2_middle.markName && reel2_middle.markName == reel3_upper.markName)
            {
                Debug.Log("右上から斜めの列で当たり！");
                GiveReward(reel1_bottom.markName);
                reel1_bottom.flash = true;
                reel2_middle.flash = true;
                reel3_upper.flash = true;
            }
        }
    }

    

    private void GiveReward(string mark)
    {
        yukarin.SetTrigger("toWin");
        switch (mark)
        {
            case "bar":
                payOut = 50;
                break;

            case "bel":
                payOut = 40;
                break;

            case "bud":
                payOut = 3;
                break;

            case "che":
                payOut = 6;
                break;

            case "pie":
                payOut = 500;
                break;

            case "sai":
                payOut = 30;
                break;

            case "sev":
                payOut = 100;
                break;
        }
        coinCount += payOut;
        coinCountUI.text = coinCount.ToString();
        payOutUI.text = payOut.ToString();
        hukidasi1.text = payOut.ToString();
        hukidasi2.text = payOut.ToString();
        coinSE.Play();

        totalCoinCount += payOut;
        totalCoinCountUI.text = totalCoinCount.ToString();

        bingoCount++;
        bingoCountUI.text = (bingoCount / gameCount * 100).ToString("N2");


        uiShield.ShieldOn(1.0f);
        winEffect.SetActive(true);

        isBingo = true;
    }
}


