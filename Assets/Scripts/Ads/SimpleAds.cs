using UnityEngine;
using UnityEngine.Advertisements;

public class SimpleAds : MonoBehaviour
{
    private void Start()
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
            Advertisement.Show("video");
        }
    }
}
