using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelProgressBar : MonoBehaviour
{
    private Image _progressFill = null;
    private TextMeshProUGUI _levelText = null;

    private void Awake() {
        _progressFill = transform.GetChild(0).GetComponent<Image>();
        _levelText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable() {
        PlayerLevel.OnPlayerEarnXP += SetFillBar;
        PlayerLevel.OnPlayerLevelUp += SetLevelText;
    }

    private void OnDisable() {
        PlayerLevel.OnPlayerEarnXP -= SetFillBar;
        PlayerLevel.OnPlayerLevelUp -= SetLevelText;
    }

    private void SetFillBar(float CurrentXP, float NecessaryXPToLevelUp) {
        _progressFill.fillAmount = 1.00f / (NecessaryXPToLevelUp / CurrentXP);
    }
    private void SetLevelText(int Level) => _levelText.SetText($"Level {Level}");
}