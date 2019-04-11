using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
            int ram;
            int sizeS, soft;
            int start, end;

            do
            {
                Console.WriteLine("==========MENU==========\n" +
                    "1. Add computer\n" +
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
                    /*Unfortunately perator ?? doesnt work with int and <null> together so I used if statements*/
                    case 1:
                        Console.WriteLine("1. Add Prototype computer\n2. Add default computers for all empty slots");
                        int console = Convert.ToInt32(Console.ReadLine());

                        if (console == 2)
                        {
                            for (int i = 0; i < Computers.Length; i++)
                            {
                                if (Computers[i] == null)
                                {
                                    //Possible default computer:
                                    Computers[i] = new Computer(i + 1, false, 500, 8000, null);
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
                                    Console.WriteLine("New computer: " + Computers[i].Id);
                                    //no break; if we want to add only one default computer
                                    break;
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
                        if (input == "null")
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
                        if (ram < 1000)
                        {
                            Console.WriteLine("RAM was set to 1000");
                        }


                        Console.WriteLine("Extra software? Enter null to skip this option or " +
                            "1 to procedure");
                        input = Console.ReadLine();
                        if (input == "null")
                        {
                            Computers[id - 1].Software = null;
                        }
                        else
                        {
                            sizeS = int.Parse(input);
                            Computers[id - 1].Software = new int?[5];
                            for (int i = 0; i < Computers[id - 1].Software.Length; i++)
                            {
                                Console.WriteLine("How many licences would you like for " +
                                    "Software {0}? Enter 0 if you don't want to purchase any licenses or " +
                                    "null to skip the installation", (i));
                                input = Console.ReadLine();

                                //Computers[id - 1].Software[i] = soft??null;
                                if (input == "null")
                                {
                                    Computers[id - 1].Software[i] = null;
                                }
                                else
                                {
                                    soft = int.Parse(input);
                                    Computers[id - 1].Software[i] = soft;
                                }

                            }
                        }

                        break;

                    case 3:
                        Console.WriteLine("Enter the id of computer:");
                        id = Convert.ToInt32(Console.ReadLine());

                        if (Computers[id - 1] != null)
                        {
                            Computers[id - 1] = null;
                            Console.WriteLine(id + " was deleted");
                        }
                        else
                            Console.WriteLine("Not existing computer");

                        break;

                    case 4:
                        int maxCU = 16000;
                        Console.WriteLine("Set to max? true or false");
                        bool cloudUpgrade = Convert.ToBoolean(Console.ReadLine());
                        //using syntactic sugar
                        Console.WriteLine(DoubleIntNotPastMax(ref CloudStorage, maxCU, cloudUpgrade) ?
                        "True. The amount is less than 16000" :
                        "Result false, amount of cloud storage was exceeded");

                        break;


                    case 5:
                        int minCU = 500;
                        Console.WriteLine("Set to min? true or false");
                        bool cloudDowngrade = Convert.ToBoolean(Console.ReadLine());
                        Console.WriteLine(HalveValueNotPastMin(ref CloudStorage, minCU, cloudDowngrade) ?
                            "Cloud Storage was downgraded by /2" :
                            "False result");
                        break;

                    case 6:
                        int maxNS = 250000;
                        Console.WriteLine("Set to max? true or false");
                        bool networkUpgrade = Convert.ToBoolean(Console.ReadLine());
                            Console.WriteLine(DoubleIntNotPastMax(ref NetworkSpeed, maxNS, networkUpgrade)?
                                "True. The amount is less than 250000":
                                "Result false, amount was exceeded");
                        break;

                    case 7:
                        int minNS = 10000;
                        Console.WriteLine("Set to min? true or false");
                        bool networkDowngrade = Convert.ToBoolean(Console.ReadLine());
                            Console.WriteLine(HalveValueNotPastMin(ref NetworkSpeed, minNS, networkDowngrade)?
                                "Network Speed was downgraded by /2":
                                "False result");
                        break;

                    case 8:
                        Console.WriteLine("Enter the id of computer:");
                        id = Convert.ToInt32(Console.ReadLine());
                        if (Computers[id - 1] == null)
                        {
                            Computers[id - 1] = new Computer(id, false, 500, 8000, null);
                            Console.WriteLine(Computers[id - 1].ToString());
                        }
                        else Console.WriteLine(Computers[id - 1].ToString());

                        break;

                    case 9:
                        if (!Computers.Contains(null))
                        {
                            /*RAM info:*/
                            var rm = Computers.Where(y => y != null).Select(r => r.Ram).ToList();
                            Console.WriteLine("Average RAM: " + string.Format("{0:N4}", rm.Average()));
                            
                            /*Antennas info*/
                            var ca = Computers.Where(y => y != null).Select(r => r.Antenna).ToList();
                            AntennasInfo(ca);                

                            /*Hard drive info:*/
                            var hd = Computers.Where(y => y != null).Select(l => l.StorageCapacity).ToList();
                            Console.WriteLine("Average Hard Drive Capacity: " + string.Format("{0:N6}", hd.Average()));
                        }
                        else Console.WriteLine("No computers were added");

                        /*Next, Software info:*/
                        //picking new list with for software arrays of computers where software array was not null.
                        //not sure if it's necessary and I think there should be a more efficient way, but I wanted to use linq. 
                        var aS = Computers.Where(y => y != null).Select(l => l.Software).ToList();
                        SoftwareAllAverageInfo(aS);


                        /*Average number of licenses for each individual program on machines where that
                        program is installed.
                        For program 1, find all machines where it is installed and get the average number of licenses. 
                        Repeat for program 2*/
                        SoftwareInfoForEachPr(aS);
                        

                        Console.WriteLine("Cloud Storage {0}\nNetwork Speed {1}", CloudStorage, NetworkSpeed);
                        break;

                    case 10:
                        //Here, I'm counting all Computers array length since the instructions say to include 
                        //prototype computer info if there is no user's computer info was entered
              
                        Console.WriteLine("Please specify an ARRAY start and stop index (between 0 and {0})" +
                            "of computers you would like to retrieve info from", Computers.Length-1);
                        Console.WriteLine("Start from:");
                        start = Convert.ToInt32(Console.ReadLine());
                        do
                        {
                            Console.WriteLine("End at:");
                            end = Convert.ToInt32(Console.ReadLine());
                        } while (end > Computers.Length-1 || end < start);
                        int diff = end - start+1;
                        var newCompList = new Computer[diff].ToArray();
                        Array.Copy(Computers, start, newCompList, 0, diff);
                        // source,source-index,dest,dest-index,count

                        for (int i = 0; i < newCompList.Length; i++)
                        {
                            if (newCompList[i] == null)
                            {
                                //Possible default computer:
                                newCompList[i] = new Computer(i + 1, false, 500, 8000, null);
                            }
                        }

                        var nrm = newCompList.Where(y => y != null).Select(r => r.Ram).ToList();
                        Console.WriteLine("Average RAM " + string.Format("{0:N4}", nrm.Average()));

                        var nca = newCompList.Where(y => y != null).Select(r => r.Antenna).ToList();
                        AntennasInfo(nca);

                        var nhd = newCompList.Where(y => y != null).Select(l => l.StorageCapacity).ToList();
                        Console.WriteLine("Average Hard Drive Capacity " +string.Format("{0:N6}", nhd.Average()));

                        /*Software info for all average*/
                        var aS2 = newCompList.Where(y => y != null).Select(l => l.Software).ToList();
                        SoftwareAllAverageInfo(aS2);

                        /*Software info per each program*/
                        SoftwareInfoForEachPr(aS2);

                        /*Storage and Speed info*/
                        Console.WriteLine("Cloud Storage {0}\nNetwork Speed {1}", CloudStorage, NetworkSpeed);
                        break;
                    default:
                        Environment.Exit(0);
                        break;
                }
            } while (num != 0);
        }

        private static void SoftwareInfoForEachPr(List<int?[]> aS)
        {
            int licensedPerEach, computersWithSoft, aver;
            //each software loop (total 5)
            for (int i = 0; i < 5; i++)
            {
                licensedPerEach = 0;
                computersWithSoft = 0;
                aver = 0;

                //each computer with not null software array loop
                for (int y = 0; y < aS.Count(); y++)
                {
                    if (aS[y].ElementAt(i) > 0)
                    {
                        licensedPerEach += (int)aS[y].ElementAt(i);
                        computersWithSoft++;
                    }
                    else continue;
                }
                if (computersWithSoft > 0)
                {
                    aver = licensedPerEach / computersWithSoft;
                    Console.WriteLine("Software {0} average: {1}", i + 1, aver);
                }
                else Console.WriteLine("No Licences for Software {0} found", i + 1);
            }
        }

        private static void SoftwareAllAverageInfo(List<int?[]> aS)
        {
            aS.RemoveAll(y => y == null); // Necessary to do this since "aS" list still contains nulls 
            int totalLicensed = 0;
            for (int i = 0; i < aS.Count(); i++)
            {
                var licensed = from n in aS[i]
                               where n > 0
                               select n;

                totalLicensed += (int)licensed.Sum();
            }
            if (aS.Count() > 0)
            {
                Console.WriteLine("Average Licensed Software for all programs: " + totalLicensed / aS.Count);
            }
            else
                Console.WriteLine("No Licensed Programs found");
        }

        private static void AntennasInfo(List<bool?> ca)
        {
            double percent;
            int yesAntenna;
            int noAntenna;
            ca.RemoveAll(y => y == null);
            yesAntenna = ca.Where(c => (bool)c).Count();
            noAntenna = ca.Where(c => !(bool)c).Count();
            Console.WriteLine("Amount of installed antennas {0}\nNot installed antennas {1}", yesAntenna, noAntenna);
            noAntenna = (int)(0.5f + ((100f * noAntenna) / ca.Count()));
            percent = 100 - noAntenna;
            Console.WriteLine("Percentage of installed antennas: %{0}", percent);
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
