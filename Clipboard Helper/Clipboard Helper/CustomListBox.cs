using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clipboard_Helper
{
    public class CustomListBox : Panel
    {
        public event MyEventHandler eventHandler;
        public delegate void MyEventHandler(object source, ListBoxEvent e);

        public List<ListBoxItem> contents = new List<ListBoxItem>();
        public int itemHeight = 30;
        public Rectangle insides;
        public Rectangle titleRect;
        public Rectangle sliderRect;
        public Rectangle sliderBGRect;

        public Graphics gfx;

        public double sliderVal = 0;

        private int sliderClickOffset;
        private int maxItems;
        public int totalItemSize;
        public int visibleItemSize;
        public int indexClicked = -1;

        public Color dynColour;
        public Color altColour;
        public Color bgColour;

        public double anim = 0;
        private double animSpeed = 0.025;
        private Boolean shouldDraw = false;
        private Boolean isDraggingSlider = false;

        public String title;
        public String listType;
        public TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak;
        public TextFormatFlags itemFlags = TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak;
        public Timer updateTimer = new Timer();

        public void Initialized()
        {
            gfx = this.CreateGraphics();

            this.SetStyle(
                System.Windows.Forms.ControlStyles.UserPaint |
                System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
                System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer,
                true);

            insides = ClientRectangle;
            insides.Width -= 20;
            insides.Height -= insides.Y;

            titleRect = ClientRectangle;
            titleRect.Height = 0;

            sliderRect = ClientRectangle;
            sliderRect.X += insides.Width;
            sliderRect.Width = 20;
            sliderRect.Height = insides.Height;
            sliderRect.Y = insides.Y;

            sliderBGRect = sliderRect;
            sliderBGRect.Height = insides.Height;

            maxItems = (insides.Height - 4) / itemHeight;
            visibleItemSize = maxItems * itemHeight;

            String tag = (String)this.Tag;

            if (tag.Split(',').Length > 1)
                listType = tag.Split(',')[1];
            else
                listType = "default";

            title = tag.Split(',')[0];

            updateTimer.Interval = 100;
            updateTimer.Tick += updateTimer_Tick;

            updateTimer.Start();
        }

        void updateTimer_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        public ListBoxItem AddItem(StoredData storedData)
        {
            Console.WriteLine("Adding: " + storedData.storedClip);
            ListBoxItem item = new ListBoxItem(storedData, insides.X + 4, 0, insides.Width - 8, itemHeight, this);

            contents.Insert(0, item);
            MasterForm.clipboardData.Insert(0, storedData);
            totalItemSize = -2;

            foreach (ListBoxItem item2 in contents)
            {
                item2.itemRect.Y += item.itemRect.Height;
                totalItemSize += item2.itemRect.Height + 2;
            }

            sliderVal = 0;

            sliderRect.Y = sliderBGRect.Y;

            UpdateSlider();

            return item;
        }

        public void RemoveItem(int index)
        {
            if (contents.Count - 1 < index)
            {
                indexClicked = -1;
                return;
            }

            Console.WriteLine("Removing: " + MasterForm.clipboardData[index].storedClip);

            contents.RemoveAt(index);
            MasterForm.clipboardData.RemoveAt(index);

            if (contents.Count == 0)
                indexClicked = -1;

            totalItemSize = -2;

            foreach (ListBoxItem item in contents)
            {
                totalItemSize += item.itemRect.Height + 2;
            }

            UpdateSlider();
        }

        public void ClearItems()
        {
            indexClicked = -1;
            contents.Clear();
            MasterForm.clipboardData.Clear();

            totalItemSize = 0;
            UpdateSlider();
        }

        public void UpdateSlider()
        {
            if (contents.Count <= 1 || totalItemSize < insides.Height)
            {
                sliderRect.Height = sliderBGRect.Height;
                sliderVal = 0;

                sliderRect.Y = sliderBGRect.Y;

                this.Invalidate();
                return;
            }

            double modifier = (double) totalItemSize / (double)insides.Height; //(double)contents.Count / ((double)insides.Height / (double)itemHeight);
            
            if (modifier <= 0)
                return;

            sliderRect.Height = (int)(sliderBGRect.Height / modifier);

            this.Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            shouldDraw = true;
            this.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            shouldDraw = false;
            this.Invalidate();
            isDraggingSlider = false;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            ListBoxEvent lbEvent = new ListBoxEvent(e, this);

            if (lbEvent.itemClicked != null)
            {
                indexClicked = lbEvent.indexClicked;
                eventHandler(this, lbEvent);
            }

            if (e.X > sliderBGRect.X && e.X < sliderBGRect.X + sliderBGRect.Width && e.Y > sliderBGRect.Y && e.Y < sliderBGRect.Y + sliderBGRect.Height
                && sliderRect.Height != sliderBGRect.Height)
            {
                isDraggingSlider = true;
                sliderClickOffset = e.Y - sliderRect.Y;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (isDraggingSlider)
            {
                int sliderPos = e.Y - sliderClickOffset;

                if (sliderPos < sliderBGRect.Y)
                    sliderPos = sliderBGRect.Y;
                else if (sliderPos + sliderRect.Height > sliderBGRect.Y + sliderBGRect.Height)
                    sliderPos = sliderBGRect.Y - sliderRect.Height + sliderBGRect.Height;

                sliderRect.Y = sliderPos;

                int heightDiff = sliderBGRect.Height - sliderRect.Height;

                sliderVal = (double)(sliderRect.Y - sliderBGRect.Y) / (double)heightDiff;

                this.Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            isDraggingSlider = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            dynColour = Color.FromArgb(255, (int)(255 * anim), (int)(130 * anim), 0);
            altColour = Color.FromArgb(255, 100 - (int)(70 * anim), 100 - (int)(70 * anim), 100 - (int)(70 * anim));
            bgColour = Color.FromArgb(255, 70 - (int)(20 * anim), 70 - (int)(20 * anim), 70 - (int)(20 * anim));

            e.Graphics.FillRectangle(new SolidBrush(bgColour), ClientRectangle);

            DrawContents(e);

            e.Graphics.FillRectangle(new SolidBrush(altColour), sliderRect);
            e.Graphics.FillRectangle(new SolidBrush(altColour), titleRect);

            e.Graphics.DrawRectangle(new Pen(new SolidBrush(dynColour), 2), ClientRectangle);

            e.Graphics.DrawRectangle(new Pen(new SolidBrush(dynColour), 1), insides);

            e.Graphics.DrawRectangle(new Pen(new SolidBrush(dynColour), 1), titleRect);

            e.Graphics.DrawRectangle(new Pen(new SolidBrush(dynColour), 1), sliderRect);

            if (shouldDraw)
            {
                if (anim < 1)
                {
                    anim += animSpeed;

                    if (anim > 1)
                        anim = 1;

                    this.Invalidate();
                }
            }
            else
            {
                if (anim > 0)
                {
                    anim -= animSpeed;

                    if (anim < 0)
                        anim = 0;

                    this.Invalidate();
                }
            }
        }

        private void DrawContents(PaintEventArgs e)
        {
            int itemIndex = -1;
            int visibleItems = -1;
            int itemYOffset = itemYOffset = (int)((totalItemSize - (insides.Height - 9)) * sliderVal);
            //Console.WriteLine("Offset: " + itemYOffset + " total: " + totalItemSize);
            int totalOffset = 0;
            int visibleOffset = 0;

            foreach (ListBoxItem item in contents)
            {
                itemIndex++;
                int newHeight = insides.Y + totalOffset + 4 - itemYOffset;
                totalOffset += item.itemRect.Height + 2;

                if (totalOffset - itemYOffset < 0 ||
                    totalOffset - item.itemRect.Height - itemYOffset> insides.Height)
                {
                    item.visible = false;
                    continue;
                }

                visibleItems++;

                item.itemRect.Y = newHeight;

                if (item.itemIndex != itemIndex)
                    item.itemIndex = itemIndex;

                item.visible = true;
                item.DrawItem(e);

                visibleOffset += item.itemRect.Height + 2;

                /*e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 70 - (int)(20 * anim) - (itemIndex == indexClicked ? 20 : 0), 70 - (int)(20 * anim) - (itemIndex == indexClicked ? 20 : 0), 70 - (int)(20 * anim) - (itemIndex == indexClicked ? 20 : 0))), new Rectangle(insides.X + 4, itemYPos, insides.Width - 8, itemHeight));
                e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black), 1), new Rectangle(insides.X + 4, itemYPos, insides.Width - 8, itemHeight));

                TextRenderer.DrawText(e.Graphics, item, Font, new Rectangle(insides.X + 4, itemYPos, insides.Width - 8, itemHeight), dynColour, flags);*/
            }
        }
    }

    public class ListBoxEvent : MouseEventArgs
    {
        public String itemClicked;
        public MouseEventArgs mainEvent;
        public CustomListBox linkedList;
        public int indexClicked;

        public ListBoxEvent(MouseEventArgs e, CustomListBox listBox)
            : base(e.Button, e.Clicks, e.X, e.Y, e.Delta)
        {
            mainEvent = e;
            linkedList = listBox;
            indexClicked = 0;

            int yClick = e.Y - (linkedList.insides.Y + 4);
            int xClick = e.X - (linkedList.insides.X + 4);

            foreach (ListBoxItem item in linkedList.contents.ToArray())
            {
                if (item.visible && xClick >= item.itemRect.X && xClick <= item.itemRect.X + item.itemRect.Width &&
                    yClick >= item.itemRect.Y - linkedList.sliderVal && yClick <= item.itemRect.Y + item.itemRect.Height - linkedList.sliderVal)
                {
                    itemClicked = item.title;
                    break;
                }

                indexClicked++;
            }

            Console.WriteLine("Index: " + indexClicked);
            Console.WriteLine("Item: " + itemClicked);
        }
    }

    public class ListBoxItem
    {
        public String title;
        public DateTime date;
        public String itemType;
        public Color bgColour;
        public Color borderColour;
        public Rectangle itemRect;
        public Rectangle dateRect;
        public Rectangle textRect;
        public double lastSliderVal;
        public CustomListBox masterList;
        public int itemIndex;
        public int bgR = 70;
        public int bgG = 70;
        public int bgB = 70;
        public Boolean connected = false;
        public Boolean visible;

        public String username;
        public String password;

        public ListBoxItem(StoredData sd, int x, int y, int width, int height, CustomListBox list)
        {
            title = sd.storedClip;

            date = sd.storedTime; //time.ToString("HH:mm - dd/MM/yyyy");//yyyyMMddHHmmss");
            itemType = list.listType;
            masterList = list;

            int bestHeight = FindBestHeight(masterList.gfx);

            itemRect = new Rectangle(x, y, width, bestHeight + 17);
            dateRect = new Rectangle(x, y, width, 16);
            textRect = new Rectangle(x, y + 17, width, bestHeight);
        }

        public int FindBestHeight(Graphics gfx)
        {
            int totalHeight = (int)gfx.MeasureString(title, masterList.Font, itemRect.Width).Height;
            int bestHeight = totalHeight > 400 ? 400 : totalHeight;

            return bestHeight;
        }

        public void DrawItem(PaintEventArgs e)
        {
            bgColour = Color.FromArgb(255, 
                bgR + (itemIndex == 0 ? 10 : 0), 
                bgG + (itemIndex == 0 ? 10 : 0), 
                bgB + (itemIndex == 0 ? 10 : 0));

            DrawDefault(e);
        }

        public void DrawDefault(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(bgColour), itemRect);

            dateRect.Y = itemRect.Y;
            textRect.Y = itemRect.Y + 17;

            //e.Graphics.FillRectangle(new SolidBrush(Color.Orange), dateRect);
            e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black), 1), dateRect);
            e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black), 1), itemRect);

            using (StringFormat sf = new StringFormat()
                {
                    FormatFlags = StringFormatFlags.MeasureTrailingSpaces
                })
            {
                sf.Trimming = StringTrimming.EllipsisCharacter;
                e.Graphics.DrawString(title, masterList.Font, Brushes.Black, textRect, sf);

                TimeSpan difference = DateTime.Now.Subtract(date);
                String showDate = "No Date";

                if (difference.Days > 0)
                    showDate = date.ToString("HH:mm - dd/MM/yyyy");
                else if (difference.Hours > 0)
                    showDate = difference.Hours + "h ago";
                else
                    showDate = difference.Minutes + "m ago";

                e.Graphics.DrawString(showDate, masterList.Font, new SolidBrush(Color.FromArgb(255, 255, 130, 0)), dateRect, sf);
            }

            //TextRenderer.DrawText(e.Graphics, title, masterList.Font, itemRect, borderColour, masterList.itemFlags);
        }
    }
}
