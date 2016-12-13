using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day11
{
    class Floor
    {
        public List<string> Generators { get; set; }
        public List<string> Microchips { get; set; }

        public List<string> allItems
        {
            get
            {
                List<string> items = new List<string>();
                items.AddRange(Generators.OrderBy(x => x));
                items.AddRange(Microchips.OrderBy(x => x));
                return items;
            }
        }

        public int Pairs
        {
            get
            {
                int i = 0;
                foreach (string gen in Generators)
                {
                    if (Microchips.Any(c => c.Contains(gen.Split('-').First())))
                    {
                        i++;
                    }
                }
                return i;
            }
        }

        public int LonelyMicrochips
        {
            get
            {
                int i = 0;
                foreach (string micro in Microchips)
                {
                    if (!Generators.Any(g => g.Contains(micro.Split('-').First())))
                    {
                        i++;
                    }
                }
                return i;
            }
        }

        public int LonelyGenerators
        {
            get
            {
                int i = 0;
                foreach (string gen in Generators)
                {
                    if (!Microchips.Any(g => g.Contains(gen.Split('-').First())))
                    {
                        i++;
                    }
                }
                return i;
            }
        }
    }

    class Building
    {
        public int Steps { get; set; }
        public List<Floor> Floors { get; set; }
        public int currentFloor { get; set; }

        public string State()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Floors.Count; i++)
            {
                sb.Append("Floor " + i);
                sb.Append(Floors[i].LonelyMicrochips + " " + Floors[i].LonelyGenerators + " " + Floors[i].Pairs);
            }
            sb.Append(currentFloor);
            return sb.ToString();
        }

        public Building Clone()
        {
            Building newBuilding = new Building();
            newBuilding.Floors = new List<Floor>();
            for (int i = 0; i < Floors.Count; i++)
            {
                newBuilding.Floors.Add(new Floor()
                {
                    Microchips = new List<string>(Floors[i].Microchips),
                    Generators = new List<string>(Floors[i].Generators)
                });
            }
            newBuilding.currentFloor = currentFloor;
            return newBuilding;
        }

        public int Goal
        {
            get
            {
                //Distance from Floor 4 summed
                int x = 0;
                for (int f = 0; f < 4; f++)
                {
                    for (int i = 0; i < Floors[f].allItems.Count; i++)
                    {
                        x += (3 - f);
                    }
                }
                return x;
            }
        }
    }
}
