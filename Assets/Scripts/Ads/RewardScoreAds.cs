using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class RewardScoreAds : MonoBehaviour
{
    [SerializeField] private string _rewardedUnitId;

    private Game _game => FindObjectOfType<Game>();
    private RewardedAd _rewardedAd;
    private void OnEnable()
    {
        _rewardedAd = new RewardedAd(_rewardedUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        _rewardedAd.LoadAd(adRequest);
        _rewardedAd.OnUserEarnedReward += EarnedReward;
    }
    private void EarnedReward(object sender, Reward e)
    {
        _game.Continue(true);
    }
    public void ShowAd()
    {
        if (_rewardedAd.IsLoaded())
            _rewardedAd.Show();
    }
}
