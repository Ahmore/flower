using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace App2
{
    public class Flower
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Flower p = (Flower)obj;
                return p.ID == this.ID;
            }
        }
        public override int GetHashCode()
        {
            return ID;
        }
    }
}
