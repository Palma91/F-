CREATE TABLE  [dbo].Student(
StudentID int not null
constraint PrimaryKey primary key,
Name nvarchar(50) null,
Age int null)
GO
CREATE TABLE [dbo].Course(
CourseID int not null
constraint PrimaryKey2 primary key,
CourseName varchar(max) null);
GO
CREATE TABLE [dbo].AnotherStudentTable (
StudentID int not null
constraint PrimaryKey3 primary key,
Name nvarchar(50) null,
Age int null,);
go
CREATE TABLE [dbo].CourseSelection(
StudentID int not null
constraint foreignKey references dbo.Student(StudentID),
CourseID int null
constraint foreignKey2 references dbo.Course(CourseID),
ID int null);