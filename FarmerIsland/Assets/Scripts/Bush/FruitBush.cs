using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
public class FruitBush : MonoBehaviour, IInteractable
{
    [SerializeField, Range(0.00f, 10.00f)] private float _randomSpawnValue = 0.00f;

    private SpriteRenderer _spriteRenderer = null;
    private BoxCollider2D _collider = null;

    public static event System.Action OnCollected;

    private const string FRUIT_BASE_PATH = "Prefabs/FruitBase";
    private Fruit _fruitPrefab = null;
    private FruitBushData _data = null;

    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
    }
    private void Start() {
        _fruitPrefab = Resources.Load<Fruit>(FRUIT_BASE_PATH);
        _collider.enabled = false;
    }


    public void Initialize<T>(T Data) where T : FruitBushData {
        _data = Data;
        _spriteRenderer.sprite = _data.BushSprite;

        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = Data.FruitData.FruitSprite;

        transform.localScale = Vector3.zero;
        transform.DOScale(1.00f, 0.30f).SetEase(Ease.OutBounce).OnComplete(EnableCollision);
        Invoke(nameof(EnableCollision), 0.50f);
    }

    private void EnableCollision() => _collider.enabled = true;

    public void Interact(PlayerCollector Collector) {

        Island.Instance.GetIslandBorders(out float HorizontalLimit, out float VerticalLimit);

        for (int i = 0; i < _data.BushLevel.Value; i++)
        {
            float HorizontalRandom = GetRandomSpawnAxis();
            float VerticalRandom = GetRandomSpawnAxis();

            Vector2 SpawnLocation = new Vector2(transform.position.x + HorizontalRandom, transform.position.y + VerticalRandom);

            //Collapse SpawnLocation in Borders
            SpawnLocation.x = SpawnLocation.x > HorizontalLimit ? HorizontalLimit :
                (SpawnLocation.x < -HorizontalLimit) ? SpawnLocation.x = -HorizontalLimit : SpawnLocation.x;
            SpawnLocation.y = SpawnLocation.y > VerticalLimit ? VerticalLimit :
               (SpawnLocation.y < -VerticalLimit) ? SpawnLocation.y = -VerticalLimit : SpawnLocation.y;
            
            Fruit SpawnedFruit = Instantiate(_fruitPrefab, transform.position, Quaternion.identity);

            SpawnedFruit.Initialize(_data.FruitData, SpawnLocation);
        }
        OnCollected?.Invoke();
        Destroy(gameObject);
    }

    private float GetRandomSpawnAxis() => Random.Range(-_randomSpawnValue, _randomSpawnValue);
}