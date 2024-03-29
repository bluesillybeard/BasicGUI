namespace BasicGUI;

using System.Collections.Generic;
using System.Text;
using System;

//This class represents a text box.
// It's a container since it needs to hold a TextElement and a background.
public sealed class TextBoxElement : IContainerNode
{
    public INode? back; //This is the element that actually gets drawn as the text background.
    private readonly TextElement text; //the text object to render the text.

    public bool changed;

    public string GetText() {return text.Text;}
    public void SetText(string t)
    {
        text.Text = t;
    }
    public TextBoxElement(IContainerNode? parent, int fontSize, uint fontColor, object font, IDisplay display, byte depth)
    {
        back = null;
        Parent = parent;
        parent?.AddChild(this);
        text = new TextElement(this, fontColor, fontSize, "", font, display, depth);
    }
    public IReadOnlyList<INode> GetChildren()
    {
        if(back is not null)return new List<INode>(new INode[]{back, text});
        return new List<INode>(new INode[]{text});
    }

    public void Iterate()
    {
    }

    public INode? GetSelectedNode()
    {
        return Parent?.GetSelectedNode();
    }

    public void OnSelect(INode? selection)
    {
        Parent?.OnSelect(selection);
    }

    public void AddChild(INode node)
    {
        back = node;
    }
    public void AddChildBeginning(INode node)
    {
        back = node;
    }
    public void RemoveChild(INode node)
    {
        if(back == node)back = null;
    }
    public void Interact(IDisplay display)
    {
        if(this == GetSelectedNode())
        {
            HandleTyping(display);
        }
        else
        {
            //Check to see if we are being selected
            if(Bounds.ContainsPoint(display.GetMouseX(), display.GetMouseY()) && display.LeftMousePressed())
            {
                OnSelect(this);
            }
        }
    }

    private void HandleTyping(IDisplay display)
    {
        bool caps = display.CapsLock();
        bool shift = display.KeyDown(KeyCode.shift);
        bool num = display.NumLock();
        StringBuilder builder = new();
        builder.Append(text.Text);
        foreach(KeyCode key in display.PressedKeys())
        {
            char? c = KeyConverter.KeyDown(key, caps, shift, num);
            if(c is not null)
            {
                //System.Console.Write(c);
                //System.Console.Write(' ');
                //System.Console.WriteLine((int)c);
                switch(c)
                {
                    case '\b':
                        if(builder.Length > 0)builder.Remove(builder.Length-1, 1);
                        break;
                    default:
                        builder.Append(c);
                        break;
                }
                changed = true;
            }
        }
        text.Text = builder.ToString();
    }

    public void Draw(IDisplay display)
    {
        back?.Draw(display);
        text.Draw(display);
    }
    public void Absolutize()
    {
        XPos ??= 0;//set null values to zero
        YPos ??= 0;
        if(Parent is not null)
        {
            this.XPos += Parent.XPos ?? 0;
            this.YPos += Parent.YPos ?? 0;
        }
        text.XPos = XPos;
        text.YPos = YPos;
        if(back is not null)
        {
            //We don't want to override the minimum size
            back.XPos = text.XPos;
            back.YPos = text.YPos;
            back.Width = text.Width;
            back.Height = text.Height;
        }
    }
    public IContainerNode? Parent { get; set; }
    public NodeBounds Bounds {
        set {
            if(back is not null)back.Bounds = value;
        }
        get {
            if(back is not null)return back.Bounds;
            else return new NodeBounds(null, null, null, null, null, null);
        }
    }
    //Note: these tend to behave oddly when the drawable hasn't been set.
    public int? XPos {
        set {
            if(back is not null)back.XPos = value;
        }
        get {
            if(back is not null) return back.XPos;
            else return null;
        }
    }
    public int? YPos {
        set {
            if(back is not null)back.YPos = value;
        }
        get {
            if(back is not null) return back.YPos;
            else return null;
        }
    }
    public int? Width {
        set {
            if(back is not null)back.Width = value;
        }
        get {
            if(back is not null) return back.Width;
            else return null;
        }
    }
    public int? Height {
        set {
            if(back is not null)back.Height = value;
        }
        get {
            if(back is not null) return back.Height;
            else return null;
        }
    }
        public int? MinWidth {
        set {
            if(back is not null)back.MinWidth = value;
        }
        get {
            if(back is not null) return back.MinWidth;
            else return null;
        }
    }
    public int? MinHeight {
        set {
            if(back is not null)back.MinHeight = value;
        }
        get {
            if(back is not null) return back.MinHeight;
            else return null;
        }
    }
}
