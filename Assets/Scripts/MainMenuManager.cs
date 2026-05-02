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

    [Header("Buttons")]
    public Image[] menuButtons;  // drag Play, Instructions, Quit buttons here

    [Header("Auto Cycle")]
    public float cycleInterval = 3.5f;
    public float fadeDuration  = 1.0f;

    [Header("Scene Names")]
    public string gameSceneName = "Level_01";

    [Header("Panels")]
    public GameObject instructionsPanel;
    public GameObject buttonsGroup;

    // era button colors
    private Color[] eraButtonColors = new Color[]
    {
        new Color(0.176f, 0.478f, 0.227f, 1f),  // Forest green  #2D7A3A
        new Color(0.961f, 0.784f, 0.259f, 1f),  // Desert gold   #F5C842
        new Color(0.290f, 0.565f, 0.769f, 1f),  // Ice blue      #4A90C4
    };

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
        instructionsPanel.SetActive(true);
        buttonsGroup.SetActive(false);
    }

    public void OnCloseButton()
    {
        instructionsPanel.SetActive(false);
        buttonsGroup.SetActive(true);
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
        UpdateButtonColors();
    }

    void UpdateButtonColors()
    {
        if (menuButtons == null) return;
        foreach (Image btn in menuButtons)
        {
            if (btn != null)
                btn.color = eraButtonColors[currentEra];
        }
    }

    CanvasGroup GetOrAddCanvasGroup(GameObject go)
    {
        CanvasGroup cg = go.GetComponent<CanvasGroup>();
        if (cg == null) cg = go.AddComponent<CanvasGroup>();
        return cg;
    }
}