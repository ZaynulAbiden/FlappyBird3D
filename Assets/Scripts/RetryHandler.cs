using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RetryHandler : MonoBehaviour
{
    float timer=5;
    public Text timerTxt;
    public Text availableGemsTxt;
    public Text requireGemsTxt;
    public Button UseGemsBtn;
    public Button WatchAdBtn;
    bool adButtonClicked;

    public void OnEnable()
    {
        timer = 5;
        adButtonClicked = false;
        StartCoroutine(InvokeRetryPanel());
        availableGemsTxt.text ="Available Gems "+ GameManager.instance.Gems;
        requireGemsTxt.text = "1 Gem Needed";
        if (GameManager.instance.Gems <=0)
            UseGemsBtn.interactable = false;
    }

    public void WatchAd()
    {  
        AdMobManager.instance.ShowRewardedVideo();
        adButtonClicked = true;
    }

    public void CloseRetry()
    {
        GameManager.instance.GameOver();
        gameObject.SetActive(false);
    }
    IEnumerator InvokeRetryPanel()
    {
        while (timer >  1 && !adButtonClicked)
        {
            timerTxt.text = Mathf.Ceil( timer)+"?";
            timer -= Time.deltaTime;
            yield return null;
        }
        CloseRetry();
    }
}
