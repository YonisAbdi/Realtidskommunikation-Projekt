Naturliga Växter & Kreativa Trädgårdar - Projektbeskrivning


I det här projektet ska vi hjälpa Åsa Kringla och hennes företag, Naturliga Växter & Kreativa Trädgårdar, att bli mer effektiva genom en ny webbapplikation. Fokus ligger på att förbättra kommunikationen med kunder och ge företaget realtidsinformation om vädret, som är avgörande för deras växthusverksamhet.

Vad jag ska bygga
Efter att ha diskuterat med Åsa och analyserat företagets behov har jag valt att implementera två huvudsakliga funktioner:

1. Chattjänst för direktkontakt med kunder
Företaget behöver ett sätt att snabbt och enkelt kommunicera med sina kunder. Genom en chattfunktion kan Åsa och hennes medarbetare svara på frågor och erbjuda rådgivning i realtid. Jag kommer att använda SignalR för att bygga chatten, så att meddelanden skickas omedelbart mellan kund och medarbetare, vilket skapar en smidigare och mer personlig upplevelse.

2. Väderprognos i realtid
Att ha koll på vädret är superviktigt för Åsa och hennes växthus. Jag kommer att integrera ett väder-API (t.ex. OpenWeatherMap) som visar realtidsprognoser direkt i appen. Detta gör att Åsa kan se vad som väntar och planera om hon behöver öppna fönstren i växthuset eller stänga. Den här funktionen kommer att göra växthusdriften mer effektiv och förhindra skador på plantorna på grund av oväntat väder.

Hur jag bygger det?
Applikationen kommer att ha en backend byggd med ASP.NET Core och SignalR för chatten. Frontend-gränssnittet kommer att byggas med ett JavaScript-ramverk, troligen Vue.js, för att göra det enkelt att använda både för Åsa och kunderna. För väderprognosen använder jag ett väder-API som integreras i applikationen för att visa aktuell väderinformation i realtid.

Klass-Diagram
![KlassDiagram](https://github.com/user-attachments/assets/160c6c74-f503-480d-8206-f1578958d2fb)

Kanban-Brädan:
![Kanban-Brädan](https://github.com/user-attachments/assets/e3bf4f5b-65ed-4737-954f-440462418001)


UseCase-diagram:
![UseCase](https://github.com/user-attachments/assets/ef2e3899-3595-45e5-8ae4-b2722286a1da)

Wireframe-Chatttjänst
![Wireframe-Chatttjänst](https://github.com/user-attachments/assets/c1437f49-8f8e-4e8b-abf2-6057cc4154cd)

Vädertjänst
![Wireframe-Vädertjänst](https://github.com/user-attachments/assets/8d5c55ef-37b8-468a-9a5c-2d1134aaa03c)
