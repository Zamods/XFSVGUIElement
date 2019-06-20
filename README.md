
# Xamarin Forms SVG UI Element

This project uses SkiaSharp libraries for Xamarin Forms to create SVG images on content pages. I have created reusable control so anybody can use it without any further changes. Please check Wiki for detailed documentation for this control.
***
<img src="https://u7ts5g.by.files.1drv.com/y4mcW-5IjMSv4KyYeM0sA89O8HMK-6cOCXq8rPE03GZ_M3ypZmuHvxuUThH7At0SdD9EH5pOCaWkAbEmzNI6a3Zxcvlb0q1zD_sX1KxDeCLXqVUu9AejbqQDlT69RP_3Z2CjnkLl3_G2C5tAUxaErixT_2boziJPrQFG73dS1FVsM2_TAx5QKEvMcGhPcTDTkS9YU0Vjw86Tb5TltsyjUWZng?width=433&height=660&cropmode=none" width="433" height="660" />

# How to use **"SVGUIElement"**

It's very easy to **"SVGUIElement"** in any Xamarin Forms project. You just need to copy **"SVGUIElement.cs"** into your project and install following required ***nuget packages***.
**SkiaSharp**
**SkiaSharp.Views.Forms**

## Want more information?
http://www.pshul.com/2018/01/25/xamarin-forms-using-svg-images-with-skiasharp/

https://www.pshul.com/2018/02/01/xamarin-forms-interactive-svg-image-using-skiasharp-with-pan-and-zoom/

https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/graphics/skiasharp/curves/path-data
## Recommendation
I would advise you to use Grid layout to control Svg size according to your need because other layouts seem to have issues with canvas that if Svg is scaled to certain point it will clip. Don't forget to explicitly set row height or you will face same clipping issues.
## Future
I might work on to fix clipping issue and add more features like gradient color.
>**Note:** I haven't tested this on **iOS** or **UWP**

