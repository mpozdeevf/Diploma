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
  VALUES ('Игорь', 'Архипов', 'Олегович');
INSERT INTO staff (name, surname, patronymic)
  VALUES ('Александр', 'Коробейников', 'Васильевич');
INSERT INTO staff (name, surname, patronymic)
  VALUES ('Валентина', 'Соболева', 'Павловна');
INSERT INTO staff (name, surname, patronymic)
  VALUES ('Владимир', 'Тарасов', 'Георгиевич');

INSERT INTO institute (name, shortname, director_id, director_deputy_id)
  VALUES ('Институт "Информатика и вычислительная техника"', 'Институт ИВТ', (SELECT s.id FROM staff s WHERE s.surname = 'Архипов'), (SELECT s.id FROM staff s WHERE s.surname = 'Соболева'));

INSERT INTO department (institute_id, name, shortname, director_id)
  VALUES ((SELECT i.id FROM institute i WHERE i.name = 'Институт "Информатика и вычислительная техника"'), 'Кафедра "Программное обеспечение"', 'Кафедра ПО', (SELECT s.id FROM staff s WHERE s.surname = 'Коробейников'));

INSERT INTO staff_department_link (department_id, staff_id)
  VALUES ((SELECT d.id FROM department d WHERE d.name = 'Кафедра "Программное обеспечение"'), (SELECT s.id FROM staff s WHERE s.surname = 'Тарасов'));
INSERT INTO staff_department_link (department_id, staff_id)
  VALUES ((SELECT d.id FROM department d WHERE d.name = 'Кафедра "Программное обеспечение"'), (SELECT s.id FROM staff s WHERE s.surname = 'Коробейников'));
INSERT INTO staff_department_link (department_id, staff_id)
  VALUES ((SELECT d.id FROM department d WHERE d.name = 'Кафедра "Программное обеспечение"'), (SELECT s.id FROM staff s WHERE s.surname = 'Архипов'));
INSERT INTO staff_department_link (department_id, staff_id)
  VALUES ((SELECT d.id FROM department d WHERE d.name = 'Кафедра "Программное обеспечение"'), (SELECT s.id FROM staff s WHERE s.surname = 'Соболева'));

INSERT INTO student (student_number, name, surname, patronymic)
  VALUES (1, 'Максим', 'Поздеев', 'Львович');
INSERT INTO student (student_number, name, surname, patronymic)
  VALUES (2, 'Иван', 'Зырянов', 'Олегович');
INSERT INTO student (student_number, name, surname)
  VALUES (3, 'Никита', 'Штэк');

INSERT INTO student_group (name, department_id, head_id)
  VALUES ('Б08-191-2', (SELECT d.id FROM department d WHERE d.name = 'Кафедра "Программное обеспечение"'), (SELECT s.id FROM student s WHERE s.surname = 'Штэк'));

INSERT INTO student_requisite (student_id, group_id, e_mail, mobile_phone_number, home_phone_number, photo_link)
  VALUES ((SELECT s.id FROM student s WHERE s.student_number = '1'), (SELECT g.id FROM student_group g WHERE g.name = 'Б08-191-2'), '@gmail.com', '322-228', '322-228', NULL);
INSERT INTO student_requisite (student_id, group_id, mobile_phone_number, home_phone_number, photo_link)
  VALUES ((SELECT s.id FROM student s WHERE s.student_number = '2'), (SELECT g.id FROM student_group g WHERE g.name = 'Б08-191-2'), '322-228', '322-228', NULL);
INSERT INTO student_requisite (student_id, group_id, e_mail, mobile_phone_number, photo_link)
  VALUES ((SELECT s.id FROM student s WHERE s.student_number = '3'), (SELECT g.id FROM student_group g WHERE g.name = 'Б08-191-2'), '@gmail.com', '322-228', NULL);
