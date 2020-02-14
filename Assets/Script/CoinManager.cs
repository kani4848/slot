using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

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

    public CreateReel reel1;
    public CreateReel reel2;
    public CreateReel reel3;

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
        //Screen.SetResolution(1280, 720, true, 60);
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

            if (reel1.middleMark.name == reel2.middleMark.name && reel2.middleMark.name == reel3.middleMark.name)
            {
                Debug.Log("真ん中の列で当たり！");
                GiveReward(reel1.middleMark, reel2.middleMark, reel3.middleMark);
            }
            else if (reel1.upperMark.name == reel2.upperMark.name && reel2.upperMark.name == reel3.upperMark.name)
            {
                Debug.Log("上の列で当たり！");
                GiveReward(reel1.upperMark, reel2.upperMark, reel3.upperMark);
            }
            else if (reel1.bottomMark.name == reel2.bottomMark.name && reel2.bottomMark.name == reel3.bottomMark.name)
            {
                Debug.Log("下の列で当たり！");
                GiveReward(reel1.bottomMark , reel2.bottomMark , reel3.bottomMark);
            }
            else if (reel1.upperMark.name == reel2.middleMark.name && reel2.middleMark.name == reel3.bottomMark.name)
            {
                Debug.Log("左上から斜めの列で当たり！");
                GiveReward(reel1.upperMark , reel2.middleMark , reel3.bottomMark);
            }
            else if (reel1.bottomMark.name == reel2.middleMark.name && reel2.middleMark.name == reel3.upperMark.name)
            {
                Debug.Log("右上から斜めの列で当たり！");
                GiveReward(reel1.bottomMark , reel2.middleMark , reel3.upperMark);
            }
        }
    }

    

    private void GiveReward(GameObject reel1Mark, GameObject reel2Mark, GameObject reel3Mark)
    {
        string rewardMark = reel1Mark.GetComponent<MarkControl>().name;

        reel1Mark.GetComponent<MarkControl>().flash = true;
        reel2Mark.GetComponent<MarkControl>().flash = true;
        reel3Mark.GetComponent<MarkControl>().flash = true;

        yukarin.SetTrigger("toWin");
        switch (rewardMark)
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


