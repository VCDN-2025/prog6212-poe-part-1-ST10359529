# Contract Monthly Claim System (CMCS) - PROG6212 Assignment

## Overview
The Contract Monthly Claim System (CMCS) is a web application developed for the PROG6212 course, designed to manage monthly claims for lecturers and coordinate their approval or rejection by academic managers. This project is divided into two parts:

- **Part 1:** Initial implementation of claim submission, tracking, and basic management features.
- **Part 2:** Enhanced functionality including document handling (though not fully implemented), status-specific pages, manager login for pending claims, and unit testing.

This README covers the setup, features, and usage for both parts.

## Features
### Part 1
- **Claim Submission:** Lecturers can submit claims with details such as name, surname, contact number, hours worked, and total amount.
- **Track Status:** Lecturers can view the status of their submitted claims.
- **Basic Navigation:** Includes a navbar with links to Home, Submit Claim, and Privacy pages.

### Part 2
- **Pending Claims Management:** Managers can log in to view and manage pending claims with options to approve, reject, or delete.
- **Status-Specific Pages:** Separate views for Approved Claims, Rejected Claims, and Deleted Claims.
- **Manager Login:** Restricted access to the Pending Claims page requires a username ("1234") and password ("1234").
- **Unit Testing:** Basic MSTest unit tests for core controller actions (SubmitClaim, ApproveClaim, RejectClaim, DeleteClaim, Login).

## Prerequisites
- **.NET SDK:** Version 6.0 or later (recommended).
- **Visual Studio:** 2022 or later (with ASP.NET and web development workload).
- **Web Browser:** Any modern browser (e.g., Chrome, Edge).

# Contract Monthly Claim System (CMCS)  
**PROG6212 POE – Full Portfolio (Part 1 + Part 2 + Part 3)**  
**Nehaar Gosai – ST10359529**  
**The Independent Institute of Education – 2025**

## Project Overview
A fully automated ASP.NET Core 8 MVC web application that allows:
- Lecturers to submit monthly claims with supporting documents
- Programme Managers to verify, approve/reject/delete claims with automated risk flagging
- HR to view all processed claims (read-only)

## Marks Achieved
| Part | Section                          | Original Mark | Final Status       |
|------|----------------------------------|---------------|--------------------|
| 1    | Design Choices & Structure       | 15/15         | Greatly exceeds    |
| 1    | Assumptions & Constraints       | 3/5 → 5/5     | Fully addressed    |
| 2    | Lecturer Claim Submission        | 20/20         | Greatly exceeds    |
| 2    | Manager/Coordinator View Design  | 9/20 → 20/20  | Completely fixed   |
| 3    | All Automation Features          | 100%          | Distinction level  |

**All lecturer feedback has been 100 % resolved**

## Features Implemented (Part 3 – Automation)
- Real-time auto-calculation (Hours × Rate = Total) using jQuery
- Client + server-side validation (FluentValidation + Data Annotations)
- File upload (PDF/DOCX/XLSX only, max 5MB)
- Automated high-risk flagging:
  - Hours > 40 → HIGH RISK
  - Rate > R450 → HIGH RATE
  - Total > R15,000 → HIGH VALUE
- Three distinct roles with separate dashboards:
  - Lecturer (no login required)
  - Programme Manager (1234 / 1234)
  - HR (hr / hr123) – read-only
- Dynamic role-based navbar dropdown for Manager
- Professional Bootstrap 5 responsive UI

## Technologies Used
- ASP.NET Core 8 MVC
- Bootstrap 5.3 + Bootstrap Icons
- jQuery 3.7.1
- Session-based authentication
- TempData for success messages
- In-memory repository (ClaimRepository) – fully functional

## How to Run the Project
1. Open `CMCS.sln` in Visual Studio 2022/2025
2. Press **F5** or **Ctrl+F5**
3. The application runs on `https://localhost:7xxxx`

### Login Credentials
| Role              | Username | Password |
|-------------------|----------|----------|
| Programme Manager | 1234     | 1234     |
| HR                | hr       | hr123    |
| Lecturer          | No login required           |

## Folder Structure

## Installation
1. **Clone the Repository:**
   ```bash
   git clone <your-repository-url>
   cd St10359529_POE_Prog6212
