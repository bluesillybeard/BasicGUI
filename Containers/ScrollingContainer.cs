namespace BasicGUI;

public sealed class ScrollingContainer : AbstractContainerNode
{
    float scroll; //Between 1 and 0; 0 being at the top, 1 being at the bottom
    public ScrollingContainer(IContainerNode? parent)
    : base(parent, true)
    {

    }

    public override void Iterate()
    {
        base.Iterate();
        // var children = GetChildren();
        // this.Width = children[0].Width;
        // this.Height = children[0].Height;
    }
    protected override void PositionChildren()
    {
        foreach(var child in GetChildren())
        {
            int y = child.YPos ?? 0;
            child.XPos = 0;
            child.YPos = y + (int)scroll*10;
        }
    }

    public override void Interact(IDisplay display)
    {
        base.Interact(display);
        if(Bounds.ContainsPoint(display.GetMouseX(), display.GetMouseY()))
        {
            scroll += display.ScrollDelta();
        }
    }
}