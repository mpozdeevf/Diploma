DELETE FROM students;
DELETE FROM groups;
DELETE FROM lecturers_departments;
DELETE FROM departments;
DELETE FROM institutes;
DELETE FROM staff;

INSERT INTO staff (name, surname, patronymic)
  VALUES ('Игорь', 'Архипов', 'Олегович');
INSERT INTO staff (name, surname, patronymic)
  VALUES ('Александр', 'Коробейников', 'Васильевич');
INSERT INTO staff (name, surname, patronymic)
  VALUES ('Валентина', 'Соболева', 'Павловна');
INSERT INTO staff (name, surname, patronymic)
  VALUES ('Владимир', 'Тарасов', 'Георгиевич');

INSERT INTO institutes (name, director_id, director_deputy_id)
  VALUES ('Институт \"Информатика и вычислительная техника\"', (SELECT s.id FROM staff s WHERE s.surname = 'Архипов'), (SELECT s.id FROM staff s WHERE s.surname = 'Соболева'));

INSERT INTO departments (institute_id, name, head_id)
  VALUES ((SELECT i.id FROM institutes i WHERE i.name = 'Институт \"Информатика и вычислительная техника\"'), 'Кафедра \"Программное обеспечение\"', (SELECT s.id FROM staff s WHERE s.surname = 'Коробейников'));

INSERT INTO lecturers_departments (department_id, lecturer_id)
  VALUES ((SELECT d.id FROM departments d WHERE d.name = 'Кафедра \"Программное обеспечение\"'), (SELECT s.id FROM staff s WHERE s.surname = 'Тарасов'));
INSERT INTO lecturers_departments (department_id, lecturer_id)
  VALUES ((SELECT d.id FROM departments d WHERE d.name = 'Кафедра \"Программное обеспечение\"'), (SELECT s.id FROM staff s WHERE s.surname = 'Коробейников'));

INSERT INTO groups (name, department_id)
  VALUES ('Б08-191-2', (SELECT d.id FROM departments d WHERE d.name = 'Кафедра \"Программное обеспечение\"'));

INSERT INTO students (student_number, name, surname, patronymic, group_id)
  VALUES (1, 'Максим', 'Поздеев', 'Львович', (SELECT g.id FROM groups g WHERE g.name = 'Б08-191-2'));
INSERT INTO students (student_number, name, surname, patronymic, group_id)
  VALUES (2, 'Иван', 'Зырянов', 'Олегович', (SELECT g.id FROM groups g WHERE g.name = 'Б08-191-2'));
