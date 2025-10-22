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

## Installation
1. **Clone the Repository:**
   ```bash
   git clone <your-repository-url>
   cd St10359529_POE_Prog6212
