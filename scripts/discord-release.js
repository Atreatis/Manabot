const https = require("https");

const version = process.argv[2];
const notes = process.argv[3];
const webhookUrl = process.env.DISCORD_WEBHOOK_URL;

if (!webhookUrl) {
    console.error("DISCORD_WEBHOOK_URL not set");
    process.exit(1);
}

const content = {
    embeds: [
        {
            title: `🚀 Manabot v${version} Released`,
            description: notes.substring(0, 4000),
            color: 0x5865F2,
            footer: {
                text: "Manabot Release Automation"
            }
        }
    ]
};

const data = JSON.stringify(content);
const url = new URL(webhookUrl);

const req = https.request(
    {
        hostname: url.hostname,
        path: url.pathname + url.search,
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Content-Length": data.length
        }
    },
    res => {
        if (res.statusCode >= 400) {
            console.error("Failed to post Discord webhook:", res.statusCode);
        }
    }
);

req.on("error", console.error);
req.write(data);
req.end();