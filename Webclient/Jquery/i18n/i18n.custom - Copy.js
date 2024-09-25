
var cacheTag = 'VRV';

var languages = [{
	language: "en",
	tag: 'en',
	dir: 'ltr',
	source: '../../i18n/en/en.json'
}, {
		language: "np",
		tag: 'np',
		dir: 'ltr',
		source: '../../i18n/np/np.json'
	}
];


// ---------------------------------- Do not edit functions below --------------------------------------

function updateLanguageTexts(language) {
	$("#languageSelector option:selected").prop("selected", false);
	$("#languageSelector option[value=" + language + "]").prop("selected", true);
	var t = languages.filter(d => d.language === language);
	//
	$.i18n().load(t[0].source, language).done(function () {
		if (['ar'].includes(t[0].language)) {
			$("html").attr("dir", t[0].dir);
		}
		$("html").attr("dir", t[0].dir);
		$("html").attr("lang", t[0].tag);
		$('html').i18n();
		$(document).trigger("langSelectorEvent", [language]);
	});
};

// Enable debug
$.i18n.debug = true;

$(document).ready(function ($) {
	'use strict';
	//
	languageDropdownSelector();

	// GET URL Params
	var sLang = localStorage.getItem(cacheTag + "-default-lang") || "en";
	var query = window.location.search.substring(1);
	var qs = parse_query_string(query);
	if (qs.Locale !== undefined) {
		sLang = qs.Locale;
	}
	//
	updateLanguageTexts(cacheLanguage(sLang, true));
	$('#languageSelector').on('change', function () {
		return updateLanguageTexts(cacheLanguage(this.value, true));
	});
});

function languageLoader() {
	var l = {};
	languages.filter(d => {
		l[d.language] = d.source;
	});
	return l;
};

function cacheLanguage(lang = "en", setLocale = false) {
	var slang = languages.filter(d => d.language === lang);
	if (slang.length != 0) {
		arabicToggle(lang);
		localStorage.setItem(cacheTag + "-default-lang", lang);
	} else {
		lang = "en";
		arabicToggle("en");
		localStorage.setItem(cacheTag + "-default-lang", "en");
	}
	//
	if (setLocale) {
		var i18n = $.i18n();
		i18n.locale = lang;
	}
	//
	let searchParams = new URLSearchParams(window.location.search);
	searchParams.set("Locale", lang);
	if (window.history.replaceState) {
		const url = window.location.protocol
			+ "//" + window.location.host
			+ window.location.pathname
			+ "?"
			+ searchParams.toString();

		window.history.replaceState({
			path: url
		}, "", url)
	}
	return lang;
};

function languageDropdownSelector() {
	var selectList = document.getElementById("languageSelector");
	if (selectList != null) {
		for (var i = 0; i < languages.length; i++) {
			var option = document.createElement("option");
			option.value = languages[i].language;
			option.text = languages[i].language.toUpperCase();
			if (languages[i].language === (localStorage.getItem(cacheTag + "-default-lang") || "en")) {
				option.setAttribute('selected', true);
			}
			selectList.appendChild(option);
		}
    }
};

function pageContentSwitcher(event, lang) {
	var promise = $.ajax({
		url: "./i18n/" + lang + "/" + event.data.page + ".html",
		cache: true,
		success: function (result) {
			if (event.data.domId !== undefined) {
				$(event.data.domId).html(result);
			} else {
				return result;
            }
		},
		error: function (XMLHttpRequest, textStatus, errorThrown) {
			console.error("An error occurred: " + textStatus + " - " + errorThrown);
		}
	});
	return promise;
};

function loadjscssfile(filename, filetype) {
	if (filetype == "js") { //if filename is a external JavaScript file
		var fileref = document.createElement('script');
		fileref.setAttribute("type", "text/javascript");
		fileref.setAttribute("src", filename);
	}
	else if (filetype == "css") { //if filename is an external CSS file
		var fileref = document.createElement("link");
		fileref.setAttribute("rel", "stylesheet");
		fileref.setAttribute("type", "text/css");
		fileref.setAttribute("href", filename);
	}
	if (typeof fileref != "undefined") {
		if (filetype == "css") {
			var c = document.getElementsByTagName("head")[0].children;
			for (var i = 0; i < c.length; i++) {
				if (c[i].tagName === 'LINK') {
					var v = c[i].attributes[0].value.split("/");
					if (v != undefined && (v[1] == "main.css" || v[2] == "main.css")) {
						c[i].insertAdjacentElement("afterEnd", fileref);
					}
				}
			};
		} else {
			document.getElementsByTagName("head")[0].appendChild(fileref);
		}
	}
}

var filesAdded = []; //list of files already added

function checkloadjscssfile(filename, filetype) {
	if (filesAdded.indexOf(filename) == -1) {
		loadjscssfile(filename, filetype);
		filesAdded.push(filename); // List of files added in the form "[filename1],[filename2],etc"
	}
}

function removeFileAdded(filename) {
	for (var i = 0; i < filesAdded.length; i++) {
		if (filesAdded[i] === filename) {
			filesAdded.splice(i, 1);
			i--;
		}
	}
}

function createjscssfile(filename, filetype) {
	if (filetype == "js") { //if filename is a external JavaScript file
		var fileref = document.createElement('script');
		fileref.setAttribute("type", "text/javascript");
		fileref.setAttribute("src", filename);
	}
	else if (filetype == "css") { //if filename is an external CSS file
		var fileref = document.createElement("link");
		fileref.setAttribute("rel", "stylesheet");
		fileref.setAttribute("type", "text/css");
		fileref.setAttribute("href", filename);
	}
	return fileref;
}

function replacejscssfile(oldfilename, newfilename, filetype) {
	var targetelement = (filetype == "js") ? "script" : (filetype == "css") ? "link" : "none"; //determine element type to create nodelist using
	var targetattr = (filetype == "js") ? "src" : (filetype == "css") ? "href" : "none"; //determine corresponding attribute to test for
	var allsuspects = document.getElementsByTagName(targetelement);
	for (var i = allsuspects.length; i >= 0; i--) { //search backwards within nodelist for matching elements to remove
		if (allsuspects[i] && allsuspects[i].getAttribute(targetattr) != null && allsuspects[i].getAttribute(targetattr).indexOf(oldfilename) != -1) {
			var newelement = createjscssfile(newfilename, filetype);
			allsuspects[i].parentNode.replaceChild(newelement, allsuspects[i]);
		}
	}
}

function removejscssfile(filename, filetype) {
	var targetelement = (filetype == "js") ? "script" : (filetype == "css") ? "link" : "none" //determine element type to create nodelist from
	var targetattr = (filetype == "js") ? "src" : (filetype == "css") ? "href" : "none" //determine corresponding attribute to test for
	var allsuspects = document.getElementsByTagName(targetelement)
	for (var i = allsuspects.length; i >= 0; i--) { //search backwards within nodelist for matching elements to remove
		if (allsuspects[i] && allsuspects[i].getAttribute(targetattr) != null && allsuspects[i].getAttribute(targetattr).indexOf(filename) != -1) {
			allsuspects[i].parentNode.removeChild(allsuspects[i]) //remove element by calling parentNode.removeChild()
		}
	}
}

function arabicToggle(newLang) {
	if (newLang == 'ar') {
		replacejscssfile("Css/bootstrap.min.css", "Css/bootstrap.rtl.min.css", "css");
		checkloadjscssfile("Css/main_arabic.css", "css");
	} else {
		replacejscssfile("Css/bootstrap.rtl.min.css", "Css/bootstrap.min.css", "css");
		removejscssfile("Css/main_arabic.css", "css");
		removeFileAdded("Css/main_arabic.css");
	}
	return newLang;
}

function parse_query_string(query) {
	var vars = query.split("&");
	var query_string = {};
	for (var i = 0; i < vars.length; i++) {
		var pair = vars[i].split("=");
		var key = decodeURIComponent(pair[0]);
		var value = decodeURIComponent(pair[1]);
		// If first entry with this name
		if (typeof query_string[key] === "undefined") {
			query_string[key] = decodeURIComponent(value);
			// If second entry with this name
		} else if (typeof query_string[key] === "string") {
			var arr = [query_string[key], decodeURIComponent(value)];
			query_string[key] = arr;
			// If third or later entry with this name
		} else {
			query_string[key].push(decodeURIComponent(value));
		}
	}
	return query_string;
}