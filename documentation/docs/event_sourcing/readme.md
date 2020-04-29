```
This article is written in dutch, a translated version will be posted soon™.
```
# Event Sourcing & CQRS

**Inhoud**
[[toc]]

## Introductie
Event-driven architecturen in het algemeen en event sourcing zijn de laatste jaren in populariteit toegenomen. Dit komt grotendeels door de opkomst van microservices en de behoefte voor het bouwen van schaalbare systemen die modulair en veerkrachtig zijn.

Event sourcing is een procedure die beschrijft hoe data op een specifieke manier opgeslagen kan worden. Wanneer event sourcing wordt gebruikt wordt niet de huidige staat van de data opgeslagen, maar alle gebeurtenissen die plaats hebben gevonden om tot deze staat te komen. 
<figure>
  <img src='../../assets/images/basic_es.png'>
  <figcaption>Acties uitgevoerd in het systeem worden opgevangen in de vorm van events.</figcaption>
</figure>

We vangen deze gebeurtenissen op in de vorm van *events*, dit zijn simpele, [immutable﹖](#termonilogie) objecten die een uitgevoerde actie beschrijven.  

Events worden opgeslagen in het *Event Log*, dit is een [append-only﹖](#termonilogie) lijst met alle events die plaats hebben gevonden in het systeem. Het Event Log wordt beschouwd als de primaire databron van het systeem. De huidige staat van de applicatie is hier slechts een afgeleide van, we kunnen deze eenvoudig herbouwen door events uit het log, in chronologische volgorde, opnieuw af te spelen. 

<figure>
  <img src='../../assets/images/event_log.png'>
  <figcaption>Events worden chronologisch toegepast om tot de huidige staat van het object te komen.</figcaption>
</figure>

#### Business Constraints
Bij het uitvoeren van een operatie, is het vaak nodig om deze te valideren. Wanneer we een nieuw Concert willen inplannen, zal bijvoorbeeld eerst moeten worden gecontroleerd of de locatie wel beschikbaar is op de gewenste datum. Om achter de beschikbaarheid van de concert locatie te komen zullen we een subgroep events moeten afspelen. Het afspelen van deze events wordt ook wel *projecting* genoemd. Projecting is een in-memory proces waarbij een groep events wordt omgezet in een gestructureerde representatie. Het resultaat hiervan noemen we een *projection*, in dit geval heeft de projection de vorm van een concertprogramma. 

Hoewel dit proces op eerste gezicht langzaam lijkt, valt dit in de praktijk reuze mee. Het groepje events dat wordt afgespeeld is zo klein mogelijk, alleen de events relevant voor de projection worden opgevraagd. Meestal gebeurt dit in één database call. 

#### Snapshots
Om het groepje events dat voor iedere projection moet worden afgespeeld nog kleiner te maken, worden vaak *snapshots* toegepast. Een snapshot geeft een overzicht van de staat op een bepaald moment in tijd. Wanneer we een groep events afspelen kunnen we dit doen vanaf een snapshot, hierdoor voorkomen we dat iedere keer alle events opnieuw afgespeeld moeten worden.

## Waarom?
#### Limities van CRUD-systemen
-	Systemen gebasseerd op [CRUD-functies﹖](#termonilogie)  voeren operaties rechtsreeks op de databron uit, dit kan de performance van de applicatie verlagen.
-	Limiteert de schaalbaarheid. Lees- en schrijfacties kunnen niet eenvoudig en onafhankelijk van elkaar worden geschaald.
-	Tenzij er een aanvullende [auditing﹖](#termonilogie) functionaliteit wordt geïmplementeerd, die een aantekening van elke uitgevoerde operatie in een log bijhoudt. Gaat de historie van de applicatie verloren.
-	Hoe meer gebruikers, hoe groter de kans op data (concurrency) conflicten.

#### Voordelen van Event Sourcing

-	Auditing (Audit log): Elke aanpassing aan de data is vastgelegd. Betrouwbaar omdat het eventlog de primaire databron van het systeem is.
-	Historische staat: De opgeslagen events beschrijven niet alleen de huidige staat van de applicatie, maar ook hoe de applicatie tot dit punt is gekomen. Dit maakt het mogelijk de applicatie te herstellen naar een moment in het verleden.
-	Debugging: Speel de events uit de productie af naar een development omgeving. Zo vaak als nodig. We zijn niet meer hoofdzakelijk afhankelijk van logging.
-	Microservices: helpt bij het beheren van data op een schaalbare manier in gedistribueerde systemen.
-	Verschillende data schema's, CQRS: het scheiden van commands en queries maakt het mogelijk om verschillende query-systemen te hebben (met verschillende schema's).
-	Maakt het mogelijk de staat van de applicatie *in memory* te bewaren, wanneer het systeem opnieuw herstart wordt kan deze vanuit het eventlog herbouwd worden. Dit kan de performance van de applicatie verbeteren.
-	De data biedt mogelijkheden voor toekomstige nieuwe applicaties, functionaliteiten en rapportages.

## CQRS
<figure>
  <img src='../../assets/images/cqrs.png'>
  <figcaption>CQRS: Het scheiden van commands en queries.</figcaption>
</figure>
[beschrijving-cqrs]
## Problemen
[problemen]
## Termonilogie

| Term            |  Beschrijving   |
| ---------------- | --- |
| **Immutable object** |   Aan het einde toegevoegd. Er kan geen data gewijzigd of verwijderd worden.  |
| **Append-only**       |  Onveranderbaar. Het object kan niet gewijzigd worden.       |
| **CRUD**| Create, Read, Update, Delete. De 4 basis functionaliteiten van een database. |
| **Auditing** | Het bijhouden van de activiteiten in een systeem. |

## Bronnen
[bronnen]