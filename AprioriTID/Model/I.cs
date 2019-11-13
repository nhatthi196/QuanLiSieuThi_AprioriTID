using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprioriTID.Model
{
    public class I
    {
        public string Item { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
        public I(string item)
        {
            this.Item = item;
        }
        public I(string item, int value)
        {
            this.Item = item;
            this.Value = value;
        }

        public I(string item, int value, int id)
        {
            this.Item = item;
            this.Value = value;
            this.Id = id;
        }
        public I(I item)
        {
            this.Id = item.Id;
            this.Item = item.Item;
            this.Value = item.Value;
        }
        public override int GetHashCode()
        {
            return Item.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var other = obj as I;
            if (other == null) return false;

            return Id == other.Id;
        }
    }
}
