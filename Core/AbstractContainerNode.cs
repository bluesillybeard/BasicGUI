namespace BasicGUI;

using System.Collections.Generic;
using System.Numerics;
public abstract class AbstractContainerNode : IContainerNode
{
    protected AbstractContainerNode(IContainerNode? parent) : this(parent, new List<INode>()){}
    protected AbstractContainerNode(IContainerNode? parent, bool shrink) : this(parent, new List<INode>(), shrink){}
    protected AbstractContainerNode(IContainerNode? parent, List<INode> children, bool shrink = true)
    {
        _children = children;
        Parent = parent;
        this.shrink = shrink;
        parent?.AddChild(this);
    }
    public bool shrink; //weather or not to shrink the bounds of this container to the elements it contains.
    protected abstract void PositionChildren();
    public IContainerNode? Parent { get; set; }

    void DetermineBounds()
    {
        int top = int.MaxValue; //lowest Y value
        int left = int.MaxValue; // lowest X value
        int bottom = int.MinValue; //highest Y value
        int right = int.MinValue; //highest X value
        //Go through all of the children and find out our bounds.
        foreach(INode child in _children)
        {
            int childX = child.XPos ?? 0;
            int childY = child.YPos ?? 0;
            int childWidth = child.Width ?? 0;
            int childHeight = child.Height ?? 0;
            //actually get the mins and maxs
            if(top > childY)top = childY;
            if(left > childX)left = childX;
            int childBottom = childY+childHeight;
            if(bottom < childBottom)bottom = childBottom;
            int childRight = childX + childWidth;
            if(right < childRight)right = childRight;
        }
        _bounds.W = right - left;
        _bounds.H = bottom - top;
        //We don't find the position here, since the parent container is what determines the position.
        // We couldn't do that anyway since the position of an element is relative to its parent.
    }

    public virtual void Iterate()
    {
        //Iterate children
        foreach(INode child in _children){
            child.XPos = null;
            child.YPos = null;
            if(child is IContainerNode container){
                container.Iterate();
            }
        }
        PositionChildren(); //This is implemented by subclasses.
        if(shrink)DetermineBounds();
    }

    public virtual void Absolutize()
    {
        XPos ??= 0;//set null values to zero
        YPos ??= 0;
        if(Parent is not null)
        {
            this.XPos += Parent.XPos ?? 0;
            this.YPos += Parent.YPos ?? 0;
        }
        foreach(INode child in _children){
            child.Absolutize();
        }
    }

    public virtual void Interact(IDisplay display)
    {
        foreach(INode node in _children)
        {
            node.Interact(display);
        }
    }

    public virtual void Draw(IDisplay display)
    {
        foreach(INode node in _children)
        {
            node.Draw(display);
        }
    }

    public INode? GetSelectedNode()
    {
        return Parent?.GetSelectedNode();
    }

    public void OnSelect(INode? selection)
    {
        Parent?.OnSelect(selection);
    }

    public NodeBounds Bounds {set => _bounds = value; get => _bounds;}
    public int? XPos {set => _bounds.X = value; get => _bounds.X;}
    public int? YPos {set => _bounds.Y = value; get => _bounds.Y;}
    public int? Width {set => _bounds.W = value; get => _bounds.W;}
    public int? Height {set => _bounds.H = value; get => _bounds.H;}
    public int? MinWidth {set => _bounds.MW = value; get => _bounds.MW;}
    public int? MinHeight {set => _bounds.MH = value; get => _bounds.MH;}
    public IReadOnlyList<INode> GetChildren() => _children;
    public void AddChild(INode child) {_children.Add(child);}
    public void AddChildBeginning(INode child) {_children.Insert(0, child);}
    public void RemoveChild(INode child) {_children.Remove(child);}
    private NodeBounds _bounds;

    private readonly List<INode> _children;
}