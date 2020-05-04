DROP TRIGGER IF EXISTS on_head_id_updated ON student_group;
DROP FUNCTION IF EXISTS on_head_id_updated;

CREATE FUNCTION on_head_id_updated ()
RETURNS trigger AS $$
  DECLARE
    new_head_group_id INTEGER;
  BEGIN
    SELECT gsv.group_id INTO new_head_group_id 
    FROM group_student_v gsv WHERE gsv.student_id = NEW.head_id;

    IF (NEW.id != new_head_group_id) THEN
      RAISE EXCEPTION 'Студент не из этой группы';
    END IF;
    RETURN NEW;
  END $$
LANGUAGE PLPGSQL;

CREATE TRIGGER on_head_id_updated BEFORE INSERT OR UPDATE ON student_group
  FOR EACH ROW EXECUTE PROCEDURE on_head_id_updated();
