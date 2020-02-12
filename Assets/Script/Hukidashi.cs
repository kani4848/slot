using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hukidashi : MonoBehaviour
{
    public GameObject hukidasi;

    void HukidasiOn()
    {
        hukidasi.SetActive(true);
    }

    void HukidasiOff()
    {
        hukidasi.SetActive(false);
    }

}
