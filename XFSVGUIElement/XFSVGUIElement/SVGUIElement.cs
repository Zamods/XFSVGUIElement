using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace XFSVGUIElement
{
    public class SVGUIElement : ContentView
    {
        #region Private Members

        private readonly SKCanvasView _canvasView = new SKCanvasView();
        private readonly double _screenWidth = DeviceDisplay.MainDisplayInfo.Width;

        #endregion

        #region Bindable Properties

        #region ResourceId

        public static readonly BindableProperty ResourceIdProperty = BindableProperty.Create(
            nameof(ResourceId), typeof(string), typeof(SVGUIElement), default(string), propertyChanged: RedrawCanvas);

        public string ResourceId
        {
            get => (string)GetValue(ResourceIdProperty);
            set => SetValue(ResourceIdProperty, value);
        }

        #endregion

        #region Color 

        public static readonly BindableProperty ColorProperty = BindableProperty.Create(
           nameof(Color), typeof(Color), typeof(SVGUIElement), Color.Black, propertyChanged: RedrawCanvas);

        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        #endregion

        #region Stroke Color 

        public static readonly BindableProperty StrokeColorProperty = BindableProperty.Create(
           nameof(StrokeColor), typeof(Color), typeof(SVGUIElement), Color.Red, propertyChanged: RedrawCanvas);

        public Color StrokeColor
        {
            get => (Color)GetValue(StrokeColorProperty);
            set => SetValue(StrokeColorProperty, value);
        }

        #endregion

        #region SVG Paint Style 

        public static readonly BindableProperty PaintStyleProperty = BindableProperty.Create(
           nameof(SVGPaintStyle), typeof(PaintStyle), typeof(SVGUIElement), PaintStyle.Fill, propertyChanged: RedrawCanvas);

        public PaintStyle SVGPaintStyle
        {
            get => (PaintStyle)GetValue(PaintStyleProperty);
            set => SetValue(PaintStyleProperty, value);
        }

        #endregion

        #region Stroke Width 

        public static readonly BindableProperty StrokeWidthProperty = BindableProperty.Create(
           nameof(StrokeWidth), typeof(float), typeof(SVGUIElement), 0.00f, propertyChanged: RedrawCanvas);

        public float StrokeWidth
        {
            get => (float)GetValue(StrokeWidthProperty);
            set
            {
                if(value > 0.00f)
                {
                    SetValue(StrokeWidthProperty, value);
                }
                else
                {
                    SetValue(StrokeWidthProperty, .500f);
                }
            }
        }

        #endregion

        #region PointScale

        public static readonly BindableProperty PointScaleProperty = BindableProperty.Create(
           nameof(PointScale), typeof(float), typeof(SVGUIElement), 0.00f, propertyChanged: RedrawCanvas);

        public float PointScale
        {
            get => (float)GetValue(PointScaleProperty);
            set => SetValue(PointScaleProperty, value);
        }

        #endregion

        #region Custom Width

        public static readonly BindableProperty CustomWidthProperty = BindableProperty.Create(
           nameof(CustomWidth), typeof(float), typeof(SVGUIElement), 1.00f, propertyChanged: RedrawCanvas);

        public float CustomWidth
        {
            get => (float)GetValue(CustomWidthProperty);
            set => SetValue(CustomWidthProperty, value);
        }

        #endregion

        #region Custom Height

        public static readonly BindableProperty CustomHeightProperty = BindableProperty.Create(
           nameof(CustomHeight), typeof(float), typeof(SVGUIElement), 1.00f, propertyChanged: RedrawCanvas);

        public float CustomHeight
        {
            get => (float)GetValue(CustomHeightProperty);
            set => SetValue(CustomHeightProperty, value);
        }

        #endregion

        #region SVG Scale Options

        public static readonly BindableProperty ScaleOptionsProperty = BindableProperty.Create(
           nameof(SVGScaleOptions), typeof(ScaleOptions), typeof(SVGUIElement), ScaleOptions.RatioScale, propertyChanged: RedrawCanvas);

        public ScaleOptions SVGScaleOptions
        {
            get => (ScaleOptions)GetValue(ScaleOptionsProperty);
            set => SetValue(ScaleOptionsProperty, value);
        }

        #endregion


        #endregion

        #region Private Methods

        private static void RedrawCanvas(BindableObject bindable, object oldvalue, object newvalue)
        {
            SVGUIElement svgUIElement = bindable as SVGUIElement;
            svgUIElement?._canvasView.InvalidateSurface();
        }

        private void CanvasViewOnPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKCanvas canvas = args.Surface.Canvas;
            canvas.Clear();

            if (string.IsNullOrEmpty(ResourceId))
                return;

            string path = null;

            using (Stream stream = GetType().Assembly.GetManifestResourceStream(ResourceId))
            {
                using (var reader = new StreamReader(stream))
                {
                    path = reader.ReadLine();
                }
            
                SKPath svg = SKPath.ParseSvgPathData(path);

                SKImageInfo info = args.Info;
                canvas.Translate(info.Width / 2f, info.Height / 2f);

                SKRect bounds;
                svg.GetBounds(out bounds);

                ScaleSVGToOption(this.SVGScaleOptions, bounds, info, canvas);
                canvas.Translate(-bounds.MidX, -bounds.MidY);
                PaintSVG(canvas, svg, this.SVGPaintStyle);
            }
        }

        #endregion

        #region Constructor

        public SVGUIElement()
        {
            Padding = new Thickness(0);
            Content = _canvasView;
            _canvasView.PaintSurface += CanvasViewOnPaintSurface;
        }

        #endregion

        #region Private Helping Methods

        #region Switch for Painting SVG

        private void PaintSVG(SKCanvas svgCanvas, SKPath svg, PaintStyle svgPaintStyle)
        {
            SKPaint fillColor = new SKPaint
            {
                ColorFilter = SKColorFilter.CreateBlendMode(
                            this.Color.ToSKColor(),
                            SKBlendMode.SrcIn),
                IsAntialias = true
            };

            switch (svgPaintStyle)
            {
               case PaintStyle.Fill:
                    svgCanvas.DrawPath(svg, fillColor);
                    break;

                case PaintStyle.Stroke:
                    {
                        SKPaint strokeColor = new SKPaint
                        {
                            IsStroke = true,
                            StrokeWidth = this.StrokeWidth,
                            Style = SKPaintStyle.Stroke,
                            ColorFilter = SKColorFilter.CreateBlendMode(
                            this.StrokeColor.ToSKColor(),
                            SKBlendMode.SrcIn),
                            IsAntialias = true
                        };
                        svgCanvas.DrawPath(svg, strokeColor);
                    }
                    break;

                case PaintStyle.FillandStroke:
                    {
                        SKPaint strokeColor = new SKPaint
                        {
                            IsStroke = true,
                            StrokeWidth = this.StrokeWidth,
                            Style = SKPaintStyle.Stroke,
                            ColorFilter = SKColorFilter.CreateBlendMode(
                            this.StrokeColor.ToSKColor(),
                            SKBlendMode.SrcIn),
                            IsAntialias = true
                        };
                        svgCanvas.DrawPath(svg, strokeColor);
                        svgCanvas.DrawPath(svg, fillColor);
                    }
                    break;

                default: svgCanvas.DrawPath(svg,fillColor);
                    break;
            }
        }

        #endregion

        #region Switch for Scale Options

        private void ScaleSVGToOption(ScaleOptions scaleOption, SKRect svgBounds, SKImageInfo imageInfo, SKCanvas svgCanvas)
        {
            float xRatio = imageInfo.Width / (svgBounds.Width + this.StrokeWidth);
            float yRatio = imageInfo.Height / (svgBounds.Height + this.StrokeWidth);

            switch (scaleOption)
            {
                case ScaleOptions.RatioScale:
                    {
                        float ratio = Math.Min(xRatio, yRatio);
                        svgCanvas.Scale(ratio + this.PointScale);
                    }
                    break;

                case ScaleOptions.AspectRatioScale:
                    {
                        float xAspectRatio = ((xRatio / (xRatio + yRatio)) * 10 ) + 10;
                        float yAspectRatio = ((yRatio / (xRatio + yRatio)) * 10 ) + 10;
                        svgCanvas.Scale(xAspectRatio * this.CustomWidth, yAspectRatio * this.CustomHeight);
                    }
                    break;

                default:
                    {
                        float ratio = Math.Min(xRatio, yRatio);
                        svgCanvas.Scale(ratio + this.PointScale);
                    }
                    break;
            }
        }

        #endregion

        #endregion
    }

    #region enum options

    public enum PaintStyle
    {
        Fill,
        Stroke,
        FillandStroke
    }

    public enum ScaleOptions
    {
        RatioScale,
        AspectRatioScale
    }

    #endregion
}
