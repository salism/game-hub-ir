# GameHub Project Context

## Project Summary

GameHub is a modern platform inspired by itch.io, GitHub, Discord, Steam Workshop and modern collaborative development platforms.

The goal is not to clone any existing platform, but to create a complete ecosystem for indie game developers and gamers.

GameHub combines game publishing, digital marketplace, developer social networking, collaborative workspaces, community management and game jam organization into a single platform.

The project is designed as a long-term product rather than a simple portfolio application.

---

# Primary User Types

- Developer
- Gamer
- Studio
- Administrator
- Moderator (Future)

---

# Core Vision

GameHub should become a complete ecosystem where developers can

- Create games
- Publish games
- Sell games
- Sell assets
- Build teams
- Collaborate
- Chat
- Organize GameJams
- Build communities
- Promote projects
- Manage studios

Players should be able to

- Discover games
- Buy games
- Download games
- Follow developers
- Join communities
- Review games
- Build wishlists
- Participate in events

---

# Major Modules

Identity

User Profiles

Social Network

Studios

Games

Uploads

Marketplace

Workspace

Chat

Notifications

Reviews

Comments

Wishlist

Search

Feed

Follow System

GameJam

Admin Panel

Moderation

Analytics

Achievements

Badges

Events

Storage

Permissions

Public API

Future AI Features

---

# Workspace Vision

Workspace is one of the most important modules.

It is not a Git replacement.

Instead, it is a collaborative development environment.

Features include

- Team management
- Tasks
- Kanban
- Documents
- Notes
- Internal chat
- Milestones
- Project management
- Asset management
- Lightweight collaborative coding features
- UI-based collaboration
- Future AI assistance

---

# Marketplace Vision

Marketplace supports

- Games
- Assets
- Plugins
- Tools
- Audio
- Music
- Models
- UI Packs
- Source Code
- Templates

Payment is planned for future versions.

The architecture already reserves space for payment integration.

---

# Social Platform Vision

Every developer has a public profile.

Profiles are customizable.

Users can

- Follow developers
- Send private messages
- Create posts
- Share development logs
- Showcase projects
- Build communities

Teams
- Only developers can make group
- Groups are for teams

Games also have customizable public pages.

Studios have public pages.

---

# Architectural Principles

Architecture Style

- Modular Monolith

Architecture Pattern

- Clean Architecture

Domain Modeling

- Domain Driven Design

API

- REST API

Frontend

- Angular

Backend

- ASP.NET Core

Database

- MongoDB

Authentication

- JWT + Refresh Token

File Storage

- Storage Abstraction

Future

- Redis
- ElasticSearch
- SignalR
- CDN
- Object Storage
- Background Workers

Microservices are intentionally NOT used in the first versions.

---

# Development Philosophy

Architecture First.

Infrastructure Later.

Design before implementation.

No premature optimization.

No premature microservices.

Every feature should be extendable without rewriting existing business logic.

Growth should happen by extension rather than refactoring.

---

# Documentation Philosophy

Blueprint is the source of truth.

The Blueprint is written first.

Implementation follows the Blueprint.

Major architectural changes require documentation updates.

Architectural decisions should be recorded using ADRs.

---

# Coding Principles

- SOLID
- Clean Code
- DDD
- Feature-based modules
- Dependency Injection
- Interface-first design
- Testable architecture
- High cohesion
- Low coupling

---

# Long-Term Goal

GameHub should be maintainable for many years.

The architecture should support millions of users through infrastructure evolution rather than business logic redesign.

The project should remain understandable, scalable and enjoyable to develop.

Every architectural decision should prioritize long-term maintainability over short-term convenience.

---