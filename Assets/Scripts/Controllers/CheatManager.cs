using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings.SplashScreen;

public enum ECheat {
    GOD_MODE,
    ADD_SCORE,
    ADD_TIME,
    RESET_HI_SCORE,
    NEXT_SESSION,
    PREVIUS_SESSION
}

public class CheatManager : MonoBehaviour
{
    public static int ADD_TIMER_IN_SECENDS = 10;
    public static int ADD_SCORE_VALUE = 100;
    private const float DELAY_MIN = 0.1f;
    private const float DELAY_MAX = 5.0f;

    private Dictionary<ECheat, Action> m_Events = new Dictionary<ECheat, Action>();


    private bool m_GodMode = false;
    float m_SpawnEnemyGroundMin = 0.1f;
    float m_SpawnEnemyGroundMax = 3f;
    float m_SpawnEnemyFlyMin = 1;
    float m_SpawnEnemyFlyMax = 4;
    float m_SpawnCoin1Min = 0.5f;
    float m_SpawnCoin1Max = 5;
    float m_SpawnCoin2Min = 2f;
    float m_SpawnCoin2Max = 2.5f;

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
        float hWidth = Screen.width / 8.0f;
        m_WinRect.height = 60f;
        m_WinRect.width = hWidth;
        GUILayout.BeginArea(new Rect(0, 20, hWidth, Screen.height));
        if (GUILayout.Button("Open")) ToggleMenuCheat();
        GUILayout.EndArea();
    }

    private void DrawFullBody() 
    {
        float hWidth = Screen.width / 8.0f;
        m_WinRect.height = Screen.height;
        m_WinRect.width = hWidth;
        GUILayout.BeginArea(new Rect(0, 20, hWidth, Screen.height));
        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        {
            m_GodMode = GUILayout.Toggle(m_GodMode, "GodMode");
        }
        GUILayout.EndHorizontal();
        {
            if (GUILayout.Button("Add Score")) m_Events[ECheat.ADD_SCORE]?.Invoke();
            if (GUILayout.Button("Add Time")) m_Events[ECheat.ADD_TIME]?.Invoke();
            if (GUILayout.Button("Reset Hi-Score")) m_Events[ECheat.RESET_HI_SCORE]?.Invoke();
            if (GUILayout.Button("Next Session")) m_Events[ECheat.NEXT_SESSION]?.Invoke();
            if (GUILayout.Button("Previous Session")) m_Events[ECheat.PREVIUS_SESSION]?.Invoke();
        }
        {
            GUILayout.Label("Delay Spaw Ground Enemy Range: ");
            m_SpawnEnemyGroundMin = DrawSlider("Min", m_SpawnEnemyGroundMin, DELAY_MIN, m_SpawnEnemyGroundMax);
            m_SpawnEnemyGroundMax = DrawSlider("Max", m_SpawnEnemyGroundMax, m_SpawnEnemyGroundMin);
        }
        {
            GUILayout.Label("Delay Spaw Fly Enemy Range: ");
            m_SpawnEnemyFlyMin = DrawSlider("Min", m_SpawnEnemyFlyMin, DELAY_MIN, m_SpawnEnemyFlyMax);
            m_SpawnEnemyFlyMax = DrawSlider("Max", m_SpawnEnemyFlyMax, m_SpawnEnemyFlyMin);
        }
        {
            GUILayout.Label("Delay Spaw Coin 1 Range: ");
            m_SpawnCoin1Min = DrawSlider("Min", m_SpawnCoin1Min, DELAY_MIN, m_SpawnCoin1Max);
            m_SpawnCoin1Max = DrawSlider("Max", m_SpawnCoin1Max, m_SpawnCoin1Min);
        }
        {
            GUILayout.Label("Delay Spaw Coin 2 Range: ");
            m_SpawnCoin2Min = DrawSlider("Min", m_SpawnCoin2Min, DELAY_MIN, m_SpawnCoin2Max);
            m_SpawnCoin2Max = DrawSlider("Max", m_SpawnCoin2Max, m_SpawnCoin2Min);
        }

        if (GUILayout.Button("Close")) ToggleMenuCheat();

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

    private float DrawSlider(String label, float value, float valueMin = DELAY_MIN, float valueMax = DELAY_MAX)
    {
        GUILayout.BeginHorizontal();
        {
            GUILayout.Label(label);
            value = GUILayout.HorizontalSlider(value, valueMin, valueMax);
            GUILayout.Label(value.ToString("00.00"));
        }
        GUILayout.EndHorizontal();
        return value;
    }

    private void ToggleMenuCheat()
    {
        m_IsCloseMenuCheat = !m_IsCloseMenuCheat;
    }

    public bool InGodMode()
    {
        return m_GodMode;
    }


    public void SubscribeEvent(ECheat cheatId, Action action) 
    {
        if (m_Events.ContainsKey(cheatId)) m_Events[cheatId] += action;
        else m_Events.Add(cheatId, action);
    }

    private void InvokeEvent(ECheat cheatId)
    {
        m_Events[cheatId]?.Invoke();
    }
}
