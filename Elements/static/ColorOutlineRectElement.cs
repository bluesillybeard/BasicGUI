namespace BasicGUI;

//This class is basically just a ColorRect element, except it draws four lines instead of filling it in.
public sealed class ColorOutlineRectElement : AbstractElementNode
{
    public ColorOutlineRectElement(IContainerNode? parent, uint rgba, int? width, int? height, int? minWidth, int? minHeight, int thickness, byte depth) : base(parent, depth)
    {
        Width = width;
        Height = height;
        MinWidth = minWidth;
        MinHeight = minHeight;
        this.rgba = rgba;
        this.thickness = thickness;
    }
    public uint rgba;
    public int thickness;
    public override void Draw(IDisplay display)
    {
        int x = XPos ?? 0;
        int y = YPos ?? 0;
        int width = Width ?? 0;
        int height = Height ?? 0;
        int yf = y + height;
        int xf = x + width;
		for (int offset = 0; offset < thickness; offset++)
		{
			display.DrawHorizontalLine(x + offset,   xf - offset, y + offset,  rgba, depth);
			display.DrawHorizontalLine(x + offset-1, xf - offset, yf - offset, rgba, depth);
			display.DrawVerticalLine  (x + offset,   y + offset,  yf - offset, rgba, depth);
			display.DrawVerticalLine  (xf - offset,  y + offset,  yf - offset, rgba, depth);
		}
    }
}