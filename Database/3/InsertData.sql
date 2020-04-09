DELETE FROM groups_subjects;
DELETE FROM sub_subjects;
DELETE FROM subjects;
DELETE FROM students_requisites;
DELETE FROM groups;
DELETE FROM students;
DELETE FROM staff_departments;
DELETE FROM departments;
DELETE FROM institutes;
DELETE FROM staff;

INSERT INTO staff (name, surname, patronymic, surname_initials)
  VALUES ('�����', '�������', '��������', '������� �.�.');
INSERT INTO staff (name, surname, patronymic, surname_initials)
  VALUES ('���������', '������������', '����������', '������������ �.�.');
INSERT INTO staff (name, surname, patronymic, surname_initials)
  VALUES ('���������', '��������', '��������', '�������� �.�.');
INSERT INTO staff (name, surname, patronymic, surname_initials)
  VALUES ('��������', '�������', '����������', '������� �.�.');

INSERT INTO institutes (name, shortname, director_id, director_deputy_id)
  VALUES ('�������� "����������� � �������������� �������"', '�������� ���', (SELECT s.id FROM staff s WHERE s.surname = '�������'), (SELECT s.id FROM staff s WHERE s.surname = '��������'));

INSERT INTO departments (institute_id, name, shortname, director_id)
  VALUES ((SELECT i.id FROM institutes i WHERE i.name = '�������� "����������� � �������������� �������"'), '������� "����������� �����������"', '������� ��', (SELECT s.id FROM staff s WHERE s.surname = '������������'));

INSERT INTO staff_departments (department_id, staff_id)
  VALUES ((SELECT d.id FROM departments d WHERE d.name = '������� "����������� �����������"'), (SELECT s.id FROM staff s WHERE s.surname = '�������'));
INSERT INTO staff_departments (department_id, staff_id)
  VALUES ((SELECT d.id FROM departments d WHERE d.name = '������� "����������� �����������"'), (SELECT s.id FROM staff s WHERE s.surname = '������������'));
INSERT INTO staff_departments (department_id, staff_id)
  VALUES ((SELECT d.id FROM departments d WHERE d.name = '������� "����������� �����������"'), (SELECT s.id FROM staff s WHERE s.surname = '�������'));
INSERT INTO staff_departments (department_id, staff_id)
  VALUES ((SELECT d.id FROM departments d WHERE d.name = '������� "����������� �����������"'), (SELECT s.id FROM staff s WHERE s.surname = '��������'));

INSERT INTO students (student_number, name, surname, patronymic, surname_initials)
  VALUES (1, '������', '�������', '�������', '������� �.�.');
INSERT INTO students (student_number, name, surname, patronymic, surname_initials)
  VALUES (2, '����', '�������', '��������', '������� �.�.');
INSERT INTO students (student_number, name, surname, surname_initials)
  VALUES (3, '������', '����', '���� �.');

INSERT INTO groups (name, department_id, head_id)
  VALUES ('�08-191-2', (SELECT d.id FROM departments d WHERE d.name = '������� "����������� �����������"'), (SELECT s.id FROM students s WHERE s.surname = '����'));

INSERT INTO students_requisites (student_id, group_id, e_mail, mobile_phone_number, home_phone_number, photo_link)
  VALUES ((SELECT s.id FROM students s WHERE s.student_number = '1'), (SELECT g.id FROM groups g WHERE g.name = '�08-191-2'), '@gmail.com', '322-228', '322-228', NULL);
INSERT INTO students_requisites (student_id, group_id, mobile_phone_number, home_phone_number, photo_link)
  VALUES ((SELECT s.id FROM students s WHERE s.student_number = '2'), (SELECT g.id FROM groups g WHERE g.name = '�08-191-2'), '322-228', '322-228', NULL);
INSERT INTO students_requisites (student_id, group_id, e_mail, mobile_phone_number, photo_link)
  VALUES ((SELECT s.id FROM students s WHERE s.student_number = '3'), (SELECT g.id FROM groups g WHERE g.name = '�08-191-2'), '@gmail.com', '322-228', NULL);
