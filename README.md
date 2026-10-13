# PolarisAI

![PolarisAI logo](Logo.png)

PolarisAI is a personal-assistant prototype built with .NET Core. Its web API uses [Starlight](https://github.com/MeiFagundes/StarlightAI) for intent classification, then selects a response for the predicted intent. The API exposes query and debug routes, and the core also records request details in its configured database.

The included intent and response code covers requests such as alarms, reminders, weather, news, music, calls, and small talk. The alarm example below illustrates the generated response text.

## Example

The following example is preserved from the original README, using its November 10, 2019 date context. The resolved date refers to the following day.

**Query:** *Wake me up at 10:30 AM*

**Current date/time in the example:** November 10, 2019, 12:30 PM

**Output:**

```json
{
  "code": 41,
  "response": "Sure, I've set an alarm for tomorrow, 10:30 AM.",
  "entities": {
    "entity": "tomorrow",
    "type": "date",
    "startIndex": 14,
    "endIndex": 18,
    "date": "2019-11-11",
    "time": "10:30 AM"
  }
}
```
