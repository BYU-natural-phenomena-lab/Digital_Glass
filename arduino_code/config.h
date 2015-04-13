/**************************
Config section

Here you define cells, frames, animation speed, sensors etc.

*/
#include "CapacitiveSensor.h"

// Which pin on the Arduino is connected to DIN on the led board
#define PIN            6


// How many LEDS are there?
#define NUMPIXELS      20

// If USE_TRANSITION is non-zero, then it will "fade" between colors
#define USE_TRANSITION 1
#define TRANS_STEPS 50 // more steps, more smooth but slower

// define sensors
// see http://playground.arduino.cc/Main/CapacitiveSensor?from=Main.CapSense to understand the circuit design
#define SENSORS 2
#define SENSOR_SEND_PIN 2

CapacitiveSensor* sensors[SENSORS];
const unsigned int sensorReceivePins[SENSORS] = {
  4,
  3,
};

// define which which frame number to jump to when sensed
const unsigned int sensorJump[SENSORS] =
{
  0,
  1,
};

// define the number of cells
#define CELLS  6
const unsigned int cellBoundary[CELLS] = { // define the LED number that begins each group
  0,
  3,
  4,
  12,
  15,
  16
};

/*
This next part is ugly, but it is not meant to be programmed by hand.
This array is grouped in frames.
Each group contains a list of the RGB colors for each cell.
This is followed by one number for the delay until the next frame, and the number of the next frame. (see example below)

Frame =  {
            r0,g0,b0, // cell 0
            r1,g1,b1, // cell 1
            r2,g2,b2, // cell 2
            delay, // delay until transition
            next_frame // which frame number. NB: using its own number will cause infinite loop on that frame
          }

NB: The "delay" is actually broken up into 10ms chunks.
Every 10ms it re-samples for a sensors that jump animation to a new frame number.

There should be FRAMES * CELLS * RGB + FRAMES numbers in frameDef,
e.g.  2 frames * 6 cells * 3 (rgb) + 2 frames = 38 numbers.
Each frame is then followed by how long to delay until the next frame

EXAMPLE

const int frameDef[] = {
  255,0,0, // frame 0, cell 0
  0,255,0, // frame 0, cell 1
  0,0,255, // etc
  255,0,0,
  255,0,0,
  255,0,0,
  150, // show frame 0 for 150ms
  -1, // go to the next frame listed

  0,255,0,  // frame 1, cell 0
  0,0,255,
  255,0,0,
  0,255,0,
  0,255,0,
  0,255,0,
  290, // show frame 1 for 290ms
  3, // goto frame 3, skipping 2

  0,255,0,  // frame 2
  0,0,255,  // and yes, this frame is skipped in the default play
  255,0,0,
  0,255,0,
  0,255,0,
  0,255,0,
  290, // show frame 1 for 290ms
  0,  // goto frame 0

  0,0,255,  // frame 3, cell 0
  255,0,0,
  0,255,0,
  0,0,255,
  0,0,255,
  0,0,255,

  1500, // show frame 3 for 1.5s.
  -1,  // go to the next frame listed, which in this case loops to frame 0
};
*/

#define FRAMES 6
#define VALIDATE_DEF (3*CELLS + 2) * FRAMES

const unsigned int frameDef[VALIDATE_DEF] = {
  0,0,0,// frame 0 (off)
  0,0,0,
  0,0,0,
  0,0,0,
  0,0,0,
  0,0,0,
  100,
  0,

  203,0,110, // frame 1
  0,0,0,
  0,0,0,
  0,0,0,
  0,0,0,
  0,0,0,
  100,
  -1,

  255,0,0, // frame 2
  220,48,3,
  0,0,0,
  0,0,0,
  0,0,0,
  0,0,0,
  100,
  -1,
  
  0,255,0, // frame 3
  220,48,3,
  0,0,0,
  0,0,0,
  252,223,20,
  0,0,0,
  100,
  -1,
  
  0,0,255, // frame 4
  220,48,3,
  80,35,5,
  21,146,253,
  252,223,20,
  21,146,253,
  100,
  -1,
  
  255,0,0, // frame 5
  220,48,3,
  253,152,11,
  21,146,253,
  252,223,20,
  21,146,253,
  500,
  1,


};
