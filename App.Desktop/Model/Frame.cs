using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Walle.Model
{
    class Frame
    {

        int gotoFrame = -1;
        int timeDelay = 100;

        public Frame()
        {
        }

        public Frame(Frame toCopy)
        {
            this.gotoFrame = toCopy.gotoFrame;
            this.timeDelay = toCopy.timeDelay;
        }

        public override String ToString()
        {
            return timeDelay + ",\n" + gotoFrame + ",\n\n";

        }

        /*
        public Frame(Frame f)
        {
            gotoFrame = f.gotoFrame;
            cells = new List<ColorCell>();
            timeDelay = f.timeDelay;

            foreach (ColorCell c in f.cells)
            {
                cells.Add(new ColorCell(c)); 
            }
        }

        public Frame deepCopy()
        {
            return new Frame(this);
        }



        internal void addEmptyColorCell(ref Cell cell)
        {
            cells.Add(new ColorCell(ref cell));
        }

        internal ColorCell getCellColor(int cellNum)
        {
            return cells[cellNum];
        }

        internal void setCellColor(int index, Color color)
        {
            cells[index].color = color;
        }
        */
    }
}
