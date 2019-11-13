# 
![Logo](C:\Users\Mei\OneDrive\Code\Git\PolarisAI\Logo.png)

A Personal Assistant Engine built with .NET Core with Natural Language Processing.
The NLP capabilities are powered by the **Starlight Core** (https://github.com/MeiFagundes/Starlight).



Example:

**Query:** *Wake me up at 10:30 AM* 

**Current date/time:** 10-nov-2019, 12:30 PM

**Output:**

```
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