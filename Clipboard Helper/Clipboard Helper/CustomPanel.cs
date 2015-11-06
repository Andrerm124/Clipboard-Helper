using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clipboard_Helper
{
    public class CustomPanel : Panel
    {
        public String title;
        TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak;

        public Rectangle insides;
        public Rectangle titleRect;

        public Color dynColour;
        public Color altColour;
        public Color bgColour;

        private double anim = 0;
        private double animSpeed = 0.025;
        private Boolean shouldDraw = false;

        public void Initialized()
        {
            this.SetStyle(
                System.Windows.Forms.ControlStyles.UserPaint |
                System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
                System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer,
                true);

            insides = ClientRectangle;
            insides.Y += (int)Font.GetHeight() + 4;

            titleRect = ClientRectangle;
            titleRect.Height = insides.Y;

            title = (String)this.Tag;
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
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            dynColour = Color.FromArgb(255, 255, 130, 0);
            bgColour = Color.FromArgb(255, 40 - (int)(20 * anim), 40 - (int)(20 * anim), 40 - (int)(20 * anim));

            e.Graphics.FillRectangle(new SolidBrush(bgColour), ClientRectangle);

            e.Graphics.DrawRectangle(new Pen(new SolidBrush(dynColour), 2), ClientRectangle);

            e.Graphics.DrawRectangle(new Pen(new SolidBrush(dynColour), 1), titleRect);

            TextRenderer.DrawText(e.Graphics, title, Font, titleRect, dynColour, flags);


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
    }
}
