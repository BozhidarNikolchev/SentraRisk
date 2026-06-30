async function calculateRisk() {

    const input = {
        usesHttps: document.getElementById("https").checked,
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
        <h2 class="${result.riskLevel.toLowerCase()}">
    Risk Score: ${result.score} (${result.riskLevel})
</h2>

        <h3>Summary</h3>
        <p>${result.summary}</p>

        <h3>Top Risk</h3>
        <p>${result.topIssue}</p>

        <h3>Critical Issues</h3>
        <ul>
            ${result.criticalIssues.map(i => `<li>${i}</li>`).join("")}
        </ul>

        <h3>Medium Issues</h3>
        <ul>
            ${result.mediumIssues.map(i => `<li>${i}</li>`).join("")}
        </ul>

        <h3>Low Issues</h3>
        <ul>
            ${result.lowIssues.map(i => `<li>${i}</li>`).join("")}
        </ul>

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