/*********************************************
Code - modify if you know what you are doing

How to use capactive sensors http://playground.arduino.cc/Main/CapacitiveSensor?from=Main.CapSense


How to use "neopixels" https://github.com/adafruit/Adafruit_NeoPixel

*/
#include "NeoPixel.h"
#include <avr/power.h>
#include "config.h"

#define FRAME_SIZE (3*CELLS + 2)
#define SENSOR_SENSITIVITY 60

// Setup the NeoPixel library
Adafruit_NeoPixel pixels = Adafruit_NeoPixel(NUMPIXELS, PIN, NEO_GRB + NEO_KHZ800);

unsigned int _frame = 0;

bool sampleSensors() {
  long val;
  long maxVal = -1;
  unsigned int lastFrame = _frame;
  for (int i = 0; i < SENSORS; i++) {
    val = sensors[i]->capacitiveSensor(10);
    Serial.print(i);
    Serial.print(": ");
    Serial.print(val);
    Serial.print("\t");
    if (val > SENSOR_SENSITIVITY && val > maxVal) {
      _frame =  sensorJump[i];
      maxVal = val;
    }
  }
  Serial.println("");
  return lastFrame != _frame;
}

void setup() {
  _frame = 0;

  pixels.begin(); // This initializes the NeoPixel library.
  // setup the sensors
  for (int i = 0; i < SENSORS; i++) {
    //setup the sensor from SEND_PIN to receivePin
    sensors[i] = new CapacitiveSensor(SENSOR_SEND_PIN, sensorReceivePins[i]);
    
    // this turns off auto recalibration of sensor 
    sensors[i]->set_CS_AutocaL_Millis(0xFFFFFFFF);
  }
  Serial.begin(9600);

}

#if USE_TRANSITION
unsigned int lastRed[CELLS];
unsigned int lastGreen[CELLS];
unsigned int lastBlue[CELLS];
#endif

void loop() {
  unsigned int pos, group;
  int red, green, blue, framePause;


#if USE_TRANSITION
  group = 0;
  pos = _frame * FRAME_SIZE;
  int stepRed[CELLS];
  int stepGreen[CELLS];
  int stepBlue[CELLS];
  group = 0;
  for (int i = 0; i < NUMPIXELS; ++i) {
    if (cellBoundary[group] == i) {
      stepRed[group] = ((int)frameDef[pos++] - (int) lastRed[group]) / TRANS_STEPS;      // calculate growth
      stepGreen[group] =  ((int)frameDef[pos++] - (int) lastGreen[group]) / TRANS_STEPS;
      stepBlue[group] =((int) frameDef[pos++] - (int) lastBlue[group]) / TRANS_STEPS;
      group++;
    }
  }

  for (int s = 1; s < TRANS_STEPS; s++)
  {
    group = 0;
    for (int i = 0; i < NUMPIXELS; ++i) {

      if (cellBoundary[group] == i) {
        red = s * stepRed[group] + lastRed[group];
        green = s * stepGreen[group] + lastGreen[group];
        blue = s * stepBlue[group] + lastBlue[group];
        group++;
      }
      pixels.setPixelColor(i, pixels.Color(red, green, blue));
    }
    pixels.show();
    delay(10);
  }
#endif


  group = 0;
  pos = _frame * FRAME_SIZE;
  for (int i = 0; i < NUMPIXELS; ++i) {
    if (cellBoundary[group] == i) {
      red = frameDef[pos++];      // order of these 3 lines matters
      green = frameDef[pos++];
      blue = frameDef[pos++];
#if USE_TRANSITION
      lastRed[group] = red;
      lastGreen[group] = green;
      lastBlue[group] = blue;
#endif
      group++;
    }

    pixels.setPixelColor(i, pixels.Color(red, green, blue));
  }
  pixels.show();


  framePause = frameDef[pos++] / 10; // truncation OK
  while (framePause-- >= 0) {
    delay(10);
    if (sampleSensors()) { // sampleSensors may change pos. Returns true when it does.
      return;
    }
  }

  int nextFrame = frameDef[pos++];
  if (nextFrame >= 0) {
    _frame = (unsigned int) nextFrame;
  } else {
    _frame++;
  }

  // safety check and cycle
  if (_frame >= FRAMES) {
    _frame = 0;
  }

}




