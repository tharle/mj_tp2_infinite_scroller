using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static int AMOUT_TIME_PER_LEVEL_DEFAULT = 8;

    [SerializeField] private TextScore m_ScorePlayerText;
    [SerializeField] AudioSource m_CoinAudio;
    HUDManager m_HUDManager;
    private int m_AmountTimePerLevel = AMOUT_TIME_PER_LEVEL_DEFAULT;

    private Session m_Session = Session.SPRING;

    private int m_Score = 0;
    private int m_HiScore = 0;
    private int m_Timmer = 0;
    private float m_AmountTimmerToNextSession = 0;
    private float m_TimmerToNextSession = 0;
    private float m_ElapseTimmer = 0;


    private void Start()
    {
        m_HUDManager = FindAnyObjectByType<HUDManager>();
        m_AmountTimmerToNextSession = m_AmountTimePerLevel * 2;
        LoadPlayerPrefs();
        UpdateScores();

        ChangeGameState(GameState.GAME);
#if DEBUG
        AddCheats();
#endif
    }

    private void Update()
    {
        UpdateTimmer();

        UpdateSession();

        UpdatePauseMenu();
    }

    private void UpdateSession()
    {
        if (m_TimmerToNextSession >= m_AmountTimmerToNextSession)
        {
            m_TimmerToNextSession -= m_AmountTimmerToNextSession;
            ChangeToNextSession();
        }
    }

    private void ChangeToNextSession()
    {
        switch (m_Session)
        {
            case Session.SUMMER:
                m_Session = Session.FALL;
                break;
            case Session.FALL:
                m_Session = Session.WINTER;
                break;
            case Session.WINTER:
                m_Session = Session.SPRING;
                break;
            case Session.SPRING:
            default:
                m_Session = Session.SUMMER;
                break;
        }
    }

    private void ChangeToPreviusSession()
    {
        switch (m_Session)
        {
            case Session.SUMMER:
                m_Session = Session.SPRING;
                break;
            case Session.FALL:
                m_Session = Session.SUMMER;
                break;
            case Session.WINTER:
                m_Session = Session.FALL;
                break;
            case Session.SPRING:
            default:
                m_Session = Session.WINTER;
                break;
        }
    }

    public int GetLevel()
    {
        return (m_Timmer / m_AmountTimePerLevel) + 1;
    }

    public Session getCurrentSession()
    {
        return m_Session;
    }

    public void AddScorePoints(int score, bool isCoin)
    {
        score += score * GetLevel();
        m_Score += score;
        ShowScorePoints(score);
        if(isCoin) m_CoinAudio.Play();
        m_HiScore = m_Score >= m_HiScore ? m_Score : m_HiScore;

        UpdateScores();
    }

    private void ShowScorePoints(int score) {
        m_ScorePlayerText.playText(score.ToString());
    }

    public void LossLife()
    {
        SavePlayerPrefs();
        ChangeGameState(GameState.GAME_OVER);
    }

    private void SavePlayerPrefs()
    {
        PlayerPrefs.SetInt("HiScore", m_HiScore);
        PlayerPrefs.Save();
    }

    private void LoadPlayerPrefs()
    {
        m_HiScore = PlayerPrefs.GetInt("HiScore");
    } 

    public void ChangeGameState(GameState gameState)
    {
        m_HUDManager.ChangeGameState(gameState);
    }


    // ---------------------------------------------
    // HUD UPDATES
    // ---------------------------------------------
    private void UpdateTimmer()
    {
        m_ElapseTimmer += Time.deltaTime;
        m_TimmerToNextSession +=  Time.deltaTime;
        if (m_ElapseTimmer > 1) // change le timmer chaque 1s
        {
            m_ElapseTimmer -= 1;
            m_HUDManager.UpdateTimmer(++m_Timmer);
        }
    }

    private void UpdateScores()
    {
        m_HUDManager.UpdateScores(m_Score, m_HiScore);
    }

    // ---------------------------------------------
    // HUD UPDATES
    // ---------------------------------------------
    public void OnTryAgain()
    {
        SceneManager.LoadScene(GameParameters.SceneName.GAME);
    }

    public void OnMainMenu()
    {
        SceneManager.LoadScene(GameParameters.SceneName.MAIN_MENU);
    }

    private void UpdatePauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnContinuePauseMenu();
        }
    }

    public void OnContinuePauseMenu()
    {
        if (Time.timeScale == 0) ChangeGameState(GameState.GAME);
        else ChangeGameState(GameState.PAUSE_MENU);
    }

    // ---------------------------------------------
    // CHEAT EVENT
    // ---------------------------------------------
#if DEBUG
    private void AddCheats()
    {
        CheatManager.Instance.SubscribeEvent(ECheat.ADD_SCORE, OnCheatAddScorePoints);
        CheatManager.Instance.SubscribeEvent(ECheat.ADD_TIME, OnCheatAddTime);
        CheatManager.Instance.SubscribeEvent(ECheat.RESET_HI_SCORE, OnCheatSetHiScore);
        CheatManager.Instance.SubscribeEvent(ECheat.NEXT_SESSION, OnCheatNextSession);
        CheatManager.Instance.SubscribeEvent(ECheat.PREVIUS_SESSION, OnCheatPreviusSession);
        CheatManager.Instance.SubscribeEvent(OnCheatEventAmountTimePerLevel);
    }
#endif

    private int OnCheatEventAmountTimePerLevel(int newAmoutTimePerLevel)
    {
        m_AmountTimePerLevel = newAmoutTimePerLevel;
        return m_AmountTimePerLevel;
    }

    private void OnCheatNextSession()
    {
        ChangeToNextSession();
    }

    private void OnCheatPreviusSession()
    {
        ChangeToPreviusSession();
    }


    private void OnCheatSetHiScore()
    {
        m_HiScore = m_Score;
        UpdateScores();
    }

    private void OnCheatAddScorePoints()
    {
        AddScorePoints(CheatManager.ADD_SCORE_VALUE, true);
    }

    private void OnCheatAddTime()
    {
        m_Timmer += CheatManager.ADD_TIMER_IN_SECENDS;
        m_TimmerToNextSession += CheatManager.ADD_TIMER_IN_SECENDS;
        m_HUDManager.UpdateTimmer(m_Timmer);
    }
}
