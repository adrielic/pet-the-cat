using UnityEngine;

public class HomeReturn : MonoBehaviour
{
    public void ReturnToHome()
    {
        SceneLoader.Instance.LoadScene("Home", 1f);
    }
}
