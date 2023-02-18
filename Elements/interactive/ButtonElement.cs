namespace BasicGUI;

using System.Collections.Generic;

using System;
//a button is the most basic element.
// You can push it, and... that's it really.
// The button does not have any interaction code itself, if you want to, for example, make it bigger when its hovered, you have to do that yourself.
// There are two ways to use a Button: boolean flags read each iteration, or function callbacks. You can use both at the same time if you would like.
public sealed class ButtonElement : IContainerNode
{
    public INode? drawable; //This is the element that actually gets drawn as the button.\
    public bool wasHover;
    public bool isHover;
    public bool isDown;
    public bool wasDown;
    public Action<ButtonElement>? OnHover;
    public Action<ButtonElement>? OnUnHover;
    public Action<ButtonElement>? OnClick;
    public Action<ButtonElement>? OnUnClick;
    public Action<ButtonElement>? OnFrame;

    public ButtonElement(IContainerNode? parent, 
    Action<ButtonElement>? onHover, Action<ButtonElement>? onUnHover,
    Action<ButtonElement>? onClick, Action<ButtonElement>? onUnClick,
    Action<ButtonElement>? onFrame)
    {
        this.drawable = null;
        _parent = parent;
        if(parent is not null)parent.AddChild(this);
        this.OnClick = onClick;
        this.OnUnClick = onUnClick;
        this.OnHover = onHover;
        this.OnUnHover = onUnHover;
        this.OnFrame = onFrame;
    }

    public ButtonElement(IContainerNode parent) : this(parent, null, null, null, null, null){}

    public IReadOnlyList<INode> GetChildren()
    {
        //TODO: more optimized version that doesn't needlessly allocating memory
        if(drawable is not null)return new List<INode>(new INode[]{drawable});
        return new List<INode>();
    }

    public void Iterate()
    {
        if(OnFrame is not null)OnFrame(this);
    }

    public INode? GetSelectedNode()
    {
        return _parent is null ? null : _parent.GetSelectedNode();
    }

    public void OnSelect(INode? selection)
    {
        if(_parent is not null)_parent.OnSelect(selection);
    }

    public void AddChild(INode node)
    {
        drawable = node;
    }
    public void AddChildBeginning(INode node)
    {
        drawable = node;
    }
    public void RemoveChild(INode node)
    {
        if(drawable == node)drawable = null;
    }
    public void Interact(IDisplay display)
    {
        wasDown = isDown;
        isDown = false;
        wasHover = isHover;
        isHover = false;
        if(Bounds.ContainsPoint(display.GetMouseX(), display.GetMouseY()))
        {
            isHover = true;
            if(display.LeftMouseDown())
            {
                isDown = true;
            }
        }
        if(isHover && !wasHover && OnHover is not null)OnHover(this);
        else if(!isHover && wasHover && OnUnHover is not null)OnUnHover(this);
        if(isDown && !wasDown && OnClick is not null)OnClick(this);
        else if(!isDown && wasDown && OnUnClick is not null)OnUnClick(this);
    }

    public void Draw(IDisplay display)
    {
        if(drawable is not null)drawable.Draw(display);
    }
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
    public NodeBounds Bounds {
        set {
            if(drawable is not null)drawable.Bounds = value;
        } 
        get {
            if(drawable is not null)return drawable.Bounds;
            else return new NodeBounds(null, null, null, null, null, null);
        }
    }
    //Note: these tend to behave oddly when the drawable hasn't been set.
    public int? XPos {
        set {
            if(drawable is not null)drawable.XPos = value;
        }
        get {
            if(drawable is not null) return drawable.XPos;
            else return null;
        }
    }
    public int? YPos {
        set {
            if(drawable is not null)drawable.YPos = value;
        }
        get {
            if(drawable is not null) return drawable.YPos;
            else return null;
        }
    }
    public int? Width {
        set {
            if(drawable is not null)drawable.Width = value;
        }
        get {
            if(drawable is not null) return drawable.Width;
            else return null;
        }
    }
    public int? Height {
        set {
            if(drawable is not null)drawable.Height = value;
        }
        get {
            if(drawable is not null) return drawable.Height;
            else return null;
        }
    }
    public int? MinWidth {
        set {
            if(drawable is not null)drawable.MinWidth = value;
        }
        get {
            if(drawable is not null) return drawable.MinWidth;
            else return null;
        }
    }
    public int? MinHeight {
        set {
            if(drawable is not null)drawable.MinHeight = value;
        }
        get {
            if(drawable is not null) return drawable.MinHeight;
            else return null;
        }
    }
    private IContainerNode? _parent;
}
