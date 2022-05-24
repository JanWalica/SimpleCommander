using System;
using System.Collections.Generic;
using System.Text;

namespace TotalCommander.Model
{
    class DirItem
    {
        public String Name { get; set; }

        public ItemType Type { get; set; }

        public DirItem(String name, ItemType type)
        {
            Name = name;
            Type = type;
        }
    }
}
