using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asgn_02
{
    class Computer
    {
        readonly int id;
        bool? antenna;
        double? storage_capacity;
        int ram;
        int?[] software;

        public int Id { get { return id; } }
        public bool? Antenna
        {
            get { return antenna; }
            set { antenna = value; }
        }

        public int?[] Software
        {
            get { return software; }
            set { software = value; }
        }

        public double? StorageCapacity
        {
            get { return storage_capacity; }
        
            set
            {
                if (value >= 0)
                {
                    storage_capacity = value;
                }
                else if (value == null)
                {
                    storage_capacity = null;
                }
                else
                    storage_capacity = 0;
                    //throw new ArgumentOutOfRangeException("The value of storage capacity is negative");
            }
        }
        public int Ram
        {
            set
            {
                if (value >= 1000)
                {
                    ram = value;
                }
                else
                    ram = 1000;
                   // throw new ArgumentOutOfRangeException("The value of ram must be 1000+");
            }
            get
            {
                int newRam = ram;
                if (antenna == true)
                {
                    newRam += 100;
                }
                else if (antenna == false)
                {
                    newRam += 50;
                }

                if (software != null)
                {

                 /*int softCount = software.Count(r => r> 0);
                    for (int i = 0; i < softCount; i++) ;*/
                    for (int i =0; i< software.Length; i++)
                    {
                        if (software[i] > 0)
                        {
                            newRam += 10 * (int)software[i];
                        }
                    }

                }
                

                return newRam;
            }
            /*The getter should always return an adjusted amount that deducts "unavailable" memory that is in use by the OS. 
             * This amount should be 100 if the device has a cellular antenna, 50 if it does not, 
             * and then an additional 10 for every piece of licensed software that is installed on the machine.*/

        }


          public Computer(int id, bool? antenna, double? storage_capacity, int ram, int?[] software)
          {
              this.id = id;
              this.antenna = antenna;
              this.storage_capacity = storage_capacity;
              this.ram = ram;
              this.software = software;
          }

  

        public override string ToString()
        {
            return "Computer id: " + id + "\nAntenna: " + Antenna + "\nStorage Capacity: " + storage_capacity + "\nRAM: " + Ram;
        }
    }
}
