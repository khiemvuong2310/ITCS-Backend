using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Enum
{
    public enum TreatmentStepType
    {
        // IUI 
        /// <summary>
        /// Chuẩn bị trước chu kỳ IUI (khám, xét nghiệm, tư vấn).
        /// </summary>
        IUI_PreCyclePreparation = 1,

        /// <summary>
        /// Đánh giá ngày 2–3 chu kỳ (nội tiết, siêu âm cơ bản).
        /// </summary>
        IUI_Day2_3_Assessment = 2,

        /// <summary>
        /// Theo dõi nang noãn ngày 7–10 bằng siêu âm/xét nghiệm.
        /// </summary>
        IUI_Day7_10_FollicleMonitoring = 3,

        /// <summary>
        /// Tiêm thuốc kích rụng trứng (trigger) chuẩn bị cho bơm IUI.
        /// </summary>
        IUI_Day10_12_Trigger = 4,

        /// <summary>
        /// Thực hiện thủ thuật bơm tinh trùng vào buồng tử cung.
        /// </summary>
        IUI_Procedure = 5,

        /// <summary>
        /// Giai đoạn sau bơm IUI, theo dõi và hỗ trợ hoàng thể.
        /// </summary>
        IUI_PostIUI = 6,

        /// <summary>
        /// Lấy máu xét nghiệm beta hCG sau IUI để xác định có thai.
        /// </summary>
        IUI_BetaHCGTest = 7,

        // IVF 
        /// <summary>
        /// Chuẩn bị trước chu kỳ IVF (tư vấn, xét nghiệm, hồ sơ).
        /// </summary>
        IVF_PreCyclePreparation = 100,

        /// <summary>
        /// Bắt đầu phác đồ kích thích buồng trứng.
        /// </summary>
        IVF_StimulationStart = 101,

        /// <summary>
        /// Theo dõi đáp ứng kích thích (siêu âm, xét nghiệm hormone).
        /// </summary>
        IVF_Monitoring = 102,

        /// <summary>
        /// Tiêm thuốc kích rụng trứng (trigger) trước chọc hút trứng.
        /// </summary>
        IVF_Trigger = 103,

        /// <summary>
        /// Chọc hút trứng (Oocyte Pick Up).
        /// </summary>
        IVF_OPU = 104,

        /// <summary>
        /// Thụ tinh trứng và tinh trùng trong labo.
        /// </summary>
        IVF_Fertilization = 105,

        /// <summary>
        /// Nuôi cấy phôi sau thụ tinh.
        /// </summary>
        IVF_EmbryoCulture = 106,

        /// <summary>
        /// Chuyển phôi tươi/trữ đông vào buồng tử cung.
        /// </summary>
        IVF_EmbryoTransfer = 107,

        /// <summary>
        /// Lấy máu xét nghiệm beta hCG sau chuyển phôi để xác định có thai.
        /// </summary>
        IVF_BetaHCGTest = 108
    }
}
