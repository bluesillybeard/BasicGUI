namespace BasicGUI;

public abstract class AbstractElementNode : IElementNode
{
    public byte depth;
    public AbstractElementNode(IContainerNode? parent, byte depth)
    {
        this.depth = depth;
        _parent = parent;
        if(parent is not null)parent.AddChild(this);
    }
    public virtual void Interact(IDisplay display) {}

    public abstract void Draw(IDisplay display);
    public void Absolutize()
    {
        XPos = XPos ?? 0;//set null values to zero
        YPos = YPos ?? 0;
        if(_parent is not null)
        {
            this.XPos += _parent.XPos ?? 0;
            this.YPos += _parent.YPos ?? 0;
        }
    }
    public IContainerNode? Parent
    {
        get => _parent;
        set => _parent = value;
    }
    public NodeBounds Bounds {set => _bounds = value; get => _bounds;}
    public int? XPos {set => _bounds.X = value; get => _bounds.X;}
    public int? YPos {set => _bounds.Y = value; get => _bounds.Y;}
    public int? Width {set => _bounds.W = value; get => _bounds.W;}
    public int? Height {set => _bounds.H = value; get => _bounds.H;}
    public int? MinWidth {set => _bounds.MW = value; get => _bounds.MW;}
    public int? MinHeight {set => _bounds.MH = value; get => _bounds.MH;}

    private IContainerNode? _parent;
    private NodeBounds _bounds;

    

}