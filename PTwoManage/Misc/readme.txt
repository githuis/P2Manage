PTwoManage intro/guide.


For at bruge programmet:
Der er hvis du har en idé om hvordan programmer fungerer allerede, eller ikke har brug for meget instruktion,
kan det være nok at følge den korte version, ellers tjek den lange.

	Kort version:
		Log ind med en bruger (Se brugere i starten af Lang version).
		Menu > Schedule > Templates and Tags.
		Lav ønskede tags, luk vinduet.
		Menu > Manage users.
		Opret Users der opfylder alle tags og ønskede kombinationer heraf, luk vinduet.
		Menu > Schedule > Templates and Tags.
		Vælg: Ugedag, starttid, sluttid og tag(s), submit. 
		Fortsæt indtil alle ønskede ShiftTemplates er oprettet, luk vinduet.
		Menu > Schedule > Generate Schedule.
		Vælg start- og slutuge samt år, klik generer (luk vinduet).
		Færdig.
		
	Lang version:

Det første der sker når programmet køres er at man bliver promptet med et login.
Der er 3 hard-codede bruger, der kan benyttes (username:password), der skelnes imellem store/små bogstaver.
	a423:123	
	Pers Biler:per
	1:1
	
Alle disse brugere benytter hver deres database. 
Indtast et username og password i de angivede felter og tryk enter (eller klik login).
Der navigeres nu i menuen til Menu > Schedule > Create Templates and Tags.
I det nyligt åbnede vindue skal der nu indtastes tags. 
Dette gøres ved at indtaste navnet på et tag i boksen og klikke på Save Tag.
	Eksempelvist kan tags være:
	Rengøring
	Opfyldning
	Åbner
	Lukker
	Kasse
	Lager Sortering

Når tag(s) er indtastet lukkes Templates and Tags vinduet.
Efter dette skal der navigeres i menuen til Menu > Manage Users.
Der skal endnu engang logges ind for at sikre at det er lederen der søger adgang til systemet.
Benyt den samme bruger som første login.

I Manage Users vinduets felter indtastes information en medarbejder.
Når alle felter (evt. med undtagelse af e-mail) er udfyldet, trykkes der på Save User.
Hvis det ønskes at redigere i eller at slette en User, skal Useren blot vælges i listen til højre, efterfulgt af et klik på Load User.
	Redigere: Informationen kan nu redigeres i, efter fulgt af at klikke på Save User for at gemme informationen.
	Slette: Tryk på Delete User.

Der skal mindst tilføjes én User til programmet før næste trin.
Det er anbefalet at alle tags er taget i brug af Users.
Hvis det forventes, at der skal være vagter med kombinationer af tags f.eks. Kasse + Lager Sortering, skal der også være mindst én User med hver af dem.
Manage Users vinduet lukkes 

Naviger til Menu > Schedule > Templates and Tags.
For at oprette en skabelon for vagtgenerering (ShiftTemplate), vælges der først en ugedag i listen til venstre.
Indtast starttid i HH:MM format. F.eks. 08:00.
Indtast sluttid i HH:MM format. F.eks. 18:45.
	Hvis der ønskes tags skal de vælges her.
	For at ShiftTemplaten får et tag med skal det blot klikkes på i listen nederst til venstre.
	Ønskes der mere end ét tag holdes 'Ctrl' nede mens der vælges de tags der skal med.
Klik nu på Submit Template.
Ønskes det at slette skabelonen skal den vælges i vinduet til højre, hvorefter der skal klikkes på Delete Template.
Der skal være mindst én ShiftTemplate før næste trin.
Luk vinduet.

Naviger til Menu > Schedule > Generate Schedule.
Vælg en start- og slutuge, Vælg år, klik på Generate Schedule.
Der kommer nu et nyt vindue op, det angiver hvor langt genereringen er. 
Når progressbaren viser aktivitet er genereringen færdig.
Vinduerne skulle gerne selv lukke når genereringen er færdig






