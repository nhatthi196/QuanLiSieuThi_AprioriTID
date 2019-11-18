using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprioriTID.Model
{
    public class Item
    {
        public string Code { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
        public Item(string item)
        {
            this.Code = item;
        }
        public Item(string item, int value)
        {
            this.Code = item;
            this.Value = value;
        }

        public Item(string item, int value, int id)
        {
            this.Code = item;
            this.Value = value;
            this.Id = id;
        }
        public Item(Item item)
        {
            this.Id = item.Id;
            this.Code = item.Code;
            this.Value = item.Value;
        }
        public override int GetHashCode()
        {
            return this.Id;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Item;
            if (other == null) return false;

            return Id == other.Id;
        }
    }
}
