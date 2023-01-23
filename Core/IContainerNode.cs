namespace BasicGUI;

using System.Collections.Generic;
public interface IContainerNode : INode
{
    IReadOnlyList<INode> GetChildren();
    ///<summary>
    ///Does three things:
    /// -iterates child containers
    /// -positions its children
    /// -determines its bounds
    /// </summary>
    void Iterate();
    /**
    <summary>
    Adds a child to the end of the children list, and sets that childs parent to this.
    </summary>
    */
    void AddChild(INode child);
    /**
    <summary>
    Adds a child to the end of the children list, and sets that childs parent to this.
    </summary>
    */
    void AddChildBeginning(INode child);
    void RemoveChild(INode child);
    /**
    <summary>
    Sets the selected node.
    Implementers simply call the parent's method, as the RootContianer is what keeps track of this.
    </summary>
    */
    void OnSelect(INode? selection);

    INode? GetSelectedNode();
}