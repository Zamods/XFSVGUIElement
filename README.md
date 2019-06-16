# Xamarin Forms SVG UI Element

This project uses SkiaSharp libraries for Xamarin Forms to create SVG images on content pages. I have created reuseable control so anybody can use it without any further changes.


# Files

Here i will discuss about files in project and there relation with **SVG** for better understanding.

## UISvgs

**UISvgs** is a folder in the main project **XFSVGUIElement** that stores text files **.txt**. Svgs are stored as paths in text file like **M 6.7132,103.12901 13.642468,91.075013 20.6169,103.10293 Z** which will draw a triangle. You can use any vector graphic software like **"InkScape"** to acquire svg path which can be found by opening **.svg** with notepad or software alike.

![Like this Svg was opened in notepad++](https://1drv.ms/u/s!Alepmc_xf33mgyPsrpM-qEd1aZi-?e=P7Pix0)
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

    <local:SVGUIElement ResourceId="XFSVGUIElement.UISvgs.Triangle.txt" Grid.Row="4"
                            Color="#874bd7" SVGPaintStyle="Fill" />
Here, you can see that **ResourceId**  contains the name of project ***"XFSVGUIElement"*** then folder in which svg paths are stored ***"UISvgs"*** and then name of the file itself ***"Triangle.txt"*** . 
So, here's the format `ResourceId="MyProject.MySvgs.CircleSvg.txt"`.
