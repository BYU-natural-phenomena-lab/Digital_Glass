Walle
=======

This project contains a simple GUI for helping artists create digital stained glass.

# Getting started with the desktop app

You will need Visual Studio to build this application.

Open "Walle.sln" using Visual Studio. Run *Build > Build solution*. 
This should automatically perform all steps necessary.

To launch the application, run *Debug > Start (with/without) debugging*.

If you encounter errors regarding "missing references", you may need to run a Nuget package restore to install the dependencies. <https://docs.nuget.org/consume/package-restore>
But this should have been setup automatically by the build process.

How to generate the documentation: 
http://gusclass.com/blog/2013/02/25/creating-html-documentation-in-c-using-visual-studio-and-sandcastle/
https://github.com/EWSoftware/SHFB

# Getting started with the Arduino code
The folder *arduino_code* contains the files necessary to run animation on a board using an Arduino. It also includes preliminary support for touch sensors, but it needs more work.

# Application Overview

## User story
A user loads an image into the application. Currently there are 2 tools. 

1. The "LED" tool allows users to click where they want LEDs to be placed. 
2. The "Cell" tool allows users to click on the image to indicate where they want a cell. This tool will automatically find the outline of the cell.

The user can then export the file into *.zip file that contains all the instructions a manufacturer needs to produce the hardware.

This currently produces files designed for the [SeeedStudio PCB service](http://www.seeedstudio.com/service/index.php?r=pcb).


## Architecture
This application is built using WPF. [Introduction to WPF](https://msdn.microsoft.com/en-us/library/aa970268%28v=vs.110%29.aspx) on MSDN.

This application uses the MVVM architecture. [WPF Apps with MVVM Design Pattern](https://msdn.microsoft.com/en-us/magazine/dd419663.aspx).

### Generation steps

1. The user creates their board configuration. The application stores this in memory.
2. The user clicks export.
    1. The application generates an eagle board file (*.brd). This board is a temporary file (in %appdatalocal%/Temp).
    2. The application launches eagle.exe to perform autorouting.
    3. The application launches eaglecon.exe to execute CAM processing on the *.brd file. 
       This produces about 10 files used by the GERBER PCB assemply process.
    4. The application bundles the gerber files into a *.zip and saved in a user specified location.



# Future work

* Gerber export for multiple PCB manufacturers. 
* An idea for better touch sensing: https://learn.sparkfun.com/tutorials/mpr121-hookup-guide
* The cell tool finds the outline of a cell (most of the time) but it does not use this information to produce the laser cutting guide.
* The GUI doesn't show well which tool is selected.
* The tools does not generate a second "touch" board. This is supposed to contain large copper regions that can be used for capacitive touch.
* 1 pixel in image = 1 mm in the board. Need to build in scaling.
* Create a simple GUI to configure the location of eagle.exe and eaglecon.exe
