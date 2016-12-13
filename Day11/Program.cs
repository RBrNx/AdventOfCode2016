using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day11
{
    class Program
    {
        static Floor F0 = new Floor()
        {
            Microchips = new List<string>(new string[] { "Strontium-M", "Plutonium-M", /*"Elerium-M", "Dilitium-M"*/ }),
            Generators = new List<string>(new string[] { "Strontium-G", "Plutonium-G", /*"Elerium-G", "Dilitium-G"*/ })
        };
        static Floor F1 = new Floor()
        {
            Microchips = new List<string>(new string[] { "Ruthenium-M", "Curium-M" }),
            Generators = new List<string>(new string[] { "Thulium-G", "Ruthenium-G", "Curium-G" })
        };
        static Floor F2 = new Floor()
        {
            Microchips = new List<string>(),
            Generators = new List<string>(new string[] { "Thulium-M" })
        };
        static Floor F3 = new Floor()
        {
            Microchips = new List<string>(),
            Generators = new List<string>()
        };

        static HashSet<string> visited = new HashSet<string>();
        static int solution = int.MaxValue;
        static Queue<Building> queue = new Queue<Building>();

        static void Main(string[] args)
        {
            Building initial = new Building();
            initial.Floors = new List<Floor>();
            initial.Floors.Add(F0);
            initial.Floors.Add(F1);
            initial.Floors.Add(F2);
            initial.Floors.Add(F3);
            initial.currentFloor = 0;
            initial.Steps = 0;

            queue.Enqueue(initial);
            int count = 0;
            
            while(queue.Count > 0)
            {
                Building curr = queue.Dequeue();
                if (visited.Contains(curr.State()))
                {
                    continue;
                }
                visited.Add(curr.State());
                count++;

                if (isBuildingComplete(curr))
                {
                    if(solution > curr.Steps)
                    {
                        solution = curr.Steps;
                        Console.WriteLine("Shortest Solution Found in : " + solution + " moves");
                        break;
                    }
                }

                List<Building> possibleMoves = getPossibleMoves(curr);
                for(int i = 0; i < possibleMoves.Count; i++)
                {
                    queue.Enqueue(possibleMoves[i]);
                }
            }

            Console.WriteLine("Finished");
            Console.Read();
        }
        
        static List<Building> getPossibleMoves(Building b)
        {
            List<Building> possMoves = new List<Building>();
            Floor curr = b.Floors[b.currentFloor];
            List<Tuple<string, string>> validPairs = getValidPairs(curr);

            for(int i = 0; i < validPairs.Count; i++)
            {
                if(b.currentFloor < 3) //Can go up
                {
                    Building moved = moveItems(b, validPairs[i], b.currentFloor + 1);
                    if(moved != null)
                    {
                        possMoves.Add(moved);
                    }
                }
                if(b.currentFloor > 0) //Can go up
                {
                    Building moved = moveItems(b, validPairs[i], b.currentFloor - 1);
                    if (moved != null)
                    {
                        possMoves.Add(moved);
                    }
                }
            }
            return possMoves.OrderBy(x => x.Goal).ToList();
        }

        static Building moveItems(Building b, Tuple<string, string> items, int toFloor)
        {
            Building newBuilding = b.Clone();
            newBuilding.Steps = b.Steps + 1;

            Floor curr = newBuilding.Floors[b.currentFloor];
            curr.Generators.Remove(items.Item1);
            curr.Generators.Remove(items.Item2);
            curr.Microchips.Remove(items.Item1);
            curr.Microchips.Remove(items.Item2);

            Floor to = newBuilding.Floors[toFloor];
            if (items.Item1 != null && items.Item1.EndsWith("-G")) //Add First Item to Gens (if valid)
            {
                to.Generators.Add(items.Item1);
            }
            if (items.Item2 != null && items.Item2.EndsWith("-G")) //Add Second Item to Gens (if valid)
            {
                to.Generators.Add(items.Item2);
            }
            if (items.Item1 != null && items.Item1.EndsWith("-M")) //Add First Item to Microchips (if valid)
            {
                to.Microchips.Add(items.Item1);
            }
            if (items.Item2 != null && items.Item2.EndsWith("-M")) //Add Second Item to Microchips (if valid)
            {
                to.Microchips.Add(items.Item2);
            }

            newBuilding.currentFloor = toFloor;

            if(isFloorValid(to) && isFloorValid(curr))
            {
                if (visited.Contains(newBuilding.State()))
                {
                    return null;
                }
                return newBuilding;
            }

            return null;
        }

        static List<Tuple<string, string>> getValidPairs(Floor f)
        {
            List<Tuple<string, string>> validPairs = new List<Tuple<string, string>>();
            validPairs.AddRange(f.allItems.SelectMany(x => f.allItems, (x, y) => Tuple.Create(x, y)).Where(tuple => String.Compare(tuple.Item1, tuple.Item2) == -1).Where(x => canMoveTogether(x)));
            validPairs.AddRange(f.allItems.Select(x => Tuple.Create<string, string>(x, null)));
            return validPairs;
        }

        static bool canMoveTogether(Tuple<string, string> itemPair)
        {
            string[] item1 = itemPair.Item1.Split('-');
            string[] item2 = itemPair.Item2.Split('-');
            return (item1.First() == item2.First()) || (item1.Last() == item2.Last());
        }

        static bool isBuildingComplete(Building b)
        {
            Floor top = b.Floors.Last();
            if (!isFloorComplete(top))
            {
                return false;
            }

            List<Floor> otherFloors = b.Floors.Take(3).ToList();
            if(otherFloors.Any(x => !isFloorEmpty(x)))
            {
                return false;
            }
            return true;
        }

        static bool isFloorEmpty(Floor f)
        {
            return f.Microchips.Count == 0 && f.Generators.Count == 0;
        }

        static bool isFloorComplete(Floor f)
        {
            return isFloorValid(f) && f.Microchips.Count > 0 && f.Generators.Count > 0;
        }

        static bool isFloorValid(Floor f)
        {
            return f.Microchips.TrueForAll(c => f.Generators.Contains(c.Replace("-M", "-G"))) || f.Generators.Count == 0;
        }
    }
}
