
# Xamarin Forms SVG UI Element

This project uses SkiaSharp libraries for Xamarin Forms to create SVG images on content pages. I have created reuseable control so anybody can use it without any further changes.


# Files

Here i will discuss about files in project and there relation with **SVG** for better understanding.

## UISvgs

**UISvgs** is a folder in the main project **XFSVGUIElement** that stores text files **.txt**. Svgs are stored as paths in text file like **M 6.7132,103.12901 13.642468,91.075013 20.6169,103.10293 Z** which will draw a triangle. You can use any vector graphic software like **"InkScape"** to acquire svg path which can be found by opening **.svg** with notepad or software alike.

Like this Svg was opened in notepad++<br/>
<img src="https://6lbhkq.by.files.1drv.com/y4mcVlnsH72f1OjhU5N2Em8PuPwK3YgEz_MCOWsh0JLzKU2jGyptLSUdXAD2564WCLXOKkGUTEoBIUu41jsyeqgTVAsYGfWNZJkbHM55pPFMOgCqe4ruSzGVe671mnHB_b_lcpnkhDrXSpNTx__Hlc-zf2Z3t4pBWP5YnBsazCZERRDKPUY_j64tz1I6CT0kSKwMWRkQDC2qHecE6GKqLbaKA?width=535&height=134&cropmode=none" width="535" height="134" />

## SVGUIElement.cs

This is code file which draws svg on canvas with certain parameters. **SVGUIElement** has inheritance of **ContentView** which means that all properties, methods and events from **ContentView** is functional on **SVGUIElement**. 

# How to use **"SVGUIElement"**

It's very easy to **SVGUIElement** in any Xamarin Forms project. You just need to copy **SVGUIElement.cs** into your project and install following required ***nuget packages***.
**SkiaSharp**
**SkiaSharp.Views.Forms**
**Xamarin.Essentials**

## ResourceId

**ResourceId** is very important property for **SVGUIElement** to work because this property tells the location of **Svg** path. If **ResourceId** is left empty then it will either result in compilation error or **SVGUIElement** will not be visible on **ContentPage**. 

### ResourceId Format

    <local:SVGUIElement ResourceId="XFSVGUIElement.UISvgs.Triangle.txt" Color="Blue" SVGPaintStyle="Fill" />
Here, you can see that **ResourceId**  contains the name of project ***"XFSVGUIElement"*** then folder in which svg paths are stored ***"UISvgs"*** and then name of the file itself ***"Triangle.txt"*** . 
So, Here's the format `ResourceId="MyProject.MySvgs.CircleSvg.txt"`.

> **Note:** Always set **Build Action** for Svg file as **Embedded Resource** .

## SVGPaintStyle

**SVGPaintStyle** handles the painting style for svg. It allows user to explicitly decide how they want to color thier svg. There are three options offered by **SVGPaintStyle** as follows:
 - Fill - Paints svg with given color. **"Default Option"**
 - Stroke - Paints svg only with stroke color. 
 -   FillAndStroke - Paints svg with both color and stroke color.
  > **Note:** For **"Stroke"** option **"StrokeWidth"** property is necessary. Otherwise, Svg will be invisible.

## PointScale
**PointScale** property allows user to rescale thier Svg according to given value. **PointScale** uses **SVGScaleOptions** of **RatioScale** ***<-- Default Option*** and changes size of Svg according to ratio and also takes in account for **StrokeWidth**. Let's look at code example:

     float ratio = Math.Min(xRatio, yRatio);
     svgCanvas.Scale(ratio + this.PointScale - this.StrokeWidth);
**xRatio** and **YRatio** are calculated as of such:

     float xRatio = imageInfo.Width / svgBounds.Width;
     float yRatio = imageInfo.Height / svgBounds.Height;

As you can see that we get **xRatio** by dividing ***imageInfo.Width**** with ***svgBounds.Width*** and **yRatio** by dividing ***imageInfo.Height***** with ***svgBounds.Height***. We then use **Math.Min(value,value)** to find lowest value among **xRatio** and **yRatio**. Once, we have that lowest value we consider it as **Ratio**. Now you notice that when we scale our **svgCanvas**, we account for **PointScale** value which is added according to arthmatic rules so negative value will reduce size and positive value will increase size. Now, It deducts **StrokeWidth** so that svg won't go out of bounds. Although, default value for **StrokeWidth** is **0.0** .
Code example for using **PointScale** in XAML:

    <local:SVGUIElement ResourceId="XFSVGUIElement.UISvgs.Car.txt"
    Color="Blue" SVGPaintStyle="FillandStroke" StrokeWidth=".2" PointScale="-15"  />

>**Note:** *Width of paint surface according to device screen dimensions.
	>**Height of paint surface according to device screen dimensions.
## SVGScaleOptions
**SVGScaleOptions** controls the diemensions of a drawn svg on canvas. It ensures that svg is scaled properly such that it looks exactly as user want. However, There are two option that user can choose from as follows:
- RatioScale - Check **PointScale** for details.
- AspectRatioScale

### AspectRatioScale
Unlike, **RatioScale** , **AspectRatioScale** allows user to modify Svg height and width independently. It calculates aspect ratio of width and height so that Svg shape stays according to aspect ratio of Svg. Now, Let's see how it works in code example below:

    float xAspectRatio = ((xRatio / (xRatio + yRatio)) * 10 ) + 10;
    float yAspectRatio = ((yRatio / (xRatio + yRatio)) * 10 ) + 10;
    svgCanvas.Scale(xAspectRatio * this.CustomWidth, yAspectRatio * this.CustomHeight);

**xRatio** and **YRatio** are same as used in **RatioScale** but instead of using lowest value, we sum **xRatio** and **YRatio** then calulate there respective ratios by dividing each of them by the sum. After we get respective ratio it is multiplied by **10** to convert it from **decimals** to **real number**. Then we add **10** more so we get scaled Svg according to aspect ratio and allows as to use smaller values to control Svg size. 
#### CustomWidth & CustomHeight
**CustomWidth & CustomHeight** is set to **1** by default which means it is same as **PointScale**. However, use **CustomWidth & CustomHeight** to fine tune or change ratio of Svg according to your needs.
Let's see Xaml code example for this application:

     <local:SVGUIElement ResourceId="XFSVGUIElement.UISvgs.Car.txt" 
     Color="Blue" SVGPaintStyle="Fill" CustomHeight="1.1" CustomWidth="1.05"/>

 
>**Note:** **CustomWidth & CustomHeight** are very sensitive properties, so use decimal values to change the size.

## Other Properties
### Color
Use **Color** property to paint Svg path and if you are using **SVGPaintStyle** option **FillAndStroke** then ensure you don't leave **Color** property empty else it will use ***default*** color **Black**.
>**Note:** By default **SVGPaintStyle** is set to **Fill** so you don't needed to set it again if you just want to color Svg path without stroke.
### StrokeColor
Use **StrokeColor** property to paint stroke on Svg path. You also need to set **SVGPaintStyle** option to **FillAndStroke** or **Stroke**. Otherwise, Svg path won't be painted with **StrokeColor**. 
>**Note:** By default **StrokeColor** is set to **Red**. 
### StrokeWidth
Use **StrokeWidth** property to size stroke on Svg path. You need to use positive values to make it work. For example if you use value less then **0.00f** then it will automatically use **.500f** as a  **StrokeWidth** .
## Want more information?
http://www.pshul.com/2018/01/25/xamarin-forms-using-svg-images-with-skiasharp/
https://www.pshul.com/2018/02/01/xamarin-forms-interactive-svg-image-using-skiasharp-with-pan-and-zoom/
https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/graphics/skiasharp/curves/path-data
## Recommendation
I would advise you to use Grid layout to control Svg size according to your need because other layouts seems to have issues with canvas that if Svg is scaled to certain point it will clip. Don't forget to explictly set row height or you will face same clipping issues.
## Future
I might work on to fix clipping issue and add more features like drop shadow.
>**Note:** I haven't tested this on **iOS** or **UWP**. 
