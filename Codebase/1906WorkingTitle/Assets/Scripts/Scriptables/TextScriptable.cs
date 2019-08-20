using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Scriptables/String")]
public class TextScriptable : ScriptableObject
{
    [Multiline]
    public string text;
}
