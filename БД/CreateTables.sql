DROP TABLE IF EXISTS students;
DROP TABLE IF EXISTS groups;
DROP TABLE IF EXISTS lecturers_departments;
DROP TABLE IF EXISTS departments;
DROP TABLE IF EXISTS institutes;
DROP TABLE IF EXISTS staff;

CREATE TABLE staff (
id SERIAL PRIMARY KEY,
name TEXT NOT NULL,
surname TEXT NOT NULL,
patronymic TEXT
);

CREATE TABLE institutes (
id SERIAL PRIMARY KEY,
name TEXT UNIQUE NOT NULL,
director_id INTEGER REFERENCES staff (id),
director_deputy_id INTEGER REFERENCES staff (id)
);

CREATE TABLE departments (
id SERIAL PRIMARY KEY,
institute_id INTEGER REFERENCES institutes (id),
name TEXT UNIQUE NOT NULL,
head_id INTEGER REFERENCES staff (id)
);

CREATE TABLE lecturers_departments (
department_id INTEGER REFERENCES departments (id),
lecturer_id INTEGER REFERENCES staff (id),
CONSTRAINT pk PRIMARY KEY (department_id, lecturer_id)
);

CREATE TABLE groups (
id SERIAL PRIMARY KEY,
name TEXT UNIQUE NOT NULL,
department_id INTEGER REFERENCES departments (id)
);

CREATE TABLE students (
student_number TEXT PRIMARY KEY,
name TEXT NOT NULL,
surname TEXT NOT NULL,
patronymic TEXT,
group_id INTEGER REFERENCES groups (id)
);


