DROP TABLE IF EXISTS group_subject_link;
DROP TABLE IF EXISTS sub_subject;
DROP TABLE IF EXISTS subject;
DROP TABLE IF EXISTS student_requisite;
DROP TABLE IF EXISTS student_group;
DROP TABLE IF EXISTS student;
DROP TABLE IF EXISTS staff_department_link;
DROP TABLE IF EXISTS department;
DROP TABLE IF EXISTS institute;
DROP TABLE IF EXISTS staff;

CREATE TABLE staff (
id SERIAL PRIMARY KEY,
name VARCHAR(50) NOT NULL,
surname VARCHAR(50) NOT NULL,
patronymic VARCHAR(50)
);

CREATE TABLE institute (
id SERIAL PRIMARY KEY,
name VARCHAR(50) UNIQUE NOT NULL,
shortname VARCHAR(25),
director_id INTEGER UNIQUE REFERENCES staff (id) NOT NULL,
director_deputy_id INTEGER UNIQUE REFERENCES staff (id) NOT NULL
);

CREATE TABLE department (
id SERIAL PRIMARY KEY,
institute_id INTEGER REFERENCES institute (id) NOT NULL,
name VARCHAR(50) UNIQUE NOT NULL,
shortname VARCHAR(25),
director_id INTEGER UNIQUE REFERENCES staff (id)
);

CREATE TABLE staff_department_link (
department_id INTEGER REFERENCES department (id) NOT NULL,
staff_id INTEGER REFERENCES staff (id) NOT NULL,
CONSTRAINT sd_pk PRIMARY KEY (department_id, staff_id)
);

CREATE TABLE student (
id SERIAL PRIMARY KEY,
student_number VARCHAR(50) UNIQUE NOT NULL,
name VARCHAR(50) NOT NULL,
surname VARCHAR(50) NOT NULL,
patronymic VARCHAR(50)
);

CREATE TABLE student_group (
id SERIAL PRIMARY KEY,
name VARCHAR(25) UNIQUE NOT NULL,
department_id INTEGER REFERENCES department (id) NOT NULL,
head_id INTEGER REFERENCES student (id) NOT NULL
);

CREATE TABLE student_requisite (
student_id INTEGER REFERENCES student (id) PRIMARY KEY,
group_id INTEGER REFERENCES student_group (id) NOT NULL,
e_mail VARCHAR(50),
mobile_phone_number VARCHAR(50),
home_phone_number VARCHAR(50),
photo_link TEXT
);

CREATE TABLE subject (
id SERIAL PRIMARY KEY,
name VARCHAR(100) UNIQUE NOT NULL,
shortname VARCHAR(25),
lecturer_id INTEGER REFERENCES staff (id) NOT NULL
);

CREATE TABLE sub_subject (
id SERIAL PRIMARY KEY,
subject_id INTEGER REFERENCES subject (id) NOT NULL,
name VARCHAR(100) UNIQUE NOT NULL,
shortname VARCHAR(25),
sub_lecturer_id INTEGER REFERENCES staff (id) NOT NULL
);

CREATE TABLE group_subject_link (
group_id INTEGER REFERENCES student_group (id) NOT NULL,
subject_id INTEGER REFERENCES subject (id) NOT NULL,
semester_number INTEGER CHECK (semester_number > 0 AND semester_number < 11) DEFAULT 1 NOT NULL,
CONSTRAINT gs_pk PRIMARY KEY (group_id, subject_id)
);




