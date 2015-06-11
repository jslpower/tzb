using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.Model
{
    #region 文件信息业务实体
    /// <summary>
    /// 文件信息业务实体
    /// </summary>
    public class MFileInfo
    {
        string _fielname = "查看";
        /// <summary>
        /// default constructor
        /// </summary>
        public MFileInfo() { }

        /// <summary>
        /// 文件编号
        /// </summary>
        public string FileId { get; set; }
        /// <summary>
        /// 文件名(显示)
        /// </summary>
        public string FileName
        {
            get { return string.IsNullOrEmpty(_fielname) ? "查看" : _fielname; }
            set { _fielname = value; }
        }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }
    }
    #endregion
}
