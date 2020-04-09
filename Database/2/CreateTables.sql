DROP TABLE IF EXISTS groups_subjects;
DROP TABLE IF EXISTS sub_subjects;
DROP TABLE IF EXISTS subjects;
DROP TABLE IF EXISTS students_requisites;
DROP TABLE IF EXISTS groups;
DROP TABLE IF EXISTS students;
DROP TABLE IF EXISTS staff_departments;
DROP TABLE IF EXISTS departments;
DROP TABLE IF EXISTS institutes;
DROP TABLE IF EXISTS staff;

CREATE TABLE staff (
id SERIAL PRIMARY KEY,
name VARCHAR(50) NOT NULL,
surname VARCHAR(50) NOT NULL,
patronymic VARCHAR(50),
surname_initials VARCHAR(55) NOT NULL
);

CREATE TABLE institutes (
id SERIAL PRIMARY KEY,
name VARCHAR(50) UNIQUE NOT NULL,
shortname VARCHAR(25),
director_id INTEGER UNIQUE REFERENCES staff (id) NOT NULL,
director_deputy_id INTEGER UNIQUE REFERENCES staff (id) NOT NULL
);

CREATE TABLE departments (
id SERIAL PRIMARY KEY,
institute_id INTEGER REFERENCES institutes (id) NOT NULL,
name VARCHAR(50) UNIQUE NOT NULL,
shortname VARCHAR(25),
director_id INTEGER UNIQUE REFERENCES staff (id)
);

CREATE TABLE staff_departments (
department_id INTEGER REFERENCES departments (id) NOT NULL,
staff_id INTEGER REFERENCES staff (id) NOT NULL,
CONSTRAINT sd_pk PRIMARY KEY (department_id, staff_id)
);

CREATE TABLE students (
id SERIAL PRIMARY KEY,
student_number VARCHAR(50) UNIQUE NOT NULL,
name VARCHAR(50) NOT NULL,
surname VARCHAR(50) NOT NULL,
patronymic VARCHAR(50),
surname_initials VARCHAR(55) NOT NULL
);

CREATE TABLE groups (
id SERIAL PRIMARY KEY,
name VARCHAR(25) UNIQUE NOT NULL,
department_id INTEGER REFERENCES departments (id) NOT NULL,
head_id INTEGER REFERENCES students (id) NOT NULL
);

CREATE TABLE students_requisites (
student_id INTEGER REFERENCES students (id) PRIMARY KEY,
group_id INTEGER REFERENCES groups (id) NOT NULL,
e_mail VARCHAR(50),
mobile_phone_number VARCHAR(50),
home_phone_number VARCHAR(50),
photo_link TEXT
);

CREATE TABLE subjects (
id SERIAL PRIMARY KEY,
name VARCHAR(100) UNIQUE NOT NULL,
shortname VARCHAR(25),
lecturer_id INTEGER REFERENCES staff (id) NOT NULL
);

CREATE TABLE sub_subjects (
id SERIAL PRIMARY KEY,
subject_id INTEGER REFERENCES subjects (id) NOT NULL,
name VARCHAR(100) UNIQUE NOT NULL,
shortname VARCHAR(25),
sub_lecturer_id INTEGER REFERENCES staff (id) NOT NULL
);

CREATE TABLE groups_subjects (
group_id INTEGER REFERENCES groups (id) NOT NULL,
subject_id INTEGER REFERENCES subjects (id) NOT NULL,
semestr_number INTEGER CHECK (semestr_number > 0 AND semestr_number < 11) DEFAULT 1 NOT NULL,
CONSTRAINT gs_pk PRIMARY KEY (group_id, subject_id)
);




