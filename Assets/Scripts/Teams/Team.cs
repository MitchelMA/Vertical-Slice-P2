using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Team
{
    [SerializeField]
    private List<Character> members = new List<Character>();
    public Character this[int index] => members[index];
    public Character Member(int index) => members[index];
    public IReadOnlyList<Character> Members => members.AsReadOnly();
}