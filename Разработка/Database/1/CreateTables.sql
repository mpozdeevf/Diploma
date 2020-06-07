DROP TABLE IF EXISTS staff_auth_data;
DROP TABLE IF EXISTS students_auth_data;
DROP TABLE IF EXISTS schedule;
DROP TABLE IF EXISTS classes;
DROP TABLE IF EXISTS groups_subjects;
DROP TABLE IF EXISTS sub_subjects;
DROP TABLE IF EXISTS subjects;
DROP TABLE IF EXISTS subjects_information;
DROP TABLE IF EXISTS news_receivers;
DROP TABLE IF EXISTS news;
DROP TABLE IF EXISTS student_requisites;
DROP TABLE IF EXISTS groups;
DROP TABLE IF EXISTS students;
DROP TABLE IF EXISTS staff_departments;
DROP TABLE IF EXISTS departments;
DROP TABLE IF EXISTS staff_requisites;
DROP TABLE IF EXISTS institutes;
DROP TABLE IF EXISTS staff;

CREATE TABLE staff (
id SERIAL PRIMARY KEY,
name VARCHAR(50) NOT NULL,
surname VARCHAR(50) NOT NULL,
patronymic VARCHAR(50)
);

CREATE TABLE institutes (
id SERIAL PRIMARY KEY,
name VARCHAR(50) UNIQUE NOT NULL,
short_name VARCHAR(25),
director_id INTEGER UNIQUE REFERENCES staff (id) UNIQUE NOT NULL,
director_deputy_id INTEGER UNIQUE REFERENCES staff (id) UNIQUE  NOT NULL
);

CREATE TABLE staff_requisites (
staff_id INTEGER REFERENCES staff (id) PRIMARY KEY,
e_mail VARCHAR(50),
mobile_phone VARCHAR(50),
home_phone VARCHAR(50)
);

CREATE TABLE departments (
id SERIAL PRIMARY KEY,
institute_id INTEGER REFERENCES institutes (id) NOT NULL,
name VARCHAR(50) UNIQUE NOT NULL,
short_name VARCHAR(25),
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
patronymic VARCHAR(50)
);

CREATE TABLE groups (
id SERIAL PRIMARY KEY,
name VARCHAR(25) UNIQUE NOT NULL,
department_id INTEGER REFERENCES departments (id) NOT NULL,
head_id INTEGER REFERENCES students (id) NOT NULL
);

CREATE TABLE student_requisites (
student_id INTEGER REFERENCES students (id) PRIMARY KEY,
group_id INTEGER REFERENCES groups (id) NOT NULL,
e_mail VARCHAR(50),
mobile_phone_number VARCHAR(50),
home_phone_number VARCHAR(50)
);

CREATE TABLE news (
id SERIAL PRIMARY KEY,
text TEXT NOT NULL,
title VARCHAR(300) NOT NULL,
author_student_id INTEGER REFERENCES students (id),
author_staff_id INTEGER REFERENCES staff (id),
date TIMESTAMP NOT NULL
);

CREATE TABLE news_receivers (
id SERIAL PRIMARY KEY,
news_id INTEGER REFERENCES news (id) NOT NULL,
group_id INTEGER REFERENCES groups (id),
staff_id INTEGER REFERENCES staff (id),
department_id INTEGER REFERENCES departments (id),
institute_id INTEGER REFERENCES institutes (id)
);

CREATE TABLE subjects_information (
id SERIAL PRIMARY KEY,
name VARCHAR(50) UNIQUE NOT NULL,
short_name VARCHAR(25),
description TEXT
);

CREATE TABLE subjects (
id SERIAL PRIMARY KEY,
lecturer_id INTEGER REFERENCES staff (id) NOT NULL,
information_id INTEGER REFERENCES subjects_information (id) NOT NULL
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
semester INTEGER CHECK (semester > 0 AND semester < 11) DEFAULT 1 NOT NULL,
CONSTRAINT gs_pk PRIMARY KEY (group_id, subject_id)
);

CREATE TABLE classes (
id SERIAL PRIMARY KEY,
start_time VARCHAR(25) UNIQUE NOT NULL,
end_time VARCHAR(25) UNIQUE NOT NULL,
number INTEGER UNIQUE NOT NULL
);

CREATE TABLE schedule (
id SERIAL PRIMARY KEY,
class_id INTEGER REFERENCES classes (id) NOT NULL,
group_id INTEGER REFERENCES groups (id),
subject_id INTEGER REFERENCES subjects (id) NOT NULL,
sub_subject_id INTEGER REFERENCES sub_subjects (id),
date TIMESTAMP NOT NULL,
week_line BOOLEAN NOT NULL,
location VARCHAR(25) NOT NULL
);

CREATE TABLE students_auth_data (
student_id INTEGER REFERENCES students (id) PRIMARY KEY,
username VARCHAR(25) UNIQUE NOT NULL,
password VARCHAR(100) NOT NULL,
salt VARCHAR(100) NOT NULL,
refresh_token VARCHAR(100)
);

CREATE TABLE staff_auth_data (
staff_id INTEGER REFERENCES staff (id) PRIMARY KEY,
username VARCHAR(25) UNIQUE NOT NULL,
password VARCHAR(100) NOT NULL,
salt VARCHAR(100) NOT NULL,
refresh_token VARCHAR(100)
);




