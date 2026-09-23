---
name: Feature Request
title: "[FEATURE] "
labels: ["enhancement"]
body:
  - type: markdown
    attributes:
      value: |
        **Propose an improvement to Vok. Describe the real communication
        situation behind it — that helps us design the right solution.**
  - type: textarea
    id: situation
    attributes:
      label: What communication need is this for?
      placeholder: Describe the situation where this would help.
  - type: textarea
    id: solution
    attributes:
      label: What would help?
      placeholder: Describe the feature or change you imagine.
  - type: input
    id: input-method
    attributes:
      label: Input method(s)
      placeholder: touch / switch / eye gaze / symbol / other
  - type: checkboxes
    id: accessibility
    attributes:
      label: Accessibility notes
      options:
        - label: Must work for users with limited fine motor control
        - label: Must work in audio-only / no-screen scenarios
        - label: Must support customization of vocabulary/voice