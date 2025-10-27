namespace FSCMS.Core.Enum;

/// <summary>
/// Defines the biological sample type stored in the cryobank.
/// </summary>
public enum SampleType
{
    None = 0,      // Không xác định (cho Canister, Goblet, Slot)
    Oocyte = 1,    // Trứng
    Sperm = 2,     // Tinh trùng
    Embryo = 3     // Phôi
}
