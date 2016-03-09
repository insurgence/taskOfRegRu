using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CheckValidEmail
{
    public static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}

namespace taskOfRegRu
{
    public class Program
    {
        static void Main(string[] args)
        {
            bool   condition;
            uint   invalid = 0;
            string str     = "";
            string[] res;

            Dictionary<string, int> domains = new Dictionary<string, int>();

            StreamReader fs = new StreamReader(@"C:\emails.ini");

            while(str != null)
            {
                str = fs.ReadLine();
                condition = CheckValidEmail.IsValidEmail(str);

                if(condition)
                {
                    res = str.Split('@');
                    if (domains.ContainsKey(res[1]))
                        domains[res[1]] += 1;
                    else
                        domains.Add(res[1], 1);
                }
                else
                {
                    ++invalid;
                }

            }

            domains.OrderByDescending(x => x.Value).ToList().
                ForEach(x => Console.WriteLine("{0} : {1}", x.Key, x.Value));

            Console.WriteLine("Invalid : {0}", invalid);
            Console.ReadLine();
        } 
    }
}
