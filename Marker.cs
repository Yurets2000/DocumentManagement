using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement
{
    public class Marker
    {
        public int Id { get; set; }
        public readonly Color markerColor;

        public enum Color
        {
            YELLOW, RED, BLUE, GREEN, ORANGE
        }

        public Marker(Color color)
        {
            markerColor = color;
        }

        public void Mark(Document document, int offset, int length) { }
    }
}
