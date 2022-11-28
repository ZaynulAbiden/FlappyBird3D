using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class LoadingScreenManager : MonoBehaviour
{

    [SerializeField] Image LoadingBar;
    [SerializeField] GameObject ContinueBtn;
    [SerializeField] float LoadingBarTime=2;
    AsyncOperation loadScene;
    void Start()
    {
        LoadingBar.fillAmount = 0;
        ContinueBtn.SetActive(false);
        StartCoroutine(StartLoading());
    }

    IEnumerator StartLoading()
    {
        loadScene = SceneManager.LoadSceneAsync(1);
        loadScene.allowSceneActivation = false;
        while (LoadingBar.fillAmount < 1)
        {
            LoadingBar.fillAmount += Time.deltaTime/LoadingBarTime;
            yield return null;
        }
        while (loadScene.progress <= .89f)
        {
            print(loadScene.progress);
            yield return null;
        }
        ContinueBtn.SetActive(true);
    }

    public void LoadMainMenu()
    {
        loadScene.allowSceneActivation = true;

    }
}
