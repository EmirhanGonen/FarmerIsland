using DG.Tweening;
using UnityEngine;

public class CustomersController : MonoBehaviour
{
    private FruitBushDataList _fruitBushDataList = null;
    private const string FRUIT_BUSH_DATA_LIST = "Datas/FruitBushDataList";

    private CustomerUI[] _customerTemplates = new CustomerUI[] { null, null, null, null };

    private void OnEnable() {
        CustomerUI.OnSoldFruit += OnSoldCustomerFruit;
    }

    private void OnDisable() {
        CustomerUI.OnSoldFruit -= OnSoldCustomerFruit;
    }

    private void Start() {
        _fruitBushDataList = Resources.Load<FruitBushDataList>(FRUIT_BUSH_DATA_LIST);

        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            _customerTemplates[i] = transform.GetChild(0).GetChild(i).GetComponent<CustomerUI>();
            InitializeCustomer(_customerTemplates[i]);
        }
    }


    private void InitializeCustomer<T>(T Customer) where T : CustomerUI {
        Customer.Initialize(GetRandomFruitData());
    }

    private void OnSoldCustomerFruit<T>(T CustomerUI) where T : CustomerUI {
        InitializeCustomer(CustomerUI);
        CustomerUI.transform.DOKill();

        Vector2 CustomerPosition = CustomerUI.transform.position;
        CustomerUI.transform.position = CustomerPosition + Vector2.up * 400;
        CustomerUI.transform.DOMove(CustomerPosition, 0.70f).SetEase(Ease.InOutExpo);
    }

    private FruitData GetRandomFruitData() => FruitBushSpawner.Instance.GetRandomSpawnableData().FruitData;
}