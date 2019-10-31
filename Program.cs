using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Text;
using txr.Model;

namespace txr
{
    class Program
    {
        static void Main(string[] args)
        {

            StreamReader sr = new StreamReader(@"G:\Users\wuxiaodong.GWMFC\Desktop\aa1.txt", Encoding.UTF8);
            int index = 1;
            string nextLine;
            Store store = new Store();
            while ((nextLine = sr.ReadLine()) != null)
            {
               
                if (index % 3 == 0)
                {
                    store.phone = nextLine.Substring(3);
                }
                else if (index % 2==0)
                {
                    store.address = nextLine;
                }
                else
                {
                    store.name = nextLine;
                    store.is_chain = "0";
                    int chnia = nextLine.IndexOf("店)") ;
                    if (chnia != -1)
                    {
                        int chnia2 = nextLine.LastIndexOf("(") ;
                        store.chain_name = nextLine.Substring(0,chnia2);
                        store.is_chain = "1";
                    }
                }

                if (index % 3 == 0)
                {
                    AddStore(store);
                    store = new Store();
                    index = 0;
                }
                index++;
            }
        }


        public void Init()
        {

            StreamReader sr = new StreamReader(@"G:\Users\wuxiaodong.GWMFC\Desktop\aa.txt", Encoding.UTF8);
            int index = 1;
            string nextLine;
            while ((nextLine = sr.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(nextLine))
                {
                    continue;
                }
                if (index % 3 == 0)
                {
                    if (nextLine.IndexOf("电话") == -1)
                    {
                        File.AppendAllText(@"G:\Users\wuxiaodong.GWMFC\Desktop\aa1.txt", "\r\n" + "电话:");
                        index += 1;
                    }
                }
                File.AppendAllText(@"G:\Users\wuxiaodong.GWMFC\Desktop\aa1.txt", "\r\n" + nextLine);
                index += 1;
            }
            sr.Close();
        }

        public static string AddStore(Store store)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            MySqlConnection con = new MySqlConnection("server=127.0.0.1;database=taoxiaoren;uid=root;pwd=zd!min123;charset='utf8'");
            string index=con.ExecuteScalar("SELECT Max(id)+1 from store").ToString();
            int id = index==null? 0:int.Parse(index);
            con.Execute("insert into store values(@id,@name,@phone,@address,@is_chain,@chain_name);"
                ,new { id= id,name=store.name,phone=store.phone,address=store.address, is_chain =store.is_chain, chain_name=store.chain_name});

            return null;
        }


        #region 案例
        //////新增数据
        // //con.Execute("insert into user values(null, '测试', 'http://www.cnblogs.com/linezero/', 18)");
        // ////新增数据返回自增id
        // //var id = con.QueryFirst<int>("insert into user values(null, 'linezero', 'http://www.cnblogs.com/linezero/', 18);select last_insert_id();");
        // ////修改数据
        // //con.Execute("update user set UserName = 'linezero123' where Id = @Id", new { Id = id });
        // //查询数据
        // var list = con.Query<Store>("select * from store");
        // foreach (var item in list)
        // {
        //     Console.WriteLine($"用户名：{item.id} 链接：{item.name}");
        // }
        // //删除数据
        // //con.Execute("delete from user where Id = @Id", new { Id = id });
        // //Console.WriteLine("删除数据后的结果");
        // //list = con.Query<Store>("select * from user");
        // //foreach (var item in list)
        // //{
        // //    Console.WriteLine($"用户名：{item.UserName} 链接：{item.Url}");
        // //}
        // Console.ReadKey();
        #endregion
    }
}
