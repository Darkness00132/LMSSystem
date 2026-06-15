# LMS System

A modern backend for a learning management platform, designed around real education workflows: course creation, structured syllabus content, student enrollment, learning progress, quizzes, assignments, and role-based administration.

This project is built to show more than endpoints. It demonstrates how a complete LMS can be modeled from the database up, with clean separation between domain, application, infrastructure, and API layers.

## What It Covers

- Student registration, login, JWT authentication, and refresh tokens
- Role-based access for Students, Instructors, Assistants, and Admins
- Course lifecycle from draft to review to published
- Ordered course sections and learning content
- Support for lessons, quizzes, assignments, submissions, and progress tracking
- Enrollment management and instructor approval flow
- Admin control over users and course review
- Entity Framework Core database modeling with Identity integration

## Product Vision

The system is designed as the foundation for a serious online learning platform. Instructors can build and manage courses, students can enroll and learn through structured content, assistants can support instructors, and admins can control quality before courses go public.

The core design focuses on clarity, ownership, and extensibility. Courses are not just records; they contain ordered sections, content items, assessment flows, student progress, and approval states.

## Architecture

The solution follows a layered backend structure:

- `Domain` contains the core entities and enums.
- `Application` contains service contracts, DTOs, business services, mappings, and exceptions.
- `Infrastructure` contains persistence, Identity, repositories, JWT, and external service wiring.
- `LMSSystem` exposes the ASP.NET Core Web API.

This keeps business intent separate from transport and database details, making the project easier to grow without turning the API layer into the whole application.

## Current Status

The project builds cleanly with no compiler errors or warnings. The core architecture, database model, authentication wiring, and API surface are in place.

Some service flows are intentionally still pending implementation, including full admin operations, enrollment behavior, instructor assistant management, syllabus editing, assignment handling, quiz execution, and learning-content access rules.

## Highlights

- Clean LMS domain model
- ASP.NET Core Identity with custom roles
- JWT-based authentication
- Refresh-token support
- Course publication workflow
- Structured learning content model
- Practical foundation for expanding into a full production LMS
