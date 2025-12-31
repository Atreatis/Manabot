# Manabot Deployment Guide

This document describes how to deploy **Manabot** using Docker.

---

## 📦 Requirements

- Docker
- A Discord Bot Token
- A MongoDB instance (MongoDB Atlas recommended)

---

## 🔐 Environment Variables

Manabot is configured entirely through environment variables.

### Required Variables

```env
DOTNET_ENVIRONMENT=Production
DISCORD_TOKEN=your_discord_bot_token
MONGODB_URI=mongodb+srv://...
DATABASE_NAME=Production
SUPPORT_SERVER=https://discord.gg/YX7895H6k8
DEV_GUILD=1269943746707849246
```

| Variable             | Description                              |
| -------------------- | ---------------------------------------- |
| `DOTNET_ENVIRONMENT` | Runtime environment (use `Production`)   |
| `DISCORD_TOKEN`      | Discord bot token                        |
| `MONGODB_URI`        | MongoDB connection string                |
| `DATABASE_NAME`      | MongoDB database name                    |
| `SUPPORT_SERVER`     | Discord support server invite            |
| `DEV_GUILD`          | Guild ID used for development or testing |

## 🐳 Docker Deployment
### Build the Image

```bash
docker build -t manabot .

Run the Container
docker run -d \
--name manabot \
--env-file .env \
manabot
```

Ensure your `.env` file contains all required environment variables.

## 🌐 Hosted Option

If you do not wish to self-host, a publicly hosted version of Manabot is available. This option removes the need for Docker, MongoDB setup, and ongoing maintenance.

## 🛠 Troubleshooting

- Verify all environment variables are set correctly
- Ensure the Discord bot has the required gateway intents
- Check Docker logs for startup or connection errors
- Confirm MongoDB network access is configured correctly

## 💬 Support

If you encounter issues or have questions, join the support server:
👉 https://discord.gg/YX7895H6k8