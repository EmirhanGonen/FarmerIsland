using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class FpsText : MonoBehaviour
{
    private TextMeshProUGUI _fpsText = null;

    private void Awake() {
        _fpsText = gameObject.GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        _fpsText.SetText("Fps: " + ((int)(1.00f / Time.unscaledDeltaTime)).ToString());
    }
}