using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WinManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject winPanel;
    public Text winText;

    [Header("Settings")]
    public string enemyTag = "Enemy";
    public float quitDelaySeconds = 2f;

    bool hasWon = false;

    void Update()
    {
        if (hasWon) return;

        // Enemy 태그의 활성 오브젝트 수를 체크 :contentReference[oaicite:2]{index=2}
        var enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        if (enemies.Length == 0)
        {
            Win();
        }
    }

    void Win()
    {
        hasWon = true;

        if (winPanel) winPanel.SetActive(true);
        if (winText) winText.text = "축하합니다!\n모든 적을 처치했습니다!"; // :contentReference[oaicite:3]{index=3}

        // 게임을 멈추고 싶다면 timeScale=0 (일시정지) :contentReference[oaicite:4]{index=4}
        Time.timeScale = 0f;

        // timeScale=0이어도 기다리려면 WaitForSecondsRealtime 사용 :contentReference[oaicite:5]{index=5}
        StartCoroutine(QuitAfterDelay());
    }

    IEnumerator QuitAfterDelay()
    {
        yield return new WaitForSecondsRealtime(quitDelaySeconds); // :contentReference[oaicite:6]{index=6}

        // 빌드된 게임에서 종료됨. (에디터에서는 Quit가 무시됨) :contentReference[oaicite:7]{index=7}
        Application.Quit();

#if UNITY_EDITOR
        // 에디터에서 테스트할 때는 플레이모드 종료
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
