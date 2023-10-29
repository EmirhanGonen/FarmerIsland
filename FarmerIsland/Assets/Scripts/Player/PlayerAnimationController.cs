using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
    private Animator _animator = null;

    private const string IDLE_KEY = "Idle";
    private const string MOVE_KEY = "Move";


    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable() {
        Joystick.OnDragAction += OnDragJoystick;
    }

    private void OnDisable() {
        Joystick.OnDragAction -= OnDragJoystick;
    }
    private void Start() {
        _animator.Play(Animator.StringToHash(IDLE_KEY));
    }

    private void OnDragJoystick(Vector2 Direction) {
        string Key = Direction.Equals(Vector2.zero) ? IDLE_KEY : MOVE_KEY;

        int Hash = Animator.StringToHash(Key);
        _animator.Play(Hash);
    }
}