
namespace Lecture_SmallGame.SmallEngine.Components;

// TODO implement multiline, wrap and overflow, maybe some color animation or _ animation
/// <summary>
/// A <see cref="Component"/> that handles text rendering. Currently supports only single line text.
/// </summary>
public class TextRenderer : Renderer
{
    public string Text { get; set { string lastText = field; field = value; OnTextChange(lastText); } } = "";

    public Sprite Sprite { get; set { field = value; } }
    public override Vector LocalPosition { get; set { field = value; Writer.ReRender(Bounds); } }
    public override Bounds Bounds => new Bounds(Transform.Position + LocalPosition, new Vector(Text.Length, 1));

    public Color ColorTopLeft { get; set { field = value; OnColorChange(); } } = Color.Gray;
    public Color ColorTopRight { get; set { field = value; OnColorChange(); } } = Color.Gray;
    public Color ColorBotLeft { get; set { field = value; OnColorChange(); } } = Color.Gray;
    public Color ColorBotRight { get; set { field = value; OnColorChange(); } } = Color.Gray;


    public TextRenderer(GameObject gameObject) : base(gameObject)
    {
        Text = "Weee";
    }

    // HELP How do I calculate the color for each char? Do I keep a complete cache of all the colors or recalc every ReRender?
    // HELP When I assign ColorTopLeft and then immediatelly ColorTopRight it will recalc it twice, probably cache and recalc on ReRender of double buffer?
    // HELP I am a bit lost at this as I feel every idea has downfalls...
    // TODO implement 2D color
    private void OnColorChange()
    {
        UpdateColors();
        Writer.ReRender(Bounds);
    }

    private void UpdateColors()
    {
        Color stepTopLine = (ColorTopRight - ColorTopLeft) / Sprite.Width;
        Color stepBotLine = (ColorBotRight - ColorBotLeft) / Sprite.Width;
        for (int i = 0; i < Sprite.Width; i++)
        {
            for (int j = 0; j < Sprite.Height; j++)
            {
                Sprite.PixelDataAt(i, j).ForegroundColor = ColorTopLeft + stepTopLine * i;
            }
        }
    }

    private void OnTextChange(string lastText)
    {
        Sprite = new Sprite([Text]);
        int len = Math.Max(lastText.Length, Text.Length);
        Bounds bounds = new Bounds(Transform.Position + LocalPosition, new Vector(len, 1));
        UpdateColors();
        Writer.ReRender(bounds);
    }

    public override char CurrentCharAt(int x, int y)
    {
        return Sprite.CharAt(x, y);
    }

    public override PixelData CurrentPixelDataAt(int x, int y)
    {
        return Sprite.PixelDataAt(x, y);
    }

    public override void OnLayerChange(int value)
    {
        Sprite.Layer = value;
    }

    public override char CurrentCharAtGlobal(int x, int y)
    {
        Vector vec = new Vector(x, y) - Transform.Position - LocalPosition;
        return CurrentCharAt((int)vec.X, (int)vec.Y);
    }

    public override PixelData CurrentPixelDataAtGlobal(int x, int y)
    {
        Vector vec = new Vector(x, y) - Transform.Position - LocalPosition;
        return CurrentPixelDataAt((int)vec.X, (int)vec.Y);
    }
}
