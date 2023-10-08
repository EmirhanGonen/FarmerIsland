using UnityEngine;

public abstract class VariableBase<T> : ScriptableObject
{
    [SerializeField] private T _initialValue = default;
    private T _value = default;

    public T Value { get => _value; set => _value = value; }
    public T InitialValue => _initialValue;

    public void Initialize() => SetValue(InitialValue);

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