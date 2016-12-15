using System.Drawing;


namespace TagsCloudVisualization {
    public class Tag {
        public readonly string Text;
        public readonly Size Form;
        //public readonly Color Color;
        public Point Location;
        public Tag(string text, Size form, Point location) {
            Text = text;
            Form = form;
            Location = location;
            //Color = Color.Brown;
        }
    }
}
