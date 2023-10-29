using UnityEngine;

public static class Utilities
{
    public static bool HasType<T, K>(ref T Collider, out K Type) where T : Collider2D => Collider.TryGetComponent(out Type);
}