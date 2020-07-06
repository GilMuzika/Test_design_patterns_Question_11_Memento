using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Test_design_patterns_Question_11_Memento.UI
{
    public class LabelField : Label
    {
        #region properties
        private int _width = 25;
        public new int  Width
        {
            get { return _width; }
            set
            {
                _width = value;
                base.Width = value;
            }
        }
        private int _height = 25;
        public new int Height
        {
            get { return _height; }
            set
            {
                _height = value;
                base.Height = value;
            }
        }
        private ContentLabel _content = new ContentLabel('?', Color.DarkRed);
        public ContentLabel Content
        {
            get { return _content; }
            set
            {
                _content = value;
                this.Image = ImageOnLabel(value.Character, value.Color);
            }
        }
        private char _character;
        public char Character
        {
            get { return _character; }
            set
            {
                _character = value;
                Color oldColor = this.Content.Color;
                this.Content = new ContentLabel(value, oldColor);
                this.Image = this.ImageOnLabel(this.Content.Character, this.Content.Color);
            }
        }
        public LabelField LefT { get; set; }
        public LabelField RighT { get; set; }
        public LabelField UP { get; set; }
        public LabelField DowN { get; set; }

        public Point MatrixIndex { get; set; }
        # endregion properties


        #region constructors
        public LabelField()
        {
            base.Width = this.Width;
            base.Height = this.Height;

            this.Image = this.ImageOnLabel(this.Content.Character, this.Content.Color);
        }
        public LabelField(ContentLabel content)
        {
            base.Width = this.Width;
            base.Height = this.Height;
            this.Content = content;

            this.Image = this.ImageOnLabel(this.Content.Character, this.Content.Color);
        }
        public LabelField(ContentLabel content, int width, int height)
        {
            this.Width = width;
            this.Height = height;
            base.Width = this.Width;
            base.Height = this.Height;
            this.Content = content;

            this.Image = this.ImageOnLabel(this.Content.Character, this.Content.Color);
        }
        #endregion constructors


        private Bitmap ImageOnLabel(char idntty, Color color)
        {
            Bitmap forPicture = new Bitmap(Width, Height);
            Graphics graphicsObj = Graphics.FromImage(forPicture);

            this.drawBorder(1, color);

            Font drawFont = new Font("Arial", 16);
            SolidBrush drawBrush = new SolidBrush(color);
            StringFormat drawFormat = new StringFormat();

            graphicsObj.DrawString(idntty.ToString(), drawFont, drawBrush, this.Width / 2 - 10, this.Height / 2 - 10, drawFormat);
            graphicsObj.Dispose();

            return forPicture;
        }
    }
}
