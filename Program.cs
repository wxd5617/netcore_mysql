using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Text;
using txr.Model;

namespace txr
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            MySqlConnection con = new MySqlConnection("server=127.0.0.1;database=taoxiaoren;uid=root;pwd=zd!min123;charset='utf8'");
            ////新增数据
            //con.Execute("insert into user values(null, '测试', 'http://www.cnblogs.com/linezero/', 18)");
            ////新增数据返回自增id
            //var id = con.QueryFirst<int>("insert into user values(null, 'linezero', 'http://www.cnblogs.com/linezero/', 18);select last_insert_id();");
            ////修改数据
            //con.Execute("update user set UserName = 'linezero123' where Id = @Id", new { Id = id });
            //查询数据
            var list = con.Query<Store>("select * from store");
            foreach (var item in list)
            {
                Console.WriteLine($"用户名：{item.id} 链接：{item.name}");
            }
            //删除数据
            //con.Execute("delete from user where Id = @Id", new { Id = id });
            //Console.WriteLine("删除数据后的结果");
            //list = con.Query<Store>("select * from user");
            //foreach (var item in list)
            //{
            //    Console.WriteLine($"用户名：{item.UserName} 链接：{item.Url}");
            //}
            Console.ReadKey();
        }
    }
}
