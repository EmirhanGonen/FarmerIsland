using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    public static System.Action<Fruit> OnInteractedFruitAction = null;

    private void OnEnable() {
        Fruit.OnInteracted += OnInteractedFruit;
    }

    private void OnDisable() {
        Fruit.OnInteracted -= OnInteractedFruit;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (Utilities.HasType(ref collision, out IInteractable Interactable))
            Interactable.Interact(this);
    }

    private void OnInteractedFruit<T>(T Fruit) where T : Fruit {
        OnInteractedFruitAction?.Invoke(Fruit);
    }

}