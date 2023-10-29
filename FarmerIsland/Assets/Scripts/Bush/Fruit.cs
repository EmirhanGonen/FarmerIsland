using UnityEngine;
using DG.Tweening;
using System.Diagnostics;

[RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
public class Fruit : MonoBehaviour, IInteractable
{
    public static event System.Action<Fruit> OnInteracted;

    private SpriteRenderer _spriteRenderer = null;
    private BoxCollider2D _boxCollider = null;

    private FruitData _data = null;

    private bool _canInteract = true;

    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();

        _boxCollider.enabled = false;
    }

    public void Initialize<T>(T Data, Vector3 TargetLocation) where T : FruitData {
        _data = Data;

        _spriteRenderer.sprite = Data.FruitSprite;
        transform.DOMove(TargetLocation, 0.30f).SetEase(Ease.OutExpo).OnComplete(() =>
        {
            _boxCollider.enabled = true;
        });
    }


    public void Interact(PlayerCollector Collector) {
        if (!_canInteract)
            return;

        _canInteract = false;

        Vector2 NormalizedDirection = ((transform.position - Collector.transform.position) * 2).normalized;
        Vector2 TargetPoint = (NormalizedDirection * Random.Range(1.50f, 2.00f)) + (Vector2)Collector.transform.position;
        print($"Target Point: {TargetPoint} -> {Collector.transform.position} - {transform.position} * 2");

        float Duration = Vector2.Distance(Collector.transform.position, transform.position) * 0.1f;

        Tweener Collecting = transform.DOMove(TargetPoint, 0.30f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            transform.DOMove(Collector.transform.position, 0.1f).OnComplete(() =>
            {
                OnInteracted?.Invoke(this);
                Destroy(gameObject);
            });
        });
    }

    public FruitData GetItemData() => _data;
}