# Contributing to Manabot

Thank you for your interest in contributing to **Manabot**. Community involvement is a core part of the project, and contributions of all kinds are welcome.

To keep development organized and sustainable, please follow the guidelines below.

---

## 📋 General Guidelines

- **Strict variables must be used at all times**  
  Configuration values must always come from environment variables. Hardcoded values are not permitted.

- **Feature requests come first**  
  Before implementing a new feature, create a feature request issue to discuss scope, design, and alignment with the project roadmap.

- **Keep changes focused**  
  Each pull request should address a single feature, improvement, or fix.

---

## 🧠 Feature Requests

1. Open an issue labeled **Feature Request**
2. Clearly describe:
    - The problem being solved
    - The proposed solution
    - Any alternatives considered
3. Wait for approval or feedback before starting development

This process helps avoid duplicated work and ensures features align with the project vision.

---

## 🛠 Development Expectations

- Follow existing project structure and conventions
- Use clear, descriptive commit messages
- Write readable and maintainable code
- Avoid introducing breaking changes without discussion
- Ensure new features are configurable via environment variables where applicable

---

## 🔍 Pull Requests

When submitting a pull request:
- Reference the related issue or feature request
- Clearly describe what was changed and why
- Ensure the build passes and the bot runs correctly in Docker

Pull requests may be reviewed, requested for changes, or merged depending on scope and quality.

---

### Commit Message Format

Manabot uses Conventional Commits:

<type>(optional scope): <description>

Examples:
- feat: add guild join application review
- fix: resolve MongoDB connection retry bug
- chore: update dependencies
- feat!: change configuration loading strategy

## ❤️ Thank You

Every contribution, from code to documentation to ideas, helps Manabot grow. Your time and effort are genuinely appreciated.