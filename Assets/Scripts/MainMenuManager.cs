using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

// =====================================================
//  MainMenuManager.cs
//  Attach to: an empty GameObject called "MenuManager"
// =====================================================

public class MainMenuManager : MonoBehaviour
{
    [Header("Era Backgrounds")]
    public GameObject[] eraBackgrounds;
    public TextMeshProUGUI eraLabel;
    public TextMeshProUGUI eraName;

    [Header("Era Info")]
    public string[] eraLabels = { "Prehistoric", "Medieval", "Futuristic" };
    public string[] eraNames  = { "Age of Stone", "Age of Kingdoms", "Age of Stars" };

    [Header("Auto Cycle")]
    public float cycleInterval = 3.5f;
    public float fadeDuration  = 1.0f;

    [Header("Scene Names")]
    public string gameSceneName = "Level_01";

    private int      currentEra = 0;
    private bool     isFading   = false;
    private Coroutine autoCycleCoroutine;

    // ── Unity lifecycle ───────────────────────────────────────────────────────

    void Start()
    {
        for (int i = 0; i < eraBackgrounds.Length; i++)
        {
            CanvasGroup cg = GetOrAddCanvasGroup(eraBackgrounds[i]);
            cg.alpha = (i == 0) ? 1f : 0f;
            eraBackgrounds[i].SetActive(true);
        }

        currentEra = 0;
        RefreshUI();
        autoCycleCoroutine = StartCoroutine(AutoCycle());
    }

    // ── Public button callbacks ───────────────────────────────────────────────

    public void OnPlayButton()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void OnInstructionsButton()
    {
        Debug.Log("Instructions button pressed");
    }

    public void OnQuitButton()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // ── Coroutines ────────────────────────────────────────────────────────────

    IEnumerator AutoCycle()
    {
        yield return new WaitForSeconds(cycleInterval);
        while (true)
        {
            int next = (currentEra + 1) % eraBackgrounds.Length;
            yield return StartCoroutine(SwitchEra(next));
            yield return new WaitForSeconds(cycleInterval);
        }
    }

    IEnumerator SwitchEra(int targetIndex)
    {
        if (isFading) yield break;
        isFading = true;

        CanvasGroup outCG = GetOrAddCanvasGroup(eraBackgrounds[currentEra]);
        CanvasGroup inCG  = GetOrAddCanvasGroup(eraBackgrounds[targetIndex]);

        inCG.alpha = 0f;
        bool textUpdated = false;

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);
            outCG.alpha = 1f - t;
            inCG.alpha  = t;

            if (!textUpdated && t >= 0.5f)
            {
                currentEra = targetIndex;
                RefreshUI();
                textUpdated = true;
            }

            yield return null;
        }

        outCG.alpha = 0f;
        inCG.alpha  = 1f;
        currentEra = targetIndex;
        isFading   = false;
    }

    // ── Helpers ───────────────────────────────────────────────────────────────

    void RefreshUI()
    {
        if (eraLabel) eraLabel.text = eraLabels[currentEra];
        if (eraName)  eraName.text  = eraNames[currentEra];
    }

    CanvasGroup GetOrAddCanvasGroup(GameObject go)
    {
        CanvasGroup cg = go.GetComponent<CanvasGroup>();
        if (cg == null) cg = go.AddComponent<CanvasGroup>();
        return cg;
    }
}