using UnityEngine;

public class GameStart : MonoBehaviour
{
    public void StartGame()
    {
        SceneLoader.Instance.LoadScene("Home", 1f);
    }
}
