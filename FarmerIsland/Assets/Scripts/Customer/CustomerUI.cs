using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomerUI : MonoBehaviour
{
    private CustomerPortraitListData _listData = null;
    private const string LIST_DATA_PATH = "Datas/CustomerPortraitList";

    [SerializeField] private Image _customerPortrait = null;

    [SerializeField] private Image Fruit0Image = null;
    [SerializeField] private Image Fruit1Image = null;

    [SerializeField] private TextMeshProUGUI _fruit0CountText = null;
    [SerializeField] private TextMeshProUGUI _fruit1CountText = null;

    [SerializeField] private Image _background = null;
    [SerializeField] private Image _portraitBackground = null;

    [SerializeField] private Color _enoughFruitBackgroundColor = Color.green;
    [SerializeField]
    private Color _enoughFruitPortraitBackgroundColor = Color.green;

    [SerializeField] private Color _notEnoughFruitBackgroundColor = Color.white;
    [SerializeField] private Color _notEnoughFruitPortraitBackgroundColor = Color.white;


    [SerializeField] private TextMeshProUGUI _costText = null;
    [SerializeField] private Image _checkMark = null;
    private int _wantedFruitCount = 0;
    private FruitData _wantedFruit = null;

    public static System.Action<CustomerUI> OnSoldFruit = null;


    public int SoldFruitCount => _wantedFruitCount;
    public FruitData SoldFruit => _wantedFruit;

    private void Awake() {
        _listData = Resources.Load<CustomerPortraitListData>(LIST_DATA_PATH);
    }

    private void Start() {
        GetComponent<Button>().onClick.AddListener(OnClickButton);
    }

    private void OnEnable() {
        PlayerInventory.OnChangedItemCount += CheckDataAmount;
    }
    private void OnDisable() {
        PlayerInventory.OnChangedItemCount -= CheckDataAmount;
    }

    public void Initialize<T>(T FruitData) where T : FruitData {
        _wantedFruit = FruitData;

        _customerPortrait.sprite = _listData.GetRandomCustomerPortrait();

        Fruit0Image.sprite = _wantedFruit.FruitSprite;
        Fruit1Image.gameObject.SetActive(false);
        _fruit0CountText.SetText(string.Empty);

        if (PlayerInventory.Instance.GetSlotByFruitData(_wantedFruit, out InventorySlot Slot))
        {   
            FruitBushData FruitBush = FruitBushSpawner.Instance.GetBushByFruitData(Slot.GetItem());
            IntVariable FruitLevel = FruitBush.BushLevel;

            _wantedFruitCount = Mathf.RoundToInt(Random.Range(5 + (FruitLevel.Value * 1.20f), 10 + (FruitLevel.Value * 1.50f)));

            _wantedFruitCount = Mathf.Clamp(_wantedFruitCount, 1, int.MaxValue);
            CheckDataAmount(Slot);

            _fruit0CountText.SetText($"{Slot.GetItemCount()}/{_wantedFruitCount}");
            _costText.SetText((_wantedFruitCount * _wantedFruit.Cost).ToString());
        }
    }

    private void CheckDataAmount<T>(T InventorySlot) where T : InventorySlot {
        if (_wantedFruit.Equals(InventorySlot.GetItem()))
        {
            _fruit0CountText.SetText($"{InventorySlot.GetItemCount()}/{_wantedFruitCount}");
            if (_wantedFruitCount <= InventorySlot.GetItemCount())
            {
                _background.color = _enoughFruitBackgroundColor;
                _portraitBackground.color = _enoughFruitPortraitBackgroundColor;
                _checkMark.gameObject.SetActive(true);
            }
            else
            {
                _background.color = _notEnoughFruitBackgroundColor;
                _portraitBackground.color = _notEnoughFruitPortraitBackgroundColor;
                _checkMark.gameObject.SetActive(false);
            }

            _fruit0CountText.SetText($"{InventorySlot.GetItemCount()}/{_wantedFruitCount}");
        }
    }
    private void OnClickButton() {
        if (PlayerInventory.Instance.GetSlotByFruitData(_wantedFruit, out InventorySlot Slot))
        {
            if (Slot.GetItemCount() >= _wantedFruitCount)
            {
                PlayerInventory.Instance.DecreaseItemCountByFruitData(_wantedFruit, _wantedFruitCount);
                OnSoldFruit?.Invoke(this);
            }
        }
    }
}