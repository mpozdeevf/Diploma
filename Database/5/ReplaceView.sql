DROP VIEW IF EXISTS group_student_v;
CREATE VIEW group_student_v AS
SELECT
  g.name AS group_name,
  get_surname_initials(s.surname, s.name, s.patronymic) AS student_name,
  ROW_NUMBER() OVER (
  ORDER BY s.name, s.surname, s.patronymic) AS serial_number,
  s.id AS student_id,
  CASE
  	WHEN g.head_id = s.id THEN TRUE
  	ELSE FALSE
  END AS is_head_of_group
FROM student_requisite sr
  INNER JOIN student_group g
    ON sr.group_id = g.id
  INNER JOIN student s
    ON sr.student_id = s.id
ORDER BY s.name, s.surname, s.patronymic;