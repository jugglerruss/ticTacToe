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

    private void Start()
    {
        RequestInterstitial();
    }
    public void ShowAd()
    {
        if (_rewardedAd.IsLoaded())
            _rewardedAd.Show();
        else
            _rewardedAd.OnAdLoaded += HandOnAdLoaded;
    }
    private void RequestInterstitial()
    {
        if (_rewardedAd != null)
            _rewardedAd.Destroy();

        _rewardedAd = new RewardedAd(_isTest ? _rewardedUnitIdTest : _rewardedUnitId);
        _rewardedAd.OnUserEarnedReward += EarnedReward;
        _rewardedAd.OnAdFailedToLoad += HandOnFailedToLoad;
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
        RequestInterstitial();
        _game.Continue(true);
    }
    private void HandOnFailedToLoad(object sender, EventArgs args)
    {
        RequestInterstitial();
        _game.LoseScore();
        _game.Continue(false);
    }
}
