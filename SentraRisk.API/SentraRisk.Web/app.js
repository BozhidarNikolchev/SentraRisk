async function calculateRisk() {

    const input = {
        websiteUrl: document.getElementById("website").value,

        hasBackup: document.getElementById("backup").checked,
        usesOutdatedPlugins: document.getElementById("plugins").checked,
        hasAdminUser: document.getElementById("admin").checked,
        hasTwoFactorAuth: document.getElementById("twofa").checked
    };

    const response = await fetch(
        "http://localhost:5232/api/Risk/calculate",
        {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(input)
        });

    const result = await response.json();

    document.getElementById("result").innerHTML = `
        <div class="risk-card ${result.riskLevel.toLowerCase()}">
    <h2>${result.riskLevel.toUpperCase()} RISK</h2>
    <h1>${result.score}</h1>
</div>
        <h3>Summary</h3>
        <p>${result.summary}</p>

        <h3>Top Risk</h3>
        <p>${result.topIssue}</p>

        ${result.criticalIssues.length > 0
            ? `
        <div class="badge badge-critical">CRITICAL</div>
<h3>Critical Issues</h3>
<ul>
    ${result.criticalIssues.map(i => `<li>${i}</li>`).join("")}
</ul>
      `
            : `<p>✅ No critical issues detected</p>`
        }

        ${result.mediumIssues.length > 0
            ? `
        <div class="badge badge-medium">MEDIUM</div>
<h3>Medium Issues</h3>
<ul>
    ${result.mediumIssues.map(i => `<li>${i}</li>`).join("")}
</ul>
      `
            : `<p>✅ No medium-risk issues detected</p>`
        }

        ${result.lowIssues.length > 0
            ? `
      <div class="badge badge-low">LOW</div>
<h3>Low Issues</h3>
<ul>
    ${result.lowIssues.map(i => `<li>${i}</li>`).join("")}
</ul>
      `
            : `<p>✅ No low-risk issues detected</p>`
        }

        <h3>Recommendations</h3>
        <ul>
            ${result.recommendations.map(r => `<li>${r}</li>`).join("")}
        </ul>

        <h3>Priority Actions</h3>
        <ul>
            ${result.priorityActions.map(a => `<li>${a}</li>`).join("")}
        </ul>
    `;
}