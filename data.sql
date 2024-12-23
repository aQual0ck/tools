INSERT INTO Roles(RoleName) 
VALUES ('Администратор'), 
('Пользователь');

INSERT INTO Category(CategoryName)
VALUES ('Газоанализатор'),
('Манометр показывающий'),
('Манометр технический'),
('Индикатор утечки газа'),
('Электрический счетчик');

INSERT INTO Users(Username, Password, FullName, RoleId)
VALUES ('admin', 'admin', 'Алексей Иванович Смирнов', 1),
('user', '12345', 'Дмитрий Сергеевич Кузнецов', 2)

INSERT INTO Tool(CategoryId, ModelName, OperatingSince, DecomissionedSince, SerialNumber)
VALUES (1, 'X100-Pro', '2020-01-15', NULL, 'SN12345AB'),
(2, 'Y200-Max', '2019-07-20', '2023-03-01', 'SN67890CD'),
(3, 'Z300-Prime', '2021-05-10', NULL, 'SN11223EF'),
(4, 'A400-Ultra', '2018-11-25', '2022-12-31', 'SN44556GH'),
(5, 'B500-Super', '2022-03-30', NULL, 'SN77889IJ'),
(1, 'C600-Elite', '2017-09-05', '2023-01-15', 'SN99001KL'),
(2, 'D700-Pro', '2020-06-18', NULL, 'SN22334MN'),
(3, 'E800-Max', '2021-08-12', NULL, 'SN55667OP'),
(4, 'F900-Prime', '2019-03-03', '2022-08-20', 'SN88990QR'),
(5, 'G1000-Ultra', '2023-01-01', NULL, 'SN11212ST');