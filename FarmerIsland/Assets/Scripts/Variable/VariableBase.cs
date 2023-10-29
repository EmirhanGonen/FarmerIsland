using UnityEngine;

public abstract class VariableBase<T> : ScriptableObject
{
    [SerializeField] private T _initialValue = default;
    private T _value = default;

    public T Value
    {
        get
        {
            Debug.Assert(_initialized, "This Value Not Initialized");
            return _value;
        }
        set => _value = value;
    }

    public T InitialValue => _initialValue;

    private bool _initialized = false;

    public void Initialize() {
        SetValue(InitialValue);
        _initialized = true;
    }

    protected abstract void SetValue(T Value);
    protected abstract void IncreaseValue(T Amount);
    protected abstract void DecreaseValue(T Amount);
}

public interface IVariable<T>
{
    public T Value { get; set; }

    public abstract void SetValue(T Value);
    public abstract void IncreaseValue(T Amount);
    public abstract void DecreaseValue(T Amount);
}