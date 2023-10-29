using UnityEngine;
using UnityEngine.UI;

public class FruitBushSpawnProgressBar : MonoBehaviour
{
    private Image _progressFillBar = null;

    private void OnEnable()
    {
        FruitBushSpawner.OnProgressUpdate += OnProgressChange;
    }
    private void OnDisable()
    {
        FruitBushSpawner.OnProgressUpdate -= OnProgressChange;
    }

    private void Awake()
    {
        _progressFillBar = transform.GetChild(1).GetComponent<Image>();
    }

    private void OnProgressChange(float progress)
    {
        _progressFillBar.fillAmount = progress;
    }
}