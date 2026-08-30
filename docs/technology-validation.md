# Technology Detection Validation

## Purpose

This document validates the reliability of SentraRisk Technology Detection.

Validation Rule:

Build
→ Validate
→ Trust
→ Release

Technology detection must be proven reliable before it is used in customer-facing reports.

---

## Test Matrix

| Website | Expected | Detected | Result |
|----------|----------|----------|----------|
| wordpress.com | WordPress | | |
| shopify.com | Shopify | | |
| github.com | GitHub | | |
| cloudflare.com | Cloudflare | | |
| microsoft.com | Microsoft | | |
| nginx.com | Nginx | | |
| apache.org | Apache | | |

---

## Validation Notes

### PASS Criteria

The expected technology appears in the detected technology list.

Example:

Expected:

WordPress

Detected:

WordPress, Nginx

Result:

PASS

---

### FAIL Criteria

The expected technology does not appear in the detected technology list.

Example:

Expected:

GitHub

Detected:

Unknown

Result:

FAIL

---

## Accuracy Calculation

Passed: 0

Failed: 0

Accuracy: 0%

Formula:

Accuracy = (Passed / Total Tests) × 100

---

## Validation Results

Record all observations here.

### wordpress.com

Detected:

Result:

Notes:

---

### shopify.com

Detected:

Result:

Notes:

---

### github.com

Detected:

Result:

Notes:

---

### cloudflare.com

Detected:

Result:

Notes:

---

### microsoft.com

Detected:

Result:

Notes:

---

### nginx.com

Detected:

Result:

Notes:

---

### apache.org

Detected:

Result:

Notes:

---

## Sign-Off Rules

### Green

90%+ Accuracy

Status:

APPROVED

Technology Detection may be considered reliable.

---

### Yellow

70% - 89% Accuracy

Status:

REVIEW REQUIRED

Investigate failed detections.

---

### Red

Below 70% Accuracy

Status:

NOT APPROVED

Technology Detection requires improvements before customer release.

---

## Final Decision

Status:

Pending

Validation Date:

YYYY-MM-DD

Validated By:

Bozhidar Nikolchev  

////////////////////////////////////////////////////////////////////////

Dkim cannot be included! The app checks URLs, not email messages.

