using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CoinText : MonoBehaviour
{
    private TextMeshProUGUI _coinText = null;

    private int _coin = 0;

    private void Awake() {
        _coinText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable() {
        PlayerInventory.OnChangedCoinAmount += SetText;
    }
    private void OnDisable() {
        PlayerInventory.OnChangedCoinAmount -= SetText;
    }

    private void SetText(int CoinAmount) => _coinText.SetText(CoinAmount.ToString());
}