# 🧱 Enterprise Compliance & Risk Management System

## 👤 Author
**Bozhidar Nikolchev**

---

# 🎯 1. Project Purpose

Organizations must manage:

- Risks (security, operational, compliance)
- Controls (policies and safeguards)
- Audits (verification of compliance)

This system simulates an internal **GRC (Governance, Risk, Compliance) platform** used to:

- Track risks
- Map controls to risks
- Enforce access control
- Maintain audit trails
- Provide visibility through dashboards

---

# 🧠 2. Core Conceptual Model

## 🔑 Main Entities

### 1. User
Represents a system user.

- Id
- Username
- PasswordHash
- Role (Admin, Auditor, User)

---

### 2. Risk
Represents a potential issue affecting the organization.

- Id
- Title
- Description
- Likelihood (1–5)
- Impact (1–5)
- Severity (calculated dynamically)
- Status (Open / In Progress / Mitigated / Accepted)
- CreatedBy

---

### 3. Control
Represents a security or compliance measure.

Examples:
- Access control policy
- Encryption requirement

Fields:
- Id
- Name
- Description
- Category

---

### 4. Risk-Control Mapping
Links risks to controls.

- RiskId
- ControlId

Purpose:
Shows how risks are mitigated using controls.

---

### 5. Audit Log
Tracks all important system actions.

- Id
- UserId
- Action
- EntityType
- EntityId
- Timestamp

Example:
"User X created Risk Y"

---

### 6. Evidence
Represents proof that a control is implemented.

- Id
- Description
- RiskId (optional)
- ControlId (optional)
- CreatedBy
- Timestamp

---

# 🔐 3. Security Model

## Role-Based Access Control (RBAC)

### Admin
- Full access
- Manages users, risks, and controls

### Auditor
- Read-only access
- Reviews compliance data

### User
- Creates and manages own risks
- Limited access to system

---

## Security Requirements

- Authentication (login system)
- Authorization (role-based access)
- Input validation
- Audit logging

---

# ⚙️ 4. System Architecture

## Layered Architecture

### API Layer (Controllers)
Handles HTTP requests.

Examples:
- POST /risks
- GET /controls

---

### Service Layer (Business Logic)
Contains system logic.

Examples:
- Risk severity calculation
- Permission validation

---

### Data Layer (Database)
Stores:

- Users
- Risks
- Controls
- Audit logs

---

# 🔢 5. Core Logic

## Risk Severity Calculation

Severity = Likelihood × Impact

Example:
- Likelihood: 4  
- Impact: 5  
- Severity: 20 (High Risk)

---

## Risk Lifecycle

1. Create Risk  
2. Assess Risk  
3. Assign Controls  
4. Update Status  
5. Mitigate or Accept  

---

# 📊 6. Dashboard Logic

The system calculates:

- Total risks
- Open risks
- Mitigated risks
- High severity risks
- Compliance percentage

Compliance % = (Mitigated Risks / Total Risks) × 100

---

# 🧾 7. Audit Logging

Every important action is recorded.

Examples:

- CREATE_RISK
- UPDATE_CONTROL
- DELETE_USER

Purpose:

- Traceability
- Accountability
- Compliance

---

# 🧱 8. Technology Stack

## Backend
C# with ASP.NET Core

## Database
SQLite

## Frontend
Minimal UI (added later)

---

# 🚀 9. Development Phases

## Phase 1
- Database design
- Risk CRUD

## Phase 2
- Authentication
- Role-based access control

## Phase 3
- Controls and mapping

## Phase 4
- Dashboard

## Phase 5
- Audit logging and improvements

---

# 🎯 Final Goal

This project demonstrates:

- Secure backend design
- Risk management logic
- Role-based access control
- Audit logging
- Business-focused system architecture  