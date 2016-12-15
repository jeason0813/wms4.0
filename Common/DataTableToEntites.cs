using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Common
{
    public class DataTableToEntites
    {
        /// <summary>
        /// 转换DataRow为实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static T GetEntity<T>(DataRow row) where T : new()
        {
            T entity = new T();
            foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
            {
                if (row.Table.Columns.Contains(propertyInfo.Name))
                {
                    if (DBNull.Value != row[propertyInfo.Name])
                    {
                        Type type = propertyInfo.PropertyType;
                        //判断type类型是否为泛型，因为nullable是泛型类,
                        if (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))//判断convertsionType是否为nullable泛型类
                        {
                            //如果type为nullable类，声明一个NullableConverter类，该类提供从Nullable类到基础基元类型的转换
                            System.ComponentModel.NullableConverter nullableConverter = new System.ComponentModel.NullableConverter(type);
                            //将type转换为nullable对的基础基元类型
                            type = nullableConverter.UnderlyingType;
                        }
                        propertyInfo.SetValue(entity, Convert.ChangeType(row[propertyInfo.Name], type), null);
                    }
                }
            }
            return entity;
        }

        /// <summary>
        /// 转换DataTable为实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static IList<T> GetEntities<T>(DataTable table) where T : new()
        {
            IList<T> entities = new List<T>();
            foreach (DataRow row in table.Rows)
            {
                T entity = new T();
                foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
                {
                    if (row.Table.Columns.Contains(propertyInfo.Name))
                    {
                        if (DBNull.Value != row[propertyInfo.Name])
                        {
                            Type type = propertyInfo.PropertyType;
                            //判断type类型是否为泛型，因为nullable是泛型类,
                            if (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))//判断convertsionType是否为nullable泛型类
                            {
                                //如果type为nullable类，声明一个NullableConverter类，该类提供从Nullable类到基础基元类型的转换
                                System.ComponentModel.NullableConverter nullableConverter = new System.ComponentModel.NullableConverter(type);
                                //将type转换为nullable对的基础基元类型
                                type = nullableConverter.UnderlyingType;
                            }
                            propertyInfo.SetValue(entity, Convert.ChangeType(row[propertyInfo.Name], type), null);
                        }
                    }
                }
                entities.Add(entity);
            }
            return entities;
        }

    }
}
