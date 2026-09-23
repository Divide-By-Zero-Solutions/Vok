---
name: Task
title: "[TASK] "
labels: ["task"]
body:
  - type: markdown
    attributes:
      value: |
        **Internal development task for Vok.**
  - type: textarea
    id: details
    attributes:
      label: Details
      placeholder: Describe the task and why it matters for the goal.
  - type: input
    id: milestone
    attributes:
      label: Milestone
      placeholder: e.g. Listen, A First Voice, In Their Hands, Beyond the Screen, v1.0
  - type: dropdown
    id: area
    attributes:
      label: Area
      options:
        - Communication workflow
        - Input methods / accessibility
        - User research
        - Design / UX
        - Infrastructure / build
        - Documentation