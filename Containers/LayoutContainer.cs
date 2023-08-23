namespace BasicGUI;

using BasicGUI;
using System.Collections.Generic;
/**
 * This is a container that anchors container children to a certain position relative to its parent.
 * Same remarks as with Centercontainer; so be careful who the parent is.
*/

public enum VAllign{
    top, center, bottom
}
public enum HAllign{
    left, center, right
}

public sealed class LayoutContainer : AbstractContainerNode
{
    public LayoutContainer(IContainerNode? parent, List<INode> children, VAllign vertical, HAllign horizontal) : base(parent, children)
    {
        this.vertical = vertical;
        this.horizontal = horizontal;
    }
    public LayoutContainer(IContainerNode? parent, VAllign vertical, HAllign horizontal) : this(parent, new List<INode>(), vertical, horizontal) {}
    public VAllign vertical;
    public HAllign horizontal;
    protected override void PositionChildren()
    {
        IContainerNode? parent = Parent;
        int? parentWidth = parent?.Width;
        int? parentHeight = parent?.Height;
        if(parentHeight is null || parentWidth is null)
        {
            System.Console.Error.WriteLine("ERROR: invalid parent bounds within LayoutContainer");
            return;
        }
        foreach(INode node in GetChildren())
        {
            //A nodes position is relative to the top left.
            int? nodeWidth = node.Width;
            int? nodeHeight = node.Height;
            if(nodeHeight is null || nodeWidth is null)
            {
                System.Console.Error.WriteLine("Warning: invalid node bounds within LayoutContainer");
                continue;
            }
            node.XPos = horizontal switch
            {
                HAllign.left => 0,
                HAllign.center => (parentWidth.Value - nodeWidth.Value) / 2,
                HAllign.right => parentWidth.Value - nodeWidth,
                _ => null,
            };
            node.YPos = vertical switch
            {
                VAllign.top => 0,
                VAllign.center => (parentHeight.Value - nodeHeight.Value) / 2,
                VAllign.bottom => parentHeight.Value - nodeHeight,
                _ => null,
            };
        }
    }
}