using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Weeho.Infrastructure.Extensions
{
    public static class DataTableExtensions
    {
        #region DataTable 转换为List 集合
        /// <summary>
        /// DataTable 转换为List 集合
        /// </summary>
        /// <typeparam name="TResult">类型</typeparam>
        /// <param name="dt">DataTable</param>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataTable dt) where T : class, new()
        {
            //datatable没有数据 返回null
            if (dt == null || dt.Rows.Count == 0)
            {
                return new List<T>(); ;
            }
            //model的容器
            List<T> modelList = new List<T>();
            //datatable可能有很多行数据，遍历每一行
            foreach (DataRow dr in dt.Rows)
            {
                //model类型
                T model = new T();
                for (int i = 0; i < dr.Table.Columns.Count; i++)
                {
                    //发现datatable的列属性
                    PropertyInfo propertyInfo = model.GetType().GetProperty(dr.Table.Columns[i].ColumnName);
                    if (propertyInfo != null && dr[i] != DBNull.Value)
                        //用索引值设置属性值
                        propertyInfo.SetValue(model, dr[i], null);
                }
                modelList.Add(model);
            }
            return modelList;
        }



        #endregion

        #region 转换为一个DataTable
        /// <summary>
        /// 转换为一个DataTable
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<TResult>(this IEnumerable<TResult> value) where TResult : class
        {
            //创建属性的集合
            List<PropertyInfo> pList = new List<PropertyInfo>();
            //获得反射的入口
            Type type = typeof(TResult);
            DataTable dt = new DataTable();
            //把所有的public属性加入到集合 并添加DataTable的列
            Array.ForEach<PropertyInfo>(type.GetProperties(), p => { pList.Add(p); dt.Columns.Add(p.Name, p.PropertyType); });
            foreach (var item in value)
            {
                //创建一个DataRow实例
                DataRow row = dt.NewRow();
                //给row 赋值
                pList.ForEach(p => row[p.Name] = p.GetValue(item, null));
                //加入到DataTable
                dt.Rows.Add(row);
            }
            return dt;
        }
        #endregion
    }
}
