using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Variables/IntVariable")]
public sealed class IntVariable : VariableBase<int>
{
    protected override void SetValue(int Value)
    {
        base.Value = Value;
    }
    protected override void IncreaseValue(int Amount)
    {
        base.Value += Amount;
    }
    protected override void DecreaseValue(int Amount)
    {
        base.Value -= Amount;
    }

    public static IntVariable operator +(IntVariable left, IntVariable right)
    {
        left.IncreaseValue(right.Value);

        return left;
    }

    public static IntVariable operator ++(IntVariable left)
    {
        left.IncreaseValue(1);

        return left;
    }
    public static IntVariable operator --(IntVariable left)
    {
        left.DecreaseValue(1);

        return left;
    }

    public static IntVariable operator -(IntVariable left, IntVariable right)
    {
        left.DecreaseValue(right.Value);

        return left;
    }
    public static IntVariable operator *(IntVariable left, IntVariable right)
    {
        left.SetValue(left.Value * right.Value);

        return left;
    }
    public static IntVariable operator /(IntVariable left, IntVariable right)
    {
        left.SetValue(left.Value / right.Value);

        return left;
    }
}