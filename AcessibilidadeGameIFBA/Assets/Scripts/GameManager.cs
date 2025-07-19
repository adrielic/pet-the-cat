using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    DateTime currentTime = DateTime.Now;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        GameData.LoadData();
    }

    void OnEnable()
    {
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    void OnDisable()
    {
        SceneManager.activeSceneChanged -= OnSceneChanged;
    }

    void OnApplicationQuit()
    {
        GameData.SaveData();
    }

    void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            GameData.SaveData();
        }
    }

    void OnSceneChanged(Scene oldScene, Scene newScene)
    {
        StopAllCoroutines();

        switch (newScene.name)
        {
            case "Eating":
                StartCoroutine(IncreaseFood());
                break;
            case "Playground":
                Screen.orientation = ScreenOrientation.LandscapeLeft;
                StartCoroutine(IncreaseHappiness());
                break;
            case "Sleeping":
                StartCoroutine(IncreaseSleep());
                break;
        }
    }

    public void MinigameOver()
    {
        Debug.Log("Game Over!");
        Screen.orientation = ScreenOrientation.Portrait;
        SceneLoader.Instance.LoadScene("Home", 1f);
    }

    private IEnumerator IncreaseFood()
    {
        while (Needs.food < 10f)
        {
            yield return new WaitForSeconds(1f);
            Needs.food = Mathf.Clamp(Needs.food + 1f, 0f, 10f);
        }

        SceneLoader.Instance.LoadScene("Home", 1f);
    }

    private IEnumerator IncreaseHappiness()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            Needs.happiness = Mathf.Clamp(Needs.happiness + 1f, 0f, 10f);
            Needs.sleep = Mathf.Clamp(Needs.sleep - 0.5f, 0f, 10f);
        }
    }

    private IEnumerator IncreaseSleep()
    {
        while (Needs.sleep < 10f && Needs.food > 0)
        {
            yield return new WaitForSeconds(1f);
            Needs.sleep = Mathf.Clamp(Needs.sleep + 0.1f, 0f, 10f);
            Needs.food = Mathf.Clamp(Needs.food - 0.1f, 0f, 10f);
        }

        SceneLoader.Instance.LoadScene("Home", 1f);
    }
}
