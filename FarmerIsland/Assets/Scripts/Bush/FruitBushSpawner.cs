using UnityEngine;
using System.Collections;

public class FruitBushSpawner : MonoBehaviour
{
    [SerializeField] private FruitBush _bushPrefab = null;

    [SerializeField] private IntVariable _bushCount = null;
    [SerializeField] private IntVariable _maxSpawnCount;
    [SerializeField] private FloatVariable _spawnInterval = null;

    public static event System.Action OnSpawnedBush = null;

    public delegate void OnProgressUpdateHandler(float Progress);
    public static event OnProgressUpdateHandler OnProgressUpdate = null;

    private float _elapsedTime = 0.00f;

    private float Progress => _elapsedTime / _spawnInterval.Value;
    private bool CanSpawn => _bushCount.Value < _maxSpawnCount.Value;


    private void OnEnable()
    {
        FruitBush.OnCollected += CollectedBush;
    }
    private void OnDisable()
    {
        FruitBush.OnCollected -= CollectedBush;
    }

    private void Spawn()
    {
        Island.Instance.GetIslandBorders(out float HorizontalBorder, out float VerticalBorder);

        Vector2 SpawnLocation = new Vector2(Random.Range(-HorizontalBorder, HorizontalBorder), Random.Range(-VerticalBorder, VerticalBorder));

        FruitBush SpawnedBush = Instantiate(_bushPrefab, SpawnLocation, Quaternion.identity);
        _bushCount++;

        OnSpawnedBush?.Invoke();
    }

    private void Update()
    {
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
    private void CollectedBush()
    {
        _bushCount--;
    }
    private void ProgressFilled()
    {
        _elapsedTime = 0.00f;
        Spawn();
    }
}