using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Team
{
    [SerializeField]
    private List<Character> members;

    public Character this[int index] => members[index];
    public Character Member(int index) => members[index];
    public IReadOnlyList<Character> Members => members.AsReadOnly();
}