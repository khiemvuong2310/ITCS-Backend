# Service Layer Migration - User to Account

## Tóm tắt thay đổi

Service layer đã được cập nhật để phù hợp với thay đổi từ `User` entity sang `Account` entity trong Core layer.

---

## Các file đã được cập nhật

### ✅ **FSCMS.Service/Services/UserService.cs**

**Thay đổi chính:**
- Thay tất cả `User` thành `Account` trong repository calls
- Thêm `using FSCMS.Core.Enum` để sử dụng Roles enum
- Cập nhật `GetUserDetailByIdAsync`:
  - Include `Patient` và `Doctor` thay vì các collections cũ
  - Account có One-to-One với Patient và Doctor
- Cập nhật `GetAllUsersAsync`:
  - Loại bỏ filter theo `UserName` (Account không có UserName)
  - Sử dụng `IsActive` thay vì `Status`
- Cập nhật `GetUsersByNameAsync`:
  - Search theo email thay vì username vì Account không có UserName
- Cập nhật `UpdateUserStatusAsync`:
  - Sử dụng `IsActive` thay vì `Status`

**Các phương thức đã cập nhật:**
- `GetUserByIdAsync`
- `GetUserByEmailAsync`
- `GetUsersByNameAsync`
- `GetUserDetailByIdAsync`
- `GetAllUsersAsync`
- `CreateUserAsync`
- `UpdateUserAsync`
- `DeleteUserAsync`
- `EmailExistsAsync`
- `VerifyUserEmailAsync`
- `UpdateUserStatusAsync`

---

### ✅ **FSCMS.Service/Services/AuthService.cs**

**Thay đổi chính:**
- Thay tất cả `User` thành `Account` trong repository calls
- Thêm `using FSCMS.Core.Enum` để sử dụng Roles enum
- Sử dụng `Roles.Patient` và `Roles.Admin` enum thay vì hardcode số:
  ```csharp
  RoleId = (int)Roles.Patient  // Thay vì RoleId = 3
  RoleId = (int)Roles.Admin    // Thay vì RoleId = 1
  ```
- Cập nhật `AuthenticateAsync`:
  - Check `IsActive` thay vì `Status`
- Loại bỏ các thuộc tính không còn tồn tại trong Account:
  - `UserName`, `Location` (trong RegisterAsync và AdminGenAcc)
- Cập nhật subject email trong `ForgotPassword`:
  - "CRYOFERT: YOUR RESET PASSWORD" thay vì "WEB EXCHANGE: YOUR RESET PASSWORD"

**Các phương thức đã cập nhật:**
- `AuthenticateAsync`
- `RefreshTokenAsync`
- `RegisterAsync`
- `AdminGenAcc`
- `SendAccount`
- `ForgotPassword`
- `SendVerificationEmailAsync`
- `VerifyAccountAsync`
- `SetEmailVerified`
- `ChangePasswordAsync`

---

### ✅ **FSCMS.Service/Mapping/UserMapping.cs**

**Thay đổi chính:**
- Đổi tất cả mapping từ `User` sang `Account`
- Cập nhật mapping cho các thuộc tính khác nhau:
  - `Image` → `Avatar`
  - `Status` → `IsActive`
- Thêm mapping ignore cho các thuộc tính không còn trong Account:
  - `UserName`, `Age`, `Location`, `Country`
- Cập nhật `UserDetailResponse` mapping:
  - Map `DoctorSpecialization` từ `Account.Doctor.Specialty`
  - Ignore các thuộc tính không thể truy cập trực tiếp từ Account

**Profile được cập nhật:**
- `UserMappingProfile`

---

## Thay đổi Entity Model

### Account Entity (mới - thay thế User)

```csharp
public class Account : BaseEntity
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string? Phone { get; set; }
    public string? Token { get; set; }
    public string? Avatar { get; set; }      // Thay vì Image
    public bool IsActive { get; set; }       // Thay vì Status
    public bool EmailVerified { get; set; }
    public DateTime? LastLogin { get; set; }
    
    // Role Management
    public int RoleId { get; set; }
    public virtual Role? Role { get; set; }
    
    // Navigation Properties
    public virtual Patient? Patient { get; set; }
    public virtual Doctor? Doctor { get; set; }
}
```

### Các thuộc tính bị loại bỏ (so với User)
- ❌ `UserName` - không có trong Account
- ❌ `Age` - không có trong Account
- ❌ `Location` - không có trong Account
- ❌ `Country` - không có trong Account
- ❌ Collections như `Appointments`, `Payments`, `Feedbacks` - được truy cập qua Patient hoặc Doctor

---

## Roles Enum

```csharp
public enum Roles
{
    Admin = 1,
    Doctor = 2,
    LaboratoryTechnician = 3,
    Receptionist = 4,
    ServiceManager = 5,
    Patient = 6
}
```

**Cách sử dụng:**
```csharp
// Thay vì hardcode
RoleId = 6  // BAD

// Sử dụng enum
RoleId = (int)Roles.Patient  // GOOD
```

---

## Response/Request Models

### UserResponse & UserDetailResponse
**Giữ nguyên tên** để không ảnh hưởng API contract với frontend.

Các thuộc tính này có thể trả về null hoặc giá trị mặc định:
- `UserName` - null (Account không có)
- `Age` - null (Account không có)
- `Location` - null (Account không có)
- `Country` - null (Account không có)
- `Image` - mapping từ `Account.Avatar`
- `Status` - mapping từ `Account.IsActive`

---

## Breaking Changes

### 1. Account Structure
- Account không còn `UserName`, `Age`, `Location`, `Country`
- Sử dụng `IsActive` thay vì `Status`
- Sử dụng `Avatar` thay vì `Image`

### 2. Relationships
- Account có One-to-One với Patient và Doctor
- Không thể truy cập trực tiếp Appointments, Payments từ Account
- Cần truy cập qua Patient hoặc Doctor profile

### 3. Role Management
- Khuyến khích sử dụng Roles enum thay vì hardcode
- Đảm bảo RoleId phù hợp với enum values

---

## Testing Checklist

Sau khi migration, cần test các chức năng sau:

### Authentication
- [ ] Login với email/password
- [ ] Register account mới
- [ ] Email verification
- [ ] Forgot password
- [ ] Change password
- [ ] Refresh token

### User Management
- [ ] Get user by ID
- [ ] Get user by email
- [ ] Search users
- [ ] Get user details
- [ ] Get all users (pagination)
- [ ] Create user
- [ ] Update user
- [ ] Delete user (soft delete)
- [ ] Update user status
- [ ] Verify user email

### Admin Functions
- [ ] Admin create account
- [ ] Send account credentials

---

## Migration Notes

1. **Database Migration**: Đảm bảo đã chạy migration để tạo bảng Account
2. **Data Migration**: Nếu có dữ liệu User cũ, cần migrate sang Account
3. **API Endpoints**: Kiểm tra xem có controller nào cần cập nhật không
4. **Frontend Integration**: Đảm bảo frontend vẫn nhận được đúng response format

---

## Next Steps

1. ✅ Cập nhật Service Layer (Completed)
2. ⏳ Cập nhật Controllers (nếu có)
3. ⏳ Cập nhật Unit Tests
4. ⏳ Integration Testing
5. ⏳ Update API Documentation

---

## Contact

Nếu có vấn đề trong quá trình sử dụng service layer mới, tham khảo:
- `FSCMS.Core/DATABASE_STRUCTURE.md` - Cấu trúc database
- `FSCMS.Core/MIGRATION_GUIDE.md` - Hướng dẫn migration database

