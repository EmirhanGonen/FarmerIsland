using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Utilities.HasType(ref collision, out FruitBush Bush))
        {
            Bush.Collect();
        }
    }
}