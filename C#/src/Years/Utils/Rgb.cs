namespace Years.Utils
{
    public class Rgb
    {
        public Rgb(int r, int g, int b)
        {
            R = r;
            G = g;
            B = b;
        }

        public int R;
        public int G;
        public int B;

        public override string ToString()
        {
            return $"({R}, {G}, {B})";
        }
    }
}
