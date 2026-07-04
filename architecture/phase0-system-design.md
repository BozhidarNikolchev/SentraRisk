# SentraRisk System Architecture

## Author

Bozhidar Nikolchev

---

# 1. Project Purpose

SentraRisk is a commercial Website Risk Assessment Platform.

The platform automatically analyzes public websites and translates technical findings into business-friendly explanations, risk assessments and remediation guidance.

The goal is to help website owners understand:

- What was checked
- What was found
- Why it matters
- Business impact
- How to fix issues
- Where fixes should be applied
- The most convenient solution path

SentraRisk is not a penetration testing platform.

SentraRisk is an automated website health and risk assessment platform.

---

# 2. Product Vision

User enters:

Website URL

Example:

https://example.com

SentraRisk performs:

- Website Reachability Analysis
- HTTPS Analysis
- SSL Certificate Analysis
- Redirect Analysis
- Security Headers Analysis
- Technology Detection
- SPF Analysis
- DKIM Analysis
- DMARC Analysis

Results are translated into:

- Risk Score
- Risk Level
- Business Explanations
- Fix Instructions
- Recommended Solutions

---

# 3. Core Workflow

Website URL
↓
Scanner Layer
↓
Risk Engine
↓
Business Interpretation Layer
↓
Report Engine
↓
Client Assessment Report

---

# 4. Scanner Layer

The Scanner Layer collects technical information.

Current metrics:

✅ Reachability

✅ SSL Certificate Analysis

✅ HTTPS Analysis

🔄 Redirect Analysis

⬜ Security Headers

⬜ Technology Detection

⬜ SPF

⬜ DKIM

⬜ DMARC

---

# 5. Reachability Analysis

Purpose:

Determine whether the website is accessible.

Checks:

- Website responds to requests
- DNS resolution succeeds
- Connection can be established

Possible Outcomes:

- Reachable
- Unreachable

If the website is unreachable:

Assessment stops.

SentraRisk does not generate findings that cannot be verified.

---

# 6. SSL Certificate Analysis

Purpose:

Evaluate website trust certificate configuration.

Checks:

- Certificate Present
- Certificate Valid
- Expiration Date
- Days Remaining
- Certificate Issuer
- Self-Signed Detection

Output:

- SSL Status
- SSL Health
- Business Impact
- Recommendations

Status:

✅ Complete

---

# 7. HTTPS Analysis

Purpose:

Determine whether encrypted communication is used.

Checks:

- HTTPS Detection

Output:

- Status
- Business Impact
- Fix Instructions
- Recommended Solution

Status:

✅ Complete

---

# 8. Redirect Analysis

Purpose:

Determine whether HTTP traffic is redirected to HTTPS.

Checks:

- HTTP → HTTPS Redirect

Status:

🔄 In Progress

---

# 9. Security Headers Analysis

Purpose:

Determine whether recommended security headers are configured.

Future Checks:

- Content-Security-Policy
- Strict-Transport-Security
- X-Frame-Options
- X-Content-Type-Options
- Referrer-Policy

Status:

⬜ Planned

---

# 10. Technology Detection

Purpose:

Identify technologies used by the website.

Examples:

- WordPress
- ASP.NET
- PHP
- React
- Angular

Status:

⬜ Planned

---

# 11. Email Security Analysis

Purpose:

Determine whether business email protection controls exist.

Modules:

- SPF
- DKIM
- DMARC

Status:

⬜ Planned

---

# 12. Risk Engine

Purpose:

Convert technical findings into risk ratings.

Responsibilities:

- Risk Scoring
- Severity Classification
- Issue Identification
- Recommendation Generation
- Priority Action Generation

Output:

- Score
- Risk Level
- Findings

---

# 13. Business Interpretation Layer

Purpose:

Translate technical results into language understandable by business owners.

Every unhealthy finding must include:

- What It Is
- What Was Found
- Why It Matters
- Business Impact
- How To Fix
- Where To Fix
- Most Convenient Solution

Every healthy finding must include:

- What Was Checked
- What The Metric Is
- Why It Matters
- Current Healthy Status
- Business Impact
- Why No Action Is Required

---

# 14. Report Engine

Purpose:

Generate customer-facing assessment reports.

Report Sections:

- Executive Summary
- Risk Score
- Risk Level
- Findings
- Business Impact
- Recommendations
- Priority Actions

---

# 15. SentraRisk Product Rules

Rule 1

Finish each metric completely before moving to the next metric.

Rule 2

All findings must include client-friendly explanations.

Rule 3

All dates use:

YYYY-MM-DD

Example:

2026-09-07

Rule 4

Never provide recommendations without explaining the issue first.

Rule 5

Prefer the most convenient solution for the client.

Rule 6

Do not generate findings that cannot be verified.

Rule 7

Healthy findings must still explain what was checked and why it matters.

Rule 8

Unreachable websites must receive professional troubleshooting guidance rather than generic errors.

---

# 16. Current Development Status

Completed

✅ Reachability Analysis

✅ SSL Certificate Analysis

✅ HTTPS Analysis

In Progress

🔄 Redirect Analysis

Planned

⬜ Security Headers

⬜ Technology Detection

⬜ SPF

⬜ DKIM

⬜ DMARC

---

# 17. Commercial Goal

SentraRisk is a Website Risk Assessment SaaS.

Primary Value:

Translate technical website findings into business-friendly actions.

Customer Outcome:

Understand issues quickly.

Prioritize risk.

Fix problems efficiently.

Improve website trust and security.