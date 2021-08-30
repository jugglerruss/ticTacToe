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

    private void Start()
    {
        RequestInterstitial();
    }
    public void ShowAd()
    {
        if (_interstitialAd.IsLoaded())
            _interstitialAd.Show();
        else
            _interstitialAd.OnAdLoaded += HandOnAdLoaded;
    }
    private void RequestInterstitial()
    {
        if (_interstitialAd != null)
            _interstitialAd.Destroy();

        _interstitialAd = new InterstitialAd(_isTest ? _interstitialUnitIdTest : _interstitialUnitId);
        _interstitialAd.OnAdClosed += ContinueGame;
        _interstitialAd.OnAdFailedToLoad += HandOnFailedToLoad;
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
    private void HandOnFailedToLoad(object sender, EventArgs args)
    {
        Continue(false);
    }
    private void ContinueGame(object sender, EventArgs args)
    {
        Continue(true);
    }
    private void Continue(bool success)
    {
        RequestInterstitial();
        _game.LoseScore();
        _game.Continue(success);
    }
}
