# Cấu trúc Database - Fertility Service and Cryobank Management System

## Tổng quan
Database đã được tái cấu trúc hoàn toàn theo thiết kế mới với 6 nhóm chính.

---

## Nhóm 1: Quản lý Người dùng & Bệnh nhân

### Account (Tài khoản)
- **Mô tả**: Quản lý thông tin đăng nhập và xác thực
- **Quan hệ**:
  - One-to-One với `Patient`: Mỗi tài khoản có thể thuộc về một bệnh nhân
  - One-to-One với `Doctor`: Mỗi tài khoản có thể thuộc về một bác sĩ
  - Many-to-One với `Role`: Mỗi tài khoản có một vai trò

### Patient (Bệnh nhân)
- **Mô tả**: Thông tin chi tiết về bệnh nhân
- **Quan hệ**:
  - One-to-One với `Account`
  - One-to-Many với `Treatment`: Một bệnh nhân có nhiều liệu trình điều trị
  - One-to-Many với `LabSample`: Một bệnh nhân sở hữu nhiều mẫu
  - One-to-Many với `CryoStorageContract`: Một bệnh nhân có nhiều hợp đồng lưu trữ
  - Many-to-Many với chính nó qua `Relationship`: Ghi nhận quan hệ vợ/chồng, hiến tặng...

### Doctor (Bác sĩ)
- **Mô tả**: Thông tin chuyên môn và kinh nghiệm của bác sĩ
- **Quan hệ**:
  - One-to-One với `Account`
  - One-to-Many với `DoctorSchedule`: Một bác sĩ có nhiều lịch làm việc
  - One-to-Many với `Treatment`: Một bác sĩ phụ trách nhiều liệu trình

### Relationship (Mối quan hệ)
- **Mô tả**: Bảng trung gian tạo quan hệ Many-to-Many giữa Patient và Patient
- **Quan hệ**: Liên kết hai bệnh nhân (vợ/chồng, cha/mẹ, con, donor...)

---

## Nhóm 2: Lịch hẹn & Dịch vụ

### DoctorSchedule (Lịch làm việc của Bác sĩ)
- **Mô tả**: Quản lý lịch làm việc hàng ngày của bác sĩ
- **Quan hệ**:
  - Many-to-One với `Doctor`
  - One-to-Many với `Slot`: Một lịch làm việc chia thành nhiều khe thời gian

### Slot (Khe hẹn)
- **Mô tả**: Khe thời gian cụ thể trong lịch làm việc
- **Quan hệ**:
  - Many-to-One với `DoctorSchedule`
  - One-to-One với `Appointment`: Mỗi khe chỉ có một cuộc hẹn

### ServiceCategory (Danh mục Dịch vụ)
- **Mô tả**: Phân loại các dịch vụ y tế
- **Quan hệ**:
  - One-to-Many với `Service`

### Service (Dịch vụ)
- **Mô tả**: Các dịch vụ y tế cụ thể (IVF, IUI, xét nghiệm...)
- **Quan hệ**:
  - Many-to-One với `ServiceCategory`
  - One-to-Many với `ServiceRequestDetails`

### ServiceRequest (Yêu cầu Dịch vụ)
- **Mô tả**: Yêu cầu sử dụng dịch vụ từ bệnh nhân
- **Quan hệ**:
  - Many-to-One với `Appointment`
  - One-to-Many với `ServiceRequestDetails`

### ServiceRequestDetails (Chi tiết Yêu cầu Dịch vụ)
- **Mô tả**: Bảng trung gian Many-to-Many giữa ServiceRequest và Service
- **Quan hệ**: Liên kết yêu cầu với các dịch vụ cụ thể, số lượng, giá cả

---

## Nhóm 3: Điều trị & Bệnh án

### Treatment (Liệu trình)
- **Mô tả**: Liệu trình điều trị tổng thể của bệnh nhân
- **Quan hệ**:
  - Many-to-One với `Patient` và `Doctor`
  - One-to-Many với `TreatmentCycle`: Một liệu trình có nhiều chu kỳ
  - One-to-One với `TreatmentIVF`: Phác đồ IVF chi tiết (nếu có)

### TreatmentIVF (Phác đồ IVF)
- **Mô tả**: Chi tiết quy trình IVF của một liệu trình
- **Quan hệ**: One-to-One với `Treatment`

### TreatmentCycle (Chu kỳ Điều trị)
- **Mô tả**: Từng chu kỳ cụ thể trong liệu trình
- **Quan hệ**:
  - Many-to-One với `Treatment`
  - One-to-Many với `Appointment`

### Appointment (Cuộc hẹn)
- **Mô tả**: Cuộc hẹn khám/điều trị giữa bác sĩ và bệnh nhân
- **Quan hệ**:
  - Many-to-One với `TreatmentCycle`
  - One-to-One với `Slot`
  - One-to-One với `MedicalRecord`

### MedicalRecord (Hồ sơ Bệnh án)
- **Mô tả**: Hồ sơ y tế của mỗi cuộc hẹn
- **Quan hệ**:
  - One-to-One với `Appointment`
  - One-to-Many với `Prescription`: Một hồ sơ có nhiều đơn thuốc

### Prescription (Đơn thuốc)
- **Mô tả**: Đơn thuốc được kê cho bệnh nhân
- **Quan hệ**:
  - Many-to-One với `MedicalRecord`
  - One-to-Many với `PrescriptionDetail`

### PrescriptionDetail (Chi tiết Đơn thuốc)
- **Mô tả**: Bảng trung gian Many-to-Many giữa Prescription và Medicine
- **Quan hệ**: Liên kết đơn thuốc với các loại thuốc, liều lượng, hướng dẫn

### Medicine (Thuốc)
- **Mô tả**: Danh mục thuốc trong hệ thống
- **Quan hệ**: One-to-Many với `PrescriptionDetail`

---

## Nhóm 4: Lab & Kho lưu trữ

### LabSample (Mẫu)
- **Mô tả**: Mẫu xét nghiệm chung (phôi, tinh trùng, trứng)
- **Quan hệ**:
  - Many-to-One với `Patient`
  - Many-to-One với `CryoLocation`
  - One-to-One với `LabSampleEmbryo` hoặc `LabSampleSperm` hoặc `LabSampleOocyte`

### LabSampleEmbryo (Mẫu Phôi)
- **Mô tả**: Chi tiết mẫu phôi (kế thừa từ LabSample)
- **Quan hệ**: One-to-One với `LabSample`

### LabSampleSperm (Mẫu Tinh trùng)
- **Mô tả**: Chi tiết mẫu tinh trùng (kế thừa từ LabSample)
- **Quan hệ**: One-to-One với `LabSample`

### LabSampleOocyte (Mẫu Trứng)
- **Mô tả**: Chi tiết mẫu trứng (kế thừa từ LabSample)
- **Quan hệ**: One-to-One với `LabSample`

### CryoLocation (Vị trí Lưu trữ)
- **Mô tả**: Vị trí cụ thể trong kho lạnh (tank, canister, cane, position)
- **Quan hệ**: One-to-Many với `LabSample`

### CryoImport (Lịch sử Nhập)
- **Mô tả**: Ghi lại thông tin khi mẫu được nhập vào kho
- **Quan hệ**: Many-to-One với `LabSample` và `CryoLocation`

### CryoExport (Lịch sử Xuất)
- **Mô tả**: Ghi lại thông tin khi mẫu được xuất khỏi kho
- **Quan hệ**: Many-to-One với `LabSample` và `CryoLocation`

---

## Nhóm 5: Hợp đồng & Gói dịch vụ

### CryoPackage (Gói Lưu trữ)
- **Mô tả**: Các gói lưu trữ lạnh với giá và điều kiện khác nhau
- **Quan hệ**: One-to-Many với `CryoStorageContract`

### CryoStorageContract (Hợp đồng Lưu trữ)
- **Mô tả**: Hợp đồng lưu trữ giữa bệnh nhân và phòng khám
- **Quan hệ**:
  - Many-to-One với `Patient`
  - Many-to-One với `CryoPackage`
  - One-to-Many với `CPSDetail`

### CPSDetail (Chi tiết Hợp đồng)
- **Mô tả**: Bảng trung gian Many-to-Many giữa CryoStorageContract và LabSample
- **Quan hệ**: Liên kết hợp đồng với các mẫu cụ thể được lưu trữ

---

## Nhóm 6: Bảng Phụ trợ

### Transaction (Giao dịch)
- **Mô tả**: Ghi lại tất cả giao dịch tài chính
- **Quan hệ**: Bảng độc lập với quan hệ logic (không có FK vật lý)
- **Liên kết logic**: RelatedEntityId + RelatedEntityType

### Media (Tài liệu đa phương tiện)
- **Mô tả**: Lưu trữ file đính kèm (ảnh, PDF, kết quả xét nghiệm...)
- **Quan hệ**: Bảng độc lập với quan hệ logic (không có FK vật lý)
- **Liên kết logic**: RelatedEntityId + RelatedEntityType

---

## Bảng giữ lại không thay đổi

- **AuditLog**: Lưu lại nhật ký hệ thống
- **Notification**: Thông báo cho người dùng
- **SystemConfig**: Cấu hình hệ thống
- **Role**: Vai trò người dùng

---

## Các bảng đã bị xóa (thay thế bởi bảng mới)

| Bảng cũ | Thay thế bởi |
|---------|--------------|
| User | Account |
| Specimen | LabSample + LabSampleEmbryo/Sperm/Oocyte |
| EmbryoAssessment | LabSampleEmbryo |
| SpermAnalysis | LabSampleSperm |
| OocyteAssessment | LabSampleOocyte |
| IVFCycle | TreatmentIVF |
| Invoice | Transaction |
| Payment | Transaction |
| Attachment | Media |
| PatientRecord | MedicalRecord |
| CryobankPosition | CryoLocation |
| CryobankTank | CryoLocation |
| Cryopreservation | CryoImport + CryoExport |
| ServicePackage | - |
| Encounter | - |
| CheckIn | - |
| ConsentForm | - |
| Content | - |
| Contract | CryoStorageContract |

---

## Migration

Sau khi xem xét cấu trúc, cần chạy migration để tạo database mới:

```bash
# Trong thư mục FSCMS.Core
dotnet ef migrations add RestructureDatabase --startup-project ../FA25-CP.CryoFert-BE

# Apply migration
dotnet ef database update --startup-project ../FA25-CP.CryoFert-BE
```

---

**Lưu ý**: Đây là một thay đổi lớn về cấu trúc database. Cần backup dữ liệu cũ trước khi migration nếu có dữ liệu quan trọng.

