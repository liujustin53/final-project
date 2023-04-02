using UnityEngine;
using System;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
public class DrawIf : PropertyAttribute
{
    public string toggleName { get; private set; }
    public bool invert { get; private set; }

    public DrawIf(string toggleName, bool inverted)
    {
        this.toggleName = toggleName;
        this.invert = inverted;
    }
}
