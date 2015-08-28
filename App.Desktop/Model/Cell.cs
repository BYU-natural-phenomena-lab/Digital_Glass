using System.Windows;
using System.Drawing;
using System.Collections.Generic;
using System;

namespace DigitalGlass.Model
{
    public class Cell
    {

        //Creates a copy of a cell
        public Cell(Cell c)
        {
            Points = c.Points;
            color = c.color;
            leds = c.leds;
        }

        public Cell()
        {
            leds = new List<Led>();
            color = new List<Color>();
        }

        public Cell(System.Windows.Point[] Points, int numFrames, int curFrame, Color color)
        {
            leds = new List<Led>();
            this.Points = Points;
            this.color = new List<Color>();
            for (int i = 0; i < numFrames; i++)
                this.color.Add(System.Drawing.Color.FromArgb(0, 0, 0, 0)); //Transparent White
            
            this.color[curFrame] = color;
        }

        internal List<Led> getLeds()
        {
            return leds;
        }

        /// <summary>
        /// Points are listed in winding order.
        /// </summary>
        public System.Windows.Point[] Points { get; set; }

        public List<Color> color { get; set; }

       List<Led> leds { get; set; }

        public void addLed(Led l)
        {
            leds.Add(l);
        }

        public string ToString(int frameNum)
        {
            return string.Format("{0}, {1}, {2},", color[frameNum].R, color[frameNum].G, color[frameNum].B);
        }

        public int ledCount()
        {
            return leds.Count;
        }

        internal void setColorAtIndex(Color color, int index)
        {
           this.color[index] = color;
        }
    }
}