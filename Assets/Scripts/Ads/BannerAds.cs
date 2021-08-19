using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class BannerAds : MonoBehaviour
{
    [SerializeField] private string _banerUnitId;
    private BannerView _bannerAd;
    
    private void OnEnable()
    {
        _bannerAd = new BannerView(_banerUnitId, AdSize.Banner, AdPosition.Top);
        AdRequest adRequest = new AdRequest.Builder().Build();
        _bannerAd.LoadAd(adRequest);
        StartCoroutine(ShowBanner());
    }
    private IEnumerator ShowBanner()
    {
        yield return new WaitForSeconds(3);
        _bannerAd.Show();
    }
}
