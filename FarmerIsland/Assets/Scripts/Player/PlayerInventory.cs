using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class PlayerInventory : Singleton<PlayerInventory>
{
    [SerializeField] private List<InventorySlot> _inventorySlots = null;
    public static System.Action<FruitData> OnAddedNewItem = null;
    public static System.Action<InventorySlot> OnChangedItemCount = null;
    public static System.Action<int> OnChangedCoinAmount = null;

    private FruitBushDataList _dataList = null;
    private const string DATA_LIST_PATH = "Datas/FruitBushDataList";


    public int Coin
    {
        get => _coin; set
        {
            _coin = value;

            OnChangedCoinAmount?.Invoke(_coin);
        }
    }

    private int _coin = 0;


    protected override void Awake() {
        base.Awake();
        _dataList = Resources.Load<FruitBushDataList>(DATA_LIST_PATH);
    }

    private void OnEnable() {
        PlayerCollector.OnInteractedFruitAction += AddItem;
        PlayerLevel.OnPlayerLevelUp += AddItemByPlayerLevel;
        CustomerUI.OnSoldFruit += OnSoldFruit;
    }
    private void OnDisable() {
        PlayerCollector.OnInteractedFruitAction -= AddItem;
        PlayerLevel.OnPlayerLevelUp -= AddItemByPlayerLevel;
        CustomerUI.OnSoldFruit -= OnSoldFruit;
    }

    public void AddItem(Fruit Fruit) => AddItem(Fruit.GetItemData());
    public void AddItem<T>(T Item) where T : FruitData {
        if (HasItem(Item, out InventorySlot Slot))
        {
            int Count = Slot.GetItemCount();
            Slot.SetItemCount(++Count);
            OnChangedItemCount?.Invoke(Slot);
            return;
        }

        _inventorySlots.Add(Slot);
        OnAddedNewItem?.Invoke(Slot.GetItem());
    }


    private bool HasItem<T>(T Item, out InventorySlot Slot) where T : FruitData {
        foreach (InventorySlot slot in _inventorySlots)
        {
            if (!slot.GetItem().Equals(Item))
                continue;

            Slot = slot;
            return true;
        }

        Slot = new InventorySlot(Item);
        return false;
    }
    public bool GetSlotByFruitData<T>(T FruitData, out InventorySlot OutSlot) where T : FruitData {
        OutSlot = null;
        foreach (InventorySlot Slot in _inventorySlots)
        {
            if (!Slot.GetItem().Equals(FruitData))
                continue;

            OutSlot = Slot;
            return true;
        }
        return false;
    }

    public void DecreaseItemCountByFruitData<T>(T FruitData, int Amount) where T : FruitData {
        if (GetSlotByFruitData(FruitData, out InventorySlot Slot))
        {
            Slot.SetItemCount(Slot.GetItemCount() - Amount);
            OnChangedItemCount?.Invoke(Slot);
        }
    }

    private void AddItemByPlayerLevel(int Level) => AddItem(_dataList.GetDataByLevel(Level).FruitData);

    private void OnSoldFruit<T>(T CustomerUI) where T : CustomerUI {
        int EarnedCoin = (int)CustomerUI.SoldFruit.Cost * CustomerUI.SoldFruitCount;
        _coin += EarnedCoin;
        OnChangedCoinAmount?.Invoke(_coin);
    }
}

[System.Serializable]
public class InventorySlot
{
    public InventorySlot(FruitData Item) {
        SetItem(Item);
        SetItemCount(0);
    }
    public InventorySlot(FruitData Item, int Amount) {
        _item = Item;
        SetItemCount(Amount);
    }


    [SerializeField] private FruitData _item = null;
    [SerializeField] private int _itemCount = 0;

    public void SetItem<T>(T Item) where T : FruitData => _item = Item;
    public FruitData GetItem() => _item;

    public void SetItemCount(int Count) => _itemCount = Count;
    public int GetItemCount() => _itemCount;
}