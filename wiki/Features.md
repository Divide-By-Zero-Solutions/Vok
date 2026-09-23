# Vok — Feature Vision

The mission: **helping people who cannot communicate, communicate more
effectively.** Every feature below exists to widen that mission — to reach more
people, remove more barriers, and hand more of a person's own voice back to them.

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

## Expressive communication

- **Symbol-first interface** — communicate with pictures before words.
- **Text-to-speech with your own voice** — voice banking and voice cloning so the
  tool sounds like *you*, not a machine.
- **Predicted phrases & smart completion** — say a whole sentence with one or two
  taps.
- **Emotion & tone markers** — express not just *what* you mean but *how* you feel:
  proud, joking, urgent, calm.
- **Storytelling & free expression** — build longer messages for conversations,
  not just needs.

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

## Input for every body

- **Touch** and simple gestures.
- **Switch access** — one or two switches for limited motor control.
- **Eye gaze** and **head tracking** for people who cannot reach a screen.
- **Joystick / sip-and-puff** and other assistive switches.
- **Mapping to existing devices** — work with the assistive hardware people are
  already using, instead of forcing new equipment.

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

## Understanding the person

- **Personal vocabulary** — words, names, and phrases unique to each user.
- **Adaptive learning** — the tool learns which words and phrases each person
  reaches for and brings them closer.
- **Profile per person** — no shared generic boards; the layout is *their* layout.

### A day with Vok

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

## Beyond the screen

- **Real-time transcript for conversation partners** — the people you're talking
  to see and hear clearly.
- **Translation** — a person's message, understood in other languages.
- **Environmental actions** — from "I'm thirsty" to turning on a light: go from
  expression to action.
- **Emergency & health phrases** — always-available critical messages.

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

## Independence at every step

- Works offline for privacy and reliability.
- Runs on affordable, accessible hardware, not only premium devices.
- Settings that a family, carer, or clinician can configure collaboratively —
  without code.

### Setting up Vok for a loved one

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

## The bigger mission

Vok is not a single app. It is the foundation for a person's independence and
dignity — and a bridge between them and everyone who loves them.