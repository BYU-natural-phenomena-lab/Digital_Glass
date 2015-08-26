using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalGlass.ViewModel;
using System.Windows.Media.Imaging;

namespace DigitalGlass.Model
{
    public class Frame : ViewModelBase
    {

        public int gotoFrame { get; set; } 
        public int timeDelay { get; set; }

        public BitmapImage image = new BitmapImage();


        public Frame()
        {
            gotoFrame = -1;
            timeDelay = 100;
            createImage();
        }

        public Frame(Frame toCopy)
        {
            this.gotoFrame = toCopy.gotoFrame;
            this.timeDelay = toCopy.timeDelay;
            createImage();
        }

        void createImage()
        {
            image.BeginInit();
            image.UriSource = new Uri(@"C:\Users\Keith Halterman\AppData\Local\Temp\DigitalGlass_Frame0.png");
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.EndInit();
        }

        public String ToString()
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
