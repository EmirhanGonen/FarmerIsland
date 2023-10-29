using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/FruitData")]
public class FruitData : ScriptableObject
{
    [SerializeField] private Sprite _fruitSprite = null;
    [SerializeField] private string _fruitName = string.Empty;
    [SerializeField] private float _cost = 0.00f;

    public Sprite FruitSprite => _fruitSprite;
    public string FruitName => _fruitName;
    public float Cost => _cost;
}