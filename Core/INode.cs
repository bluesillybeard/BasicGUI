namespace BasicGUI;

using System.Numerics;
public interface INode
{
    IContainerNode? Parent{get;set;}
    //These return null if it's unset, a value if it is set.
    NodeBounds Bounds {get;set;}
    int? XPos {get;set;}
    int? YPos {get;set;}
    int? Width {get;set;}
    int? Height {get;set;}

    int? MinWidth {get;set;}
    int? MinHeight {get;set;}

    //Elements just draw themself
    // Containers should call this function on all of its children.
    void Draw(IDisplay display);

    ///<summary> changes all of the relative coordinates of elements into absolute coordinates. </summary>
    void Absolutize();
    void Interact(IDisplay display);
}