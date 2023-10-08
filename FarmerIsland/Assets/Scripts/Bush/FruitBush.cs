using UnityEngine;

public class FruitBush : MonoBehaviour
{
    public static event System.Action OnCollected;
    [SerializeField] private int _level;


    public void Collect()
    {
        OnCollected?.Invoke();
        Destroy(gameObject);
    }

}