using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerFlip : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer = null;

    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable() {
        Joystick.OnDragAction += OnDragJoystick;
    }

    private void OnDisable() {
        Joystick.OnDragAction -= OnDragJoystick;
    }

    private void OnDragJoystick(Vector2 Direction) {
        _spriteRenderer.flipX = Direction.x != 0.00f ? Direction.x < 0 : _spriteRenderer.flipX;
    }
}