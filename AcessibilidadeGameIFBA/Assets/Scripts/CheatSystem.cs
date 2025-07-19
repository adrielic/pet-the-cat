using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatSystem : MonoBehaviour
{
    [Header("Requirements")]
    public NeedsSystem needs;

    private string command = "";
    private float clearTime = 5f;
    private float timer;
    private Dictionary<string, System.Action> cheatCommands;

    void Start()
    {
        cheatCommands = new Dictionary<string, System.Action>()
        {
            { "afoo", () => needs.Food++ },
            { "ahap", () => needs.Happiness++ },
            { "asle", () => needs.Sleep++ },
            { "dfoo", () => needs.Food-- },
            { "dhap", () => needs.Happiness-- },
            { "dsle", () => needs.Sleep-- },
            { "normal", () => Time.timeScale = 1f },
            { "fast", () => Time.timeScale = 8f },
            { "stop", () => Time.timeScale = 0f }
        };
    }

    void Update()
    {
        foreach (char c in Input.inputString.ToLower())
        {
            if (c == '\b')
            {
                if (command.Length > 0)
                    command = command.Substring(0, command.Length - 1);
            }
            else if (c == '\n' || c == '\r')
            {
                ExecuteCommand();
                command = "";
                timer = 0;
            }
            else if (char.IsLetter(c))
            {
                command += c;
                timer = 0;
            }
        }

        if (command.Length > 0)
        {
            timer += Time.unscaledDeltaTime;
            if (timer > clearTime)
            {
                command = "";
                timer = 0;
            }
        }
    }

    void ExecuteCommand()
    {
        if (cheatCommands.TryGetValue(command, out var action))
        {
            action.Invoke();
        }
        else
        {
            Debug.LogWarning($"Command '{command}' invalid.");
        }
    }
}
