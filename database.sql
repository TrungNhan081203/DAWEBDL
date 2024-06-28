Create database WebDL;
-- Bảng Người dùng
CREATE TABLE NguoiDung (
    MaNguoiDung INT IDENTITY(1,1) PRIMARY KEY, 
    TenDangNhap NVARCHAR(50) UNIQUE NOT NULL,
    MatKhau NVARCHAR(255) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    HoTen NVARCHAR(100) NOT NULL,
    SoDienThoai NVARCHAR(20),
    DiaChi NVARCHAR(255),
    NgayTao DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- Bảng Điểm đến
CREATE TABLE DiemDen (
    MaDiemDen INT IDENTITY(1,1) PRIMARY KEY,
    TenDiemDen NVARCHAR(100) NOT NULL,
    MoTa NVARCHAR(MAX),
    ViTri NVARCHAR(255),
    LinkAnh NVARCHAR(255)
);

-- Bảng Tour du lịch
CREATE TABLE TourDuLich (
    MaTour INT IDENTITY(1,1) PRIMARY KEY,
    MaDiemDen INT,
    TenTour NVARCHAR(100) NOT NULL,
    MoTa NVARCHAR(MAX),
    Gia DECIMAL(10, 2) NOT NULL,
    NgayBatDau DATE,
    NgayKetThuc DATE,
    LinkAnh NVARCHAR(255),
    FOREIGN KEY (MaDiemDen) REFERENCES DiemDen(MaDiemDen)
);

-- Bảng Đặt chỗ
CREATE TABLE DatCho (
    MaDatCho INT IDENTITY(1,1) PRIMARY KEY,
    MaNguoiDung INT,
    MaTour INT,
	SoLuongNguoi INT NOT NULL DEFAULT 1,
    NgayDat DATETIME DEFAULT CURRENT_TIMESTAMP,
    TrangThai NVARCHAR(20) DEFAULT 'Chờ xác nhận',
    FOREIGN KEY (MaNguoiDung) REFERENCES NguoiDung(MaNguoiDung),
    FOREIGN KEY (MaTour) REFERENCES TourDuLich(MaTour)
);

-- Bảng Đánh giá
CREATE TABLE DanhGia (
    MaDanhGia INT IDENTITY(1,1) PRIMARY KEY,
    MaNguoiDung INT,
    MaTour INT,
    DiemDanhGia INT,
    BinhLuan NVARCHAR(MAX),
    NgayDanhGia DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (MaNguoiDung) REFERENCES NguoiDung(MaNguoiDung),
    FOREIGN KEY (MaTour) REFERENCES TourDuLich(MaTour)
);

-- Bảng Thanh toán
CREATE TABLE ThanhToan (
    MaThanhToan INT IDENTITY(1,1) PRIMARY KEY,
    MaDatCho INT,
    SoTien DECIMAL(10, 2),
    NgayThanhToan DATETIME DEFAULT CURRENT_TIMESTAMP,
    PhuongThuc NVARCHAR(50),
    FOREIGN KEY (MaDatCho) REFERENCES DatCho(MaDatCho)
);

-- Bảng Quản trị viên
CREATE TABLE QuanTriVien (
    MaQuanTriVien INT IDENTITY(1,1) PRIMARY KEY,
    TenDangNhap NVARCHAR(50) UNIQUE NOT NULL,
    MatKhau NVARCHAR(255) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL
);

CREATE TABLE Reservation (
    Id INT PRIMARY KEY IDENTITY,
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    Destination NVARCHAR(100) NOT NULL,
    Participants INT NOT NULL
);

-- Chèn dữ liệu mẫu vào bảng Người dùng
INSERT INTO NguoiDung (TenDangNhap, MatKhau, Email, HoTen, SoDienThoai, DiaChi)
VALUES
    ('user1', 'password1', 'user1@gmail.com', 'khach 1', '123456789', '141 DBP'),
    ('user2', 'password2', 'user2@gmail.com', 'khach 2', '987654321', '276 DBP');

-- Chèn dữ liệu mẫu vào bảng Quản trị viên
INSERT INTO QuanTriVien (TenDangNhap, MatKhau, Email)
VALUES
    ('admin', 'admin123', 'admin@gmail.com');

-- Chèn dữ liệu mẫu vào bảng Điểm đến
INSERT INTO DiemDen (TenDiemDen, MoTa, ViTri, LinkAnh)
VALUES
    ('Đà Nẵng', 'Thành phố Đà Nẵng nổi tiếng với bãi biển Mỹ Khê và cầu Rồng.', 'Việt Nam', 'da-nang.jpg'),
    ('Hạ Long', 'Vịnh Hạ Long nổi tiếng với hàng ngàn đảo đá vôi đặc trưng.', 'Việt Nam', 'ha-long.jpg'),
    ('Phú Quốc', 'Hòn đảo Phú Quốc là một trong những địa điểm du lịch nổi tiếng của Việt Nam.', 'Việt Nam', 'phu-quoc.jpg'),
    ('Sapa', 'Thị trấn Sapa thuộc tỉnh Lào Cai, là điểm đến phổ biến với khách du lịch trong và ngoài nước.', 'Việt Nam', 'sapa.jpg'),
    ('Bangkok', 'Thủ đô của Thái Lan, Bangkok nổi tiếng với các đền chùa, các khu chợ đêm và cuộc sống về đêm sôi động.', 'Thái Lan', 'bangkok.jpg'),
    ('Tokyo', 'Thủ đô của Nhật Bản, Tokyo là một thành phố hiện đại với các khu mua sắm, đền chùa và văn hóa ẩm thực đa dạng.', 'Nhật Bản', 'tokyo.jpg');

-- Chèn dữ liệu mẫu vào bảng Tour du lịch
INSERT INTO TourDuLich (MaDiemDen, TenTour, MoTa, Gia, NgayBatDau, NgayKetThuc, LinkAnh)
VALUES
    (1, 'Tour Đà Nẵng 3 ngày 2 đêm', 'Khám phá các điểm du lịch nổi tiếng tại Đà Nẵng trong 3 ngày 2 đêm.', 1500000, '2024-07-10', '2024-07-12', 'tour-da-nang-3-ngay.jpg'),
    (2, 'Tour Hạ Long 4 ngày 3 đêm', 'Tham quan vịnh Hạ Long và các hòn đảo xinh đẹp trong 4 ngày 3 đêm.', 2000000, '2024-08-05', '2024-08-08', 'tour-ha-long-4-ngay.jpg'),
    (3, 'Tour Phú Quốc 5 ngày 4 đêm', 'Trải nghiệm cuộc sống nghỉ dưỡng tại hòn đảo Phú Quốc trong 5 ngày 4 đêm.', 2500000, '2024-09-15', '2024-09-19', 'tour-phu-quoc-5-ngay.jpg'),
    (4, 'Tour Sapa 2 ngày 1 đêm', 'Khám phá vẻ đẹp thiên nhiên và văn hóa dân tộc tại thị trấn Sapa trong 2 ngày 1 đêm.', 1200000, '2024-10-20', '2024-10-21', 'tour-sapa-2-ngay.jpg'),
    (5, 'Tour Bangkok 4 ngày 3 đêm', 'Tham quan các điểm du lịch nổi tiếng tại thủ đô Bangkok của Thái Lan trong 4 ngày 3 đêm.', 1800000, '2024-11-10', '2024-11-13', 'tour-bangkok-4-ngay.jpg'),
    (6, 'Tour Tokyo 7 ngày 6 đêm', 'Khám phá văn hóa hiện đại và truyền thống của thủ đô Tokyo trong 7 ngày 6 đêm.', 3000000, '2025-01-05', '2025-01-11', 'tour-tokyo-7-ngay.jpg');
	

