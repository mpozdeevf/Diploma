DELETE FROM group_subject_link;
DELETE FROM sub_subject;
DELETE FROM subject;
DELETE FROM student_requisite;
DELETE FROM student_group;
DELETE FROM student;
DELETE FROM staff_department_link;
DELETE FROM department;
DELETE FROM institute;
DELETE FROM staff;

INSERT INTO staff (name, surname, patronymic)
  VALUES ('�����', '�������', '��������');
INSERT INTO staff (name, surname, patronymic)
  VALUES ('���������', '������������', '����������');
INSERT INTO staff (name, surname, patronymic)
  VALUES ('���������', '��������', '��������');
INSERT INTO staff (name, surname, patronymic)
  VALUES ('��������', '�������', '����������');

INSERT INTO institute (name, shortname, director_id, director_deputy_id)
  VALUES ('�������� "����������� � �������������� �������"', '�������� ���', (SELECT s.id FROM staff s WHERE s.surname = '�������'), (SELECT s.id FROM staff s WHERE s.surname = '��������'));

INSERT INTO department (institute_id, name, shortname, director_id)
  VALUES ((SELECT i.id FROM institute i WHERE i.name = '�������� "����������� � �������������� �������"'), '������� "����������� �����������"', '������� ��', (SELECT s.id FROM staff s WHERE s.surname = '������������'));

INSERT INTO staff_department_link (department_id, staff_id)
  VALUES ((SELECT d.id FROM department d WHERE d.name = '������� "����������� �����������"'), (SELECT s.id FROM staff s WHERE s.surname = '�������'));
INSERT INTO staff_department_link (department_id, staff_id)
  VALUES ((SELECT d.id FROM department d WHERE d.name = '������� "����������� �����������"'), (SELECT s.id FROM staff s WHERE s.surname = '������������'));
INSERT INTO staff_department_link (department_id, staff_id)
  VALUES ((SELECT d.id FROM department d WHERE d.name = '������� "����������� �����������"'), (SELECT s.id FROM staff s WHERE s.surname = '�������'));
INSERT INTO staff_department_link (department_id, staff_id)
  VALUES ((SELECT d.id FROM department d WHERE d.name = '������� "����������� �����������"'), (SELECT s.id FROM staff s WHERE s.surname = '��������'));

INSERT INTO student (student_number, name, surname, patronymic)
  VALUES (1, '������', '�������', '�������');
INSERT INTO student (student_number, name, surname, patronymic)
  VALUES (2, '����', '�������', '��������');
INSERT INTO student (student_number, name, surname)
  VALUES (3, '������', '����');

INSERT INTO student_group (name, department_id, head_id)
  VALUES ('�08-191-2', (SELECT d.id FROM department d WHERE d.name = '������� "����������� �����������"'), (SELECT s.id FROM student s WHERE s.surname = '����'));

INSERT INTO student_requisite (student_id, group_id, e_mail, mobile_phone_number, home_phone_number, photo_link)
  VALUES ((SELECT s.id FROM student s WHERE s.student_number = '1'), (SELECT g.id FROM student_group g WHERE g.name = '�08-191-2'), '@gmail.com', '322-228', '322-228', NULL);
INSERT INTO student_requisite (student_id, group_id, mobile_phone_number, home_phone_number, photo_link)
  VALUES ((SELECT s.id FROM student s WHERE s.student_number = '2'), (SELECT g.id FROM student_group g WHERE g.name = '�08-191-2'), '322-228', '322-228', NULL);
INSERT INTO student_requisite (student_id, group_id, e_mail, mobile_phone_number, photo_link)
  VALUES ((SELECT s.id FROM student s WHERE s.student_number = '3'), (SELECT g.id FROM student_group g WHERE g.name = '�08-191-2'), '@gmail.com', '322-228', NULL);
