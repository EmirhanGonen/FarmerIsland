using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FruitUpgradeUI : MonoBehaviour
{
    [SerializeField] private Image _fruitImage = null;

    [SerializeField] private TextMeshProUGUI _fruitNameText = null;
    [SerializeField] private TextMeshProUGUI _fruitIndexText = null;
    [SerializeField] private TextMeshProUGUI _fruitCountText = null;
    [SerializeField] private TextMeshProUGUI _perHarvestText = null;
    [SerializeField] private TextMeshProUGUI _costText = null;

    [SerializeField] private Button _upgradeButton = null;

    [SerializeField] private GameObject _lockUI = null;
    [SerializeField] private TextMeshProUGUI _necessaryLevelText = null;

    private FruitUpgradeUIData _data = null;

    private bool _isInitialized = false;

    public static event System.Action OnUpgradeUI = null;

    private void OnEnable() {
        FruitUpgradeUIController.OnOpened += SetCountText;
        FruitUpgradeUIController.OnOpened += SetCostText;
        FruitUpgradeUIController.OnOpened += SetPerHarvestText;

        PlayerLevel.OnPlayerLevelUp += CheckForUnlock;

        FruitUpgradeUI.OnUpgradeUI += SetCostText;
        FruitUpgradeUI.OnUpgradeUI += SetPerHarvestText;
    }
    private void OnDisable() {
        FruitUpgradeUIController.OnOpened -= SetCountText;
        FruitUpgradeUIController.OnOpened -= SetCostText;
        FruitUpgradeUIController.OnOpened -= SetPerHarvestText;

        PlayerLevel.OnPlayerLevelUp += CheckForUnlock;
    }


    public void Initialize<T>(T UIData) where T : FruitUpgradeUIData {
        _data = UIData;

        _fruitImage.sprite = _data.BushData.FruitData.FruitSprite;
        _fruitNameText.SetText(_data.BushData.FruitData.FruitName);

        _fruitIndexText.SetText(UIData.FruitIndex.ToString());

        _upgradeButton.onClick.AddListener(Purchase);

        _isInitialized = true;

        CheckForUnlock(PlayerLevel.Instance.GetLevel());

        SetCountText();
        SetCostText();
        SetPerHarvestText();

        if (PlayerLevel.Instance.GetLevel() < _data.BushData.NecessaryLevel)
        {
            _lockUI.SetActive(true);
            _necessaryLevelText.SetText($"{_data.BushData.NecessaryLevel} Level");
        }

    }


    public void SetCountText() {
        if (!_isInitialized)
            return;

        if (!PlayerInventory.Instance.GetSlotByFruitData(_data.BushData.FruitData, out InventorySlot slot))
        {
            _fruitCountText.SetText($"x{0}");
            return;
        }

        _fruitCountText.SetText($"x{slot.GetItemCount()}");
    }
    public void SetCostText() {
        if (!_isInitialized)
            return;

        _costText.SetText(GetCurrentCost().ToString());
        _costText.color = PlayerInventory.Instance.Coin >= GetCurrentCost() ? Color.green : Color.red;
    }
    public void SetPerHarvestText() {
        if (!_isInitialized)
            return;

        _perHarvestText.SetText($"{_data.BushData.BushLevel.Value}");
    }

    private void Purchase() {
        if (GetCurrentCost() > PlayerInventory.Instance.Coin)
            return;

        PlayerInventory.Instance.Coin -= GetCurrentCost();
        _data.BushData.BushLevel.Value++;

        OnUpgradeUI?.Invoke();
    }

    private int GetCurrentCost() => Mathf.RoundToInt((_data.BushData.FruitData.Cost * _data.BushData.BushLevel.Value) * 5.00f);

    private void CheckForUnlock(int Level) {
        _lockUI.SetActive(_data?.BushData?.NecessaryLevel > Level);
    }
}