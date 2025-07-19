using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    [Header("Requirements")]
    public NeedsSystem needs;
    public GameObject foodFull;
    public GameObject foodEmpty;
    
    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit)
        {
            GameObject hitObject = hit.collider.gameObject;

            Debug.Log("Mouse over " + hitObject + ".");

            if (Input.GetMouseButtonDown(0))
            {
                if (hitObject.CompareTag("FoodObject"))
                {
                    if (hitObject == foodFull)
                    {
                        Debug.Log("Starts to eat.");
                        foodEmpty.SetActive(true);
                        foodFull.SetActive(false);
                        SceneLoader.Instance.LoadScene("Eating", 1f);
                    }
                    else
                    {
                        Debug.Log("Pours food.");
                        foodEmpty.SetActive(false);
                        foodFull.SetActive(true);
                    }   
                }
                else if (hitObject.CompareTag("HappinessObject"))
                {
                    Debug.Log("Minigame starts.");
                    SceneLoader.Instance.LoadScene("Playground", 1f);
                }
                else if (hitObject.CompareTag("SleepObject"))
                {
                    Debug.Log("Goes to sleep.");
                    SceneLoader.Instance.LoadScene("Sleeping", 1f);
                }
                else
                {
                    Debug.Log("You pet the cat.");

                    hit.collider.gameObject.GetComponent<Animator>().SetTrigger("Pet");
                    needs.Happiness += 1f;
                }
            }
        }
    }
}
