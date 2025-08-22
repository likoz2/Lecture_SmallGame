namespace Lecture_SmallGame.SmallEngine.Components;

/// <summary>
/// A base <see cref="Component"/> that allows rendering handling for inheriting classes.
/// </summary>
public abstract class Renderer : Component
{
    public int Layer { get; set { field = value; OnLayerChange(value); Writer.OnRendererChangeLayer(this); } } = 10;

    public abstract Bounds Bounds { get; }
    public abstract Vector LocalPosition { get; set; }
    public bool IsDirty { get; set; }

    public Renderer(GameObject gameObject) : base(gameObject)
    {
    }

    public abstract void OnLayerChange(int value);

    public abstract char CurrentCharAt(int x, int y);
    public abstract char CurrentCharAtGlobal(int x, int y);

    public abstract PixelData CurrentPixelDataAt(int x, int y);
    public abstract PixelData CurrentPixelDataAtGlobal(int x, int y);
}