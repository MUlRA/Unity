using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleAutoSwap : MonoBehaviour
{
    private static ReticleAutoSwap instance = null;
    public static ReticleAutoSwap Instance

{
    get { return instance; }
}



    public GameObject Standard;
    public GameObject Scatter;
    public GameObject Sniper;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;

        }
        else
        {
            instance = this;
        }
    }
        public void ReticleSwap()
    {
        if (Shipmovement.Instance.LoadoutModifier == 0)
        {
            Standard.SetActive (true);
            Scatter.SetActive(false);
            Sniper.SetActive(false);
        }
        if (Shipmovement.Instance.LoadoutModifier == 1)
        {
            Standard.SetActive(false);
            Scatter.SetActive(true);
            Sniper.SetActive(false);
        }
        if (Shipmovement.Instance.LoadoutModifier == 2)
        {
            Standard.SetActive(false);
            Scatter.SetActive(false);
            Sniper.SetActive(true);
        }
    }
}
