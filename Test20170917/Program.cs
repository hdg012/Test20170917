using HDG.FrameWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test20170917
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("请选择\nA、Insert    B、Update    C、Delete    D、Read\n");
            string input = string.Empty;
            string sql = string.Empty;
            int result = -1;
            bool read = false;
            while (true)
            {
                read = false;
                input = Console.ReadLine().ToUpper().Trim();
                switch (input)
                {
                    case "A":
                        sql = @"INSERT INTO TEST1 VALUES (SYS_GUID(), 'A' || SYSDATE, NULL, TO_CHAR(SYSDATE, 'ss'), SYS_GUID())";
                        break;
                    case "B":
                        sql = @"UPDATE TEST1 T SET T.NAME = 'B' || TO_CHAR(SYSDATE, 'ss') WHERE ROWNUM < 3";
                        break;
                    case "C":
                        sql = @"DELETE TEST1 T WHERE T.NAME LIKE '%B%'";
                        break;
                    case "D":
                        sql = @"SELECT COUNT(1) FROM TEST1";
                        read = true;
                        break;
                    default:
                        sql = string.Empty;
                        break;
                }
                if (string.IsNullOrEmpty(sql))
                {
                    Console.WriteLine("输入无效，请重新输入\nA、Insert    B、Update    C、Delete    D、Read\n");
                }
                else
                {
                    Console.WriteLine("输入有效，结果如下：");
                    result = read ? OracleHelper.ExecuteQuery(sql) : OracleHelper.ExecuteNonQuery(sql);
                    Console.WriteLine("结果为：" + result);
                    if (result < 0)
                    {
                        Console.WriteLine("异常为：" + OracleHelper.err);
                    }
                    Console.WriteLine("\n请再次选择\nA、Insert    B、Update    C、Delete    D、Read\n");
                }
            }
        }
    }
}
