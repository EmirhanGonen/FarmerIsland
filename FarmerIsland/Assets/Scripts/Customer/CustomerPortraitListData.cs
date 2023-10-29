using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/List/CustomerPortraitList")]
public class CustomerPortraitListData : ScriptableObject
{
    [SerializeField] private List<Sprite> _customerPortraits = null;

    private int Count => _customerPortraits.Count;
    public Sprite GetRandomCustomerPortrait() => _customerPortraits[Random.Range(0, Count)];
}