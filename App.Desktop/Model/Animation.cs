using System;
using System.Collections.Generic;
//using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Walle.Model
{
    class Animation
    {

        #region Singleton
        static Animation instance;

        public static Animation getInstance()
        {
            if (instance == null)
                instance = new Animation();

            return instance;
        }
        #endregion

        private Animation()
        {
            pixelToCellMap = new Dictionary<Point, Cell>();
            instance = this;
            frames = new List<Frame>();
            cells = new List<Cell>();
            touchRegions = new List<TouchRegion>();


        }

        List<Frame> frames;
        public List<TouchRegion> touchRegions { get; private set; }

        public List<Cell> cells { get; private set; }

        public List<Led> getLeds()
        {
            List<Led> leds = new List<Led>();

            foreach (Cell c in cells)
            {
                leds.AddRange(c.getLeds());
            }
            return leds;
        }

        Dictionary<Point, Cell> pixelToCellMap;

        internal void addTouchRegion(TouchRegion t)
        {
            touchRegions.Add(t);
        }

        public Boolean PointInCell(Point p)
        {
            System.Windows.Point roundedPt = new System.Windows.Point((int)p.X, (int)p.Y);
            return pixelToCellMap.ContainsKey(roundedPt);
        }

        public Cell findCell(Point p)
        {
            System.Windows.Point roundedPt = new System.Windows.Point((int)p.X, (int)p.Y);
            return pixelToCellMap[roundedPt];
        }

        public int findColorCell(Point p)
        {
            System.Windows.Point roundedPt = new System.Windows.Point((int)p.X, (int)p.Y);
            return cells.IndexOf(pixelToCellMap[roundedPt]);
        }

        Frame getFrame(int index)
        {
            return frames[index];
        }

        public override string ToString()
        {
            string output = "";
            output += "// define the number of cells \n";
            output += "#define CELLS " + cells.Count + "\n\n";

            output += "// define the LED number that begins each group \n";
            output += "const unsigned int cellBoundary[] = { \n";
            output += numLedsInEachCell();
            output += "};\n\n";

            output += "//Define nubmer of frames\n";
            output += "#define FRAMES " + frames.Count + "\n\n"; 
            output += "const unsigned int frameDef[] = {\n";
            output += outputFrames() + "\n";
            output += "};\n\n";

            Console.Write(output);

            return output;
        }

        internal int numFrames()
        {
            return frames.Count;
        }

        internal void addFrame(Frame f)
        {
            frames.Add(f);
            foreach (Cell c in cells)
            {
                c.color.Add(System.Drawing.Color.Black);
            }
        }

        internal Frame addCopyOfFrame(int indexOfCopyFrame)
        {
            int newFrameLocation = numFrames();
            Frame f = new Frame(frames[indexOfCopyFrame]);
            addFrame(f);
            copyFrameAndPlaceAtIndex(indexOfCopyFrame, newFrameLocation);

            return f;
        }

        internal void copyFrameAndPlaceAtIndex(int indexOfCopyFrame, int newFrameLocation)
        {
            foreach(Cell c in cells)
            {
                c.setColorAtIndex(c.color[indexOfCopyFrame], newFrameLocation);
            }
        }

        internal void addCell(Cell c)
        {
            cells.Add(c);
            foreach (System.Windows.Point p in c.Points)
            {
                if(!PointInCell(p)) 
                    pixelToCellMap.Add(p, c);
            }
        }

        internal string numLedsInEachCell()
        {
            string output = "";
            int numPreviousLeds = 0;
            foreach (Cell c in cells)
            {
                output += string.Format("{0},\n", numPreviousLeds);
                numPreviousLeds += c.ledCount();
            }
            return output;
        }

        string outputFrames()
        {
            string output = "";
            for (int f = 0; f < frames.Count; f++)
            {
                output += cellConfig(f);
                output += frames[f].ToString() + "\n";
            }

            return output;
        }

        private string cellConfig(int f)
        {
            string output = "";
            foreach (Cell c in cells)
                output += c.ToString(f) + "\n";

            return output;
        }

        internal int getCellNumber(Cell c)
        {
            return cells.IndexOf(c);
        }
    }
}
