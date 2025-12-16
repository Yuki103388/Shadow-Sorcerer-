using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class ShowIfAttribute : PropertyAttribute
{
    public string ConditionFieldName;
    public bool Invert;

    public ShowIfAttribute(string conditionFieldName, bool invert = false)
    {
        ConditionFieldName = conditionFieldName;
        Invert = invert;
    }
}
