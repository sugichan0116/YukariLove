using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class DynamicDropdownAttribute : PropertyAttribute
{
    public string enumMethodName { get; private set; }

    public DynamicDropdownAttribute(string name)
    {
        enumMethodName = name;
    }
}
