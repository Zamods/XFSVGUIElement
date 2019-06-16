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

    <local:SVGUIElement ResourceId="XFSVGUIElement.UISvgs.Triangle.txt" Grid.Row="4" Color="#874bd7" SVGPaintStyle="Fill" />
Here, you can see that **ResourceId**  contains the name of project ***"XFSVGUIElement"*** then folder in which svg paths are stored ***"UISvgs"*** and then name of the file itself ***"Triangle.txt"*** . 
So, Here's the format `ResourceId="MyProject.MySvgs.CircleSvg.txt"`.

> **Note:** Always set **Build Action** for Svg file as **Embedded Resource** .

## SVGPaintStyle

**SVGPaintStyle** handles the painting style for svg. It allows user to explicitly decide how they want to color thier svg. There are three options offered by **SVGPaintStyle** as follows:
 - Fill - Paints svg with given color. **"Default Option"**
 - Stroke - Paints svg only with stroke color. 
 -   FillAndStroke - Paints svg with both color and stroke color.
  > **Note:** For **"Stroke"** option **"StrokeWidth"** property is necessary. Otherwise, Svg will be invisible.

### I will write more later...
