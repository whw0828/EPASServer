using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.5411
    /// Description:作业指导书 由产品、生产设备、工序共同决定
    /// </summary>
    public class QualityControlledDocuments:BaseEntity
    {
        public QualityControlledDocuments()
        {
        }

        /// <summary>
        /// 表名: "QualityControlledDocuments"
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("QualityControlledDocumentsId")]
        public String QualityControlledDocumentsId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 文件分组 例如 作业指导书
        /// </summary>
        [DisplayName("DocType")]
        public int DocType
        {
            get ;
            set ;
        }
        /// <summary>
        /// 文档编号
        /// </summary>
        [DisplayName("DocNum")]
        public String DocNum
        {
            get ;
            set ;
        }
        /// <summary>
        /// 文档名称
        /// </summary>
        [DisplayName("DocName")]
        public String DocName
        {
            get;
            set;
        }
        /// <summary>
        /// 文档内容
        /// </summary>
        [DisplayName("DocData")]
        public Byte[] DocData
        {
            get ;
            set ;
        }
        /// <summary>
        /// 作业指导书文件类型
        /// </summary>
        [DisplayName("FileType")]
        public int FileType
        {
            get ;
            set ;
        }

        /// <summary>
        /// 文档版本
        /// </summary>
        [DisplayName("DocVersion")]
        public String DocVersion
        {
            get;
            set;
        }
        /// <summary>
        /// 责任部门
        /// </summary>
        [DisplayName("ResponsibleDepartment")]
        public String ResponsibleDepartment
        {
            get;
            set;
        }
        /// <summary>
        /// 失效时间
        /// </summary>
        [DisplayName("FailureTime")]
        public DateTime? FailureTime
        {
            get;
            set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("DocDescribe")]
        public String DocDescribe
        {
            get;
            set;
        }
    }
}
