---
name: Bug Report
title: "[BUG] "
labels: ["bug"]
body:
  - type: markdown
    attributes:
      value: |
        **Thanks for reporting a problem with Vok. The more context you give,
        the faster we can fix it.**
  - type: textarea
    id: what-happened
    attributes:
      label: What happened?
      placeholder: Describe the problem, step by step.
  - type: textarea
    id: expected
    attributes:
      label: What did you expect?
      placeholder: What should have happened instead?
  - type: textarea
    id: environment
    attributes:
      label: Environment
      placeholder: Device, operating system, input method (touch / switch / eye gaze / etc.), accessibility settings.
  - type: textarea
    id: impact
    attributes:
      label: How did this affect communication?
      placeholder: Did it block/slow a message? Make the tool unusable? Degrade the experience?