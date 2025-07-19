using System;
using System.Collections;
using UnityEngine;

public class NeedsSystem : MonoBehaviour
{
    public event Action<float> OnFoodChanged, OnHappinessChanged, OnSleepChanged;
    public Coroutine needsDecay;

    void Start()
    {
        needsDecay = StartCoroutine(NeedsDecay());
    }

    public float Food
    {
        get => Needs.food;

        set
        {
            Needs.food = Mathf.Clamp(value, 0, 10f);
            OnFoodChanged?.Invoke(Needs.food);
        }
    }

    public float Happiness
    {
        get => Needs.happiness;

        set
        {
            Needs.happiness = Mathf.Clamp(value, 0, 10f);
            OnHappinessChanged?.Invoke(Needs.happiness);
        }
    }

    public float Sleep
    {
        get => Needs.sleep;

        set
        {
            Needs.sleep = Mathf.Clamp(value, 0, 10f);
            OnSleepChanged?.Invoke(Needs.sleep);
        }
    }

    private IEnumerator NeedsDecay()
    {
        while (true)
        {
            yield return new WaitForSeconds(60f);

            Food -= 1f;
            Happiness -= 1f;
            Sleep -= 0.1f;
        }
    }
}
