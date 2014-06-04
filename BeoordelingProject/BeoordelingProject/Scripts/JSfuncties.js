//alle gebruikte javascript functies in het project
//door Milan Tocan

//de help knop laten meescrollen met de pagina, zodat deze altijd zichtbaar is
var _rys = jQuery.noConflict();
_rys("document").ready(function()
{
	_rys(window).scroll(function ()
	{
		if (_rys(this).scrollTop() >= 75)
		{
			_rys('#help').attr("id","scroll");
			_rys('#helpclick').attr("id","clickscroll");
		} else
		{
			_rys('#scroll').attr("id","help");
			_rys('#clickscroll').attr("id","helpclick");
		}
	});
});

//BELANGRIJK: <tr> met uitleg moet als ID dezelfde waarde hebben als de naam van het deelaspect
//Toont extra uitleg ivm puntenverdeling op beoordelingpagina
function showinfo(object)
{
	var clicked = object;
    var info = document.getElementById(clicked.innerHTML);

    if(clicked.className == "deelaspectplus")
    	clicked.className = "deelaspectmin";
    else
    	clicked.className = "deelaspectplus";

    if(info.style.display == "none")
    	info.style.display = "table-row";
    else
    	info.style.display = "none";
}

//Functie om de juiste helptekst in te laden. Tekst (met mogelijkheid voor tags/images) wordt hier ingeladen
//switch werkt op de paginanaam van in de URL
//substring wordt gebruikt om .html weg te knippen, dit zal niet nodig zijn voor ASP, aangezien er geen zichtbaar bestandstype in de addressbar is
function helptekst(pagename)
{
	var path = window.location.pathname;
	var page = path.split("/").pop();
	
	page = page.substring(0, page.length - 5);

	var helpTekst = "<Insert text here>";

	switch(page)
	{
		case "Index":
			helpTekst = "Voer je <b>gebruikersnaam</b> en <b>wachtwoord</b> in en klik op \"log in\".\n";
			helpTekst += "Gebruikersnaam of Wachtwoord vergeten? Contacteer de Administrator.";

			return helpTekst;
		break;
		case "Beoordeling":
			helpTekst = "Om meer informatie te krijgen over de beoordelingsgraden kunt u op \"+\" knop drukken.\n";
			helpTekst += "Beoordelingsaspecten kunnen meerdere deelaspecten hebben.\n";
			helpTekst += "Voor elke deelaspect moet er een punt gegeven worden!";

			return helpTekst;
		break;
		case "StudentKeuze":
			helpTekst = "Selecteer de student dat u wenst te beoordelen.\n";
			helpTekst += "U heeft de mogelijkheid om de naam op te zoeken.";

			return helpTekst;
		break;
		case "Accountbeheer":
			helpTekst = "U heeft de mogelijkheid om aan elk student één of meerdere beoordelaars toe te staan.\n";
			helpTekst += "Elk beoordelaar heeft één of meerdere rollen naar gelang de situatie. Maar elk student moet een promotor hebben.\n";
			helpTekst += "Het is ook mogelijk om bestaande accounts (beoordelaars) te kunnen wijzigen en/of toe wijzen aan een ander student. Maar opletten voor de rollen. Een student kan maar één promotor hebben!\n";

			// Is dit genoeg? Of moet er nog iets speciek erbij?

			return helpTekst;
		break;
		case "Adminpaneel":
			// Extra opties in header
			helpTekst = "Bovenaan het scherm kunt u drie pictogrammen zien.\n";
			helpTekst += "Het meeste linkse pictogram is voor statistieken te verkrijgen.\n";
			helpTekst += "Het middelste pictogram is het \"Adminpaneel\" waar u zicht momenteel bevind.\n";
			helpTekst += "En het meeste rechtse pictogram is de helper. Wat u momenteel aan het lezen bent.\n\n";

			// Form
			helpTekst += "U heeft de mogelijkheid om het e-mail adres te wijzigen en/of het wachtwoord te wijzigen van de administrator.\n";
			helpTekst += "Indien u all feedbacks automatisch in uw e-mail adres wenst te verkrijgen kunt \"Feedback verzenden naar mijn mailbox\" aanvinken.\n";
			helpTekst += "Naast bovenstaande mogelijkheden kunt u ook studentdata importeren in de database.\n";
			helpTekst += "En u kunt accounts beheren.\n";
			helpTekst += "Deze optie biedt de mogelijkheid om beoordelaars aan te maken, wijzigen en/of verwijderen.";

			return helpTekst;
		break;
		case "KiesRichting":
			helpTekst = "Selecteer de richting waar de student zich bevind.";

			return helpTekst;
		break;
		case "Overzicht":
			helpTekst = "Als administrator heeft u de mogelijkheid om alle studenten te zien per richting.\n";
			helpTekst += "U kunt kijken of de student al een tussentijdse- en/of eind evoluatie gehad heeft met de bijhorende resultaten.\n";
			helpTekst += "U ziet ook welke traject een student volgt.\n";
			helpTekst += "En u kunt de rapporten en/of feedback formulieren downloaden.";

			return helpTekst;
		break;
		default:
			return "<b>placeholder, vervangen met eventuele switch die werkt op de paginaURL of iets in die aard</b> deze tekst is te vinden in het .js bestand, tags werken ook dus we kunnen gemakkelijk images toevoegen! Joepie! nu nog wat tekst spammen om te zien of we geen problemen hebben met word-wrapping, kwestie dat de afgebeelde tekst mooi in het helpvenster blijft, we willen geen zwevende woorden hebben natuurlijk. tekst tekst tekst tekst.";
		break;
	}
}

//Help knop uitvergroten en laten meescrollen
function help(object)
{
	if(object.id == "help")
	{
		object.id = "helpclick";
		object.innerHTML = helptekst("temp");
	}
	else if(object.id == "helpclick")
	{
		object.id = "help";
		object.innerHTML = "";
	}
	else if(object.id == "scroll")
	{
		object.id = "clickscroll";
		object.innerHTML = helptekst("temp");
	}
	else
	{
		object.id = "scroll";
		object.innerHTML = "";
	}
}

//Tabel met studenten voor bepaalde richtingen tonen/verbergen
function ShowStudentTabel(object)
{
    var richting = object.innerHTML;
    var tabel;

    richting = richting.trim();

	switch(richting)
	{
		case "BaKo":
		    tabel = document.getElementsByClassName("studentlist")[0];
		    
			if(tabel.style.display == "none")
			{
				tabel.style.display = "block";
				object.className = "richtingmin";
			}
			else
			{
				tabel.style.display = "none";
				object.className = "richting";
			}
		break;
		case "BaLo":
			tabel = document.getElementsByClassName("studentlist")[1];
			if(tabel.style.display == "none")
			{
				tabel.style.display = "block";
				object.className = "richtingmin";
			}
			else
			{
				tabel.style.display = "none";
				object.className = "richting";
			}
		break;
		case "BaSo":
			tabel = document.getElementsByClassName("studentlist")[2];
			if(tabel.style.display == "none")
			{
				tabel.style.display = "block";
				object.className = "richtingmin";
			}
			else
			{
				tabel.style.display = "none";
				object.className = "richting";
			}
		break;
	}
}

//ID ophalen uit de <ul> met studentnamen/accounts
function setID(object)
{
	var listitems = document.getElementsByTagName("LI");

	for(var i = 0; i < listitems.length; i++)
	{
		listitems[i].style.background = "";
		listitems[i].style.color = "";
	}

	object.style.background = "#3CB6DF";
	object.style.color = "#FFFFFF";

	var hidden = document.getElementById("IDvalue");
	hidden.value = object.id;
}