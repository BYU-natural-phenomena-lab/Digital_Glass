Digital Glass
=======

This project contains a simple GUI for helping artists create digital stained glass.

# Getting started with the desktop app

You will need Visual Studio to build this application.

Open "DigitalGlass.sln" using Visual Studio. Run *Build > Build solution*. 
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
A user loads an image into the application. Currently there are 3 tools. 

1. The "LED" tool allows users to click where they want LEDs to be placed. 
2. The "Fill Color" tool allows users to click on a cell and indicate the color the cell should light up with in the curent frame.
	The user can add as many frames as they would like. Each frame can have a new configuration of colors. The user can specify how long the frame should apear, and which frame comes next in the animation.  
3. The "Touch Region" tool allows the user to select touch regions for the board, these wires are routed into the PVC board

The user can then export the file into *.zip file that contains all the instructions a manufacturer needs to produce the hardware.
The user can also export a .config file to be run on the ardinuo that will run the light animations and interactions. 

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

* Save/Load previous work - Store the placed LEDs and animation sequence in XML so that it can be revisited and modified.
* Gerber export for multiple PCB manufacturers. 
* An idea for better touch sensing: https://learn.sparkfun.com/tutorials/mpr121-hookup-guide
* The cell tool finds the outline of a cell (most of the time) but it does not use this information to produce the laser cutting guide.
* The GUI doesn't show well which tool is selected.
* Create a simple GUI to configure the location of eagle.exe and eaglecon.exe


# Known Bugs

* Image loads in wrong postition - Temporary work around; Resize the window
* LEDs, touch regions, and colored cells overlap and hide one another -- Need to plan best way to display them