using UnityEngine;

public class PlayerLevel : Singleton<PlayerLevel>
{
    [SerializeField] private AnimationCurve _levelCurve = null;

    public static event System.Action<int> OnPlayerLevelUp = null;
    public static event System.Action<float, float> OnPlayerEarnXP = null;

    [Min(1), SerializeField] private int _level = 1;
    private float _currentXP = 0.00f;
    private float _necessaryXPToLevelUp = 0.00f;

    private float CurrentXP
    {
        get => _currentXP;
        set
        {
            float NeedXP = _necessaryXPToLevelUp - _currentXP;

            _currentXP = value;
            OnPlayerEarnXP?.Invoke(_currentXP, _necessaryXPToLevelUp);

            if (_currentXP >= _necessaryXPToLevelUp)
            {
                _level++;
                _necessaryXPToLevelUp = _levelCurve.Evaluate(_level);
                OnPlayerLevelUp?.Invoke(GetLevel());

                if (value > NeedXP)
                {
                    CurrentXP = value - NeedXP;
                }
            }
        }
    }

    public int GetLevel() => _level;

    private void OnEnable() {
        CustomerUI.OnSoldFruit += OnSoldFruit;
    }

    private void OnDisable() {
        CustomerUI.OnSoldFruit -= OnSoldFruit;
    }

    private void Start() {
        OnPlayerLevelUp?.Invoke(_level);
        _necessaryXPToLevelUp = _levelCurve.Evaluate(_level);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _level++;
            OnPlayerLevelUp?.Invoke(_level);
        }
    }

    private void OnSoldFruit<T>(T CustomerUI) where T : CustomerUI {
        int Count = CustomerUI.SoldFruitCount;
        FruitData Fruit = CustomerUI.SoldFruit;

        CurrentXP += Count * Fruit.Cost;
    }
}