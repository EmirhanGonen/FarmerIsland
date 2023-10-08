using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Variables/FloatVariable")]
public sealed class FloatVariable : VariableBase<float>
{
    protected override void SetValue(float Value)
    {
        base.Value = Value;
    }
    protected override void IncreaseValue(float Amount)
    {
        base.Value += Amount;
    }
    protected override void DecreaseValue(float Amount)
    {
        base.Value -= Amount;
    }

    public static FloatVariable operator +(FloatVariable left, FloatVariable right)
    {
        left.IncreaseValue(right.Value);

        return left;
    }

    public static FloatVariable operator ++(FloatVariable left)
    {
        left.IncreaseValue(1);

        return left;
    }
    public static FloatVariable operator --(FloatVariable left)
    {
        left.DecreaseValue(1);

        return left;
    }

    public static FloatVariable operator -(FloatVariable left, FloatVariable right)
    {
        left.DecreaseValue(right.Value);

        return left;
    }
    public static FloatVariable operator *(FloatVariable left, FloatVariable right)
    {
        left.SetValue(left.Value * right.Value);

        return left;
    }
    public static FloatVariable operator /(FloatVariable left, FloatVariable right)
    {
        left.SetValue(left.Value / right.Value);

        return left;
    }
}