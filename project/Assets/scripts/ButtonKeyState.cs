internal struct ButtonKeyState
{
    public bool NewState;
    private bool OldState;

    public bool IsKeyDown
    {
        get
        {
            var isKeyDown = NewState && !OldState;
            OldState = NewState;
            return isKeyDown;
        }
    }
}