using System.Drawing;


namespace TagsCloudVisualization {
    public class Tag {
        public readonly string Text;
        public readonly Font Font;
        public readonly Size Form;
        public readonly Color Color;
        public Point Location;
        public Tag(string text, Font font, Size form, Point location, Color color) {
            Text = text;
            Form = form;
            Location = location;
            Font = font;
            Color = color;
        }
    }
}
