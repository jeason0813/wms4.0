using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DALFactory
{

    //抽象工厂类  用来数据操作对象实例
    public partial class AbstractFactory
    {
        private static readonly string AssemblyPath = ConfigurationManager.AppSettings["AssemblyPath"];//程序集名称
        private static readonly string NameSpace = ConfigurationManager.AppSettings["Namespace"];//命名空间名称
         /// <summary>
         /// 创建UserInfoDal实例
         /// </summary>
         /// <returns></returns>                                                                                        
                                                                                                


        //public  static IUserInfoDal CreateUserInfoDal()
        //{
        //    string fullClassName = NameSpace + ".UserInfoDAL";
        //    return CreateInstance(fullClassName) as IUserInfoDal;
        //}

        /// <summary>
        /// 通过反射创建指定类型的实例
        /// </summary>
        /// <param name="fullClassName"></param>
        /// <returns></returns>
        private static object CreateInstance(string fullClassName)
        {
            var assembly = Assembly.Load(AssemblyPath);
            return assembly.CreateInstance(fullClassName);
        }
    }
}
