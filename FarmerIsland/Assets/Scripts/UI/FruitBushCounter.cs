using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class FruitBushCounter : MonoBehaviour
{
    private TextMeshProUGUI _counterText = null;

    private void OnEnable() {
        FruitBushSpawner.OnChangedBushCount += SetCounterText;
    }
    private void OnDisable() {
        FruitBushSpawner.OnChangedBushCount -= SetCounterText;
    }

    private void Awake() {
        _counterText = GetComponent<TextMeshProUGUI>();
    }

    private void SetCounterText(IntVariable BushCount, IntVariable MaxSpawnCount) {
        _counterText.SetText($"{BushCount.Value}/{MaxSpawnCount.Value}");
    }
}