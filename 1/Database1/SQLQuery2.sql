use dbo
INSERT Student VALUES (1, 'Lisa', 21)
INSERT Student VALUES (2, 'Brent', 22)
INSERT Student VALUES (3, 'Anita', 20)
INSERT Student VALUES (4, 'Ken', 22)
INSERT Student VALUES (5, 'Cathy', 22)
INSERT Student VALUES (6, 'Tom', 20)
INSERT Student VALUES (7, 'Zeoy', 21)
INSERT Student VALUES (8, 'Mark', 23)
INSERT Student VALUES (9, 'John', null)
go
INSERT Course VALUES (1, 'Math')
INSERT Course VALUES (2, 'Physics')
INSERT Course VALUES (3, 'Biology')
INSERT Course VALUES (4, 'English')
go
INSERT CourseSelection VALUES(1, 1, null)
INSERT CourseSelection VALUES(2, 1, null)
INSERT CourseSelection VALUES(3, 1, null)
INSERT CourseSelection VALUES(2, 2, null)
INSERT CourseSelection VALUES(2, 3, null)
INSERT CourseSelection VALUES(3, 3, null)
INSERT CourseSelection VALUES(3, 2, null)
go

select * from Student