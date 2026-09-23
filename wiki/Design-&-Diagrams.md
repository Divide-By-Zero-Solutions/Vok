# Vok — Design & Diagrams

Visuals that illustrate how Vok turns a person's intent into real communication.

## The core loop

```mermaid
flowchart LR
    A["Person's intent"] --> B[Input]
    B --> C{Vok}
    C --> D["Message (text + voice)"]
    D --> E["Communication partner"]
    E --> F["Connection, action,\nindependence"]
    F --> A
```

## From person to connection

```mermaid
flowchart TD
    P[Person who cannot\nspeak reliably] --> I[Any input method]
    I -->|touch| T[Touch]
    I -->|switches| S[Switch access]
    I -->|gaze| G[Eye gaze / head tracking]
    I -->|puff| SP[Sip-and-puff]

    T --> C[Vok core]
    S --> C
    G --> C
    SP --> C

    C --> V1[Symbols & prediction]
    C --> V2[Personal voice]
    C --> V3[Emotion markers]
    C --> V4[Translation]

    V1 --> M[Clear message]
    V2 --> M
    V3 --> M
    V4 --> M

    M --> Partner[Communication partner\nunderstands]
    Partner --> Action["Action: response,\nlight, request met"]
```

## Feature vision map

```mermaid
mindmap
  root((Vok))
    Expressive communication
      Personal voice
      Symbols
      Emotion markers
      One-tap phrases
    Input for every body
      Touch
      Switch access
      Eye gaze
      Head tracking
      Sip-and-puff
    Understanding the person
      Adaptive vocabulary
      Personal layouts
      Per-person profiles
    Beyond the screen
      Partner transcripts
      Translation
      Environmental actions
      Emergency phrases
    Independence
      Offline-first
      Affordable hardware
      Collaborative setup
```

## Milestone journey

```mermaid
journey
    title From mission to daily use
    section Listen
      Research & concept: 2: Team
    section A First Voice
      Prototype: 3: Team, Users
    section In Their Hands
      Adaptation & pilot: 4: Users, Clinicians
    section Beyond the Screen
      Impact & beta: 5: Community
    section v1.0
      Reaching more voices: 5: Everyone
```

## User journeys

### The AAC user's daily loop

```mermaid
journey
    title A day with Vok (AAC user)
    section Morning
      Wake & greet family: 3: AAC user
      Ask for breakfast: 4: AAC user, Carer
      Say good morning to mum: 3: AAC user, Family
    section Midday
      Tell someone "I'm thirsty": 4: AAC user
      Let a carer know "I need help": 2: AAC user, Carer
      Share a joke with a friend: 3: AAC user, Friend
    section Evening
      Talk with family about the day: 5: AAC user, Family
      Say goodnight: 4: AAC user, Family
```

### First-week onboarding (family / carer)

```mermaid
journey
    title Setting up Vok for a loved one
    section Day 1
      Install on the device: 3: Carer
      Create the user's profile: 2: Carer
      Pick vocabulary categories: 3: Carer, AAC user
    section Day 3
      Customise symbols & phrases: 3: Carer
      User composes first sentence: 4: AAC user, Carer
    section Day 7
      Daily use feels natural: 5: AAC user, Carer
      Add new personal phrases: 4: AAC user, Carer
```

### First message ever

```mermaid
flowchart TD
    A[User wants to say something] --> B{Can they reach the device?}
    B -->|Yes| C[Tap starting symbol]
    B -->|No| D[Switch / gaze / puff input]
    C --> E[Pick next symbol]
    D --> E
    E --> F{Enough to compose?}
    F -->|No| E
    F -->|Yes| G[Tap 'Speak']
    G --> H[Partner hears the message]
    H --> I[User & partner connect]
    I --> J{"Was it easy?\nDid it feel like them?"}
    J -->|Yes| K[Confidence grows — they use it again 💪]
    J -->|No| L[Vok learns & adapts the layout]
    L --> C
```

### Voice banking journey

```mermaid
journey
    title Building the user's own voice
    section Record
      Read short sentences aloud: 2: AAC user
      App captures speech samples: 3: AAC user, App
    section Create
      Voice is trained/imported: 4: App
      Voice is tested: 4: AAC user, Carer
    section Live
      Vok speaks in the user's voice: 5: AAC user, Family
      Everyday messages sound like them: 5: AAC user, Everyone
```

### The conversation partner's flow

```mermaid
sequenceDiagram
    actor User as AAC user
    actor Partner as Conversation partner
    participant App as Vok

    Partner->>User: "What would you like?"
    User->>App: Composes symbols
    App-->>User: Suggests likely next phrase
    User->>App: Taps 'Speak'
    App-->>Partner: Plays message in user's voice
    Partner-->>User: Responds warmly
    Partner->>App: (Optional) reads live transcript
```