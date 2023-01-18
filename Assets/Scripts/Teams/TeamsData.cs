
using UnityEngine;

public class TeamsData : GenericSingleton<TeamsData>
{
    // First team should be right team, second team should be left team;
    [SerializeField] private Team[] teams = new Team[2];

    public Team this[Side side] => teams[(int)side];
    public Team Team(Side side) => teams[(int) side];
    public Team[] Teams => teams;
}