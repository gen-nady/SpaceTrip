using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class RevordedAds : MonoBehaviour
{
    public Text coinsT;
    public int coins;
    void Start()
    {
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize("3774257", false);
        }
    }
    public void ShowAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("rewardedVideo");
            coins = PlayerPrefs.GetInt("coins");
            coins += 30;
            PlayerPrefs.SetInt("coins", coins);
            coinsT.text = coins.ToString();
        }
    }
}
