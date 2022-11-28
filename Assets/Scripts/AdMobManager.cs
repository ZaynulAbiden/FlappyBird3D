using GoogleMobileAds.Api;
using UnityEngine;

public class AdMobManager : MonoBehaviour
{
    #region Singleton
    public static AdMobManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
    }
    #endregion
    private BannerView bannerView;
    public InterstitialAd interstitial;
    public RewardedAd rewardedAd;

    public static int rewardType=2;
    string rewardedAd_ID = "ca-app-pub-3940256099942544/6300978111";
    public void Start()
    {
        MobileAds.Initialize(initStatus => { });
    }
  
    private void RequestRewardedAd()
    {
        rewardedAd = new RewardedAd(rewardedAd_ID);
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        AdRequest request = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(request);
    }

    public void ShowRewardedVideo()
    {
        RequestRewardedAd();
        if (rewardedAd.IsLoaded())
            rewardedAd.Show();
        rewardType = 1;
    }
    public void HandleUserEarnedReward(object sender, Reward args)
    {
        if(rewardType == 1)
        {
            GameManager.instance.SetGems(+1);
            GameManager.instance.ActivatePlayer(1);
        }
        rewardType = 2;
    }
}