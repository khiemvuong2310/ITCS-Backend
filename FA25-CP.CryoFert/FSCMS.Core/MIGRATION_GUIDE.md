# Hướng dẫn Migration - Tái cấu trúc Database

## Tóm tắt thay đổi

Database của dự án **Fertility Service and Cryobank Management System** đã được tái cấu trúc hoàn toàn với:
- ✅ **39 entities mới** được tạo/cập nhật
- ✅ **31 entities cũ** đã được xóa
- ✅ **AppDbContext** đã được cập nhật với tất cả relationships

---

## Các bước Migration

### Bước 1: Review các thay đổi

Trước khi migration, hãy review:
- `DATABASE_STRUCTURE.md` - Cấu trúc database mới
- `FSCMS.Core/Entities/` - Các entity files
- `FSCMS.Core/AppDbContext.cs` - Relationships configuration

### Bước 2: Backup dữ liệu (quan trọng!)

Nếu database hiện tại có dữ liệu quan trọng:

```sql
-- SQL Server
BACKUP DATABASE [YourDatabaseName] 
TO DISK = N'D:\Backup\BeforeRestructure.bak'
WITH FORMAT, INIT, NAME = N'Before Restructure', SKIP, NOREWIND, NOUNLOAD;
```

### Bước 3: Xóa migrations cũ (tùy chọn)

Nếu muốn bắt đầu từ đầu với migration mới:

```bash
# Xóa tất cả migration files trong FSCMS.Core/Migrations/
# Giữ lại file AppDbContextModelSnapshot.cs
```

### Bước 4: Tạo Migration mới

```bash
# Di chuyển đến thư mục FSCMS.Core
cd "FSCMS.Core"

# Tạo migration mới
dotnet ef migrations add RestructureDatabase --startup-project ../FA25-CP.CryoFert-BE

# Hoặc nếu muốn initial migration
dotnet ef migrations add InitialRestructure --startup-project ../FA25-CP.CryoFert-BE
```

### Bước 5: Review Migration Code

Kiểm tra file migration được tạo trong `FSCMS.Core/Migrations/`:
- Xem các bảng sẽ được tạo
- Xem các bảng sẽ được xóa
- Xem các relationships

### Bước 6: Apply Migration

```bash
# Update database
dotnet ef database update --startup-project ../FA25-CP.CryoFert-BE
```

### Bước 7: Verify Database

Sau khi migration, kiểm tra:
- Tất cả bảng đã được tạo đúng
- Foreign keys đã được thiết lập
- Indexes đã được tạo (nếu có)

---

## Troubleshooting

### Lỗi: "There is already an object named 'XXX' in the database"

**Giải pháp**: Database cũ vẫn còn. Có 2 lựa chọn:

1. **Drop và tạo mới** (mất dữ liệu):
```bash
dotnet ef database drop --startup-project ../FA25-CP.CryoFert-BE
dotnet ef database update --startup-project ../FA25-CP.CryoFert-BE
```

2. **Manual cleanup**: Xóa thủ công các bảng cũ không dùng nữa

### Lỗi: "The Entity Framework tools version 'X.X.X' is older than..."

**Giải pháp**: Update EF Core tools:
```bash
dotnet tool update --global dotnet-ef
```

### Lỗi: Foreign key constraint

**Giải pháp**: Kiểm tra relationships trong AppDbContext.cs, đảm bảo:
- Tên property FK đúng
- Navigation properties được khai báo đầy đủ
- DeleteBehavior phù hợp

---

## Data Migration (nếu cần)

Nếu cần migrate dữ liệu từ cấu trúc cũ sang mới:

### 1. Account ← User
```sql
INSERT INTO Accounts (Email, Password, Phone, Token, Avatar, IsActive, EmailVerified, RoleId, CreatedDate, UpdatedDate, IsDelete)
SELECT Email, Password, Phone, Token, Image, Status, EmailVerified, RoleId, CreatedDate, UpdatedDate, IsDelete
FROM Users;
```

### 2. Patient - Cập nhật AccountId
```sql
UPDATE Patients 
SET AccountId = (SELECT Id FROM Accounts WHERE Accounts.Email = (SELECT Email FROM Users WHERE Users.Id = Patients.UserId))
WHERE UserId IS NOT NULL;
```

### 3. LabSample ← Specimen
```sql
INSERT INTO LabSamples (PatientId, SampleCode, SampleType, Status, CollectionDate, StorageDate, Quality, Notes, IsAvailable, CreatedDate, UpdatedDate, IsDelete)
SELECT PatientId, SpecimenCode, Type, Status, CollectionDate, StorageDate, Quality, Notes, IsAvailable, CreatedDate, UpdatedDate, IsDelete
FROM Specimens;
```

*(Tương tự cho các bảng khác...)*

---

## Post-Migration Tasks

### 1. Update Service Layer

Cập nhật các service files để sử dụng entities mới:
- `FSCMS.Service/Services/AuthService.cs` - Dùng Account thay vì User
- Các service khác tương ứng

### 2. Update API Controllers

Cập nhật controllers để phù hợp với entities mới.

### 3. Update Request/Response Models

Cập nhật DTOs trong:
- `FSCMS.Service/RequestModel/`
- `FSCMS.Service/ReponseModel/`

### 4. Test thoroughly

Kiểm tra kỹ tất cả chức năng:
- Đăng ký/Đăng nhập
- Quản lý bệnh nhân
- Đặt lịch hẹn
- Quản lý mẫu
- Hợp đồng lưu trữ

---

## Rollback Plan

Nếu có vấn đề, có thể rollback:

```bash
# Remove migration
dotnet ef migrations remove --startup-project ../FA25-CP.CryoFert-BE

# Restore database from backup
# (Use SQL Server Management Studio or sqlcmd)
```

---

## Notes

- ⚠️ **QUAN TRỌNG**: Backup database trước khi migration
- 📝 Cấu trúc mới phức tạp hơn nhưng rõ ràng và mở rộng dễ dàng hơn
- 🔄 Một số entities cũ đã được gộp/tách để tối ưu thiết kế
- 💾 Transaction và Media là bảng độc lập với logical relationships

---

## Contact

Nếu có vấn đề trong quá trình migration, liên hệ team lead hoặc tham khảo:
- `DATABASE_STRUCTURE.md` - Cấu trúc chi tiết
- Entity Relationship Diagram (nếu có)

