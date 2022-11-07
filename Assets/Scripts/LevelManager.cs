using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public float waitToRespawn;

    public int gemsCollected;
    public float timeInLevel; 

    public string LevelToLoad;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        timeInLevel = 0f;
    }

    void Update()
    {
        timeInLevel += Time.deltaTime;
    }

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCo());
    }

    IEnumerator RespawnCo()
    {
        PlayerController.instance.gameObject.SetActive(false);

        yield return new WaitForSeconds(waitToRespawn - (1f / UIController.instance.fadeSpeed));

        UIController.instance.FadeToBlack();

        yield return new WaitForSeconds((1f / UIController.instance.fadeSpeed) + .2f);

        UIController.instance.FadeFromBlack();

        PlayerController.instance.gameObject.SetActive(true);

        PlayerController.instance.transform.position = CheckpointController.instance.spawnPoint;

        PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth;
    }

    public void EndLevel()
    {
        StartCoroutine(EndLevelCo());
    }

    public IEnumerator EndLevelCo()
    {
        PlayerController.instance.stopInput = true;

        CameraController.instance.stopFollow  = true;

        UIController.instance.LevelCompleteText.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        UIController.instance.FadeToBlack();

        yield return new WaitForSeconds((1f / UIController.instance.fadeSpeed) + .25f);

        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_unlocked", 1);
        PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);
               
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_gems", gemsCollected);
        PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_time", timeInLevel);


        SceneManager.LoadScene(LevelToLoad);
    }
}
