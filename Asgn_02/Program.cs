using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Asgn_02
{
    class Program
    {
        private static int CloudStorage = 500;
        private static int NetworkSpeed = 10000;
        private static Computer[] Computers;
        private static int num;

        static void Main(string[] args)
        {
            MaxComputers();
            Menu();
           
            Console.ReadKey(true);
        }

        private static void MaxComputers()
        {
            Console.WriteLine("Hi. Please enter maximum amount of computers to track (between 5 and 20)");
            num = Convert.ToInt32(Console.ReadLine());
            if(!GetIntFromUser(ref num, 5, 20, 10))
            {
                Console.WriteLine("Wrong amount entered. The defaust amount 10 was set");
            }
        }

        private static void Menu()
        {
            string input;
            bool? antenna;
            double? storage_capacity;
            double outSC;
            int ram;
            int?[] software;
            int sizeS, soft;

            do
            {
                Console.WriteLine("1. Add computer\n" +
                    "2. Specify Prototype computer\n" +
                    "3. Remove Prototype computer\n" +
                    "4. Upgrade cloud storage\n" +
                    "5. Downgrade cloud storage\n" +
                    "6. Upgrade Network Speed\n" +
                    "7. Downgrade Network Speed\n" +
                    "8. Summary about computer (by array index)\n" +
                    "9. Statistics of all computers\n" +
                    "10.Statistics with array location\n" +
                    "0. Exit");

                num = Convert.ToInt32(Console.ReadLine());

                switch (num)
                {
                    case 1:
                        Console.WriteLine("1. Add Prototype computer\n2. Add default computers");
                        int console = Convert.ToInt32(Console.ReadLine());
                        if (console == 2)
                        {
                            for (int i = 0; i < Computers.Length; i++)
                            {
                                if (Computers[i] == null)
                                {
                                    //Possible default computer:
                                    Computers[i] = new Computer(i+1, false, 500, 8000, null);
                                }

                            }
                        }
                        else
                        {
                            for (int i = 0; i < Computers.Length; i++)
                            {
                                if (Computers[i] == null)
                                {
                                    //The way of implementing depends on constructor in Computer
                                    // Computers[i] = new Computer(i + 1, null, null, 1000, null);
                                    
                                    Computers[i] = new Computer(i + 1, null, null, 1000, null);
                                    Console.WriteLine("New computer: " + Computers[i].Id);// + " " + Computers[i].Ram);
                                                                                          //break; 
                                    break;                                                    // getting a rid of break so there will be just created all instances of Computer object in whole array.
                                                                                              // doing this because instruction #8 says:
                                                                                              /* If the array does not yet have a computer at that location, you should return the information about your Prototype computer,
                                                                                               regardless of whether or not the user has specified their own prototype*/

                                }
                            }
                        }

                        break;
                    case 2:
                        
                        Console.WriteLine("Enter the id of computer:");
                        int id = Convert.ToInt32(Console.ReadLine());

                        //Suppose user enters id of existing computer correctly 

                        Console.WriteLine("Antenna to be added? true/false or null to skip");
                        input = Console.ReadLine();
                        if (input == "null")
                        {
                            Computers[id - 1].Antenna = null;
                        }
                        else
                        {
                            antenna = bool.Parse(input);
                            Computers[id - 1].Antenna = antenna;
                        }
                        

                        Console.WriteLine("Storage capacity? Enter null to skip or enter the size of SSD/HDD");
                        input = Console.ReadLine();
                        if (input=="null")
                        {
                            Computers[id - 1].StorageCapacity = null;
                        }
                        else {
                                storage_capacity = double.Parse(input);
                                Computers[id - 1].StorageCapacity = storage_capacity;
                        }



                        Console.WriteLine("RAM. Must be >= 1000");
                        ram = Convert.ToInt32(Console.ReadLine());
                        Computers[id - 1].Ram = ram;


                        Console.WriteLine("Extra software? Enter null to skip this option or " +
                            "number from 1 to 5 for amount of sotware.");
                        input = Console.ReadLine();
                        if (input=="null")
                        {
                            Computers[id - 1].Software = null;
                        }
                        else
                        {
                                sizeS = int.Parse(input);
                                Computers[id - 1].Software = new int?[5];
                                for (int i = 0; i< sizeS; i++)
                                {
                                Console.WriteLine((i + 1) + " is installed or not? 1 - yes, 0 - no");
                                soft = Convert.ToInt32(Console.ReadLine());
                                Computers[id - 1].Software[i] = soft;
                            }

                        }
                     
                        break;

                    case 3:
                        Console.WriteLine("Enter the id of computer:");
                        id = Convert.ToInt32(Console.ReadLine());

                        if(Computers[id-1] != null)
                        {
                            Computers[id - 1] = null;
                            Console.WriteLine(id+" was deleted");
                        }
                        else
                            Console.WriteLine("Not existing computer");

                        break;

                    case 4:
                        int maxCU = 16000;
                        Console.WriteLine("Set to max? true or false");
                        bool cloudUpgrade = Convert.ToBoolean(Console.ReadLine());
                        if (DoubleIntNotPastMax(ref CloudStorage, maxCU, cloudUpgrade))
                        {
                            Console.WriteLine("True. The amount is less than 16000");
                        }
                        else
                            Console.WriteLine("Result false, amount of cloud storage was exceeded");

                        break;


                    case 5:
                        int minCU = 500;
                        Console.WriteLine("Set to min? true or false");
                        bool cloudDowngrade = Convert.ToBoolean(Console.ReadLine());
                        if (HalveValueNotPastMin(ref CloudStorage, minCU, cloudDowngrade))
                        {
                            Console.WriteLine("Cloud Storage was downgraded by /2");
                        }
                        else
                            Console.WriteLine("False result");
                        break;

                    case 6:
                        int maxNS = 250000;
                        Console.WriteLine("Set to max? true or false");
                        bool networkUpgrade = Convert.ToBoolean(Console.ReadLine());
                        if (DoubleIntNotPastMax(ref NetworkSpeed, maxNS, networkUpgrade))
                        {
                            Console.WriteLine("True. The amount is less than 250000");
                        }
                        else
                            Console.WriteLine("Result false, amount was exceeded");

                        break;

                    case 7:
                        int minNS = 10000;
                        Console.WriteLine("Set to min? true or false");
                        bool networkDowngrade = Convert.ToBoolean(Console.ReadLine());
                        if (HalveValueNotPastMin(ref NetworkSpeed, minNS, networkDowngrade))
                        {
                            Console.WriteLine("Network Speed was downgraded by /2");
                        }
                        else
                            Console.WriteLine("False result");

                        break;

                    case 8:
                        Console.WriteLine("Enter the id of computer:");
                        id = Convert.ToInt32(Console.ReadLine());
                        if (Computers[id - 1] == null)
                        {
                            Computers[id-1] = new Computer(id, false, 500, 8000, null);
                            Console.WriteLine(Computers[id - 1].ToString());
                        }
                        else Console.WriteLine(Computers[id-1].ToString());

                        break;

                    case 9:
                        var rm = Computers.Where(y => y != null).Select(r => r.Ram).ToList();
                        Console.WriteLine("Average RAM "+ rm.Average().ToString());

                        var ca = Computers.Where(y => y != null).Select(r => r.Antenna).ToList();
                        int yesAntenna = ca.Where(c =>(bool) c).Count();
                        int noAntenna = ca.Where(c => !(bool)c).Count();
                        Console.WriteLine("Amount of installed antennas {0}\nNot installed antennas {1}", yesAntenna, noAntenna);
                        //double percent = (yesAntenna / ca.Count) * 100;
                        //Console.WriteLine("Percentage of installed antennas: %{0}", percent);

                        var hd = Computers.Where(y => y != null).Select(l => l.StorageCapacity).ToList();
                        Console.WriteLine("Average Hard Drive Capacity " + hd.Average().ToString());
                        // Console.WriteLine("Average Software " + Computers.Select(a => a.Software).Average().ToString());

                        Console.WriteLine("Cloud Storage {0}\nNetwork Speed {1}", CloudStorage, NetworkSpeed);
                        break;

                    default:
                        Environment.Exit(0);
                        break;
                }
            } while (num != 0);
        }


        public static bool GetIntFromUser(ref int num, int min, int max, int defaultVal)
        {
            if (num < min || num > max)
            {
                Console.WriteLine("Invalid number. The amount was set to 10.");
                Computers = new Computer[defaultVal];
                return false;
            }
            else Computers = new Computer[num];
            return true;
        }

        
        public static bool DoubleIntNotPastMax(ref int num, int max, bool setToMax)
        {
            if (num * 2 <= max)
            {
                num = num * 2;
                return true;
            }
            else
            {
                if (setToMax)
                {
                    num = max;
                    return false;
                }
                else 
                    return false;
            }
            
        }

        public static bool HalveValueNotPastMin(ref int num, int min, bool setToMin)
        {
            if (num / 2 < min)
            {
                if (setToMin)
                {
                    num = min;
                    return false;
                }
                else
                    return false;
            }
            else
            {
                num = num / 2;
                return true;
            }
        }
    }
}
