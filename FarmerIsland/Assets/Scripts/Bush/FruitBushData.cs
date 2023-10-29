using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BushData")]
public class FruitBushData : ScriptableObject
{
    [SerializeField] private Sprite _bushSprite = null;
    [SerializeField] private FruitData _fruitData = null;
    [SerializeField] private IntVariable _bushLevel = null;

    [SerializeField, Min(1)] private int _necessaryLevel = 0;

    public Sprite BushSprite => _bushSprite;
    public FruitData FruitData => _fruitData;
    public IntVariable BushLevel => _bushLevel;

    public int NecessaryLevel => _necessaryLevel;
}