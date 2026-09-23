# Data model and persistence

The application uses domain models for vocabulary and profiles, SQLite for structured vocabulary persistence, JSON files for selected settings/profiles/backups, and MAUI Preferences for lightweight serialized state.

```mermaid
erDiagram
	AAC_CATEGORY ||--o{ AAC_TILE : contains
	USER_PROFILE ||--o{ AAC_CATEGORY : selects
	AAC_CATEGORY {
		string id PK
		string name
		string color
		string icon
	}
	AAC_TILE {
		string id PK
		string category_id FK
		string text
		string image_path
		int tile_type
	}
	USER_PROFILE {
		string id PK
		string name
		string avatar_path
		string selected_category_id FK
	}
	DB_CATEGORY ||--o{ DB_TILE : stores
	DB_CATEGORY {
		int id PK
		string name
		string color
	}
	DB_TILE {
		int id PK
		int category_id FK
		string text
		string image_path
		int tile_type
	}
```

## Persistence responsibilities

- `SqliteVocabularyService` owns SQLite schema creation, category/tile mapping, reads, and writes.
- `LocalVocabularyService` provides lightweight serialized vocabulary state through MAUI Preferences.
- `LocalProfileService` persists profile data under the application data directory.
- `AppConfig`, `AiSettings`, and voice settings persist configuration and provider choices.
- `BackupService` serializes category and tile collections for caregiver export/import.

The database and application data paths are runtime-specific. Tests and benchmarks must use temporary isolated paths and must not depend on production user data.
