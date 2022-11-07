using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LSUIManager : MonoBehaviour
{
    public static LSUIManager instance;

    public Image fadeScreen;
    public float fadeSpeed;
    private bool shouldFadeToBlack, shouldFadeFromBlack;

    public GameObject levelInfoPanel;

    public Text levelName, gemsCount, gemsInLevel, timeCount, timeTarget;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        FadeFromBlack();
    }

    void Update()
    {
        if (shouldFadeFromBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 0f)
            {
                shouldFadeFromBlack = false;
            }
        }
    }

    public void FadeToBlack()
    {
        shouldFadeToBlack = true;
        shouldFadeFromBlack = false;
    }

    public void FadeFromBlack()
    {
        shouldFadeToBlack = false;
        shouldFadeFromBlack = true;
    }

    public void ShowInfo(MapPoint levelInfo)
    {
        levelName.text = levelInfo.name;

        gemsCount.text = levelInfo.gemsCollected.ToString();
        gemsInLevel.text = levelInfo.totalGems.ToString();

        timeTarget.text = "Target: " + levelInfo.targetTime + "s";

        if(levelInfo.bestTime == 0)
        {
            timeCount.text = "Best: ---";
        }else
        {
        timeCount.text = "Best: " + levelInfo.bestTime.ToString("F2") + "s";
        }

        levelInfoPanel.SetActive(true);
    }

    public void HideInfo()
    {
        levelInfoPanel.SetActive(false);
    }
}
