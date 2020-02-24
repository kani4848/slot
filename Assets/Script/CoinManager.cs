using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class CoinManager : MonoBehaviour
{
    private List<GameObject> bingoMarkList = new List<GameObject>();
    public int payOut = 0;
    public int coinCount = 0;

    public Text coinCountUI;
    public Text payOutUI;

    public GameObject hukidashi;
    public Text hukidasi1;
    public Text hukidasi2;

    public AudioSource audioSource;
    public AudioClip bingoSE;
    public AudioClip coinSE;

    public Animator yukarin;

    public CreateReel reel1;
    public CreateReel reel2;
    public CreateReel reel3;

    public BetButtonControl betButton;

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
        hukidashi.SetActive(false);
    }

    private void Update()
    {
        if (betButton.pushed)
        {
            if (isBingo)
            {
                AnimatorClipInfo[] clipInfo = yukarin.GetCurrentAnimatorClipInfo(0);
                string clipName = clipInfo[0].clip.name;
                if (clipName == "yukarin_winIdle")
                {
                    yukarin.SetTrigger("toIdle");
                }
                isBingo = false;
                hukidashi.SetActive(false);
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
        if (betButton.reelAllStoped)
        {
            CheckBingo();
        }
    }

    private void CheckBingo()
    {
        goCount = false;
        betButton.reelAllStoped = false;

        if (reel2.upperMark.name != "che" && reel2.middleMark.name != "che" && reel2.bottomMark.name != "che")
        {
            if (reel1.upperMark.name == "che")
            {
                bingoMarkList.Add(reel1.upperMark);
                payOut += CheckReward("che");
                isBingo = true;
            }
            else if(reel1.middleMark.name == "che")
            {
                bingoMarkList.Add(reel1.middleMark);
                payOut += CheckReward("che");
                isBingo = true;
            }
            else if(reel1.bottomMark.name == "che")
            {
                bingoMarkList.Add(reel1.bottomMark);
                payOut += CheckReward("che");
                isBingo = true;
            }
        }

        if (reel1.middleMark.name == reel2.middleMark.name && reel2.middleMark.name == reel3.middleMark.name)
        {
            Debug.Log("真ん中の列で当たり！");
            payOut += CheckReward(reel1.middleMark.name);
            bingoMarkList.Add(reel1.middleMark);
            bingoMarkList.Add(reel2.middleMark);
            bingoMarkList.Add(reel3.middleMark);
            isBingo = true;
        }
        else if (reel1.upperMark.name == reel2.upperMark.name && reel2.upperMark.name == reel3.upperMark.name)
        {
            Debug.Log("上の列で当たり！");
            payOut += CheckReward(reel1.upperMark.name);
            bingoMarkList.Add(reel1.upperMark);
            bingoMarkList.Add(reel2.upperMark);
            bingoMarkList.Add(reel3.upperMark);
            isBingo = true;
        }
        else if (reel1.bottomMark.name == reel2.bottomMark.name && reel2.bottomMark.name == reel3.bottomMark.name)
        {
            Debug.Log("下の列で当たり！");
            payOut += CheckReward(reel1.bottomMark.name);
            bingoMarkList.Add(reel1.bottomMark);
            bingoMarkList.Add(reel2.bottomMark);
            bingoMarkList.Add(reel3.bottomMark);
            isBingo = true;
        }
        else if (reel1.upperMark.name == reel2.middleMark.name && reel2.middleMark.name == reel3.bottomMark.name)
        {
            Debug.Log("左上から斜めの列で当たり！");
            payOut += CheckReward(reel1.upperMark.name);
            bingoMarkList.Add(reel1.upperMark);
            bingoMarkList.Add(reel2.middleMark);
            bingoMarkList.Add(reel3.bottomMark);
            isBingo = true;
        }
        else if (reel1.bottomMark.name == reel2.middleMark.name && reel2.middleMark.name == reel3.upperMark.name)
        {
            Debug.Log("右上から斜めの列で当たり！");
            payOut += CheckReward(reel1.bottomMark.name);
            bingoMarkList.Add(reel1.bottomMark);
            bingoMarkList.Add(reel2.middleMark);
            bingoMarkList.Add(reel3.upperMark);
            isBingo = true;
        }

        if (isBingo)
        {
            GiveReward();
        }
    }

    private int CheckReward(string bingoName)
    {
        int reward = 0;

        switch (bingoName)
        {
            case "bar":
                reward = 50;
                break;

            case "bel":
                reward = 40;
                break;

            case "bud":
                reward = 3;
                break;

            case "che":
                reward = 1;
                break;

            case "pie":
                reward = 500;
                break;

            case "sai":
                reward = 30;
                break;

            case "sev":
                reward = 100;
                break;
        }
        return reward;
    }

    private void GiveReward()
    {
        foreach(GameObject bingoMark in bingoMarkList)
        {
            bingoMark.GetComponent<MarkControl>().flash = true;
        }
        if (payOut >1)
        {
            yukarin.SetTrigger("toWin");
            winEffect.SetActive(true);
            audioSource.PlayOneShot(bingoSE);
        }
        else
        {
            audioSource.PlayOneShot(coinSE);
        }
        
  
        coinCount += payOut;
        coinCountUI.text = coinCount.ToString();
        payOutUI.text = payOut.ToString();

        hukidashi.SetActive(true);
        hukidasi1.text = payOut.ToString();
        hukidasi2.text = payOut.ToString();
        
        totalCoinCount += payOut;
        totalCoinCountUI.text = totalCoinCount.ToString();

        bingoCount++;
        bingoCountUI.text = (bingoCount / gameCount * 100).ToString("N2");

        bingoMarkList.Clear();
        uiShield.ShieldOn(1.0f);
        payOut = 0;
        isBingo = true;
    }
}


