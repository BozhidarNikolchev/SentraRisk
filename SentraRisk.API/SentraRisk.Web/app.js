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

    document.getElementById("result").textContent =
        JSON.stringify(result, null, 2);
}