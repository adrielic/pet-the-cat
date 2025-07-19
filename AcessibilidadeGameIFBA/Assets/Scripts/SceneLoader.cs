using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("Requirements")]
    public GameObject transitionCanvas, newTransition;

    public static SceneLoader Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        newTransition = Instantiate(transitionCanvas);
    }

    public IEnumerator Transition(string sceneName, float waitTime)
    {
        newTransition.GetComponent<Animator>().SetTrigger("End");
        yield return new WaitForSecondsRealtime(waitTime);
        SceneManager.LoadScene(sceneName);
    }

    public void LoadScene(string sceneName, float waitTime)
    {
        StartCoroutine(Transition(sceneName, waitTime));
    }
}