using System.Collections.Generic;
using UnityEngine;

public class FruitUpgradeUIController : MonoBehaviour
{
    private List<FruitUpgradeUI> _fruitUpgradeUIs = new List<FruitUpgradeUI>();

    private const string LIST_DATA_PATH = "Datas/FruitBushDataList";
    private FruitBushDataList _listData = null;

    public static System.Action OnOpened = null;

    private void Awake() {
        FillUpgradeUIs();

        _listData = Resources.Load<FruitBushDataList>(LIST_DATA_PATH);
    }

    private void Start() {
        InitializeUIs();
    }

    private void FillUpgradeUIs() {
        _fruitUpgradeUIs.AddRange(transform.GetComponentsInChildren<FruitUpgradeUI>());
    }

    private void InitializeUIs() {
        foreach (FruitUpgradeUI UI in _fruitUpgradeUIs)
        {
            InitializeUI(UI);
        }
    }

    private void InitializeUI<T>(T UI) where T : FruitUpgradeUI {
        int UIIndex = _fruitUpgradeUIs.IndexOf(UI);
        int FruitIndex = UIIndex + 1;

        FruitBushData BushData = _listData.GetDataByLevel(FruitIndex);

        UI.Initialize(new FruitUpgradeUIData(BushData, FruitIndex));
       /* UI.UpgradeButton.onClick.AddListener(() =>
        {

        });*/
    }

    public void Open() {
        OnOpened?.Invoke();
    }
}
public class FruitUpgradeUIData
{
    public FruitBushData BushData => _bushData;
    public int FruitIndex => _fruitIndex;

    private FruitBushData _bushData = null;
    private int _fruitIndex = 0;

    public FruitUpgradeUIData(FruitBushData BushData, int FruitIndex) {
        _bushData = BushData;
        _fruitIndex = FruitIndex;
    }
}