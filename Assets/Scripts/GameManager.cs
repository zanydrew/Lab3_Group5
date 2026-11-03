using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// STARTER — Score and win condition for the "Orb Collector" lab goal.
/// Create an empty GameObject named "GameManager" and attach this script.
///
/// ALREADY WORKS: a public Collect() method the Player calls when it grabs
/// an orb, plus an OnGUI() label showing the live score in the top-left corner.
///
/// YOUR LAB TASKS:
///   TODO 6: in Awake(), find all existing orbs with GameObject.FindGameObjectsWithTag
///           and set totalOrbs from the result.
///   TODO 7: in OnGUI(), when collected == totalOrbs show a big centered
///           "YOU WIN" message instead of the score line.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("State")]
    [SerializeField] private int totalOrbs = 5;

    private int collected;

    void Awake()
    {
        // Simple singleton so the player can call GameManager.Instance.Collect().
        Instance = this;

        // TODO 6 (done): count the orbs that exist in the scene right now.
        // Falls back to the serialized value if none are tagged "Orb".
        GameObject[] orbs = GameObject.FindGameObjectsWithTag("Orb");
        if (orbs.Length > 0) totalOrbs = orbs.Length;
        collected = 0;
        Debug.Log($"[GameManager] Found {totalOrbs} orbs.");
    }

    public void Collect()
    {
        collected++;
        Debug.Log($"[GameManager] Collected {collected}/{totalOrbs}");
    }

void OnGUI()
{
    GUIStyle style = new GUIStyle(GUI.skin.label)
    {
        fontSize = 28,
        fontStyle = FontStyle.Bold
    };
    style.normal.textColor = Color.yellow;

    if (collected >= totalOrbs)
    {
        GUIStyle win = new GUIStyle(style)
        {
            fontSize = 64,
            alignment = TextAnchor.MiddleCenter
        };
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "YOU WIN!", win);
        return;
    }

    GUI.Box(new Rect(8, 8, 360, 44), GUIContent.none);
    GUI.Label(new Rect(16, 12, 350, 40), $"Orbs collected: {collected} / {totalOrbs}", style);
}
}
