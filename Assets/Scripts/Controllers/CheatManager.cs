using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings.SplashScreen;

public enum ECheat {
    GOD_MODE,
    ADD_SCORE,
    ADD_TIME,
    SET_HI_SCORE,
    NEXT_SESSION,
    PREVIUS_SESSION
}

public class CheatManager : MonoBehaviour
{
    private const float ADD_TIMER_IN_SECENDS = 10f;
    private const float ADD_SCORE_VALUE = 100.0f;
    private const float DELAY_MIN = 0.1f;
    private const float DELAY_MAX = 5.0f;

    private Dictionary<ECheat, Func<float, float>> m_Events = new Dictionary<ECheat, Func<float, float>>();


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
            if (GUILayout.Button("Add Score")) AddScore();
            if (GUILayout.Button("Add Time")) AddTime();
            if (GUILayout.Button("Reset Hi-Score")) ResetHiScore();
            if (GUILayout.Button("Next Session")) NextSession();
            if (GUILayout.Button("Previous Session")) PreviusSession();
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

    private void PreviusSession()
    {
        InvokeEvent(ECheat.PREVIUS_SESSION, 0);
    }

    private void NextSession()
    {
        InvokeEvent(ECheat.NEXT_SESSION, 0);
    }

    private void ResetHiScore()
    {
        InvokeEvent(ECheat.SET_HI_SCORE, 0);
    }

    private void AddTime()
    {
        InvokeEvent(ECheat.ADD_TIME, ADD_TIMER_IN_SECENDS);
    }

    private void AddScore()
    {
        InvokeEvent(ECheat.ADD_SCORE, ADD_SCORE_VALUE);
    }


    public void SubscribeEvent(ECheat cheatId, Func<float, float> action) 
    {
        if (m_Events.ContainsKey(cheatId)) m_Events[cheatId] += action;
        else m_Events.Add(cheatId, action);
    }

    private float InvokeEvent(ECheat cheatId, float value)
    {
        if (!m_Events.ContainsKey(cheatId)) return value;

        return m_Events[cheatId].Invoke(value);
    }
}
