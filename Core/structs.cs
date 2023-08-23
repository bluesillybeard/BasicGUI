namespace BasicGUI;

//Represents an indeterminant position.
public struct NodeBounds
{
    public int? X, Y, MW, MH;
    //These are methods to enfoce the minimum width and height
    public int? W
    {
        readonly get => _w;
        set
        {
            _w = value;
            if(_w is not null && MW is not null && _w < MW)_w = MW;
        }
    }
    public int? H
        {
        readonly get => _h;
        set
        {
            _h = value;
            if(_h is not null && MH is not null && _h < MH)_w = MH;
        }
    }

    private int? _w, _h;
    public NodeBounds(int? x, int? y, int? w, int? h, int? mw, int? mh){
        this.X = x;
        this.Y = y;
        this.MW = mw;
        this.MH = mh;
        this._w = w;
        this._h = h;
        //I set them twice because I can't use these minimum enforcing ones before first setting all of the variables.
        this.W = w;
        this.H = h;
    }
    public readonly bool ContainsPoint(int xp, int yp)
    {
        int x0 = X ?? 0;
        int y0 = Y ?? 0;
        int xf = X+W ?? 0;
        int yf = Y+H ?? 0;
        return xp > x0 && xp < xf && yp > y0 && yp < yf;
    }

    public override readonly string ToString()
    {
        return $"({X},{Y},{W},{H},{MW},{MH})";
    }
}

public static class KeyConverter
{
    public static char? KeyDown(KeyCode keyIn, bool capsLock, bool shift, bool numLock)
    {
        //one or the other makes caps, 
        // both or non makes lowercase.
        bool capitalize = capsLock ^ shift;
        //C# switch statements are truly awful.
        // Get your switch game on Microsoft!
        // I thought C# was supposed to be better than Java!
        return keyIn switch
        {
            KeyCode.backspace => '\b',
            KeyCode.tab => '\t',
            KeyCode.enter => '\n',
            KeyCode.space => ' ',
            KeyCode.zero => shift ? ')' : '0',
            KeyCode.one => shift ? '!' : '1',
            KeyCode.two => shift ? '@' : '2',
            KeyCode.three => shift ? '#' : '3',
            KeyCode.four => shift ? '$' : '4',
            KeyCode.five => shift ? '%' : '5',
            KeyCode.six => shift ? '^' : '6',
            KeyCode.seven => shift ? '&' : '7',
            KeyCode.eight => shift ? '*' : '8',
            KeyCode.nine => shift ? '(' : '9',
            KeyCode.a => capitalize ? 'A' : 'a',
            KeyCode.b => capitalize ? 'B' : 'b',
            KeyCode.c => capitalize ? 'C' : 'c',
            KeyCode.d => capitalize ? 'D' : 'd',
            KeyCode.e => capitalize ? 'E' : 'e',
            KeyCode.f => capitalize ? 'F' : 'f',
            KeyCode.g => capitalize ? 'G' : 'g',
            KeyCode.h => capitalize ? 'H' : 'h',
            KeyCode.i => capitalize ? 'I' : 'i',
            KeyCode.j => capitalize ? 'J' : 'j',
            KeyCode.k => capitalize ? 'K' : 'k',
            KeyCode.l => capitalize ? 'L' : 'l',
            KeyCode.m => capitalize ? 'M' : 'm',
            KeyCode.n => capitalize ? 'N' : 'n',
            KeyCode.o => capitalize ? 'O' : 'o',
            KeyCode.p => capitalize ? 'P' : 'p',
            KeyCode.q => capitalize ? 'Q' : 'q',
            KeyCode.r => capitalize ? 'R' : 'r',
            KeyCode.s => capitalize ? 'S' : 's',
            KeyCode.t => capitalize ? 'T' : 't',
            KeyCode.u => capitalize ? 'U' : 'u',
            KeyCode.v => capitalize ? 'V' : 'v',
            KeyCode.w => capitalize ? 'W' : 'w',
            KeyCode.x => capitalize ? 'X' : 'x',
            KeyCode.y => capitalize ? 'Y' : 'y',
            KeyCode.z => capitalize ? 'Z' : 'z',
            KeyCode.num0 => numLock ? '0' : null,
            KeyCode.num1 => numLock ? '1' : null,
            KeyCode.num2 => numLock ? '2' : null,
            KeyCode.num3 => numLock ? '3' : null,
            KeyCode.num4 => numLock ? '4' : null,
            KeyCode.num5 => numLock ? '5' : null,
            KeyCode.num6 => numLock ? '6' : null,
            KeyCode.num7 => numLock ? '7' : null,
            KeyCode.num8 => numLock ? '8' : null,
            KeyCode.num9 => numLock ? '9' : null,
            KeyCode.multiply => numLock ? '*' : null,
            KeyCode.add => numLock ? '+' : null,
            KeyCode.subtract => numLock ? '-' : null,
            KeyCode.decimalPoint => numLock ? '.' : null,
            KeyCode.divide => numLock ? '/' : null,
            KeyCode.numLock => null,//numLock = !numLock; //numlock is a toggle
            KeyCode.semicolon => shift ? ':' : ';',
            KeyCode.equals => shift ? '+' : '=',
            KeyCode.comma => shift ? '<' : ',',
            KeyCode.dash => shift ? '_' : '-',
            KeyCode.period => shift ? '>' : '.',
            KeyCode.slash => shift ? '?' : '/',
            KeyCode.grave => shift ? '~' : '`',
            KeyCode.bracketLeft => shift ? '{' : '[',
            KeyCode.backSlash => shift ? '|' : '\\',
            KeyCode.bracketRight => shift ? '}' : ']',
            KeyCode.quote => shift ? '\"' : '\'',
            //This means its intentionally left out and should do nothing
            _ => null,
        };
    }
}