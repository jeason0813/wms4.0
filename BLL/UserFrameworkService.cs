using DBModel;
using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public partial class UserFrameworkService : BaseService<UserFramework>, IUserFrameworkService
    {
        /// <summary>
        /// 保存权限设置
        /// </summary>
        /// <returns></returns>
        public string SavePower(string userCode, string powersStr)
        {
            string[] powers = powersStr.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);//权限编号数组
            //删除原有权限
            List<UserRelation> list=CurrentDBSession.UserRelationDal.LoadEntities(u => u.UserCode == userCode).ToList();
            for (int i = 0; i < list.Count; i++)
            {
                CurrentDBSession.UserRelationDal.DeleteEntity(list[i]);
            }
            //添加
            foreach (string power in powers)
            {
                CurrentDBSession.UserRelationDal.AddEntity(new UserRelation() { DeptCode = power, UserCode = userCode });
            }
            return CurrentDBSession.SaveChanges() ? "保存成功" : "保存失败";

        }

    }
}
