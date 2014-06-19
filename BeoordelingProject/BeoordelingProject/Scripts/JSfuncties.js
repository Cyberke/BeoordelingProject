//alle gebruikte javascript functies in het project
//door Milan Tocan

//de help knop laten meescrollen met de pagina, zodat deze altijd zichtbaar is
/*
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
*/

//Container hoogte instellen
function zetContainer() {
    var container = document.getElementById("container");
    var header = document.getElementById("mijnheader");
    var body = document.body;
    var html = document.documentElement;

    container.style.height = (html.height - header.clientHeight) - 20 + "px";

    //alert("Doc ClientHeight:" + myHeight + "\nHeader ClientHeight: " + header.clientHeight + "\nTotaal:" + container.style.height);

}

//BELANGRIJK: <tr> met uitleg moet als ID dezelfde waarde hebben als de naam van het deelaspect
//Toont extra uitleg ivm puntenverdeling op beoordelingpagina
function showinfo(object)
{
    var clicked = object;
    var id = clicked.innerHTML.trim();
    var info = document.getElementById(id);

    if(clicked.className == "expanderPlus")
    	clicked.className = "collapseMin";
    else
    	clicked.className = "expanderPlus";

    if(info.style.display == "none")
    	info.style.display = "table-row";
    else
    	info.style.display = "none";
}

//Functie om de juiste helptekst in te laden. Tekst (met mogelijkheid voor tags/images) wordt hier ingeladen
//switch werkt op de paginanaam van in de URL
//substring wordt gebruikt om .html weg te knippen, dit zal niet nodig zijn voor ASP, aangezien er geen zichtbaar bestandstype in de addressbar is
var path = window.location.pathname;
var array = path.split("/");
var page = array[1] + "/" + array[2];

function helptekst(pagename)
{
	var helpTekst = "<Insert text here>";

	switch(page)
	{
		case "Account/Login":
			helpTekst = "Voer je <b>gebruikersnaam</b> en <b>wachtwoord</b> in en klik op \"Log in\".\n";
			helpTekst += "Gebruikersnaam of Wachtwoord vergeten? Contacteer de Administrator.";
			return helpTekst;
		break;
		case "Beoordelaar/Beoordeling":
			helpTekst = "Om meer informatie te krijgen over de beoordelingsgraden kunt u op <b>\"+\"</b> knop drukken.\n";
			helpTekst += "Beoordelingsaspecten kunnen meerdere deelaspecten hebben.\n";
			helpTekst += "Voor elke deelaspect moet er een punt gegeven worden!";

			return helpTekst;
		break;
		case "Beoordelaar/undefined":
			helpTekst = "Selecteer de student dat u wenst te beoordelen.\n";
			helpTekst += "Naast de student moet u ook kiezen als het om een tussentijdse of een eindbeoordeling gaat";

			return helpTekst;
		break;
	    case "Accountbeheer/AddStudentRol":
	        helpTekst = "Op dit scherm kunt u een beoordelaar linken met een bepaalde student en hem voor de gekozen student een rol toekennen."
	        helpTekst += " Om een nieuwe beoordelaar aan te maken klikt u op de knop Toevoegen en vult u het formuliertje in zoals u wilt."
	        helpTekst += " U hebt natuurlijk ook de mogelijkheid om een bestaande beoordelaar te wijzigen (naam, rol voor een student of studenten zelf)"
            helpTekst += " Ten slotte kunt u de beoordelaar ook verwijderen, daarna kan deze beoordelaar niet meer inloggen om een evaluatie uit te voeren"

			return helpTekst;
		break;
		case "Adminpaneel/Index":
			// Form
			helpTekst += "U heeft de mogelijkheid om het e-mail adres te wijzigen en/of het wachtwoord te wijzigen van de administrator.\n";
			helpTekst += "Indien u feedbacks automatisch in uw e-mail adres wenst te verkrijgen kunt \"Feedback verzenden naar mijn mailbox\" aanvinken.\n";
			helpTekst += "Naast bovenstaande mogelijkheden kunt u ook studentdata importeren in de database.\n";
			helpTekst += "En u kunt accounts beheren.\n";
			helpTekst += "Deze optie biedt de mogelijkheid om beoordelaars aan te maken, wijzigen en/of verwijderen.";

			return helpTekst;
		break;
		case "KiesRichting":
			helpTekst = "Selecteer de richting waar de student zich bevind.";

			return helpTekst;
		break;
		case "Admin/Index":
			helpTekst = "Als administrator heeft u de mogelijkheid om alle studenten te zien per richting.\n";
			helpTekst += "U kunt kijken of de student al een tussentijdse- en/of eindevaluatie gehad heeft met de bijhorende resultaten.\n";
			helpTekst += "U ziet ook welke traject een student volgt.\n";
			helpTekst += "En u kunt de rapporten en/of feedback formulieren downloaden.";

			return helpTekst;
			break;
	    case "/undefined":
	        helpTekst = "Als administrator heeft u de mogelijkheid om alle studenten te zien per richting.\n";
	        helpTekst += "U kunt kijken of de student al een tussentijdse- en/of eindevaluatie gehad heeft met de bijhorende resultaten.\n";
	        helpTekst += "U ziet ook welke traject een student volgt.\n";
	        helpTekst += "En u kunt de rapporten en/of feedback formulieren downloaden.";

	        return helpTekst;
	        break;
	    case "Student/undefined":
	        helpTekst = "Kies eerst een .csv bestand met de nodige studentgegevens door op de knop 'Bladeren' te klikken."
            helpTekst += " Om dit bestand te importeren in de applicatie klikt u op de knop 'Importeren'"

	        return helpTekst;
	        break;
	    case "Student/Charts":
	        helpTekst = "Deze pagina toont de statistieken weer voor de punten van de studenten. "
	        helpTekst += "Er zijn 2 grafieken te zien per richting, voor tussentijds beoordeling en voor eindbeoordeling. "
	        helpTekst += "In de grafiek is voor iedere puntengroep een andere kleur."
	        return helpTekst;
	        break;
		default:
		    helpTekst = "Er is iets misgelopen met de help."
		    return helpTekst;
		    break;
	}
}

//Help knop uitvergroten en laten meescrollen
function help(object)
{
	if(object.id == "help")
	{
		object.id = "helpclick";
		object.innerHTML = helptekst(page);
	}
	else if(object.id == "helpclick")
	{
		object.id = "help";
		object.innerHTML = "";
	}
	else if(object.id == "scroll")
	{
		object.id = "clickscroll";
		object.innerHTML = helptekst(page);
	}
	else
	{
		object.id = "scroll";
		object.innerHTML = "";
	}
}

//Toevoegen user formulier tonen/verbergen
function toggleUserToevoegen()
{
    formdiv = document.getElementById("inklapdiv");

    if (formdiv.style.display == "none") {
        formdiv.style.display = "block";
        formdiv.style.opacity = 1;
    }
    else {
        formdiv.style.display = "none";
        formdiv.style.opacity = 0;
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

// Extra opties die alleen te zien zijn voor de administrator (opties, statistieken)
function redirectTo(controllerName) {
    window.location = '../' + controllerName + '/Index';
}