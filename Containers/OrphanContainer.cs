namespace BasicGUI;
using System.Collections.Generic;
public class OrphanageContainer : IContainerNode
{
    //an orphan container does not require a parent, allowing sections of the GUI tree to be moved/hidden/reshown
    // without completely reconstructing its part of the tree.
    // It's a botched solution but I honestly don't really care.
    public OrphanageContainer()
    {
        _children = new List<INode>();
    }
    public List<INode> GetChildren()
    {
        return _children;
    }
    public void Iterate()
    {
        foreach(var child in _children)
        {
            if(child is IContainerNode container)
            {
                container.Iterate();
            }
        }
    }
    public void AddChild(INode child)
    {
        _children.Add(child);
    }
    public void OnSelect(INode selection)
    {
        if(_parent is not null)_parent.OnSelect(selection);

    }
    public INode? GetSelectedNode()
    {
        if(_parent is not null) return _parent.GetSelectedNode();
        return null;
    }

    private List<INode> _children;
    private IContainerNode? _parent;
}