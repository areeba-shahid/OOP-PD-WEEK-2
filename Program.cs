using pd2.BLpd2;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Runtime.InteropServices;

namespace pd2
{
    internal class Program
    {

        static void logo()
        {

            Console.WriteLine("                                    WELCOME TO ");

            Console.WriteLine("                                                                                      ");
            Console.WriteLine("                                                                                       ");
            Console.WriteLine("          ***********************************************__***************");

            Console.WriteLine("          **       ORDERING  MANAGEMENT  SYSTEM       __|__|__          **");

            Console.WriteLine("          **                                         |__|  |__|         **");

            Console.WriteLine("          *****************************************************************");

        }
        static int firstpage()
        {
            Console.WriteLine("To sign up . press 1");
            Console.WriteLine("To sign in . press 2");
            int option = int.Parse(Console.ReadLine());
            return option;
        }
        static student signup(string path, List<string> names, List<string> passes, List<string> rolls)
        {
            Console.Clear();
            logo();
            student s = new student();
           
                Console.WriteLine("-----------------sign up ----------------- ");
            int check = 1;
            while (check == 1)
            {
                Console.WriteLine("Enter Username   ");
                s.name = Console.ReadLine();
                for (int i = 0; i < names.Count; i++)
                {
                    if (s.name == names[i])
                    {
                        
                        Console.WriteLine("username already exists");
                        break;
                        
                        
                    }
                   
                    else
                    {
                       
                        check = 2;
                    }
                  
                }
            }

                names.Add(s.name);
                Console.WriteLine("Enter password   ");
                s.password = Console.ReadLine();

                passes.Add(s.password);
                Console.WriteLine("Enter roll (user or admin)  ");
                s.roll = Console.ReadLine();

                rolls.Add(s.roll);
               
            
                StreamWriter loginfile = new StreamWriter(path, true);
                loginfile.WriteLine(s.name + ',' + s.password + ',' + s.roll);
                loginfile.Flush();
                loginfile.Close();
                return s;

            }
    
        static int signin(List<string> names, List<string> passes, List<string> rolls)
        {
            Console.Clear();
            logo();
            Console.WriteLine("-----------------sign in ------------- ");
            Console.WriteLine("Enter Username   ");
            string name1 = Console.ReadLine();
           
            for (int i = 0; i < names.Count ; i++)
            {
                if (name1 != names[i])
                {


                    continue;

                }
               else
                {
                    
                    break;
                }
            }

            Console.WriteLine("Enter password    ");
            string pass1 = Console.ReadLine();

           
            for (int i = 0; i < passes.Count; i++)
            {
                if (pass1 != passes[i])
                {
                    continue;


                }
                else
                {
                 
                    break;
                }
            }
            for (int i = 0; i < rolls.Count; i++)
            {
                if (pass1 == passes[i] && name1 == names[i])
                {
                    Console.WriteLine($"You are identified as : {rolls[i]}");
                }

            }
            Console.WriteLine("To go to user menu enter 1 \n To go to admin menu enter 2 ");
            int op = int.Parse(Console.ReadLine());
            return op;
        }
        static int adminmenu()
        {
            Console.Clear();
            logo();
            Console.WriteLine("view the records of log in users ");
            Console.WriteLine("view the records of purchased products ");
           
            Console.WriteLine("To go to 1st func enter 1 \n To go to 2nd func enter 2  ");
            int op = int.Parse(Console.ReadLine());
            return op;
        }


        static string ParseData(string record, int field)
        {
            int comma = 1;
            string item = "";
            for (int i = 0; i < record.Length; i++)
            {
                if (record[i] == ',')
                {
                    comma++;
                }
                else if (comma == field)
                {
                    item = item + record[i];
                }
            }
            return item;
        }


        static void readData(string path, List<string> names, List<string> passes, List<string> rolls)
        {
            int x = 0 ;
            if (File.Exists(path))
            {
                StreamReader loginfile = new StreamReader(path);
                string record;
                while ((record = loginfile.ReadLine()) != null)
                {
                    names.Add(ParseData(record,1));
                    passes.Add( ParseData(record,2));
                    rolls.Add(ParseData(record,3));

                    x++;
                    

                }
                loginfile.Close();
            }
           
        }
        static int adminfunc1(string path)
        {
            Console.Clear();
            logo();
            if (File.Exists(path))
            {
                StreamReader loginfile = new StreamReader(path);
                string records;
                while ((records = loginfile.ReadLine()) != null)
                {
                    Console.WriteLine(records);

                }
                loginfile.Close();

            }
            else
            {
                Console.WriteLine("Not exists");
            }
            Console.WriteLine("To exit ,press 0 ");
            int op = int.Parse(Console.ReadLine());
            return op;
        }
        static int purchase(string path1items,List<string> showitems, List<int> showprice, List<int> bill, List<string> itemsclasslist , List<int> price, List<string> items)
        {
            Console.Clear();
            logo();
           
            Console.WriteLine("Select items you want to purchase");
            for(int i = 0; i < showitems.Count; i++)
            {
                Console.WriteLine(showitems[i]);
            }
            Console.WriteLine("Select items you want to purchase");
            for (int i = 0; i < showprice.Count; i++)
            {
                Console.WriteLine(showprice[i]);
            }
        
            Console.WriteLine(" How many items you want to purchase ????");
           int nofitems =  int.Parse(Console.ReadLine());
           
            string nameofitem = "";
            int priceofitem = 0;
            int sum = 0;
            for (int i = 0; i < nofitems; i++)
            {
                Console.WriteLine(" Enter name of  items u want to buy ");
                nameofitem =   Console.ReadLine();
                for (int j = 0; j < showitems.Count; j++)
                {
                    if (nameofitem != showitems[j])
                    {
                        continue;
                       
                    }
                    else
                    {
                        Console.WriteLine($"The item is of {showprice[j]}");
                        priceofitem = showprice[j];
                        items it = new items();
                        it.itemname = nameofitem;
                        items.Add(it.itemname);
                        it.itemprice = priceofitem;
                        price.Add(it.itemprice);
                        itemsclasslist.Add(it.itemname + it.itemprice);
                        StreamWriter itemsfile = new StreamWriter(path1items, true);
                        itemsfile.WriteLine(it.itemname + ',' + it.itemprice);
                        itemsfile.Flush();
                        itemsfile.Close();
                        break;

                    }
                }
                sum = sum + priceofitem;
            }
            
            Console.WriteLine($"The total bill is {sum}");
            bill.Add(sum);
            int op = int.Parse(Console.ReadLine());
            return op;
        }
        static string ParseDataitems(string record, int field)
        {
            int comma = 1;
            string item = "";
            for (int i = 0; i < record.Length; i++)
            {
                if (record[i] == ',')
                {
                    comma++;
                }
                else if (comma == field)
                {
                    item = item + record[i];
                }
            }
            return item;
        }
        static void readDataitems(string path1items, List<string> items, List<int> price)
        {
          
            if (File.Exists(path1items))
            {
                StreamReader itemsfile = new StreamReader(path1items);
                string record;
                while ((record = itemsfile.ReadLine()) != null)
                {
                    items.Add(ParseDataitems(record, 1));
                    price.Add(int.Parse(ParseDataitems(record, 2)));
                   

                  


                }
                itemsfile.Close();
            }

        }
        static int adminfunc2(string path1items)
        {
            Console.Clear();
            logo();
            if (File.Exists(path1items))
            {
                StreamReader itemsfile = new StreamReader(path1items);
                string records;
                while ((records = itemsfile.ReadLine()) != null)
                {
                    Console.WriteLine(records);

                }
                itemsfile.Close();

            }
            else
            {
                Console.WriteLine("Not exists");
            }
            Console.WriteLine("To exit ,press 0 ");
            int op = int.Parse(Console.ReadLine());
            return op;
        }
        static int usermenu()
        {
            Console.Clear();
            logo();
            Console.WriteLine("shop the products  ");
           

            Console.WriteLine("To go to 1st func enter 1 ");
            int op = int.Parse(Console.ReadLine());
            return op;
        }
        static void Main(string[] args)
        {
            string path = "C:\\Users\\Rashid ch\\OneDrive\\Documents\\semester1\\loginfile.txt";
            string path1items = "C:\\Users\\Rashid ch\\OneDrive\\Documents\\semester1\\itemsfile.txt";
            int op;

            List<string> names = new List<string>();
            List<string> passes = new List<string>();
            List<string> rolls = new List<string>();
            List<int> bill = new List<int>();
            List<string> showitems = new List<string>();
            List<int> showprice = new List<int>();
            showitems.Add("refrigrator");
            showitems.Add("LCD");
            showitems.Add("microwave");
            showitems.Add("Vacuum cleaner");
            showitems.Add("washing machine");
            showitems.Add("juicer");
            showitems.Add("blender");
            showitems.Add("Fan");
            showprice.Add(15000);
            showprice.Add(20000);
            showprice.Add(45000);
            showprice.Add(25000);
            showprice.Add(30000);
            showprice.Add(5000);
            showprice.Add(4000);
            showprice.Add(1500);
            List<string> items = new List<string>();
            List<int> price = new List<int>();
            readDataitems(path1items, items, price);
            readData(path, names, passes, rolls);
           


            List<student> records = new List<student>();

            for (int i = 0; i < records.Count; i++)
            {
                records[i] = signup(path, names, passes, rolls);
            }
            List<string> itemsclasslist = new List<string>();
           

            logo();
            signup(path, names, passes, rolls);
            op = signin(names, passes, rolls);
            if (op == 2)
            {


                while (true)
                {
                    op = adminmenu();
                    while (op != 0)
                    {
                        if (op == 1)
                        {
                            op = adminfunc1(path);

                        }
                        if (op == 2)
                        {
                            op = adminfunc2(path1items);
                        }
                       
                    }
                }
            }
            if(op == 1)
            {
                while (true)
                {
                    op = usermenu();
                    while(op != 0)
                    {
                        if (op == 1)
                        {
                            op = purchase(path1items, showitems, showprice, bill, itemsclasslist, price, items);
                        }
                    }
                }
            }
        }



        }

    }
