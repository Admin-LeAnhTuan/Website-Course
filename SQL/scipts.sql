	USE MASTER
	GO

	IF EXISTS (SELECT NAME FROM SYS.DATABASES WHERE NAME = 'Course')
		DROP DATABASE Course
	GO

	CREATE DATABASE Course
	ON (NAME = 'Course_DAT', FILENAME = 'D:\Workspace_SQL\Web_Course\Course.MDF')
	LOG ON(NAME = 'Course_LOG', FILENAME = 'D:\Workspace_SQL\Web_Course\Course.LDF')
	GO

	USE Course
	GO

CREATE TABLE Category
(
	category_id int identity(1,1) primary key,
	Name nvarchar(255)

)
GO

CREATE TABLE Course
(
	course_id int identity(1,1) primary key,
	title nvarchar(255),
	description nvarchar(255),
	price nvarchar(255),
	duration nvarchar(255),
	userid nvarchar(128),
	img_course nvarchar(255),
	category_id int
	foreign key (userid) references AspNetUsers(Id),
	foreign key (category_id) references Category(category_id),
)
Go

CREATE TABLE Unit
(
	Unit_id int identity(1,1) primary key,
	lesson nvarchar(255),
	course_id int,
	foreign key(course_id) references Course(course_id)
)
Go

CREATE TABLE Test
(
	Test_id int identity(1,1) primary key,
	Test_unit_id int,
	Test_case nvarchar(255)
	foreign key(Test_unit_id) references Unit(Unit_id)
)
Go

CREATE TABLE Enrollment
(
	enrollment_id int identity(1,1) primary key, 
	 course_id int,
	 users_id nvarchar(128),
	 enrollment_date datetime,
	 statuss int
	foreign key (users_id) references AspNetUsers(Id)

)
GO

CREATE TABLE Payment
(
	payment_id int identity(1,1) primary key,
	enrollment_id int,
	payment_date datetime,
	amount float
	foreign key (enrollment_id) references Enrollment(enrollment_id)

)
GO

CREATE TABLE Review
(
	review_id int identity(1,1) primary key,
	course_id int,
	users_id nvarchar(128),
	rating float,
	comment text,
	review_date datetime
	foreign key (users_id) references AspNetUsers(Id)
)