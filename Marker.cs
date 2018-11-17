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

        public void Persist()
        {
            DataLists dataLists = DataStorage.GetInstance().DataLists;
            Id = DataLists.GenerateMarkerId();
            dataLists.Markers.Add(this);
        }

        public void Delete()
        {
            DataLists dataLists = DataStorage.GetInstance().DataLists;
            dataLists.Markers.Remove(this);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return false;
            }
            Marker marker = (Marker)obj;
            if (marker.Id <= 0 || Id <= 0)
            {
                return false;
            }
            return Id == marker.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }
    }
}
