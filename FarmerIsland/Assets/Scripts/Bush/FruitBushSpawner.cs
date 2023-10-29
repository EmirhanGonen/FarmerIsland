using UnityEngine;
using System.Collections.Generic;

public class FruitBushSpawner : Singleton<FruitBushSpawner>
{
    [SerializeField] private FruitBush _bushPrefab = null;

    [SerializeField] private IntVariable _bushCount = null;
    [SerializeField] private IntVariable _maxSpawnCount = null;
    [SerializeField] private FloatVariable _spawnInterval = null;


    private List<FruitBushData> _canSpawnBushs = new List<FruitBushData>();
    private float _elapsedTime = 0.00f;


    #region Encapsulations

    private float Progress => _elapsedTime / _spawnInterval.Value;
    private bool CanSpawn => _bushCount.Value < _maxSpawnCount.Value;
    private int BushCount
    {
        get => _bushCount.Value;
        set
        {
            _bushCount.Value = value;
            OnChangedBushCount?.Invoke(_bushCount, _maxSpawnCount);
        }
    }

    #endregion
    #region Events

    public static event System.Action OnSpawnedBush = null;
    public static event System.Action<IntVariable, IntVariable> OnChangedBushCount = null;

    public delegate void OnProgressUpdateHandler(float Progress);
    public static event OnProgressUpdateHandler OnProgressUpdate = null;

    #endregion
    #region Data's

    private FruitBushDataList _dataList = null;
    private const string DATA_LIST_PATH = "Datas/FruitBushDataList";

    #endregion

    private void OnEnable() {
        FruitBush.OnCollected += CollectedBush;

        PlayerLevel.OnPlayerLevelUp += GetNewData;
        PlayerLevel.OnPlayerLevelUp += UpgradeByLevel;
    }
    private void OnDisable() {
        FruitBush.OnCollected -= CollectedBush;

        PlayerLevel.OnPlayerLevelUp -= GetNewData;
        PlayerLevel.OnPlayerLevelUp -= UpgradeByLevel;
    }

    protected override void Awake() {
        base.Awake();

        _dataList = Resources.Load<FruitBushDataList>(DATA_LIST_PATH);
    }
    private void Start() {
        BushCount = 0;
    }
    private void Update() {
        if (!CanSpawn)
        {
            _elapsedTime = 0.00f;
            return;
        }

        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _spawnInterval.Value)
            ProgressFilled();

        OnProgressUpdate?.Invoke(Progress);
    }

    private void Spawn() {
        Island.Instance.GetIslandBorders(out float HorizontalBorder, out float VerticalBorder);

        Vector2 SpawnLocation = new Vector2(Random.Range(-HorizontalBorder, HorizontalBorder), Random.Range(-VerticalBorder, VerticalBorder));

        FruitBush SpawnedBush = Instantiate(_bushPrefab, SpawnLocation, Quaternion.identity);
        SpawnedBush.Initialize(GetRandomSpawnableData());
        BushCount++;

        OnSpawnedBush?.Invoke();
    }

    private void CollectedBush() {
        BushCount--;
    }
    private void ProgressFilled() {
        _elapsedTime = 0.00f;
        Spawn();
    }

    private void GetNewData(int Level) {
        _canSpawnBushs.Add(_dataList.GetDataByLevel(Level));
    }

    private void UpgradeByLevel(int Level) {
        _maxSpawnCount.Value = 10 * Level;
    }

    public FruitBushData GetRandomSpawnableData() => _canSpawnBushs[Random.Range(0, _canSpawnBushs.Count)];


    public FruitBushData GetBushByFruitData<T>(T FruitData) where T : FruitData {
        foreach (FruitBushData bush in _canSpawnBushs)
        {
            if (!bush.FruitData.Equals(FruitData))
                continue;

            return bush;
        }

        return null;
    }
}