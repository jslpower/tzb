using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enow.TZB.Model;

namespace Enow.TZB.BLL
{
    public class BCity
    {
        /// <summary>
        /// 根据名称查询
        /// </summary>
        /// <param name="usid">查询名称</param>
        /// <returns></returns>
        public static List<tbl_SysCity> Getnamesel(string getname)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = (from tmp in rdc.tbl_SysCity where tmp.Name.StartsWith(getname) select tmp).ToList();
                if (model.Count > 0)
                {
                    return model;
                }
                return null;
            }
        }
        /// <summary>
        /// 根据ID查询城市名称
        /// </summary>
        /// <param name="ID">城市ID</param>
        /// <returns></returns>
        public static string Getcityname(int ID)
        {
            using (FWDC rdc = new FWDC())
            {
                var model =rdc.tbl_SysCity.FirstOrDefault(w=>w.CityId==ID);
                if (model!=null)
                {
                    return model.Name;
                }
                return "全部";
            }
        }
        /// <summary>
        /// 查询所有城市信息
        /// </summary>
        /// <param name="usid">会员ID</param>
        /// <returns></returns>
        public static List<tbl_SysCity> Getctyszm()
        {
            using (FWDC rdc = new FWDC())
            {
                var model = (from tmp in rdc.tbl_SysCity where tmp.CityId <= 796 select tmp).ToList();
                if (model.Count > 0)
                {
                    return model;
                }
                return null;
            }
        }
        /// <summary>
        /// 查询所有城市信息
        /// </summary>
        /// <param name="usid">会员ID</param>
        /// <returns></returns>
        public static void Ipdatectyname(List<tbl_SysCity> modellist)
        {
            using (FWDC rdc = new FWDC())
            {
                var list = (from tmp in rdc.tbl_SysCity where tmp.CityId <= 796 select tmp).ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Name == modellist[i].Name)
                    {
                        list[i].JP = modellist[i].JP;
                    }
                }
                rdc.SubmitChanges();
            }
        }
        /// <summary>
        /// 获取第一个汉字的首字母，只能输入汉字
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static string GetPYChar(string c)
        {
            byte[] array = new byte[2];
            array = System.Text.Encoding.Default.GetBytes(c);
            int i = (short)(array[0] - '\0') * 256 + ((short)(array[1] - '\0'));
            if (i < 0xB0A1) return "*";
            if (i < 0xB0C5) return "A";
            if (i < 0xB2C1) return "B";
            if (i < 0xB4EE) return "C";
            if (i < 0xB6EA) return "D";
            if (i < 0xB7A2) return "E";
            if (i < 0xB8C1) return "F";
            if (i < 0xB9FE) return "G";
            if (i < 0xBBF7) return "H";
            if (i < 0xBFA6) return "J";
            if (i < 0xC0AC) return "K";
            if (i < 0xC2E8) return "L";
            if (i < 0xC4C3) return "M";
            if (i < 0xC5B6) return "N";
            if (i < 0xC5BE) return "O";
            if (i < 0xC6DA) return "P";
            if (i < 0xC8BB) return "Q";
            if (i < 0xC8F6) return "R";
            if (i < 0xCBFA) return "S";
            if (i < 0xCDDA) return "T";
            if (i < 0xCEF4) return "W";
            if (i < 0xD1B9) return "X";
            if (i < 0xD4D1) return "Y";
            if (i < 0xD7FA) return "Z";
            return "*";
        }
    }
}
