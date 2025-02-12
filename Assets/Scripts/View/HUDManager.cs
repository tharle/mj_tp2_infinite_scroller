using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    private GameState m_State = GameState.GAME;

    [SerializeField] private TextMeshProUGUI m_TxtMPTime;
    [SerializeField] private TextMeshProUGUI m_TxtMPScoreInGame;
    [SerializeField] private TextMeshProUGUI m_TxtMPScoreGameOver;
    [SerializeField] private TextMeshProUGUI m_TxtMPHiScoreInGame;
    [SerializeField] private TextMeshProUGUI m_TxtMPHiScoreGameOver;

    [SerializeField] private GameObject m_GameOverScreen;
    [SerializeField] private GameObject m_PauseMenuScreen;

    private void Start()
    {
        m_GameOverScreen.SetActive(false);
    }

    public void UpdateTimmer(int time)
    {
        m_TxtMPTime.text = time.ToString();
    }

    public void UpdateScores(int score, int hiScore)
    {
        m_TxtMPScoreInGame.text = score.ToString("000000");
        m_TxtMPScoreGameOver.text = score.ToString("000000");
        m_TxtMPHiScoreInGame.text = hiScore.ToString("000000");
        m_TxtMPHiScoreGameOver.text = hiScore.ToString("000000");
    }

    public void ChangeGameState(GameState state)
    {
        m_State = state;

        OnChangeGameState();
    }

    private void OnChangeGameState()
    {
        switch (m_State)
        {
            case GameState.MAIN_MENU:
                PauseGame();
                break;
            case GameState.PAUSE_MENU:
                ShowPauseMenuScreen();
                break;
            case GameState.GAME_OVER:
                ShowGameOverScreen();
                break;
            case GameState.GAME:
            default:
                ResumeGame();
                break;
        }
    }

    private void ShowGameOverScreen()
    {
        PauseGame();
        m_GameOverScreen.SetActive(true);
    }

    private void ShowPauseMenuScreen()
    {
        PauseGame();
        m_PauseMenuScreen.SetActive(true);
    }


    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    private void ResumeGame()
    {
        m_PauseMenuScreen.SetActive(false);
        m_GameOverScreen.SetActive(false);
        Time.timeScale = 1;
    }
}
