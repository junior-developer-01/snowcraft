using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameEndManager : MonoBehaviour
{
    public static GameEndManager Instance { get; private set; }

    [Header("Lose UI")]
    public GameObject losePanel;
    public Text loseText;

    [Header("Settings")]
    public float quitDelaySeconds = 2f;

    bool ended;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void Lose()
    {
        if (ended) return;
        ended = true;

        if (losePanel) losePanel.SetActive(true);
        if (loseText) loseText.text = "패배했습니다!"; // :contentReference[oaicite:1]{index=1}

        Time.timeScale = 0f; // 게임 일시정지 :contentReference[oaicite:2]{index=2}
        StartCoroutine(QuitAfterDelay()); // :contentReference[oaicite:3]{index=3}
    }

    IEnumerator QuitAfterDelay()
    {
        yield return new WaitForSecondsRealtime(quitDelaySeconds); // timeScale=0이어도 기다림 :contentReference[oaicite:4]{index=4}

        Application.Quit(); // 빌드에서 종료, 에디터에서는 무시됨 :contentReference[oaicite:5]{index=5}

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
