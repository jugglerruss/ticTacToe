using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class RewardScoreAds : MonoBehaviour
{
    [SerializeField] private string _rewardedUnitId;
    [SerializeField] private string _rewardedUnitIdTest;
    [SerializeField] private bool _isTest;

    private Game _game => FindObjectOfType<Game>();
    private RewardedAd _rewardedAd;

    public void ShowAd()
    {
        RequestInterstitial();
    }
    private void RequestInterstitial()
    {
        if (_rewardedAd != null)
            _rewardedAd.Destroy();

        _rewardedAd = new RewardedAd(_isTest ? _rewardedUnitIdTest : _rewardedUnitId);
        _rewardedAd.OnUserEarnedReward += EarnedReward;
        _rewardedAd.OnAdLoaded += HandOnAdLoaded;
        _rewardedAd.LoadAd(CreateNewRequest());
    }
    private AdRequest CreateNewRequest()
    {
        return new AdRequest.Builder().Build();
    }
    private void HandOnAdLoaded(object sender, EventArgs args)
    {
        if (_rewardedAd.IsLoaded())
            _rewardedAd.Show();
    }
    private void EarnedReward(object sender, Reward e)
    {
        _game.Continue(true);
    }
}
