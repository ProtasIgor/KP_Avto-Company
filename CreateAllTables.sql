
-- �����������
CREATE TABLE Company (
    Id INT IDENTITY(1,1) PRIMARY KEY, -- �������������������� ���� id
    Name NVARCHAR(255),
    License NVARCHAR(255) NULL,
    �ddress NVARCHAR(255) NULL,
    Phone NVARCHAR(13) NULL,
    Mail NVARCHAR(255) NULL
);

-- ��� ������� ��������� ���������
CREATE TABLE Type_Facility (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255)
);

-- ������ ��������� ���������
CREATE TABLE Facility (
    Id INT IDENTITY(1,1) PRIMARY KEY,
	CompanyId INT,
	TypeFacilityId INT,
    Name NVARCHAR(255),
    Square INT NULL,
    Address NVARCHAR(255) NULL,
    Phone NVARCHAR(13) NULL,
	FOREIGN KEY (CompanyId) REFERENCES Company(Id),
	FOREIGN KEY (TypeFacilityId) REFERENCES Type_Facility(Id)
);

-- �������
CREATE TABLE Brigade (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CompanyId INT,
    Name NVARCHAR(255),
    Phone NVARCHAR(13) NULL,
    FOREIGN KEY (CompanyId) REFERENCES Company(Id)
);

-- ��� ����������
CREATE TABLE Type_Employee (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255)
);

-- ���������
CREATE TABLE Employee (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    BrigadeId INT,
    TypeEmployeeId INT,
	AvtoId INT NULL,
	Name NVARCHAR(255),
    Surname NVARCHAR(255),
    Patronymic NVARCHAR(255),
    Age INT NULL,
    Phone NVARCHAR(13) NULL,
    Experience DECIMAL(5, 2) NULL, -- Example: 5 years and 3 months
    FOREIGN KEY (BrigadeId) REFERENCES Brigade(Id),
    FOREIGN KEY (TypeEmployeeId) REFERENCES Type_Employee(Id),
	FOREIGN KEY (AvtoId) REFERENCES Avto(Id)
);

-- ������ �����������

-- ��������� ����
CREATE TABLE Nachalnik_Tsekha (
    Id INT IDENTITY(1,1) PRIMARY KEY,
	CompanyId INT,
	Name NVARCHAR(255),
    Surname NVARCHAR(255),
    Patronymic NVARCHAR(255),
    Age INT NULL,
    Phone NVARCHAR(13) NULL,
	FOREIGN KEY (CompanyId) REFERENCES Company(Id)
);

-- ��������� �������
CREATE TABLE Nachalnik_Uchastka (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nachalnik_TsekhaId INT,
	Name NVARCHAR(255),
    Surname NVARCHAR(255),
    Patronymic NVARCHAR(255),
    Age INT NULL,
    Phone NVARCHAR(13) NULL,
    FOREIGN KEY (Nachalnik_TsekhaId) REFERENCES Nachalnik_Tsekha(Id)
);

-- ������
CREATE TABLE Master (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nachalnik_UchastkaId INT,
	Name NVARCHAR(255),
    Surname NVARCHAR(255),
    Patronymic NVARCHAR(255),
    Age INT NULL,
    Phone NVARCHAR(13) NULL,
    FOREIGN KEY (Nachalnik_UchastkaId) REFERENCES Nachalnik_Uchastka(Id)
);

-- ��������
CREATE TABLE Brigadier (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    BrigadeId INT,
    MasterId INT,
	Name NVARCHAR(255),
    Surname NVARCHAR(255),
    Patronymic NVARCHAR(255),
    Age INT NULL,
    Phone NVARCHAR(13) NULL,
    FOREIGN KEY (BrigadeId) REFERENCES Brigade(Id),
    FOREIGN KEY (MasterId) REFERENCES Master(Id)
);

-- �� ��� ����

-- ��� ����
CREATE TABLE Type_Avto (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255)
);

-- ������ ����
CREATE TABLE Status_Avto (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255)
);

-- ����� ����
CREATE TABLE Marka_Avto (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Marka NVARCHAR(255),
    Model NVARCHAR(255)
);

-- ����
CREATE TABLE Avto (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FacilityId INT,
    StatusId INT,
    MarkaId INT,
	TypeAvtoId INT,
    Color NVARCHAR(255),
    Weight INT  NULL,
    Cost INT NULL,
	LiftingCapacity INT NULL,
	PeopleCapacity INT NULL,
    Horsepower INT NULL,
    ReceptionDateTime DATETIME NULL,
    DisposalDateTime DATETIME NULL,
    FOREIGN KEY (FacilityId) REFERENCES Facility(Id),
    FOREIGN KEY (StatusId) REFERENCES Status_Avto(Id),
    FOREIGN KEY (MarkaId) REFERENCES Marka_Avto(Id),
	FOREIGN KEY (TypeAvtoId) REFERENCES Type_Avto(Id)
);

-- �������

-- �������
CREATE TABLE Route (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255),
    StartPoint NVARCHAR(255),
    EndPoint NVARCHAR(255),
    Distance DECIMAL(10, 2)
);

-- ��� �������
CREATE TABLE Type_Drive (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255)
);

-- �������
CREATE TABLE Drive (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    AvtoId INT,
    TypeDriveId INT,
    RouteId INT,
    Distance DECIMAL(10, 2),
    FuelConsumption DECIMAL(10, 2), -- ����� �������� ��� ����
	�apacity INT NULL,
	cargoWeight INT NULL,
    StartDrive DATETIME NULL,
    FinishDrive DATETIME NULL,
    FOREIGN KEY (AvtoId) REFERENCES Avto(Id),
    FOREIGN KEY (TypeDriveId) REFERENCES Type_Drive(Id),
    FOREIGN KEY (RouteId) REFERENCES Route(Id)
);

-- ���� ��� �������
CREATE TABLE Node_To_Repair (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255),
	Cost DECIMAL(10, 2)
);

-- ������
CREATE TABLE Repair (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    AvtoId INT,
    EmployeeId INT,
    NodeId INT,
    StartDateTime DATETIME NULL,
    EndDateTime DATETIME NULL,
    TotalWorkTime DATETIME NULL,
    NumOfRepairedNodes INT,
    FOREIGN KEY (AvtoId) REFERENCES Avto(Id),
    FOREIGN KEY (EmployeeId) REFERENCES Employee(Id),
	FOREIGN KEY (NodeId) REFERENCES Node_To_Repair(Id)
);
















































-- ���������� ��������
INSERT INTO Type_Facility
VALUES
('���'),
('�����'),
('����')


INSERT INTO Type_Avto
VALUES
('�������'),
('�����'),
('���������� �����'),
('�������� ���������'),
('�������� ���������'),
('��������� ���������������� ���������')


INSERT INTO Type_Employee
VALUES
('��������'),
('������'),
('�������'),
('�������'),
('�������')


INSERT INTO Type_Drive
VALUES
('������������'),
('��������')


INSERT INTO Node_To_Repair
VALUES
('���������', 10000),
('���', 7000),
('����', 12000),
('�����', 11500)

INSERT INTO Status_Avto
VALUES
('��������'),
('������'),
('��������'),
('������'),
('C�����')

INSERT INTO Marka_Avto
VALUES
('Opel', 'Zafira'),
('Ford', 'Mondeo'),
('BMW', 'X5'),
('Mercedes-Benz', 'E-Class'),
('Audi', 'A4'),
('Toyota', 'Camry'),
('Volkswagen', 'Golf'),
('MAN', 'Lion'),
('Volvo', '7900 Electric'),
('Scania', 'Citywide LF'),
('Mercedes-Benz', 'Citaro'),
('Solaris', 'Urbino 18'),
('Ford', 'Transit'),
('Iveco', 'Daily'),
('Mercedes-Benz', 'Actros'),
('Volvo', 'FH'),
('Scania', 'R-series')
















drop table Company