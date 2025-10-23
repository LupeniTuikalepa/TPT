using System;

namespace TPT.Core.Data.Skills
{
    [Flags]
    public enum TargetTeam
    {
        Allies = 1,
        Enemies = 2,
        Self = 4,
    }

    public static class TargetTeamExtensions
    {
        public static bool HasFlagFast(this TargetTeam value, TargetTeam flag)
        {
            return (value & flag) != 0;
        }
    }
}