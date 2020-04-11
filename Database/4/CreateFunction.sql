CREATE OR REPLACE FUNCTION get_surname_initials (surname TEXT, name TEXT, patronymic TEXT)
RETURNS TEXT AS $$
  BEGIN
  RETURN CONCAT(surname, ' ', SUBSTRING(name, 1, 1), '.') || COALESCE((SUBSTRING(patronymic, 1, 1) || '.'), '');
  END; $$
LANGUAGE PLPGSQL;
  