DELETE FROM students;
DELETE FROM groups;
DELETE FROM lecturers_departments;
DELETE FROM departments;
DELETE FROM institutes;
DELETE FROM staff;

INSERT INTO staff (name, surname, patronymic)
  VALUES ('�����', '�������', '��������');
INSERT INTO staff (name, surname, patronymic)
  VALUES ('���������', '������������', '����������');
INSERT INTO staff (name, surname, patronymic)
  VALUES ('���������', '��������', '��������');
INSERT INTO staff (name, surname, patronymic)
  VALUES ('��������', '�������', '����������');

INSERT INTO institutes (name, director_id, director_deputy_id)
  VALUES ('�������� \"����������� � �������������� �������\"', (SELECT s.id FROM staff s WHERE s.surname = '�������'), (SELECT s.id FROM staff s WHERE s.surname = '��������'));

INSERT INTO departments (institute_id, name, head_id)
  VALUES ((SELECT i.id FROM institutes i WHERE i.name = '�������� \"����������� � �������������� �������\"'), '������� \"����������� �����������\"', (SELECT s.id FROM staff s WHERE s.surname = '������������'));

INSERT INTO lecturers_departments (department_id, lecturer_id)
  VALUES ((SELECT d.id FROM departments d WHERE d.name = '������� \"����������� �����������\"'), (SELECT s.id FROM staff s WHERE s.surname = '�������'));
INSERT INTO lecturers_departments (department_id, lecturer_id)
  VALUES ((SELECT d.id FROM departments d WHERE d.name = '������� \"����������� �����������\"'), (SELECT s.id FROM staff s WHERE s.surname = '������������'));

INSERT INTO groups (name, department_id)
  VALUES ('�08-191-2', (SELECT d.id FROM departments d WHERE d.name = '������� \"����������� �����������\"'));

INSERT INTO students (student_number, name, surname, patronymic, group_id)
  VALUES (1, '������', '�������', '�������', (SELECT g.id FROM groups g WHERE g.name = '�08-191-2'));
INSERT INTO students (student_number, name, surname, patronymic, group_id)
  VALUES (2, '����', '�������', '��������', (SELECT g.id FROM groups g WHERE g.name = '�08-191-2'));
