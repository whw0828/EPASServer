using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{

    /// <summary>
    /// 临时时间计算用 不存在数据库中对应表
    /// </summary>
    public class ProductionMakeWorkOrderTime
    {
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ProductionMakeWorkOrderId")]
        public String ProductionMakeWorkOrderId
        {
            get;
            set;
        }

        //工序内部时间差 单位秒
        [DisplayName("spanTime")]
        public double spanTime
        {
            get;
            set;
        }
        /// <summary>
        /// 最早开始时间
        /// </summary>
        [DisplayName("EarliestStartTime")]
        public DateTime EarliestStartTime
        {
            get;
            set;
        }
        /// <summary>
        /// 最早结束时间
        /// </summary>
        [DisplayName("EarliestEndTime")]
        public DateTime EarliestEndTime
        {
            get;
            set;
        }

        /// <summary>
        /// 最晚开始时间
        /// </summary>
        [DisplayName("LatestStartTime")]
        public DateTime LatestStartTime
        {
            get;
            set;
        }
        /// <summary>
        /// 最晚结束时间
        /// </summary>
        [DisplayName("LatestEndTime")]
        public DateTime LatestEndTime
        {
            get;
            set;
        }

    }

    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.4364
    /// Description:
    /// </summary>
    public class ProductionMakeWorkOrder:BaseEntity
    {



        public ProductionMakeWorkOrder()
        {
        }

        /// <summary>
        /// 表名: "ProductionMakeWorkOrder"
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ProductionMakeWorkOrderId")]
        public String ProductionMakeWorkOrderId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ProcessOrder")]
        public int ProcessOrder
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("WorkOrderId")]
        public String WorkOrderId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ProductionVersionId")]
        public String ProductionVersionId
        {
            get ;
            set ;
        }

 
        /// <summary>
        /// 新生成报工条码（0-否 1-是）
        /// </summary>
        [DisplayName("IsCreateNewBarCode")]
        public int IsCreateNewBarCode
        {
            get ;
            set ;
        }
        /// <summary>
        /// 禁止重复条码报工
        /// </summary>
        [DisplayName("IsRepeatBarCheck")]
        public int IsRepeatBarCheck
        {
            get ;
            set ;
        }
        /// <summary>
        /// 是否支持第三方条码
        /// </summary>
        [DisplayName("IsThirdBarcode")]
        public int IsThirdBarcode
        {
            get ;
            set ;
        }
        /// <summary>
        /// 报工条码默认包装数量
        /// </summary>
        [DisplayName("DefaultBarCodePackageNum")]
        public decimal DefaultBarCodePackageNum
        {
            get ;
            set ;
        }
        /// <summary>
        /// 工序作业人员需求量
        /// </summary>
        [DisplayName("WorkManCount")]
        public decimal WorkManCount
        {
            get;
            set;
        }
        /// <summary>
        /// 报工模式(0-手动报工 1-自动报工）
        /// </summary>
        [DisplayName("ReportMode")]
        public int ReportMode
        {
            get ;
            set ;
        }

        /// <summary>
        ///扫码自动报工次数
        /// </summary>
        [DisplayName("AutoScanBarcodeNum")]
        public int AutoScanBarcodeNum
        {
            get;
            set;
        }
        /// <summary>
        /// 报工条码模板ID
        /// </summary>
        [DisplayName("BarcodeTemplateId")]
        public string BarcodeTemplateId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 二次包装模板ID
        /// </summary>
        [DisplayName("SecondaryPackingTemplateId")]
        public string SecondaryPackingTemplateId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 是否打印箱包装码
        /// </summary>
        [DisplayName("IsPrintSecondaryPacking")]
        public int IsPrintSecondaryPacking
        {
            get ;
            set ;
        }
        /// <summary>
        /// 二次包装码打印数量
        /// </summary>
        [DisplayName("SecondaryPackingPrintNum")]
        public int SecondaryPackingPrintNum
        {
            get ;
            set ;
        }
        /// <summary>
        /// 是否末尾工序
        /// </summary>
        [DisplayName("IsEndWorkOrder")]
        public int IsEndWorkOrder
        {
            get ;
            set ;
        }
        /// <summary>
        /// 工序质检（0-管控 1-免检）
        /// </summary>
        [DisplayName("WorkOrderQC")]
        public int WorkOrderQC
        {
            get ;
            set ;
        }

        /// <summary>
        /// 工序成品率
        /// </summary>
        [DisplayName("FinishProductRate")]
        public Decimal FinishProductRate
        {
            get;
            set;
        }

        /// <summary>
        /// 工序前设置时间值
        /// </summary>
        [DisplayName("FrontTimeValue")]
        public decimal FrontTimeValue
        {
            get;
            set;
        }
        /// <summary>
        /// 工序前设置时间单位  以秒为单位计量
        /// </summary>
        [DisplayName("FrontTimeUnit")]
        public int FrontTimeUnit
        {
            get;
            set;
        }

        /// <summary>
        /// 工序标准产能时间值
        /// </summary>
        [DisplayName("StandardMakeTimeValue")]
        public decimal StandardMakeTimeValue
        {
            get;
            set;
        }
        /// <summary>
        /// 工序标准产能时间单位  以秒为单位计量
        /// </summary>
        [DisplayName("StandardMakeTimeUnit")]
        public int StandardMakeTimeUnit
        {
            get;
            set;
        }
        /// <summary>
        /// 工序标准产能数量
        /// </summary>
        [DisplayName("StandardMakeValue")]
        public Decimal StandardMakeValue
        {
            get;
            set;
        }

        /// <summary>
        /// 工序后设置时间值
        /// </summary>
        [DisplayName("BackTimeValue")]
        public decimal BackTimeValue
        {
            get;
            set;
        }
        /// <summary>
        /// 工序前后置时间单位  以秒为单位计量
        /// </summary>
        [DisplayName("BackTimeUnit")]
        public int BackTimeUnit
        {
            get;
            set;
        }

        /// <summary>
        /// 是否瓶颈工序
        /// </summary>
        [DisplayName("Bottleneck")]
        public int Bottleneck
        {
            get;
            set;
        }

    }
}
