using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using Org.GS;

namespace Org.DocGen.DocSpec
{
    public class RunKit
    {
        public Graphics Graphics { get; set; }
        private GraphicsState graphicsState;
        private bool InDiagnosticsMode;
        private int DiagnosticLevel; 

        public Font Font { get; set; }
        public System.Drawing.Color Color { get; set; }
        public JustificationValues Justification { get; set; }
        public float Scale { get; set; }
        public float WidthFactor { get; set; }
        public float SpaceWidthFactor { get; set; }
        public float TextCompressionFactor { get; set; }
        public float StandardSpaceWidth { get; set; }
        public float LetterWidth { get; set; }
        public float WidthPaddingTrim { get; set; }
        public float WordSpacing { get; set; }

        public float Left { get; set; }
        public float Top { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float LineHeight { get; set; }
        public float TotalHeightUsed { get; set; }

        private string[] _tokens;
        public string[] Tokens { get { return _tokens; } }
        public List<string> LineTokens { get; set; }

        private string _runText;
        public string RunText
        {
            get { return _runText; }
            set { this.Set_RunText(value); }
        }

        public float Width { get; set; }
        public float SpaceRemaining { get; set; }

        public RunKit(Graphics graphics, Font font, System.Drawing.Color color, JustificationValues justification, float width, float left, float top, float scale, float widthFactor, float spaceWidthFactor, 
                      float textCompressionFactor, bool inDiagnosticsMode, int diagnosticLevel)
        {
            this.Graphics = graphics;
            this.InDiagnosticsMode = inDiagnosticsMode;
            this.DiagnosticLevel = diagnosticLevel;
            this.Font = font;
            this.Color = color;
            this.Justification = justification;
            this.Scale = scale;
            this.WidthFactor = widthFactor;
            this.SpaceWidthFactor = spaceWidthFactor;
            this.TextCompressionFactor = textCompressionFactor;
            this.WordSpacing = 0F;

            this.Width = width;
            this.SpaceRemaining = this.Width;

            this.Left = left;
            this.X = this.Left;
            this.Top = top;
            this.Y = this.Top;
            this.LineHeight = 0F;
            this.TotalHeightUsed = 0F;

            this._runText = String.Empty;
            this._tokens = new string[0];
            this.LineTokens = new List<string>();

            this.SetScalables();
        }

        public void ScaleGraphics()
        {
            this.graphicsState = this.Graphics.Save();

            GraphicsState state = this.Graphics.Save();
            this.Graphics.ResetTransform();
            this.Graphics.ScaleTransform(this.Scale * this.TextCompressionFactor, this.Scale);

            this.SetScalables();
        }

        public void ResetGraphicsScale()
        {
            if (this.graphicsState == null)
                return;

            this.Graphics.ResetTransform();
            this.Graphics.Restore(this.graphicsState);

            this.SetScalables();
        }

        public void ProcessJustification()
        {
            switch (this.Justification)
            {
                case JustificationValues.Center:
                    this.X += this.SpaceRemaining / 2;
                    break;

                case JustificationValues.Right:
                    this.X += this.SpaceRemaining;
                    break;

                case JustificationValues.Both:
                    this.WordSpacing = this.SpaceRemaining / (this.LineTokens.Count - 1);
                    break;
            }
        }

        private void Set_RunText(string text)
        {
            this._tokens = new string[0];
            this.LineTokens = new List<string>();

            if (text == null)
                return;

            if (text.IsBlank())
                return;

            this._tokens = text.Split(Constants.SpaceDelim);
        }

        private void SetScalables()
        {
            this.StandardSpaceWidth = this.Graphics.MeasureString(" ", this.Font).Width;
            this.LetterWidth = this.Graphics.MeasureString("o", this.Font).Width;
            this.WidthPaddingTrim = this.LetterWidth * this.WidthFactor;
        }

        public void MapLineOfText()
        {
            this.WordSpacing = 0F;
            this.LineHeight = 0F;
            this.ProcessJustification();

            bool firstToken = true;
            foreach (string s in this.LineTokens)
            {
                SizeF sz = this.Graphics.MeasureString(s, this.Font);
                if (sz.Height > this.LineHeight)
                    this.LineHeight = sz.Height;

                float xAdjustment = 0F;
                if (!firstToken)
                    xAdjustment = this.WidthPaddingTrim / 2;

                this.X -= xAdjustment;

                float adjustedWidth = sz.Width - this.WidthPaddingTrim;
                float adjustedSpace = this.StandardSpaceWidth * this.SpaceWidthFactor;

                this.X += adjustedWidth + adjustedSpace + this.WordSpacing;

                firstToken = false;
            }

            this.X = this.Left;
            this.Y += this.LineHeight;
            this.LineTokens.Clear();
            this.SpaceRemaining = this.Width;
            this.TotalHeightUsed += this.LineHeight;
        }

        public void DrawLineOfText()
        {
            this.WordSpacing = 0F;
            this.LineHeight = 0F;
            this.ProcessJustification();

            bool firstToken = true;
            foreach (string s in this.LineTokens)
            {
                SizeF sz = this.Graphics.MeasureString(s, this.Font);
                if (sz.Height > this.LineHeight)
                    this.LineHeight = sz.Height;

                float xAdjustment = 0F;
                if (!firstToken)
                    xAdjustment = this.WidthPaddingTrim / 2;

                this.X -= xAdjustment;

                float adjustedWidth = sz.Width - this.WidthPaddingTrim;
                float adjustedSpace = this.StandardSpaceWidth * this.SpaceWidthFactor;

                Brush b = new SolidBrush(this.Color);

                this.Graphics.DrawString(s, this.Font, b, this.X, this.Y);

                if (this.InDiagnosticsMode)
                {
                    if (this.DiagnosticLevel > 0)
                        this.Graphics.DrawRectangle(new Pen(Brushes.Blue, 0.2F), this.X, this.Y, adjustedWidth, sz.Height);
                }

                this.X += adjustedWidth + adjustedSpace + this.WordSpacing;

                firstToken = false;
            }

            this.X = this.Left;
            this.Y += this.LineHeight;
            this.LineTokens.Clear();
            this.SpaceRemaining = this.Width;
            this.TotalHeightUsed += this.LineHeight;
        }
    }
}
