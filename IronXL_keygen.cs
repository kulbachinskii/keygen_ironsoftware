using System;
using System.Linq;

namespace ironsoftware_license_generator
{
    class Program
    {
        static void Main()
        {
            var rnd = new Random();
            var key_array = new string[8]; // Key consists of 8 fields, delimited with '-'
            key_array[1] = rnd.Next().ToString(); // Random seed
            key_array[7] = rnd.Next().ToString(); // Random seed
            key_array[0] = "IRONSTUDIO"; // Software name: "IRONXL", "IRONSTUDIO"

            // Date is encoded by reversing chars of hex-representation DateTime.Ticks 
            // Prefix: "TEx" (Trial License), "UEx" and "NEx"
            key_array[6] = "UEx" + new string(DateTime.Now.AddYears(50).Ticks.ToString("X").Reverse<char>().ToArray<char>());

            string read = Program.Hash1(key_array[0] + "@" + key_array[6] + "@" + key_array[1] + "@" + key_array[7]);
            key_array[2] = read;

            string str4 = read + "-" + Program.Hash2(read);
            key_array[3] = str4.Split('-')[1];

            string str5 = str4 + "-" + Program.Hash3(str4 + key_array[1]);
            key_array[4] = str5.Split('-')[2];

            string str6 = str5 + "-" + Program.Hash4(key_array[0] + Program.Hash1(str5 + key_array[1] + (string.Compare(key_array[1], "5") == -1 ? str5 : key_array[6])));
            key_array[5] = str6.Split('-')[3];

            // Print joined fields
            Console.WriteLine(string.Join("-", key_array));
            Console.ReadKey();
        }

        // Custom hash functions
        static string Hash1(string read)
        {
            ulong num = 3071457345618256791;
            for (int index = 0; index < read.Length; ++index)
                num = (num + (ulong)read[index]) * 3074157345618158799UL;
            return (num.ToString() + "73456AA").Substring(0, 6);
        }

        static string Hash4(string read)
        {
            ulong num = 3154457345728256791;
            for (int index = 0; index < read.Length; ++index)
                num = (num + (ulong)read[index]) * 3071457345620258791UL;
            return (num.ToString("X") + "EE01EE01").Substring(0, 6);
        }

        static string Hash3(string read)
        {
            ulong num = 74457345628256792;
            for (int index = 0; index < read.Length; ++index)
                num = (num + (ulong)read[index]) * 3071457345618258899UL;
            return (num.ToString("X") + "8800AAEE99").Substring(0, 9);
        }

        static string Hash2(string read)
        {
            ulong num = 3174657345628256791;
            for (int index = 0; index < read.Length; ++index)
                num = (num + (ulong)read[read.Length - 1 - index]) * 3024457345618158799UL;
            return (num.ToString("X") + "8256A").Substring(0, 7);
        }

    }
}
