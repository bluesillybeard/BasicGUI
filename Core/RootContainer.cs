namespace BasicGUI;

using System.Collections.Generic;
using System.Numerics;
public class RootContainer : IContainerNode
{
    public RootContainer(int width, int height) : this(width, height, new List<INode>()){}
    public RootContainer(int width, int height, List<INode> children)
    {
        _children = children;
        Width = width;
        Height = height;
    }
    public IContainerNode? Parent
    {
        get => null;
        set {}
    }    public void Iterate()
    {
        //Iterate children
        foreach(INode child in _children){
            child.XPos = null;
            child.YPos = null;
            if(child is IContainerNode container){
                container.Iterate();
            }
        }
    }
    public void Absolutize()
    {
        foreach(INode child in _children){
            child.Absolutize();
        }
    }
    public void Draw(IDisplay display)
    {
        foreach(INode node in _children){
            node.Draw(display);
        }
    }

    public void Interact(IDisplay display)
    {
        foreach(INode node in _children)
        {
            node.Interact(display);
        }
    }

    //We are the root container, so we keep track of who is selected.
    public INode? GetSelectedNode()
    {
        return _selection;
    }

    public void OnSelect(INode? selection)
    {
        _selection = selection;
    }

    public NodeBounds Bounds {
        get => new(0, 0, Width, Height, 0, 0);
        set {
            //XPos and YPos must always be 0, so the position is ignored.
            Width = value.W;
            Height = value.H;
        }
    }
    public int? XPos {set {} get => 0;}
    public int? YPos {set {} get => 0;}
    public int? Width {set => _width = value ?? 0; get => _width;}
    public int? Height {set => _height = value ?? 0; get => _height;}
    public int? MinWidth {set {} get => 0;}
    public int? MinHeight {set {} get => 0;}
    public IReadOnlyList<INode> GetChildren() => _children;
    public void AddChild(INode child) {_children.Add(child);}
    public void AddChildBeginning(INode child) {_children.Insert(0, child);}
    public void RemoveChild(INode child) {_children.Remove(child);}

    private readonly List<INode> _children;
    private int _width;
    private int _height;

    private INode? _selection;
}