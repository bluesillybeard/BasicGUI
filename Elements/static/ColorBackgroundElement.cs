namespace BasicGUI;
public class ColorBackgroundElement : AbstractElementNode
{
    public uint color;
    public ColorBackgroundElement(IContainerNode? parent, uint color, byte depth) : base(parent, depth)
    {
        this.color = color;
    }
    public override void Interact(IDisplay display)
    {
        //Set size to zero so there isn't any effect the placement of other objects.
        this.Bounds = new NodeBounds(null, null, 0, 0);
        if(this.GetParent() is AbstractContainerNode parent) parent.shrink = false;
        base.Interact(display);
    }

    public override void Draw(IDisplay display)
    {
        IContainerNode? parent = GetParent();
        NodeBounds bounds = parent is null ? new NodeBounds() : parent.Bounds;
        display.FillRect(bounds.X ?? 0, bounds.Y ?? 0, (bounds.X ?? 0) + (bounds.W ?? 0), (bounds.Y ?? 0) + (bounds.H ?? 0), color, depth);
    }
}