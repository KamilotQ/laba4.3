using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace L4_3
{
    class TelephoneDirectory
    {
        private string[] LastName = new string[Sizing() - 1];
        private string[] Name = new string[Sizing() - 1];
        private string[] Surname = new string[Sizing() - 1];
        private string[] Address = new string[Sizing() - 1];
        private string[] TelephoneNumber = new string[Sizing() - 1];
        private int[] NumberTelephone = new int[Sizing() - 1];
        public string Heading;
        public TelephoneDirectory()
        {

        }
        public TelephoneDirectory(string lastname, string name, string surname, string address, string telephonenumber)
        {
            if (lastname.Length == 0) throw new System.Exception("Last name is not found.");
            if (name.Length == 0) throw new System.Exception("Name is not found.");
            if (surname.Length == 0) throw new System.Exception("Surname is not found.");
            if (address.Length == 0) throw new System.Exception("Address is not found.");
            if (telephonenumber.Length == 0) throw new System.Exception("Telephone number is not found.");
        }
        public string[] GetLastName
        {
            get
            {
                return LastName;
            }
            set
            {
                LastName = value;
            }
        }
        public string[] GetName
        {
            get
            {
                return Name;
            }
            set
            {
                Name = value;
            }
        }
        public string[] GetSurname
        {
            get
            {
                return Surname;
            }
            set
            {
                Surname = value;
            }
        }
        public string[] GetAddress
        {
            get
            {
                return Address;
            }
            set
            {
                Address = value;
            }
        }
        public string[] GetTelephoneNumber
        {
            get
            {
                return TelephoneNumber;
            }
            set
            {
                TelephoneNumber = value;
            }
        }
        public int[] GetNumberTelephone
        {
            get
            {
                return NumberTelephone;
            }
            set
            {
                NumberTelephone = value;
            }
        }
        public string GetHeading
        {
            get
            {
                return Heading;
            }
            set
            {
                Heading = value;
            }
        }
        public static int Sizing()
        {
            var fileStream = new FileStream(@"text.txt", FileMode.Open);
            using var f = new StreamReader(fileStream);
            string mains = f.ReadToEnd();
            string[] lines = mains.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            int size = lines.Length;
            f.Close();
            return size;
        }
        public void Opening()
        {
            var fileStream = new FileStream(@"text.txt", FileMode.Open);
            using var f = new StreamReader(fileStream);
            string mains = f.ReadToEnd();
            string[] lines = mains.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            GetHeading = lines[0];
            int count = lines.Length;
            int t = 0;
            for (int i = 1; i < count; i++)
            {
                if (lines[i].Length > t) t = lines[i].Length;

            }
            char[,] temps = new char[t, count - 1];
            for (int i = 1; i < count; i++)
            {
                char[] tem = lines[i].ToCharArray();
                for (int j = 0; j < lines[i].Length; j++)
                {
                    temps[j, i - 1] = tem[j];
                }
            }
            bool maybe = false;
            bool start = false;
            bool go = false;
            bool yes = false;
            string lass = "";
            string namm = "";
            string surr = "";
            string adrr = "";
            string tell = "";
            string numm = "";

            for (int i = 1; i < count; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (!maybe)
                    {
                        if (temps[j, i - 1] == '\t')
                        {
                            maybe = true;
                            j += 1;
                        }
                        else if (!(char.IsControl(temps[j, i - 1]))&&!(char.IsNumber(temps[j, i - 1]))) lass = lass + temps[j, i - 1];

                    }
                    else if (!start)
                    {

                        if (temps[j, i - 1] == '\t')
                        {
                            start = true;
                            j += 1;
                        }
                        else if (!(char.IsControl(temps[j, i - 1])) && !(char.IsNumber(temps[j, i - 1]))) namm = namm + temps[j, i - 1];
                    }
                    else if (!go)
                    {
                        if (temps[j, i - 1] == '\t')
                        {
                            go = true;
                            j += 1;
                        }
                        else if (!(char.IsControl(temps[j, i - 1])) && !(char.IsNumber(temps[j, i - 1]))) surr = surr + temps[j, i - 1];
                    }
                    else if (!yes)
                    {
                        if (temps[j, i - 1] == '\t')
                        {
                            yes = true;
                            j += 1;
                        }
                        else adrr = adrr + temps[j, i - 1];

                    }
                    else
                    {
                        if (char.IsNumber(temps[j, i - 1])||(temps[j, i - 1]=='-')) tell = tell + temps[j, i - 1];
                        if (char.IsNumber(temps[j, i - 1])) numm = numm + temps[j, i - 1];
                        if (j == lines[i].Length - 1)
                        {
                            GetLastName[i - 1] = lass;
                            GetName[i - 1] = namm;
                            GetSurname[i - 1] = surr;
                            GetAddress[i - 1] = adrr;
                            GetTelephoneNumber[i - 1] = tell;
                            GetNumberTelephone[i - 1] = Int32.Parse(numm);
                            lass = "";
                            namm = "";
                            surr = "";
                            adrr = "";
                            tell = "";
                            numm = "";
                            maybe = false;
                            start = false;
                            go = false;
                            yes = false;
                            break;
                        }
                    }
                }
            }
            Console.WriteLine(Heading);
            for (int i = 0; i < GetLastName.Length; i++)
            {
                Console.WriteLine(GetLastName[i] + "\t\t" + GetName[i] + "\t\t" + GetSurname[i] + "\t\t" + GetAddress[i] + "\t\t" + GetTelephoneNumber[i]);

            }
        }
        public void Adding(string lastN, string N, string surN, string addR, string tellN)
        {
            var fileStream = new FileStream(@"text.txt", FileMode.Truncate);
            using var f = new StreamWriter(fileStream);
            Array.Resize(ref LastName, LastName.Length + 1);
            Array.Resize(ref Name, Name.Length + 1);
            Array.Resize(ref Surname, Surname.Length + 1);
            Array.Resize(ref Address, Address.Length + 1);
            Array.Resize(ref TelephoneNumber, TelephoneNumber.Length + 1);
            Array.Resize(ref NumberTelephone, NumberTelephone.Length + 1);
            GetLastName[GetLastName.Length - 1] = lastN;
            GetName[GetName.Length - 1] = N;
            GetSurname[GetSurname.Length - 1] = surN;
            GetAddress[GetAddress.Length - 1] = addR;
            GetTelephoneNumber[GetTelephoneNumber.Length - 1] = tellN;
            char[] som = tellN.ToCharArray();
            tellN = "";
            for (int i = 0; i < som.Length; i++)
            {
                if (char.IsNumber(som[i])) tellN += som[i];
            }
            GetNumberTelephone[GetNumberTelephone.Length - 1] = Int32.Parse(tellN);
            Console.WriteLine(Heading);
            f.WriteLine(Heading);
            for (int i = 0; i < GetLastName.Length; i++)
            {
                if (i == GetLastName.Length - 1)
                {
                    f.Write(GetLastName[i] + "\t\t" + GetName[i] + "\t\t" + GetSurname[i] + "\t\t" + GetAddress[i] + "\t\t" + GetTelephoneNumber[i]);
                }
                else f.WriteLine(GetLastName[i] + "\t\t" + GetName[i] + "\t\t" + GetSurname[i] + "\t\t" + GetAddress[i] + "\t\t" + GetTelephoneNumber[i]);
                Console.WriteLine(GetLastName[i] + "\t\t" + GetName[i] + "\t\t" + GetSurname[i] + "\t\t" + GetAddress[i] + "\t\t" + GetTelephoneNumber[i]);
            }
            f.Close();
        }
        public int Searching(string l)
        {
            int n = -1;
            for (int i = 0; i < GetLastName.Length; i++)
            {
                if (GetLastName[i].Contains(l)||(GetLastName[i]==l)) n = i;
            }
            if (n >= 0)
            {
                Console.WriteLine("________________________________________________________________________________________________________________________________________________________\n");
                Console.WriteLine(GetLastName[n] + "\t\t" + GetName[n] + "\t\t" + GetSurname[n] + "\t\t" + GetAddress[n] + "\t\t" + GetTelephoneNumber[n]);
                Console.WriteLine("________________________________________________________________________________________________________________________________________________________\n");
            }
            else Console.WriteLine("The last name does not exist in the file.\n");
            return n;
        }
        public void Editing(int n, string lastN, string N, string surN, string addR, string tellN)
        {
            var fileStream = new FileStream(@"text.txt", FileMode.Truncate);
            using var f = new StreamWriter(fileStream);
            if (lastN.Length == 0) throw new System.Exception("Last name is not found.");
            if (N.Length == 0) throw new System.Exception("Name is not found.");
            if (surN.Length == 0) throw new System.Exception("Surname is not found.");
            if (addR.Length == 0) throw new System.Exception("Address is not found.");
            if (tellN.Length == 0) throw new System.Exception("Telephone number is not found.");
            LastName[n] = lastN;
            Name[n] = N;
            Surname[n] = surN;
            Address[n] = addR;
            TelephoneNumber[n] = tellN;
            char[] som = tellN.ToCharArray();
            tellN = "";
            for (int i = 0; i < som.Length; i++)
            {
                if (char.IsNumber(som[i])) tellN += som[i];
            }
            GetNumberTelephone[n] = Int32.Parse(tellN);
            Console.WriteLine(Heading);
            f.WriteLine(Heading);
            for (int i = 0; i < LastName.Length; i++)
            {
                if (i == GetLastName.Length - 1)
                {
                    f.Write(GetLastName[i] + "\t\t" + GetName[i] + "\t\t" + GetSurname[i] + "\t\t" + GetAddress[i] + "\t\t" + GetTelephoneNumber[i]);
                }
                else f.WriteLine(GetLastName[i] + "\t\t" + GetName[i] + "\t\t" + GetSurname[i] + "\t\t" + GetAddress[i] + "\t\t" + GetTelephoneNumber[i]);
                Console.WriteLine(GetLastName[i] + "\t\t" + GetName[i] + "\t\t" + GetSurname[i] + "\t\t" + GetAddress[i] + "\t\t" + GetTelephoneNumber[i]);
            }
            f.Close();
        }
        public void Deleting(int n)
        {
            var fileStream = new FileStream(@"text.txt", FileMode.Truncate);
            using var f = new StreamWriter(fileStream);
            string[] a = GetLastName;
            string[] ar = GetName;
            string[] arr = GetSurname;
            string[] arrr = GetAddress;
            string[] arrrr = GetTelephoneNumber;
            int[] min = GetNumberTelephone;
            bool start = false;
            for (int i = 0; i < GetLastName.Length; i++)
            {
                if (i == n) start = true;
                if (i == GetLastName.Length - 1)
                {
                    Array.Resize(ref a, a.Length - 1);
                    Array.Resize(ref ar, ar.Length - 1);
                    Array.Resize(ref arr, arr.Length - 1);
                    Array.Resize(ref arrr, arrr.Length - 1);
                    Array.Resize(ref arrrr, arrrr.Length - 1);
                    Array.Resize(ref min, min.Length - 1);
                    GetLastName = a;
                    GetName = ar;
                    GetSurname = arr;
                    GetAddress = arrr;
                    GetTelephoneNumber = arrrr;
                    GetNumberTelephone = min;
                    break;
                }
                if (start)
                {
                    GetLastName[i] = GetLastName[i + 1];
                    GetName[i] = GetName[i + 1];
                    GetSurname[i] = GetSurname[i + 1];
                    GetAddress[i] = GetAddress[i + 1];
                    GetTelephoneNumber[i] = GetTelephoneNumber[i + 1];
                    GetNumberTelephone[i] = GetNumberTelephone[i + 1];
                }
            }
            Console.WriteLine(Heading);
            f.WriteLine(Heading);
            for (int i = 0; i < GetLastName.Length; i++)
            {
                if (i == GetLastName.Length - 1)
                {
                    f.Write(GetLastName[i] + "\t\t" + GetName[i] + "\t\t" + GetSurname[i] + "\t\t" + GetAddress[i] + "\t\t" + GetTelephoneNumber[i]);
                }
                else f.WriteLine(GetLastName[i] + "\t\t" + GetName[i] + "\t\t" + GetSurname[i] + "\t\t" + GetAddress[i] + "\t\t" + GetTelephoneNumber[i]);
                Console.WriteLine(GetLastName[i] + "\t\t" + GetName[i] + "\t\t" + GetSurname[i] + "\t\t" + GetAddress[i] + "\t\t" + GetTelephoneNumber[i]);
            }
            f.Close();
        }
        public void Sorting()
        {
            var fileStream = new FileStream(@"text.txt", FileMode.Truncate);
            using var f = new StreamWriter(fileStream);
            string temp_1;
            string temp_2;
            string temp_3;
            string temp_4;
            string temp_5;
            int temp_6;
            for (int i = 0; i < GetTelephoneNumber.Length - 1; i++)
            {
                for (int j = i + 1; j < GetTelephoneNumber.Length; j++)
                {
                    if (GetNumberTelephone[i] > GetNumberTelephone[j])
                    {

                        temp_1 = GetLastName[i];
                        GetLastName[i] = GetLastName[j];
                        GetLastName[j] = temp_1;

                        temp_2 = GetName[i];
                        GetName[i] = GetName[j];
                        GetName[j] = temp_2;

                        temp_3 = GetSurname[i];
                        GetSurname[i] = GetSurname[j];
                        GetSurname[j] = temp_3;

                        temp_4 = GetAddress[i];
                        GetAddress[i] = GetAddress[j];
                        GetAddress[j] = temp_4;

                        temp_5 = GetTelephoneNumber[i];
                        GetTelephoneNumber[i] = GetTelephoneNumber[j];
                        GetTelephoneNumber[j] = temp_5;

                        temp_6 = GetNumberTelephone[i];
                        GetNumberTelephone[i] = GetNumberTelephone[j];
                        GetNumberTelephone[j] = temp_6;
                    }
                }
            }
            string ex_1 = "";
            string ex_2 = "";
            string ex_3 = "";
            string ex_4 = "";
            for (int b = 0; b < GetLastName.Length; b++)
            {
                if (GetLastName[b].Length > ex_1.Length) ex_1 = GetLastName[b];
                if (GetName[b].Length > ex_2.Length) ex_2 = GetName[b];
                if (GetSurname[b].Length > ex_3.Length) ex_3 = GetSurname[b];
                if (GetAddress[b].Length > ex_4.Length) ex_4 = GetAddress[b];
            }
            for (int b = 0; b < GetLastName.Length; b++)
            {
                if (GetLastName[b].Length != ex_1.Length)
                {
                    int dif = ex_1.Length - GetLastName[b].Length;
                    for (int a = 0; a < dif; a++) GetLastName[b] += " ";
                }
                if (GetName[b].Length != ex_2.Length)
                {
                    int dif = ex_2.Length - GetName[b].Length;
                    for (int a = 0; a < dif; a++) GetName[b] += " ";
                }
                if (GetSurname[b].Length != ex_3.Length)
                {
                    int dif = ex_3.Length - GetSurname[b].Length;
                    for (int a = 0; a < dif; a++) GetSurname[b] += " ";
                }
                if (GetAddress[b].Length != ex_4.Length)
                {
                    int dif = ex_4.Length - GetAddress[b].Length;
                    for (int a = 0; a < dif; a++) GetAddress[b] += " ";
                }
            }
            Console.WriteLine(Heading);
            f.WriteLine(Heading);
            for (int i = 0; i < GetLastName.Length; i++)
            {
                if (i == GetLastName.Length - 1)
                {
                    f.Write(GetLastName[i] + "\t\t" + GetName[i] + "\t\t" + GetSurname[i] + "\t\t" + GetAddress[i] + "\t\t" + GetTelephoneNumber[i]);
                }
                else f.WriteLine(GetLastName[i] + "\t\t" + GetName[i] + "\t\t" + GetSurname[i] + "\t\t" + GetAddress[i] + "\t\t" + GetTelephoneNumber[i]);
                Console.WriteLine(GetLastName[i] + "\t\t" + GetName[i] + "\t\t" + GetSurname[i] + "\t\t" + GetAddress[i] + "\t\t" + GetTelephoneNumber[i]);
            }
            f.Close();
        }
    }
    class Program
    {
        static void Main()
        {
            TelephoneDirectory r = new TelephoneDirectory();
            string task = "";
            string func = "";
            string la = "";
            string na = "";
            string su = "";
            string ad = "";
            string te = "";
            do
            {
                Console.WriteLine("\nEnter 'o' to open the file and 'e' to exit.");
                task = Console.ReadLine();
                switch (task)
                {
                    case "e": break;
                    case "o":
                        {
                            r.Opening();
                            Console.WriteLine("\nAfter the sorting: ");
                            r.Sorting();
                            do
                            {
                                Console.WriteLine("\n'a' to add new information, 's' to search and 'e' to exit.");
                                func = Console.ReadLine();
                                switch (func)
                                {
                                    case "e": r.Sorting();  task = "e"; break;
                                    case "a":
                                        {
                                            do
                                            {
                                                Console.WriteLine("\nEnter the last name: ");
                                                la = Console.ReadLine();
                                            } while (la.Length == 0);
                                            do
                                            {
                                                Console.WriteLine("\nEnter the name: ");
                                                na = Console.ReadLine();
                                            } while (na.Length == 0);
                                            do
                                            {
                                                Console.WriteLine("\nEnter the surname: ");
                                                su = Console.ReadLine();
                                            } while (su.Length == 0);
                                            do
                                            {
                                                Console.WriteLine("\nEnter the address: ");
                                                ad = Console.ReadLine();
                                            } while (ad.Length == 0);
                                            do
                                            {
                                                Console.WriteLine("\nEnter the telephone number(example: 2-12-33): ");
                                                te = Console.ReadLine();
                                            } while (te.Length != 7);
                                            r.Adding(la, na, su, ad, te);
                                            break;
                                        }
                                    case "s":
                                        {
                                            string last = "";
                                            string choice = "";
                                            do
                                            {
                                                Console.WriteLine("\nEnter the last name to search for: ");
                                                last = Console.ReadLine();
                                            } while (last.Length == 0);
                                            int def = r.Searching(last);
                                            if (def >= 0)
                                            {
                                                do
                                                {
                                                    Console.WriteLine("\nEnter 'e' to edit, 'd' to delete and 'r' to return.");
                                                    choice = Console.ReadLine();
                                                    switch (choice)
                                                    {
                                                        case "r": break;
                                                        case "e":
                                                            {
                                                                string ll = "";
                                                                string nn = "";
                                                                string ss = "";
                                                                string aa = "";
                                                                string tt = "";
                                                                do
                                                                {
                                                                    Console.WriteLine("\nEnter the last name to replace the previous one: ");
                                                                    ll = Console.ReadLine();
                                                                } while (ll.Length == 0);
                                                                do
                                                                {
                                                                    Console.WriteLine("\nEnter the name to replace the previous one: ");
                                                                    nn = Console.ReadLine();
                                                                } while (nn.Length == 0);
                                                                do
                                                                {
                                                                    Console.WriteLine("\nEnter the surname to replace the previous one: ");
                                                                    ss = Console.ReadLine();
                                                                } while (ss.Length == 0);
                                                                do
                                                                {
                                                                    Console.WriteLine("\nEnter the address to replace the previous one: ");
                                                                    aa = Console.ReadLine();
                                                                } while (aa.Length == 0);
                                                                do
                                                                {
                                                                    Console.WriteLine("\nEnter the telephone number to replace the previous one: ");
                                                                    tt = Console.ReadLine();
                                                                } while (tt.Length != 7);
                                                                r.Editing(def, ll, nn, ss, aa, tt);
                                                                break;
                                                            }
                                                        case "d":
                                                            {
                                                                r.Deleting(def);
                                                                choice = "r";
                                                                break;
                                                            }
                                                        default: Console.WriteLine("Try again. ('e' to edit, 'd' to delete and 'r' to return)"); break;
                                                    }
                                                } while (choice != "r");
                                            }
                                            break;
                                        }
                                    default: Console.WriteLine("Try again. ('a' to add, 's' to search and 'e' to exit)"); break;
                                }
                            } while (func != "e");
                            break;
                        }
                    default: Console.WriteLine("Try again. ('o' to open file and 'e' to exit)"); break;
                }
            } while (task != "e");
        }
    }
}

