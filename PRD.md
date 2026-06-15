# LMS System PRD

## Overview

The LMS System is a backend API for managing online courses, enrollment, learning content, quizzes, assignments, and role-based administration.

The product supports four user roles: Student, Instructor, Assistant, and Admin.

## Goals

- Allow students to register, log in, browse published courses, enroll, learn course content, complete progress, take quizzes, and submit assignments.
- Allow instructors to create and manage courses, sections, lessons, quizzes, assignments, and enrollments.
- Allow assistants to help instructors manage course syllabus, quizzes, and submissions when assigned.
- Allow admins to manage users and review submitted courses before publication.

## Non-Goals

- Payment processing is not part of the current MVP.
- Real-time chat, notifications, certificates, and analytics dashboards are outside the current MVP.
- Frontend UI is outside the current scope.

## User Roles

### Student

- Register and log in.
- View published courses.
- Enroll in courses.
- Access enrolled course content.
- Mark content as completed.
- Start and submit quiz attempts.
- View quiz results.
- Submit assignments.
- View own assignment submissions.

### Instructor

- Create courses.
- Update course details.
- Submit courses for admin review.
- Unpublish courses.
- Create, update, delete, and reorder course sections.
- Add lessons, quizzes, and assignments to sections.
- Create and manage quiz questions.
- View and approve course enrollments.
- View and grade assignment submissions.
- Assign and remove assistants.

### Assistant

- Manage course syllabus/content for assigned instructor courses.
- Manage quiz questions.
- View and grade submissions if allowed by instructor ownership.

### Admin

- View all users.
- Promote users to Instructor or Assistant roles.
- Delete users.
- Review submitted courses and approve or reject them.

## Core Data Model

### Identity

- `User` extends ASP.NET Identity user and stores full name, creation date, optional instructor link for assistants, courses, and enrollments.
- `Role` extends ASP.NET Identity role.

### Course

- A course belongs to one instructor.
- A course has title, description, thumbnail URL, optional price, status, creation date, sections, and enrollments.
- Course status controls visibility: Draft, UnderReview, Published, or Rejected.

### Syllabus

- A course contains ordered sections.
- A section contains ordered content items.
- A content item represents one lesson, quiz, or assignment.

### Learning Content

- Lesson content can contain video, text, file URL, duration, and content type.
- Quiz content contains questions, options, attempts, answers, score, and passing score.
- Assignment content contains title, description, max score, due date, submissions, grading, and feedback.

### Enrollment and Progress

- A student can enroll once per course.
- Enrollment tracks status and progress percentage.
- Content progress tracks completion per student and content item.

## API Areas

### Authentication

- `POST /api/auth/register`
- `POST /api/auth/login`
- `POST /api/auth/login-web`
- `POST /api/auth/refresh`
- `DELETE /api/auth/logout`

### Courses

- `GET /api/courses`
- `GET /api/courses/{courseId}`
- `POST /api/courses`
- `PUT /api/courses/{courseId}`
- `POST /api/courses/{courseId}/submit`
- `POST /api/courses/{courseId}/unpublish`
- `PUT /api/courses/{courseId}/review`

### Enrollments

- `GET /api/enrollments/my`
- `POST /api/enrollments/{courseId}`
- `GET /api/courses/{courseId}/enrollments`
- `POST /api/enrollments/{enrollmentId}/approval`

### Syllabus

- `POST /api/courses/{courseId}/sections`
- `PUT /api/sections/{sectionId}`
- `DELETE /api/sections/{sectionId}`
- `PUT /api/courses/{courseId}/sections/reorder`
- `POST /api/sections/{sectionId}/content`
- `PUT /api/content/{contentId}`
- `DELETE /api/content/{contentId}`
- `PUT /api/sections/{sectionId}/content/reorder`

### Learning

- `GET /api/courses/{courseId}/learn`
- `GET /api/content/{contentId}`
- `POST /api/progress/{contentItemId}/complete`

### Quizzes

- `POST /api/quizzes/{quizId}/questions`
- `PUT /api/questions/{questionId}`
- `DELETE /api/questions/{questionId}`
- `POST /api/quizzes/{quizId}/start`
- `POST /api/quiz-attempts/{attemptId}/submit`
- `GET /api/quiz-attempts/{attemptId}/result`

### Assignments

- `POST /api/assignments/{assignmentId}/submit`
- `GET /api/assignments/{assignmentId}/my-submission`
- `GET /api/assignments/{assignmentId}/submissions`
- `PUT /api/assignments/submissions/{submissionId}/grade`

### Admin

- `GET /api/admin/users`
- `PUT /api/admin/users/{userId}/promote-instructor`
- `PUT /api/admin/users/{userId}/promote-assistant`
- `DELETE /api/admin/users/{userId}`

## MVP Acceptance Criteria

- The API builds with no compile errors.
- Auth endpoints can register users, assign Student role, log in, refresh access tokens, and log out.
- JWT authorization works for protected endpoints.
- Courses are owned by the authenticated instructor.
- Published courses are visible publicly.
- Students cannot access learning content unless enrolled.
- Instructors and assistants cannot modify courses they do not own or assist with.
- Admins can approve or reject submitted courses.
- Placeholder endpoints must be implemented before MVP release.

## Known Implementation Gaps

- Several service methods are currently placeholders.
- Quiz and learning services need concrete implementations.
- Assignment endpoints need request/response DTOs and service implementation.
- Ownership checks are needed across instructor and assistant actions.
- File upload/storage behavior for thumbnails and submissions needs final implementation.
