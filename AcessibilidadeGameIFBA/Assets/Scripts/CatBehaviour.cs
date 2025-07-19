using UnityEngine;
using System.Collections.Generic;

public class CatBehaviour : MonoBehaviour
{
    [Header("Requirements")]
    public NeedsSystem needs;

    [Header("Feelings List")]
    public FeelingCatList[] catsByFeeling;

    [System.Serializable]
    public class FeelingCatList
    {
        public Feelings feeling;
        public GameObject[] catVariants;
    }

    private Dictionary<Feelings, GameObject[]> feelingCatDict;
    private Feelings lastFeeling, currentFeeling;
    public enum Feelings
    {
        Normal,
        Hungry,
        Satisfied,
        Sad,
        Happy,
        Sleepy,
        Dead
    }

    void Start()
    {
        feelingCatDict = new Dictionary<Feelings, GameObject[]>();

        foreach (var entry in catsByFeeling)
        {
            feelingCatDict[entry.feeling] = entry.catVariants;
        }

        lastFeeling = CurrentFeeling();
        HandleFeelingChange(lastFeeling);
    }

    void Update()
    {
        currentFeeling = CurrentFeeling();

        if (currentFeeling != lastFeeling)
        {
            Debug.Log("From: " + lastFeeling + " To: " + currentFeeling);
            HandleFeelingChange(currentFeeling);
            lastFeeling = currentFeeling;
        }
    }

    Feelings CurrentFeeling()
    {
        if (needs.Food == 0 && needs.Happiness == 0 && needs.Sleep == 0)
            return Feelings.Dead;

        if (needs.Food <= 3)
            return Feelings.Hungry;

        if (needs.Sleep <= 3)
            return Feelings.Sleepy;

        if (needs.Happiness <= 3)
            return Feelings.Sad;

        if (needs.Food >= 7)
            return Feelings.Satisfied;

        if (needs.Happiness >= 7)
            return Feelings.Happy;

        return Feelings.Normal;
    }

    void HandleFeelingChange(Feelings newFeeling)
    {
        foreach (var entry in feelingCatDict)
        {
            foreach (var cat in entry.Value)
            {
                if (cat != null)
                    cat.SetActive(false);
            }
        }

        if (feelingCatDict.TryGetValue(newFeeling, out GameObject[] options) && options.Length > 0)
        {
            GameObject chosen = options[Random.Range(0, options.Length)];

            if (chosen != null)
                chosen.SetActive(true);
        }
    }
}
