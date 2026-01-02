This roadmap outlines the planned direction for Manabot.
It is a living document and may evolve based on community feedback,
feature requests, and contributor availability.

-----------------------------------

🎯 PROJECT GOALS
----------------
- Provide reliable, guild-focused tooling for MMORPG communities
- Remain fully MMORPG-agnostic (no hard dependencies on specific games)
- Reduce manual moderation and administrative overhead
- Maintain a clean, modular, and extensible architecture
- Support both self-hosted and managed (hosted) deployments
- Prioritize transparency, auditability, and role-based permissions

-----------------------------------

🚀 NEAR-TERM (v1.x) — MVP FOUNDATION
----------------------------------

CORE GUILD & COMMUNITY MANAGEMENT
---------------------------------
- Rank & Role Management
  - Custom guild ranks mapped to Discord roles
  - Promotion and demotion commands
  - Permission templates per rank
  - Officer-only audit logs

- Member Profile Registry
  - Per-member profiles:
    - Character names
    - Preferred roles (tank, healer, DPS, crafter, etc.)
    - Time zone

- Recruitment & Join Applications
  - Custom application forms
  - Role-based review permissions
  - Approval / rejection workflows
  - Auto-role assignment on acceptance

- New Member Onboarding Flow
  - Automated welcome messages
  - Rules and expectations delivery
  - Setup checklist (roles, profiles, channels)
  - Optional mentor assignment

COMMUNICATION & UX
------------------
- Announcement System
  - Target announcements by rank or role
  - Optional DM delivery for critical notices
  - Expiration for outdated announcements

- Join-to-Create Channels
  - Per-theme permissions
  - Auto-cleanup once abandoned
  - Voice chat only

CONFIGURATION & STABILITY
-------------------------
- Centralized configuration commands
- Improved error handling and user feedback
- Localization groundwork

-----------------------------------

🧭 MID-TERM (v2.x) — QUALITY OF LIFE & AUTOMATION
------------------------------------------------

EVENTS & PARTICIPATION
----------------------
- Event & Raid Scheduling
  - Time zone–aware scheduling
  - Role requirements and capacity limits
  - Signup via reactions or slash commands
  - Automatic reminders

- Attendance & Reliability Tracking
  - Track signups vs attendance
  - Late and no-show tracking
  - Reliability summaries for officers

AUTOMATION & MODERATION
-----------------------
- Scheduled announcements
- Activity tracking and member statistics
- Auto-role assignment based on criteria
- Moderation & Conduct Tracking
  - Guild rules storage
  - Warning and note logging
  - Escalation thresholds
  - Private officer logs

VISIBILITY & ACCOUNTABILITY
---------------------------
- Audit logs for administrative actions
- Cross-channel logging for:
  - Promotions and role changes

-----------------------------------

🌌 LONG-TERM (v3.x+) — ADVANCED SYSTEMS & SCALE
----------------------------------------------

ADVANCED GUILD SYSTEMS
----------------------
- Availability & Time Zone Analyzer
  - Weekly availability input
  - Suggested optimal event times
  - Leadership summaries

- Shared Resource Ledger
  - Abstract tracking of guild resources
  - Deposits and withdrawals
  - Approval workflows
  - Full audit history

COMMUNITY & CULTURE
-------------------
- Recognition & Rewards System
  - Commendations or kudos
  - Seasonal recognition
  - Cosmetic roles or badges

- Polls & Decision Tools
  - Structured voting
  - Rank-weighted options
  - Transparent results

PLATFORM & EXTENSIBILITY
------------------------
- Multi-Game Support
  - Game-specific tags and roles
  - Filtered announcements and events
  - Single Discord for multiple MMORPGs

- Plugin / Module System
  - Custom extensions
  - Optional feature bundles
  - Community-developed modules

- External Integrations
  - Integration with external game APIs where available
  - Optional, non-blocking connectors

-----------------------------------

🤝 COMMUNITY DRIVEN DEVELOPMENT
-------------------------------
Feature prioritization is guided by:
- Approved feature requests
- Community discussion and demand
- Contributor interest and availability

Have an idea?
Open a Feature Request issue and help shape the future of Manabot.

-----------------------------------

DESIGN PRINCIPLES
-----------------
- MMORPG-agnostic by default
- Modular and configurable systems
- Permission-first architecture
- Transparency without micromanagement
- Scales from small guilds to large communities