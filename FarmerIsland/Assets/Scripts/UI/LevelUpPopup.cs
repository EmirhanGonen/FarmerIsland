using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpPopup : MonoBehaviour
{
    [SerializeField] private Image _newFruitImage = null;
    [SerializeField] private TextMeshProUGUI _newFruitName = null;

    [SerializeField] private TextMeshProUGUI _oldIslandSize = null;
    [SerializeField] private TextMeshProUGUI _newIslandSize = null;


    private FruitBushDataList _dataList = null;
    private const string DATA_LIST_PATH = "Datas/FruitBushDataList";

    public static System.Action OnClosedPopup = null;

    private void Awake() {
        _dataList = Resources.Load<FruitBushDataList>(DATA_LIST_PATH);
    }

    private void OnEnable() {
        PlayerLevel.OnPlayerLevelUp += InitializePopup;
    }

    private void OnDisable() {
        PlayerLevel.OnPlayerLevelUp -= InitializePopup;
    }

    private void InitializePopup(int Level) {
        if (Level.Equals(1))
            return;

        FruitData NewFruit = _dataList.GetDataByLevel(Level).FruitData;

        _newFruitImage.sprite = NewFruit.FruitSprite;
        _newFruitName.SetText(NewFruit.FruitName);

        _oldIslandSize.SetText((Island.Instance.Scale.x).ToString());
        _newIslandSize.SetText(Island.Instance.GetNewIslandSize(Level).ToString());

        transform.GetChild(0).gameObject.SetActive(true);
    }
    public void ClosePopup()
    {
        OnClosedPopup?.Invoke();
        transform.GetChild(0).gameObject.SetActive(false);
    }
}