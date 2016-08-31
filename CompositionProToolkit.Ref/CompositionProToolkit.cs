namespace CompositionProToolkit
{
    public delegate void CacheProgressHandler(int progress);
    public static partial class CompositionExtensions
    {
        public static Windows.UI.Composition.CompositionEffectBrush CreateMaskedBackdropBrush(this Windows.UI.Composition.Compositor compositor, CompositionProToolkit.IMaskSurface mask, Windows.UI.Color blendColor, float blurAmount, Windows.UI.Composition.CompositionBackdropBrush backdropBrush=null) { return default(Windows.UI.Composition.CompositionEffectBrush); }
        public static void UpdateSurfaceBrushOptions(this Windows.UI.Composition.CompositionSurfaceBrush surfaceBrush, Windows.UI.Xaml.Media.Stretch stretch, Windows.UI.Xaml.Media.AlignmentX alignX, Windows.UI.Xaml.Media.AlignmentY alignY, Windows.UI.Composition.ScalarKeyFrameAnimation alignXAnimation=null, Windows.UI.Composition.ScalarKeyFrameAnimation alignYAnimation=null) { }
    }
    public static partial class CompositionGeneratorFactory
    {
        public static CompositionProToolkit.ICompositionGenerator GetCompositionGenerator(Windows.UI.Composition.CompositionGraphicsDevice graphicsDevice) { return default(CompositionProToolkit.ICompositionGenerator); }
        public static CompositionProToolkit.ICompositionGenerator GetCompositionGenerator(Windows.UI.Composition.Compositor compositor, bool useSharedCanvasDevice=true, bool useSoftwareRenderer=false) { return default(CompositionProToolkit.ICompositionGenerator); }
    }
    public partial interface ICompositionGenerator : System.IDisposable
    {
        Microsoft.Graphics.Canvas.CanvasDevice Device { get; }
        event System.EventHandler<object> DeviceReplaced;
        CompositionProToolkit.IGeometrySurface CreateGeometrySurface(Windows.Foundation.Size size, Microsoft.Graphics.Canvas.Geometry.CanvasGeometry geometry, Microsoft.Graphics.Canvas.Brushes.ICanvasBrush foregroundBrush);
        CompositionProToolkit.IGeometrySurface CreateGeometrySurface(Windows.Foundation.Size size, Microsoft.Graphics.Canvas.Geometry.CanvasGeometry geometry, Microsoft.Graphics.Canvas.Brushes.ICanvasBrush foregroundBrush, Microsoft.Graphics.Canvas.Brushes.ICanvasBrush backgroundBrush);
        CompositionProToolkit.IGeometrySurface CreateGeometrySurface(Windows.Foundation.Size size, Microsoft.Graphics.Canvas.Geometry.CanvasGeometry geometry, Microsoft.Graphics.Canvas.Brushes.ICanvasBrush foregroundBrush, Windows.UI.Color backgroundColor);
        CompositionProToolkit.IGeometrySurface CreateGeometrySurface(Windows.Foundation.Size size, Microsoft.Graphics.Canvas.Geometry.CanvasGeometry geometry, Windows.UI.Color foregroundColor);
        CompositionProToolkit.IGeometrySurface CreateGeometrySurface(Windows.Foundation.Size size, Microsoft.Graphics.Canvas.Geometry.CanvasGeometry geometry, Windows.UI.Color foregroundColor, Microsoft.Graphics.Canvas.Brushes.ICanvasBrush backgroundBrush);
        CompositionProToolkit.IGeometrySurface CreateGeometrySurface(Windows.Foundation.Size size, Microsoft.Graphics.Canvas.Geometry.CanvasGeometry geometry, Windows.UI.Color foregroundColor, Windows.UI.Color backgroundColor);
        System.Threading.Tasks.Task<CompositionProToolkit.IImageSurface> CreateImageSurfaceAsync(System.Uri uri, Windows.Foundation.Size size, CompositionProToolkit.ImageSurfaceOptions options);
        CompositionProToolkit.IMaskSurface CreateMaskSurface(Windows.Foundation.Size size, Microsoft.Graphics.Canvas.Geometry.CanvasGeometry geometry);
        void CreateReflection(Windows.UI.Composition.ContainerVisual visual, float reflectionDistance=0f, float reflectionLength=0.7f, CompositionProToolkit.ReflectionLocation location=(CompositionProToolkit.ReflectionLocation)(0));
    }
    public partial interface IGeometrySurface : CompositionProToolkit.IRenderSurface, System.IDisposable
    {
        Microsoft.Graphics.Canvas.Brushes.ICanvasBrush BackgroundBrush { get; }
        Microsoft.Graphics.Canvas.Brushes.ICanvasBrush ForegroundBrush { get; }
        Microsoft.Graphics.Canvas.Geometry.CanvasGeometry Geometry { get; }
        void Redraw(Microsoft.Graphics.Canvas.Brushes.ICanvasBrush foregroundBrush);
        void Redraw(Microsoft.Graphics.Canvas.Brushes.ICanvasBrush foregroundBrush, Microsoft.Graphics.Canvas.Brushes.ICanvasBrush backgroundBrush);
        void Redraw(Microsoft.Graphics.Canvas.Brushes.ICanvasBrush foregroundBrush, Windows.UI.Color backgroundColor);
        void Redraw(Microsoft.Graphics.Canvas.Geometry.CanvasGeometry geometry);
        void Redraw(Windows.Foundation.Size size, Microsoft.Graphics.Canvas.Geometry.CanvasGeometry geometry);
        void Redraw(Windows.Foundation.Size size, Microsoft.Graphics.Canvas.Geometry.CanvasGeometry geometry, Microsoft.Graphics.Canvas.Brushes.ICanvasBrush foregroundBrush);
        void Redraw(Windows.Foundation.Size size, Microsoft.Graphics.Canvas.Geometry.CanvasGeometry geometry, Microsoft.Graphics.Canvas.Brushes.ICanvasBrush foregroundBrush, Microsoft.Graphics.Canvas.Brushes.ICanvasBrush backgroundBrush);
        void Redraw(Windows.Foundation.Size size, Microsoft.Graphics.Canvas.Geometry.CanvasGeometry geometry, Microsoft.Graphics.Canvas.Brushes.ICanvasBrush foregroundBrush, Windows.UI.Color backgroundColor);
        void Redraw(Windows.Foundation.Size size, Microsoft.Graphics.Canvas.Geometry.CanvasGeometry geometry, Windows.UI.Color foregroundColor);
        void Redraw(Windows.Foundation.Size size, Microsoft.Graphics.Canvas.Geometry.CanvasGeometry geometry, Windows.UI.Color foregroundColor, Microsoft.Graphics.Canvas.Brushes.ICanvasBrush backgroundBrush);
        void Redraw(Windows.Foundation.Size size, Microsoft.Graphics.Canvas.Geometry.CanvasGeometry geometry, Windows.UI.Color foregroundColor, Windows.UI.Color backgroundColor);
        void Redraw(Windows.UI.Color foregroundColor);
        void Redraw(Windows.UI.Color foregroundColor, Microsoft.Graphics.Canvas.Brushes.ICanvasBrush backgroundBrush);
        void Redraw(Windows.UI.Color foregroundColor, Windows.UI.Color backgroundColor);
    }
    public partial interface IImageSurface : CompositionProToolkit.IRenderSurface, System.IDisposable
    {
        CompositionProToolkit.ImageSurfaceOptions Options { get; }
        System.Uri Uri { get; }
        void Redraw(CompositionProToolkit.ImageSurfaceOptions options);
        System.Threading.Tasks.Task RedrawAsync(System.Uri uri, CompositionProToolkit.ImageSurfaceOptions options);
        System.Threading.Tasks.Task RedrawAsync(System.Uri uri, Windows.Foundation.Size size, CompositionProToolkit.ImageSurfaceOptions options);
        void Resize(Windows.Foundation.Size size, CompositionProToolkit.ImageSurfaceOptions options);
    }
    public static partial class ImageCache
    {
        public static System.Threading.Tasks.Task<System.Uri> GetCachedUriAsync(object objectToCache, CompositionProToolkit.CacheProgressHandler progressHandler=null) { return default(System.Threading.Tasks.Task<System.Uri>); }
    }
    public partial class ImageSurfaceOptions
    {
        public ImageSurfaceOptions() { }
        public bool AutoResize { [System.Runtime.CompilerServices.CompilerGeneratedAttribute]get { return default(bool); } [System.Runtime.CompilerServices.CompilerGeneratedAttribute]set { } }
        public static CompositionProToolkit.ImageSurfaceOptions Default { get { return default(CompositionProToolkit.ImageSurfaceOptions); } }
        public static CompositionProToolkit.ImageSurfaceOptions DefaultOptimized { get { return default(CompositionProToolkit.ImageSurfaceOptions); } }
        public Windows.UI.Xaml.Media.AlignmentX HorizontalAlignment { [System.Runtime.CompilerServices.CompilerGeneratedAttribute]get { return default(Windows.UI.Xaml.Media.AlignmentX); } [System.Runtime.CompilerServices.CompilerGeneratedAttribute]set { } }
        public Microsoft.Graphics.Canvas.CanvasImageInterpolation Interpolation { [System.Runtime.CompilerServices.CompilerGeneratedAttribute]get { return default(Microsoft.Graphics.Canvas.CanvasImageInterpolation); } [System.Runtime.CompilerServices.CompilerGeneratedAttribute]set { } }
        public float Opacity { [System.Runtime.CompilerServices.CompilerGeneratedAttribute]get { return default(float); } [System.Runtime.CompilerServices.CompilerGeneratedAttribute]set { } }
        public Windows.UI.Xaml.Media.Stretch Stretch { [System.Runtime.CompilerServices.CompilerGeneratedAttribute]get { return default(Windows.UI.Xaml.Media.Stretch); } [System.Runtime.CompilerServices.CompilerGeneratedAttribute]set { } }
        public Windows.UI.Color SurfaceBackgroundColor { [System.Runtime.CompilerServices.CompilerGeneratedAttribute]get { return default(Windows.UI.Color); } [System.Runtime.CompilerServices.CompilerGeneratedAttribute]set { } }
        public Windows.UI.Xaml.Media.AlignmentY VerticalAlignment { [System.Runtime.CompilerServices.CompilerGeneratedAttribute]get { return default(Windows.UI.Xaml.Media.AlignmentY); } [System.Runtime.CompilerServices.CompilerGeneratedAttribute]set { } }
    }
    public partial interface IMaskSurface : CompositionProToolkit.IRenderSurface, System.IDisposable
    {
        Microsoft.Graphics.Canvas.Geometry.CanvasGeometry Geometry { get; }
        void Redraw(Microsoft.Graphics.Canvas.Geometry.CanvasGeometry geometry);
        void Redraw(Windows.Foundation.Size size, Microsoft.Graphics.Canvas.Geometry.CanvasGeometry geometry);
    }
    public partial interface IRenderSurface : System.IDisposable
    {
        CompositionProToolkit.ICompositionGenerator Generator { get; }
        Windows.Foundation.Size Size { get; }
        Windows.UI.Composition.ICompositionSurface Surface { get; }
        void Redraw();
        void Resize(Windows.Foundation.Size size);
    }
    public enum ReflectionLocation
    {
        Bottom = 0,
        Left = 2,
        Right = 3,
        Top = 1,
    }
}
namespace CompositionProToolkit.Common
{
    public static partial class Utils
    {
        public static Windows.Foundation.Size CollapseThickness(this Windows.UI.Xaml.Thickness thick) { return default(Windows.Foundation.Size); }
        public static double ConvertToValidCornerValue(double corner) { return default(double); }
        public static Windows.Foundation.Rect Deflate(this Windows.Foundation.Rect rect, Windows.UI.Xaml.Thickness thick) { return default(Windows.Foundation.Rect); }
        public static Windows.Foundation.Rect Inflate(this Windows.Foundation.Rect rect, Windows.UI.Xaml.Thickness thick) { return default(Windows.Foundation.Rect); }
        public static bool IsCloseTo(this double value1, double value2) { return default(bool); }
        public static bool IsCloseTo(this float value1, float value2) { return default(bool); }
        public static bool IsCloseTo(this Windows.Foundation.Point point1, Windows.Foundation.Point point2) { return default(bool); }
        public static bool IsCloseTo(this Windows.Foundation.Rect rect1, Windows.Foundation.Rect rect2) { return default(bool); }
        public static bool IsCloseTo(this Windows.Foundation.Size size1, Windows.Foundation.Size size2) { return default(bool); }
        public static bool IsEqualTo(this System.Uri uri, System.Uri otherUri) { return default(bool); }
        public static bool IsEqualTo(this Windows.UI.Xaml.Media.Brush brush, Windows.UI.Xaml.Media.Brush otherBrush) { return default(bool); }
        public static bool IsGreaterThan(this double value1, double value2) { return default(bool); }
        public static bool IsGreaterThan(this float value1, float value2) { return default(bool); }
        public static bool IsLessThan(double value1, double value2) { return default(bool); }
        public static bool IsLessThan(float value1, float value2) { return default(bool); }
        public static bool IsNaN(double value) { return default(bool); }
        public static bool IsOne(this double value) { return default(bool); }
        public static bool IsOne(this float value) { return default(bool); }
        public static bool IsOpaqueSolidColorBrush(this Windows.UI.Xaml.Media.Brush brush) { return default(bool); }
        public static bool IsUniform(this Windows.UI.Xaml.CornerRadius corner) { return default(bool); }
        public static bool IsUniform(this Windows.UI.Xaml.Thickness thick) { return default(bool); }
        public static bool IsValid(this Windows.UI.Xaml.CornerRadius corner, bool allowNegative, bool allowNaN, bool allowPositiveInfinity, bool allowNegativeInfinity) { return default(bool); }
        public static bool IsValid(this Windows.UI.Xaml.Thickness thick, bool allowNegative, bool allowNaN, bool allowPositiveInfinity, bool allowNegativeInfinity) { return default(bool); }
        public static bool IsZero(this double value) { return default(bool); }
        public static bool IsZero(this float value) { return default(bool); }
        public static bool IsZero(this Windows.UI.Xaml.CornerRadius corner) { return default(bool); }
        public static bool IsZero(this Windows.UI.Xaml.Thickness thick) { return default(bool); }
        public static double RoundLayoutValue(double value, double dpiScale) { return default(double); }
    }
}
namespace CompositionProToolkit.CompositionProToolkit_XamlTypeInfo
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks", "14.0.0.0")]
    public sealed partial class XamlMetaDataProvider : Windows.UI.Xaml.Markup.IXamlMetadataProvider
    {
        public XamlMetaDataProvider() { }
        public Windows.UI.Xaml.Markup.IXamlType GetXamlType(string fullName) { return default(Windows.UI.Xaml.Markup.IXamlType); }
        public Windows.UI.Xaml.Markup.IXamlType GetXamlType(System.Type type) { return default(Windows.UI.Xaml.Markup.IXamlType); }
        public Windows.UI.Xaml.Markup.XmlnsDefinition[] GetXmlnsDefinitions() { return default(Windows.UI.Xaml.Markup.XmlnsDefinition[]); }
    }
}
namespace CompositionProToolkit.Controls
{
    public sealed partial class FluidBanner : Windows.UI.Xaml.Controls.Panel
    {
        public static readonly Windows.UI.Xaml.DependencyProperty AlignXProperty;
        public static readonly Windows.UI.Xaml.DependencyProperty AlignYProperty;
        public const int DefaultDecodeHeight = 0;
        public const int DefaultDecodeWidth = 0;
        public static readonly Windows.UI.Color DefaultItemBackground;
        public const double DefaultItemGap = 30;
        public static System.TimeSpan DefaultOpacityAnimationDuration;
        public static System.TimeSpan DefaultScaleAnimationDuration;
        public const float HoverScaleFactor = 1.1f;
        public static System.TimeSpan InsetAnimationDelayDuration;
        public static System.TimeSpan InsetAnimationDuration;
        public static System.TimeSpan InsetClipAnimationDuration;
        public static readonly Windows.UI.Xaml.DependencyProperty ItemBackgroundProperty;
        public static readonly Windows.UI.Xaml.DependencyProperty ItemGapProperty;
        public static readonly Windows.UI.Xaml.DependencyProperty ItemsSourceProperty;
        public static readonly Windows.UI.Xaml.DependencyProperty PaddingProperty;
        public const float ScaleDownFactor = 0.7f;
        public static readonly Windows.UI.Xaml.DependencyProperty StretchProperty;
        public const float TargetOpacity = 0f;
        public FluidBanner() { }
        public Windows.UI.Xaml.Media.AlignmentX AlignX { get { return default(Windows.UI.Xaml.Media.AlignmentX); } set { } }
        public Windows.UI.Xaml.Media.AlignmentY AlignY { get { return default(Windows.UI.Xaml.Media.AlignmentY); } set { } }
        public Windows.UI.Color ItemBackground { get { return default(Windows.UI.Color); } set { } }
        public double ItemGap { get { return default(double); } set { } }
        public System.Collections.Generic.IEnumerable<System.Uri> ItemsSource { get { return default(System.Collections.Generic.IEnumerable<System.Uri>); } set { } }
        public Windows.UI.Xaml.Thickness Padding { get { return default(Windows.UI.Xaml.Thickness); } set { } }
        public Windows.UI.Xaml.Media.Stretch Stretch { get { return default(Windows.UI.Xaml.Media.Stretch); } set { } }
        protected override Windows.Foundation.Size ArrangeOverride(Windows.Foundation.Size finalSize) { return default(Windows.Foundation.Size); }
        protected override Windows.Foundation.Size MeasureOverride(Windows.Foundation.Size availableSize) { return default(Windows.Foundation.Size); }
    }
    public partial class FluidPointerDragBehavior : Microsoft.Xaml.Interactivity.Behavior<Windows.UI.Xaml.UIElement>
    {
        public static readonly Windows.UI.Xaml.DependencyProperty DragButtonProperty;
        public FluidPointerDragBehavior() { }
        public CompositionProToolkit.Controls.FluidPointerDragBehavior.DragButtonType DragButton { get { return default(CompositionProToolkit.Controls.FluidPointerDragBehavior.DragButtonType); } set { } }
        protected override void OnAttached() { }
        protected override void OnDetaching() { }
        public enum DragButtonType
        {
            MouseLeftButton = 0,
            MouseMiddleButton = 1,
            MouseRightButton = 2,
            Pen = 3,
            Touch = 4,
        }
    }
    public partial class FluidProgressRing : Windows.UI.Xaml.Controls.Control
    {
        public static readonly Windows.UI.Xaml.DependencyProperty ActiveNodesProperty;
        public static readonly Windows.UI.Xaml.DependencyProperty MaxNodesProperty;
        public static readonly Windows.UI.Xaml.DependencyProperty NodeColorProperty;
        public static readonly Windows.UI.Xaml.DependencyProperty NodeDurationProperty;
        public static readonly Windows.UI.Xaml.DependencyProperty NodeSizeFactorProperty;
        public static readonly Windows.UI.Xaml.DependencyProperty RingDurationProperty;
        public FluidProgressRing() { }
        public int ActiveNodes { get { return default(int); } set { } }
        public int MaxNodes { get { return default(int); } set { } }
        public Windows.UI.Color NodeColor { get { return default(Windows.UI.Color); } set { } }
        public System.TimeSpan NodeDuration { get { return default(System.TimeSpan); } set { } }
        public double NodeSizeFactor { get { return default(double); } set { } }
        public System.TimeSpan RingDuration { get { return default(System.TimeSpan); } set { } }
        protected override Windows.Foundation.Size ArrangeOverride(Windows.Foundation.Size finalSize) { return default(Windows.Foundation.Size); }
    }
    public sealed partial class FluidWrapPanel : Windows.UI.Xaml.Controls.Panel
    {
        public static System.TimeSpan DefaultFluidAnimationDuration;
        public const double DefaultItemHeight = 10;
        public const double DefaultItemWidth = 10;
        public static System.TimeSpan DefaultOpacityAnimationDuration;
        public static System.TimeSpan DefaultScaleAnimationDuration;
        public const double DragOpacityDefault = 0.7;
        public static readonly Windows.UI.Xaml.DependencyProperty DragOpacityProperty;
        public const double DragScaleDefault = 1.2;
        public static readonly Windows.UI.Xaml.DependencyProperty DragScaleProperty;
        public static readonly Windows.UI.Xaml.DependencyProperty FluidItemsProperty;
        public static System.TimeSpan InitializationAnimationDuration;
        public static readonly Windows.UI.Xaml.DependencyProperty IsComposingProperty;
        public static readonly Windows.UI.Xaml.DependencyProperty ItemHeightProperty;
        public static readonly Windows.UI.Xaml.DependencyProperty ItemsSourceProperty;
        public static readonly Windows.UI.Xaml.DependencyProperty ItemWidthProperty;
        public const double NormalOpacity = 1;
        public const double NormalScale = 1;
        public const double OpacityMin = 0.1;
        public static readonly Windows.UI.Xaml.DependencyProperty OptimizeChildPlacementProperty;
        public static readonly Windows.UI.Xaml.DependencyProperty OrientationProperty;
        public const int ZIndexDrag = 10;
        public const int ZIndexIntermediate = 1;
        public const int ZIndexNormal = 0;
        public FluidWrapPanel() { }
        public double DragOpacity { get { return default(double); } set { } }
        public double DragScale { get { return default(double); } set { } }
        public System.Collections.ObjectModel.ObservableCollection<Windows.UI.Xaml.UIElement> FluidItems { get { return default(System.Collections.ObjectModel.ObservableCollection<Windows.UI.Xaml.UIElement>); } }
        public bool IsComposing { get { return default(bool); } set { } }
        public double ItemHeight { get { return default(double); } set { } }
        public System.Collections.IEnumerable ItemsSource { get { return default(System.Collections.IEnumerable); } set { } }
        public double ItemWidth { get { return default(double); } set { } }
        public bool OptimizeChildPlacement { get { return default(bool); } set { } }
        public Windows.UI.Xaml.Controls.Orientation Orientation { get { return default(Windows.UI.Xaml.Controls.Orientation); } set { } }
        public System.Threading.Tasks.Task AddChildAsync(Windows.UI.Xaml.FrameworkElement child) { return default(System.Threading.Tasks.Task); }
        protected override Windows.Foundation.Size ArrangeOverride(Windows.Foundation.Size finalSize) { return default(Windows.Foundation.Size); }
        protected override Windows.Foundation.Size MeasureOverride(Windows.Foundation.Size availableSize) { return default(Windows.Foundation.Size); }
    }
    public sealed partial class ImageFrame : Windows.UI.Xaml.Controls.Control
    {
        public static readonly Windows.UI.Xaml.DependencyProperty AlignXProperty;
        public static readonly Windows.UI.Xaml.DependencyProperty AlignYProperty;
        public static readonly Windows.UI.Xaml.DependencyProperty CornerRadiusProperty;
        public static readonly Windows.UI.Xaml.DependencyProperty DisplayShadowProperty;
        public static readonly Windows.UI.Xaml.DependencyProperty FrameBackgroundProperty;
        public static readonly Windows.UI.Xaml.DependencyProperty InterpolationProperty;
        public static readonly Windows.UI.Xaml.DependencyProperty PlaceholderBackgroundProperty;
        public static readonly Windows.UI.Xaml.DependencyProperty PlaceholderColorProperty;
        public static readonly Windows.UI.Xaml.DependencyProperty RenderFastProperty;
        public static readonly Windows.UI.Xaml.DependencyProperty RenderOptimizedProperty;
        public static readonly Windows.UI.Xaml.DependencyProperty ShadowBlurRadiusProperty;
        public static readonly Windows.UI.Xaml.DependencyProperty ShadowColorProperty;
        public static readonly Windows.UI.Xaml.DependencyProperty ShadowOffsetXProperty;
        public static readonly Windows.UI.Xaml.DependencyProperty ShadowOffsetYProperty;
        public static readonly Windows.UI.Xaml.DependencyProperty ShadowOpacityProperty;
        public static readonly Windows.UI.Xaml.DependencyProperty ShowPlaceholderProperty;
        public static readonly Windows.UI.Xaml.DependencyProperty SourceProperty;
        public static readonly Windows.UI.Xaml.DependencyProperty StretchProperty;
        public static readonly Windows.UI.Xaml.DependencyProperty TransitionDurationProperty;
        public static readonly Windows.UI.Xaml.DependencyProperty TransitionModeProperty;
        public ImageFrame() { }
        public Windows.UI.Xaml.Media.AlignmentX AlignX { get { return default(Windows.UI.Xaml.Media.AlignmentX); } set { } }
        public Windows.UI.Xaml.Media.AlignmentY AlignY { get { return default(Windows.UI.Xaml.Media.AlignmentY); } set { } }
        public Windows.UI.Xaml.CornerRadius CornerRadius { get { return default(Windows.UI.Xaml.CornerRadius); } set { } }
        public bool DisplayShadow { get { return default(bool); } set { } }
        public Windows.UI.Color FrameBackground { get { return default(Windows.UI.Color); } set { } }
        public Microsoft.Graphics.Canvas.CanvasImageInterpolation Interpolation { get { return default(Microsoft.Graphics.Canvas.CanvasImageInterpolation); } set { } }
        public Windows.UI.Color PlaceholderBackground { get { return default(Windows.UI.Color); } set { } }
        public Windows.UI.Color PlaceholderColor { get { return default(Windows.UI.Color); } set { } }
        public bool RenderFast { get { return default(bool); } set { } }
        public bool RenderOptimized { get { return default(bool); } set { } }
        public double ShadowBlurRadius { get { return default(double); } set { } }
        public Windows.UI.Color ShadowColor { get { return default(Windows.UI.Color); } set { } }
        public double ShadowOffsetX { get { return default(double); } set { } }
        public double ShadowOffsetY { get { return default(double); } set { } }
        public double ShadowOpacity { get { return default(double); } set { } }
        public bool ShowPlaceholder { get { return default(bool); } set { } }
        public object Source { get { return default(object); } set { } }
        public Windows.UI.Xaml.Media.Stretch Stretch { get { return default(Windows.UI.Xaml.Media.Stretch); } set { } }
        public System.TimeSpan TransitionDuration { get { return default(System.TimeSpan); } set { } }
        public CompositionProToolkit.Controls.TransitionModeType TransitionMode { get { return default(CompositionProToolkit.Controls.TransitionModeType); } set { } }
        public event Windows.UI.Xaml.RoutedEventHandler ImageFailed { add { } remove { } }
        public event Windows.UI.Xaml.RoutedEventHandler ImageOpened { add { } remove { } }
        protected override Windows.Foundation.Size ArrangeOverride(Windows.Foundation.Size finalSize) { return default(Windows.Foundation.Size); }
        protected override Windows.Foundation.Size MeasureOverride(Windows.Foundation.Size availableSize) { return default(Windows.Foundation.Size); }
    }
    public partial class ImageFrameEventArgs : Windows.UI.Xaml.RoutedEventArgs
    {
        public ImageFrameEventArgs(object source, string message) { }
        public string Message { [System.Runtime.CompilerServices.CompilerGeneratedAttribute]get { return default(string); } }
        public object Source { [System.Runtime.CompilerServices.CompilerGeneratedAttribute]get { return default(object); } }
    }
    public enum TransitionModeType
    {
        FadeIn = 0,
        SlideDown = 4,
        SlideLeft = 1,
        SlideRight = 2,
        SlideUp = 3,
        ZoomIn = 5,
    }
}
namespace CompositionProToolkit.Expressions
{
    public static partial class CompositionAnimationExtensions
    {
        public static Windows.UI.Composition.KeyFrameAnimation InsertExpressionKeyFrame<T>(this Windows.UI.Composition.KeyFrameAnimation animation, float normalizedProgressKey, System.Linq.Expressions.Expression<CompositionProToolkit.Expressions.CompositionLambda<T>> expression, Windows.UI.Composition.CompositionEasingFunction easingFunction=null) { return default(Windows.UI.Composition.KeyFrameAnimation); }
        public static System.Collections.Generic.Dictionary<string, object> SetExpression<T>(this Windows.UI.Composition.ExpressionAnimation animation, System.Linq.Expressions.Expression<CompositionProToolkit.Expressions.CompositionLambda<T>> expression) { return default(System.Collections.Generic.Dictionary<string, object>); }
        public static bool SetParameter<T>(this T animation, string key, object input) where T : Windows.UI.Composition.CompositionAnimation { return default(bool); }
        public static T SetParameters<T>(this T animation, System.Collections.Generic.Dictionary<string, object> parameters) where T : Windows.UI.Composition.CompositionAnimation { return default(T); }
    }
    public partial class CompositionExpressionContext<T>
    {
        internal CompositionExpressionContext() { }
        public T FinalValue { [System.Runtime.CompilerServices.CompilerGeneratedAttribute]get { return default(T); } }
        public T StartingValue { [System.Runtime.CompilerServices.CompilerGeneratedAttribute]get { return default(T); } }
        public System.Numerics.Vector2 Abs(System.Numerics.Vector2 value) { return default(System.Numerics.Vector2); }
        public System.Numerics.Vector3 Abs(System.Numerics.Vector3 value) { return default(System.Numerics.Vector3); }
        public System.Numerics.Vector4 Abs(System.Numerics.Vector4 value) { return default(System.Numerics.Vector4); }
        public float Abs(float value) { return default(float); }
        public float Acos(float value) { return default(float); }
        public float Asin(float value) { return default(float); }
        public float Atan(float value) { return default(float); }
        public float Ceiling(float value) { return default(float); }
        public float Clamp(float value, float min, float max) { return default(float); }
        public Windows.UI.Color ColorLerp(Windows.UI.Color ColorTo, Windows.UI.Color ColorFrom, float Progression) { return default(Windows.UI.Color); }
        public Windows.UI.Color ColorLerpHSL(Windows.UI.Color ColorTo, Windows.UI.Color ColorFrom, float Progression) { return default(Windows.UI.Color); }
        public Windows.UI.Color ColorLerpRGB(Windows.UI.Color ColorTo, Windows.UI.Color ColorFrom, float Progression) { return default(Windows.UI.Color); }
        public System.Numerics.Quaternion Concatenate(System.Numerics.Quaternion value, System.Numerics.Quaternion value2) { return default(System.Numerics.Quaternion); }
        public float Cos(float value) { return default(float); }
        public System.Numerics.Vector2 Distance(System.Numerics.Vector2 value1, System.Numerics.Vector2 value2) { return default(System.Numerics.Vector2); }
        public System.Numerics.Vector3 Distance(System.Numerics.Vector3 value1, System.Numerics.Vector3 value2) { return default(System.Numerics.Vector3); }
        public System.Numerics.Vector4 Distance(System.Numerics.Vector4 value1, System.Numerics.Vector4 value2) { return default(System.Numerics.Vector4); }
        public float DistanceSquared(System.Numerics.Vector2 value1, System.Numerics.Vector2 value2) { return default(float); }
        public float DistanceSquared(System.Numerics.Vector3 value1, System.Numerics.Vector3 value2) { return default(float); }
        public float DistanceSquared(System.Numerics.Vector4 value1, System.Numerics.Vector4 value2) { return default(float); }
        public float Floor(float value) { return default(float); }
        public System.Numerics.Vector2 Inverse(System.Numerics.Vector2 value) { return default(System.Numerics.Vector2); }
        public System.Numerics.Vector3 Inverse(System.Numerics.Vector3 value) { return default(System.Numerics.Vector3); }
        public System.Numerics.Vector4 Inverse(System.Numerics.Vector4 value) { return default(System.Numerics.Vector4); }
        public System.Numerics.Vector2 Length(System.Numerics.Vector2 value) { return default(System.Numerics.Vector2); }
        public System.Numerics.Vector3 Length(System.Numerics.Vector3 value) { return default(System.Numerics.Vector3); }
        public System.Numerics.Vector4 Length(System.Numerics.Vector4 value) { return default(System.Numerics.Vector4); }
        public System.Numerics.Vector2 LengthSquared(System.Numerics.Vector2 value) { return default(System.Numerics.Vector2); }
        public System.Numerics.Vector3 LengthSquared(System.Numerics.Vector3 value) { return default(System.Numerics.Vector3); }
        public System.Numerics.Vector4 LengthSquared(System.Numerics.Vector4 value) { return default(System.Numerics.Vector4); }
        public System.Numerics.Matrix3x2 Lerp(System.Numerics.Matrix3x2 value1, System.Numerics.Matrix3x2 value2, float progress) { return default(System.Numerics.Matrix3x2); }
        public System.Numerics.Matrix4x4 Lerp(System.Numerics.Matrix4x4 value1, System.Numerics.Matrix4x4 value2, float progress) { return default(System.Numerics.Matrix4x4); }
        public System.Numerics.Vector2 Lerp(System.Numerics.Vector2 value1, System.Numerics.Vector2 value2, float progress) { return default(System.Numerics.Vector2); }
        public System.Numerics.Vector3 Lerp(System.Numerics.Vector3 value1, System.Numerics.Vector3 value2, float progress) { return default(System.Numerics.Vector3); }
        public System.Numerics.Vector4 Lerp(System.Numerics.Vector4 value1, System.Numerics.Vector4 value2, float progress) { return default(System.Numerics.Vector4); }
        public float Ln(float value) { return default(float); }
        public float Log10(float value) { return default(float); }
        public System.Numerics.Matrix3x2 Matrix3x2(float M11, float M12, float M21, float M22, float M31, float M32) { return default(System.Numerics.Matrix3x2); }
        public System.Numerics.Matrix3x2 Matrix3x2CreateFromScale(System.Numerics.Vector2 scale) { return default(System.Numerics.Matrix3x2); }
        public System.Numerics.Matrix3x2 Matrix3x2CreateFromTranslation(System.Numerics.Vector2 translation) { return default(System.Numerics.Matrix3x2); }
        public System.Numerics.Matrix4x4 Matrix4x4(float M11, float M12, float M13, float M14, float M21, float M22, float M23, float M24, float M31, float M32, float M33, float M34, float M41, float M42, float M43, float M44) { return default(System.Numerics.Matrix4x4); }
        public System.Numerics.Matrix4x4 Matrix4x4CreateFromAxisAngle(System.Numerics.Vector3 axis, float angle) { return default(System.Numerics.Matrix4x4); }
        public System.Numerics.Matrix4x4 Matrix4x4CreateFromScale(System.Numerics.Vector3 scale) { return default(System.Numerics.Matrix4x4); }
        public System.Numerics.Matrix4x4 Matrix4x4CreateFromTranslation(System.Numerics.Vector3 translation) { return default(System.Numerics.Matrix4x4); }
        public float Max(float value1, float value2) { return default(float); }
        public float Min(float value1, float value2) { return default(float); }
        public float Mod(float dividend, float divisor) { return default(float); }
        public float Normalize() { return default(float); }
        public System.Numerics.Vector2 Normalize(System.Numerics.Vector2 value) { return default(System.Numerics.Vector2); }
        public System.Numerics.Vector3 Normalize(System.Numerics.Vector3 value) { return default(System.Numerics.Vector3); }
        public System.Numerics.Vector4 Normalize(System.Numerics.Vector4 value) { return default(System.Numerics.Vector4); }
        public float Pow(float value, int power) { return default(float); }
        public System.Numerics.Quaternion QuaternionCreateFromAxisAngle(System.Numerics.Vector3 axis, float angle) { return default(System.Numerics.Quaternion); }
        public float Round(float value) { return default(float); }
        public System.Numerics.Matrix3x2 Scale(System.Numerics.Matrix3x2 value, float factor) { return default(System.Numerics.Matrix3x2); }
        public System.Numerics.Matrix4x4 Scale(System.Numerics.Matrix4x4 value, float factor) { return default(System.Numerics.Matrix4x4); }
        public System.Numerics.Vector2 Scale(System.Numerics.Vector2 value, float factor) { return default(System.Numerics.Vector2); }
        public System.Numerics.Vector3 Scale(System.Numerics.Vector3 value, float factor) { return default(System.Numerics.Vector3); }
        public System.Numerics.Vector4 Scale(System.Numerics.Vector4 value, float factor) { return default(System.Numerics.Vector4); }
        public float Sin(float value) { return default(float); }
        public System.Numerics.Quaternion Slerp(System.Numerics.Quaternion value1, System.Numerics.Quaternion value2, float progress) { return default(System.Numerics.Quaternion); }
        public float Sqrt(float value) { return default(float); }
        public float Square(float value) { return default(float); }
        public float Tan(float value) { return default(float); }
        public float ToDegrees(float radians) { return default(float); }
        public float ToRadians(float degrees) { return default(float); }
        public System.Numerics.Vector2 Transform(System.Numerics.Vector2 value, System.Numerics.Matrix3x2 matrix) { return default(System.Numerics.Vector2); }
        public System.Numerics.Vector4 Transform(System.Numerics.Vector4 value, System.Numerics.Matrix4x4 matrix) { return default(System.Numerics.Vector4); }
        public System.Numerics.Vector2 Vector2(float x, float y) { return default(System.Numerics.Vector2); }
        public System.Numerics.Vector3 Vector3(float x, float y, float z) { return default(System.Numerics.Vector3); }
        public System.Numerics.Vector4 Vector4(float x, float y, float z, float w) { return default(System.Numerics.Vector4); }
    }
    public abstract partial class CompositionExpressionEngine
    {
        protected CompositionExpressionEngine() { }
        public static object GetObject(System.Linq.Expressions.Expression expression) { return default(object); }
    }
    public partial class CompositionExpressionResult
    {
        public CompositionExpressionResult() { }
        public string Expression { [System.Runtime.CompilerServices.CompilerGeneratedAttribute]get { return default(string); } [System.Runtime.CompilerServices.CompilerGeneratedAttribute]set { } }
        public System.Collections.Generic.Dictionary<string, object> Parameters { [System.Runtime.CompilerServices.CompilerGeneratedAttribute]get { return default(System.Collections.Generic.Dictionary<string, object>); } [System.Runtime.CompilerServices.CompilerGeneratedAttribute]set { } }
    }
    public delegate T CompositionLambda<T>(CompositionProToolkit.Expressions.CompositionExpressionContext<T> ctx);
    public static partial class CompositionObjectExtensions
    {
        public static string ScaleXY(this Windows.UI.Composition.CompositionObject compositionObject) { return default(string); }
        public static void StartAnimation(this Windows.UI.Composition.CompositionObject compositionObject, System.Linq.Expressions.Expression<System.Func<object>> expression, Windows.UI.Composition.CompositionAnimation animation) { }
        public static void StopAnimation(this Windows.UI.Composition.CompositionObject compositionObject, System.Linq.Expressions.Expression<System.Func<object>> expression) { }
    }
    public static partial class CompositionPropertySetExtensions
    {
        public static T Get<T>(this Windows.UI.Composition.CompositionPropertySet propertySet, string key) { return default(T); }
        public static void Insert<T>(this Windows.UI.Composition.CompositionPropertySet propertySet, string key, object input) { }
        public static Windows.UI.Composition.CompositionPropertySet ToPropertySet(object input, Windows.UI.Composition.Compositor compositor) { return default(Windows.UI.Composition.CompositionPropertySet); }
    }
    public static partial class CompositorExtensions
    {
        public static Windows.UI.Composition.CompositionEffectFactory CreateEffectFactory(this Windows.UI.Composition.Compositor compositor, Windows.Graphics.Effects.IGraphicsEffect graphicsEffect, params System.Linq.Expressions.Expression<System.Func<object>>[] animatablePropertyExpressions) { return default(Windows.UI.Composition.CompositionEffectFactory); }
        public static Windows.UI.Composition.ExpressionAnimation CreateExpressionAnimation<T>(this Windows.UI.Composition.Compositor compositor, System.Linq.Expressions.Expression<CompositionProToolkit.Expressions.CompositionLambda<T>> expression) { return default(Windows.UI.Composition.ExpressionAnimation); }
        public static System.Linq.Expressions.Expression<CompositionProToolkit.Expressions.CompositionLambda<T>> CreateFinalValueExpression<T>(this Windows.UI.Composition.Compositor compositor) { return default(System.Linq.Expressions.Expression<CompositionProToolkit.Expressions.CompositionLambda<T>>); }
        public static CompositionProToolkit.Expressions.KeyFrameAnimation<T> CreateKeyFrameAnimation<T>(this Windows.UI.Composition.Compositor compositor) { return default(CompositionProToolkit.Expressions.KeyFrameAnimation<T>); }
        public static void CreateScopedBatch(this Windows.UI.Composition.Compositor compositor, Windows.UI.Composition.CompositionBatchTypes batchType, System.Action action, System.Action postAction=null) { }
        public static void CreateScopedBatch(this Windows.UI.Composition.Compositor compositor, Windows.UI.Composition.CompositionBatchTypes batchType, System.Action<Windows.UI.Composition.CompositionScopedBatch> action, System.Action<Windows.UI.Composition.CompositionScopedBatch> postAction=null) { }
        public static System.Linq.Expressions.Expression<CompositionProToolkit.Expressions.CompositionLambda<T>> CreateStartingValueExpression<T>(this Windows.UI.Composition.Compositor compositor) { return default(System.Linq.Expressions.Expression<CompositionProToolkit.Expressions.CompositionLambda<T>>); }
    }
    public static partial class DoubleExtensions
    {
        public static float Single(this double value) { return default(float); }
    }
    public sealed partial class KeyFrame<T>
    {
        public KeyFrame(float normalizedProgressKey, T value, Windows.UI.Composition.CompositionEasingFunction easing=null) { }
        public Windows.UI.Composition.CompositionEasingFunction Easing { [System.Runtime.CompilerServices.CompilerGeneratedAttribute]get { return default(Windows.UI.Composition.CompositionEasingFunction); } [System.Runtime.CompilerServices.CompilerGeneratedAttribute]set { } }
        public float Key { [System.Runtime.CompilerServices.CompilerGeneratedAttribute]get { return default(float); } [System.Runtime.CompilerServices.CompilerGeneratedAttribute]set { } }
        public T Value { [System.Runtime.CompilerServices.CompilerGeneratedAttribute]get { return default(T); } [System.Runtime.CompilerServices.CompilerGeneratedAttribute]set { } }
    }
    public sealed partial class KeyFrameAnimation<T>
    {
        public KeyFrameAnimation(Windows.UI.Composition.KeyFrameAnimation animation) { }
        public Windows.UI.Composition.KeyFrameAnimation Animation { [System.Runtime.CompilerServices.CompilerGeneratedAttribute]get { return default(Windows.UI.Composition.KeyFrameAnimation); } }
        public System.TimeSpan DelayTime { get { return default(System.TimeSpan); } set { } }
        public Windows.UI.Composition.AnimationDirection Direction { get { return default(Windows.UI.Composition.AnimationDirection); } set { } }
        public System.TimeSpan Duration { get { return default(System.TimeSpan); } set { } }
        public Windows.UI.Composition.AnimationIterationBehavior IterationBehavior { get { return default(Windows.UI.Composition.AnimationIterationBehavior); } set { } }
        public int IterationCount { get { return default(int); } set { } }
        public int KeyFrameCount { get { return default(int); } }
        public Windows.UI.Composition.AnimationStopBehavior StopBehavior { get { return default(Windows.UI.Composition.AnimationStopBehavior); } set { } }
        public string Target { get { return default(string); } set { } }
        public CompositionProToolkit.Expressions.KeyFrameAnimation<T> DelayBy(System.TimeSpan delayTime) { return default(CompositionProToolkit.Expressions.KeyFrameAnimation<T>); }
        public CompositionProToolkit.Expressions.KeyFrameAnimation<T> ForTarget(System.Linq.Expressions.Expression<System.Func<object>> targetExpression) { return default(CompositionProToolkit.Expressions.KeyFrameAnimation<T>); }
        public CompositionProToolkit.Expressions.KeyFrameAnimation<T> HavingDuration(System.TimeSpan duration) { return default(CompositionProToolkit.Expressions.KeyFrameAnimation<T>); }
        public void InsertExpressionKeyFrame(float normalizedProgressKey, System.Linq.Expressions.Expression<CompositionProToolkit.Expressions.CompositionLambda<T>> expression, Windows.UI.Composition.CompositionEasingFunction easingFunction=null) { }
        public void InsertKeyFrame(CompositionProToolkit.Expressions.KeyFrame<T> keyFrame) { }
        public void InsertKeyFrame(float normalizedProgressKey, T value, Windows.UI.Composition.CompositionEasingFunction easingFunction=null) { }
        public void InsertKeyFrames(params CompositionProToolkit.Expressions.KeyFrame<T>[] keyFrames) { }
        public CompositionProToolkit.Expressions.KeyFrameAnimation<T> InTheDirection(Windows.UI.Composition.AnimationDirection direction) { return default(CompositionProToolkit.Expressions.KeyFrameAnimation<T>); }
        public CompositionProToolkit.Expressions.KeyFrameAnimation<T> OnStop(Windows.UI.Composition.AnimationStopBehavior stopBehavior) { return default(CompositionProToolkit.Expressions.KeyFrameAnimation<T>); }
        public CompositionProToolkit.Expressions.KeyFrameAnimation<T> Repeats(int count) { return default(CompositionProToolkit.Expressions.KeyFrameAnimation<T>); }
        public CompositionProToolkit.Expressions.KeyFrameAnimation<T> RepeatsForever() { return default(CompositionProToolkit.Expressions.KeyFrameAnimation<T>); }
    }
    public static partial class TypeExtensions
    {
        public static System.Type BaseType(this System.Type type) { return default(System.Type); }
        public static System.Type[] GetGenericArguments(this System.Type type) { return default(System.Type[]); }
        public static System.Reflection.MethodInfo GetMethod(this System.Type type, string methodName) { return default(System.Reflection.MethodInfo); }
        public static System.Reflection.PropertyInfo GetProperty(this System.Type type, string propertyName) { return default(System.Reflection.PropertyInfo); }
        public static object GetPropertyValue(this object instance, string propertyValue) { return default(object); }
        public static System.Reflection.TypeInfo GetTypeInfo(this System.Type type) { return default(System.Reflection.TypeInfo); }
        public static bool IsClass(this System.Type type) { return default(bool); }
        public static bool IsEnum(this System.Type type) { return default(bool); }
        public static bool IsGenericType(this System.Type type) { return default(bool); }
        public static bool IsPrimitive(this System.Type type) { return default(bool); }
        public static bool IsPublic(this System.Type type) { return default(bool); }
        public static bool IsSubclassOf(this System.Type type, System.Type parentType) { return default(bool); }
    }
}
