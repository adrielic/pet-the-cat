using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Requirements")]
    public NeedsSystem needs;

    [Header("Needs System")]
    public GameObject needsPanel;
    public Image foodBar;
    public Image happinessBar;
    public Image sleepBar;
    public TMP_Text foodTxt;
    public TMP_Text happinessTxt;
    public TMP_Text sleep;

    [Header("Accessibility Setting")]
    public Button accessibilityButton;
    public Sprite[] accessibilityButtonImg;

    private bool accessibilityOn;

    public static UIManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void OnEnable()
    {
        needs.OnFoodChanged += UpdateFoodDisplay;
        needs.OnHappinessChanged += UpdateHappinessDisplay;
        needs.OnSleepChanged += UpdateSleepDisplay;
    }

    void OnDisable()
    {
        needs.OnFoodChanged -= UpdateFoodDisplay;
        needs.OnHappinessChanged -= UpdateHappinessDisplay;
        needs.OnSleepChanged -= UpdateSleepDisplay;
    }

    void Start()
    {
        float f = needs.Food;
        float h = needs.Happiness;
        float s = needs.Sleep;

        foodTxt.text = f.ToString("0");
        happinessTxt.text = h.ToString("0");
        sleep.text = s.ToString("0");

        foodBar.fillAmount = f / 10f;
        happinessBar.fillAmount = h / 10f;
        sleepBar.fillAmount = s / 10f;
    }

    void UpdateFoodDisplay(float newValue)
    {
        foodTxt.text = newValue.ToString("0");
        foodBar.fillAmount = newValue / 10f;
    }

    void UpdateHappinessDisplay(float newValue)
    {
        happinessTxt.text = newValue.ToString("0");
        happinessBar.fillAmount = newValue / 10f;
    }

    void UpdateSleepDisplay(float newValue)
    {
        sleep.text = newValue.ToString("0");
        sleepBar.fillAmount = newValue / 10f;
    }

    public void Accessibility()
    {
        accessibilityOn = !accessibilityOn;

        if (!accessibilityOn)
        {
            accessibilityButton.GetComponent<Image>().sprite = accessibilityButtonImg[0];
            needsPanel.GetComponent<Animator>().SetBool("AccessibilityOn", false);
        }
        else
        {
            accessibilityButton.GetComponent<Image>().sprite = accessibilityButtonImg[1];
            needsPanel.GetComponent<Animator>().SetBool("AccessibilityOn", true);
        }

        needsPanel.GetComponent<Animator>().SetTrigger("Scale");
    }
}
