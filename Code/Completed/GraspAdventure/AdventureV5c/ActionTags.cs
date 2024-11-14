namespace AdventureV5c;

[Flags]
public enum ActionTags
{
    Aggressive = 2 << 0,
    Helpful = 2 << 1,
    Move = 2 << 2,
    Rest = 2 << 3,
    Investigate = 2 << 4,
    HandleObject = 2 << 5,
    TakeObject = 2 << 6,
    DropObject = 2 << 7,
    Quest = 2 << 8,
    DebugOnly = 2 << 9,
    Quit = 2 << 10,
    Error = 2 << 11
}
