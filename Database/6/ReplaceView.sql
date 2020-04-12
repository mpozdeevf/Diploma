DROP VIEW IF EXISTS group_subject_v;
CREATE VIEW group_subject_v AS
SELECT
  g.id AS group_id,
  gsl.semester_number AS semester,
  s.shortname AS subject_name,
  gsl.subject_id
FROM group_subject_link gsl
  INNER JOIN student_group g
    ON gsl.group_id = g.id
  INNER JOIN subject s
    ON gsl.subject_id = s.id;