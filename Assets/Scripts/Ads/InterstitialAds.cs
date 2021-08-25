using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class InterstitialAds : MonoBehaviour
{
    [SerializeField] private string _interstitialUnitId;
    [SerializeField] private string _interstitialUnitIdTest;
    [SerializeField] private bool _isTest;

    private Game _game => FindObjectOfType<Game>();
    private InterstitialAd _interstitialAd;

    public void ShowAd()
    {
        RequestInterstitial();
    }
    private void RequestInterstitial()
    {
        if (_interstitialAd != null)
            _interstitialAd.Destroy();

        _interstitialAd = new InterstitialAd(_isTest ? _interstitialUnitIdTest : _interstitialUnitId);
        _interstitialAd.OnAdLoaded += HandOnAdLoaded;
        _interstitialAd.OnAdClosed += ContinueGame;
        _interstitialAd.LoadAd(CreateNewRequest());
    }
    private AdRequest CreateNewRequest()
    {
        return new AdRequest.Builder().Build();
    }
    private void HandOnAdLoaded(object sender,EventArgs args)
    {
        if (_interstitialAd.IsLoaded())
            _interstitialAd.Show();
    }
    private void ContinueGame(object sender, EventArgs args)
    {
        _game.LoseScore();
        _game.Continue(true);
    }
}
