using System;
using System.Collections.Generic;
using UnityEngine;

public enum ECheat {
    GOD_MODE,
    ADD_SCORE,
    ADD_TIME,
    RESET_HI_SCORE,
    NEXT_SESSION,
    PREVIUS_SESSION,
    AMOUNT_TIME_PER_LEVEL
}

public class CheatManager : MonoBehaviour
{
    public static int ADD_TIMER_IN_SECENDS = 12;
    public static int ADD_SCORE_VALUE = 100;
    private const float DELAY_MIN = 0.1f;
    private const float DELAY_MAX = 5.0f;

    private Dictionary<ECheat, Action> m_Events = new Dictionary<ECheat, Action>();
    private event Func<int, int> m_CheatEventAmountTimePerLevel;
    private int m_AmoutTimePerLevel = GameController.AMOUT_TIME_PER_LEVEL_DEFAULT;


    private bool m_GodMode = false;
    private bool m_IninitJump = false;

    private bool m_IsCloseMenuCheat = false;
    private Rect m_WinRect = new Rect(0, 0, 0, 0);

    private static CheatManager m_Instance;
    public static CheatManager Instance { 
        get { 
            if (m_Instance == null)
            {
                GameObject go = new GameObject("CheatManager");
                go.AddComponent<CheatManager>();
            }

            return m_Instance; 
        }
    }

    private void Awake()
    {
        if(m_Instance != null) Destroy(m_Instance);
        m_Instance = this;
    }

    private void OnGUI()
    {
        GUI.backgroundColor = Color.blue;

        m_WinRect = GUI.Window(0, m_WinRect, DrawWindow, "Cheat");
    }

    void DrawWindow(int windowID)
    {
        if(m_IsCloseMenuCheat) DrawEmptyBody();
        else DrawFullBody();
        GUI.DragWindow();
    }

    private void DrawEmptyBody()
    {
        float hWidth = 100;
        float vWidth = 300;
        m_WinRect.height = 60f;
        m_WinRect.width = hWidth;
        GUILayout.BeginArea(new Rect(5, 20, hWidth, vWidth));
        if (GUI.Button(new Rect(20, 10, 50, 20), "Open")) ToggleMenuCheat();
        GUILayout.EndArea();
    }

    private void DrawFullBody() 
    {
        float hWidth = 100;
        float vWidth = 300;
        m_WinRect.height = vWidth;
        m_WinRect.width = hWidth;
        GUILayout.BeginArea(new Rect(5, 10, hWidth, Screen.height));
        GUILayout.BeginVertical();
        {
            m_GodMode = GUILayout.Toggle(m_GodMode, "God Mode");
            m_IninitJump = GUILayout.Toggle(m_IninitJump, "Infinit Jump");
        }
        {
            if (GUILayout.Button("Add Score")) m_Events[ECheat.ADD_SCORE]?.Invoke();
            if (GUILayout.Button("Add Time")) m_Events[ECheat.ADD_TIME]?.Invoke();
            if (GUILayout.Button("Reset Hi-Score")) m_Events[ECheat.RESET_HI_SCORE]?.Invoke();
        }
        GUILayout.Label("Session:");
        {
            if (GUILayout.Button("Next")) m_Events[ECheat.NEXT_SESSION]?.Invoke();
            if (GUILayout.Button("Previous")) m_Events[ECheat.PREVIUS_SESSION]?.Invoke();
        }
        {
            GUILayout.Label("Amount time per level: "+ m_AmoutTimePerLevel.ToString("00"));
            if(m_CheatEventAmountTimePerLevel != null)
            {
                float newAmoutTime = GUILayout.HorizontalSlider(m_AmoutTimePerLevel, 1, 24);
                m_AmoutTimePerLevel = m_CheatEventAmountTimePerLevel.Invoke(Mathf.RoundToInt(newAmoutTime));
            }
        }

        if (GUI.Button(new Rect(20, vWidth - 35, 50, 20), "Close")) ToggleMenuCheat();

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

    private void ToggleMenuCheat()
    {
        m_IsCloseMenuCheat = !m_IsCloseMenuCheat;
    }

    public bool InGodMode()
    {
        return m_GodMode;
    }

    public bool InInfinitJump()
    {
        return m_IninitJump;
    }


    public void SubscribeEvent(ECheat cheatId, Action action) 
    {
        if (m_Events.ContainsKey(cheatId)) m_Events[cheatId] += action;
        else m_Events.Add(cheatId, action);
    }

    public void SubscribeEvent(Func<int, int> func)
    {
        m_CheatEventAmountTimePerLevel += func;
    }

    private void InvokeEvent(ECheat cheatId)
    {
        m_Events[cheatId]?.Invoke();
    }
}
