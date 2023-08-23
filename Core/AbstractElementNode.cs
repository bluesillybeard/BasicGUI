namespace BasicGUI;

public abstract class AbstractElementNode : IElementNode
{
    public byte depth;
    protected AbstractElementNode(IContainerNode? parent, byte depth)
    {
        this.depth = depth;
        Parent = parent;
        parent?.AddChild(this);
    }
    public virtual void Interact(IDisplay display) {}

    public abstract void Draw(IDisplay display);
    public void Absolutize()
    {
        XPos ??= 0;//set null values to zero
        YPos ??= 0;
        if(Parent is not null)
        {
            this.XPos += Parent.XPos ?? 0;
            this.YPos += Parent.YPos ?? 0;
        }
    }
    public IContainerNode? Parent { get; set; }
    public NodeBounds Bounds {set => _bounds = value; get => _bounds;}
    public int? XPos {set => _bounds.X = value; get => _bounds.X;}
    public int? YPos {set => _bounds.Y = value; get => _bounds.Y;}
    public int? Width {set => _bounds.W = value; get => _bounds.W;}
    public int? Height {set => _bounds.H = value; get => _bounds.H;}
    public int? MinWidth {set => _bounds.MW = value; get => _bounds.MW;}
    public int? MinHeight {set => _bounds.MH = value; get => _bounds.MH;}

    private NodeBounds _bounds;
}