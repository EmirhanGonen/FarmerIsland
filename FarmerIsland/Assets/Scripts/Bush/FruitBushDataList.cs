using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "ScriptableObjects/List/FruitBushDataList")]
public class FruitBushDataList : ScriptableObject
{
    [SerializeField] private List<FruitBushData> _datas = null;

    private int Count => _datas.Count;


    public FruitBushData GetRandomData() => _datas[Random.Range(0, Count)];
    public List<FruitBushData> Datas => _datas;

    public FruitBushData GetDataByLevel(int Level) {
        foreach (FruitBushData Data in _datas)
        {
            if (!Data.NecessaryLevel.Equals(Level))
                continue;

            return Data;
        }

        return null;
    }
}